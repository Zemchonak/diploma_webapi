using System;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using FitnessCenterManagement.Api.Identity;
using FitnessCenterManagement.Api.Models;
using FitnessCenterManagement.BusinessLogic.Dtos;
using FitnessCenterManagement.BusinessLogic.Exceptions;
using FitnessCenterManagement.BusinessLogic.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FitnessCenterManagement.Api.Controllers
{
    [Route("api/[controller]")]
    public class WeeklyEventsController : ControllerBase
    {
        private readonly ISchedulesService _schedulesService;
        private readonly IMapper _mapper;

        public WeeklyEventsController(ISchedulesService schedulesService, IMapper mapper)
        {
            _schedulesService = schedulesService;
            _mapper = mapper;
        }

        /// <summary>
        /// Gets all the weekly events.
        /// </summary>
        /// <response code="200">Get is successful, the response contains data about the weekly events.</response>
        [HttpGet("")]
        [AllowAnonymous]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> Index(int part = 0)
        {
            var items = await _schedulesService.GetAllWeeklyEventsAsync();

            return part == 0 ?
                Ok(items) :
                Ok(items.Where(s => s.FitnessEventId == part).ToList());
        }

        /// <summary>
        /// Gets the info about the weekly event.
        /// </summary>
        /// <response code="200">Get is successful, the response contains data about the weekly event.</response>
        /// <response code="404">The weekly event wasn't found.</response>
        [HttpGet("{id}")]
        [AllowAnonymous]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> IndexById([FromRoute] int id)
        {
            try
            {
                var model = _mapper.Map<WeeklyEventModel>(await _schedulesService.GetWeeklyEventByIdAsync(id));
                return Ok(model);
            }
            catch (BusinessLogicException)
            {
                return NotFound();
            }
        }

        /// <summary>
        /// Updates the weekly event.
        /// </summary>
        /// <response code="200">Successful operation.</response>
        /// <response code="400">There was an error occured while updating the weekly event.</response>
        /// <response code="401">Update of the weekly event is available for authorized users.</response>
        [HttpPut("{id}")]
        [Authorize(Roles = Identity.IdentityConstants.ManagerRole)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] WeeklyEventModel model)
        {
            try
            {
                if (model is null || id != model.Id)
                {
                    throw new BusinessLogicException("", fieldName: "", null);
                }

                await _schedulesService.UpdateWeeklyEventAsync(_mapper.Map<WeeklyEventDto>(model));
            }
            catch (ValidationException ex)
            {
                return BadRequest(new ErrorViewModel { ErrorMessage = ex.Message, ErrorAttribute = ex.FieldName });
            }
            catch (BusinessLogicException ex)
            {
                return BadRequest(new ErrorViewModel { ErrorMessage = ex.Message, ErrorAttribute = ex.FieldName });
            }

            return Ok();
        }

        /// <summary>
        /// Creates an weekly event.
        /// </summary>
        /// <response code="200">Successful operation.</response>
        /// <response code="400">There was an error occured while creating the weekly event.</response>
        /// <response code="401">Creation of the weekly event is available for authorized users.</response>
        [HttpPost("")]
        [Authorize(Roles = Identity.IdentityConstants.ManagerRole)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> Create([FromBody] WeeklyEventModel model)
        {
            int result;

            try
            {
                if (model is null)
                {
                    throw new BusinessLogicException("", fieldName: "", null);
                }

                result = await _schedulesService.CreateWeeklyEventAsync(_mapper.Map<WeeklyEventDto>(model));
            }
            catch (ValidationException ex)
            {
                return BadRequest(new ErrorViewModel { ErrorMessage = ex.Message, ErrorAttribute = ex.FieldName });
            }
            catch (BusinessLogicException ex)
            {
                return BadRequest(new ErrorViewModel { ErrorMessage = ex.Message, ErrorAttribute = ex.FieldName });
            }

            return Ok(result);
        }

        /// <summary>
        /// Deletes the weekly event.
        /// </summary>
        /// <response code="200">Delete is successful.</response>
        /// <response code="400">There was an error occured while deleting the weekly event.</response>
        /// <response code="401">Deletion of the weekly event is available for authorized users.</response>
        [HttpDelete("{id}")]
        [Authorize(Roles = Identity.IdentityConstants.ManagerRole)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            try
            {
                await _schedulesService.DeleteWeeklyEventAsync(id);
            }
            catch (ValidationException ex)
            {
                return BadRequest(new ErrorViewModel { ErrorMessage = ex.Message, ErrorAttribute = ex.FieldName });
            }
            catch (BusinessLogicException ex)
            {
                return BadRequest(new ErrorViewModel { ErrorMessage = ex.Message, ErrorAttribute = ex.FieldName });
            }

            return Ok();
        }
    }
}

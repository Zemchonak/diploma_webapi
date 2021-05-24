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
    public class AbonementsController : ControllerBase
    {
        private readonly IAbonementsService _abonementsService;
        private readonly IMapper _mapper;

        public AbonementsController(IAbonementsService abonementsService, IMapper mapper)
        {
            _abonementsService = abonementsService;
            _mapper = mapper;
        }

        /// <summary>
        /// Gets all the abonements.
        /// </summary>
        /// <response code="200">Get is successful, the response contains data about the abonements.</response>
        [HttpGet("")]
        [AllowAnonymous]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> Index(string part = "")
        {
            if (!User.Identity.IsAuthenticated)
            {
                return Unauthorized();
            }

            var fitnessEvents = await _abonementsService.GetAllAbonementsAsync();

            return string.IsNullOrEmpty(part) ?
                Ok(fitnessEvents) :
                Ok(fitnessEvents.Where(s => s.Name.Contains(part, System.StringComparison.OrdinalIgnoreCase)).ToList());
        }

        /// <summary>
        /// Gets the info about the abonement.
        /// </summary>
        /// <response code="200">Get is successful, the response contains data about the abonement.</response>
        /// <response code="404">The fitness event wasn't found.</response>
        [HttpGet("{id}")]
        [AllowAnonymous]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> IndexById([FromRoute] int id)
        {
            SpecializationModel model;

            try
            {
                model = _mapper.Map<SpecializationModel>(await _abonementsService.GetAbonementByIdAsync(id));
            }
            catch (BusinessLogicException)
            {
                return NotFound();
            }

            return Ok(model);
        }

        /// <summary>
        /// Updates the abonement.
        /// </summary>
        /// <response code="200">Successful operation.</response>
        /// <response code="400">There was an error occured while updating the abonement.</response>
        /// <response code="401">Update of the abonement is available for authorized users.</response>
        [HttpPut("{id}")]
        [Authorize(Roles = Identity.IdentityConstants.ManagerRole)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] AbonementModel model)
        {
            try
            {
                if (model is null || id != model.Id)
                {
                    throw new BusinessLogicException("", fieldName: "", null);
                }

                await _abonementsService.UpdateAbonementAsync(_mapper.Map<AbonementDto>(model));
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
        /// Creates an abonement.
        /// </summary>
        /// <response code="200">Successful operation.</response>
        /// <response code="400">There was an error occured while creating the abonement.</response>
        /// <response code="401">Creation of the abonement is available for authorized users.</response>
        [HttpPost("")]
        [Authorize(Roles = Identity.IdentityConstants.ManagerRole)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> Create([FromBody] AbonementModel model)
        {
            int result;

            try
            {
                if (model is null)
                {
                    throw new BusinessLogicException("", fieldName: "", null);
                }

                result = await _abonementsService.CreateAbonementAsync(_mapper.Map<AbonementDto>(model));
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
        /// Deletes the abonement.
        /// </summary>
        /// <response code="200">Delete is successful.</response>
        /// <response code="400">There was an error occured while deleting the abonement.</response>
        /// <response code="401">Deletion of the abonement is available for authorized users.</response>
        [HttpDelete("{id}")]
        [Authorize(Roles = Identity.IdentityConstants.ManagerRole)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            try
            {
                await _abonementsService.DeleteAbonementAsync(id);
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

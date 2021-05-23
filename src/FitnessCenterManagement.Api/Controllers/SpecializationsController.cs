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
    public class SpecializationsController : ControllerBase
    {
        private readonly IFitnessCatalogsService _fitnessCatalogsService;
        private readonly IMapper _mapper;

        public SpecializationsController(IFitnessCatalogsService fitnessCatalogsService, IMapper mapper)
        {
            _fitnessCatalogsService = fitnessCatalogsService;
            _mapper = mapper;
        }

        /// <summary>
        /// Gets all the specializations.
        /// </summary>
        /// <response code="200">Get is successful, the response contains data about the specializations.</response>
        /// <response code="401">The info about the specializations is available only for authorized users.</response>
        [HttpGet("")]
        [Authorize(Roles = IdentityConstants.ManagerRole
            + "," + IdentityConstants.MarketerRole
            + "," + IdentityConstants.DirectorRole)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> Index(string part = "")
        {
            if (!User.Identity.IsAuthenticated)
            {
                return Unauthorized();
            }

            var specializations = await _fitnessCatalogsService.GetAllSpecializationsAsync();

            return string.IsNullOrEmpty(part) ?
                Ok(specializations) :
                Ok(specializations.Where(s => s.Info.Contains(part,
                    System.StringComparison.OrdinalIgnoreCase)).ToList());
        }

        /// <summary>
        /// Gets the info about the specialization.
        /// </summary>
        /// <response code="200">Get is successful, the response contains data about the specialization.</response>
        /// <response code="404">The specialization wasn't found.</response>
        [HttpGet("{id}")]
        [AllowAnonymous]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Index([FromRoute] int id)
        {
            SpecializationModel model;

            try
            {
                model = _mapper.Map<SpecializationModel>(await _fitnessCatalogsService.GetSpecializationByIdAsync(id));
            }
            catch (BusinessLogicException)
            {
                return NotFound();
            }

            return Ok(model);
        }

        /// <summary>
        /// Updates the specialization.
        /// </summary>
        /// <response code="200">Successful operation.</response>
        /// <response code="400">There was an error occured while updating the specialization.</response>
        /// <response code="401">Update of the specialization is available for authorized users.</response>
        [HttpPut("{id}")]
        [Authorize(Roles = Identity.IdentityConstants.ManagerRole)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] SpecializationModel model)
        {
            try
            {
                if (model is null || id != model.Id)
                {
                    throw new BusinessLogicException("", fieldName: "", null);
                }

                await _fitnessCatalogsService.UpdateSpecializationAsync(_mapper.Map<SpecializationDto>(model));
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
        /// Creates a specialization.
        /// </summary>
        /// <response code="200">Successful operation.</response>
        /// <response code="400">There was an error occured while creating the specialization.</response>
        /// <response code="401">Creation of the specialization is available for authorized users.</response>
        [HttpPost("")]
        [Authorize(Roles = Identity.IdentityConstants.ManagerRole)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> Create([FromBody] SpecializationModel model)
        {
            int result;

            try
            {
                if (model is null)
                {
                    throw new BusinessLogicException("", fieldName: "", null);
                }

                result = await _fitnessCatalogsService.CreateSpecializationAsync(_mapper.Map<SpecializationDto>(model));
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
        /// Deletes the specialization.
        /// </summary>
        /// <response code="200">Delete is successful.</response>
        /// <response code="400">There was an error occured while deleting the specialization.</response>
        /// <response code="401">Deletion of the specialization is available for authorized users.</response>
        [HttpDelete("{id}")]
        [Authorize(Roles = Identity.IdentityConstants.ManagerRole)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            try
            {
                await _fitnessCatalogsService.DeleteSpecializationAsync(id);
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

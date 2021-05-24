using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
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
    public class ServicesController : ControllerBase
    {
        private readonly IFitnessCatalogsService _fitnessCatalogsService;
        private readonly IMapper _mapper;

        public ServicesController(
            IFitnessCatalogsService fitnessCatalogsService, IMapper mapper)
        {
            _fitnessCatalogsService = fitnessCatalogsService;
            _mapper = mapper;
        }

        /// <summary>
        /// Gets all the services.
        /// </summary>
        /// <response code="200">Get is successful, the response contains data about all the services.</response>
        [HttpGet("")]
        [AllowAnonymous]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> Index(string part = "")
        {
            var services = await _fitnessCatalogsService.GetAllServicesAsync();
            var specializations = await _fitnessCatalogsService.GetAllSpecializationsAsync();

            var servicesJson = services.Select(s => new ServiceModel
            {
                Id = s.Id,
                Description = s.Description,
                Name = s.Name,
                SpecializationId = s.SpecializationId,
                Price = s.Price,
                SpecializationInfo = specializations.FirstOrDefault(sp => sp.Id == s.SpecializationId).Info,
            }).ToList().AsReadOnly();

            return string.IsNullOrEmpty(part) ?
                Ok(servicesJson) :
                Ok(servicesJson
                .Where(s => s.Name.Contains(part, System.StringComparison.OrdinalIgnoreCase) ||
                s.Description.Contains(part, System.StringComparison.OrdinalIgnoreCase)).ToList());
        }

        /// <summary>
        /// Gets the info about the service.
        /// </summary>
        /// <response code="200">Get is successful, the response contains data about the service.</response>
        /// <response code="404">The service wasn't found.</response>
        [HttpGet("{id}")]
        [AllowAnonymous]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Index([FromRoute] int id)
        {
            try
            {
                var model = _mapper.Map<ServiceModel>(await _fitnessCatalogsService.GetServiceByIdAsync(id));
                return Ok(model);
            }
            catch (BusinessLogicException)
            {
                return NotFound();
            }
        }

        /// <summary>
        /// Updates the service.
        /// </summary>
        /// <response code="200">Update is successful.</response>
        /// <response code="400">There was an error occured while updating the service.</response>
        /// <response code="401">Update of the service is available for authorized users.</response>
        [HttpPut("{id}")]
        [Authorize(Roles = Identity.IdentityConstants.MarketerRole)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] ServiceModel model)
        {
            try
            {
                if (model is null || id != model.Id)
                {
                    throw new BusinessLogicException("", fieldName: "", null);
                }

                await _fitnessCatalogsService.UpdateServiceAsync(_mapper.Map<ServiceDto>(model));
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
        /// Creates a service.
        /// </summary>
        /// <response code="200">Creation is successful, the response contains the ID of the created entity.</response>
        /// <response code="400">There was an error occured while creating the service.</response>
        /// <response code="401">Creation of the service is available for authorized users.</response>
        [HttpPost("")]
        [Authorize(Roles = Identity.IdentityConstants.MarketerRole)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> Create([FromBody] ServiceModel model)
        {
            int result;

            try
            {
                if (model is null)
                {
                    throw new BusinessLogicException("", fieldName: "", null);
                }

                result = await _fitnessCatalogsService.CreateServiceAsync(_mapper.Map<ServiceDto>(model));
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
        /// Deletes the service.
        /// </summary>
        /// <response code="200">Delete is successful.</response>
        /// <response code="400">There was an error occured while deleting the service.</response>
        /// <response code="401">Deletion of the service is available for authorized users.</response>
        [HttpDelete("{id}")]
        [Authorize(Roles = Identity.IdentityConstants.MarketerRole)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            try
            {
                await _fitnessCatalogsService.DeleteServiceAsync(id);
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

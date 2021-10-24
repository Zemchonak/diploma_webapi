using System;
using System.Collections.Generic;
using System.IO;
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
        private readonly string[] _availableExtensions = { ".png", ".jpg", ".jpeg", ".bmp" };

        private readonly ISchedulesService _schedulesService;
        private readonly IFitnessCatalogsService _fitnessCatalogsService;
        private readonly IAbonementsService _abonementsService;
        private readonly IMapper _mapper;

        public AbonementsController(IAbonementsService abonementsService,
            IFitnessCatalogsService fitnessCatalogsService,
            ISchedulesService schedulesService, IMapper mapper)
        {
            _schedulesService = schedulesService;
            _fitnessCatalogsService = fitnessCatalogsService;
            _abonementsService = abonementsService;
            _mapper = mapper;
        }

        /// <summary>
        /// Gets the price of the abonement.
        /// </summary>
        /// <response code="200">Get is successful, the response contains data about the price.</response>
        [HttpGet("{id}/price")]
        [AllowAnonymous]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> PriceById([FromRoute] int id)
        {
            var abonement = await _abonementsService.GetAbonementByIdAsync(id);
            var abonementfitnessEvents = await _schedulesService.GetAbonementFitnessEventsByAbonementIdAsync(id);

            decimal price = 0.0m;

            foreach (var one in abonementfitnessEvents)
            {
                var serviceId = (await _fitnessCatalogsService.GetFitnessEventByIdAsync(one.FitnessEventId)).ServiceId;
                price += (await _fitnessCatalogsService.GetServiceByIdAsync(serviceId)).Price;
            }

            return Ok(price * abonement.Coefficient);
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
            var items = await _abonementsService.GetAllAbonementsAsync();

            return string.IsNullOrEmpty(part) ?
                Ok(items) :
                Ok(items.Where(s => s.Name.Contains(part, System.StringComparison.OrdinalIgnoreCase)).ToList());
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
            try
            {
                var model = _mapper.Map<AbonementModel>(await _abonementsService.GetAbonementByIdAsync(id));
                return Ok(model);
            }
            catch (BusinessLogicException)
            {
                return NotFound();
            }
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

                model.ImageName = ImageProcessingContants.DefaultAbonementImageFileName;
                model.Status = Enums.AbonementStatus.Disabled;

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
                var abonement = await _abonementsService.GetAbonementByIdAsync(id);

                await _abonementsService.DeleteAbonementAsync(id);

                System.IO.File.Delete(Environment.CurrentDirectory + ImageProcessingContants.DefaultAbonementsImagesFolder + abonement.ImageName);
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
        /// Gets the abonement image.
        /// </summary>
        /// <response code="200">Get is successful, the response contains the image of the abonement.</response>
        /// <response code="404">The specified venue wasn't found.</response>
        [AllowAnonymous]
        [HttpGet("{id}/image")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetAbonementImageAsync([FromRoute] int id)
        {
            try
            {
                var abonement = await _abonementsService.GetAbonementByIdAsync(id);
                var imageName = abonement.ImageName;
                var folderName = Environment.CurrentDirectory + ImageProcessingContants.DefaultAbonementsImagesFolder;
                return PhysicalFile(CheckImageAvailability(folderName, imageName), "image/jpeg");
            }
            catch (BusinessLogicException)
            {
                return NotFound();
            }
        }

        /// <summary>
        /// Updates the abonement image.
        /// </summary>
        /// <response code="200">The image was updated successfully.</response>
        /// <response code="400">Corrupted file.</response>
        /// <response code="404">Abonement wasn't found.</response>
        [HttpPut("{id}/image")]
        [Authorize(Roles = Identity.IdentityConstants.ManagerRole)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UploadAbonementImageAsync([FromRoute] int id, [FromForm(Name = "file")] IFormFile file)
        {
            if (file is null)
            {
                return BadRequest(file);
            }

            var abonement = await _abonementsService.GetAbonementByIdAsync(id);
            if (abonement is null)
            {
                return NotFound(file);
            }

            var extension = Path.GetExtension(file.FileName);
            if (_availableExtensions.Contains(extension))
            {
                var guid = Guid.NewGuid();
                await SaveOrReplaceFile(abonement.ImageName, file, guid.ToString() + extension);
                abonement.ImageName = guid.ToString() + extension;
                await _abonementsService.UpdateAbonementAsync(abonement);
                return Ok();
            }

            return BadRequest(file);
        }

        private static string CheckImageAvailability(string folder, string filename)
        {
            var newImagePath = folder + filename;

            return System.IO.File.Exists(newImagePath)
                ? newImagePath
                : folder + ImageProcessingContants.DefaultAbonementImageNotAvailableFileName;
        }

        private static async Task SaveOrReplaceFile(string oldAbonementImagePath, IFormFile file, string newFileName)
        {
            var folder = Environment.CurrentDirectory + ImageProcessingContants.DefaultAbonementsImagesFolder;
            using (var stream = new FileStream(folder + newFileName, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            if (oldAbonementImagePath != ImageProcessingContants.DefaultAbonementImageFileName
                && oldAbonementImagePath != ImageProcessingContants.DefaultAbonementImageNotAvailableFileName
                && System.IO.File.Exists(Environment.CurrentDirectory
                    + ImageProcessingContants.DefaultAbonementsImagesFolder
                    + oldAbonementImagePath))
            {
                System.IO.File.Delete(folder + oldAbonementImagePath);
            }
        }
    }
}

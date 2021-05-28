using System;
using System.IO;
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
    public class VenuesController : Controller
    {
        private readonly string[] _availableExtensions = { ".png", ".jpg", ".jpeg", ".bmp" };

        private readonly IFitnessCatalogsService _fitnessCatalogsService;
        private readonly IMapper _mapper;
        private readonly IQrService _qrcodeGeneratorService;

        public VenuesController(
            IFitnessCatalogsService fitnessCatalogsService, IQrService qrcodeGeneratorService, IMapper mapper)
        {
            _fitnessCatalogsService = fitnessCatalogsService;
            _mapper = mapper;
            _qrcodeGeneratorService = qrcodeGeneratorService;
        }

        /// <summary>
        /// Gets all the venues.
        /// </summary>
        /// <response code="200">Get is successful, the response contains data about all the venues.</response>
        [HttpGet("")]
        [AllowAnonymous]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> Index(string part = "")
        {
            var items = await _fitnessCatalogsService.GetAllVenuesAsync();

            return string.IsNullOrEmpty(part) ?
                Ok(items) :
                Ok(items
                .Where(s => s.Name.Contains(part, System.StringComparison.OrdinalIgnoreCase) ||
                s.Location.Contains(part, System.StringComparison.OrdinalIgnoreCase)).ToList());
        }

        /// <summary>
        /// Gets the info about the venue.
        /// </summary>
        /// <response code="200">Get is successful, the response contains data about the venue.</response>
        /// <response code="404">The venue wasn't found.</response>
        [HttpGet("{id}")]
        [AllowAnonymous]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Index([FromRoute] int id)
        {
            VenueModel model;

            try
            {
                model = _mapper.Map<VenueModel>(await _fitnessCatalogsService.GetVenueByIdAsync(id));
            }
            catch (BusinessLogicException)
            {
                return NotFound();
            }

            return Ok(model);
        }

        /// <summary>
        /// Updates the venue.
        /// </summary>
        /// <response code="200">Update is successful.</response>
        /// <response code="400">There was an error occured while updating the venue.</response>
        /// <response code="401">Update of the venue is available for authorized users.</response>
        [HttpPut("{id}")]
        [Authorize(Roles = Identity.IdentityConstants.ManagerRole)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] VenueModel model)
        {
            try
            {
                if (model is null || id != model.Id)
                {
                    throw new BusinessLogicException("", fieldName: "", null);
                }

                await _fitnessCatalogsService.UpdateVenueAsync(_mapper.Map<VenueDto>(model));
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
        /// Creates a venue.
        /// </summary>
        /// <response code="200">Creation is successful, the response contains the ID of the created entity.</response>
        /// <response code="400">There was an error occured while creating the venue.</response>
        /// <response code="401">Creation of the venue is available for authorized users.</response>
        [HttpPost("")]
        [Authorize(Roles = Identity.IdentityConstants.ManagerRole)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> Create([FromBody] VenueModel model)
        {
            int result;

            try
            {
                if (model is null)
                {
                    throw new BusinessLogicException("", fieldName: "", null);
                }

                model.ImageName = ImageProcessingContants.DefaultVenueImageFileName;
                model.QrCodeId = Guid.NewGuid().ToString();
                result = await _fitnessCatalogsService.CreateVenueAsync(_mapper.Map<VenueDto>(model));
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
        /// Deletes the venue.
        /// </summary>
        /// <response code="200">Delete is successful.</response>
        /// <response code="400">There was an error occured while deleting the venue.</response>
        /// <response code="401">Deletion of the venue is available for authorized users.</response>
        [HttpDelete("{id}")]
        [Authorize(Roles = Identity.IdentityConstants.ManagerRole)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            try
            {
                await _fitnessCatalogsService.DeleteVenueAsync(id);
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
        /// Generates the venue QR.
        /// </summary>
        /// <response code="200">Get is successful, the response contains the QR ode of the venue.</response>
        /// <response code="401">Venue QR code is available only for authorized users.</response>
        /// <response code="404">The specified venue wasn't found.</response>
        [Authorize]
        [HttpGet("{venueId}/qr")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetVenueQrImageAsync([FromRoute] int venueId)
        {
            if (User.IsInRole(Identity.IdentityConstants.ManagerRole) || User.IsInRole(Identity.IdentityConstants.DirectorRole))
            {
                try
                {
                    var venue = await _fitnessCatalogsService.GetVenueByIdAsync(venueId);

                    var qrpath = $"{Environment.CurrentDirectory}{ImageProcessingContants.ContentFolder}{venue.QrCodeId}.jpg";

                    _qrcodeGeneratorService.CreateQrCode($"{Identity.IdentityConstants.VenuePrefix}{venue.QrCodeId}")
                           .Save(qrpath);

                    return PhysicalFile(qrpath, "image/jpg");
                }
                catch (BusinessLogicException)
                {
                    return NotFound();
                }
            }

            return NotFound();
        }

        /// <summary>
        /// Gets the venue image.
        /// </summary>
        /// <response code="200">Get is successful, the response contains the image of the venue.</response>
        /// <response code="404">The specified venue wasn't found.</response>
        [AllowAnonymous]
        [HttpGet("{id}/image")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetVenueImageAsync([FromRoute] int id)
        {
            try
            {
                var venue = await _fitnessCatalogsService.GetVenueByIdAsync(id);
                var imageName = venue.ImageName;
                var folderName = Environment.CurrentDirectory + ImageProcessingContants.DefaultVenuesImagesFolder;
                return PhysicalFile(CheckImageAvailability(folderName, imageName), "image/jpeg");
            }
            catch (BusinessLogicException)
            {
                return NotFound();
            }
        }

        /// <summary>
        /// Updates the venue image.
        /// </summary>
        /// <response code="200">The image was updated successfully.</response>
        /// <response code="400">Corrupted file.</response>
        /// <response code="404">Venue wasn't found.</response>
        [HttpPut("{id}/image")]
        [Authorize(Roles = Identity.IdentityConstants.ManagerRole)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UploadVenueImageAsync([FromRoute] int id, [FromForm(Name = "file")] IFormFile file)
        {
            if (file is null)
            {
                return BadRequest(file);
            }

            var venue = await _fitnessCatalogsService.GetVenueByIdAsync(id);
            if (venue is null)
            {
                return NotFound(file);
            }

            var extension = Path.GetExtension(file.FileName);
            if (_availableExtensions.Contains(extension))
            {
                var guid = Guid.NewGuid();
                await SaveOrReplaceFile(venue.ImageName, file, guid.ToString() + extension);
                venue.ImageName = guid.ToString() + extension;
                await _fitnessCatalogsService.UpdateVenueAsync(venue);
                return Ok();
            }

            return BadRequest(file);
        }

        private static string CheckImageAvailability(string folder, string filename)
        {
            var newImagePath = folder + filename;

            return System.IO.File.Exists(newImagePath)
                ? newImagePath
                : folder + ImageProcessingContants.DefaultVenueImageNotAvailableFileName;
        }

        private static async Task SaveOrReplaceFile(string oldVenueImagePath, IFormFile file, string newFileName)
        {
            var folder = Environment.CurrentDirectory + ImageProcessingContants.DefaultVenuesImagesFolder;
            using (var stream = new FileStream(folder + newFileName, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            if (oldVenueImagePath != ImageProcessingContants.DefaultVenueImageFileName
                && oldVenueImagePath != ImageProcessingContants.DefaultVenueImageNotAvailableFileName
                && System.IO.File.Exists(Environment.CurrentDirectory
                    + ImageProcessingContants.DefaultVenuesImagesFolder
                    + oldVenueImagePath))
            {
                System.IO.File.Delete(folder + oldVenueImagePath);
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using FitnessCenterManagement.Api.Identity;
using FitnessCenterManagement.Api.Models;
using FitnessCenterManagement.BusinessLogic.Exceptions;
using FitnessCenterManagement.BusinessLogic.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace FitnessCenterManagement.Api.Controllers
{
    [Route("api/[controller]")]
    public class ProfileController : ControllerBase
    {
        private readonly string[] _availableExtensions = { ".png", ".jpg", ".jpeg", ".bmp" };

        private readonly UserManager<User> _userManager;

        private readonly ILocalizationService _localizationService;

        public ProfileController(
            UserManager<User> userManager,
            ILocalizationService localizationService)
        {
            _localizationService = localizationService;
            _userManager = userManager;
        }

        /// <summary>
        /// Gets current user's profile info.
        /// </summary>
        /// <response code="200">Get is successful, the response contains data about the user's profile.</response>
        /// <response code="401">Profile info is available only for authorized users.</response>
        [HttpGet("")]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> Index()
        {
            if (!User.Identity.IsAuthenticated)
            {
                return Unauthorized();
            }

            var userId = HttpContext.User.FindFirst(Identity.IdentityConstants.UserIdClaimType).Value;
            var user = await _userManager.FindByIdAsync(userId);
            var lang = await _localizationService.GetByIdAsync(user.LanguageId);
            var role = await GetUserRoleAsync(user);

            var model = new ProfileModel
            {
                Surname = user.Surname,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Address = user.Address,
                Email = user.Email,
                Balance = user.Balance,
                Language = lang.Code,
                LanguageName = lang.Name,
                Languages = await _localizationService.GetAllAsync(),
                RoleName = role,
            };

            return Ok(model);
        }

        /// <summary>
        /// Change current user's balance.
        /// </summary>
        /// <response code="200">Change is successful.</response>
        /// <response code="400">There was an error occured while updating user's balance.</response>
        /// <response code="401">Balance changes are available only for authorized users.</response>
        [HttpPut("balance")]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> ChangeCurrentUserBalance([FromBody] ProfileChangeBalanceModel model)
        {
            if (!User.Identity.IsAuthenticated)
            {
                return Unauthorized();
            }

            if (model is null || model.Balance < Identity.IdentityConstants.MinBalance)
            {
                return BadRequest(FormErrorMessage(new BusinessLogicException(BusinessLogic.Resources.StringRes.NegativeBalance)));
            }

            var userId = HttpContext.User.FindFirst(Identity.IdentityConstants.UserIdClaimType).Value;
            var user = await _userManager.FindByIdAsync(userId);
            user.Balance = model.Balance;

            var result = await _userManager.UpdateAsync(user);

            return result.Succeeded? Ok() : BadRequest();
        }

        /// <summary>
        /// Updates current user's profile info.
        /// </summary>
        /// <response code="200">Profile info was successfully updated.</response>
        /// <response code="400">Profile info wasn't updated due to an error occured.</response>
        /// <response code="401">Profile info is available only for authorized users.</response>
        [HttpPut("")]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> Edit([FromBody] ProfileEditModel model)
        {
            if (!User.Identity.IsAuthenticated)
            {
                return Unauthorized();
            }

            if (model is null)
            {
                return BadRequest(FormErrorMessage(new BusinessLogicException(BusinessLogic.Resources.StringRes.NullEntityMsg)));
            }

            var userId = HttpContext.User.FindFirst(Identity.IdentityConstants.UserIdClaimType).Value;
            var user = await _userManager.FindByIdAsync(userId);

            if (string.IsNullOrEmpty(model.Surname) || string.IsNullOrEmpty(model.FirstName) ||
                string.IsNullOrEmpty(model.Email) || model.Balance < Api.Identity.IdentityConstants.MinBalance)
            {
                return BadRequest(FormErrorMessage(new BusinessLogicException(BusinessLogic.Resources.StringRes.InvalidProfileInfoMsg)));
            }

            var userWithMail = await _userManager.FindByEmailAsync(model.Email);

            if (userWithMail != null && userWithMail.Id != user.Id)
            {
                return BadRequest(FormErrorMessage(new BusinessLogicException(BusinessLogic.Resources.StringRes.EmailIsTakenMsg)));
            }

            user.Surname = model.Surname;
            user.FirstName = model.FirstName;
            user.LastName = model.LastName;
            user.Address = model.Address;
            user.Email = model.Email;
            user.UserName = model.Email;
            user.Balance = model.Balance;

            await _userManager.UpdateAsync(user);

            return Ok();
        }

        /// <summary>
        /// Gets the profile image.
        /// </summary>
        /// <response code="200">Get is successful, the response contains the image of the profile.</response>
        /// <response code="404">The specified profile wasn't found.</response>
        [AllowAnonymous]
        [HttpGet("{userId}/image")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetUserAvatarImageAsync([FromRoute] string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user is null)
            {
                return NotFound();
            }

            var imageName = user.AvatarName;
            var folderName = Environment.CurrentDirectory + ImageProcessingContants.DefaultAvatarsImagesFolder;
            return PhysicalFile(CheckImageAvailability(folderName, imageName), "image/jpeg");
        }

         /// <summary>
         /// Updates the profile image.
         /// </summary>
         /// <response code="200">The image was updated successfully.</response>
         /// <response code="400">The specified user wasn't found.</response>
         /// <response code="404">User wasn't found.</response>
        [HttpPut("avatar")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UploadUserAvatarImageAsync([FromForm(Name = "file")] IFormFile file)
        {
            if (file is null)
            {
                return BadRequest(file);
            }

            var user = await _userManager.FindByIdAsync(HttpContext.User.FindFirst(Identity.IdentityConstants.UserIdClaimType).Value);
            if (user is null)
            {
                return NotFound(file);
            }

            var extension = Path.GetExtension(file.FileName);
            if (_availableExtensions.Contains(extension))
            {
                var guid = Guid.NewGuid();
                await SaveOrReplaceFile(user.AvatarName, file, guid.ToString() + extension);
                user.AvatarName = guid.ToString() + extension;
                await _userManager.UpdateAsync(user);
                return Ok();
            }

            return BadRequest(file);
        }

        private static string CheckImageAvailability(string folder, string filename)
        {
            var newImagePath = folder + filename;

            return System.IO.File.Exists(newImagePath)
                ? newImagePath
                : folder + ImageProcessingContants.DefaultAvatarImageNotAvailableFileName;
        }

        private static async Task SaveOrReplaceFile(string oldEventImagePath, IFormFile file, string newFileName)
        {
            var folder = Environment.CurrentDirectory + ImageProcessingContants.DefaultAvatarsImagesFolder;
            using (var stream = new FileStream(folder + newFileName, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            if (oldEventImagePath != ImageProcessingContants.DefaultAvatarImageFileName
                && oldEventImagePath != ImageProcessingContants.DefaultAvatarImageNotAvailableFileName
                && System.IO.File.Exists(Environment.CurrentDirectory
                    + ImageProcessingContants.DefaultAvatarsImagesFolder
                    + oldEventImagePath))
            {
                System.IO.File.Delete(folder + oldEventImagePath);
            }
        }

        private async Task<string> GetUserRoleAsync(User user)
        {
            var isTrainer = await _userManager.IsInRoleAsync(user, Api.Identity.IdentityConstants.TrainerRole);
            if (isTrainer)
            {
                return Api.Identity.IdentityConstants.TrainerRole;
            }

            var isManager = await _userManager.IsInRoleAsync(user, Api.Identity.IdentityConstants.ManagerRole);
            if (isManager)
            {
                return Api.Identity.IdentityConstants.ManagerRole;
            }

            var isMarketer = await _userManager.IsInRoleAsync(user, Api.Identity.IdentityConstants.MarketerRole);
            if (isMarketer)
            {
                return Api.Identity.IdentityConstants.MarketerRole;
            }

            var isDirector = await _userManager.IsInRoleAsync(user, Api.Identity.IdentityConstants.DirectorRole);
            if (isDirector)
            {
                return Api.Identity.IdentityConstants.DirectorRole;
            }

            return Api.Identity.IdentityConstants.UserRole;
        }

        protected static Dictionary<string, string> FormErrorMessage(Exception exception, string errorAttributeName = "")
        {
            if (exception is null)
            {
                throw new ArgumentNullException(nameof(exception));
            }

            return new Dictionary<string, string>
            {
                ["description"] = exception.Message,
                ["attribute"] = errorAttributeName,
            };
        }
    }
}

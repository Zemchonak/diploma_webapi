using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using FitnessCenterManagement.Api.Identity;
using FitnessCenterManagement.Api.Models;
using FitnessCenterManagement.BusinessLogic.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace FitnessCenterManagement.Api.Controllers
{
    [Route("api/[controller]")]
    public class LocalizationController : ControllerBase
    {
        private readonly UserManager<User> _userManager;

        private readonly ILocalizationService _localizationService;

        public LocalizationController(ILocalizationService localizationService, UserManager<User> userManager)
        {
            _userManager = userManager;
            _localizationService = localizationService;
        }

        /// <summary>
        /// Gets current user's culture info.
        /// </summary>
        /// <response code="200">Get is successful, the response contains culture info.</response>
        /// <response code="401">Getting culture info is available only for authorized users.</response>
        [HttpGet("")]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult> GetUserSavedCulture()
        {
            var cultures = await _localizationService.GetAllAsync();

            var userName = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var user = await _userManager.FindByNameAsync(userName);

            var savedCulture = cultures.First(cul => cul.Id == user.LanguageId);

            return Ok(savedCulture);
        }

        /// <summary>
        /// Sets current user's language.
        /// </summary>
        /// <response code="200">Set is successful.</response>
        /// <response code="400">There was an error occured while setting user's language.</response>
        /// <response code="401">Setting language is available only for authorized users.</response>
        [HttpPut("")]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult> SetLanguage([FromBody] ChangeLanguageModel model)
        {
            var cultures = await _localizationService.GetAllAsync();

            if (!cultures.Any(c => c.Code == model.LanguageCode))
            {
                return BadRequest();
            }

            var userName = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var user = await _userManager.FindByNameAsync(userName);
            user.LanguageId = cultures.FirstOrDefault(c => c.Code == model.LanguageCode).Id;

            var result = await _userManager.UpdateAsync(user);

            return result.Succeeded? Ok() : BadRequest();
        }
    }
}

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
    public class ReviewsController : ControllerBase
    {
        private readonly IUsersService _usersService;
        private readonly IMapper _mapper;

        public ReviewsController(IUsersService usersService, IMapper mapper)
        {
            _usersService = usersService;
            _mapper = mapper;
        }

        /// <summary>
        /// Gets all the reviews.
        /// </summary>
        /// <response code="200">Get is successful, the response contains data about the reviews.</response>
        [HttpGet("")]
        [AllowAnonymous]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> Index(string part = "")
        {
            if (!User.Identity.IsAuthenticated)
            {
                return Unauthorized();
            }

            var items = await _usersService.GetAllReviewsAsync();

            return string.IsNullOrEmpty(part)?
                Ok(items) :
                Ok(items.Where(s => s.Text.Contains(part, StringComparison.OrdinalIgnoreCase)).ToList());
        }

        /// <summary>
        /// Gets the info about the review.
        /// </summary>
        /// <response code="200">Get is successful, the response contains data about the review.</response>
        /// <response code="404">The review wasn't found.</response>
        [HttpGet("{id}")]
        [AllowAnonymous]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> IndexById([FromRoute] int id)
        {
            try
            {
                var model = _mapper.Map<ReviewModel>(await _usersService.GetReviewByIdAsync(id));
                return Ok(model);
            }
            catch (BusinessLogicException)
            {
                return NotFound();
            }
        }

        /// <summary>
        /// Updates the review.
        /// </summary>
        /// <response code="200">Successful operation.</response>
        /// <response code="400">There was an error occured while updating the review.</response>
        /// <response code="401">Update of the review is available for authorized users.</response>
        [HttpPut("{id}")]
        [Authorize(Roles = Identity.IdentityConstants.MarketerRole)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] ReviewModel model)
        {
            try
            {
                if (model is null || id != model.Id)
                {
                    throw new BusinessLogicException("", fieldName: "", null);
                }

                await _usersService.UpdateReviewAsync(_mapper.Map<ReviewDto>(model));
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
        /// Creates an review.
        /// </summary>
        /// <response code="200">Successful operation.</response>
        /// <response code="400">There was an error occured while creating the review.</response>
        /// <response code="401">Creation of the review is available for authorized users.</response>
        [HttpPost("")]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> Create([FromBody] ReviewModel model)
        {
            int result;

            try
            {
                if (model is null)
                {
                    throw new BusinessLogicException("", fieldName: "", null);
                }

                result = await _usersService.CreateReviewAsync(_mapper.Map<ReviewDto>(model));
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
        /// Deletes the review.
        /// </summary>
        /// <response code="200">Delete is successful.</response>
        /// <response code="400">There was an error occured while deleting the review.</response>
        /// <response code="401">Deletion of the review is available for authorized users.</response>
        [HttpDelete("{id}")]
        [Authorize(Roles = Identity.IdentityConstants.MarketerRole)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            try
            {
                await _usersService.DeleteReviewAsync(id);
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

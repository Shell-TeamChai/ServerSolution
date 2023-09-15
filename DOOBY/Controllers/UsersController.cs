using DOOBY.GloablExceptions;
using DOOBY.Models;
using DOOBY.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DOOBY.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private IUser _user;

        public UsersController(IUser _user)
        {
            this._user = _user;
        }

        [AllowAnonymous]
        [HttpPost("login")]
        [ProducesResponseType(typeof(AuthenticateResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<AuthenticateResponse>> Authenticate(AuthenticateRequest model)
        {
            if (model != null)
            {
                var response = await _user.Authenticate(model);
                return Ok(response);
            }
            else
            {
                return BadRequest("No Data Posted");
            }
        }

        [AllowAnonymous]
        [HttpPost("register")]
        [ProducesResponseType(typeof(User), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<User>> AddUser(User user)
        {
            try
            {
                var newUser = await _user.AddUser(user);

                if (newUser != null)
                {
                    return Ok(new { message = "User Created Successfully" });
                }
                else
                {
                    return NoContent();
                }
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ExceptionDetails.exceptionMessages[1] });
            }
        }

        [HttpGet("{user_id}")]
        public async Task<ActionResult<User>> GetUserDetailById(int user_id)
        {
            var result = await _user.GetUserDetailById(user_id);

            return result;
        }

    }
}

using DOOBY.Models;
using DOOBY.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DOOBY.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        IAdmin _admin;

        public AdminController(IAdmin admin) {
            _admin = admin;
        }

        [HttpGet("{adminId}")]
        [Authorize(Roles = Roles.Admin)]
        [ProducesResponseType(typeof(Admin), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Admin>> GetAdminDetailById(int adminId)
        {
            try
            {
                var result = await _admin.GetAdminDetailById(adminId);

                if (result == null)
                {
                    return NotFound(new { message = $"No admin found for {adminId}" });
                }

                return Ok(result);

            }
            catch (Exception ex)
            {
                return NotFound(new { message = ex.Message }); ;
            }
        }

        //[Authorize(Roles = Roles.Admin)]
        [HttpPut]
        [ProducesResponseType(typeof(Admin), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Admin), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Admin>> AddNewAdmin(Admin admin)
        {
            try
            {
                var newUser = await _admin.AddNewAdmin(admin);

                if (newUser != null)
                {
                    return Ok(new { message = "Admin info updated Successfully", user = newUser });
                }
                else
                {
                    return NoContent();
                }
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }
    }
}

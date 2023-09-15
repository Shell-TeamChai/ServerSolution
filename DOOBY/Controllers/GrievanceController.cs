using DOOBY.DTOs;
using DOOBY.Models;
using DOOBY.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DOOBY.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class GrievanceController : ControllerBase
    {

        IGrievance _grievance;

        public GrievanceController(IGrievance grievance)
        {
            _grievance = grievance;
        }

        [HttpGet]
        [Authorize(Roles = "Admin, Customer")]
        [ProducesResponseType(typeof(Grievance), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Grievance), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<List<Grievance>>> GetAllGrievances()
        {
            var result = await _grievance.GetAllGrievances();

            return result;
        }

        [HttpGet("{cust_id}")]
        [Authorize(Roles = "Admin, Customer")]
        [ProducesResponseType(typeof(Grievance), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Grievance), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<List<Grievance>> GetAllGrievancesFromCustomer(int cust_id)
        {
            var result = await _grievance.GetAllGrievancesFromCustomer(cust_id);

            return result;
        }

        [HttpPost]
        [Authorize(Roles = Roles.Customer)]
        [ProducesResponseType(typeof(Grievance), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Grievance), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<Grievance> PostGrievance(CustomerGrievanceDTO response)
        {
            var result = await _grievance.PostGrievance(response);

            return result;
        }
    }
}


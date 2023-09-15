using DOOBY.Models;
using DOOBY.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DOOBY.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {

        private ICustomer _customer;


        public CustomerController(ICustomer customer)
        {
            _customer = customer;
        }

        [HttpGet("{custId}")]
        [Authorize(Roles = Roles.Customer)]
        [ProducesResponseType(typeof(Customer), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Customer>> GetCustomerDetailById(int custId)
        {
            try
            {
                var result = await _customer.GetCustomerDetailById(custId);

                if (result == null)
                {
                    return NotFound(new { message = $"No customer found for {custId}" });
                }

                return Ok(result);

            }
            catch (Exception ex)
            {
                return NotFound(new { message = ex.Message }); ;
            }
        }

        [HttpPut]
        [ProducesResponseType(typeof(Customer), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Customer), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Customer>> AddNewCustomer(Customer customer)
        {
            try
            {
                var newUser = await _customer.AddNewCustomer(customer);

                if (newUser != null)
                {
                    return Ok(new { message = "Customer info updated Successfully", user = newUser });
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

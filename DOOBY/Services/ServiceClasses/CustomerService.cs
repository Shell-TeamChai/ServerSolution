using DOOBY.GloablExceptions;
using DOOBY.Models;
using DOOBY.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DOOBY.Services.ServiceClasses
{
    public class CustomerService : ICustomer
    {
        private CaseStudyContext _context;

        public CustomerService(CaseStudyContext context)
        {
            _context = context;
        }

        public async Task<Customer> GetCustomerDetailById(int custId)
        {
            var findUserById = await _context.Users.Where(item => item.UserId == custId).ToListAsync();

            if (findUserById == null)
            {
                throw new NullReferenceException(ExceptionDetails.exceptionMessages[0]);
            }

            var result = await _context.Customers.FirstOrDefaultAsync(item => item.CustId == custId);

            if (result == null)
            {
                throw new Exception(ExceptionDetails.exceptionMessages[1]);
            }
            var feedbacks = await _context.Feedbacks.Where(item => item.UserId == custId).ToListAsync();

            result.Feedbacks = feedbacks;
            return result;
        }


        public async Task<Customer> AddNewCustomer(Customer customer)
        {
            var _customer = await _context.Customers.FirstOrDefaultAsync(item => item.CustId == customer.CustId);

            if (_customer == null)
            {
                throw new Exception(ExceptionDetails.exceptionMessages[0]);
            }

            _customer.Fname = customer.Fname;
            _customer.Lname = customer.Lname;
            _customer.PhoneNum = customer.PhoneNum;

            await _context.SaveChangesAsync();

            var newCustomer = await _context.Customers.Where(item => item.CustId == customer.CustId).ToListAsync();

            if (newCustomer == null)
            {
                throw new Exception(ExceptionDetails.exceptionMessages[2]);
            }
            return newCustomer[0];
        }
    }
}

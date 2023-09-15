using DOOBY.Models;

namespace DOOBY.Services.Interfaces
{
    public interface ICustomer
    {
        public Task<Customer> GetCustomerDetailById(int id);

        public Task<Customer> AddNewCustomer(Customer customer);
    }
}

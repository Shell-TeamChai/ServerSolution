using DOOBY.Models;

namespace DOOBY.Services.Interfaces
{
    public interface IAdmin
    {

        public Task<Admin> GetAdminDetailById(int id);

        public Task<Admin> AddNewAdmin(Admin admin);
    }
}

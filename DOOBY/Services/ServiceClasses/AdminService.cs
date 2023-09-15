using DOOBY.GloablExceptions;
using DOOBY.Models;
using DOOBY.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DOOBY.Services.ServiceClasses
{
    public class AdminService : IAdmin
    {
        private CaseStudyContext _context;

        public AdminService(CaseStudyContext context)
        {
            _context = context;
        }

        public async Task<Admin> GetAdminDetailById(int id)
        {
            var findUserById = await _context.Users.Where(item => item.UserId == id).ToListAsync();

            if (findUserById == null)
            {
                throw new NullReferenceException(ExceptionDetails.exceptionMessages[0]);
            }

            var result = await _context.Admins.FirstOrDefaultAsync(item => item.AdminId == id);

            if (result == null)
            {
                throw new Exception(ExceptionDetails.exceptionMessages[1]);
            }
            
            return result;
        }


        public async Task<Admin> AddNewAdmin(Admin admin)
        {
            var _admin = await _context.Admins.FirstOrDefaultAsync(item => item.AdminId == admin.AdminId);

            if(_admin == null)
            {
                throw new Exception(ExceptionDetails.exceptionMessages[0]);
            }

            _admin.Fname = admin.Fname;
            _admin.Lname = admin.Lname;
            _admin.Permissions = admin.Permissions;

            await _context.SaveChangesAsync();

            var newCustomer = await _context.Admins.Where(item => item.AdminId == _admin.AdminId).ToListAsync();

            if (newCustomer == null)
            {
                throw new Exception(ExceptionDetails.exceptionMessages[0]);
            }
            return newCustomer[0];
        }
    }
}

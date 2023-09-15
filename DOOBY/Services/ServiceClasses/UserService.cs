using DOOBY.GloablExceptions;
using DOOBY.Models;
using DOOBY.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DOOBY.Services.ServiceClasses
{
    public class UserService : IUser
    {

        private CaseStudyContext _context;
        private IToken _tokenGenerator;


        public UserService(CaseStudyContext context, IToken tokenGenerator)
        {
            _context = context;
            _tokenGenerator = tokenGenerator;
        }

        public Task<AuthenticateResponse> Authenticate(AuthenticateRequest model)
        {
            var user = _context.Users.SingleOrDefault(x => x.Email == model.Email);

            // validate
            if (user == null || user.Password != model.Password)
                throw new Exception("Username or password is incorrect");

            user.Type = (user.Type == Roles.Admin ? Roles.Admin : Roles.Customer);
            // authentication successful so generate jwt token
            var jwtToken = _tokenGenerator.GenerateToken(user.Email, user.Type);

            var newResponse = new AuthenticateResponse(user, jwtToken);
            return Task.FromResult(newResponse);
        }

        public async Task<User> AddUser(User user)
        {
            var _user = await _context.Users.FirstOrDefaultAsync(item => item.UserId == user.UserId);
            
            if(_user != null)
            {
                throw new Exception(ExceptionDetails.exceptionMessages[16]);
            }

            await _context.Users.AddAsync(user);

            if(user.Type == Roles.Admin)
            {
                _context.Admins.Add(new Admin()
                {
                    AdminId = user.UserId,
                    Fname = "",
                    Lname = "",
                    Permissions = { }
                });
            } 
            else
            {
                _context.Customers.Add(new Customer()
                {
                    CustId = user.UserId,
                    Fname = "",
                    Lname = "",
                    PhoneNum = ""
                });
            }
            await _context.SaveChangesAsync();

            var newUser = await _context.Users.FirstOrDefaultAsync(item => item.Email == user.Email);

            if (newUser == null)
            {
                throw new Exception(ExceptionDetails.exceptionMessages[17]);
            }
            return newUser;
        }


        public async Task<List<User>> DeleteUser(string username)
        {
            var ruser = await _context.Users.FindAsync(username);

            if (ruser != null)
            {
                _context.Users.Remove(ruser);
                _context.SaveChanges();
                return await _context.Users.ToListAsync();
            }
            else
            {
                throw new Exception(ExceptionDetails.exceptionMessages[0]);
            }
        }

        public async Task<User> GetUserDetailById(int user_id)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.UserId == user_id);
            
            if (user != null)
            {
                return user;
            }
            else
            {
                throw new Exception(ExceptionDetails.exceptionMessages[1]);
            }
        }

    }
}

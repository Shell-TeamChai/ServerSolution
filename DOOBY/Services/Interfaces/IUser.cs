using DOOBY.Models;

namespace DOOBY.Services.Interfaces
{
    public interface IUser
    {
        public Task<User> GetUserDetailById(int user_id);
        
        public Task<AuthenticateResponse> Authenticate(AuthenticateRequest model);
        public Task<User> AddUser(User user);

    }
}

using System.Data;

namespace DOOBY.Models
{
    public class AuthenticateResponse
    {
        public int UserId { get; set; }
        public string Email { get; set; }
        public string Type { get; set; }
        public string Token { get; set; }

        public AuthenticateResponse(User user, string token)
        {
            UserId = user.UserId;
            Email = user.Email;
            Type = user.Type;
            Token = token;
        }
    }
}

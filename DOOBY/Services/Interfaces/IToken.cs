namespace DOOBY.Services.Interfaces
{
    public interface IToken
    {
        string GenerateToken(string email, string type);
    }
}

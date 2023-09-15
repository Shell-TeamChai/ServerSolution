namespace DOOBY.Models.CustomPartialClasses
{
    public class User
    {
        public int UserId { get; set; }

        public string Type { get; set; } = null!;

        public string Email { get; set; } = null!;

        public virtual Admin? Admin { get; set; }
    }
}

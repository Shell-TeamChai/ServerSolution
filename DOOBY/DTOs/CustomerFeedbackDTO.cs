namespace DOOBY.DTOs
{
    public class CustomerFeedbackDTO
    {
        public int UserId { get; set; }

        public int? Rating { get; set; }

        public string? Description { get; set; }

        public int? StationId { get; set; }

        public DateOnly? CreatedAt { get; set; }

        public DateOnly? LastEdit { get; set; }

    }
}

namespace DOOBY.DTOs
{
    public class CustomerGrievanceDTO
    {
        public int GrievanceId { get; set; }

        public int UserId { get; set; }

        public string Status { get; set; } = null!;

        public string? Description { get; set; }

        public int? StationId { get; set; }

        public DateOnly? CreatedAt { get; set; }

        public DateOnly? LastEdit { get; set; }
    }
}

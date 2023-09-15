using DOOBY.DTOs;
using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace DOOBY.Models;

public partial class Grievance
{
    public int GrievanceId { get; set; }

    public int UserId { get; set; }

    public string Status { get; set; } = null!;

    public string? Description { get; set; }

    public int? StationId { get; set; }

    public DateOnly? CreatedAt { get; set; }

    public DateOnly? LastEdit { get; set; }

    [JsonIgnore]
    public virtual Customer User { get; set; } = null!;

    public Grievance() { }

    public Grievance(CustomerGrievanceDTO customerGrievanceDTO, Customer customer)
    {
        GrievanceId = customerGrievanceDTO.GrievanceId;
        UserId = customerGrievanceDTO.UserId;
        Status = customerGrievanceDTO.Status;
        Description = customerGrievanceDTO.Description;
        StationId = customerGrievanceDTO.StationId;
        CreatedAt = customerGrievanceDTO.CreatedAt;
        LastEdit = customerGrievanceDTO.LastEdit;
        this.User = customer;
    }
}

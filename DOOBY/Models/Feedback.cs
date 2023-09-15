using DOOBY.DTOs;
using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace DOOBY.Models;

public partial class Feedback
{
    public int FeedbackId { get; set; }

    public int? UserId { get; set; }

    public int? Rating { get; set; }

    public string? Description { get; set; }

    public int? StationId { get; set; }

    public DateOnly? CreatedAt { get; set; }

    public DateOnly? LastEdit { get; set; }

    //[JsonIgnore]
    public virtual Customer? User { get; set; }

    public Feedback() { }

    public Feedback(int feedbackId, CustomerFeedbackDTO customerFeedbackDTO, Customer customer)
    {
        FeedbackId = feedbackId;
        UserId = customerFeedbackDTO.UserId;
        Rating = customerFeedbackDTO.Rating;
        Description = customerFeedbackDTO.Description;
        StationId = customerFeedbackDTO.StationId;
        CreatedAt = customerFeedbackDTO.CreatedAt;
        LastEdit = customerFeedbackDTO.LastEdit;
        //this.User = customer;
    }
}

using DOOBY.DTOs;
using DOOBY.Models;

namespace DOOBY.Services.Interfaces
{
    public interface IFeedback
    {
        public Task<List<Feedback>> GetAllFeedbacks();

        public Task<List<Feedback>> GetAllFeedbacksFromCustomer(int cust_id);

        public Task<Feedback> PostFeedback(CustomerFeedbackDTO response);
    }
}

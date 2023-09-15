using DOOBY.DTOs;
using DOOBY.GloablExceptions;
using DOOBY.Models;
using DOOBY.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DOOBY.Services.ServiceClasses
{
    public class FeedbackService : IFeedback
    {

        private CaseStudyContext _context;

        public FeedbackService(CaseStudyContext context)
        {
            _context = context;
        }

        public Task<List<Feedback>> GetAllFeedbacks()
        {
            var result = _context.Feedbacks.Include(item => item.User).ToListAsync();

            if (result == null)
            {
                throw new Exception(ExceptionDetails.exceptionMessages[7]);
            }
            return result;
        }

        public async Task<List<Feedback>> GetAllFeedbacksFromCustomer(int cust_id)
        {
            var res = await _context.Feedbacks.Include(item => item.User).Where(item => item.UserId == cust_id).ToListAsync();

            if (res == null)
            {
                throw new Exception(ExceptionDetails.exceptionMessages[8]);
            }

            return res;
        }

        public async Task<Feedback> PostFeedback(CustomerFeedbackDTO response)
        {
            var res = await _context.Customers.Where(item => item.CustId == response.UserId).ToListAsync();

            if (res == null)
            {
                throw new Exception(ExceptionDetails.exceptionMessages[0]);
            }

            var id = _context.Feedbacks.Max(item => item.FeedbackId) + 1;
            response.CreatedAt = DateOnly.FromDateTime(DateTime.Now);

            Feedback feedback = new Feedback(id, response, res[0]);
            
            await _context.Feedbacks.AddAsync(feedback);
            await _context.SaveChangesAsync();
            var result = await _context.Feedbacks.FindAsync(feedback.FeedbackId);

            if (result == null)
            {
                throw new Exception(ExceptionDetails.exceptionMessages[9]);
            }
            return result;
        }
    }
}

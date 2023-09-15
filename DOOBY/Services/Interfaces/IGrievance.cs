using DOOBY.DTOs;
using DOOBY.Models;

namespace DOOBY.Services.Interfaces
{
    public interface IGrievance
    {
        public Task<List<Grievance>> GetAllGrievances();

        public Task<List<Grievance>> GetAllGrievancesFromCustomer(int cust_id);

        public Task<Grievance> PostGrievance(CustomerGrievanceDTO response);
    }
}

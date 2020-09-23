using System.Collections.Generic;
using System.Threading.Tasks;
using JobBoard.Model;

namespace JobBoard.Data.Repositories
{
    public interface IJobRepository
    {
        Task<Job> GetJobByIdAsync(int? idJob);
        Task<List<Job>> GetJobsAsync();
        Task<Job> CreateJobAsync(Job job);
        Task<bool> EditJobAsync(Job job);
        Task<bool> DeleteJobAsync(Job job);

    }
}

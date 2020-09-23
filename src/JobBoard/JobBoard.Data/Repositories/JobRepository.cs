using JobBoard.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace JobBoard.Data.Repositories
{
    public class JobRepository : IJobRepository
    {
        public Task<Job> CreateJobAsync(Job job)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteJobAsync(Job job)
        {
            throw new NotImplementedException();
        }

        public Task<bool> EditJobAsync(Job job)
        {
            throw new NotImplementedException();
        }

        public Task<Job> GetJobByIdAsync(int? idJob)
        {
            throw new NotImplementedException();
        }

        public Task<List<Job>> GetJobsAsync()
        {
            throw new NotImplementedException();
        }
    }
}

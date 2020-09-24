using JobBoard.Model;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace JobBoard.Data.Repositories
{
    public class JobRepository : IJobRepository
    {
        private readonly JobBoardContext _context;

        public JobRepository(JobBoardContext appDbContext)
        {
            _context = appDbContext;
            _context.Database.EnsureCreated();
        }
        public async Task<Job> CreateJobAsync(Job job)
        {
            _context.Jobs.Add(job);
            await _context.SaveChangesAsync();
            return job;

        }

        public async Task<bool> DeleteJobAsync(Job job)
        {
            _context.Jobs.Remove(job);
            await _context.SaveChangesAsync();
            return true;

        }

        public async Task<bool> EditJobAsync(Job job)
        {
            _context.Entry(job).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<List<Job>> GetJobsAsync()
        {
            return await _context.Jobs.ToListAsync();
        }

        public async Task<Job> GetJobByIdAsync(int? idJob)
        {
            var job = await _context.Jobs.FindAsync(idJob);
            _context.Entry(job).State = EntityState.Detached;
            return job;
        }
    }
}

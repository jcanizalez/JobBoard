using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using JobBoard.Model;
using JobBoard.Data.Repositories;

namespace JobBoard.API
{
    [Route("api/[controller]")]
    [ApiController]
    public class JobsController : ControllerBase
    {

        private readonly IJobRepository _repository;

        public JobsController(IJobRepository repository)
        {
            _repository = repository;
        }

        // GET: api/Jobs
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Job>>> GetJobs()
        {
            return await _repository.GetJobsAsync();
        }

        // GET: api/Jobs/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Job>> GetJob(int id)
        {
            var job = await _repository.GetJobByIdAsync(id);

            if (job == null)
            {
                return NotFound();
            }

            return job;
        }


        // POST: api/Jobs
        [HttpPost]
        public async Task<ActionResult<Job>> PostJob(Job job)
        {
            var resultJob = await _repository.CreateJobAsync(job);
            return CreatedAtAction("GetJob", new { id = resultJob.Id }, job);
        }


        // PUT: api/TodoItems/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutJob(int id, Job job)
        {
            if (id != job.Id)
            {
                return BadRequest();
            }


            var jobRead = await _repository.GetJobByIdAsync(id);
            if (jobRead == null)
            {
                return NotFound();
            }

            await _repository.EditJobAsync(job);



            return NoContent();
        }

        // DELETE: api/Jobs/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Job>> DeleteJob(int id)
        {
            var job = await _repository.GetJobByIdAsync(id);
            if (job == null)
            {
                return NotFound();
            }

            await _repository.DeleteJobAsync(job);


            return job;
        }
    }
}

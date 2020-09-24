using JobBoard.Data;
using JobBoard.Data.Repositories;
using JobBoard.Model;
using JobBoard.API;
using GenFu;
using System.Threading.Tasks;
using Xunit;
using System.Collections.Generic;
using System.Linq;
using System;
using Microsoft.EntityFrameworkCore;

namespace JobBoard.Test.JobBoard.API
{
    public class JobControllerTest
    {

        private readonly JobsController _jobController;


        public JobControllerTest()
        {
            var context = CreateDbContext();
            var repository = new JobRepository(context);
            _jobController = new JobsController(repository);
        }

        [Fact]
        public void GetJobTest()
        {
            // arrange
            int id = 1;

            // act
            var job = _jobController.GetJob(id);

            // assert
            Assert.Equal(id, job.Id);
        }


        [Fact]
        public void PostJobTest()
        {
            // arrange
            var job = new Job() { Title = "Job", Description = "Job Description", CreatedAt = DateTime.Now, ExpiresAt = DateTime.Now.AddDays(3) };

            // act
            var createdJob = _jobController.PostJob(job).Result;

            // assert
            Assert.NotNull(createdJob);
        }


        [Fact]
        public void EditJobAsyncTest()
        {
            // arrange
            int jobId = 1;
            var editedTitle = "New Job";

            // act
            var job = _jobController.GetJob(jobId).Result.Value;
            job.Title = editedTitle;
            var result = _jobController.PutJob(jobId, job).Result;
            var editedJob = _jobController.GetJob(jobId).Result.Value;


            // assert
            Assert.Equal(editedJob.Title, editedTitle);
        }

        public List<Job> GetFakeData()
        {

            var i = 1;
            var jobs = A.ListOf<Job>(20);
            jobs.ForEach(x => x.Id = i++);
            return jobs;

        }

        private JobBoardContext CreateDbContext()
        {
            var jobs = GetFakeData().AsQueryable();
            var options = new DbContextOptionsBuilder<JobBoardContext>().UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            var context = new JobBoardContext(options);
            context.Jobs.AddRange(jobs);
            context.SaveChanges();
            return context;
        }
    }
}

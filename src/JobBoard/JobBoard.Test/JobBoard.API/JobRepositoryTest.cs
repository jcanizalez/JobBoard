using JobBoard.Data;
using JobBoard.Data.Repositories;
using JobBoard.Model;
using GenFu;
using Xunit;
using System.Collections.Generic;
using System.Linq;
using System;
using Microsoft.EntityFrameworkCore;

namespace JobBoard.Test.JobBoard.Data
{
    public class JobRepositoryTest
    {

        private readonly JobBoardContext _context;

        public JobRepositoryTest()
        {
            _context = CreateDbContext();
        }

        [Fact]
        public void GetJobsAsyncTest()
        {
            // arrange
            var jobRepository = new JobRepository(_context);


            // act
            var jobs = jobRepository.GetJobsAsync();

            // assert
            Assert.Equal(_context.Jobs.Count(), jobs.Result.Count);
        }




        [Fact]
        public void EditJobAsyncTest()
        {
            // arrange
            var jobRepository = new JobRepository(_context);
            int jobId = 1;
            var editedTitle = "New Job";

            // act
            var job = jobRepository.GetJobByIdAsync(jobId).Result;
            job.Title = editedTitle;
            bool result = jobRepository.EditJobAsync(job).Result;
            var editedJob = jobRepository.GetJobByIdAsync(jobId).Result;


            // assert
            Assert.True(result);
            Assert.Equal(editedJob.Title, editedTitle);
        }


        [Fact]
        public void DeleteJobAsyncTest()
        {
            // arrange
            var jobRepository = new JobRepository(_context);
            int jobId = 10;

            // act
            int previousCount = jobRepository.GetJobsAsync().Result.Count;
            var job = jobRepository.GetJobByIdAsync(jobId).Result;
            var deletedJob = jobRepository.DeleteJobAsync(job);
            int postCount = jobRepository.GetJobsAsync().Result.Count;


            // assert
            Assert.Equal(previousCount, postCount + 1);

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

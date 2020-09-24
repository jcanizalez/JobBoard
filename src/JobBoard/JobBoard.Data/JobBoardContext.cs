using Microsoft.EntityFrameworkCore;
using JobBoard.Model;

namespace JobBoard.Data
{
    public class JobBoardContext : DbContext
    {

        public JobBoardContext(DbContextOptions<JobBoardContext> options)
        : base(options)
        { }



        public DbSet<Job> Jobs { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Map table names
            modelBuilder.Entity<Job>().ToTable("Jobs", "JobBoard");
            modelBuilder.Entity<Job>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.HasIndex(e => e.Title).IsUnique();
                entity.Property(e => e.CreatedAt).HasDefaultValueSql("CURRENT_TIMESTAMP");

            });
            base.OnModelCreating(modelBuilder);
        }
    }
}

using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DLFeedback360.Model
{
    public class FeedDBContext  : DbContext
    {
        public string ConnectionString { get; set; }
        public FeedDBContext(string conString)
        {
            ConnectionString = conString;
        }
        public DbSet<UserDetail> UserDetails { get; set; }
        public DbSet<Role> Roles { get; set; }

        public DbSet<Category> Categories { get; set; }
        public DbSet<FeedbackSchedule> FeedbackSchedules {  get; set; }

        public DbSet<FeedbackQustion> FeedbackQustions { get; set; }
        public DbSet<FeedbackRating> FeedbackRatings { get; set; }
        public DbSet<AddQuestion> AddQuestions { get; set; }

        public DbSet<Designation> Designations { get; set; }

        public DbSet<RatingCriteria> RatingCriteria { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(ConnectionString);
            }
            base.OnConfiguring(optionsBuilder);
        }
    }
}

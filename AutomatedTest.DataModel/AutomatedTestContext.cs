namespace AutomatedTest.DataModel
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public class AutomatedTestContext : DbContext
    {
        public AutomatedTestContext() : base("name=AutomatedTestContext")
        {
        }

        public DbSet<TestRun> TestRuns { get; set; }

        public DbSet<TestCase> TestCases { get; set; }


        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
        }
    }
}

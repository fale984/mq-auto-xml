namespace AutomatedTest.DataModel
{
    using System.Data.Entity;

    public class AutomatedTestContext : DbContext
    {
        public AutomatedTestContext() : base("name=AutomatedTestContext")
        {
        }

        public AutomatedTestContext(string connection) : base(connection)
        {
        }

        public DbSet<TestRun> TestRuns { get; set; }

        public DbSet<TestCase> TestCases { get; set; }


        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TestRun>().ToTable("TestRun");
            modelBuilder.Entity<TestCase>().ToTable("TestCase");
        }
    }
}

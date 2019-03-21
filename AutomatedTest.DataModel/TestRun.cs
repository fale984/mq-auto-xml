namespace AutomatedTest.DataModel
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class TestRun
    {
        public Guid Id { get; set; }

        public string TestRunSourceFilePath { get; set; }

        public string TestRunDestinationFilePath { get; set; }

        public DateTime? TestRunTimeStart { get; set; }

        public decimal? TestRunDuration { get; set; }

        public string TestRunResult { get; set; }

        public decimal? TestSuiteRunDuration { get; set; }

        public string TestSuiteSettingBrowser { get; set; }

        public string TestEnvironmentMachineName { get; set; }

        public string TestSuiteFailureMessage { get; set; }

        public bool? FalseFailure { get; set; }

        public ICollection<TestCase> TestCases { get; set; }
    }
}

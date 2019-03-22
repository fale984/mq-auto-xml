namespace AutomatedTest.DataModel
{
    using System;

    public class TestCase
    {
        public Guid Id { get; set; }

        public string TestCaseId { get; set; }

        public string TestCaseType { get; set; }

        public string TestCaseName { get; set; }

        public string TestCaseFullName { get; set; }

        public string TestCaseMethodName { get; set; }

        public string TestCaseResult { get; set; }

        public string TestCaseFailureMessage { get; set; }

        public string TestCaseStackTrace { get; set; }

        public string TestCaseScreenShotFileName { get; set; }

        public Guid TestRunId { get; set; }

        public virtual TestRun TestRun { get; set; }
    }
}

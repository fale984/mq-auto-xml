namespace AutomatedTest.FileProcessing
{
    using AutomatedTest.DataModel;
    using System;
    using System.IO;
    using System.Linq;
    using System.Xml.Linq;

    public class FileProcessing
    {
        public TestRun ProccessFile(FileInfo testRunSourceFilePath)
        {
            XElement document = XElement.Load(testRunSourceFilePath.FullName);
            TestRun testRun = BuildTestRunObjectFromDucument(document);

            testRun.TestRunSourceFilePath = testRunSourceFilePath.FullName;
            testRun.TestRunDestinationFilePath = $"\\\\devclient1\\c$\\AutomatedTestingFiles\\{testRun.Id}_{testRunSourceFilePath.Name}";

            var context = new AutomatedTestContext();
            context.TestRuns.Add(testRun);
            context.SaveChanges();

            return testRun;
        }

        private TestRun BuildTestRunObjectFromDucument(XElement document)
        {
            var testSuite = document.Descendants("test-suite").FirstOrDefault();
            var TestSuiteSettingBrowser = testSuite.Descendants("setting")
                    .FirstOrDefault(x => x.Attribute("name")?.Value == "TestParametersDictionary");

            var testRun = new TestRun
            {
                TestRunResult = document.Attribute("result")?.Value,
                TestEnvironmentMachineName = document.Descendants("environment").FirstOrDefault()?.Attribute("machine-name")?.Value,
                TestSuiteSettingBrowser = TestSuiteSettingBrowser.Descendants("item").FirstOrDefault().Attribute("value")?.Value
            };

            if (DateTime.TryParse(document.Attribute("start-time")?.Value, out var testRunTimeStart))
            {
                testRun.TestRunTimeStart = testRunTimeStart;
            }

            if (decimal.TryParse(document.Attribute("duration")?.Value, out var testRunDuration))
            {
                testRun.TestRunDuration = testRunDuration;
            }

            if (decimal.TryParse(testSuite.Attribute("duration")?.Value, out var testSuiteRunDuration))
            {
                testRun.TestSuiteRunDuration = testSuiteRunDuration;
            }

            testRun.TestCases = document.Descendants("test-case").Select(p => new TestCase
            {
                TestCaseId = p.Attribute("id")?.Value,
                TestCaseType = p.Parent.Attribute("type")?.Value,
                TestCaseName = p.Attribute("name")?.Value,
                TestCaseFullName = p.Attribute("fullname")?.Value,
                TestCaseMethodName = p.Attribute("methodname")?.Value,
                TestCaseResult = p.Attribute("result")?.Value,
                TestCaseFailureMessage = p.Descendants("failure").Elements("message").FirstOrDefault()?.Value,
                TestCaseStackTrace = p.Descendants("failure").Elements("stack-trace").FirstOrDefault()?.Value,
                TestCaseScreenShotFileName = p.Descendants("attachments").Elements("attachment").Elements("filePath").FirstOrDefault()?.Value
            }).ToList();

            return testRun;
        }
    }
}

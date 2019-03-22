namespace AutomatedTest.FileProcessing
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Xml.Linq;
    using AutomatedTest.DataModel;

    public class FileProcessing
    {
        public void ProccessFile(string filePath)
        {
            XElement document = XElement.Load($"{filePath}");
            TestRun testRun = BuildTestRunObjectFromDucument(document);
            var testRunId = document.FirstAttribute.Value;
        }

        private TestRun BuildTestRunObjectFromDucument(XElement document)
        {
            var testSuite = document.Descendants("test-suite").FirstOrDefault();
            var testRun = new TestRun
            {
                Id = Guid.NewGuid(),
                TestRunDestinationFilePath = string.Empty,
                TestRunSourceFilePath = string.Empty,
                TestRunTimeStart = DateTime.Parse(document.Attribute("start-time").Value),
                TestRunDuration = decimal.Parse(document.Attribute("duration").Value),
                TestRunResult = document.Attribute("result").Value,
                TestEnvironmentMachineName = document.Descendants("environment").FirstOrDefault().Attribute("machine-name").Value,
                TestSuiteRunDuration = decimal.Parse(testSuite.Attribute("duration").Value),
                TestSuiteSettingBrowser = testSuite.Descendants("setting")
                .FirstOrDefault(x => x.Attribute("name")?.Value == "TestParameters").Attribute("value").Value,
                TestCases = new List<TestCase>()
            };

            testRun.TestCases = document.Descendants("test-case").Select(p => new TestCase
            {
                Id = Guid.NewGuid(),
                TestRunId = testRun.Id,
                TestCaseId = p.Attribute("id").Value,
                TestCaseType = p.Parent.Attribute("type").Value,
                TestCaseName = p.Attribute("name").Value,
                TestCaseFullName = p.Attribute("fullname").Value,
                TestCaseMethodName = p.Attribute("methodname").Value,
                TestCaseResult = p.Attribute("result").Value,
                TestCaseFailureMessage = p.Descendants("failure").Elements("message").FirstOrDefault()?.Value,
                TestCaseStackTrace = p.Descendants("failure").Elements("stack-trace").FirstOrDefault()?.Value,
                TestCaseScreenShotFileName = p.Descendants("attachments").Elements("attachment").Elements("filePath").FirstOrDefault()?.Value
            }).ToList();

            return testRun;
        }
    }
}

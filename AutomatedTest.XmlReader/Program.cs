namespace AutomatedTest.XmlReader
{
    using System;
    using System.IO;
    
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Mediquant Result Process");

            var fileName = "results.xml";
            var currentDirectory = Directory.GetCurrentDirectory();
            var filePath = Path.Combine(currentDirectory, fileName);

            Console.WriteLine("Reading file {0}...", filePath);

            var program = new FileProcessing.FileProcessing();
            var testResult = program.ProccessFile(filePath);

            Console.WriteLine("Processed {0} test cases.", testResult.TestCases.Count);

            Console.ReadKey();
        }
    }
}

namespace AutomatedTest.XmlReader
{
    using System;
    using System.IO;

    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Mediquant Result Process");

            string fileName;

            // Look for file in the args
            if (args.Length > 0)
            {
                fileName = args[0];
            }
            else
            {
                // Request file name
                Console.WriteLine("Specify the file to process:");
                fileName = Console.ReadLine();
            }

            if (string.IsNullOrEmpty(fileName))
            {
                Console.WriteLine("Invalid file name.");
                Console.ReadKey();
                return;
            }

            var fileInfo = new FileInfo(fileName);
            if (fileInfo.Exists)
            {
                Console.WriteLine("Reading file {0}...", fileInfo.FullName);

                var processor = new FileProcessing.FileProcessing();
                var testResult = processor.ProccessFile(fileInfo);

                Console.WriteLine("Processed {0} test cases.", testResult.TestCases.Count);
            }
            else
            {
                Console.WriteLine("File not found.");
            }

            Console.ReadKey();
        }
    }
}

using Newtonsoft.Json;
using NLog;
using NLog.Config;
using NLog.Targets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SupportBank
{
    class Program
    {
        private static readonly ILogger logger = LogManager.GetCurrentClassLogger();       

        static void Main(string[] args)
        {
            var config = new LoggingConfiguration();
            var target = new FileTarget { FileName = "SupportBankLog.log", Layout = @"${longdate} ${level} - ${logger}: ${message}" };
            config.AddTarget("File Logger", target);
            config.LoggingRules.Add(new LoggingRule("*", LogLevel.Debug, target));
            LogManager.Configuration = config;
            Logissue("The program was started", LogLevel.Info);

            MainMenu();
            int choice;
            while ((choice = ValidInput(1, 3)) != 3)
            {                
                switch (choice)
                {
                    case 1:
                        Import import = new Import();
                        import.Start();
                        break;
                    case 2:
                        Export export = new Export();
                        export.Start();
                        break;
                }
                MainMenu();
            }
        }
        static void MainMenu()
        {
            Console.WriteLine("Support Bank");
            Console.WriteLine("-----------------------------");
            Console.WriteLine(" 1. Import");
            Console.WriteLine(" 2. Export");
            Console.WriteLine();
            Console.WriteLine(" 3. Exit");
        }
        public static int ValidInput(int minValue, int maxValue)
        {
            string inputStr;
            int inputInt;
            while (true)
            {
                inputStr = Console.ReadLine();
                if(int.TryParse(inputStr, out inputInt))
                {
                    if(inputInt >= minValue && inputInt <= maxValue)
                    {
                        return inputInt;
                    }
                }
                Console.WriteLine("Please try again.");
            }
        }    
        public static void Logissue(string message, LogLevel level)
        {
            LogEventInfo logEvent = new LogEventInfo();
            logEvent.Level = level;
            logEvent.Message = message;
            logEvent.LoggerName = Environment.MachineName;
            logEvent.TimeStamp = DateTime.Now;
            logger.Log(logEvent);
        }
        public static string chooseFile(string filetype)
        {
            string[] files = System.IO.Directory.GetFiles(System.IO.Directory.GetCurrentDirectory());
            int choice;
            List<string> validFiles = new List<string>();
            foreach (string file in files)
            {
                if (file.Substring(file.Length - filetype.Length, filetype.Length) == filetype)
                {
                    validFiles.Add(file);
                }
            }
            Console.WriteLine("Choose file:");
            for (int i = 0; i < validFiles.Count; i++)
            {
                Console.WriteLine("   {0} - {1}", i, validFiles[i]);
            }
            choice = ValidInput(0, validFiles.Count - 1);
            return validFiles[choice];
        }
    }
   
}

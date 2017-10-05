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

            Dictionary<string, Person> people = new Dictionary<string, Person>();

            while (true)
            {
                switch (fileType())
                {
                    case 1:
                        Logissue("User chose the 'CSV' file type.", LogLevel.Info);
                        CSVParser csvParser = new CSVParser();
                        people = csvParser.GetTransactions();
                        break;
                    case 2:
                        JsonParser jsonParser = new JsonParser();
                        people = jsonParser.GetTranscations();
                        break;
                    case 3:
                        XMLParser xmlParser = new XMLParser();
                        break;
                }

                switch (MenuOption())
                {
                    case 1:
                        Logissue("User chose the 'ListAll' option.", LogLevel.Info);
                        ListAll(people);
                        break;
                    case 2:
                        Logissue("User chose the 'ListAccount' option.", LogLevel.Info);
                        ListAccount(people);
                        break;
                }
            }

        }
        static int? fileType()
        {
            string input;
            int output;
            Console.WriteLine("Choose the file type to readfrom: ");
            Console.WriteLine("     1. CSV");
            Console.WriteLine("     2. JSON");
            Console.WriteLine("     3. XML");
            while (true)
            {
                input = Console.ReadLine();
                if (int.TryParse(input, out output))
                {
                    if (output > 0 && output <= 3)
                    {
                        return output;
                    }
                    else
                    {
                        Logissue(String.Format("Invalid input \"{0}\" from user", input), LogLevel.Warn);
                        Console.WriteLine("Invalid input, please try again.");
                    }
                }
            }
        }
        static void ListAccount(Dictionary<string, Person> people)
        {
            string account = "";
            Boolean validInput = false;
            Person person;
            while (!validInput)
            {
                Console.Write("  Please enter the name of the account: ");
                account = Console.ReadLine();
                if (people.ContainsKey(account))
                {
                    validInput = true;
                }
                else
                {
                    Logissue(String.Format("Invalid input \"{0}\" from user", account), LogLevel.Warn);
                    Console.WriteLine(" Invalid input, pleaese try again.");
                }
            }
            person = people[account];
            Console.WriteLine("   Date, From, To, Narrative, Amount");
            foreach (Transaction transaction in person.transactions)
            {
                Console.WriteLine("   {0}, {1}, {2}, {3}, {4}", transaction.Date, transaction.From.Name, transaction.To.Name, transaction.Narrative, transaction.Amount);
            }
            Console.WriteLine();
        }
        static void ListAll(Dictionary<string, Person> people)
        {
            foreach (KeyValuePair<string, Person> person in people)
            {
                Console.WriteLine(" Name: {0}; Balance: {1};", person.Value.Name, person.Value.Balance);
            }
                Console.WriteLine();
        }
        static int? MenuOption()
        {
            Boolean valid = false;
            string input;
            int choice = 0;
            Console.WriteLine("Support Bank");
            Console.WriteLine("------------------------------------");
            Console.WriteLine("  1. List all");
            Console.WriteLine("  2. List account");

            while (!valid)
            {
                Console.Write("  :");
                input = Console.ReadLine();
                if (int.TryParse(input, out choice) && choice > 0 && choice <= 2) {
                    valid = true;
                    return choice;
                }
                Logissue(String.Format("Invalid input \"{0}\" from user", input), LogLevel.Warn);
                Console.WriteLine("Invalid input, please try again.");
            }
            return null;
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
        
    }
   
}

using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SupportBank
{
    class Import
    {
        public void Start()
        {
            Dictionary<string, Person> people = new Dictionary<string, Person>();
            int fileType;
            while ((fileType = FileType()) != 4)
            {
                switch (fileType)
                {
                    case 1:
                        Program.Logissue("User chose the 'CSV' file type.", LogLevel.Info);
                        CSVParser csvParser = new CSVParser();
                        people = csvParser.GetTransactions();
                        break;
                    case 2:
                        Program.Logissue("User chose the 'Json' file type.", LogLevel.Info);
                        JsonParser jsonParser = new JsonParser();
                        people = jsonParser.GetTransactions();
                        break;
                    case 3:
                        Program.Logissue("User chose the 'XML' file type.", LogLevel.Info);
                        XMLParser xmlParser = new XMLParser();
                        people = xmlParser.GetTransactions();
                        break;
                }

                int menuOption;
                while ((menuOption = ImportMenuOption()) != 3)
                    switch (menuOption)
                    {
                        case 1:
                            Program.Logissue("User chose the 'ListAll' option.", LogLevel.Info);
                            ListAll(people);
                            break;
                        case 2:
                            Program.Logissue("User chose the 'ListAccount' option.", LogLevel.Info);
                            ListAccount(people);
                            break;
                    }
            }
        }
        void ListAccount(Dictionary<string, Person> people)
        {
            string account = "";
            Boolean ValidInput = false;
            Person person;
            while (!ValidInput)
            {
                Console.Write("  Please enter the name of the account: ");
                account = Console.ReadLine();
                if (people.ContainsKey(account))
                {
                   ValidInput = true;
                }
                else
                {
                    Program.Logissue(String.Format("Invalid input \"{0}\" from user", account), LogLevel.Warn);
                    Console.WriteLine(" Invalid input, pleaese try again.");
                }
            }
            person = people[account];
            Console.WriteLine("   Date, From, To, Narrative, Amount");
            foreach (Transaction transaction in person.transactions)
            {
                Console.WriteLine("   {0}, {1}, {2}, {3}, {4}", transaction.Date.ToString("dd/MM/yyyy"), transaction.FromAccount, transaction.ToAccount, transaction.Narrative, transaction.Amount);
            }
            Console.WriteLine();
        }
        void ListAll(Dictionary<string, Person> people)
        {
            foreach (KeyValuePair<string, Person> person in people)
            {
                Console.WriteLine(" Name: {0}; Balance: {1};", person.Value.Name, person.Value.Balance);
            }
            Console.WriteLine();
        }
        int ImportMenuOption()
        {
            int choice = 0;
            Console.WriteLine("Support Bank");
            Console.WriteLine("------------------------------------");
            Console.WriteLine("  1. List all");
            Console.WriteLine("  2. List account");
            Console.WriteLine();
            Console.WriteLine("  3. Back");

            choice = Program.ValidInput(1, 3);
            return choice;
        }
        static int FileType()
        {
            string input;
            int output;
            Console.WriteLine("Choose the file type to read from: ");
            Console.WriteLine("     1. CSV");
            Console.WriteLine("     2. JSON");
            Console.WriteLine("     3. XML");
            Console.WriteLine();
            Console.WriteLine("     4. Back");
            while (true)
            {
                input = Console.ReadLine();
                if (int.TryParse(input, out output))
                {
                    if (output > 0 && output <= 4)
                    {
                        return output;
                    }
                    else
                    {
                        Program.Logissue(String.Format("Invalid input \"{0}\" from user", input), LogLevel.Warn);
                        Console.WriteLine("Invalid input, please try again.");
                    }
                }
            }
        }
    }
}

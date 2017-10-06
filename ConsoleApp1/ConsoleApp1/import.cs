using NLog;
using System;
using System.Collections.Generic;

namespace SupportBank
{
    internal class Import
    {
        public void Start()
        {
            Dictionary<string, Person> people = new Dictionary<string, Person>();
            string filePath;
            filePath = Program.chooseFile();
            ParserFactory parserFactory = new ParserFactory(filePath);
            people = parserFactory.GetParser.GetPeople(filePath);
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

        private void ListAccount(Dictionary<string, Person> people)
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

        private void ListAll(Dictionary<string, Person> people)
        {
            foreach (KeyValuePair<string, Person> person in people)
            {
                Console.WriteLine(" Name: {0}; Balance: {1};", person.Value.Name, person.Value.Balance);
            }
            Console.WriteLine();
        }

        private int ImportMenuOption()
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
    }
}
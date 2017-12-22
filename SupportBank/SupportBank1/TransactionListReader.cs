using System;
using System.Collections.Generic;
using NLog;

namespace ConsoleApp1
{
    internal class TransactionListReader
    {
        public void Start()
        {
            Dictionary<string, Person> people = new Dictionary<string, Person>();
            List<Transaction> transactions = new List<Transaction>();
            int menuOption;
            ImportFile(out people, out transactions);

            while ((menuOption = ImportMenuOption()) != 3)
                switch (menuOption)
                {
                    case 1:
                        Program.logger.Log(LogLevel.Info, "The user chose the 'List all' option.");
                        ListAll(people);
                        break;

                    case 2:
                        Program.logger.Log(LogLevel.Info, "The user chose the 'List account' option.");
                        ListAccount(people);
                        break;
                }
        }

        private void ImportFile(out Dictionary<string, Person> people, out List<Transaction> transactions)
        {
            Dictionary<string, Person> peopleDictionary = new Dictionary<string, Person>();
            List<Transaction> transactionList = new List<Transaction>();
            string filePath = Program.ChooseFile();
            ParserFactory parserFactory = new ParserFactory();

            transactions = parserFactory.GetParser(filePath).GetTransactions();

            people = parserFactory.GetParser(filePath).GetPeople();
        }

        public static Dictionary<string, Person> ParseTransaction(Dictionary<string, Person> people, Transaction transaction)
        {
            if (!people.ContainsKey(transaction.FromAccount))
            {
                Person person = new Person();
                person.Name = transaction.FromAccount;
                people.Add(person.Name, person);
            }
            if (!people.ContainsKey(transaction.ToAccount))
            {
                Person person = new Person();
                person.Name = transaction.ToAccount;
                people.Add(person.Name, person);
            }
            Person payer = people[transaction.FromAccount];
            Person payee = people[transaction.ToAccount];
            payer.Balance -= transaction.Amount;
            payee.Balance += transaction.Amount;
            payer.transactions.Add(transaction);
            payee.transactions.Add(transaction);
            return people;
        }

        private void ListAccount(Dictionary<string, Person> people)
        {
            string account = GetAccount(people);
            Person person = people[account];
            Console.WriteLine("   Date, From, To, Narrative, Amount");
            foreach (Transaction transaction in person.transactions)
            {
                Console.WriteLine("   {0}, {1}, {2}, {3}, £{4:0.00}", transaction.Date.ToString("dd/MM/yyyy"), transaction.FromAccount, transaction.ToAccount, transaction.Narrative, transaction.Amount);
            }
            Console.WriteLine();
        }

        private void ListAll(Dictionary<string, Person> people)
        {
            foreach (KeyValuePair<string, Person> person in people)
            {
                Console.WriteLine(" Name: {0}; Balance: £{1:0.00};", person.Value.Name, person.Value.Balance);
            }
            Console.WriteLine();
        }

        private string GetAccount(Dictionary<string, Person> people)
        {
            string account = "";
            Boolean ValidInput = false;
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
                    Program.logger.Log(LogLevel.Warn, String.Format("Invalid input \"{0}\" from user", account));
                    Console.WriteLine(" Invalid input, pleaese try again.");
                }
            }
            return account;
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

            choice = Program.GetIntValueBetween(1, 3);
            return choice;
        }
    }
}
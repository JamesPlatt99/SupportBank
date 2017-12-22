using System;
using System.Collections.Generic;

namespace ConsoleApp1
{
    internal class Default
    {
        public Default()
        {
            string name = GetUsersName();
            int choice;
            Congregator congregator = new Congregator();

            while ((choice = GetFunctionChoice()) != 4)
            {
                JsonParser parser = new JsonParser("Global.json");
                congregator.CreateGlobalFile();
                List<Transaction> transactions = parser.GetTransactions();
                Dictionary<string, Person> people = parser.GetPeople();
                switch (choice)
                {
                    case 1:
                        AddTransaction(name, transactions);
                        break;

                    case 2:
                        DisplayBalance(name, people);
                        break;

                    case 3:
                        DisplayTransactions(name, people);
                        break;
                }
                Console.Write("...");
                Console.ReadLine();
            }
        }

        private void AddTransaction(string name, List<Transaction> transactions)
        {
            transactions.Add(CreateTransaction(name));
            const string path = "Other.json";
            CreateJsonFile creator = new CreateJsonFile(path);
            creator.CreateFile(transactions);
        }

        private Transaction CreateTransaction(string name)
        {
            Transaction transaction = new Transaction();
            string amount = "";
            double amountVal;

            Console.WriteLine();
            Console.WriteLine("Transaction type:");
            Console.WriteLine("  1. Request");
            Console.WriteLine("  2. Send");

            transaction.Date = DateTime.Now;
            int input = Program.GetIntValueBetween(1, 2);
            if (input == 1)
            {
                transaction.ToAccount = name;
                Console.Write("Enter the name of the sender: ");
                transaction.FromAccount = Console.ReadLine();
            }
            else
            {
                transaction.FromAccount = name;
                Console.Write("Enter the name of the recipient: ");
                transaction.ToAccount = Console.ReadLine();
            }

            Console.WriteLine("Please enter a reason for this transaction: ");
            transaction.Narrative = Console.ReadLine();

            while (!double.TryParse(amount, out amountVal))
            {
                Console.Write("Please enter the value of the transaction: £");
                amount = Console.ReadLine();
            }
            transaction.Amount = amountVal;
            return transaction;
        }

        private void DisplayBalance(string name, IReadOnlyDictionary<string, Person> people)
        {
            double balance = people.ContainsKey(name) ? people[name].Balance : 0;
            Console.WriteLine("Your balance is £{0:0.00}.", balance);
        }
        
        private void DisplayTransactions(string name, Dictionary<string, Person> people)
        {
            Console.WriteLine();
            Console.WriteLine("Date, To Account, From Account, Description, Amount");
            if (!people.ContainsKey(name)) return;
            foreach (Transaction transaction in people[name].transactions)
            {
                Console.WriteLine("{0:dd/MM/yyyy}, {1}, {2}, {3}, £{4:0.00}", transaction.Date, transaction.ToAccount, transaction.FromAccount, transaction.Narrative, transaction.Amount);
            }
        }

        private int GetFunctionChoice()
        {
            Console.WriteLine();
            Console.WriteLine("What would you like to do?");
            Console.WriteLine("  1. Create new transaction.");
            Console.WriteLine("  2. Check current balance.");
            Console.WriteLine("  3. View a list of your transactions.");
            Console.WriteLine();
            Console.WriteLine("  4. Back");
            while (true)
            {
                Console.Write("Enter an option: ");
                string input = Console.ReadLine();
                if (int.TryParse(input, out int output))
                {
                    if (output > 0 && output <= 4)
                    {
                        return output;
                    }
                }
                Console.WriteLine("Invalid input.");
            }
        }

        private string GetUsersName()
        {
            string name = "";
            Console.WriteLine();
            while (name != null && name.Length == 0)
            {
                Console.Write("Please enter your name: ");
                name = Console.ReadLine();
            }
            return name;
        }
    }
}
using NLog;
using System;
using System.Collections.Generic;

namespace SupportBank
{
    internal class Export
    {
        public void Start()
        {
            List<Transaction> transactions = new List<Transaction>();
            string input = "y";
            string fileType = Program.chooseFileType("export to");
            string fileName = String.Format("{0}.{1}", GetFileName(), fileType);

            while (input == "y")
            {
                Console.WriteLine("Transaction {0}:", transactions.Count);
                Console.WriteLine("-------------------");
                transactions.Add(GetTransaction());
                Console.Write("Add another? (y/n):");
                input = Console.ReadLine().ToLower().ToCharArray()[0].ToString();
                Console.WriteLine();
            }
            Program.Logissue(String.Format("The user entered a list of {0} transactions.", transactions.Count + 1), LogLevel.Info);
            CreatorFactory creatorFactory = new CreatorFactory();
            ICreator creator = creatorFactory.GetCreator(fileName);
            creator.CreateFile(transactions);
        }

        public void Start(List<Transaction> transactions, string fileName)
        {
            string fileType = Program.chooseFileType("convert to");
            CreatorFactory creatorFactory = new CreatorFactory();
            ICreator creator = creatorFactory.GetCreator(fileName);
            creator.CreateFile(transactions);
        }

        public string GetFileName()
        {
            string input = "";
            while (input.Length == 0)
            {
                Console.Write(" Please enter a file name: ");
                input = Console.ReadLine();
            }
            return input;
        }

        public Transaction GetTransaction()
        {
            Transaction transaction = new Transaction();
            string input;
            double amount;
            DateTime date;

            Console.Write("Date: ");
            while (!DateTime.TryParse(input = Console.ReadLine(), out date))
            {
                Console.WriteLine("Invalid input, please try again.");
            }
            transaction.Date = date;
            Console.Write("From Account: ");
            while ((input = Console.ReadLine()).Length == 0)
            {
                Console.WriteLine("Invalid input, please try again.");
            }
            transaction.FromAccount = input;
            Console.Write("To Account: ");
            while ((input = Console.ReadLine()).Length == 0)
            {
                Console.WriteLine("Invalid input, please try again.");
            }
            transaction.ToAccount = input;
            Console.Write("Narrative: ");
            while ((input = Console.ReadLine()).Length == 0)
            {
                Console.WriteLine("Invalid input, please try again.");
            }
            transaction.Narrative = input;
            Console.Write("Amount: ");
            while (!double.TryParse(Console.ReadLine(), out amount))
            {
                Console.WriteLine("Invalid input, please try again.");
            }
            transaction.Amount = amount;
            return transaction;
        }
    }
}
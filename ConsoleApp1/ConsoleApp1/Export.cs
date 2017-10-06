using Newtonsoft.Json;
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
            string fileType = GetFileType();
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
            switch (fileType)
            {
                case "csv":
                    Program.Logissue("The user selected the csv datatype. [export]", LogLevel.Info);
                    CreateCSVFile(fileName, transactions);
                    break;

                case "json":
                    Program.Logissue("The user selected the json datatype. [export]", LogLevel.Info);
                    CreateJSONFile(fileName, transactions);
                    break;

                case "xml":
                    Program.Logissue("The user selected the xml datatype. [export]", LogLevel.Info);
                    CreateXMLFile(fileName, transactions);
                    break;
            }
        }

        public void Start(List<Transaction> transactions, string fileName)
        {
            string fileType = GetFileType();
            switch (fileType)
            {
                case "csv":
                    Program.Logissue("The user selected the csv datatype. [convert]", LogLevel.Info);
                    CreateCSVFile(fileName, transactions);
                    break;

                case "json":
                    Program.Logissue("The user selected the json datatype. [convert]", LogLevel.Info);
                    CreateJSONFile(fileName, transactions);
                    break;

                case "xml":
                    Program.Logissue("The user selected the xml datatype. [convert]", LogLevel.Info);
                    CreateXMLFile(fileName, transactions);
                    break;
            }
        }

        public void CreateCSVFile(string fileName, List<Transaction> transactions)
        {
            System.IO.StreamWriter file = new System.IO.StreamWriter(fileName);
            file.WriteLine("Date,From,To,Narrative,Amount\n");
            foreach (Transaction transaction in transactions)
            {
                file.WriteLine("{0},{1},{2},{3},{4}\n", transaction.Date, transaction.FromAccount, transaction.ToAccount, transaction.Narrative, transaction.Amount);
            }
            file.Close();
            Program.Logissue(String.Format("The file {0} was created successfully.", fileName), LogLevel.Info);
        }

        public void CreateJSONFile(string fileName, List<Transaction> transactions)
        {
            System.IO.StreamWriter file = new System.IO.StreamWriter(fileName);
            file.WriteLine("[");
            foreach (Transaction transaction in transactions)
            {
                file.WriteLine(JsonConvert.SerializeObject(transaction));
            }
            file.WriteLine("]");
            file.Close();
            Program.Logissue(String.Format("The file {0} was created successfully.", fileName), LogLevel.Info);
        }

        public void CreateXMLFile(string fileName, List<Transaction> transactions)
        {
            System.IO.StreamWriter file = new System.IO.StreamWriter(fileName);
            file.WriteLine("<?xml version=\"1.0\" encoding=\"utf - 8\"?>");
            file.WriteLine("<TransactionList>");
            foreach (Transaction transaction in transactions)
            {
                file.WriteLine("  <SupportTransaction Date=\"{0}\">\n", transaction.Date.ToOADate());
                file.WriteLine("    <Description>{0}</Description>\n", transaction.Narrative);
                file.WriteLine("    <Value>{0}</Value>\n", transaction.Amount);
                file.WriteLine("    <Parties>\n");
                file.WriteLine("      <From>{0}</From>\n", transaction.FromAccount);
                file.WriteLine("      <To>{0}</To>\n", transaction.ToAccount);
                file.WriteLine("    </Parties>\n");
                file.WriteLine("  </SupportTransaction>\n");
            }
            file.WriteLine("</TransactionList>");
            file.Close();
            Program.Logissue(String.Format("The file {0} was created successfully.", fileName), LogLevel.Info);
        }

        public string GetFileType()
        {
            string input;
            Console.WriteLine("Please enter your desired filetype:");
            Console.WriteLine("   csv/json/xml");
            while ((input = Console.ReadLine()) != "csv" && input != "json" && input != "xml")
            {
                input = Console.ReadLine();
            }
            return input;
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
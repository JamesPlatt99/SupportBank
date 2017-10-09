using NLog;
using System;
using System.Collections.Generic;
using CsvHelper;

namespace SupportBank
{
    internal class CSVParser : IParser
    {
        private string filePath;

        public CSVParser(string filepath)
        {
            filePath = filepath;
        }

        public List<Transaction> GetTransactions()
        {
            System.IO.StreamReader file = new System.IO.StreamReader(filePath);
            List<Transaction> transactions = new List<Transaction>();
            var csv = new CsvReader(file);
            Transaction transaction;

            while (csv.Read())
            {
                transaction = new Transaction();
                transaction.Date = csv.GetField<DateTime>(0);
                transaction.FromAccount = csv.GetField<String>(1);
                transaction.ToAccount = csv.GetField<String>(2);
                transaction.Narrative = csv.GetField<String>(3);
                transaction.Amount = csv.GetField<Double>(4);
                transactions.Add(transaction);
            }
            return transactions;
        }

        public Dictionary<string, Person> GetPeople()
        {
            List<Transaction> transactions = GetTransactions();
            Dictionary<string, Person> people = new Dictionary<string, Person>();
            foreach(Transaction transaction in transactions)
            {
                if (!people.ContainsKey(transaction.FromAccount))
                {
                    Person person = new Person();
                    person.Name = transaction.FromAccount;
                    people.Add(transaction.FromAccount, person);
                }
                if (!people.ContainsKey(transaction.ToAccount))
                {
                    Person person = new Person();
                    person.Name = transaction.ToAccount;
                    people.Add(transaction.ToAccount, person);
                }
                people[transaction.FromAccount].Balance -= transaction.Amount;
                people[transaction.ToAccount].Balance += transaction.Amount;
            }
            return people;
        }
        
        public Transaction CreateTransaction(string[] transactionAr, Person payer, Person payee, int lineNumber)
        {
            Transaction transaction = new Transaction();
            try
            {
                transaction.Date = DateTime.Parse(transactionAr[0]);
                transaction.FromAccount = payer.Name;
                transaction.ToAccount = payee.Name;
                transaction.Narrative = transactionAr[3];
                transaction.Amount = Convert.ToDouble(transactionAr[4]);
            }
            catch (Exception)
            {
                Console.WriteLine("Input \"{0}\" was not valid, please ensure data is in the correct format. This line has been skipped.", string.Join(",", transactionAr));
                Console.WriteLine();
                Program.Logissue(String.Format("Invalid line {0} , \"{1}\" was read from file", lineNumber, string.Join(",", transactionAr)), LogLevel.Warn);
                return null;
            }
            return transaction;
        }
    }
}
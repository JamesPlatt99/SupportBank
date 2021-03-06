﻿using System;
using System.Collections.Generic;
using CsvHelper;
using NLog;

namespace ConsoleApp1
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
            int lineNumber = 0;
            csv.Read();
            while (csv.Read())
            {
                lineNumber++;
                transaction = new Transaction();
                try
                {
                    transaction.Date = csv.GetField<DateTime>(0);
                    transaction.FromAccount = csv.GetField<String>(1);
                    transaction.ToAccount = csv.GetField<String>(2);
                    transaction.Narrative = csv.GetField<String>(3);
                    transaction.Amount = csv.GetField<Double>(4);
                    transactions.Add(transaction);
                }
                catch (Exception e)
                {
                    Console.WriteLine("Error:");
                    Console.WriteLine("   " + e.Message);
                    Console.WriteLine("   Line {0} is in an incorrect format, skipping line.", lineNumber);
                    Program.logger.Log(LogLevel.Error, String.Format("{0} : Occurred while reading csv file.", e.Message));
                }
            }
            file.Close();
            return transactions;
        }

        public Dictionary<string, Person> GetPeople()
        {
            List<Transaction> transactions = GetTransactions();
            Dictionary<string, Person> people = new Dictionary<string, Person>();
            foreach (Transaction transaction in transactions)
            {
                people = TransactionListReader.ParseTransaction(people, transaction);
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
                Console.ForegroundColor = System.ConsoleColor.Red;
                Console.WriteLine("Input \"{0}\" was not valid, please ensure data is in the correct format. This line has been skipped.", string.Join(",", transactionAr));
                Console.ForegroundColor = System.ConsoleColor.White;
                Console.WriteLine();
                Program.logger.Log(LogLevel.Warn, String.Format("Invalid line {0} , \"{1}\" was read from file", lineNumber, string.Join(",", transactionAr)));
                return null;
            }
            return transaction;
        }
    }
}
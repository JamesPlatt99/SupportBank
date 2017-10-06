﻿using NLog;
using System;
using System.Collections.Generic;

namespace SupportBank
{
    internal class CSVParser
    {
        public Dictionary<string, Person> GetTransactions()
        {
            Dictionary<string, Person> people = new Dictionary<string, Person>();
            string path = Program.chooseFile("csv");
            string tmp;
            string[] line;
            int lineNumber = 1;
            System.IO.StreamReader file = new System.IO.StreamReader(path);
            tmp = file.ReadLine();
            while ((tmp = file.ReadLine()) != null)
            {
                lineNumber++;
                line = tmp.Split(',');
                if (!people.ContainsKey(line[1]))
                {
                    Person person = new Person();
                    person.Name = line[1];
                    people.Add(person.Name, person);
                }
                if (!people.ContainsKey(line[2]))
                {
                    Person person = new Person();
                    person.Name = line[2];
                    people.Add(person.Name, person);
                }
                Person payer = people[line[1]];
                Person payee = people[line[2]];
                Transaction transaction = CreateTransaction(line, payer, payee, lineNumber);
                if (transaction != null)
                {
                    payer.Balance -= transaction.Amount;
                    payee.Balance += transaction.Amount;
                    payer.transactions.Add(transaction);
                    payee.transactions.Add(transaction);
                }
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
using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SupportBank
{
    class CSVParser
    {
        public Dictionary<string, Person> GetTransactions()
        {
            Dictionary<string, Person> people = new Dictionary<string, Person>();
            //string path = "Transactions2014.csv";
            string path = "DodgyTransactions2015.csv";
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
                transaction.From = payer;
                transaction.To = payee;
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

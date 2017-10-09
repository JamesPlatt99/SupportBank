using NLog;
using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;

namespace SupportBank
{
    public class XMLParser : IParser
    {
        private string filePath;

        public XMLParser(string filepath)
        {
            filePath = filepath;
        }

        public Dictionary<string, Person> GetPeople()
        {
            Dictionary<string, Person> people = new Dictionary<string, Person>();
            foreach (Transaction transaction in GetTransactions())
            {
                people = TransactionListReader.ParseTransaction(people, transaction);
            }
            return people;
        }

        private Transaction ConvertToTransaction(SupportTransaction supportTransaction)
        {
            Transaction transaction = new Transaction();
            try
            {
                transaction.Date = DateTime.FromOADate(Double.Parse(supportTransaction.Date.Replace("\"", "")));
                transaction.FromAccount = supportTransaction.From;
                transaction.ToAccount = supportTransaction.To;
                transaction.Amount = Double.Parse(supportTransaction.Value);
                transaction.Narrative = supportTransaction.Description;
            }
            catch (Exception e)
            {
                Console.ForegroundColor = System.ConsoleColor.Red;
                Console.WriteLine("Input was not valid, please ensure data is in the correct format. This line has been skipped.");
                Console.WriteLine(e.Message);
                Console.ForegroundColor = System.ConsoleColor.White;
                Console.WriteLine();
                Program.logger.Log(LogLevel.Warn, "Invalid file format was parsed. The erronous line was skipped.");
                return null;
            }
            return transaction;
        }

        public List<Transaction> GetTransactions()
        {
            List<SupportTransaction> tempXMLTransactions = new List<SupportTransaction>();
            List<Transaction> transactions = new List<Transaction>();
            Transaction transaction;
            string fileText;
            StreamReader file = new StreamReader(filePath);
            fileText = file.ReadToEnd();
            TransactionList result;

            fileText = fileText.Replace("<Parties>", "");
            fileText = fileText.Replace("</Parties>", "");
            fileText = fileText.Replace("<SupportTransaction ", "<SupportTransaction> \n<");
            fileText = fileText.Replace("Date=", "Date>");
            fileText = fileText.Replace("\">", "</Date>");

            XmlSerializer serializer = new XmlSerializer(typeof(TransactionList));
            using (TextReader reader = new StringReader(fileText))
            {
                result = (TransactionList)serializer.Deserialize(reader);
            }

            foreach (SupportTransaction curTransaction in result.SupportTransaction)
            {
                transaction = ConvertToTransaction(curTransaction);
                if (transaction != null)
                {
                    transactions.Add(transaction);
                }
            }
            file.Close();
            return transactions;
        }
    }
}
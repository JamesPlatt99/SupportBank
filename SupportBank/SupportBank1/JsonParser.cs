using Newtonsoft.Json;
using NLog;
using System;
using System.Collections.Generic;

namespace SupportBank
{
    internal class JsonParser : IParser
    {
        private string filePath;

        public JsonParser(string filepath)
        {
            filePath = filepath;
        }

        public Dictionary<string, Person> GetPeople()
        {
            Dictionary<string, Person> people = new Dictionary<string, Person>();
            Transaction transaction = new Transaction();
            List<string> jsonObjects = parseFile();
            foreach (string jsonObject in jsonObjects)
            {
                try
                {
                    transaction = JsonConvert.DeserializeObject<Transaction>(jsonObject + '}');
                    people = TransactionListReader.ParseTransaction(people, transaction);
                }
                catch (Exception e)
                {
                    Console.ForegroundColor = System.ConsoleColor.Red;
                    Console.WriteLine("Input was not valid, please ensure data is in the correct format. This line has been skipped.");
                    Console.WriteLine(e.Message);
                    Console.ForegroundColor = System.ConsoleColor.White;
                    Console.WriteLine();
                    Program.logger.Log(LogLevel.Warn, "Invalid file format was parsed. The erronous line was skipped.");
                }
            }
            return people;
        }

        public List<Transaction> GetTransactions()
        {
            List<Transaction> transactions = new List<Transaction>();
            Transaction transaction = new Transaction();
            List<string> jsonObjects = parseFile();
            foreach (string jsonObject in jsonObjects)
            {
                try
                {
                    transaction = JsonConvert.DeserializeObject<Transaction>(jsonObject + '}');
                    transactions.Add(transaction);
                }
                catch (Exception e)
                {
                    Console.ForegroundColor = System.ConsoleColor.Red;
                    Console.WriteLine("Input was not valid, please ensure data is in the correct format. This line has been skipped.");
                    Console.WriteLine(e.Message);
                    Console.ForegroundColor = System.ConsoleColor.White;
                    Console.WriteLine();
                    Program.logger.Log(LogLevel.Warn, "Invalid file format was parsed. The erronous line was skipped.");
                }
            }
            return transactions;
        }

        private List<string> parseFile()
        {
            List<string> objects = new List<string>();
            string curLine;
            string curObject = "";
            System.IO.StreamReader file = new System.IO.StreamReader(filePath);
            file.ReadLine();
            while ((curLine = file.ReadLine()) != "]")
            {
                curObject += curLine;
                if (curLine == "  }," | curLine[curLine.Length - 1] == '}')
                {
                    curObject = curObject.Remove(curObject.Length - 2);
                    objects.Add(curObject);
                    curObject = "";
                }
            }
            file.Close();
            return objects;
        }
    }
}
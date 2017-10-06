using Newtonsoft.Json;
using System.Collections.Generic;

namespace SupportBank
{
    internal class JsonParser : IParser
    {
        public Dictionary<string, Person> GetPeople(string path)
        {
            Dictionary<string, Person> people = new Dictionary<string, Person>();
            Transaction transaction = new Transaction();
            List<string> jsonObjects = parseFile(path);
            foreach (string jsonObject in jsonObjects)
            {
                transaction = JsonConvert.DeserializeObject<Transaction>(jsonObject + '}');
                people = Program.ParseTransaction(people, transaction);
            }
            return people;
        }

        public List<Transaction> GetTransactions(string path)
        {
            List<Transaction> transactions = new List<Transaction>();
            Transaction transaction = new Transaction();
            List<string> jsonObjects = parseFile(path);
            foreach (string jsonObject in jsonObjects)
            {
                transaction = JsonConvert.DeserializeObject<Transaction>(jsonObject + '}');
                transactions.Add(transaction);
            }
            return transactions;
        }

        private List<string> parseFile(string path)
        {
            List<string> objects = new List<string>();
            string curLine;
            string curObject = "";
            System.IO.StreamReader file = new System.IO.StreamReader(path);
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
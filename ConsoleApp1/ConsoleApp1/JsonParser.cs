using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SupportBank
{
    class JsonParser
    {
        public Dictionary<string, Person> GetTransactions()
        {
            Dictionary<string, Person> people = new Dictionary<string, Person>();
            JsonObject curTransaction = new JsonObject();            
            List<string> jsonObjects = GetJsonObjects();
            foreach (string jsonObject in jsonObjects)
            {
                curTransaction = JsonConvert.DeserializeObject<JsonObject>(jsonObject + '}');
                if (!people.ContainsKey(curTransaction.FromAccount))
                {
                    Person person = new Person();
                    person.Name = curTransaction.FromAccount;
                    people.Add(person.Name, person);
                }
                if (!people.ContainsKey(curTransaction.ToAccount))
                {
                    Person person = new Person();
                    person.Name = curTransaction.ToAccount;
                    people.Add(person.Name, person);
                }
                Transaction transaction = CreateTransaction(curTransaction, people);
                Person payer = transaction.From;
                Person payee = transaction.To;
                payer.Balance -= transaction.Amount;
                payee.Balance += transaction.Amount;
                payer.transactions.Add(transaction);
                payee.transactions.Add(transaction);
            }
            return people;
        }
        public Transaction CreateTransaction(JsonObject curTransaction, Dictionary<string, Person> people)
        {
            Transaction transaction = new Transaction();
            try
            {
                transaction.Date = curTransaction.Date;
                transaction.From = people[curTransaction.FromAccount];
                transaction.To = people[curTransaction.ToAccount];
                transaction.Narrative = curTransaction.Narrative;
                transaction.Amount = curTransaction.Amount;
                return transaction;
            }
            catch (Exception e)
            {
                return null;
            }

        }
        private List<string> GetJsonObjects()
        {
            List<string> objects = new List<string>();
            string path = "Transactions2013.json.txt";
            int lineNumber = 0;
            string curLine;
            string curObject = "";
            System.IO.StreamReader file = new System.IO.StreamReader(path);
            file.ReadLine();
            while ((curLine = file.ReadLine()) != "]")
            {
                lineNumber++;
                curObject += curLine;
                if (lineNumber % 7 == 0)
                {
                    curObject = curObject.Remove(curObject.Length - 2);
                    objects.Add(curObject);
                    curObject = "";
                }
            }
            return objects;
        }
    }
}

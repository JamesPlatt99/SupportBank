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
            Transaction transaction = new Transaction();            
            List<string> jsonObjects = parseFile();
            foreach (string jsonObject in jsonObjects)
            {
                transaction = JsonConvert.DeserializeObject<Transaction>(jsonObject + '}');
                if (!people.ContainsKey(transaction.FromAccount))
                {
                    Person person = new Person();
                    person.Name = transaction.FromAccount;
                    people.Add(person.Name, person);
                }
                if (!people.ContainsKey(transaction.ToAccount))
                {
                    Person person = new Person();
                    person.Name = transaction.ToAccount;
                    people.Add(person.Name, person);
                }
                Person payer = people[transaction.FromAccount];
                Person payee = people[transaction.ToAccount];
                payer.Balance -= transaction.Amount;
                payee.Balance += transaction.Amount;
                payer.transactions.Add(transaction);
                payee.transactions.Add(transaction);
            }
            return people;
        }
        private List<string> parseFile()
        {
            List<string> objects = new List<string>();
            string path = Program.chooseFile("json");
            string curLine;
            string curObject = "";
            System.IO.StreamReader file = new System.IO.StreamReader(path);
            file.ReadLine();
            while ((curLine = file.ReadLine()) != "]")
            {
                curObject += curLine;
                if (curLine == "  },")
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

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml;

namespace SupportBank
{
    class XMLParser
    {
        public Dictionary<string,Person> GetTransactions()
        {
            Dictionary<string, Person> people = new Dictionary<string, Person>();
            Transaction transaction = new Transaction();
            List<string> XMLObjects = ParseFileHorribly();
            foreach(string curObject in XMLObjects)
            {
                transaction = getTransaction(curObject);
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
        private Transaction getTransaction(string curObject)
        {
            curObject = curObject.Replace("\"","");
            Transaction transaction = new Transaction();
            string query = @"(?<=Date=)\d{5}(?=>)";
            transaction.Date = DateTime.FromOADate(Convert.ToDouble(regexSearch(curObject,query)));
            query = @"(?<=From>).+(?=<\/From)";
            transaction.FromAccount = regexSearch(curObject, query);
            query = @"(?<=To>).+(?=<\/To)";
            transaction.ToAccount = regexSearch(curObject, query);
            query = @"(?<=<Description>).*(?=<\/Description>)";
            transaction.Narrative = regexSearch(curObject, query);
            query = @"(?<=<Value>).*(?=<\/Value>)";
            transaction.Amount = Convert.ToDouble(regexSearch(curObject, query));

            return transaction;
        }
        private string regexSearch(string str, string query)
        {
            string output = "";
            output = Regex.Match(str,query).ToString();
            return output;
        }
        //private List<string> ParseFile()
        //{
        //    List<string> objects = new List<string>();
        //    string path = "Transactions2012.XML";
        //    string fileText;
        //    string curObject;
        //    System.IO.StreamReader file = new System.IO.StreamReader(path);
        //    fileText = file.ReadToEnd();
        //    xml
        //    return objects;
        //}
        private List<string> ParseFileHorribly()
        {
            List<string> objects = new List<string>();
            string path = Program.chooseFile("xml");
            string curLine;
            string curObject = "";
            System.IO.StreamReader file = new System.IO.StreamReader(path);
            file.ReadLine();
            while ((curLine = file.ReadLine()) != @"</TransactionList>")
            {
                curObject += curLine;
                if (curLine == @"  </SupportTransaction>")
                {
                    objects.Add(curObject);
                    curObject = "";
                }
            }
            return objects;
        }    
    }
}

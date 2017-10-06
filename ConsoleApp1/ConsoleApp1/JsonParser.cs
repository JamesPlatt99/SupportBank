using Newtonsoft.Json;
using System.Collections.Generic;

namespace SupportBank
{
    internal class JsonParser
    {
        public Dictionary<string, Person> GetTransactions()
        {
            Dictionary<string, Person> people = new Dictionary<string, Person>();
            Transaction transaction = new Transaction();
            List<string> jsonObjects = parseFile();
            foreach (string jsonObject in jsonObjects)
            {
                transaction = JsonConvert.DeserializeObject<Transaction>(jsonObject + '}');
                people = Program.ParseTransaction(people, transaction);
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
                if (curLine == "  }," | curLine[curLine.Length - 1] == '}')
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
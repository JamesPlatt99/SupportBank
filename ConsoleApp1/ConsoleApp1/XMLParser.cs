using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SupportBank
{
    class XMLParser
    {
        public Dictionary<string, Person> GetTransactions()
        {
            Dictionary<string, Person> people = new Dictionary<string, Person>();
            
        }
        public Transaction CreateTransaction()
        {

        }
        private List<string> GetObjects()
        {
            string path = "Transactions2012.xml";

        }
        private DateTime getDate(int days)
        {
            DateTime date = DateTime.Parse("1/1/1900");
            date.AddDays(days);
            return date;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SupportBank
{
    public class Person
    {
        public List<Transaction> transactions = new List<Transaction>();
        public string Name { get; set; }
        public double Balance { get; set; }
    }
}

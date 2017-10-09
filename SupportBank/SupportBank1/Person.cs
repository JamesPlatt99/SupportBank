using System.Collections.Generic;

namespace SupportBank
{
    public class Person
    {
        public List<Transaction> transactions = new List<Transaction>();
        public string Name { get; set; }
        public double Balance { get; set; }
    }
}
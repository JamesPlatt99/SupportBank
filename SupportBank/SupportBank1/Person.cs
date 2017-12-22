using System.Collections.Generic;

namespace ConsoleApp1
{
    public class Person
    {
        public List<Transaction> transactions = new List<Transaction>();
        public string Name { get; set; }
        public double Balance { get; set; }
    }
}
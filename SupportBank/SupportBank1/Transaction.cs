using System;

namespace SupportBank
{
    public class Transaction
    {
        public DateTime Date { get; set; }
        public String FromAccount { get; set; }
        public String ToAccount { get; set; }
        public String Narrative { get; set; }
        public Double Amount { get; set; }
    }
}
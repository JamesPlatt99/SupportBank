using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SupportBank
{
    class Transaction
    {
        public DateTime Date { get; set; }
        public Person From { get; set; }
        public Person To { get; set; }
        public string Narrative { get; set; }
        public double Amount { get; set; }
    }
}

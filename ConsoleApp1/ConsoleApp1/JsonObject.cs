using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SupportBank
{
    class JsonObject
    {
        public DateTime Date { get; set; }
        public String FromAccount { get; set; }
        public String ToAccount { get; set; }
        public String Narrative { get; set; }
        public Double Amount { get; set; }
    }
}

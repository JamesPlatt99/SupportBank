using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace SupportBank
{
    [XmlRoot("TransactionList")]
    public class TransactionList
    {
        [XmlElement("SupportTransaction")]
        public List<SupportTransaction> SupportTransaction { get; set; }
    }
}

using System.Collections.Generic;
using System.Xml.Serialization;

namespace ConsoleApp1
{
    [XmlRoot("TransactionList")]
    public class TransactionList
    {
        [XmlElement("SupportTransaction")]
        public List<SupportTransaction> SupportTransaction { get; set; }
    }
}
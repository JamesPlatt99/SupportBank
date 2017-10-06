using System.Xml.Serialization;

namespace SupportBank
{
    public class SupportTransaction
    {
        [XmlElement("Date")]
        public string Date { get; set; }

        [XmlElement("From")]
        public string From { get; set; }

        [XmlElement("To")]
        public string To { get; set; }

        [XmlElement("Description")]
        public string Description { get; set; }

        [XmlElement("Value")]
        public string Value { get; set; }
    }
}
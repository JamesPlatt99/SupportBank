using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SupportBank
{
    class ParserFactory
    {
        public dynamic GetParser;
        public ParserFactory(string filePath)
        {
            switch(filePath.Substring(filePath.Length - 4, 4))
            {
                case ".csv":
                    GetParser = new CSVParser();
                    break;
                case "json":
                    GetParser = new JsonParser();
                    break;
                case ".xml":
                    GetParser = new XMLParser();
                    break;
            }
        }
    }
}

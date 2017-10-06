namespace SupportBank
{
    internal class ParserFactory
    {
        public dynamic GetParser;

        public ParserFactory(string filePath)
        {
            switch (filePath.Substring(filePath.Length - 4, 4))
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
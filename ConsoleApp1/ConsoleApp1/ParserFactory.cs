namespace SupportBank
{
    internal class ParserFactory
    {      
        public IParser GetParser(string filePath)
        {
            switch(filePath.Substring(filePath.Length - 4, 4))
            {
                case ".csv":
                    return new CSVParser(filePath);

                case "json":
                    return new JsonParser(filePath);

                case ".xml":
                    return new XMLParser(filePath);
                default:
                    return null;
            }

        }
    }
}
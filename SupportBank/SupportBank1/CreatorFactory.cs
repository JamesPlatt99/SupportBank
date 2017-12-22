namespace ConsoleApp1
{
    internal class CreatorFactory
    {
        public ICreator GetCreator(string fileName)
        {
            switch (fileName.Substring(fileName.Length - 4, 4))
            {
                case ".csv":
                    return new CreateCSVFile(fileName);

                case "json":
                    return new CreateJsonFile(fileName);

                case ".xml":
                    return new CreateXMLFile(fileName);

                default:
                    return null;
            }
        }
    }
}
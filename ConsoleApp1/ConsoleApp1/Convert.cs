using System;
using System.Collections.Generic;

namespace SupportBank
{
    internal class ConvertFile
    {
        public void Start()
        {
            List<Transaction> transactions = new List<Transaction>();
            int fileType;
            string file;
            Console.WriteLine();
            Export exporter = new Export();
            Import importer = new Import();
            file = Program.chooseFile();
            switch (file.Substring(file.Length -4,4))
            {
                case ".csv":
                    CSVParser csvParser = new CSVParser();
                    transactions = csvParser.GetTransactions(file);
                    break;

                case "json":
                    JsonParser jsonParser = new JsonParser();
                    transactions = jsonParser.GetTransactions(file);
                    break;

                case ".xml":
                    XMLParser xmlParser = new XMLParser();
                    transactions = xmlParser.GetTransactions(file);
                    break;
            }

            fileType = importer.FileType("convert to");
            file = file.Substring(0, file.Length - 4);
            switch (fileType)
            {
                case 1:
                    exporter.CreateCSVFile(file + ".csv", transactions);
                    break;

                case 2:
                    exporter.CreateJSONFile(file + ".json", transactions);
                    break;

                case 3:
                    exporter.CreateXMLFile(file + ".xml", transactions);
                    break;
            }
        }
    }
}
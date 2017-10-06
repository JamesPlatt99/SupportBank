using System;
using System.Collections.Generic;

namespace SupportBank
{
    internal class ConvertFile
    {
        public void Start()
        {
            List<Transaction> transactions = new List<Transaction>();
            string fileType;
            string file;
            Console.WriteLine();
            Export exporter = new Export();
            Import importer = new Import();
            file = Program.chooseFile();

            ParserFactory parserFactory = new ParserFactory();
            transactions = parserFactory.GetParser(file).GetTransactions();

            fileType = Program.chooseFileType("convert to");
            file = file.Substring(0, file.Length - 4);
            switch (fileType)
            {
                case "csv":
                    exporter.CreateCSVFile(file + ".csv", transactions);
                    break;

                case "json":
                    exporter.CreateJSONFile(file + ".json", transactions);
                    break;

                case "xml":
                    exporter.CreateXMLFile(file + ".xml", transactions);
                    break;
            }
        }
    }
}
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
            Import importer = new Import();
            file = Program.chooseFile();

            ParserFactory parserFactory = new ParserFactory();
            transactions = parserFactory.GetParser(file).GetTransactions();

            fileType = Program.chooseFileType("convert to");
            file = file.Substring(0, file.Length - 4) + fileType;
            CreatorFactory creatorFactory = new CreatorFactory();
            ICreator creator = creatorFactory.GetCreator(file);
            creator.CreateFile(transactions);
        }
    }
}
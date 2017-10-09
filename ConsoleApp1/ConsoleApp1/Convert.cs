using System;
using System.Collections.Generic;
using System.IO;

namespace SupportBank
{
    internal class ConvertFile
    {
        public void Start()
        {
            List<Transaction> transactions = new List<Transaction>();
            string fileType;
            string file;
            string newFile;
            Console.WriteLine();
            Import importer = new Import();
            file = Program.ChooseFile();

            ParserFactory parserFactory = new ParserFactory();
            transactions = parserFactory.GetParser(file).GetTransactions();

            fileType = Program.ChooseFileType("convert to");
            newFile = file.Substring(0, file.Length - 4) + fileType;
            CreatorFactory creatorFactory = new CreatorFactory();
            ICreator creator = creatorFactory.GetCreator(newFile);
            creator.CreateFile(transactions);

            File.Delete(file);
        }
    }
}
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
            string curFile;
            string curFileType;
            string newFile;
            string newFileType;
            Console.WriteLine();
            TransactionReader importer = new TransactionReader();
            curFile = Program.ChooseFile();

            ParserFactory parserFactory = new ParserFactory();
            transactions = parserFactory.GetParser(curFile).GetTransactions();
            curFileType = parserFactory.GetFileExtension(curFile);

            newFileType = Program.ChooseFileType("convert to");
            newFile = curFile.Substring(0, curFile.Length - curFileType.Length) + "." + newFileType;
            CreatorFactory creatorFactory = new CreatorFactory();
            ICreator creator = creatorFactory.GetCreator(newFile);
            creator.CreateFile(transactions);

            File.Delete(curFile);
        }
    }
}
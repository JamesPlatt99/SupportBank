using System;
using System.Collections.Generic;
using System.IO;

namespace ConsoleApp1
{
    internal class ConvertFile
    {
        public void Start()
        {
            Console.WriteLine();
            string curFile = Program.ChooseFile();

            ParserFactory parserFactory = new ParserFactory();
            List<Transaction> transactions = parserFactory.GetParser(curFile).GetTransactions();
            string curFileType = parserFactory.GetFileExtension(curFile);

            string newFileType = Program.ChooseFileType("convert to");
            string newFile = curFile.Substring(0, curFile.Length - curFileType.Length) + "." + newFileType;
            CreatorFactory creatorFactory = new CreatorFactory();
            ICreator creator = creatorFactory.GetCreator(newFile);
            creator.CreateFile(transactions);

            File.Delete(curFile);
        }
    }
}
using System;
using System.Collections.Generic;
using CsvHelper;
using NLog;

namespace ConsoleApp1
{
    internal class CreateCSVFile : ICreator
    {
        private readonly string _fileName;

        public CreateCSVFile(string fileName)
        {
            _fileName = fileName;
        }

        public void CreateFile(IEnumerable<Transaction> transactions)
        {
            System.IO.StreamWriter file = new System.IO.StreamWriter(_fileName);
            CsvWriter csv = new CsvWriter(file);
            file.WriteLine("Date,From,To,Narrative,Amount\n");
            csv.WriteRecords(transactions);
            file.Close();
            Program.logger.Log(LogLevel.Info, $"The file {_fileName} was created successfully.");
        }
    }
}
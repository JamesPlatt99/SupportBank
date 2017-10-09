﻿using CsvHelper;
using NLog;
using System;
using System.Collections.Generic;

namespace SupportBank
{
    internal class CreateCSVFile : ICreator
    {
        private string fileName;

        public CreateCSVFile(string filename)
        {
            fileName = filename;
        }

        public void CreateFile(List<Transaction> transactions)
        {
            System.IO.StreamWriter file = new System.IO.StreamWriter(fileName);
            var csv = new CsvWriter(file);
            file.WriteLine("Date,From,To,Narrative,Amount\n");
            csv.WriteRecords(transactions);
            file.Close();
            Program.logger.Log(LogLevel.Info, String.Format("The file {0} was created successfully.", fileName));
        }
    }
}
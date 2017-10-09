﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SupportBank
{
    class Congregator
    {
        public void CreateGlobalFile()
        {
            List<string> validFiles = GetValidFiles();
            List<Transaction> transactions = new List<Transaction>();
            ParserFactory parserFactory = new ParserFactory();
            CreateJsonFile creator = new CreateJsonFile("Global.json");

            foreach (string file in validFiles)
            {
                try
                {
                    transactions = transactions.Concat(parserFactory.GetParser(file).GetTransactions()).ToList();
                }
                catch(Exception e)
                {
                    Program.Logissue("Invalid file format :" + e.Message, NLog.LogLevel.Error);
                }
            }
            System.IO.File.Delete("Global.json");
            creator.CreateFile(transactions);
        }
        private List<string> GetValidFiles()
        {
            string[] files = System.IO.Directory.GetFiles(System.IO.Directory.GetCurrentDirectory());
            List<string> validFiles = new List<string>();
            foreach (string file in files)
            {
                foreach (Program.validFiles fileType in Enum.GetValues(typeof(Program.validFiles)))
                {
                    if (file.Substring(file.Length - fileType.ToString().Length) == fileType.ToString() && file.Substring(file.Length - 12, 12) != @"\Global.json")
                    {
                        validFiles.Add(file);
                    }
                }
            }
            return validFiles;
        }
    }
}
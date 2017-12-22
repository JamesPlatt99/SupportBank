using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using NLog;

namespace ConsoleApp1
{
    internal class CreateJsonFile : ICreator
    {
        private readonly string _fileName;

        public CreateJsonFile(string fileName)
        {
            _fileName = fileName;
        }

        public void CreateFile(IEnumerable<Transaction> transactions)
        {
            System.IO.StreamWriter file = new System.IO.StreamWriter(_fileName);
            file.WriteLine("[");
            foreach (Transaction transaction in transactions)
            {
                file.WriteLine(JsonConvert.SerializeObject(transaction));
            }
            file.WriteLine("]");
            file.Close();
            Program.logger.Log(LogLevel.Info, $"The file {_fileName} was created successfully.");
        }
    }
}
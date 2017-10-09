using Newtonsoft.Json;
using NLog;
using System;
using System.Collections.Generic;

namespace SupportBank
{
    internal class CreateJsonFile : ICreator
    {
        private string fileName;

        public CreateJsonFile(string filename)
        {
            fileName = filename;
        }

        public void CreateFile(List<Transaction> transactions)
        {
            System.IO.StreamWriter file = new System.IO.StreamWriter(fileName);
            file.WriteLine("[");
            foreach (Transaction transaction in transactions)
            {
                file.WriteLine(JsonConvert.SerializeObject(transaction));
            }
            file.WriteLine("]");
            file.Close();
            Program.logger.Log(LogLevel.Info, String.Format("The file {0} was created successfully.", fileName));
        }
    }
}
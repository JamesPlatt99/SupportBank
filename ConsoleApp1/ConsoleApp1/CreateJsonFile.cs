using Newtonsoft.Json;
using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SupportBank
{
    class CreateJsonFile : ICreator
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
            Program.Logissue(String.Format("The file {0} was created successfully.", fileName), LogLevel.Info);
        }
    }
}

using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SupportBank
{
    class CreateCSVFile : ICreator
    {
        private string fileName;
        public CreateCSVFile(string filename)
        {
            fileName = filename;
        }
        public void CreateFile(List<Transaction> transactions)
        {
            System.IO.StreamWriter file = new System.IO.StreamWriter(fileName);
            file.WriteLine("Date,From,To,Narrative,Amount\n");
            foreach (Transaction transaction in transactions)
            {
                file.WriteLine("{0},{1},{2},{3},{4}\n", transaction.Date, transaction.FromAccount, transaction.ToAccount, transaction.Narrative, transaction.Amount);
            }
            file.Close();
            Program.Logissue(String.Format("The file {0} was created successfully.", fileName), LogLevel.Info);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SupportBank
{
    class CreateXMLFile : ICreator
    {
        private string fileName;
        public CreateXMLFile(string filename)
        {
            fileName = filename;
        }
        public void CreateFile( List<Transaction> transactions)
        {
            System.IO.StreamWriter file = new System.IO.StreamWriter(fileName);
            file.WriteLine("<?xml version=\"1.0\" encoding=\"utf - 8\"?>");
            file.WriteLine("<TransactionList>");
            foreach (Transaction transaction in transactions)
            {
                file.WriteLine("  <SupportTransaction Date=\"{0}\">\n", transaction.Date.ToOADate());
                file.WriteLine("    <Description>{0}</Description>\n", transaction.Narrative);
                file.WriteLine("    <Value>{0}</Value>\n", transaction.Amount);
                file.WriteLine("    <Parties>\n");
                file.WriteLine("      <From>{0}</From>\n", transaction.FromAccount);
                file.WriteLine("      <To>{0}</To>\n", transaction.ToAccount);
                file.WriteLine("    </Parties>\n");
                file.WriteLine("  </SupportTransaction>\n");
            }
            file.WriteLine("</TransactionList>");
            file.Close();
            Program.Logissue(String.Format("The file {0} was created successfully.", fileName), LogLevel.Info);
        }
    }
}

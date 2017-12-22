using System;
using System.Collections.Generic;
using NLog;

namespace ConsoleApp1
{
    internal class CreateXMLFile : ICreator
    {
        private readonly string _fileName;

        public CreateXMLFile(string fileName)
        {
            _fileName = fileName;
        }

        public void CreateFile(IEnumerable<Transaction> transactions)
        {
            System.IO.StreamWriter file = new System.IO.StreamWriter(_fileName);
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
            Program.logger.Log(LogLevel.Info, $"The file {_fileName} was created successfully.");
        }
    }
}
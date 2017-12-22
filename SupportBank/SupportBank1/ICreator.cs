using System.Collections.Generic;

namespace ConsoleApp1
{
    internal interface ICreator
    {
        void CreateFile(IEnumerable<Transaction> transactions);
    }
}
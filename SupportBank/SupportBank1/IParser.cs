using System.Collections.Generic;

namespace ConsoleApp1
{
    internal interface IParser
    {
        Dictionary<string, Person> GetPeople();

        List<Transaction> GetTransactions();
    }
}
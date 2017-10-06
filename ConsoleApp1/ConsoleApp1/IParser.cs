using System.Collections.Generic;

namespace SupportBank
{
    internal interface IParser
    {
        Dictionary<string, Person> GetPeople(string path);

        List<Transaction> GetTransactions(string path);
    }
}
using System.Collections.Generic;

namespace SupportBank
{
    internal interface IParser
    {
        Dictionary<string, Person> GetPeople();

        List<Transaction> GetTransactions();
    }
}
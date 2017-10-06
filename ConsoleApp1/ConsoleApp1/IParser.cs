using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SupportBank
{
    interface IParser
    {
        Dictionary<string, Person> GetPeople(string path);
        List<Transaction> GetTransactions(string path);
    }
}

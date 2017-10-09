using System.Collections.Generic;

namespace SupportBank
{
    internal interface ICreator
    {
        void CreateFile(List<Transaction> transactions);
    }
}
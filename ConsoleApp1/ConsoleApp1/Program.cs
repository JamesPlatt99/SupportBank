using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SupportBank
{
    class Program
    {
        static void Main(string[] args)
        {
            Dictionary<string,Person> people = GetTransactions();
            while (true)
            {
                switch (MenuOption())
                {
                    case 1:
                        ListAll(people);
                        break;
                    case 2:
                        ListAccount(people);
                        break;
                }
            }

        }
        static void ListAccount(Dictionary<string,Person> people)
        {
            string account = "";
            Boolean validInput = false;
            Person person;
            while (!validInput)
            {
                Console.Write("  Please enter the name of the account: ");
                account = Console.ReadLine();
                if (people.ContainsKey(account))
                {
                    validInput = true;
                }
                else
                {
                    Console.WriteLine(" Invalid input, pleaese try again.");
                }
            }
            person = people[account];
            Console.WriteLine("   Date, From, To, Narrative, Amount");
            foreach(Transaction transaction in person.transactions)
            {
                Console.WriteLine("   {0}, {1}, {2}, {3}, {4}",transaction.Date, transaction.From.Name, transaction.To.Name, transaction.Narrative,transaction.Amount);
            }

        }
        static void ListAll(Dictionary<string, Person> people)
        {
            foreach(KeyValuePair<string,Person> person in people)
            {
                Console.WriteLine(" Name: {0}; Balance: {1};",person.Value.Name, person.Value.Balance);
            }
        }
        static int? MenuOption()
        {
            Boolean valid = false;
            string input;
            int choice = 0;
            Console.WriteLine("Support Bank");
            Console.WriteLine("------------------------------------");
            Console.WriteLine("  1. List all");
            Console.WriteLine("  2. List account");

            while (!valid)
            {
                Console.Write("  :");
                input = Console.ReadLine();
                if (int.TryParse(input, out choice) && choice > 0 && choice <= 2){
                    valid = true;
                    return choice;
                }
                Console.WriteLine("Invalid input, please try again.");
            }
            return null;
        }
        static Dictionary<string, Person> GetTransactions()
        {
            Dictionary<string, Person> people = new Dictionary<string, Person>();
            //string path = "Transactions2014.csv";
            string path = "DodgyTransactions2015.csv";
            string tmp;
            string[] line;
            int curLine = 1;
            System.IO.StreamReader file = new System.IO.StreamReader(path);
            tmp = file.ReadLine();
            while ((tmp = file.ReadLine()) != null)
            {
                curLine++;
                line = tmp.Split(',');
                if (!people.ContainsKey(line[1])) {
                    Person person = new Person();
                    person.Name = line[1];
                    people.Add(person.Name, person);
                }
                if (!people.ContainsKey(line[2]))
                {
                    Person person = new Person();
                    person.Name = line[2];
                    people.Add(person.Name, person);
                }
                Person payer = people[line[1]];
                Person payee = people[line[2]];
                Transaction transaction = CreateTransaction(line, payer, payee);
                if(transaction != null) { 
                    payer.Balance -= transaction.Amount;
                    payee.Balance += transaction.Amount;
                    payer.transactions.Add(transaction);
                    payee.transactions.Add(transaction);
                }
            }
            return people;
        }
        static Transaction CreateTransaction(string[] transactionAr, Person payer,Person payee)
        {
            Transaction transaction = new Transaction();
            try { 
            transaction.Date = DateTime.Parse(transactionAr[0]);
            transaction.From = payer;
            transaction.To = payee;
            transaction.Narrative = transactionAr[3];
            transaction.Amount = Convert.ToDouble(transactionAr[4]);
            }
            catch(Exception)
            {
                Console.WriteLine("Input \"{0}\" was not valid, please ensure data is in the correct format. This line has been skipped.", string.Join(",", transactionAr));
                Console.WriteLine();
                return null;
            }
            return transaction;
        }
    }
   
}

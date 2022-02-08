using Events;

namespace Program
{
    public class Program
    {
        public static Queue<string> Queries = new Queue<string>();

        public static void PerformeQuery(object? o, InsertEventArgs args)
        {
            Console.WriteLine($"Performed Query {args.Query}");
        }

        public static void CompleteQuery(object? o, EventArgs args)
        {
            Console.WriteLine($"Performed Queries");
        }

        public static string QueryBuilder(string? name, string? age)
        {
            return $"INSERT INTO users (name, age) VALUES ({name}, {age})";
        }

        public static void Main()
        {
            var insert = new Insert();

            insert.InsertPerformed += PerformeQuery;
            insert.InsertCompleted += CompleteQuery;

            string? option;
            string? name;
            string? age;
            int count = 0;
            while (true)
            {
                Console.WriteLine("1- Adicionar Usuario || 2- Commit DataBase\n");
                option = Console.ReadLine();
                if ((option is null || option == "2") && count > 0)
                {
                    insert.Run(Queries);
                    break;
                }
                else
                {
                    Console.Write("Digite o Nome do Usuario: ");
                    name = Console.ReadLine();
                    Console.Write("Digite a Idade do Usuario: ");
                    age = Console.ReadLine();
                    Queries.Enqueue(QueryBuilder(name, age));
                    count++;
                }
            }

        }
    }

}

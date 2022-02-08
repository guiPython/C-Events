Action<string> InsertInMySql = query =>
{
    Console.WriteLine($"{query} --- MYSQL");
};

Action<string> InsertInMemory = query =>
{
    Console.WriteLine($"{query} --- InMemory");
};

Action<string> ConsoleLog = query =>
{
    Console.WriteLine($"Completed {query}");
};

Action<string> TxtLog = query =>
{
    string path = "path of logs.txt";
    using var sw = new StreamWriter(path, true);
    sw.WriteLine(query);
};


Action<string> onPerformed;
Action<string> onCompleted;
Action<Queue<string>, Action<string>, Action<string>> run = (queries, perform, complete) =>
 {
     while (queries.Count() > 0)
     {
         var query = queries.Dequeue();
         perform(query);
         complete(query);
     }
 };

Queue<string> queries = new Queue<string>();
onPerformed = InsertInMemory + InsertInMySql;
onCompleted = TxtLog + ConsoleLog;

while (true)
{
    Console.WriteLine("1- Adicionar novo usuario\n2- Rodar Inserts");
    if (int.Parse(Console.ReadLine()) == 1)
    {
        Console.Write("Insira seu nome: ");
        var nome = Console.ReadLine();

        Console.Write("Insira sua idade: ");
        var idade = int.Parse(Console.ReadLine());
        queries.Enqueue($"INSERT INTO users (name, age) VALUES ({nome}, {idade})");

        Console.Clear();
    }
    else
    {
        run(queries, onPerformed, onCompleted);
        break;
    }
}



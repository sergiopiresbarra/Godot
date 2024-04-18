class Program
{
    static void Main()
    {
        Hello.say_hello();
        //Hello.say_my_name();
        //Hello.frutas();
        //ILogger logger = new ConsoleLogger();
        ILogger logger = new FileLogger("mylog.txt");
        BankAccount account1 = new BankAccount("fredi", 100, logger);
        BankAccount account2 = new BankAccount("mariana", 100, logger);
        BankAccount account3 = new BankAccount("sergio", 300, logger);

        account1.Deposit(-100);
        account2.Deposit(100);

        //lista dinamica
        List<BankAccount> accounts = new List<BankAccount> { account1, account2 };
        accounts.Add(account3);
        foreach (var item in accounts)
        {
            Console.WriteLine($"Name: {item.Name}, Balance: {item.Balance}");
        }

        //Console.WriteLine(account2.Balance);
        //o modificador require exige a inicialização dos membros marcados com require
        DataStore<int> store = new DataStore<int>(){Value = 0};
        store.Value = 18;
        Console.WriteLine(store.Value);

        Calculate calculate = new Calculate(Sum);
        var result = calculate(10, 20);
        Console.WriteLine("Delegate Result: "+result);

        //metodo anonimo com delegate
        var multiply = delegate (int x, int y){return x * y;};
        Console.WriteLine(multiply(10, 5));
        //expressao lambda
        var multiply2 = (int x, int y) => x * y;
        Console.WriteLine(multiply2(10, 7));
    }

    static int Sum(int a, int b){
        return a + b;
    }

}

delegate int Calculate(int x, int y);

class DataStore<T>
{
    public required T Value { get; set; }
}

class FileLogger : ILogger
{
    private readonly string filepath;
    readonly ILogger console = new ConsoleLogger();

    public FileLogger(string filepath)
    {
        this.filepath = filepath;
    }
    public void Log(string message)
    {

        console.Log(message);
        File.AppendAllText(filepath, $"{message}{Environment.NewLine}");
    }
}

class ConsoleLogger : ILogger
{
}

interface ILogger
{
    void Log(string message)
    {
        Console.WriteLine($"LOGGER: {message}");
    }
}

class BankAccount
{
    private readonly ILogger logger;

    //props - getters e setters
    public decimal Balance { get; private set; }
    public string Name { get; }

    public BankAccount(string name, decimal balance, ILogger logger)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            throw new ArgumentException("Nome Invalido!", nameof(name));
        }
        if (balance < 0)
        {
            throw new Exception("Saldo nao pode ser negativo!");
        }
        Name = name;
        Balance = balance;
        this.logger = logger;
    }

    public void Deposit(decimal amount)
    {
        if (amount <= 0)
        {
            logger.Log($"Nao é possivel depositar {amount} na conta de {Name}");
            //throw new Exception("Deposito tem que ser positivo!");
            return;
        }
        Balance += amount;
    }

    /* public decimal GetBalance(){
        return balance;
    } */
}

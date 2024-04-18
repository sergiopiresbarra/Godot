class Hello
{
    public static void say_hello()
    {
        Console.ForegroundColor = ConsoleColor.Red;
        //imprimindo na tela
        Console.WriteLine("hello!");
        Console.ResetColor();
    }
    public static void say_my_name()
    {
        //lendo dados de entrada
        Console.Write("Digite o seu nome: ");
        string? name = Console.ReadLine();
        Console.WriteLine($"O seu nome é: {name}!");
        Console.WriteLine("Digite seu ano de nascimento: ");
        //tipos anulaveis
        int? year;
        if (int.TryParse(Console.ReadLine(), out int x)) {
            year = x;
        }else{
            year = null;
        }
        int age = DateTime.Now.Year - year.GetValueOrDefault(DateTime.Now.Year);
        Console.WriteLine($"Voce tem: {age} anos.");
        legal_age(age);
    }

    static void legal_age(int age){
        //condicionais
        if (age >= 18){
            Console.WriteLine("Voce é Maior de idade!");
        }
        else
        {
            Console.WriteLine("Voce é Menor de idade!");
        }
    }

    public static void frutas(){
        //Arrays e Loop
        string[] frutas = {"maçã", "pera", "abacate", "banana"};
        int pos = 7;
        try
        {
             Console.WriteLine(frutas[pos]);
        }
        catch (System.Exception)
        {
             Console.WriteLine($"deu ruim! 🥲, nao existe... nao existe a fruta {pos+1}...");
        }
       
        for (int i = 0; i < frutas.Length; i++)
        {
            if(i != frutas.Length-1) Console.Write(frutas[i] + " ");
            else Console.Write(frutas[i]);
        }
        Console.Write(".\n");
    }

}
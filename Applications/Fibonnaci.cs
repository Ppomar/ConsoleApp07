namespace ConsoleApp07.Applications;

public class Fibonnaci
{
    public static void Execute() 
    {
        //create a fibonacci sequence wir the next result with 10(after try with 100) repetitions: 0 1 1 2 3 5 8 13 21 34 55

        GetFibonacci(10);
    }

    private static void GetFibonacci(int repetitions) 
    {
        int a = 0, b = 1, c = 0;

        for (int i = 0; i <= repetitions; i++) 
        {
            Console.Write("{0} ", a);
            c = a;
            a = b;
            b = c + b;
        }
    }

}

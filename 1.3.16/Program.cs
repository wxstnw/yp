using System;
class Program
{
    static void Main()
    {
        Console.Write("Введите количество чисел Фибоначчи (N): ");
        int n;
        if (!int.TryParse(Console.ReadLine(), out n) || n <= 0)
        {
            Console.WriteLine("Введите положительное целое число.");
            return;
        }
        long a = 0, b = 1;
        Console.WriteLine("Первые {0} чисел Фибоначчи:", n);
        for (int i = 0; i < n; i++)
        {
            Console.Write(a + " ");
            long temp = a;
            a = b;
            b = temp + b;
        }
        Console.WriteLine();
    }
}

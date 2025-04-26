using System;
class Program
{
    static void Main()
    {
        // Курс валют (можно обновить при необходимости)
        double usdToRub = 93.5;
        double eurToRub = 101.2;
        double gbpToRub = 117.8;
        Console.WriteLine("Выберите валюту для конвертации (USD, EUR, GBP):");
        string currency = Console.ReadLine().ToUpper();
        Console.Write("Введите сумму: ");
        double amount;
        if (!double.TryParse(Console.ReadLine(), out amount) || amount < 0)
        {
            Console.WriteLine("Неверная сумма.");
            return;
        }
        double result = 0;

        // Разветвление по выбору валюты
        if (currency == "USD")
        {
            result = amount * usdToRub;
            Console.WriteLine($"{amount} USD = {result:F2} RUB");
        }
        else if (currency == "EUR")
        {
            result = amount * eurToRub;
            Console.WriteLine($"{amount} EUR = {result:F2} RUB");
        }
        else if (currency == "GBP")
        {
            result = amount * gbpToRub;
            Console.WriteLine($"{amount} GBP = {result:F2} RUB");
        }
        else
        {
            Console.WriteLine("Неизвестная валюта.");
        }
    }
}

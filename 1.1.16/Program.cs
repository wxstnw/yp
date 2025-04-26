using System;
class Program
{
    static void Main()
    {
        // Время, за которое каждая труба наполняет бассейн
        double timePipe1 = 6.0;
        double timePipe2 = 4.0;
        // Производительность каждой трубы (часть бассейна в час)
        double ratePipe1 = 1 / timePipe1;
        double ratePipe2 = 1 / timePipe2;
        // Суммарная производительность
        double totalRate = ratePipe1 + ratePipe2;
        // Время на наполнение бассейна при совместной работе
        double totalTime = 1 / totalRate;
        Console.WriteLine("Бассейн наполнится за {0:F2} часов.", totalTime);
    }
}

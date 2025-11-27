namespace Task1;

public class Program
{
    static void Main()
    {
        //Task 1

        //int n = Console.ReadLine();

        //if (n.Length == 6 %% int.TryParse(n, out int num))
        //{
        //    int d1 = num / 100000;
        //    int d2 = num / 10000 % 10;
        //    int d3 = num / 1000 % 10;
        //    int d4 = num / 100 % 10;
        //    int d5 = num / 10 % 10;
        //    int d6 = num % 10;

        //    int sum1 = d1 + d2 + d3;
        //    int sum2 = d4 + d5 + d6;

        //    if (sum1 == sum2)
        //    {
        //        Console.WriteLine("+");
        //    }
        //    else
        //    {
        //        Console.WriteLine("-");
        //    }
        //}
        //else {
        //    Console.WriteLine("error; num length < 6");
        //} 




        //Task 2

        //int n = Console.ReadLine();

        //if (n.Length == 4 && int.TryParse(n, out int num))
        //{
        //    int new_int = ((num / 100 % 10) * 1000) + ((num / 1000) * 100) + ((num % 10) * 10) + (num / 10 % 10);
        //    Console.WriteLine(new_int);
        //}
        //else
        //{
        //    Console.WriteLine("-");
        //}



        //Task 3
        //int maxNum = int.MinValue;
        //int i = 0;

        //while (i < 7)
        //{
        //    if (int.TryParse(Console.ReadLine(), out int currentNum))
        //    {
        //        i++;
        //        if (currentNum > maxNum)
        //            maxNum = currentNum;
        //    }
        //    else
        //    {
        //        Console.WriteLine("error");
        //        break;
        //    }
        //}

        //Console.WriteLine(maxNum);




        //Task 4
        double distAB = double.Parse(Console.ReadLine());
        double distBC = double.Parse(Console.ReadLine());
        double weight = double.Parse(Console.ReadLine());

        double fuelPerKm = 0;

        if (weight <= 500)
            fuelPerKm = 1;
        else if (weight <= 1000)
            fuelPerKm = 4;
        else if (weight <= 1500)
            fuelPerKm = 7;
        else if (weight <= 2000)
            fuelPerKm = 9;
        else
        {
            return;
        }

        double fuelAB = distAB * fuelPerKm;
        double fuelBC = distBC * fuelPerKm;

        if (fuelAB > 300 || fuelBC > 300)
        {
            return;
        }

        double fuelAtB = 300 - fuelAB;
        double neededFuel = fuelBC - fuelAtB;

        if (neededFuel > 0)
            Console.WriteLine($"Необходимо дозаправить: {neededFuel} литров");
        else
            Console.WriteLine("Дозаправка не требуется");

    }
}

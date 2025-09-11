namespace Task1;

public class Program
{
  static void Main()
  {
    int num1 = int.Parse(Console.ReadLine());
    int num2 = int.Parse(Console.ReadLine());

    if (num1 != num2)
    {
      Console.WriteLine($"{int.Min(num1, num2)} {int.Max(num1, num2)}");
    }
    else
    {
      Console.WriteLine("равны");
    }
  }
}

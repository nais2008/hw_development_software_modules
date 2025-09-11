namespace Task1;

public class Program
{
  static void Main()
  {
    int number = int.Parse(Console.ReadLine());

    if (number > 0)
    {
      Console.WriteLine($"{number} > 0");
    }
    else if (number < 0)
    {
      Console.WriteLine($"{number} < 0");
    }
    else
    {
      Console.WriteLine($"{number} = 0");
    }
  }
}

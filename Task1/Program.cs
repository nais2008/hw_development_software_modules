namespace Task1;

public class Program
{
  static void Main()
  {
    int number = int.Parse(Console.ReadLine());

    if (number % 2 == 0)
    {
      Console.WriteLine(true);
    }
    else
    {
      Console.WriteLine(false);
    }
  }
}

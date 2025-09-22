namespace Task1;

public class Program
{
  static void Main()
  {
    int number1 = int.Parse(Console.ReadLine());
    int number2 = int.Parse(Console.ReadLine());

    Console.WriteLine(int.Min(number1, number2));
  }
}

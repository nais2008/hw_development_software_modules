int num1 = int.Parse(Console.ReadLine());
int num2 = int.Parse(Console.ReadLine());

int max_d = Math.Max(num1, num2);
int min_d = Math.Min(num1, num2);

for (int i = min_d; i <= max_d; i += 2)
{
  Console.Write(i + " ");
}

Console.WriteLine();

for (int i = min_d + 1; i <= max_d; i += 2)
{
  Console.Write(i + " ");
}

Console.WriteLine();

for (int i = min_d; i <= max_d; i++)
{
  if (i % 7 == 0)
  {
    Console.Write(i + " ");
  }
}

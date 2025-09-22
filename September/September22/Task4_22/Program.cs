int sum = 0;

while (true)
{
  int i = int.Parse(Console.ReadLine());

  if (i == 0)
  {
    break;
  }

  sum += i;
}

Console.WriteLine(sum);

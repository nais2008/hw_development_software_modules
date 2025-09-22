int num1 = int.Parse(Console.ReadLine());
int num2 = int.Parse(Console.ReadLine());

int max_d = Math.Max(num1, num2);
int min_d = Math.Min(num1, num2);

int sum = 0;

for (int i = min_d; i <= max_d; i++)
{
  sum += i;
}

Console.WriteLine(sum);

int[] prof = new int[12];
int max = 0;
int min = 999999999;

for (int i = 0; i < 12; i++)
{
  int num = int.Parse(Console.ReadLine());
  prof[i] = num;

  if (num > max)
    max = num;
  if (num < min)
    min = num;
}

Console.WriteLine(max);
Console.WriteLine(min);

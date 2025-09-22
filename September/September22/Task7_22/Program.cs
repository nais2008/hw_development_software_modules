int[] p = [];

for (int i = 0; i <= 5; i++)
  _ = p.Append(i);

int per = 0;

foreach (int side in p)
  per += side;

Console.WriteLine(per);

int[] numbers = { 1, 2, 3, 4, 5 };

foreach (int n in numbers)
    Console.Write(n + " ");

for (int i = numbers.Length - 1; i >= 0; i--)
    Console.Write(numbers[i] + " ");

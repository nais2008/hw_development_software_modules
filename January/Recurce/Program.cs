using System;

namespace Recurce;
class Program
{
    static int Power(int a, int n)
    {
        if (n < 0) 
            return -1;

        if (n == 0) 
            return 1;

        return a * Power(a, n - 1);
    }

    static int SumOfDigits(int number)
    {
        if (number == 0) 
            return 0;

        return (number % 10) + SumOfDigits(number / 10);
    }

    static bool IsPalindrome(string str)
    {
        if (str.Length <= 1) 
            return true;

        if (str[0] != str[str.Length - 1]) 
            return false;

        return IsPalindrome(str.Substring(1, str.Length - 2));
    }

    static int GCD(int a, int b)
    {
        if (b == 0) 
            return a;

        return GCD(b, a % b);
    }

    static void PrintNumbers(int from, int to)
    {
        if (from > to) 
            return;

        if (from == to)
        {
            Console.Write(from);
            return;
        }

        Console.Write(from + " ");
        PrintNumbers(from + 1, to);
    }

    static int CountDigits(int number)
    {
        if (number == 0) 
            return 1;
        if (number < 10) 
            return 1;

        return 1 + CountDigits(number / 10);
    }

    static void Main()
    {
        /* task 1 */
        Console.WriteLine(Power(2, 3) + "\n");

        /* task 2 */
        Console.WriteLine(SumOfDigits(78) + "\n");
        
        /* task 3 */
        Console.WriteLine(IsPalindrome("qweewq") + "\n");
        
        /* task 4 */
        Console.WriteLine(GCD(48, 18) + "\n");
        
        /* task 5 */
        PrintNumbers(1, 18);
        Console.WriteLine("\n");

        /* task 6 */
        Console.WriteLine(CountDigits(48) + "\n");
    }
}
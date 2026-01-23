using System;
using System.Diagnostics;

namespace Algoritm
{
    internal class Program
    {
        private static Random rnd = new Random();

        static int LinearSearch(int[] array, int target, out int comparisons)
        {
            comparisons = 0;

            for (int i = 0; i < array.Length; i++)
            {
                comparisons++;
                if (array[i] == target)
                    return i;
            }

            return -1;
        }

        static int BinarySearchIterative(int[] array, int target, out int comparisons)
        {
            comparisons = 0;
            int left = 0;
            int right = array.Length - 1;

            while (left <= right)
            {
                comparisons++;
                int mid = left + (right - left) / 2;

                if (array[mid] == target)
                    return mid;
                else if (array[mid] < target)
                    left = mid + 1;
                else
                    right = mid - 1;
            }

            return -1;
        }

        static int BinarySearchRecursive(int[] array, int target, out int comparisons)
        {
            comparisons = 0;
            return BinarySearchRecursiveHelper(array, target, 0, array.Length - 1, ref comparisons);
        }

        static int BinarySearchRecursiveHelper(int[] array, int target, int left, int right, ref int comparisons)
        {
            if (left > right) return -1;

            comparisons++;
            int mid = left + (right - left) / 2;

            if (array[mid] == target) 
                return mid;
            else if (array[mid] < target)
                return BinarySearchRecursiveHelper(array, target, mid + 1, right, ref comparisons);
            
            return BinarySearchRecursiveHelper(array, target, left, mid - 1, ref comparisons);
        }

        static int[] GenerateRandomArray(int size)
        {
            int[] array = new int[size];
            for (int i = 0; i < size; i++)
                array[i] = rnd.Next(0, size * 10);

            return array;
        }

        static void TestAlgorithms()
        {
            int[] testArray1 = { 5, 3, 8, 1, 9, 2 };
            int target1 = 8;
            int result1 = LinearSearch(testArray1, target1, out int comp1);
            Console.WriteLine($"{target1} \t {result1} \t {comp1}");

            int[] testArray2 = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
            int target2 = 7;
            int result2 = BinarySearchIterative(testArray2, target2, out int comp2);
            Console.WriteLine($"{target2} \t {result2} \t {comp2}");
        }

        static void PerformanceComparison()
        {
            int[] sizes = { 10, 100, 1000 };

            foreach (int size in sizes)
            {
                int[] array = GenerateRandomArray(size);
                int[] sortedArray = new int[size];
                Array.Copy(array, sortedArray, size);
                Array.Sort(sortedArray);

                int target = array[rnd.Next(0, size)];

                var sw = Stopwatch.StartNew();
                LinearSearch(array, target, out int linearComp);
                sw.Stop();
                Console.WriteLine($"| {size,6} | Линейный       | {linearComp,9} | {sw.ElapsedTicks,12} |");

                sw.Restart();
                BinarySearchIterative(sortedArray, target, out int binaryComp);
                sw.Stop();
                Console.WriteLine($"| {size,6} | Бинарный       | {binaryComp,9} | {sw.ElapsedTicks,12} |");
            }
        }

        static void DemonstrateBinarySearchProblem()
        {
            int[] unsortedArray = { 5, 3, 8, 1, 9, 2, 7, 4, 6, 10 };
            int target = 7;

            int binaryResult = BinarySearchIterative(unsortedArray, target, out int binaryComp);
            int linearResult = LinearSearch(unsortedArray, target, out int linearComp);

            Console.WriteLine(target);
            Console.WriteLine($"{binaryResult} \t {binaryComp}");
            Console.WriteLine($"{linearResult} \t {linearComp}");

            if (binaryResult != linearResult)
            {
                Console.WriteLine("Ошибка: бинарный поиск дал неверный результат!");
            }
        }

        static void Main(string[] args)
        {
            TestAlgorithms();
            PerformanceComparison();
            DemonstrateBinarySearchProblem();
        }
    }
}

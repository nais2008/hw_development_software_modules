using System.Diagnostics;
using System.Security.Cryptography.X509Certificates;

namespace SortMethods
{
    internal class Program
    {
        static void Main(string[] args)
        {
            PrintTests(1000);

            Console.WriteLine();

            PrintTests(10000);

            Console.WriteLine();

            PrintTests(100000);

            Console.WriteLine();
        }

        public static void PrintTests(int size)
        {
            Console.WriteLine($"Algoritm name \t | \t iterations \t | \t time(ms) \t | \t size: {size}");
            Console.WriteLine(new string('-', 85));
            TestFunc("Bubble sort", size, BubbleSort);
            TestFunc("Selection sort", size, SelectionStort);
            TestFunc("Insertion sort", size, InsertionSort);
            TestFunc("Quick sort", size, QuickSort);
            TestFunc("Merge sort", size, MergeSort);
            TestFunc("Shell sort", size, ShellSort);
        }

        static public void TestFunc(string sortName, int sizeArr, Func<int[], long> sortFunc)
        {
            Random random = new();
            int[] arr = new int[sizeArr];

            for (int i = 0; i < sizeArr; i++)
            {
                arr[i] = random.Next();
            }

            Stopwatch sw = new();
            sw.Start();
            long iter = sortFunc(arr);
            sw.Stop();

            Console.WriteLine($"{sortName} \t | \t {(iter / 100 < 1000 ? $"{iter}\t" : $"{iter}")} \t | \t {sw.Elapsed.TotalMilliseconds}");
        }

        public static long BubbleSort(int[] arr)
        {
            bool swapped;
            long iter = 0;

            for (int i = 0; i < arr.Length - 1; i++)
            {
                swapped = false;

                for (int j = 0; j < arr.Length - i - 1; j++)
                {
                    if (arr[j] > arr[j + 1])
                    {
                        swapped = true;
                        (arr[j], arr[j + 1]) = (arr[j + 1], arr[j]);
                        iter++;
                    }
                }

                if (!swapped)
                    break;
            }

            return iter;
        }

        public static long SelectionStort(int[] arr)
        {
            long iter = 0;

            for (int i = 0; i < arr.Length - 1; i++)
            {
                int minId = i;

                for (int j = i + 1; j < arr.Length; j++)
                {
                    if (arr[j] < arr[minId])
                        minId = j;
                }

                if (minId != i)
                {
                    iter++;
                    (arr[i], arr[minId]) = (arr[minId], arr[i]);
                }
            }

            return iter;
        }

        public static long InsertionSort(int[] arr)
        {
            long iter = 0;
            int 
                n = arr.Length,
                temp,
                j;

            for (int i = 1; i < n; i++)
            {
                temp = arr[i];
                j = i - 1;

                while (j >= 0 && arr[j] > temp)
                {
                    arr[j + 1] = arr[j];
                    j--;
                    iter++;
                }

                arr[j + 1] = temp;
            }

            return iter;
        }
        public static long QuickSort(int[] arr)
        {
            long iter = 0;

            QuickSortRecursive(arr, 0, arr.Length - 1, ref iter);

            return iter;
        }

        public static void QuickSortRecursive(int[] arr, int left, int right, ref long iter)
        {
            if (left < right)
            {
                int pivotIndex = Partition(arr, left, right, ref iter);
                QuickSortRecursive(arr, left, pivotIndex - 1, ref iter);
                QuickSortRecursive(arr, pivotIndex + 1, right, ref iter);
            }
        }
        public static int Partition(int[] arr, int left, int right, ref long iter)
        {
            int pivot = arr[right];
            int i = left - 1;

            for (int j = left; j < right; j++)
            {
                if (arr[j] <= pivot)
                {
                    i++;
                    if (i != j)
                    {
                        (arr[i], arr[j]) = (arr[j], arr[i]);
                        iter++;
                    }
                }
            }

            (arr[i + 1], arr[right]) = (arr[right], arr[i + 1]);

            return i + 1;
        }
        public static long MergeSort(int[] arr)
        {
            long iter = 0;

            MergeSortRecursive(arr, 0, arr.Length - 1, ref iter);

            return iter;
        }
        private static void MergeSortRecursive(int[] arr, int left, int right, ref long iter)
        {
            if (left < right)
            {
                int mid = left + (right - left) / 2;

                MergeSortRecursive(arr, left, mid, ref iter);
                MergeSortRecursive(arr, mid + 1, right, ref iter);

                Merge(arr, left, mid, right, ref iter);
            }
        }

        private static void Merge(int[] arr, int left, int mid, int right, ref long iter)
        {
            int n1 = mid - left + 1;
            int n2 = right - mid;

            int[] leftArr = new int[n1];
            int[] rightArr = new int[n2];

            for (int i = 0; i < n1; i++)
            {
                leftArr[i] = arr[left + i];
                iter++;
            }
            for (int j = 0; j < n2; j++)
            {
                rightArr[j] = arr[mid + 1 + j];
                iter++;
            }

            int k = left;
            int p = 0, q = 0;

            while (p < n1 && q < n2)
            {
                if (leftArr[p] <= rightArr[q])
                {
                    arr[k] = leftArr[p];
                    p++;
                }
                else
                {
                    arr[k] = rightArr[q];
                    q++;
                }
                k++;
                iter++;
            }

            while (p < n1)
            {
                arr[k] = leftArr[p];
                p++;
                k++;
                iter++;
            }

            while (q < n2)
            {
                arr[k] = rightArr[q];
                q++;
                k++;
                iter++;
            }
        }
        public static long ShellSort(int[] arr)
        {
            long iter = 0;
            int n = arr.Length;

            for (int gap = n / 2; gap > 0; gap /= 2)
            {
                for (int i = gap; i < n; i++)
                {
                    int temp = arr[i];
                    int j;

                    for (j = i; j >= gap; j -= gap)
                    {
                        iter++;
                        if (arr[j - gap] > temp)
                        {
                            arr[j] = arr[j - gap];
                        }
                        else
                        {
                            break;
                        }
                    }

                    arr[j] = temp;
                }
            }

            return iter;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace List
{
    class Task
    {
        public string Description;
        public bool IsCompleted;

        public Task(string description)
        {
            Description = description;
            IsCompleted = false;
        }

        public void setCompleted()
        {
            IsCompleted = true;
        }
    }

    internal class Program
    {
        public static void task1()
        {
            List<Task> tasks = new List<Task>();

            while(true)
            {
                int deistvie = Convert.ToInt32(Console.ReadLine());

                switch (deistvie)
                {
                    case 1:
                        string description = Console.ReadLine();

                        if (description.Length == 0)
                            break;

                        tasks.Add(new Task(description));

                        break;
                    case 2:
                        for (int i = 0; i < tasks.Count(); i++)
                            Console.WriteLine($"{i}. {(tasks[i].IsCompleted ? "[X]" : "[ ]")} {tasks[i].Description}");

                        break;
                    case 3:
                        string indexStr = Console.ReadLine();
                        int index = Convert.ToInt32(indexStr);

                        if (index >= 0 && index < tasks.Count())
                            tasks[index].setCompleted();
                        break;
                    case 4:
                        string indexStrDel = Console.ReadLine();
                        int indexDel = Convert.ToInt32(indexStrDel);

                        if (indexDel >= 0 && indexDel < tasks.Count())
                            tasks.RemoveAt(indexDel);

                        break;
                    case 5:
                        for (int i = 0; i < tasks.Count(); i++)
                            if (!tasks[i].IsCompleted)
                                Console.WriteLine($"{i}. {(tasks[i].IsCompleted ? "[X]" : "[ ]")} {tasks[i].Description}");

                        break;
                    default:
                        return;
                }
            }
        }

        static void task2()
        {
            Random rnd = new Random();

            List<int> A = new List<int>();
            List<int> B = new List<int>();

            for (int i = 0; i < 10; i++)
            {
                A.Add(rnd.Next(1, 21));
                B.Add(rnd.Next(1, 21));
            }

            foreach (int x in A) Console.Write(x + " ");
            Console.WriteLine();

            foreach (int x in B) Console.Write(x + " ");
            Console.WriteLine();

            List<int> intersection = new List<int>();
            foreach (int x in A)
                if (B.Contains(x) && !intersection.Contains(x))
                    intersection.Add(x);

            List<int> uniqueA = new List<int>();
            foreach (int x in A)
                if (!B.Contains(x) && !uniqueA.Contains(x))
                    uniqueA.Add(x);

            List<int> uniqueB = new List<int>();
            foreach (int x in B)
                if (!A.Contains(x) && !uniqueB.Contains(x))
                    uniqueB.Add(x);

            List<int> union = new List<int>();
            foreach (int x in A)
                if (!union.Contains(x)) union.Add(x);
            foreach (int x in B)
                if (!union.Contains(x)) union.Add(x);

            List<int> symDiff = new List<int>();
            foreach (int x in union)
                if (!(A.Contains(x) && B.Contains(x)))
                    symDiff.Add(x);

            foreach (int x in intersection) Console.Write(x + " ");
            Console.WriteLine();

            foreach (int x in uniqueA) Console.Write(x + " ");
            Console.WriteLine();

            foreach (int x in uniqueB) Console.Write(x + " ");
            Console.WriteLine();

            foreach (int x in union) Console.Write(x + " ");
            Console.WriteLine();

            foreach (int x in symDiff) Console.Write(x + " ");
            Console.WriteLine();
        }

        static void task3()
        {
            Random rnd = new Random();
            List<int> list = new List<int>();

            for (int i = 0; i < 10; i++)
                list.Add(rnd.Next(1, 101));

            foreach (int x in list) Console.Write(x + " ");
            Console.WriteLine();

            string dir = Console.ReadLine();
            int k = int.Parse(Console.ReadLine());

            int n = list.Count;
            k = k % n;

            List<int> result = new List<int>();

            if (dir == "L")
            {
                for (int i = 0; i < n; i++)
                {
                    int index = (i + k) % n;
                    result.Add(list[index]);
                }
            }
            else
            {
                for (int i = 0; i < n; i++)
                {
                    int index = (i - k + n) % n;
                    result.Add(list[index]);
                }
            }

            foreach (int x in result) Console.Write(x + " ");
            Console.WriteLine();
        }

        static void task4()
        {
            Random rnd = new Random();
            List<int> list = new List<int>();

            for (int i = 0; i < 30; i++)
                list.Add(rnd.Next(1, 11));

            foreach (int x in list) Console.Write(x + " ");
            Console.WriteLine();

            List<int> unique = new List<int>();
            foreach (int x in list)
                if (!unique.Contains(x)) unique.Add(x);

            foreach (int x in unique) Console.Write(x + " ");
            Console.WriteLine();

            int max = 0;
            List<int> freq = new List<int>();

            for (int i = 0; i < unique.Count; i++)
            {
                int count = 0;
                foreach (int x in list)
                    if (x == unique[i]) count++;

                freq.Add(count);
                if (count > max) max = count;
            }

            for (int i = 0; i < unique.Count; i++)
                Console.WriteLine(unique[i] + ": " + freq[i]);

            Console.WriteLine();

            for (int i = 0; i < unique.Count; i++)
                if (freq[i] == max)
                    Console.Write(unique[i] + " ");
            Console.WriteLine();

            Console.WriteLine();

            for (int i = 0; i < unique.Count; i++)
            {
                Console.Write(unique[i] + ": ");
                for (int j = 0; j < freq[i]; j++)
                    Console.Write("*");
                Console.WriteLine();
            }
        }

        static void task5()
        {
            List<string> queue = new List<string>();
            int served = 0;

            while (true)
            {
                int choice = int.Parse(Console.ReadLine());

                switch (choice)
                {
                    case 1:
                        string name1 = Console.ReadLine();
                        queue.Add(name1);
                        break;

                    case 2:
                        if (queue.Count > 0)
                        {
                            Console.WriteLine(queue[0]);
                            queue.RemoveAt(0);
                            served++;
                        }
                        else
                        {
                            Console.WriteLine("0");
                        }
                        break;

                    case 3:
                        if (queue.Count == 0)
                        {
                            Console.WriteLine("0");
                        }
                        else
                        {
                            for (int i = 0; i < queue.Count; i++)
                                Console.WriteLine((i + 1) + ". " + queue[i]);
                        }
                        break;

                    case 4:
                        string name2 = Console.ReadLine();
                        queue.Insert(0, name2);
                        break;

                    case 5:
                        Console.WriteLine(served);
                        if (queue.Count > 0)
                            Console.WriteLine(queue[0]);
                        else
                            Console.WriteLine("0");
                        break;

                    case 6:
                        return;
                }
            }
        }

        static void Main(string[] args)
        {
            //task1();
            //task2();
            //task3();
            //task4();
            //task5();
        }
    }
}

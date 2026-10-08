using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

internal class Program
{
    class Edge
    {
        public int From, To, Weight;
        public Edge(int f, int t, int w)
        {
            From = f;
            To = t;
            Weight = w;
        }
    }
    static void Main()
    {
        string[] name = { "A", "B", "C", "D", "E", "F", "G", "H", "I", "J" };
        int n = name.Length;

        var edges = new List<Edge>
        {
            new Edge(0, 1, 2),
            new Edge(0, 2, 4),
            new Edge(1, 2, 1),
            new Edge(1, 3, 3),
            new Edge(2, 4, 2),
            new Edge(3, 5, 5),
            new Edge(4, 5, 1),
            new Edge(4, 6, -4),
            new Edge(5, 6, 2),
            new Edge(6, 7, 3),
            new Edge(7, 8, 1),
            new Edge(7, 9, 2),
            new Edge(8, 9, -6),
            new Edge(9, 8, 2),
        };

        Console.WriteLine("Дейкстра из А");
        Print(Dijkstra(n, edges, 0), name);
        Console.WriteLine("Беллмана-Форда из А");
        Print(BellmaFord(n, edges, 0), name);
    }
    static int[] Dijkstra(int n, List<Edge> edges, int start)
    {
        //Список смежности для каждой вершины - ее исходящие ребра
        var graph = new List<Edge>[n];
        for (int i = 0; i < n; i++) graph[i] = new List<Edge>();
        foreach (var e in edges) graph[e.From].Add(e);

        int[] dist = new int[n];
        for (int i = 0; i < n; i++) dist[i] = int.MaxValue;
        dist[start] = 0;

        bool[] visited = new bool[n]; //вершины для которых расстояние уже окончательное

        //На каждом шаге выбираем непосещенную вершину с минимальным расстоянием
        for (int step = 0; step < n; step++)
        {
            int u = -1;
            for (int i = 0; i < n; i++)
            {
                if (!visited[i] && dist[i] != int.MaxValue && (u == -1 || dist[i] < dist[u]))
                {
                    u = i;
                }
            }
            if (u == -1) break; //все достижимые вершины посещены
            visited[u] = true;

            //Пытаемся улучшить пути до соседней вершины
            foreach (var e in graph[u])
            {
                if (dist[u] + e.Weight < dist[e.To])
                {
                    dist[e.To] = dist[u] + e.Weight;
                }
            }
        }
        return dist;
    }
    static int[] BellmaFord(int n, List<Edge> edges, int start)
    {
        int[] dist = new int[n];
        for (int i = 0; n > i; i++) dist[i] = int.MaxValue;
        dist[start] = 0;

        //Повторяем n-1 раз: пробуем улучшить все ребра
        for (int i = 0; i < n - 1; i++)
        {
            foreach (var e in edges)
            {
                if (dist[e.From] != int.MaxValue && dist[e.From] + e.Weight < dist[e.To])
                {
                    dist[e.To] = dist[e.From] + e.Weight;
                }
            }
        }
        //Еще одна проверка: если что-то улучшается - есть отрицательный цикл
        foreach (var e in edges)
        {
            if (dist[e.From] != int.MaxValue && dist[e.From] + e.Weight < dist[e.To])
            {
                Console.WriteLine("Обнаружен отрицательный цикл");
                break;
            }
        }
        return dist;
    }
    static void Print(int[] dist, string[] name)
    {
        for (int i = 0; i < dist.Length; i++)
        {
            string d = dist[i] == int.MaxValue ? "Бесконечность" : dist[i].ToString();
            Console.WriteLine($"{name[i]} : {d}");
        }
    }
}

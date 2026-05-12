using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace glava2
{
    // task 1

    class Book
    {
        public string Title;
        public string Author;
        public int Year;

        public virtual void DisplayInfo()
        {
            Console.WriteLine(
                $"{Title} {Author} {Year}"
            );
        }
    }

    class EBook : Book
    {
        public double FileSizeMB;

        public override void DisplayInfo()
        {
            Console.WriteLine(
                $"{Title} {Author} {Year} {FileSizeMB}MB"
            );
        }
    }

    class AudioBook : Book
    {
        public int DurationMinutes;
        public string Narrator;

        public override void DisplayInfo()
        {
            Console.WriteLine(
                $"{Title} {Author} {Year} " +
                $"{DurationMinutes}min {Narrator}"
            );
        }
    }

    // task 2

    interface IShape
    {
        double GetArea();

        double GetPerimeter();
    }

    class Rectangle : IShape
    {
        public double Width;
        public double Height;

        public double GetArea()
        {
            return Width * Height;
        }

        public double GetPerimeter()
        {
            return 2 * (Width + Height);
        }
    }

    class Circle : IShape
    {
        public double Radius;

        public double GetArea()
        {
            return Math.PI * Radius * Radius;
        }

        public double GetPerimeter()
        {
            return 2 * Math.PI * Radius;
        }
    }

    class Triangle : IShape
    {
        public double SideA;
        public double SideB;
        public double SideC;

        public double GetArea()
        {
            double p = GetPerimeter() / 2;

            return Math.Sqrt(
                p *
                (p - SideA) *
                (p - SideB) *
                (p - SideC)
            );
        }

        public double GetPerimeter()
        {
            return SideA + SideB + SideC;
        }
    }

    // task 3

    struct Point2D
    {
        public int X;
        public int Y;

        public Point2D(int x, int y)
        {
            X = x;
            Y = y;
        }

        public void Print()
        {
            Console.WriteLine($"{X} {Y}");
        }
    }

    delegate Point2D TransformPointDelegate(
        Point2D p
    );

    // task 4

    class Student
    {
        public string FullName;
        public int GroupNumber;
        public double AverageGrade;
    }

    static class StudentExtensions
    {
        public static void RemoveBadStudents(
            this List<Student> students,
            double minGrade
        )
        {
            students.RemoveAll(
                s => s.AverageGrade < minGrade
            );
        }
    }

    // task 5

    interface IDeliverable
    {
        void Deliver();
    }

    class CourierDelivery : IDeliverable
    {
        public void Deliver()
        {
            Console.WriteLine(
                "Курьер несет заказ пешком"
            );
        }
    }

    class DroneDelivery : IDeliverable
    {
        public void Deliver()
        {
            Console.WriteLine(
                "Дрон летит до клиента"
            );
        }
    }

    class Order
    {
        public int OrderId;
        public string CustomerName;

        public IDeliverable DeliveryMethod;

        public delegate void OrderHandler(
            string message
        );

        public event OrderHandler OrderCompleted;

        public void ProcessOrder()
        {
            DeliveryMethod.Deliver();

            if (OrderCompleted != null)
            {
                OrderCompleted(
                    $"Заказ {OrderId} готов!"
                );
            }
        }
    }

    // task 6

    class Entity
    {
        public int Id;
    }

    class Product : Entity
    {
        public string Name;
        public double Price;
    }

    class Customer : Entity
    {
        public string Name;
        public string Email;
    }

    interface IRepository<T>
    {
        void Add(T item);

        T GetById(int id);

        IEnumerable<T> GetAll();

        void Remove(T item);
    }

    class GenericRepository<T> :
        IRepository<T>
        where T : Entity
    {
        private List<T> items =
            new List<T>();

        public void Add(T item)
        {
            items.Add(item);
        }

        public T GetById(int id)
        {
            foreach (var item in items)
            {
                if (item.Id == id)
                {
                    return item;
                }
            }

            return null;
        }

        public IEnumerable<T> GetAll()
        {
            return items;
        }

        public void Remove(T item)
        {
            items.Remove(item);
        }
    }

    class Program
    {
        // task 1

        static void ShowBooks(
            List<Book> books
        )
        {
            foreach (var book in books)
            {
                book.DisplayInfo();
            }
        }

        static void ShowBooksAfterYear(
            List<Book> books,
            int year
        )
        {
            foreach (var book in books)
            {
                if (book.Year > year)
                {
                    book.DisplayInfo();
                }
            }
        }

        static void Task1()
        {
            List<Book> books =
                new List<Book>
                {
                    new Book
                    {
                        Title = "Book1",
                        Author = "Author1",
                        Year = 2001
                    },

                    new Book
                    {
                        Title = "Book2",
                        Author = "Author2",
                        Year = 2005
                    },

                    new EBook
                    {
                        Title = "EBook1",
                        Author = "Author3",
                        Year = 2015,
                        FileSizeMB = 5.4
                    },

                    new EBook
                    {
                        Title = "EBook2",
                        Author = "Author4",
                        Year = 2020,
                        FileSizeMB = 8.1
                    },

                    new AudioBook
                    {
                        Title = "Audio1",
                        Author = "Author5",
                        Year = 2018,
                        DurationMinutes = 120,
                        Narrator = "Ivan"
                    },

                    new AudioBook
                    {
                        Title = "Audio2",
                        Author = "Author6",
                        Year = 2022,
                        DurationMinutes = 90,
                        Narrator = "Alex"
                    }
                };

            ShowBooks(books);

            int year =
                int.Parse(Console.ReadLine());

            ShowBooksAfterYear(
                books,
                year
            );

            Console.WriteLine();
        }

        // task 2

        static void Task2()
        {
            Random random = new Random();

            List<IShape> shapes =
                new List<IShape>
                {
                    new Rectangle
                    {
                        Width = random.Next(1, 10),
                        Height = random.Next(1, 10)
                    },

                    new Rectangle
                    {
                        Width = random.Next(1, 10),
                        Height = random.Next(1, 10)
                    },

                    new Circle
                    {
                        Radius = random.Next(1, 10)
                    },

                    new Circle
                    {
                        Radius = random.Next(1, 10)
                    },

                    new Triangle
                    {
                        SideA = 3,
                        SideB = 4,
                        SideC = 5
                    }
                };

            foreach (var shape in shapes)
            {
                Console.WriteLine(
                    $"{shape.GetType().Name} " +
                    $"{shape.GetArea():F2} " +
                    $"{shape.GetPerimeter():F2}"
                );
            }

            IShape maxShape = shapes[0];

            foreach (var shape in shapes)
            {
                if (
                    shape.GetArea() >
                    maxShape.GetArea()
                )
                {
                    maxShape = shape;
                }
            }

            Console.WriteLine(
                maxShape.GetType().Name
            );

            Console.WriteLine();
        }

        // task 3

        static TransformPointDelegate Translate(
            int dx,
            int dy
        )
        {
            return p =>
            {
                p.X += dx;
                p.Y += dy;

                return p;
            };
        }

        static TransformPointDelegate Scale(
            int k
        )
        {
            return p =>
            {
                p.X *= k;
                p.Y *= k;

                return p;
            };
        }

        static void Task3()
        {
            List<Point2D> points =
                new List<Point2D>
                {
                    new Point2D(1, 1),
                    new Point2D(2, 2),
                    new Point2D(3, 3),
                    new Point2D(4, 4),
                    new Point2D(5, 5)
                };

            TransformPointDelegate move =
                Translate(2, 3);

            TransformPointDelegate scale =
                Scale(2);

            foreach (var point in points)
            {
                Point2D temp = move(point);

                temp = scale(temp);

                temp.Print();
            }

            Console.WriteLine();

            foreach (var point in points)
            {
                point.Print();
            }

            Console.WriteLine();
        }

        // task 4

        static void Task4()
        {
            Random random = new Random();

            List<Student> students =
                new List<Student>();

            for (int i = 1; i <= 10; i++)
            {
                students.Add(
                    new Student
                    {
                        FullName =
                            $"Student{i}",

                        GroupNumber =
                            random.Next(101, 103),

                        AverageGrade =
                            Math.Round(
                                random.NextDouble() *
                                3 + 2,
                                2
                            )
                    }
                );
            }

            List<Student> goodStudents =
                students.FindAll(
                    s => s.AverageGrade > 4
                );

            foreach (
                var student in goodStudents
            )
            {
                Console.WriteLine(
                    $"{student.FullName} " +
                    $"{student.AverageGrade}"
                );
            }

            Console.WriteLine();

            students.Sort(
                (a, b) =>
                    b.AverageGrade.CompareTo(
                        a.AverageGrade
                    )
            );

            foreach (var student in students)
            {
                Console.WriteLine(
                    $"{student.FullName} " +
                    $"{student.AverageGrade}"
                );
            }

            Console.WriteLine();

            students.RemoveBadStudents(3);

            foreach (var student in students)
            {
                Console.WriteLine(
                    $"{student.FullName} " +
                    $"{student.AverageGrade}"
                );
            }

            Console.WriteLine();
        }

        // task 5

        static void Task5()
        {
            List<Order> orders =
                new List<Order>
                {
                    new Order
                    {
                        OrderId = 1,
                        CustomerName = "Ivan",
                        DeliveryMethod =
                            new CourierDelivery()
                    },

                    new Order
                    {
                        OrderId = 2,
                        CustomerName = "Alex",
                        DeliveryMethod =
                            new DroneDelivery()
                    }
                };

            foreach (var order in orders)
            {
                order.OrderCompleted +=
                    message =>
                        Console.WriteLine(message);
            }

            foreach (var order in orders)
            {
                order.ProcessOrder();
            }

            Console.WriteLine();
        }

        // task 6

        static void Task6()
        {
            GenericRepository<Product>
                productRepo =
                    new GenericRepository<Product>();

            GenericRepository<Customer>
                customerRepo =
                    new GenericRepository<Customer>();

            productRepo.Add(
                new Product
                {
                    Id = 1,
                    Name = "Phone",
                    Price = 1000
                }
            );

            productRepo.Add(
                new Product
                {
                    Id = 2,
                    Name = "Laptop",
                    Price = 2000
                }
            );

            customerRepo.Add(
                new Customer
                {
                    Id = 1,
                    Name = "Ivan",
                    Email = "ivan@mail.com"
                }
            );

            customerRepo.Add(
                new Customer
                {
                    Id = 2,
                    Name = "Alex",
                    Email = "alex@mail.com"
                }
            );

            foreach (
                var product in
                productRepo.GetAll()
            )
            {
                Console.WriteLine(
                    $"{product.Id} " +
                    $"{product.Name} " +
                    $"{product.Price}"
                );
            }

            Console.WriteLine();

            foreach (
                var customer in
                customerRepo.GetAll()
            )
            {
                Console.WriteLine(
                    $"{customer.Id} " +
                    $"{customer.Name} " +
                    $"{customer.Email}"
                );
            }
        }

        static void Main()
        {
            // task 1
            Task1();

            // task 2
            Task2();

            // task 3
            Task3();

            // task 4
            Task4();

            // task 5
            Task5();

            // task 6
            Task6();
        }
    }
}
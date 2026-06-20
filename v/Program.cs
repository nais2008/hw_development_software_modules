using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace v
{
    class Employee
    {
        public string FullName;
        public string BirthDate;
        public string Phone;
        public string Email;
        public string Position;
        public string Duties;

        public void Input()
        {
            FullName = Console.ReadLine();
            BirthDate = Console.ReadLine();
            Phone = Console.ReadLine();
            Email = Console.ReadLine();
            Position = Console.ReadLine();
            Duties = Console.ReadLine();
        }

        public void Show()
        {
            Console.WriteLine($"{FullName} {BirthDate} {Phone} {Email} {Position} {Duties}");
        }

        public string GetFullName()
        {
            return FullName;
        }

        public string GetPosition()
        {
            return Position;
        }
    }

    abstract class GeometricFigure
    {
        public abstract double GetArea();
        public abstract double GetPerimeter();
    }

    class Triangle : GeometricFigure
    {
        private double a;
        private double b;
        private double c;

        public Triangle(double a, double b, double c)
        {
            this.a = a;
            this.b = b;
            this.c = c;
        }

        public override double GetArea()
        {
            double p = (a + b + c) / 2;
            return Math.Sqrt(p * (p - a) * (p - b) * (p - c));
        }

        public override double GetPerimeter()
        {
            return a + b + c;
        }
    }

    class Square : GeometricFigure
    {
        private double side;

        public Square(double side)
        {
            this.side = side;
        }

        public override double GetArea()
        {
            return side * side;
        }

        public override double GetPerimeter()
        {
            return side * 4;
        }
    }

    class Rhombus : GeometricFigure
    {
        private double diagonal1;
        private double diagonal2;
        private double side;

        public Rhombus(double diagonal1, double diagonal2, double side)
        {
            this.diagonal1 = diagonal1;
            this.diagonal2 = diagonal2;
            this.side = side;
        }

        public override double GetArea()
        {
            return diagonal1 * diagonal2 / 2;
        }

        public override double GetPerimeter()
        {
            return side * 4;
        }
    }

    class Rectangle : GeometricFigure
    {
        private double width;
        private double height;

        public Rectangle(double width, double height)
        {
            this.width = width;
            this.height = height;
        }

        public override double GetArea()
        {
            return width * height;
        }

        public override double GetPerimeter()
        {
            return 2 * (width + height);
        }
    }

    class Parallelogram : GeometricFigure
    {
        private double sideA;
        private double sideB;
        private double height;

        public Parallelogram(double sideA, double sideB, double height)
        {
            this.sideA = sideA;
            this.sideB = sideB;
            this.height = height;
        }

        public override double GetArea()
        {
            return sideA * height;
        }

        public override double GetPerimeter()
        {
            return 2 * (sideA + sideB);
        }
    }

    class Trapezoid : GeometricFigure
    {
        private double a;
        private double b;
        private double c;
        private double d;
        private double height;

        public Trapezoid(double a, double b, double c, double d, double height)
        {
            this.a = a;
            this.b = b;
            this.c = c;
            this.d = d;
            this.height = height;
        }

        public override double GetArea()
        {
            return (a + b) * height / 2;
        }

        public override double GetPerimeter()
        {
            return a + b + c + d;
        }
    }

    class Circle : GeometricFigure
    {
        private double radius;

        public Circle(double radius)
        {
            this.radius = radius;
        }

        public override double GetArea()
        {
            return Math.PI * radius * radius;
        }

        public override double GetPerimeter()
        {
            return 2 * Math.PI * radius;
        }
    }

    class Ellipse : GeometricFigure
    {
        private double a;
        private double b;

        public Ellipse(double a, double b)
        {
            this.a = a;
            this.b = b;
        }

        public override double GetArea()
        {
            return Math.PI * a * b;
        }

        public override double GetPerimeter()
        {
            return 2 * Math.PI * Math.Sqrt((a * a + b * b) / 2);
        }
    }

    class CompositeFigure
    {
        private List<GeometricFigure> figures = new List<GeometricFigure>();

        public void AddFigure(GeometricFigure figure)
        {
            figures.Add(figure);
        }

        public double GetArea()
        {
            double sum = 0;

            foreach (GeometricFigure figure in figures)
            {
                sum += figure.GetArea();
            }

            return sum;
        }
    }

    internal class Program
    {
        static void Main()
        {
            /* task 1 */
            Employee employee = new Employee();

            employee.FullName = "Иванов Иван Иванович";
            employee.BirthDate = "01.01.2000";
            employee.Phone = "+79999999999";
            employee.Email = "ivanov@mail.ru";
            employee.Position = "Программист";
            employee.Duties = "Разработка приложений";

            employee.Show();

            /* task 2 */

            Square square = new Square(5);
            Circle circle = new Circle(3);
            Rectangle rectangle = new Rectangle(4, 6);
            Triangle triangle = new Triangle(3, 4, 5);

            Console.WriteLine($"{square.GetArea()} {square.GetPerimeter()} ");
            Console.WriteLine($"{circle.GetArea()} {circle.GetPerimeter()} ");
            Console.WriteLine($"{rectangle.GetArea()} {rectangle.GetPerimeter()} ");
            Console.WriteLine($"{triangle.GetArea()} {triangle.GetPerimeter()} ");

            Console.WriteLine();

            Console.WriteLine($"{rectangle.GetArea()} {rectangle.GetPerimeter()}");
            Console.WriteLine($"{triangle.GetArea()} {triangle.GetPerimeter()}");

            CompositeFigure composite = new CompositeFigure();

            composite.AddFigure(square);
            composite.AddFigure(circle);
            composite.AddFigure(rectangle);
            composite.AddFigure(triangle);

            Console.WriteLine($"{composite.GetArea()}");
        }

    }
}

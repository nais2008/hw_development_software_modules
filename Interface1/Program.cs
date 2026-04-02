using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interface1
{
    /* task 1 */
    interface ISpeakable
    {
        void Speak();
    }

    class Person  : ISpeakable
    {
        public void Speak()
        {
            Console.WriteLine("Привет!");
        }
    }

    class Dog : ISpeakable
    {
        public void Speak()
        {
            Console.WriteLine("Гав-гав!");
        }
    }

    /* task2 */
    interface IHasArea
    {
        double CalculateArea();
    }

    class Reactangle : IHasArea
    {
        public double Width;
        public double Height;

        public Reactangle(int width, int height)
        {
            Width = width;
            Height = height;
        }
        public double CalculateArea()
        {
            return Width * Height;
        }
    }
    class Circle : IHasArea
    {
        public double Radius;
        public Circle(int radius)
        {
            Radius = radius;
        }
        public double CalculateArea()
        {
            return Math.PI * Radius * Radius;
        }
    }

    /* task 3 */
    interface ISwitchable
    {
        void TurnOn();
        void TurnOff();
    }

    class Lamp : ISwitchable
    {
        public void TurnOn()
        {
            Console.WriteLine("Лампа зажглась");
        }
        public void TurnOff()
        {
            Console.WriteLine("Лампа погасла");
        }
    }

    class Computer : ISwitchable
    {
        public void TurnOn()
        {
            Console.WriteLine("Компьютер запущен");
        }
        public void TurnOff()
        {
            Console.WriteLine("Компьютер выключен");
        }
    }

    /* task 4 */
    interface IComparableByPrice
    {
        int CompareByPrice(object other);
    }

    class Product : IComparableByPrice
    {
        public string Name;
        public double Price;
        public Product(string name, double price)
        {
            Name = name;
            Price = price;
        }
        public int CompareByPrice(object other)
        {
            if (other is Product otherProduct)
            {
                if (this.Price < otherProduct.Price) return -1;
                if (this.Price > otherProduct.Price) return 1;
                return 0;
            }
            throw new ArgumentException("Объект не является товаром");
        }
    }

    /* task 5 */
    interface IProcessor
    {
        void Process();
    }

    class TextProcessor : IProcessor
    {
        public void Process()
        {
            Console.WriteLine("Обрабатываю текст");
        }
    }

    class ImageProcessor : IProcessor
    {
        public void Process()
        {
            Console.WriteLine("Обрабатываю изображение");
        }
    }

    class DataProcessor : IProcessor
    {
        public void Process()
        {
            Console.WriteLine("Обрабатываю данные");
        }
    }

    internal class Program
    {
        static void Main(string[] args)
        {
            /* task 1 */
            Person p = new Person();
            p.Speak();
            Dog d = new Dog();
            d.Speak();

            Console.WriteLine();

            /* task 2 */
            Reactangle r = new Reactangle(5, 10);
            Console.WriteLine(r.CalculateArea());
            Circle c = new Circle(5);
            Console.WriteLine(c.CalculateArea());

            Console.WriteLine();

            /* task 3 */
            Lamp l = new Lamp();
            l.TurnOn();
            l.TurnOff();
            Computer comp = new Computer();
            comp.TurnOn();
            comp.TurnOff();

            Console.WriteLine();

            /* task 4 */
            Product[] products = new Product[]
            {
                new Product("Товар 1", 10.99),
                new Product("Товар 2", 5.49),
                new Product("Товар 3", 20.00),
                new Product("Товар 4", 15.75)
            };

            for (int i = 0; i < products.Length - 1; i++)
            {
                for (int j = 0; j < products.Length - i - 1; j++)
                {
                    if (products[j].CompareByPrice(products[j + 1]) > 0)
                    {
                        Product temp = products[j];
                        products[j] = products[j + 1];
                        products[j + 1] = temp;
                    }
                }
            }

            foreach (var product in products)
                Console.WriteLine($"{product.Name}: {product.Price}");

            Console.WriteLine();

            /*task 5*/
            IProcessor[] processors = new IProcessor[]{
                new TextProcessor(),
                new ImageProcessor(),
                new DataProcessor()
            };

            foreach (var processor in processors)
                processor.Process();
        }
    }
}

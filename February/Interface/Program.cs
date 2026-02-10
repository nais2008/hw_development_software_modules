using System;

namespace Interface
{
    /* task 1 */
    interface IVolumeControl
    {
        void IncreaseVolume();
        void DecreaseVolume();
        int GetCurrentVolume();
    }

    class Smartphone : IVolumeControl
    {
        private int _volume = 50;
        public void DecreaseVolume()
        {
            _volume -= 5;

            if (_volume < 0)
                _volume = 0;
        }

        public int GetCurrentVolume()
        {
            return _volume;
        }

        public void IncreaseVolume()
        {
            _volume += 5;

            if (_volume > 100) 
                _volume = 100;
        }
    }

    class Radio : IVolumeControl
    {
        private int _volume = 30;

        public void DecreaseVolume()
        {
            _volume -= 10;

            if (_volume < 0)
                _volume = 0;
        }

        public int GetCurrentVolume()
        {
            return _volume;
        }

        public void IncreaseVolume()
        {
            _volume += 10;

            if (_volume > 100)
                _volume = 100;
        }
    }

    /* task 2 */
    interface IDiscountable
    {
        decimal ApplyDiscount(decimal price);
    }

    class StudentDiscount : IDiscountable
    {
        private decimal _discount = (decimal)0.2;

        public decimal ApplyDiscount(decimal price)
        {
            return (decimal)(price * (1 - _discount));
        }
    }

    class VIPDiscount  : IDiscountable
    {
        private decimal _discount = (decimal)0.3;

        public decimal ApplyDiscount(decimal price)
        {
            return (decimal)(price * (1 - _discount));
        }
    }

    /* task 3 */
    interface IMessenger
    {
        void SendMessage(string text, string recipient);
    }

    class Telegram : IMessenger
    {
        public void SendMessage(string text, string recipient)
        {
            Console.WriteLine($"Telegram: {text} для [{recipient}]");
        }
    }
    class WhatsApp : IMessenger
    {
        public void SendMessage(string text, string recipient)
        {
            Console.WriteLine($"WhatsApp: {text} -> [{recipient}]");
        }
    }

    /* task 4 */
    interface IPhotoFilter
    {
        string ApplyFilter(string photoName);
    }

    class BlackWhiteFilter : IPhotoFilter
    {
        public string ApplyFilter(string photoName)
            => photoName + "_bw";
    }

    class SepiaFilter : IPhotoFilter
    {
        public string ApplyFilter(string photoName)
            => photoName + "_sepia";
    }

    /* task 5 */
    interface ICurrencyConverter
    {
        decimal Convert(decimal amount);
    }

    class DollarToRubles : ICurrencyConverter
    {
        public decimal Convert(decimal amount)
            => amount * 90;
    }

    class EuroToRubles : ICurrencyConverter
    {
        public decimal Convert(decimal amount)
            => amount * 100;
    }

    internal class Program
    {
        static void Main(string[] args)
        {
            /* task 1 */
            Smartphone phone = new Smartphone();
            Radio radio = new Radio();

            Console.WriteLine(phone.GetCurrentVolume());
            phone.IncreaseVolume();
            Console.WriteLine(phone.GetCurrentVolume());


            Console.WriteLine(radio.GetCurrentVolume());
            radio.IncreaseVolume();
            Console.WriteLine(radio.GetCurrentVolume());

            Console.WriteLine();

            /* task 2 */
            StudentDiscount student = new StudentDiscount();
            VIPDiscount vip = new VIPDiscount();

            Console.WriteLine(student.ApplyDiscount(1000));
            Console.WriteLine(vip.ApplyDiscount(1000));

            Console.WriteLine();

            /* task 3 */
            Telegram telegram = new Telegram();
            WhatsApp whatsapp = new WhatsApp();

            telegram.SendMessage("Hello", "User1");
            whatsapp.SendMessage("Hi", "User2");

            Console.WriteLine();

            /* task 4 */
            BlackWhiteFilter bw = new BlackWhiteFilter();
            SepiaFilter sepia = new SepiaFilter();

            Console.WriteLine(bw.ApplyFilter("photo1"));
            Console.WriteLine(sepia.ApplyFilter("photo2"));

            Console.WriteLine();

            /* task 5 */
            DollarToRubles dollar = new DollarToRubles();
            EuroToRubles euro = new EuroToRubles();

            Console.WriteLine(dollar.Convert(10));
            Console.WriteLine(euro.Convert(10));
        }
    }
}

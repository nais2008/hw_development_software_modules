using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PatternState
{
    interface ICoffeeMachineState
    {
        void InsertMoney(decimal amount);
        void SelectDrink(string drink);
        void Cancel();
        void Confirm();
        void TakeDrink();
        string GetStateName();
    }

    class CoffeeMachine
    {
        private ICoffeeMachineState currentState;

        public decimal Money { get; set; }
        public string SelectedDrink { get; set; }

        public CoffeeMachine()
        {
            currentState = new WaitingForPaymentState(this);
        }

        public void SetState(ICoffeeMachineState state)
        {
            currentState = state;
            Console.WriteLine($"Состояние: {currentState.GetStateName()}");
        }

        public void InsertMoney(decimal amount)
        {
            currentState.InsertMoney(amount);
        }

        public void SelectDrink(string drink)
        {
            currentState.SelectDrink(drink);
        }

        public void Cancel()
        {
            currentState.Cancel();
        }

        public void Confirm()
        {
            currentState.Confirm();
        }

        public void TakeDrink()
        {
            currentState.TakeDrink();
        }
    }

    class WaitingForPaymentState : ICoffeeMachineState
    {
        private CoffeeMachine machine;

        public WaitingForPaymentState(CoffeeMachine machine)
        {
            this.machine = machine;
        }

        public void InsertMoney(decimal amount)
        {
            machine.Money += amount;

            Console.WriteLine($"Внесено {amount} руб.");

            machine.SetState(new DrinkSelectionState(machine));
        }

        public void SelectDrink(string drink)
        {
            Console.WriteLine("Сначала внесите деньги");
        }

        public void Cancel()
        {
            Console.WriteLine("Отменять нечего");
        }

        public void Confirm()
        {
            Console.WriteLine("Сначала выберите напиток");
        }

        public void TakeDrink()
        {
            Console.WriteLine("Напиток не готов");
        }

        public string GetStateName()
        {
            return "Ожидание оплаты";
        }
    }

    class DrinkSelectionState : ICoffeeMachineState
    {
        private CoffeeMachine machine;

        public DrinkSelectionState(CoffeeMachine machine)
        {
            this.machine = machine;
        }

        public void InsertMoney(decimal amount)
        {
            Console.WriteLine("Деньги уже внесены");
        }

        public void SelectDrink(string drink)
        {
            machine.SelectedDrink = drink;

            Console.WriteLine($"Выбран напиток: {drink}");
        }

        public void Cancel()
        {
            Console.WriteLine("Заказ отменен");

            machine.Money = 0;
            machine.SelectedDrink = "";

            machine.SetState(new WaitingForPaymentState(machine));
        }

        public void Confirm()
        {
            if (string.IsNullOrEmpty(machine.SelectedDrink))
            {
                Console.WriteLine("Сначала выберите напиток");
                return;
            }

            machine.SetState(new BrewingState(machine));
        }

        public void TakeDrink()
        {
            Console.WriteLine("Напиток еще не готов");
        }

        public string GetStateName()
        {
            return "Выбор напитка";
        }
    }

    class BrewingState : ICoffeeMachineState
    {
        private CoffeeMachine machine;

        public BrewingState(CoffeeMachine machine)
        {
            this.machine = machine;

            Console.WriteLine($"Готовится {machine.SelectedDrink}...");

            machine.SetState(new DispensingState(machine));
        }

        public void InsertMoney(decimal amount)
        {
            Console.WriteLine("Идет приготовление");
        }

        public void SelectDrink(string drink)
        {
            Console.WriteLine("Идет приготовление");
        }

        public void Cancel()
        {
            Console.WriteLine("Отменить нельзя");
        }

        public void Confirm()
        {
            Console.WriteLine("Напиток уже готовится");
        }

        public void TakeDrink()
        {
            Console.WriteLine("Подождите приготовления");
        }

        public string GetStateName()
        {
            return "Приготовление";
        }
    }

    class DispensingState : ICoffeeMachineState
    {
        private CoffeeMachine machine;

        public DispensingState(CoffeeMachine machine)
        {
            this.machine = machine;
        }

        public void InsertMoney(decimal amount)
        {
            Console.WriteLine("Сначала заберите напиток");
        }

        public void SelectDrink(string drink)
        {
            Console.WriteLine("Сначала заберите напиток");
        }

        public void Cancel()
        {
            Console.WriteLine("Отменить нельзя");
        }

        public void Confirm()
        {
            Console.WriteLine("Напиток уже готов");
        }

        public void TakeDrink()
        {
            Console.WriteLine($"Вы получили: {machine.SelectedDrink}");

            machine.Money = 0;
            machine.SelectedDrink = "";

            machine.SetState(new WaitingForPaymentState(machine));
        }

        public string GetStateName()
        {
            return "Выдача напитка";
        }
    }


    internal class Program
    {
        static void Main(string[] args)
        {
            CoffeeMachine cm = new CoffeeMachine();
            cm.InsertMoney(150);
            cm.SelectDrink("Капучино");
            cm.Confirm();
            cm.TakeDrink();

        }
    }
}

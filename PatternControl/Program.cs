using System;
using System.Collections.Generic;

namespace PatternControl
{
    interface ICommand
    {
        void Execute();
        void Undo();
        string GetName();
    }

    class Light
    {
        public bool IsOn { get; private set; }

        public void On()
        {
            IsOn = true;
            Console.WriteLine("Свет включен");
        }

        public void Off()
        {
            IsOn = false;
            Console.WriteLine("Свет выключен");
        }
    }

    class AirConditioner
    {
        public bool IsOn { get; private set; }
        public int Temperature { get; private set; } = 22;

        public void On()
        {
            IsOn = true;
            Console.WriteLine($"Кондиционер включен, температура {Temperature}C");
        }

        public void Off()
        {
            IsOn = false;
            Console.WriteLine("Кондиционер выключен");
        }

        public void SetTemperature(int temp)
        {
            Temperature = temp;
            Console.WriteLine($"Температура установлена на {Temperature}C");
        }
    }

    class MusicSystem
    {
        public bool IsOn { get; private set; }
        public int Volume { get; private set; } = 50;

        public void On()
        {
            IsOn = true;
            Console.WriteLine($"Музыка включена, громкость {Volume}");
        }

        public void Off()
        {
            IsOn = false;
            Console.WriteLine("Музыка выключена");
        }

        public void SetVolume(int volume)
        {
            Volume = volume;
            Console.WriteLine($"Громкость: {Volume}");
        }
    }

    class LightOnCommand : ICommand
    {
        private Light light;

        public LightOnCommand(Light light)
        {
            this.light = light;
        }

        public void Execute() => light.On();

        public void Undo() => light.Off();

        public string GetName() => "Включить свет";
    }

    class LightOffCommand : ICommand
    {
        private Light light;

        public LightOffCommand(Light light)
        {
            this.light = light;
        }

        public void Execute() => light.Off();

        public void Undo() => light.On();

        public string GetName() => "Выключить свет";
    }

    class ACOnCommand : ICommand
    {
        private AirConditioner ac;

        public ACOnCommand(AirConditioner ac)
        {
            this.ac = ac;
        }

        public void Execute() => ac.On();

        public void Undo() => ac.Off();

        public string GetName() => "Включить кондиционер";
    }

    class ACOffCommand : ICommand
    {
        private AirConditioner ac;

        public ACOffCommand(AirConditioner ac)
        {
            this.ac = ac;
        }

        public void Execute() => ac.Off();

        public void Undo() => ac.On();

        public string GetName() => "Выключить кондиционер";
    }

    class ACSetTempCommand : ICommand
    {
        private AirConditioner ac;
        private int newTemp;
        private int oldTemp;

        public ACSetTempCommand(AirConditioner ac, int temp)
        {
            this.ac = ac;
            newTemp = temp;
        }

        public void Execute()
        {
            oldTemp = ac.Temperature;
            ac.SetTemperature(newTemp);
        }

        public void Undo()
        {
            ac.SetTemperature(oldTemp);
        }

        public string GetName()
        {
            return $"Установить температуру {newTemp}C";
        }
    }

    class MusicOnCommand : ICommand
    {
        private MusicSystem music;

        public MusicOnCommand(MusicSystem music)
        {
            this.music = music;
        }

        public void Execute() => music.On();

        public void Undo() => music.Off();

        public string GetName() => "Включить музыку";
    }

    class MusicOffCommand : ICommand
    {
        private MusicSystem music;

        public MusicOffCommand(MusicSystem music)
        {
            this.music = music;
        }

        public void Execute() => music.Off();

        public void Undo() => music.On();

        public string GetName() => "Выключить музыку";
    }

    class MusicSetVolumeCommand : ICommand
    {
        private MusicSystem music;
        private int newVolume;
        private int oldVolume;

        public MusicSetVolumeCommand(MusicSystem music, int volume)
        {
            this.music = music;
            newVolume = volume;
        }

        public void Execute()
        {
            oldVolume = music.Volume;
            music.SetVolume(newVolume);
        }

        public void Undo()
        {
            music.SetVolume(oldVolume);
        }

        public string GetName()
        {
            return $"Установить громкость {newVolume}";
        }
    }

    class MacroCommand : ICommand
    {
        private List<ICommand> commands;

        public MacroCommand(List<ICommand> commands)
        {
            this.commands = commands;
        }

        public void Execute()
        {
            foreach (var command in commands)
            {
                command.Execute();
            }
        }

        public void Undo()
        {
            for (int i = commands.Count - 1; i >= 0; i--)
            {
                commands[i].Undo();
            }
        }

        public string GetName()
        {
            return $"Макрос: {commands.Count} команд";
        }
    }

    class RemoteControl
    {
        private ICommand[] commands = new ICommand[8];
        private Stack<ICommand> history = new Stack<ICommand>();

        public void SetCommand(int slot, ICommand command)
        {
            commands[slot] = command;
        }

        public void PressButton(int slot)
        {
            if (commands[slot] == null)
            {
                Console.WriteLine("Команда не назначена");
                return;
            }

            Console.WriteLine($"Нажата кнопка {slot}: {commands[slot].GetName()}");

            commands[slot].Execute();
            history.Push(commands[slot]);
        }

        public void PressUndo()
        {
            if (history.Count == 0)
            {
                Console.WriteLine("История пуста");
                return;
            }

            ICommand command = history.Pop();

            Console.WriteLine($"Отмена: {command.GetName()}");

            command.Undo();
        }
    }


    internal class Program
    {
        static void Main(string[] args)
        {
            Light l = new Light();
            AirConditioner ac = new AirConditioner();
            MusicSystem m = new MusicSystem();

            RemoteControl rc = new RemoteControl();

            rc.SetCommand(0, new LightOnCommand(l));
            rc.SetCommand(1, new LightOffCommand(l));
            rc.SetCommand(2, new ACOnCommand(ac));
            rc.SetCommand(3, new ACOffCommand(ac));
            rc.SetCommand(4, new ACSetTempCommand(ac, 25));
            rc.SetCommand(5, new MusicOnCommand(m));
            rc.SetCommand(6, new MusicOffCommand(m));

            rc.PressButton(0);
            rc.PressButton(4);
            rc.PressButton(6);

            rc.PressUndo();
            rc.PressUndo();
            rc.PressUndo();

            MacroCommand mc = new MacroCommand(
                new List<ICommand>{
                    new LightOnCommand(l),
                    new ACSetTempCommand(ac, 24),
                    new MusicOnCommand(m),
                    new MusicSetVolumeCommand(m, 30)
                }
            );

            rc.SetCommand(7, mc);

            rc.PressButton(7);

            rc.PressUndo();
        }
    }
}

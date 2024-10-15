using System;
using System.Linq;

namespace HouseConstruction
{
    interface IPart
    {
        void Build();
        bool IsBuilt { get; }
    }
    interface IWorker
    {
        void Work(House house);
    }
    class House
    {
        public Basement Basement { get; private set; }
        public Walls Walls { get; private set; }
        public Door Door { get; private set; }
        public Window[] Windows { get; private set; }
        public Roof Roof { get; private set; }
        public House()
        {
            Basement = new Basement();
            Walls = new Walls();
            Door = new Door();
            Windows = new Window[4];
            for (int i = 0; i < 4; i++)
            {
                Windows[i] = new Window();
            }
            Roof = new Roof();
        }
        public bool IsBuilt()
        {
            return Basement.IsBuilt && Walls.IsBuilt && Door.IsBuilt && Windows.All(w => w.IsBuilt) && Roof.IsBuilt;
        }
        public void Print()
        {
            Console.WriteLine("Дом готов нащальника ");

        }
    }
    class Basement : IPart
    {
        public bool IsBuilt { get; private set; }
        public void Build()
        {
            if (!IsBuilt)
            {
                Console.WriteLine("Строится фундамент...");
                IsBuilt = true;
            }
            else
            {
                Console.WriteLine("Фундамент уже построен.");
            }
        }
    }
    class Walls : IPart
    {
        public bool IsBuilt { get; private set; }
        public void Build()
        {
            if (!IsBuilt)
            {
                Console.WriteLine("Строятся стены...");
                IsBuilt = true;
            }
            else
            {
                Console.WriteLine("Стены уже построены.");
            }
        }
    }
    class Door : IPart
    {
        public bool IsBuilt { get; private set; }

        public void Build()
        {
            if (!IsBuilt)
            {
                Console.WriteLine("Строится дверь...");
                IsBuilt = true;
            }
            else
            {
                Console.WriteLine("Дверь уже построена.");
            }
        }
    }
    class Window : IPart
    {
        public bool IsBuilt { get; private set; }

        public void Build()
        {
            if (!IsBuilt)
            {
                Console.WriteLine("Строится окно...");
                IsBuilt = true;
            }
            else
            {
                Console.WriteLine("Окно уже построено.");
            }
        }
    }
    class Roof : IPart
    {
        public bool IsBuilt { get; private set; }

        public void Build()
        {
            if (!IsBuilt)
            {
                Console.WriteLine("Строится крыша...");
                IsBuilt = true;
            }
            else
            {
                Console.WriteLine("Крыша уже построена.");
            }
        }
    }
    class Worker : IWorker
    {
        public string Name { get; private set; }
        public Worker(string name)
        {
            Name = name;
        }
        public void Work(House house)
        {
            if (!house.Basement.IsBuilt)
            {
                house.Basement.Build();
            }
            else if (!house.Walls.IsBuilt)
            {
                house.Walls.Build();
            }
            else if (!house.Door.IsBuilt)
            {
                house.Door.Build();
            }
            else if (!house.Windows.All(w => w.IsBuilt))
            {
                foreach (Window window in house.Windows)
                {
                    if (!window.IsBuilt)
                    {
                        window.Build();
                        break;
                    }
                }
            }
            else if (!house.Roof.IsBuilt)
            {
                house.Roof.Build();
            }
            else
            {
                Console.WriteLine($"{Name}: Дом уже построен.");
            }
        }
    }
    class TeamLeader : IWorker
    {
        public string Name { get; private set; }
        public TeamLeader(string name)
        {
            Name = name;
        }
        public void Work(House house)
        {
            Console.WriteLine($"{Name}: Отчет о ходе строительства:");
            Console.WriteLine($"Фундамент: {(house.Basement.IsBuilt ? "Построен" : "Не построен")}");
            Console.WriteLine($"Стены: {(house.Walls.IsBuilt ? "Построены" : "Не построены")}");
            Console.WriteLine($"Дверь: {(house.Door.IsBuilt ? "Построена" : "Не построена")}");
            Console.WriteLine($"Окна: {house.Windows.Count(w => w.IsBuilt)} из 4 построены.");
            Console.WriteLine($"Крыша: {(house.Roof.IsBuilt ? "Построена" : "Не построена")}");
        }
    }
    class Team
    {
        public Worker[] Workers { get; private set; }
        public TeamLeader Leader { get; private set; }

        public Team(string[] workerNames, string leaderName)
        {
            Workers = new Worker[workerNames.Length];
            for (int i = 0; i < workerNames.Length; i++)
            {
                Workers[i] = new Worker(workerNames[i]);
            }
            Leader = new TeamLeader(leaderName);
        }
        public void BuildHouse()
        {
            House house = new House();

            Random random = new Random();
            int workerIndex = 0;

            while (!house.IsBuilt())
            {
                if (random.Next(3) == 0)
                {
                    Leader.Work(house);
                }
                else
                {
                    Workers[workerIndex].Work(house);
                    workerIndex = (workerIndex + 1) % Workers.Length;
                }
            }

            Console.WriteLine("Строительство дома завершено!");
            house.Print();
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            string[] workerNames = { "Иван", "Петр", "Сидор" };
            string leaderName = "Бригадир";

            Team team = new Team(workerNames, leaderName);
            team.BuildHouse();

            Console.ReadKey();
        }
    }
}
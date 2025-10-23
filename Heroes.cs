using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Lab_02.Program;

namespace Lab_02
{
    public abstract class Hero
    {

        public int Id { get; set; } // Значение поля не должно быть 0
        public string Name { get; set; }
        public string ClassName { get; set; }

        public decimal Health { get; set; } = 20; // Значение поля должно быть больше 0 и не больше 30
        public int CritChance { get; set; } = 10; // Максимальное значение поля 90


        public Stack<Item> Pocket = new Stack<Item>();
        public enum PassiveSkill
        {
            BlastPunch = 1,
            SteelHeart = 2
        }
        public enum ActiveSkill
        {
            Perfection = 1,
            MeltingHeart = 2
        }
        public PassiveSkill PSkill { get; set; }
        public ActiveSkill ASkill { get; set; }
        public Weapon Weapon { get; set; }
        public Defense Shield { get; set; }
        public struct Defense
        {
            public int Def { get; set; } // Значение поля должно быть больше 0
            public Defense(int def)
            {
                Def = def;
            }
        }
        public Hero(string name, int id, ActiveSkill aSkill)

        {
            Id = id;
            Name = name;

            ASkill = aSkill;


            PSkill = (PassiveSkill)Random.Shared.Next(1, 3);

            if (PSkill is PassiveSkill.SteelHeart)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                //Console.WriteLine("Была получена пассивная способность Стальное Сердце [+10 к здоровью]");
                Console.ForegroundColor = ConsoleColor.White;
                Health += 10;
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                //Console.WriteLine("Была получена пассивная способность Взрывной удар [+10 к урону от оружия]");
                Console.ForegroundColor = ConsoleColor.White;
            }
        }

        public void GetWeapon(Weapon weapon)
        {
            Weapon = weapon;
            Console.WriteLine($"{this.Name} получил оружие {weapon.Name} имеющее {weapon.Damage} урона!");
        }
        public void GetItem(Item item)
        {
            Console.WriteLine($"Герой получил {item.Name}");
            Pocket.Push(item);
        }
        public void UseItem()
        {
            if (Pocket.Count > 0)
            {
                var temp = Pocket.Pop();
                temp.Use(this);
                Console.WriteLine("Предмет был использован");
            }
            else
            {
                Console.WriteLine("У вас нет предметов");
            }
        }
        public override bool Equals(object? obj)
        {
            return obj.GetHashCode == obj.GetHashCode;
        }
        public override int GetHashCode()
        {
            return Convert.ToInt32(Health);
        }
        public override string ToString()
        {
            return $"{this.Name}";
        }

    }
    public class NoWeaponException : Exception
    {
        public NoWeaponException(string message) : base(message)
        {

        }
        public override string Message => base.Message + " [Игровое Исключение]";
    }
    public class Knight : Hero
    {

        public Knight(string name, int id, ActiveSkill aSkill) : base(name, id, aSkill)
        {
            ClassName = "Рыцарь";
        }


        public decimal Slash()
        {
            try
            {
                if (Weapon != null)
                {
                    int bonus = PSkill is PassiveSkill.BlastPunch ? 10 : 0;

                    Console.WriteLine($"{this.Name} взмахнул оружием на {Weapon.Damage + bonus} урона");
                    return Weapon.Damage + bonus;
                }
                else
                {
                    throw new NoWeaponException("Weapon is null");

                }
            }
            catch (NoWeaponException ex)
            {
                Console.WriteLine($"У {this.Name} нет оружия!");
                return 0;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Произошла ошибка: {ex.Message}");
                return 0;
            }
        }
        

        public static Knight CreateKnight(int id)
        {
            Console.WriteLine("Введите имя");
            string name = Console.ReadLine();
            Console.WriteLine("Введите значение параметра 'Здоровье' [Значение поля должно быть больше 0 и не больше 30]");
        hStart:
            decimal.TryParse(Console.ReadLine(), out decimal health);
            switch (health)
            {
                case > 0 and <= 30:
                    break;
                default:
                    Console.WriteLine("Некорректный ввод");
                    goto hStart;
            }
            Console.WriteLine("Введите значение параметра 'Шанс крит. удара' [Значение поля может быть максимум 90]");
        cStart:
            int crit;
            try
            {
                crit = int.Parse(Console.ReadLine());

            }
            catch
            {
                Console.WriteLine("Некорректный ввод");
                goto cStart;
            }
            switch (crit)
            {
                case < 90:
                    break;
                default:
                    Console.WriteLine("Неккоректное значение");
                    goto cStart;
            }
            Console.WriteLine("""
                 Выберите один активный навык из списка:
                 Perfection,
                 MeltingHeart
                
                """);
        askillStart:
            string skill = Console.ReadLine();
            ActiveSkill aSkill;
            switch (skill)
            {
                case "Perfection":
                    aSkill = ActiveSkill.Perfection;
                    break;
                case "MeltingHeart":
                    aSkill = ActiveSkill.MeltingHeart;
                    break;
                default:
                    Console.WriteLine("Некорректный ввод");
                    goto askillStart;
            }
            Console.WriteLine("Введите название для оружия");
            Weapon weapon = new Weapon(Console.ReadLine());
            Console.WriteLine("Введите значение защиты щита [Значение поля должно быть больше 0 и не больше 20]");
        shStart:
            int.TryParse(Console.ReadLine(), out int def);
            Defense shield;
            switch (def)
            {
                case > 0 and <= 20:
                    shield = new Defense(def);
                    break;
                default:
                    Console.WriteLine("Некорректное значение");
                    goto shStart;
            }

            Knight knight = new Knight(name, id, aSkill)
            {
                Health = health,
                CritChance = crit,
                Weapon = weapon,
                Shield = shield

            };
            
            return knight;

        }
    } 
}

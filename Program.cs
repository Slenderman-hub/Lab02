namespace Lab_02
{
    class Program
    {
        static void Main()
        {
            
        }
        static async void ControlCancel(object? sender, ConsoleCancelEventArgs e)
        {
            e.Cancel = true;
        }
        public static class GameSession
        {

            public static void Start(Knight knight)
            {
                decimal stockDamage;
                Enemy enemy;
                if (Random.Shared.Next(0, 3) == 0)
                {
                    enemy = new Ghoul();
                }
                else
                {
                    enemy = new Slime();
                }
                bool flag = true;


                do
                {
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.WriteLine($"Начат бой c врагом типа |{enemy.Name}|");
                    Console.WriteLine("Что сделать?\n1] Взмахнуть оружием\n2] Использовать предмет");
                    Console.ForegroundColor = ConsoleColor.White;
                    ConsoleKeyInfo key = Console.ReadKey(true);
                    if (ConsoleModifiers.Control == key.Modifiers)
                    {

                    }


                    switch (key.Key)
                    {
                        case ConsoleKey.D1:
                            {

                                stockDamage = knight.Slash();
                                enemy.Health -= stockDamage;

                                if (enemy.Health <= 0)
                                {
                                    Console.ForegroundColor = ConsoleColor.Green;
                                    Console.WriteLine("Вы убили врага");

                                    flag = false;
                                }
                                else
                                {
                                    Console.WriteLine($"У врага осталось {enemy.Health} здоровья");
                                    Console.WriteLine($"{enemy.Name} ударяет в ответ на {enemy.Damage} урона");
                                    knight.Health -= enemy.Damage;
                                    if (knight.Health <= 0)
                                    {
                                        Console.ForegroundColor = ConsoleColor.Red;
                                        Console.WriteLine("Вас убили");
                                        flag = false;
                                        Console.WriteLine();
                                    }
                                    else
                                    {
                                        Console.WriteLine($"У вас осталось {knight.Health} здоровья");
                                    }
                                }

                                break;
                            }
                        case ConsoleKey.D2:
                            {
                                knight.UseItem();
                                break;

                            }
                        default:
                            Console.WriteLine("Неправильный ввод");
                            break;

                    }
                } while (flag);
            }
            public abstract class Enemy

            {
                public string Name { get; set; } = "Unknown";
                public decimal Health { get; set; } = 10;
                public decimal Damage { get; set; } = 10;




            }
            public class Ghoul : Enemy
            {
                public Ghoul()
                {
                    Name = "Гуль";
                    Health = 20;

                }

            }

            public class Slime : Enemy
            {
                public Slime()
                {
                    Name = "Cлайм";
                    Health = 40;
                    Damage = 7;

                }

            }

        }

        public class NoWeaponException : Exception
        {
            public NoWeaponException(string message) : base(message)
            {

            }
            public override string Message => base.Message + " [Игровое Исключение]";
        }
        public record Weapon
        {
            public string Name { get; init; }
            public int Damage { get; init; }

            public Weapon(string name)
            {
                Name = name;
                Damage = Random.Shared.Next(1, 10);
            }
            public override int GetHashCode()
            {
                return Damage;
            }
            public override string ToString()
            {
                return Name.ToString();
            }

        }
        public interface Item
        {
            public string Name { get; }
            public abstract void Use(Hero hero);
        }
        public record StrongPotion : Item
        {
            public string Name { get => "Сильное зелье"; }
            public void Use(Hero hero)
            {
                hero.Health += Random.Shared.Next(5, 40);
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("Вы увеличили свое максимальное здоровье");
                Console.ForegroundColor = ConsoleColor.White;
            }
        }
        public abstract class Hero
        {
            //Харктеристики
            public string Name { get; protected set; }
            public string ClassName { get; protected set; }

            public decimal Health { get; set; } = 20;
            public int CritChance { get; set; } = 10;
            public decimal BonusHeart { get; set; } = 0;

            protected Stack<Item> Pocket = new Stack<Item>();
            //Навыки
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
            //Оружие
            protected Weapon Weapon { get; set; }
            struct Shield
            {
                public int defense { get; set; }
            }
            //Методы
            public Hero(string name, string className)

            {
                Name = name;
                ClassName = className;
                ASkill = ActiveSkill.Perfection;

                PSkill = (PassiveSkill)Random.Shared.Next(1, 3);

                if (PSkill is PassiveSkill.SteelHeart)
                {
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine("Была получена пассивная способность Стальное Сердце [+10 к здоровью]");
                    Console.ForegroundColor = ConsoleColor.White;
                    Health += 10;
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine("Была получена пассивная способность Взрывной удар [+10 к урону от оружия]");
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
        public class Knight : Hero
        {

            public Knight(string name, string className) : base(name, className)
            {

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
        }

    }
}

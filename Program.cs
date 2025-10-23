using static Lab_02.Hero;
using D = System.IO.Directory;
using J = System.Text.Json.JsonSerializer;
namespace Lab_02
{
    public class Program
    {
        public static readonly string ProgPath = D.GetParent(D.GetCurrentDirectory()).Parent.Parent.FullName;
        public static string FilePath = null;
        public static HashSet<Knight> Set = new HashSet<Knight>();
        public static HashSet<int> Ids = new HashSet<int>();
        public static bool Flag = true;
        public static DateTime DateSetInit;
        static void Main()
        {
            Console.CancelKeyPress += ControlCancel;
            LoadFile();
            Console.WriteLine("Введите команду. [help - вывести справку по доступным командам]");
            while (Flag)
            {
                CommandProcessing(Console.ReadLine().Trim());
            }
            
            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.WriteLine("Программа завершилась");
            Console.ForegroundColor = ConsoleColor.White;
        }
        static void LoadFile()
        {
            do
            {
                try
                {
                    Console.WriteLine("Назовите существующий или новый файл");
                    string input = Console.ReadLine();
                    if (string.IsNullOrWhiteSpace(input))
                    {
                        throw new Exception(" Пустой ввод не допускается");
                       
                    }
                    FilePath = ProgPath + @"\" + input + ".json";
                    if (File.Exists(FilePath))
                    {
                        Console.WriteLine("Была загружена коллекция из " + FilePath);
                        string json = File.ReadAllText(FilePath);
                        Set = J.Deserialize<HashSet<Knight>>(json);

                        foreach (var item in Set)
                        {
                            Ids.Add(item.Id);
                        }
                        DateSetInit = DateTime.Now;

                    }
                    else
                    {
                        

                        
                        File.WriteAllText(FilePath, "[]");
                        
                        
                        DateSetInit = DateTime.Now;
                        Console.WriteLine("Был создана новая коллекция по пути " + FilePath);

                    }

                    break;

                }
                catch (Exception e)
                {
                    Console.WriteLine("Произошла ошибка:" + e.Message);
                }

            } while (true);
        }
        static public void CommandProcessing(string arg)
        {
                try
                {
                    string[] args = arg.Trim().Split(" ");
                    switch (args[0])
                    {
                        case "help":
                            if(args.Length == 1)
                            {
                                Cmd.Help();
                            }
                            else
                            {
                                Console.WriteLine("Некорректный ввод аргументов");
                            }
                            break;
                        case "info":
                        if (args.Length == 1)
                        {
                            Cmd.Info();
                        }
                        else
                        {
                            Console.WriteLine("Некорректный ввод аргументов");
                        }
                        break;
                    case "show":
                        if (args.Length == 1)
                        {
                            Cmd.Show();
                        }
                        else
                        {
                            Console.WriteLine("Некорректный ввод аргументов");
                        }
                        break;
                        case "insert":
                        if (args.Length == 1)
                        {
                            Cmd.Insert();
                        }
                        else
                        {
                            Console.WriteLine("Некорректный ввод аргументов");
                        }
                        break;
                        case "remove":
                            if (args.Length == 1)
                            {
                                Console.WriteLine("Ошибка. Не введен id элемента");
                                break;
                            }
                            else if (args.Length == 2)
                            {
                                Cmd.Remove(args[1]);
                                break;
                            }
                            else
                            {
                                Console.WriteLine("Некорректный ввод аргументов");
                                break;
                            }
                        case "clear":
                        if (args.Length == 1)
                        {
                            Cmd.Clear();
                        }
                        else
                        {
                            Console.WriteLine("Некорректный ввод аргументов");
                        }
                        break;
                        case "save":
                        if (args.Length == 1)
                        {
                            Cmd.Save();
                        }
                        else
                        {
                            Console.WriteLine("Некорректный ввод аргументов");
                        }
                        break;
                        case "execute_script":
                            if (args.Length == 1)
                            {
                                Console.WriteLine("Ошибка. Нe введенно имя файла");
                                break;
                            }
                            else if (args.Length == 2)
                            {
                                Cmd.ExecuteScript(args[1]);
                                break;
                            }
                            else
                            {
                                Console.WriteLine("Некорректный ввод аргументов");
                                break;
                            }
                        case "exit":
                        if (args.Length == 1)
                        {
                            Flag = false;
                        }
                        else
                        {
                            Console.WriteLine("Некорректный ввод аргументов");
                        }
                        break;
                        case "print_field_descending":
                            if (args.Length == 1)
                            {
                                Console.WriteLine("Ошибка. Нe введено поле элемента");
                                break;
                            }
                            else if (args.Length == 2)
                            {
                                Cmd.PrintFieldDescending(args[1]);
                                break;
                            }
                            else
                            {
                                Console.WriteLine("Некорректный ввод аргументов");
                                break;
                            }
                        case "max":
                            if (args.Length == 1)
                            {
                                Console.WriteLine("Ошибка. Нe введено поле элемента");
                                break;
                            }
                            else if (args.Length == 2)
                            {
                                Cmd.Max(args[1]);
                                break;
                            }
                            else
                            {
                                Console.WriteLine("Некорректный ввод аргументов");
                                break;
                            }
                        case "min":
                            if (args.Length == 1)
                            {
                                Console.WriteLine("Ошибка. Нe введено поле элемента");
                                break;
                            }
                            else if (args.Length == 2)
                            {
                                Cmd.Min(args[1]);
                                break;
                            }
                            else
                            {
                                Console.WriteLine("Некорректный ввод аргументов");
                                break;
                            }
                        default:
                            Console.WriteLine("Неверный ввод");
                            break;
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine("Ошибка ввода: " + e.Message);
                    
                }
        }
        

        static async void ControlCancel(object? sender, ConsoleCancelEventArgs e)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("Была введена секретная команда - Экстренный взлет на воздух");
            Console.ForegroundColor = ConsoleColor.White;
            e.Cancel = false;

            Environment.Exit(0);
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

    }
}

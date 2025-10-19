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
        public static DateTime DateSetInit;
        static void Main()
        {
            Console.CancelKeyPress += ControlCancel;
            LoadFile();
            CommandProcessing();
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
                        DateSetInit = DateTime.Now;

                    }
                    else
                    {
                        Console.WriteLine("Был создана новая коллекция по пути " + FilePath);

                        
                        File.WriteAllText(FilePath, "[]");
                        
                        
                        DateSetInit = DateTime.Now;


                    }

                    break;

                }
                catch (Exception e)
                {
                    Console.WriteLine("Произошла ошибка:" + e.Message);
                }

            } while (true);
        }
        static void CommandProcessing()
        {
            Console.WriteLine("Введите команду. [help - вывести справку по доступным командам]");
            
            start:
            try
            {
                string[] args = Console.ReadLine().Trim().Split(" ");
                switch (args[0])
                {
                    case "help":
                        Cmd.Help();
                        goto start;
                    case "info":
                        Cmd.Info();
                        goto start;
                    case "show":
                        Cmd.Show();
                        goto start;
                    case "insert":
                        if (args.Length == 1)
                        {
                            Console.WriteLine("Ошибка. Не введен id элемента");
                            goto start;
                        }
                        else if (args.Length == 2)
                        {
                            Cmd.Insert(args[1]);
                            goto start;
                        }
                        else
                        {
                            Console.WriteLine("Некорректный ввод аргументов");
                            goto start;
                        }
                    case "update":
                        if (args.Length == 1)
                        {
                            Console.WriteLine("Ошибка. Не введен id элемента");
                            goto start;
                        }
                        else if (args.Length == 2)
                        {
                            Cmd.Update(args[1]);
                            goto start;
                        }
                        else
                        {
                            Console.WriteLine("Некорректный ввод аргументов");
                            goto start;
                        }
                    case "remove":
                        if (args.Length == 1)
                        {
                            Console.WriteLine("Ошибка. Не введен id элемента");
                            goto start;
                        }
                        else if (args.Length == 2)
                        {
                            Cmd.Remove(args[1]);
                            goto start;
                        }
                        else
                        {
                            Console.WriteLine("Некорректный ввод аргументов");
                            goto start;
                        }
                    case "clear":
                        Cmd.Clear();
                        goto start;
                    case "save":
                        Cmd.Save();
                        goto start;
                    case "execute_script":
                        if (args.Length == 1)
                        {
                            Console.WriteLine("Ошибка. Нe введенно имя файла");
                            goto start;
                        }
                        else if (args.Length == 2)
                        {
                            Cmd.ExecuteScript(args[1]);
                            goto start;
                        }
                        else
                        {
                            Console.WriteLine("Некорректный ввод аргументов");
                            goto start;
                        }
                    case "exit":
                        break;
                    case "print_field_descending":
                        if (args.Length == 1)
                        {
                            Console.WriteLine("Ошибка. Нe введено поле элемента");
                            goto start;
                        }
                        else if (args.Length == 2)
                        {
                            Cmd.PrintFieldDescending(args[1]);
                            goto start;
                        }
                        else
                        {
                            Console.WriteLine("Некорректный ввод аргументов");
                            goto start;
                        }
                    case "max":
                        if (args.Length == 1)
                        {
                            Console.WriteLine("Ошибка. Нe введено поле элемента");
                            goto start;
                        }
                        else if (args.Length == 2)
                        {
                            Cmd.Max(args[1]);
                            goto start;
                        }
                        else
                        {
                            Console.WriteLine("Некорректный ввод аргументов");
                            goto start;
                        }

                    case "min":
                        if (args.Length == 1)
                        {
                            Console.WriteLine("Ошибка. Нe введено поле элемента");
                            goto start;
                        }
                        else if (args.Length == 2)
                        {
                            Cmd.Min(args[1]);
                            goto start;
                        }
                        else
                        {
                            Console.WriteLine("Некорректный ввод аргументов");
                            goto start;
                        }
                    default:
                        Console.WriteLine("Неверный ввод");
                        goto start;
                }
            }
            catch(Exception e)
            {
                Console.WriteLine("Ошибка ввода: " + e.Message);
                goto start;
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

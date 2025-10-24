using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace Lab_02
{
    public static class Cmd
    {
        //help — вывести справку по доступным командам.
        //info — вывести информацию о коллекции(тип, дата инициализации, количество элементов).
        //show — вывести все элементы коллекции в строковом представлении.
        //insert — добавить новый элемент в коллекцию.
        //update {id элемента} — обновить элемент коллекции по идентификатору.
        //remove {id элемента} — удалить элемент по ключу (для словарей) или по идентификатору.
        //clear — очистить коллекцию.
        //save — сохранить коллекцию в файл.
        //execute_script {название файла} — выполнить набор команд из файла.
        //exit — завершить программу.
        //print_field_descending {название поля} — вывести значения поля в порядке убывания.
        //max {название поля} — вывести элемент с максимальным значением указанного поля.
        //min {название поля} — вывести элемент с минимальным значением указанного поля.

        public static void Help()
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine(
                """
                help — вывести справку по доступным командам.
                info — вывести информацию о коллекции (тип, дата инициализации, количество элементов).
                show — вывести все элементы коллекции в строковом представлении.
                insert — добавить новый элемент в коллекцию.
                update {id элемента} — обновить элемент коллекции по идентификатору.
                remove {id элемента} — удалить элемент по ключу (для словарей) или по идентификатору.
                clear — очистить коллекцию.
                save — сохранить коллекцию в файл.
                execute_script {название файла} — выполнить набор команд из файла.
                exit — завершить программу.
                print_field_descending {название поля} — вывести значения поля в порядке убывания.
                max {название поля} — вывести элемент с максимальным значением указанного поля.
                min {название поля} — вывести элемент с минимальным значением указанного поля.
                """
                );
            Console.ForegroundColor = ConsoleColor.White;

        }
        public static void Info()
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine(
                $""" 
                Тип коллекции: {Program.Set.GetType().Name.Trim('`', '1')}<{Program.Set.GetType().GenericTypeArguments[0].Name}>
                Дата инициализации: {Program.DateSetInit}
                Количество элементов: {Program.Set.Count}
                """);
            Console.ForegroundColor = ConsoleColor.White;
        }
        public static void Show() 
        {
            
                //Console.WriteLine(JsonSerializer.Serialize(Program.Set)); !! Имеет место быть, но нечитабельно
                if(Program.Set.Count == 0)
                {
                    Console.WriteLine("В коллекции нет элементов");
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    foreach (var item in Program.Set)
                    {

                        Console.WriteLine($"Id : {item.Id}, Класс: {item.ClassName}, Имя: {item.Name}, Шанс крит. удара: {item.CritChance}, Здоровье: {item.Health}, Акт. Навык: {item.ASkill}, Пассивный навык: {item.PSkill},\n Щит: {item.Shield.Def},Оружие: {item.Weapon.Name}, Урон Оружия: {item.Weapon.Damage}");
                        Console.WriteLine("\n");

                    }
                    Console.ForegroundColor = ConsoleColor.White;
                }
                    
            
        } 
        public static void Insert()
        {
            int id = 0;
            for (int i = 1; i <= Program.Set.Count + 1; i++)
            {
                if (!Program.Ids.Contains(i))
                {
                    id = i;
                }
            }
            if (id == 0) 
            { 
                Console.WriteLine("Что-то пошло не так");
                return; 
            }

                Console.WriteLine("Начало описания обьекта рыцарь");
                Program.Set.Add(Knight.CreateKnight(id));
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Обьект был внесен в коллекцию");
                Program.Ids.Add(id);
                Console.ForegroundColor = ConsoleColor.White;
                return;

            
        }
        public static void Update(string arg)
        {
            int.TryParse(arg, out int id);
            if (id == 0)
            {
                Console.WriteLine("Некорректный ввод аргумента");
                return;
            }
            else
            {
                Knight knight = new Knight("dummy",0, Hero.ActiveSkill.Perfection);

                foreach(var item in Program.Set)
                {
                    if(item.Id == id)
                    {
                        knight = item;
                    }
                }
                
                if (knight.Id == 0)
                {
                    Console.WriteLine("Обьекта с таким id несуществует");
                }
                else
                {
                    Program.Set.Remove(knight);
                    Console.WriteLine($"Начало замены обьекта с id: {id}");
                    knight = Knight.CreateKnight(id);
                    Program.Set.Add(knight);
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("Обьект в коллекции был обновлен");
                    Console.ForegroundColor = ConsoleColor.White;

                }
                
            }
        }
        public static void Remove(string arg)
        {
            int.TryParse(arg, out int id);
            if (id == 0)
            {
                Console.WriteLine("Некорректный ввод аргумента");
                return;
            }
            else
            {
                Knight knight = new Knight("dummy", 0, Hero.ActiveSkill.Perfection);

                foreach (var item in Program.Set)
                {
                    if (item.Id == id)
                    {
                        knight = item;
                    }
                }

                if (knight.Id == 0)
                {
                    Console.WriteLine("Обьекта с таким id несуществует");
                }
                else
                {
                    Program.Set.Remove(knight);
                    Program.Ids.Remove(id);
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("Обьект был удален из коллекции");
                    Console.ForegroundColor = ConsoleColor.White;

                }

            }
        }
        public static void Clear()
        {
            Program.Set.Clear();
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Коллекция была очищена");
            Console.ForegroundColor = ConsoleColor.White;
        }
        public static void Save()
        {
            try
            {
                string json = JsonSerializer.Serialize(Program.Set);
                File.WriteAllText(Program.FilePath, json);
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Коллекция была сохранена в файл");
                Console.ForegroundColor = ConsoleColor.White;
            }
            catch(Exception e)
            {
                Console.WriteLine("Ошибка сохранения файла: " + e.Message);
            }
            

        }
        public static void PrintFieldDescending(string arg)
        {
            if (Program.Set.Count == 0)
            {
                Console.WriteLine("В коллекции нет элементов");
                return;
            }
            HashSet<string> fields = ["Id", "CritChance", "Health", "Shield","Weapon"];
            if (fields.Contains(arg))
            {
                Console.WriteLine();
                Console.ForegroundColor = ConsoleColor.Cyan;
                switch (arg)
                {
                    case "Id":
                        foreach (var item in Program.Set.OrderByDescending(item => item.Id))
                        {
                            Console.WriteLine($"Id : {item.Id}, Класс:{item.ClassName} Имя: {item.Name},Шанс крит. удара: {item.CritChance},Здоровье: {item.Health}, Акт. Навык: {item.ASkill}, Пассивный навык: {item.PSkill},\n Щит: {item.Shield.Def},Оружие: {item.Weapon.Name}, Урон Оружия: {item.Weapon.Damage}");
                            Console.WriteLine("\n");
                        }
                        break;
                    case "CritChance":
                        foreach (var item in Program.Set.OrderByDescending(item => item.CritChance))
                        {
                            Console.WriteLine($"Id : {item.Id}, Класс:{item.ClassName} Имя: {item.Name},Шанс крит. удара: {item.CritChance},Здоровье: {item.Health}, Акт. Навык: {item.ASkill}, Пассивный навык: {item.PSkill},\n Щит: {item.Shield.Def},Оружие: {item.Weapon.Name}, Урон Оружия: {item.Weapon.Damage}");
                            Console.WriteLine("\n");
                        }
                        break;
                    case "Health":
                        foreach (var item in Program.Set.OrderByDescending(item => item.Health))
                        {
                            Console.WriteLine($"Id : {item.Id}, Класс:{item.ClassName} Имя: {item.Name},Шанс крит. удара: {item.CritChance},Здоровье: {item.Health}, Акт. Навык: {item.ASkill}, Пассивный навык: {item.PSkill},\n Щит: {item.Shield.Def},Оружие: {item.Weapon.Name}, Урон Оружия: {item.Weapon.Damage}");
                            Console.WriteLine("\n");
                        }
                        break;
                    case "Shield":
                        foreach (var item in Program.Set.OrderByDescending(item => item.Shield.Def))
                        {
                            Console.WriteLine($"Id : {item.Id}, Класс:{item.ClassName} Имя: {item.Name},Шанс крит. удара: {item.CritChance},Здоровье: {item.Health}, Акт. Навык: {item.ASkill}, Пассивный навык: {item.PSkill},\n Щит: {item.Shield.Def},Оружие: {item.Weapon.Name}, Урон Оружия: {item.Weapon.Damage}");
                            Console.WriteLine("\n");
                        }
                        break;
                    case "Weapon":
                        foreach (var item in Program.Set.OrderByDescending(item => item.Weapon.Damage))
                        {
                            Console.WriteLine($"Id : {item.Id}, Класс:{item.ClassName} Имя: {item.Name},Шанс крит. удара: {item.CritChance},Здоровье: {item.Health}, Акт. Навык: {item.ASkill}, Пассивный навык: {item.PSkill},\n Щит: {item.Shield.Def},Оружие: {item.Weapon.Name}, Урон Оружия: {item.Weapon.Damage}");
                            Console.WriteLine("\n");
                        }
                        break;
                    default:
                        Console.WriteLine("Некорректное поле");
                        break;
                }
                Console.ForegroundColor = ConsoleColor.White;
            }
            else
            {
                Console.WriteLine("Некорректное поле");
            }
        }
        public static void Max(string arg)
        {
            if (Program.Set.Count == 0)
            {
                Console.WriteLine("В коллекции нет элементов");
                return;
            }
            HashSet<string> fields = ["Id", "CritChance", "Health", "Shield", "Weapon"];
            
                Console.ForegroundColor = ConsoleColor.Cyan;
                if (fields.Contains(arg))
                {


                    switch (arg)
                    {
                        case "Id":
                            int maxId = Program.Set.Max(item => item.Id);
                            Knight maxIdKnight = Program.Set.First(item => item.Id == maxId);
                            Console.WriteLine();
                            Console.WriteLine($"Id : {maxIdKnight.Id}, Класс:{maxIdKnight.ClassName} Имя: {maxIdKnight.Name},Шанс крит. удара: {maxIdKnight.CritChance},Здоровье: {maxIdKnight.Health}, Акт. Навык: {maxIdKnight.ASkill}, Пассивный навык: {maxIdKnight.PSkill},\n Щит: {maxIdKnight.Shield.Def},Оружие: {maxIdKnight.Weapon.Name}, Урон Оружия: {maxIdKnight.Weapon.Damage}");
                            break;
                        case "Health":
                            decimal maxHp = Program.Set.Max(item => item.Health);
                            Knight maxHpKnight = Program.Set.First(item => item.Health == maxHp);
                            Console.WriteLine();
                            Console.WriteLine($"Id : {maxHpKnight.Id}, Класс:{maxHpKnight.ClassName} Имя: {maxHpKnight.Name},Шанс крит. удара: {maxHpKnight.CritChance},Здоровье: {maxHpKnight.Health}, Акт. Навык: {maxHpKnight.ASkill}, Пассивный навык: {maxHpKnight.PSkill},\n Щит: {maxHpKnight.Shield.Def},Оружие: {maxHpKnight.Weapon.Name}, Урон Оружия: {maxHpKnight.Weapon.Damage}");
                            break;

                        case "CritChance":
                            int maxCC = Program.Set.Max(item => item.CritChance);
                            Knight maxCCKnight = Program.Set.First(item => item.CritChance == maxCC);
                            Console.WriteLine();
                            Console.WriteLine($"Id : {maxCCKnight.Id}, Класс:{maxCCKnight.ClassName} Имя: {maxCCKnight.Name},Шанс крит. удара: {maxCCKnight.CritChance},Здоровье: {maxCCKnight.Health}, Акт. Навык: {maxCCKnight.ASkill}, Пассивный навык: {maxCCKnight.PSkill},\n Щит: {maxCCKnight.Shield.Def},Оружие: {maxCCKnight.Weapon.Name}, Урон Оружия: {maxCCKnight.Weapon.Damage}");
                            break;
                        case "Shield":
                            int maxDef = Program.Set.Max(item => item.Shield.Def);
                            Knight maxDefKnight = Program.Set.First(item => item.Shield.Def == maxDef);
                            Console.WriteLine();
                            Console.WriteLine($"Id : {maxDefKnight.Id}, Класс:{maxDefKnight.ClassName} Имя: {maxDefKnight.Name},Шанс крит. удара: {maxDefKnight.CritChance},Здоровье: {maxDefKnight.Health}, Акт. Навык: {maxDefKnight.ASkill}, Пассивный навык: {maxDefKnight.PSkill},\n Щит: {maxDefKnight.Shield.Def},Оружие: {maxDefKnight.Weapon.Name}, Урон Оружия: {maxDefKnight.Weapon.Damage}");
                            break;
                        case "Weapon":
                            int maxDmg = Program.Set.Max(item => item.Weapon.Damage);
                            Knight maxDmgKnight = Program.Set.First(item => item.Weapon.Damage == maxDmg);
                            Console.WriteLine();
                            Console.WriteLine($"Id : {maxDmgKnight.Id}, Класс:{maxDmgKnight.ClassName} Имя: {maxDmgKnight.Name},Шанс крит. удара: {maxDmgKnight.CritChance},Здоровье: {maxDmgKnight.Health}, Акт. Навык: {maxDmgKnight.ASkill}, Пассивный навык: {maxDmgKnight.PSkill},\n Щит: {maxDmgKnight.Shield.Def},Оружие: {maxDmgKnight.Weapon.Name}, Урон Оружия: {maxDmgKnight.Weapon.Damage}");
                            break;
                        default:
                            Console.WriteLine("Неккоректное поле");
                            break;
                    }
                    Console.ForegroundColor = ConsoleColor.White;
                }
                else
                {

                        Console.WriteLine("Некорректное поле");
                
                }


           
            
        }
        
        public static void Min(string arg)
        {
            if (Program.Set.Count == 0)
            {
                Console.WriteLine("В коллекции нет элементов");
                return;
            }
            HashSet<string> fields = ["Id", "CritChance", "Health", "Shield", "Weapon"];
            if (fields.Contains(arg))
            {
                Console.ForegroundColor = ConsoleColor.Cyan;
                switch (arg)
                {
                    case "Id":
                        int minId = Program.Set.Min(item => item.Id);
                        Knight minIdKnight = Program.Set.First(item => item.Id == minId);
                        Console.WriteLine();
                        Console.WriteLine($"Id : {minIdKnight.Id}, Класс:{minIdKnight.ClassName} Имя: {minIdKnight.Name},Шанс крит. удара: {minIdKnight.CritChance},Здоровье: {minIdKnight.Health}, Акт. Навык: {minIdKnight.ASkill}, Пассивный навык: {minIdKnight.PSkill},\n Щит: {minIdKnight.Shield.Def},Оружие: {minIdKnight.Weapon.Name}, Урон Оружия: {minIdKnight.Weapon.Damage}");
                        break;
                    case "Health":
                        decimal minHp = Program.Set.Min(item => item.Health);
                        Knight minHpKnight = Program.Set.First(item => item.Health == minHp);
                        Console.WriteLine();
                        Console.WriteLine($"Id : {minHpKnight.Id}, Класс:{minHpKnight.ClassName} Имя: {minHpKnight.Name},Шанс крит. удара: {minHpKnight.CritChance},Здоровье: {minHpKnight.Health}, Акт. Навык: {minHpKnight.ASkill}, Пассивный навык: {minHpKnight.PSkill},\n Щит: {minHpKnight.Shield.Def},Оружие: {minHpKnight.Weapon.Name}, Урон Оружия: {minHpKnight.Weapon.Damage}");
                        break;

                    case "CritChance":
                        int minCC = Program.Set.Min(item => item.CritChance);
                        Knight minCCKnight = Program.Set.First(item => item.CritChance == minCC);
                        Console.WriteLine();
                        Console.WriteLine($"Id : {minCCKnight.Id}, Класс:{minCCKnight.ClassName} Имя: {minCCKnight.Name},Шанс крит. удара: {minCCKnight.CritChance},Здоровье: {minCCKnight.Health}, Акт. Навык: {minCCKnight.ASkill}, Пассивный навык: {minCCKnight.PSkill},\n Щит: {minCCKnight.Shield.Def},Оружие: {minCCKnight.Weapon.Name}, Урон Оружия: {minCCKnight.Weapon.Damage}");
                        break;
                    case "Shield":
                        int minDef = Program.Set.Min(item => item.Shield.Def);
                        Knight minDefKnight = Program.Set.First(item => item.Shield.Def == minDef);
                        Console.WriteLine();
                        Console.WriteLine($"Id : {minDefKnight.Id}, Класс:{minDefKnight.ClassName} Имя: {minDefKnight.Name},Шанс крит. удара: {minDefKnight.CritChance},Здоровье: {minDefKnight.Health}, Акт. Навык: {minDefKnight.ASkill}, Пассивный навык: {minDefKnight.PSkill},\n Щит: {minDefKnight.Shield.Def},Оружие: {minDefKnight.Weapon.Name}, Урон Оружия: {minDefKnight.Weapon.Damage}");
                        break;
                    case "Weapon":
                        int minDmg = Program.Set.Min(item => item.Weapon.Damage);
                        Knight minDmgKnight = Program.Set.First(item => item.Weapon.Damage == minDmg);
                        Console.WriteLine();
                        Console.WriteLine($"Id : {minDmgKnight.Id}, Класс:{minDmgKnight.ClassName} Имя: {minDmgKnight.Name},Шанс крит. удара: {minDmgKnight.CritChance},Здоровье: {minDmgKnight.Health}, Акт. Навык: {minDmgKnight.ASkill}, Пассивный навык: {minDmgKnight.PSkill},\n Щит: {minDmgKnight.Shield.Def},Оружие: {minDmgKnight.Weapon.Name}, Урон Оружия: {minDmgKnight.Weapon.Damage}");
                        break;
                    default:
                        Console.WriteLine("Некорректное поле");
                        break;
                }
                Console.ForegroundColor = ConsoleColor.White;
            }
            else
            {
                Console.WriteLine("Некорректное поле");
            }
        }

        public static void ExecuteScript(string arg)
        {
            string filePath = Program.ProgPath + @"\" + arg;
            if (!File.Exists(filePath))
            {
                Console.WriteLine("Файла несуществует");
                return;
            }
            HashSet<string> cmds = ["help", "info", "show", "insert", "update", "remove", "clear", "save", "execute_script", "exit", "print_field_descending", "max", "min"];
            List<string> args = new List<string>(0);
            try
            {
                args = File.ReadAllText(filePath).Split(',').ToList();
            }
            catch (Exception ex) 
            {
                Console.WriteLine("Ошибка работы с файлом:" + ex.Message);
                return;
            }
            
            foreach(var item in args)
            {
                if (!cmds.Contains(item.Split(' ')[0]))
                {
                    Console.WriteLine("Скрипт-файл некорректен или содержит ошибки");
                    return;
                }
            }
            foreach(var item in args)
            {
                if (Program.Flag)
                {
                    Program.CommandProcessing(item);
                }
                else
                {
                    break;
                }
                
            }
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Скрипт-файл был выполнен");
            Console.ForegroundColor = ConsoleColor.White;
            
        }

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab_02
{
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
}

using System;
namespace ConsoleApp129
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var map = new Map();
            var menu = new Menu();
            menu.MainMenu();
            map.Map_generation();
            map.Drawing_the_map();
            int a = 0;
            while (a == 0)
            {
                Console.SetCursorPosition(0,26);
                switch (Console.ReadKey().Key)
                {
                    case ConsoleKey.UpArrow:
                        Console.Clear();
                        map.MovePersons(ConsoleKey.UpArrow);
                        map.MovePersons();
                        map.Drawing_the_map();
                        break;
                    case ConsoleKey.DownArrow:
                        Console.Clear();
                        map.MovePersons(ConsoleKey.DownArrow);
                        map.MovePersons();
                        map.Drawing_the_map();
                        break;
                    case ConsoleKey.LeftArrow:
                        Console.Clear();
                        map.MovePersons(ConsoleKey.LeftArrow);
                        map.MovePersons();
                        map.Drawing_the_map();
                        break;
                    case ConsoleKey.RightArrow:
                        Console.Clear();
                        map.MovePersons(ConsoleKey.RightArrow);
                        map.MovePersons();
                        map.Drawing_the_map();
                        break;
                    case ConsoleKey.Escape:
                        a++;
                        break;
                    
                }
            }
        }
    }
}
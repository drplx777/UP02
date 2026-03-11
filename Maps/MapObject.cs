using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConsoleApp129.Kazikk;

namespace ConsoleApp129
{
    internal abstract class MapObject
    {
        public abstract char Rendering_on_the_map();
    }

    internal class Wall : MapObject
    {
        
        public override char Rendering_on_the_map()
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            return '+';
        }
    }
    internal class Field : MapObject
    {
        public override char Rendering_on_the_map()
        {
            Console.ForegroundColor = ConsoleColor.DarkGreen;
            return '.';
        }
    }
    internal class Tree : MapObject
    {
        public override char Rendering_on_the_map()
        {
            Console.ForegroundColor = ConsoleColor.Green;
            return 'T';
        }
    }
    internal class HealthPoint : MapObject
    {
        public int index = 10;
        public override char Rendering_on_the_map()
        {
            Console.ForegroundColor = ConsoleColor.Red;
            return '❤';
        }
    }
    internal class Casino : MapObject
    {
        public override char Rendering_on_the_map()
        {
            Console.ForegroundColor = ConsoleColor.Magenta;
            return '♤';
        }
        public void Interaction()
        {
            Console.WriteLine("Вы в казино! \n 1. Играть \n Escape. Выйти");
            Kazik kazik = new Kazik();

            switch (Console.ReadKey().Key)
            {
                case ConsoleKey.D1:
                    Console.Clear();
                    kazik.Entry(); 
                    break;
                case ConsoleKey.Escape:
                    Console.Clear();
                    break;
            }
        }
    }
    internal class Door : MapObject
    {
        public override char Rendering_on_the_map()
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            return '█';
        }
    }
    
}

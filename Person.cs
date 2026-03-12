using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConsoleApp129;

namespace ConsoleApp129
{
    internal class Person : MapObject
    {
        int pointX;
        int pointY;

        public Person(int X,int Y)
        {
            pointX = X; pointY = Y; 
        }
        public override char Rendering_on_the_map()
        {
            return '☺';
        }
    }

    internal class Hero : Person
    {
        public int HP { get; set; }
        public int MaxHP { get; set; }
        public int Balance { get; set; }

        public int Damage {get; set; }
        public Hero(int X, int Y) : base(X, Y)
        {
            HP = 100;
            MaxHP = 1000;
            Balance = 1000;
            Damage = 10;

        }
        public override char Rendering_on_the_map()
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            return '☻';
        }
    }

    internal class Enemy : Person
    {
        public int HP {get; set;}
        public int Damage{get; set;}
        public Enemy(int X, int Y) : base(X, Y)
        {
            HP = 20;
            Damage = 10;
        }
        public override char Rendering_on_the_map()
        {
            Console.ForegroundColor = ConsoleColor.Red;
            return base.Rendering_on_the_map();
        }
    }
}

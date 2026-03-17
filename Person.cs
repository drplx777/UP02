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

        /// <summary>
        /// Создаёт персонажа в указанных координатах.
        /// </summary>
        /// <param name="X">Координата X (столбец).</param>
        /// <param name="Y">Координата Y (строка).</param>
        public Person(int X,int Y)
        {
            pointX = X; pointY = Y; 
        }

        /// <summary>
        /// Возвращает символ для отображения объекта на карте.
        /// Переопределяется в наследниках.
        /// </summary>
        /// <returns>Символ, представляющий персонажа на карте.</returns>
        public override char Rendering_on_the_map()
        {
            return '☺';
        }
    }

    internal class Hero : Person
    {
        public int HP { get; set; }
        public int Balance { get; set; }

        public int Damage {get; set; }

        /// <summary>
        /// Создаёт героя с начальными значениями HP, Balance и Damage.
        /// </summary>
        /// <param name="X">Координата X (столбец).</param>
        /// <param name="Y">Координата Y (строка).</param>
        public Hero(int X, int Y) : base(X, Y)
        {
            HP = 100;
            Balance = 1000;
            Damage = 10;

        }

        /// <summary>
        /// Возвращает символ для отображения героя на карте.
        /// Устанавливает желтый цвет текста перед отрисовкой.
        /// </summary>
        /// <returns>Символ, представляющий героя на карте.</returns>
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

        /// <summary>
        /// Создаёт врага с начальными значениями HP и Damage.
        /// </summary>
        /// <param name="X">Координата X (столбец).</param>
        /// <param name="Y">Координата Y (строка).</param>
        public Enemy(int X, int Y) : base(X, Y)
        {
            HP = 20;
            Damage = 10;
        }

        /// <summary>
        /// Возвращает символ для отображения врага на карте.
        /// Устанавливает красный цвет текста перед отрисовкой.
        /// </summary>
        /// <returns>Символ, представляющий врага на карте.</returns>
        public override char Rendering_on_the_map()
        {
            Console.ForegroundColor = ConsoleColor.Red;
            return base.Rendering_on_the_map();
        }
    }
}
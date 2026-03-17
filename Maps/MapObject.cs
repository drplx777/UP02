using System;
using System.Collections.Generic;
using ConsoleApp129.Kazikk;

namespace ConsoleApp129
{
    /// <summary>
    /// Базовый тип для всех объектов карты. Определяет метод для отрисовки символа.
    /// </summary>
    internal abstract class MapObject
    {
        /// <summary>
        /// Возвращает символ, используемый для отображения объекта на карте.
        /// </summary>
        /// <returns>Символ объекта.</returns>
        public abstract char Rendering_on_the_map();
    }

    internal class Wall : MapObject
    {
        /// <summary>Возвращает символ стены и устанавливает цвет.</summary>
        public override char Rendering_on_the_map()
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            return '+';
        }
    }
    internal class Field : MapObject
    {
        /// <summary>Возвращает символ поля и устанавливает цвет.</summary>
        public override char Rendering_on_the_map()
        {
            Console.ForegroundColor = ConsoleColor.DarkGreen;
            return '.';
        }
    }
    internal class Tree : MapObject
    {
        /// <summary>Возвращает символ дерева и устанавливает цвет.</summary>
        public override char Rendering_on_the_map()
        {
            Console.ForegroundColor = ConsoleColor.Green;
            return 'T';
        }
    }
    internal class HealthPoint : MapObject
    {
        public int index = 10;

        /// <summary>Возвращает символ аптечки и устанавливает цвет.</summary>
        public override char Rendering_on_the_map()
        {
            Console.ForegroundColor = ConsoleColor.Red;
            return '❤';
        }
    }
    internal class Casino : MapObject
    {
        /// <summary>Возвращает символ казино и устанавливает цвет.</summary>
        public override char Rendering_on_the_map()
        {
            Console.ForegroundColor = ConsoleColor.Magenta;
            return '♤';
        }

        /// <summary>Открывает взаимодействие с казино (вызов мини-игр).</summary>
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
        /// <summary>Возвращает символ двери и устанавливает цвет.</summary>
        public override char Rendering_on_the_map()
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            return '█';
        }
    }
    internal class Boss : Enemy
    {
        /// <summary>Количество жизней босса (итерации).</summary>
        public int Lives { get; set; } = 3;

        /// <summary>Создаёт босса в заданных координатах с увеличенными характеристиками.</summary>
        /// <param name="X">Координата X.</param>
        /// <param name="Y">Координата Y.</param>
        public Boss(int X, int Y) : base(X, Y)
        {
            HP = 100;
            Damage = 20;
            Lives = 3;
        }

        /// <summary>Возвращает символ босса и устанавливает цвет.</summary>
        public override char Rendering_on_the_map()
        {
            Console.ForegroundColor = ConsoleColor.Magenta;
            return '♕';
        }
    }
    
}
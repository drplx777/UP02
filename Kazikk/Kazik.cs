using System;
using System.Threading;
using System.Xml;
using ConsoleApp129.Exceptions;

namespace ConsoleApp129.Kazikk
{
    class Kazik
    {
        public Kazik()
        {}
        public void Entry()
        {
            Console.WriteLine("Добро пожаловать в казино! \n Для продолжения выберите игру:");
            Console.WriteLine("1.BlackJack \n 2.Рулетка \n 3.Колесо фортуны");
            switch (Console.ReadKey().Key)
            {
                case ConsoleKey.D1:
                BlackJack();
                break;
                case ConsoleKey.D2:
                Roulette();
                break;
                case ConsoleKey.D3:
                WheelOfFortune();
                break;
            }
        }
        public void BlackJack()
        {
            int Pcards = 0;
            int Dcards = 0;
            Console.WriteLine("Добро Пожаловать в игру");
            Console.WriteLine("Для начала игры нажмите любую клавишу");
            Console.ReadKey();
            Random random = new Random();
            while (true)
            {
                Console.WriteLine("Диллер раздаёт карты...");
                Pcards = random.Next(1, 11) + random.Next(1, 11);
                Dcards = random.Next(1, 11) + random.Next(1, 11);
                Thread.Sleep(1000);
                Console.WriteLine($"Ваши карты: {Pcards}");
                Console.WriteLine($"Карты дилера: {Dcards}");
                
                while(Pcards <= 21)
                {
                    Console.WriteLine("Хотите взять еще одну карту? \n 1. Да \n 2. Нет");
                    if (Console.ReadKey().Key == ConsoleKey.D2)
                    {
                        break;
                    }
                    switch (Console.ReadKey().Key)
                    {
                        case ConsoleKey.D1:
                            Pcards += random.Next(1, 11);
                            Console.WriteLine($"Ваши карты: {Pcards}");
                            Console.WriteLine($"Карты дилера: {Dcards}");
                        break;
                        default:
                            Console.WriteLine("Такого варианта нет");
                        break;
                    }
                }
                if (Pcards > 21)
                {
                    Console.WriteLine("Вы проиграли! Перебор.");
                }
                else if (Dcards > 21)
                {
                    Console.WriteLine("Вы выиграли! Дилер перебрал.");
                }
                if (Pcards > Dcards)
                {
                    Console.WriteLine("Вы выиграли!");
                }
                if (Pcards < Dcards)
                {
                    Console.WriteLine("Вы проиграли!");
                }
                else
                {
                    Console.WriteLine("Ничья!");
                }
                Console.WriteLine("Хотите сыграть ещё раз? \n Для продолжения нажмите любую клавишу \n Для выхода нажмите Escape");
                if (Console.ReadKey().Key == ConsoleKey.Escape)
                {
                    break;
                }
            }
            Console.Clear();
        }
        public void Roulette()
        {
            char[]pics = new char[]{'❤', '♕', '✦', '➆'};
            int index = 0;
            Random rnd = new Random();
            char[]result = new char[3];
            Console.WriteLine("Добро Пожаловать в игру");
            Console.WriteLine("Для начала игры нажмите любую клавишу");
            Console.ReadKey();
            while (true)
            {
                for(int i = 0; i < 10; i++)
                {
                    index = rnd.Next(0,3);
                    result[0] = pics[index];
                    index = rnd.Next(0,3);
                    result[1] = pics[index];
                    index = rnd.Next(0,3);
                    result[2] = pics[index];
                    for(int j = 0; j < result.Length; j++)
                    {
                        Thread.Sleep(350);
                        Console.Write(result[j]);
                    }
                    Console.Clear();
                    
                }
                for(int i = 0; i < result.Length; i++)
                {
                    Console.Write(result[i]);
                }
                if (result[0] == result[1] && result[1] == result[2])
                {
                    Console.WriteLine();
                    Console.WriteLine("Вы выйграли! \nНажмите на любую клавишу, чтобы попробовать еще раз \n Escape для выхода");

                }
                else if(result[0] != result[1] || result[0] != result[2] || result[1] != result[2])
                {
                    Console.WriteLine("Вы проиграли \nНажмите на любую клавишу, чтобы попробовать еще раз \nEscape для выхода");
                }
                if(Console.ReadKey().Key == ConsoleKey.Escape)
                {
                    break;
                }
                Console.Clear();
                
            }
        
        }
        static void WheelOfFortune()
        {
            Console.WriteLine("Добро пожаловать в игру «Колесо фортуны»!");
            Console.WriteLine("Секторы колеса: 10–100 очков (шаг 10), БАНКРОТ, ПРОПУСК ХОДА");
            Console.WriteLine("Для выхода введите 'exit'.\n");

            var wheel = new Wheel();
            var random = new Random();
            int totalScore = 0;
            bool gameRunning = true;

            while (gameRunning)
            {
                Console.WriteLine($"Текущий счёт: {totalScore}");
                Console.Write("Нажмите '1' чтобы крутить колесо, или 'Escape' для выхода: ");
                var input = Console.ReadKey().Key;

                if (input == ConsoleKey.Escape)
                {
                    Console.WriteLine("Спасибо за игру! До свидания.");
                    gameRunning = false;
                    break;
                }
                else if (input == ConsoleKey.D1)
                {
                    var segment = wheel.Spin(random);
                    Console.Write($"Колесо остановилось на секторе: ");

                    switch (segment.Type)
                    {
                        case SegmentType.Points:
                            Console.WriteLine($"{segment.Value} очков");
                            totalScore += segment.Value;
                            break;
                        case SegmentType.Bankrupt:
                            Console.WriteLine("БАНКРОТ! Все очки сгорают.");
                            totalScore = 0;
                            break;
                        case SegmentType.SkipTurn:
                            Console.WriteLine("ПРОПУСК ХОДА. Вы ничего не получаете.");
                            break;
                    }

                    if (totalScore >= 1000)
                    {
                        Console.WriteLine($"\nПоздравляем! Вы набрали {totalScore} очков и победили!");
                        gameRunning = false;
                    }
                }
                else
                {
                    Console.WriteLine("Неизвестная команда. Попробуйте 'spin' или 'exit'.");
                }

                Console.WriteLine();
            }
        }

        
    }
}
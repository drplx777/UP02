using System;

namespace ConsoleApp129
{
    class Menu
    {
        public void InteractionMenu()
        {
            Console.WriteLine("Нажмите Е для взаимодействия");
        }
        public void MainMenu()
        {
            int a = 0;
            while (a == 0)
            {
                Console.WriteLine("Главное меню: \n 1. Начать игру \n 2. Управление \n 3. Настройки \n 4. Настройка персонажа \n Escape. Выход");

                switch (Console.ReadKey().Key)
                {
                    case ConsoleKey.D1:
                        Console.Clear();
                        a++;
                        break;
                    case ConsoleKey.D2:
                        Console.Clear();
                        int b = 0;
                        while (b == 0){
                            Console.WriteLine("Управление: \n Стрелки - движение персонажа \n Escape - Выход в главное меню");
                            if (Console.ReadKey().Key == ConsoleKey.Escape)
                            {
                                b++;
                            }
                        }
                        break;
                    case ConsoleKey.D3:
                        Console.Clear();
                        break;
                    case ConsoleKey.D4:
                        Console.Clear();
                        break;
                    case ConsoleKey.Escape:
                        Environment.Exit(0);
                        break;
                }
            }
        }
    }
}
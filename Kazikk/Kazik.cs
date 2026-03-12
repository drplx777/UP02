using System;
using System.Threading;

namespace ConsoleApp129.Kazikk
{
    class Kazik
    {
        public Kazik() { }

        public void Entry()
        {
            var hero = Map.Current?.FindHero();
            if (hero == null)
            {
                Console.WriteLine("Ошибка: герой не найден. Казино недоступно.");
                Console.WriteLine("Убедитесь, что в классе Map есть статическое свойство Current и оно установлено.");
                Console.WriteLine("Нажмите любую клавишу...");
                Console.ReadKey(true);
                return;
            }

            while (true)
            {
                Console.Clear();
                Console.WriteLine("Добро пожаловать в казино! \nВыберите игру:");
                Console.WriteLine("1. BlackJack");
                Console.WriteLine("2. Рулетка (слоты)");
                Console.WriteLine("3. Колесо фортуны");
                Console.WriteLine("Escape. Выйти из казино");
                var key = Console.ReadKey(true).Key;
                switch (key)
                {
                    case ConsoleKey.D1:
                        BlackJack(hero);
                        break;
                    case ConsoleKey.D2:
                        Roulette(hero);
                        break;
                    case ConsoleKey.D3:
                        WheelOfFortune(hero);
                        break;
                    case ConsoleKey.Escape:
                        Console.Clear();
                        return;
                    default:
                        Console.WriteLine("Неизвестная команда. Повторите.");
                        Thread.Sleep(700);
                        break;
                }
            }
        }

        private void BlackJack(Hero hero)
        {
            var rnd = new Random();

            Console.Clear();
            Console.WriteLine("=== BlackJack ===");
            Console.WriteLine($"Ваш баланс: {hero.Balance}");
            int bet = AskBet(hero);
            if (bet <= 0) return;

            while (true)
            {
                Console.Clear();
                Console.WriteLine("Дилер раздаёт карты...");
                int Pcards = rnd.Next(1, 11) + rnd.Next(1, 11);
                int Dcards = rnd.Next(1, 11) + rnd.Next(1, 11);

                Console.WriteLine($"Ваши карты: {Pcards}");
                Console.WriteLine($"Карты дилера (скрыты): ?");

                bool stand = false;
                while (!stand && Pcards <= 21)
                {
                    Console.WriteLine("Взять ещё? 1 - Да, 2 - Нет");
                    var k = Console.ReadKey(true).Key;
                    if (k == ConsoleKey.D1)
                    {
                        Pcards += rnd.Next(1, 11);
                        Console.WriteLine($"Вы взяли. Ваши карты: {Pcards}");
                    }
                    else if (k == ConsoleKey.D2)
                    {
                        stand = true;
                    }
                    else
                    {
                        Console.WriteLine("Неправильный ввод.");
                    }
                }

                Console.WriteLine($"Карты дилера: {Dcards}");

                if (Pcards > 21)
                {
                    Console.WriteLine("Перебор! Вы проиграли.");
                    hero.Balance -= bet;
                }
                else if (Dcards > 21)
                {
                    Console.WriteLine("Дилер перебрал — вы выиграли!");
                    hero.Balance += bet;
                }
                else if (Pcards > Dcards)
                {
                    Console.WriteLine("Вы выиграли!");
                    hero.Balance += bet;
                }
                else if (Pcards < Dcards)
                {
                    Console.WriteLine("Вы проиграли!");
                    hero.Balance -= bet;
                }
                else
                {
                    Console.WriteLine("Ничья — ставка возвращается.");
                    // ничего не делаем
                }

                Console.WriteLine($"\nТекущий баланс: {hero.Balance}");
                Console.WriteLine("Сыграть ещё? Любая клавиша — ещё, Escape — выйти в казино");
                var nk = Console.ReadKey(true).Key;
                if (nk == ConsoleKey.Escape) break;
            }
            Console.Clear();
        }

        private void Roulette(Hero hero)
        {
            char[] pics = new char[] { '❤', '♕', '✦', '➆' };
            var rnd = new Random();

            Console.Clear();
            Console.WriteLine("=== Рулетка (слоты) ===");
            Console.WriteLine($"Ваш баланс: {hero.Balance}");
            int bet = AskBet(hero);
            if (bet <= 0) return;

            while (true)
            {
                Console.Clear();
                Console.WriteLine("Крутим барабаны...");
                char[] result = new char[3];

                for (int t = 0; t < 10; t++)
                {
                    for (int i = 0; i < 3; i++)
                        result[i] = pics[rnd.Next(pics.Length)];

                    foreach (var c in result)
                    {
                        Console.Write(c);
                        Thread.Sleep(120);
                    }
                    Console.WriteLine();
                    Console.Clear();
                }

                for (int i = 0; i < 3; i++)
                    result[i] = pics[rnd.Next(pics.Length)];

                Console.WriteLine($"{result[0]} {result[1]} {result[2]}");

                if (result[0] == result[1] && result[1] == result[2])
                {
                    int win = bet * 3;
                    hero.Balance += win;
                    Console.WriteLine($"Три в ряд! Вы выиграли {win} монет!");
                }
                else
                {
                    hero.Balance -= bet;
                    Console.WriteLine($"Увы, вы проиграли {bet} монет.");
                }

                Console.WriteLine($"Текущий баланс: {hero.Balance}");
                Console.WriteLine("Ещё раз — любая клавиша, Escape — выйти в казино.");
                var k = Console.ReadKey(true).Key;
                if (k == ConsoleKey.Escape) break;
            }

            Console.Clear();
        }

        private void WheelOfFortune(Hero hero)
        {
            var wheel = new Wheel();
            var rnd = new Random();

            while (true)
            {
                Console.Clear();

                Console.ForegroundColor = ConsoleColor.Magenta;
                Console.WriteLine("===== КОЛЕСО ФОРТУНЫ =====");
                Console.ResetColor();

                Console.WriteLine($"HP: {hero.HP}/{hero.MaxHP}");
                Console.WriteLine($"Урон: {hero.Damage}");
                Console.WriteLine();
                Console.WriteLine("1 — Крутить колесо");
                Console.WriteLine("Escape — Выйти");

                var key = Console.ReadKey(true).Key;

                if (key == ConsoleKey.Escape)
                    break;

                if (key != ConsoleKey.D1){
                    hero.Balance -= 50;
                    continue;
                }
                Console.Clear();
                Console.WriteLine("Колесо вращается...");

                Thread.Sleep(1000);

                var seg = wheel.Spin(rnd);

                Console.Clear();
                Console.WriteLine("Колесо остановилось!");

                switch (seg.Type)
                {
                    case SegmentType.Heal:

                        hero.HP += seg.Value;

                        if (hero.HP > hero.MaxHP)
                            hero.HP = hero.MaxHP;

                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine($"Вы восстановили {seg.Value} HP!");
                        break;

                    case SegmentType.DamageUp:

                        hero.Damage += seg.Value;

                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine($"Ваш урон увеличен на {seg.Value}!");
                        break;

                    case SegmentType.MaxHPUp:

                        hero.MaxHP += seg.Value;
                        hero.HP += seg.Value;

                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.WriteLine($"Максимальное HP увеличено на {seg.Value}!");
                        break;

                    case SegmentType.Bankrupt:

                        hero.HP -= 10;

                        Console.ForegroundColor = ConsoleColor.DarkRed;
                        Console.WriteLine("Неудача! Вы потеряли 10 HP.");
                        break;

                    case SegmentType.SkipTurn:

                        Console.ForegroundColor = ConsoleColor.Gray;
                        Console.WriteLine("Пропуск! Ничего не произошло.");
                        break;
                }

                Console.ResetColor();

                Console.WriteLine();
                Console.WriteLine("Нажмите любую клавишу...");
                Console.ReadKey();
            }
    }
        public void BossBlackjack(Hero hero, Boss boss)
        {
            var rnd = new Random();

            Console.Clear();
            Console.WriteLine($"Вы вступили в поединок с БОССОМ! (Осталось жизней: {boss.Lives})");
            Console.WriteLine("Правила просты: раунд 21. Выигрыш — отнимает одну жизнь у босса. Проигрыш — вы получаете урон.");
            Console.WriteLine("Нажмите любую клавишу чтобы начать раунд...");
            Console.ReadKey(true);

            while (boss.Lives > 0 && hero.HP > 0)
            {
                Console.Clear();
                Console.WriteLine($"Босс — жизни: {boss.Lives}. Ваш HP: {hero.HP}/{hero.MaxHP}");
                int Pcards = rnd.Next(1, 11) + rnd.Next(1, 11);
                int Dcards = rnd.Next(1, 11) + rnd.Next(1, 11);

                Console.WriteLine($"Ваши карты: {Pcards}");
                Console.WriteLine($"Карты босса (скрыты): ?");

                bool stand = false;
                while (!stand && Pcards <= 21)
                {
                    Console.WriteLine("Взять ещё? 1 - Да, 2 - Нет");
                    var k = Console.ReadKey(true).Key;
                    if (k == ConsoleKey.D1)
                    {
                        Pcards += rnd.Next(1, 11);
                        Console.WriteLine($"Вы взяли. Ваши карты: {Pcards}");
                    }
                    else if (k == ConsoleKey.D2)
                    {
                        stand = true;
                    }
                    else
                    {
                        Console.WriteLine("Неправильный ввод.");
                    }
                }

                Console.WriteLine($"Карты босса: {Dcards}");

                if (Pcards > 21)
                {
                    Console.WriteLine("Перебор! Вы проиграли раунд.");
                    hero.HP -= boss.Damage;
                    Console.WriteLine($"Вы получили {boss.Damage} урона.");
                }
                else if (Dcards > 21)
                {
                    Console.WriteLine("Босс перебрал — вы выиграли раунд!");
                    boss.Lives--;
                }
                else if (Pcards > Dcards)
                {
                    Console.WriteLine("Вы выиграли раунд!");
                    boss.Lives--;
                }
                else if (Pcards < Dcards)
                {
                    Console.WriteLine("Вы проиграли раунд!");
                    hero.HP -= boss.Damage;
                    Console.WriteLine($"Вы получили {boss.Damage} урона.");
                }
                else
                {
                    Console.WriteLine("Ничья — ничего не происходит.");
                }

                if (hero.HP <= 0)
                {
                    Console.WriteLine("Вы погибли в бою с боссом...");
                    Console.WriteLine("Нажмите любую клавишу.");
                    Console.ReadKey(true);
                    Environment.Exit(0);
                }

                if (boss.Lives <= 0)
                {
                    Console.WriteLine("Босс повержен!");
                    Console.WriteLine("Нажмите любую клавишу чтобы вернуться в игру.");
                    Console.ReadKey(true);
                    break;
                }

                Console.WriteLine($"Осталось жизней босса: {boss.Lives}. Ваш HP: {hero.HP}/{hero.MaxHP}");
                Console.WriteLine("Сыграть следующий раунд? Любая клавиша — продолжить, Escape — отступить (вернётесь на место).");
                var nk = Console.ReadKey(true).Key;
                if (nk == ConsoleKey.Escape) break;
            }

            Console.Clear();
        }

        private int AskBet(Hero hero)
        {
            while (true)
            {
                Console.Write($"\nВведите ставку (баланс {hero.Balance}) или 0 чтобы отменить: ");
                string s = Console.ReadLine();
                if (!int.TryParse(s, out int bet) || bet < 0)
                {
                    Console.WriteLine("Неверная ставка. Введите положительное число или 0.");
                    continue;
                }

                if (bet == 0) return 0;
                if (bet > hero.Balance)
                {
                    Console.WriteLine("У вас недостаточно денег для такой ставки.");
                    continue;
                }

                return bet;
            }
        }
    }
}
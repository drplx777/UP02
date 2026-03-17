using System;
using System.Threading;
using ConsoleApp129;

namespace ConsoleApp129.Kazikk
{
    /// <summary>
    /// Основной класс, реализующий интерфейс казино и мини-игры.
    /// Также содержит реализации боёв с обычным и финальным боссом.
    /// </summary>
    internal class Kazik
    {
        /// <summary>Создаёт логический объект казино.</summary>
        public Kazik() { }

        /// <summary>
        /// Точка входа в интерфейс казино: выбор игр (BlackJack, Рулетка, Колесо фортуны).
        /// </summary>
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

        /// <summary>
        /// Простая версия игры BlackJack для героя с ставками и результатом изменения баланса.
        /// </summary>
        /// <param name="hero">Экземпляр героя, участвующего в игре.</param>
        public void BlackJack(Hero hero)
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

        /// <summary>
        /// Простая слот-рулетка: три символа, выигрыш при трёх одинаковых символах.
        /// </summary>
        /// <param name="hero">Экземпляр героя, участвующего в игре.</param>
        public void Roulette(Hero hero)
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

        /// <summary>
        /// Интерфейс колеса фортуны: снимает 50 монет и применяет эффект выбранного сегмента.
        /// </summary>
        /// <param name="hero">Экземпляр героя.</param>
        public void WheelOfFortune(Hero hero)
        {
            var wheel = new Wheel();
            var rnd = new Random();

            while (true)
            {
                Console.Clear();

                Console.ForegroundColor = ConsoleColor.Magenta;
                Console.WriteLine("===== КОЛЕСО ФОРТУНЫ =====");
                Console.WriteLine($"Баланс: {hero.Balance}");
                Console.WriteLine("Стоимость: 50 монет");
                Console.ResetColor();

                Console.WriteLine($"HP: {hero.HP}/");
                Console.WriteLine($"Урон: {hero.Damage}");
                Console.WriteLine();
                Console.WriteLine("1 — Крутить колесо");
                Console.WriteLine("Escape — Выйти");

                var key = Console.ReadKey(true).Key;

                if (key == ConsoleKey.Escape)
                    break;

                if (key != ConsoleKey.D1){
                    continue;
                }
                Console.Clear();
                Console.WriteLine("Колесо вращается...");
                hero.Balance -= 50;

                Thread.Sleep(1000);

                var seg = wheel.Spin(rnd);

                Console.Clear();
                Console.WriteLine("Колесо остановилось!");

                switch (seg.Type)
                {
                    case SegmentType.Heal:

                        hero.HP += seg.Value;

                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine($"Вы восстановили {seg.Value} HP!");
                        break;

                    case SegmentType.DamageUp:

                        hero.Damage += seg.Value;

                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine($"Ваш урон увеличен на {seg.Value}!");
                        break;

                    case SegmentType.DamageDown:

                        hero.Damage -= seg.Value;

                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.WriteLine($"Ваш урон уменьщен на {seg.Value}!");
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
                    case SegmentType.HardMode:
                        hero.HP = 1;
                        Console.ForegroundColor = ConsoleColor.DarkRed;
                        Console.WriteLine("Хардмод! Ваш HP снижен до 1");
                        break;
                }

                Console.ResetColor();

                Console.WriteLine();
                Console.WriteLine("Нажмите любую клавишу...");
                Console.ReadKey();
            }
        }

        /// <summary>
        /// Поединок с обычным боссом: несколько раундов в стиле 21 (mini-blackjack). При победе — лишняя жизнь босса теряется.
        /// </summary>
        /// <param name="hero">Экземпляр героя.</param>
        /// <param name="boss">Экземпляр обычного босса.</param>
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
                Console.WriteLine($"Босс — жизни: {boss.Lives}. Ваш HP: {hero.HP}");
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

                Console.WriteLine($"Осталось жизней босса: {boss.Lives}. Ваш HP: {hero.HP}");
                Console.WriteLine("Сыграть следующий раунд? Любая клавиша — продолжить, Escape — отступить (вернётесь на место).");
                var nk = Console.ReadKey(true).Key;
                if (nk == ConsoleKey.Escape) break;
            }

            Console.Clear();
        }

        /// <summary>
        /// Особая боевая система с финальным боссом.
        /// Каждый ход босса он крутит своё колесо фортуны и получает временные эффекты (увеличение урона, щит, реген, двойной удар).
        /// Игрок ходит первым: имеет выбор атаковать или лечиться.
        /// </summary>
        /// <param name="hero">Экземпляр героя.</param>
        /// <param name="bossObj">Экземпляр финального босса (FinalBoss).</param>
        public void BossFinalFight(Hero hero, FinalBoss bossObj)
        {
            var rnd = new Random();
            var wheel = new FinalBossWheel();

            Console.Clear();
            Console.WriteLine("!!! ВЫ ВСТУПИЛИ В БОЙ С ГЛАВНЫМ БОССОМ !!!");
            Console.WriteLine("Уникальная боевая система: каждый ход босс крутит колесо и получает временные улучшения.");
            Console.WriteLine("Выберите действия мудро. Нажмите любую клавишу чтобы начать...");
            Console.ReadKey(true);

            while (hero.HP > 0 && bossObj.HP > 0)
            {
                // показываем состояние
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine($"Ваш HP: {hero.HP}  | Урон: {hero.Damage}");
                Console.ForegroundColor = ConsoleColor.Magenta;
                Console.WriteLine($"Босс HP: {bossObj.HP}");
                Console.ResetColor();
                Console.WriteLine();

                // --- ХОД ГЕРОЯ ---
                Console.WriteLine("Ваш ход: 1 - атаковать, 2 - лечь (восстановить 25 HP), Escape - отступить");
                var key = Console.ReadKey(true).Key;
                if (key == ConsoleKey.Escape)
                {
                    Console.WriteLine("Вы отвернулись от боя и вернулись на своё место...");
                    Thread.Sleep(1000);
                    break;
                }
                else if (key == ConsoleKey.D2)
                {
                    hero.HP += 25;
                    Console.WriteLine("Вы восстановили 25 HP.");
                }
                else // атака по умолчанию
                {
                    int heroDamage = hero.Damage;
                    // учитываем щит босса (уменьшает входящий урон в процентах)
                    int reduction = bossObj.GetIncomingDamageReductionPercent();
                    int damageAfterReduction = heroDamage - (heroDamage * reduction / 100);
                    damageAfterReduction = Math.Max(0, damageAfterReduction);
                    bossObj.HP -= damageAfterReduction;
                    Console.WriteLine($"Вы нанесли {damageAfterReduction} урона (щиты и эффекты учтены).");
                }

                // проверяем, убит ли босс
                if (bossObj.HP <= 0)
                {
                    Console.WriteLine("Босс повержен!");
                    Console.WriteLine("Нажмите любую клавишу...");
                    Console.ReadKey(true);
                    break;
                }

                // --- ХОД БОССА ---
                Console.WriteLine("\nХод босса: он крутит колесо фортуны...");
                Thread.Sleep(900);
                var seg = wheel.Spin(rnd);
                bossObj.ApplyEffect(seg);

                // отображаем эффект
                switch (seg.Type)
                {
                    case FinalBossEffectType.DamageUp:
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine($"Босс усилился! +{seg.Value} к урону на {seg.TurnsRemaining} ход(а/ов).");
                        Console.ResetColor();
                        break;
                    case FinalBossEffectType.Shield:
                        Console.ForegroundColor = ConsoleColor.Cyan;
                        Console.WriteLine($"Босс поднял щит! -{seg.Value}% к входящему урону на {seg.TurnsRemaining} ход(а/ов).");
                        Console.ResetColor();
                        break;
                    case FinalBossEffectType.Regen:
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine($"Босс начинает регенерировать по {seg.Value} HP в течение {seg.TurnsRemaining} ход(а/ов).");
                        Console.ResetColor();
                        break;
                    case FinalBossEffectType.DoubleStrike:
                        Console.ForegroundColor = ConsoleColor.Magenta;
                        Console.WriteLine("Босс подготовил двойной удар в этом ходу!");
                        Console.ResetColor();
                        break;
                    case FinalBossEffectType.Nothing:
                        Console.WriteLine("Колесо ничего не дало...");
                        break;
                }

                // применяем эффекты (например реген)
                bossObj.TickEffects();

                // босс наносит урон (учитываем бонусы урона)
                int totalDamage = bossObj.Damage + bossObj.GetDamageBonus();
                int attacks = bossObj.HasDoubleStrike() ? 2 : 1;
                for (int a = 0; a < attacks; a++)
                {
                    hero.HP -= totalDamage;
                    Console.WriteLine($"Босс атакует и наносит {totalDamage} урона.");
                    if (hero.HP <= 0) break;
                    Thread.Sleep(300);
                }

                if (hero.HP <= 0)
                {
                    Console.ForegroundColor = ConsoleColor.DarkRed;
                    Console.WriteLine("Вы погибли в бою с Главным Боссом...");
                    Console.ResetColor();
                    Console.WriteLine("Нажмите любую клавишу.");
                    Console.ReadKey(true);
                    Environment.Exit(0);
                }

                // уменьшаем таймеры эффектов на конце хода (ещё один Tick — чтобы эффекты корректно считались)
                bossObj.TickEffects();

                Console.WriteLine("\nНажмите любую клавишу, чтобы продолжить следующий раунд...");
                Console.ReadKey(true);
            }

            Console.Clear();
        }

        /// <summary>
        /// Просит пользователя ввести ставку для азартных игр. Валидация входа и проверка баланса.
        /// </summary>
        /// <param name="hero">Экземпляр героя, у которого запрашивается ставка.</param>
        /// <returns>Сумма ставки (положительное целое) или 0 — если отмена.</returns>
        public int AskBet(Hero hero)
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
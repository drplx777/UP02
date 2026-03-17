using System;
namespace ConsoleApp129
{
    internal class Shop : MapObject
    {
        /// <summary>
        /// Возвращает символ магазина для отображения на карте.
        /// </summary>
        /// <returns>Символ, представляющий магазин.</returns>
        public override char Rendering_on_the_map()
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            return '$';
        }

        /// <summary>
        /// Открывает меню магазина и обрабатывает покупки игрока.
        /// Ищет текущую карту и героя; если герой не найден — выводит ошибку.
        /// </summary>
        public void Interaction()
        {

            var map = Map.Current;
            var hero = map?.FindHero();

            if (hero == null)
            {
                Console.WriteLine("Ошибка: герой не найден. Невозможно открыть магазин.");
                Console.WriteLine("Нажмите любую клавишу...");
                Console.ReadKey(true);
                return;
            }

            bool keepRunning = true;
            while (keepRunning)
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("=== Магазин ===");
                Console.ResetColor();
                Console.WriteLine($"Ваш баланс: {hero.Balance} монет");
                Console.WriteLine($"HP: {hero.HP}");
                Console.WriteLine($"Урон: {hero.Damage}\n");

                Console.WriteLine("1. Восстановить 20 HP — 50 монет");
                Console.WriteLine("2. Добавить 300 HP — 300 монет");
                Console.WriteLine("3. Улучшить урон +5 — 200 монет");
                Console.WriteLine("Escape. Выйти из магазина");

                var key = Console.ReadKey(true).Key;
                switch (key)
                {
                    case ConsoleKey.D1:
                        TryBuy(hero, price: 50, onBuy: () =>
                        {
                            hero.HP += 20;
                            Console.WriteLine("Вы получили +20 HP.");
                        });
                        break;

                    case ConsoleKey.D2:
                        TryBuy(hero, price: 300, onBuy: () =>
                        {
                            hero.HP += 300;
                            Console.WriteLine("+300 HP добавлено.");
                        });
                        break;

                    case ConsoleKey.D3:
                        TryBuy(hero, price: 200, onBuy: () =>
                        {
                            hero.Damage += 5;
                            Console.WriteLine("Урон увеличен на +5.");
                        });
                        break;

                    case ConsoleKey.Escape:
                        keepRunning = false;
                        break;

                    default:
                        Console.WriteLine("Неизвестная команда.");
                        break;
                }

                if (keepRunning)
                {
                    Console.WriteLine("\nНажмите любую клавишу, чтобы продолжить...");
                    Console.ReadKey(true);
                }
            }

            Console.Clear();
        }

        /// <summary>
        /// Пытается выполнить покупку: проверяет баланс, снимает цену и вызывает действие покупки.
        /// </summary>
        /// <param name="hero">Экземпляр героя, совершающего покупку.</param>
        /// <param name="price">Стоимость покупки в монетах.</param>
        /// <param name="onBuy">Делегат, выполняющий действие при успешной покупке.</param>
        private void TryBuy(Hero hero, int price, Action onBuy)
        {
            if (hero.Balance < price)
            {
                Console.WriteLine($"У вас недостаточно денег. Нужно: {price}, есть: {hero.Balance}");
                return;
            }

            hero.Balance -= price;
            try
            {
                onBuy?.Invoke();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Ошибка при выполнении покупки: " + ex.Message);
            }
        }
    }
}
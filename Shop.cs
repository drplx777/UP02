using System;
using System.Collections.Generic;
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

            // Создаём список стратегий (товаров)
            var items = new Dictionary<ConsoleKey, IShopItemStrategy>
            {
                { ConsoleKey.D1, new HealSmallStrategy() },
                { ConsoleKey.D2, new HealBigStrategy() },
                { ConsoleKey.D3, new DamageUpStrategy() }
            };

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

                // Выводим товары из стратегий
                foreach (var item in items)
                {
                    Console.WriteLine($"{item.Key.ToString().Replace("D", "")}. {item.Value.Description}");
                }

                Console.WriteLine("Escape. Выйти из магазина");

                var key = Console.ReadKey(true).Key;

                if (key == ConsoleKey.Escape)
                {
                    keepRunning = false;
                    continue;
                }

                if (items.TryGetValue(key, out var strategy))
                {
                    TryBuy(hero, strategy.Price, () => strategy.Apply(hero));
                }
                else
                {
                    Console.WriteLine("Неизвестная команда.");
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
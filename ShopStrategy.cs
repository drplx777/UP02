using System;

namespace ConsoleApp129
{
    /// <summary>
    /// Интерфейс стратегии покупки в магазине.
    /// Определяет цену и действие при покупке.
    /// </summary>
    internal interface IShopItemStrategy
    {
        /// <summary>Цена товара.</summary>
        int Price { get; }

        /// <summary>Описание товара (для меню).</summary>
        string Description { get; }

        /// <summary>
        /// Применяет эффект покупки к герою.
        /// </summary>
        /// <param name="hero">Герой.</param>
        void Apply(Hero hero);
    }

    /// <summary>Стратегия восстановления 20 HP.</summary>
    internal class HealSmallStrategy : IShopItemStrategy
    {
        public int Price => 50;
        public string Description => "Восстановить 20 HP — 50 монет";

        public void Apply(Hero hero)
        {
            hero.HP += 20;
            Console.WriteLine("Вы получили +20 HP.");
        }
    }

    /// <summary>Стратегия восстановления 300 HP.</summary>
    internal class HealBigStrategy : IShopItemStrategy
    {
        public int Price => 300;
        public string Description => "Добавить 300 HP — 300 монет";

        public void Apply(Hero hero)
        {
            hero.HP += 300;
            Console.WriteLine("+300 HP добавлено.");
        }
    }

    /// <summary>Стратегия увеличения урона.</summary>
    internal class DamageUpStrategy : IShopItemStrategy
    {
        public int Price => 200;
        public string Description => "Улучшить урон +5 — 200 монет";

        public void Apply(Hero hero)
        {
            hero.Damage += 5;
            Console.WriteLine("Урон увеличен на +5.");
        }
    }
}
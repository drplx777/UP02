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

    /// <summary>
    /// Финальный (главный) босс игры с уникальной боевой системой и временными эффектами.
    /// </summary>
    internal class FinalBoss : Enemy
    {
        /// <summary>Список активных эффектов у босса.</summary>
        public readonly List<Kazikk.FinalBossEffect> ActiveEffects = new List<Kazikk.FinalBossEffect>();

        /// <summary>Создаёт финального босса с увеличенными HP/уроном.</summary>
        /// <param name="X">Координата X.</param>
        /// <param name="Y">Координата Y.</param>
        public FinalBoss(int X, int Y) : base(X, Y)
        {
            HP = 350;
            Damage = 25;
            // финальный босс умирает при падении HP до 0
        }

        /// <summary>Возвращает символ финального босса и устанавливает цвет.</summary>
        public override char Rendering_on_the_map()
        {
            Console.ForegroundColor = ConsoleColor.Magenta;
            return '♛';
        }

        /// <summary>
        /// Применяет эффект к боссу (добавляет в список активных эффектов).
        /// </summary>
        /// <param name="effect">Эффект для применения.</param>
        public void ApplyEffect(Kazikk.FinalBossEffect effect)
        {
            ActiveEffects.Add(effect);
        }

        /// <summary>
        /// Выполняет шаг времени для активных эффектов: уменьшает оставшиеся ходы, применяет реген и удаляет просроченные эффекты.
        /// Должен вызываться в начале или в конце хода босса.
        /// </summary>
        public void TickEffects()
        {
            for (int i = ActiveEffects.Count - 1; i >= 0; i--)
            {
                var e = ActiveEffects[i];
                // регенерация применяется каждый ход, если есть соответствующий эффект
                if (e.Type == Kazikk.FinalBossEffectType.Regen)
                {
                    this.HP += e.Value;
                }

                e.TurnsRemaining--;
                if (e.TurnsRemaining <= 0)
                {
                    ActiveEffects.RemoveAt(i);
                }
            }
        }

        /// <summary>
        /// Вычисляет дополнительный урон босса на основе активных эффектов (например, DamageUp).
        /// </summary>
        /// <returns>Дополнительный урон (int) к базовому Damage.</returns>
        public int GetDamageBonus()
        {
            int bonus = 0;
            foreach (var e in ActiveEffects)
            {
                if (e.Type == Kazikk.FinalBossEffectType.DamageUp)
                    bonus += e.Value;
            }
            return bonus;
        }

        /// <summary>
        /// Вычисляет процент уменьшения входящего урона от героя (например, щит).
        /// </summary>
        /// <returns>Значение в диапазоне 0..100 — процент уменьшения урона.</returns>
        public int GetIncomingDamageReductionPercent()
        {
            int reduce = 0;
            foreach (var e in ActiveEffects)
            {
                if (e.Type == Kazikk.FinalBossEffectType.Shield)
                    reduce += e.Value; // e.Value содержит проценты
            }
            return Math.Min(90, reduce); // ограничение
        }

        /// <summary>
        /// Проверяет, есть ли эффект DoubleStrike (что даёт двойную атаку).
        /// </summary>
        /// <returns>True, если эффект активен.</returns>
        public bool HasDoubleStrike()
        {
            foreach (var e in ActiveEffects)
                if (e.Type == Kazikk.FinalBossEffectType.DoubleStrike) return true;
            return false;
        }
    }
}
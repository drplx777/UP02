using System;
using System.Collections.Generic;

namespace ConsoleApp129.Kazikk
{
    /// <summary>
    /// Тип сегмента колеса фортуны (для игрока).
    /// </summary>
    public enum SegmentType
    {
        Heal,
        DamageUp,
        DamageDown,
        Bankrupt,
        SkipTurn,
        HardMode
    }

    /// <summary>
    /// Сегмент обычного колеса фортуны (для игрока).
    /// </summary>
    public class WheelSegment
    {
        /// <summary>Тип сегмента колеса фортуны.</summary>
        public SegmentType Type { get; set; }
        /// <summary>Значение (например, количество HP или урона).</summary>
        public int Value { get; set; }
    }

    /// <summary>
    /// Обычное колесо фортуны, которым пользуется игрок (в казино).
    /// </summary>
    public class Wheel
    {
        /// <summary>Список сегментов колеса.</summary>
        public readonly List<WheelSegment> _segments;

        /// <summary>Создаёт стандартное колесо фортуны с набором сегментов.</summary>
        public Wheel()
        {
            _segments = new List<WheelSegment>()
            {
                new WheelSegment{Type = SegmentType.Heal, Value = 20},
                new WheelSegment{Type = SegmentType.Heal, Value = 30},

                new WheelSegment{Type = SegmentType.DamageUp, Value = 5},
                new WheelSegment{Type = SegmentType.DamageUp, Value = 3},

                new WheelSegment{Type = SegmentType.DamageDown, Value = 2},
                new WheelSegment{Type = SegmentType.DamageDown, Value = 4},

                new WheelSegment{Type = SegmentType.SkipTurn, Value = 0},
                new WheelSegment{Type = SegmentType.Bankrupt, Value = 0},
                new WheelSegment{Type = SegmentType.HardMode, Value = 0}
            };
        }

        /// <summary>
        /// Крутит колесо и возвращает один случайный сегмент.
        /// </summary>
        /// <param name="random">Экземпляр <see cref="Random"/> для получения случайности.</param>
        /// <returns>Выбранный сегмент <see cref="WheelSegment"/>.</returns>
        public WheelSegment Spin(Random random)
        {
            int index = random.Next(_segments.Count);
            return _segments[index];
        }
    }

    #region Final boss wheel and effects

    /// <summary>
    /// Типы эффектов, которые может получить финальный босс.
    /// </summary>
    public enum FinalBossEffectType
    {
        DamageUp,
        Shield,
        Regen,
        DoubleStrike,
        Nothing
    }

    /// <summary>
    /// Описывает эффект, действующий на финального босса в течение нескольких ходов.
    /// </summary>
    public class FinalBossEffect
    {
        /// <summary>Тип эффекта.</summary>
        public FinalBossEffectType Type { get; set; }

        /// <summary>Числовое значение эффекта (урон, проценты, реген и т.д.).</summary>
        public int Value { get; set; }

        /// <summary>Оставшееся количество ходов действия эффекта.</summary>
        public int TurnsRemaining { get; set; }
    }

    /// <summary>
    /// Колесо финального босса, выдающее временные эффекты (DamageUp, Shield, Regen, DoubleStrike).
    /// </summary>
    public class FinalBossWheel
    {
        private readonly List<FinalBossEffect> _segments = new List<FinalBossEffect>();

        /// <summary>Создаёт колесо финального босса с набором эффектов и длительностей.</summary>
        public FinalBossWheel()
        {
            _segments.Add(new FinalBossEffect { Type = FinalBossEffectType.DamageUp, Value = 10, TurnsRemaining = 3 });
            _segments.Add(new FinalBossEffect { Type = FinalBossEffectType.DamageUp, Value = 6, TurnsRemaining = 2 });

            _segments.Add(new FinalBossEffect { Type = FinalBossEffectType.Shield, Value = 30, TurnsRemaining = 2 }); // уменьшает входящий урон на 30%
            _segments.Add(new FinalBossEffect { Type = FinalBossEffectType.Shield, Value = 50, TurnsRemaining = 1 });

            _segments.Add(new FinalBossEffect { Type = FinalBossEffectType.Regen, Value = 15, TurnsRemaining = 3 }); // реген HP/ход
            _segments.Add(new FinalBossEffect { Type = FinalBossEffectType.DoubleStrike, Value = 0, TurnsRemaining = 1 });

            _segments.Add(new FinalBossEffect { Type = FinalBossEffectType.Nothing, Value = 0, TurnsRemaining = 0 });
        }

        /// <summary>
        /// Крутит колесо и возвращает случайный эффект (копия, чтобы не менять шаблон).
        /// </summary>
        /// <param name="random">Экземпляр <see cref="Random"/>.</param>
        /// <returns>Возвращает новый объект <see cref="FinalBossEffect"/> с параметрами выбранного сегмента.</returns>
        public FinalBossEffect Spin(Random random)
        {
            var pick = _segments[random.Next(_segments.Count)];
            return new FinalBossEffect { Type = pick.Type, Value = pick.Value, TurnsRemaining = pick.TurnsRemaining };
        }
    }

    #endregion
}
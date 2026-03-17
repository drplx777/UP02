using System;
using System.Collections.Generic;

namespace ConsoleApp129.Kazikk
{
    enum SegmentType
    {
        Heal,
        DamageUp,
        DamageDown,
        Bankrupt,
        SkipTurn,
        HardMode
    }

    class WheelSegment
    {
        /// <summary>Тип сегмента колеса фортуны.</summary>
        public SegmentType Type { get; set; }
        /// <summary>Значение (например, количество HP или урона).</summary>
        public int Value { get; set; }
    }

    class Wheel
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
}
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
        public SegmentType Type { get; set; }
        public int Value { get; set; }
    }

    class Wheel
    {
        public readonly List<WheelSegment> _segments;

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

        public WheelSegment Spin(Random random)
        {
            int index = random.Next(_segments.Count);
            return _segments[index];
        }
    }
}
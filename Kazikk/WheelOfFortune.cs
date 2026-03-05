using System;
using System.Collections.Generic;

namespace ConsoleApp129.Kazikk
{
    enum SegmentType
    {
        Points,
        Bankrupt,
        SkipTurn
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
            _segments = new List<WheelSegment>();


            for (int points = 10; points <= 100; points += 10)
            {
                _segments.Add(new WheelSegment { Type = SegmentType.Points, Value = points });
            }

            _segments.Add(new WheelSegment { Type = SegmentType.Bankrupt, Value = 0 });
            _segments.Add(new WheelSegment { Type = SegmentType.SkipTurn, Value = 0 });
        }
        public WheelSegment Spin(Random random)
        {
            int index = random.Next(_segments.Count);
            return _segments[index];
        }
        }
}
using System.Collections.Generic;

namespace ConsoleApp129.Save
{
    public class GameData
    {
        public int Width { get; set; }
        public int Height { get; set; }

        public int HeroX { get; set; }
        public int HeroY { get; set; }
        public int HeroHP { get; set; }
        public int HeroBalance { get; set; }
        public int HeroDamage { get; set; } 

        public int MapLevel { get; set; } = 1;

        public List<MapItem> Items { get; set; } = new();
    }

    public class MapItem
    {
        public string Type { get; set; }
        public int X { get; set; }
        public int Y { get; set; }
    }
}
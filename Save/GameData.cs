using System.Collections.Generic;

namespace ConsoleApp129.Save
{
    public class GameData
    {
        /// <summary>
        /// Ширина карты (количество столбцов).
        /// </summary>
        public int Width { get; set; }

        /// <summary>
        /// Высота карты (количество строк).
        /// </summary>
        public int Height { get; set; }

        /// <summary>Координаты и характеристики героя.</summary>
        public int HeroX { get; set; }
        public int HeroY { get; set; }
        public int HeroHP { get; set; }
        public int HeroBalance { get; set; }
        public int HeroDamage { get; set; } 

        /// <summary>Текущий уровень карты (уровень мира).</summary>
        public int MapLevel { get; set; } = 1;

        /// <summary>Список всех объектов карты, отличных от поля.</summary>
        public List<MapItem> Items { get; set; } = new();
    }

    public class MapItem
    {
        /// <summary>Имя типа объекта (например, "Wall", "Enemy").</summary>
        public string Type { get; set; }
        /// <summary>Координата X объекта.</summary>
        public int X { get; set; }
        /// <summary>Координата Y объекта.</summary>
        public int Y { get; set; }
    }
}
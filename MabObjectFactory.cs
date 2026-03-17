using System;

namespace ConsoleApp129
{
    /// <summary>
    /// Фабрика для создания экземпляров <see cref="MapObject"/> по имени типа.
    /// Позволяет инкапсулировать логику создания и упростить расширение типов объектов карты.
    /// </summary>
    internal static class MapObjectFactory
    {
        /// <summary>
        /// Создаёт объект карты по его имени типа.
        /// Если тип неизвестен — возвращает <see cref="Field"/>.
        /// </summary>
        /// <param name="typeName">Имя типа (например, "Wall", "Enemy").</param>
        /// <param name="x">Координата X (нужна для объектов, которым нужны координаты в конструкторе).</param>
        /// <param name="y">Координата Y (нужна для объектов, которым нужны координаты в конструкторе).</param>
        /// <returns>Экземпляр <see cref="MapObject"/> соответствующего типа или <see cref="Field"/> по умолчанию.</returns>
        public static MapObject Create(string typeName, int x, int y)
        {
            // Используем switch expression для компактности и ясности.
            return typeName switch
            {
                nameof(Wall) => new Wall(),
                nameof(Tree) => new Tree(),
                nameof(HealthPoint) => new HealthPoint(),
                nameof(Casino) => new Casino(),
                nameof(Enemy) => new Enemy(x, y),
                nameof(Door) => new Door(),
                nameof(Shop) => new Shop(),
                nameof(Boss) => new Boss(x, y),
                nameof(FinalBoss) => new FinalBoss(x, y),
                _ => new Field(), // безопасный дефолт
            };
        }
    }
}
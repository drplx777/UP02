using System;

namespace ConsoleApp129
{
    /// <summary>
    /// Интерфейс стратегии движения врага.
    /// Позволяет задавать различные алгоритмы перемещения (рандом, агрессивный и т.д.).
    /// </summary>
    internal interface IEnemyMovementStrategy
    {
        /// <summary>
        /// Вычисляет новую позицию для врага.
        /// </summary>
        /// <param name="x">Текущая координата X (строка массива).</param>
        /// <param name="y">Текущая координата Y (столбец массива).</param>
        /// <param name="width">Ширина карты (map.GetLength(0)).</param>
        /// <param name="height">Высота карты (map.GetLength(1)).</param>
        /// <param name="rand">Экземпляр Random для получения случайностей.</param>
        /// <returns>Кортеж (newX, newY) — вычисленная позиция.</returns>
        (int newX, int newY) GetNextPosition(int x, int y, int width, int height, Random rand);
    }

    /// <summary>
    /// Стандартная (по умолчанию) стратегия: случайное движение в 4 направлениях с "wrap-around".
    /// Повторяет текущее поведение проекта, но находится в отдельном классе.
    /// </summary>
    internal class RandomEnemyMovementStrategy : IEnemyMovementStrategy
    {
        public (int newX, int newY) GetNextPosition(int x, int y, int width, int height, Random rand)
        {
            int direction = rand.Next(4);

            int newX = x, newY = y;
            switch (direction)
            {
                case 0:
                    newX = (x - 1 + width) % width;
                    break;
                case 1:
                    newX = (x + 1) % width;
                    break;
                case 2:
                    newY = (y - 1 + height) % height;
                    break;
                case 3:
                    newY = (y + 1) % height;
                    break;
            }

            return (newX, newY);
        }
    }

    // Пример альтернативной стратегии (скелет, можно расширить):
    // internal class AggressiveEnemyMovementStrategy : IEnemyMovementStrategy { ... }
}
using System;
using System.Collections.Generic;
using ConsoleApp129.Save;
using ConsoleApp129.Maps;
using System.Threading;
using ConsoleApp129.Kazikk;

namespace ConsoleApp129
{
    internal class Map
    {
        public static Map Current { get; private set; }
        public int MapLevel = 1;
        Random rand = new Random();
        MapObject[,] map = new MapObject[25, 25];
        private bool doorExists = false;
        private int doorX = -1;
        private int doorY = -1;

        public void Map_generation()
        {
            GenerateLevel(MapLevel);
        }

        private void GenerateLevel(int level)
        {
            int width = map.GetLength(0);
            int height = map.GetLength(1);

            map = new MapObject[width, height];

            for (int i = 0; i < map.GetLength(0); i++)
            {
                for (int j = 0; j < map.GetLength(1); j++)
                {
                    int A = rand.Next(100);
                    map[i, j] = new Field();

                    int enemyThreshold = Math.Max(1, level);
                    int treeThresholdMin = 5;
                    int treeThresholdMax = 20;

                    if (A > 1 && A < 6)
                    {
                        map[i, j] = new Wall();
                    }
                    if (A < enemyThreshold)
                    {
                        map[i, j] = new Enemy(i, j);
                    }
                    if (A > treeThresholdMin && A < treeThresholdMax)
                    {
                        map[i, j] = new Tree();
                    }
                    if (A > 5 && A < 10)
                    {
                        map[i, j] = new HealthPoint();
                    }

                    if (level > 1 && i == 10 && j == 10)
                    {
                        map[i, j] = new Casino();
                    }
                    if (level > 2 && i == 15 && j == 15)
                    {
                        map[i, j] = new Shop();
                    }

                    if (i == map.GetLength(0) / 2 && j == map.GetLength(1) / 2)
                    {
                        map[i, j] = new Hero(i, j);
                    }
                }
            }
            if (level % 3 == 0)
            {
                List<(int x, int y)> free = new List<(int, int)>();
                for (int i = 0; i < map.GetLength(0); i++)
                    for (int j = 0; j < map.GetLength(1); j++)
                        if (map[i, j] is Field) free.Add((i, j));

                if (free.Count > 0)
                {
                    var pick = free[rand.Next(free.Count)];
                    map[pick.x, pick.y] = new Boss(pick.x, pick.y);
                }
            }

            doorExists = false;
            Current = this;
            doorX = doorY = -1;
        }

        public void Drawing_the_map()
        {
            for (int i = 0; i < map.GetLength(0); i++)
            {
                for (int j = 0; j < map.GetLength(1); j++)
                {
                    Console.Write(map[i, j].Rendering_on_the_map() + " ");
                    Console.ResetColor();
                }
                Console.WriteLine();
            }

            Hero hero = FindHero();
            int Enemies = CountEnemies();

            if (hero != null)
            {
                Console.WriteLine();
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine($"HP героя: {hero.HP}");
                Console.WriteLine($"Гроши: {hero.Balance}");
                Console.WriteLine($"Урон героя: {hero.Damage}");
                Console.WriteLine($"Врагов на карте: {Enemies}");
                Console.WriteLine($"Уровень мира: {MapLevel}");
                Console.ResetColor();
            }

            if (Enemies == 0)
            {
                if (MapLevel < 9)
                {
                    if (!doorExists)
                    {
                        PlaceDoorOnRandomField();
                        Console.ForegroundColor = ConsoleColor.Cyan;
                        Console.WriteLine();
                        Console.WriteLine("Появилась дверь! Войдите в неё, чтобы перейти на следующий уровень.");
                        Console.ResetColor();
                    }
                }
                else
                {
                    Console.Clear();
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("Поздравляем! Вы очистили 9-й уровень и победили в игре!");
                    Console.ResetColor();
                    Console.WriteLine("Нажмите любую клавишу для выхода...");
                    Console.ReadKey();
                    Environment.Exit(0);
                }
            }
        }

        private void PlaceDoorOnRandomField()
        {
            List<(int x, int y)> free = new List<(int, int)>();
            for (int i = 0; i < map.GetLength(0); i++)
                for (int j = 0; j < map.GetLength(1); j++)
                {
                    if (map[i, j] is Field) free.Add((i, j));
                }

            if (free.Count == 0) return;

            var pick = free[rand.Next(free.Count)];
            doorX = pick.x;
            doorY = pick.y;
            map[doorX, doorY] = new Door();
            doorExists = true;
        }

        public Hero FindHero()
        {
            for (int i = 0; i < map.GetLength(0); i++)
                for (int j = 0; j < map.GetLength(1); j++)
                    if (map[i, j] is Hero h) return h;
            return null;
        }

        public void MovePersons()
        {
            MapObject[,] newMap = new MapObject[map.GetLength(0), map.GetLength(1)];
            Array.Copy(map, newMap, map.Length);

            for (int i = 0; i < map.GetLength(0); i++)
            {
                for (int j = 0; j < map.GetLength(1); j++)
                {
                    if (map[i, j] is Enemy)
                    {
                        int direction = rand.Next(4);

                        int newX = i, newY = j;
                        switch (direction)
                        {
                            case 0:
                                newX = (i - 1 + map.GetLength(0)) % map.GetLength(0);
                                break;
                            case 1:
                                newX = (i + 1) % map.GetLength(0);
                                break;
                            case 2:
                                newY = (j - 1 + map.GetLength(1)) % map.GetLength(1);
                                break;
                            case 3:
                                newY = (j + 1) % map.GetLength(1);
                                break;
                        }

                        if (newMap[newX, newY] is Field)
                        {
                            newMap[newX, newY] = map[i, j];
                            newMap[i, j] = new Field();
                        }
                    }
                }
            }

            Array.Copy(newMap, map, map.Length);
        }

        public void MovePersons(ConsoleKey key)
        {
            MapObject[,] newMap = new MapObject[map.GetLength(0), map.GetLength(1)];
            Array.Copy(map, newMap, map.Length);

            for (int i = 0; i < map.GetLength(0); i++)
            {
                for (int j = 0; j < map.GetLength(1); j++)
                {
                    if (map[i, j] is Hero hero)
                    {
                        int newX = i, newY = j;
                        switch (key)
                        {
                            case ConsoleKey.UpArrow:
                                newX = (i - 1 + map.GetLength(0)) % map.GetLength(0);
                                break;
                            case ConsoleKey.DownArrow:
                                newX = (i + 1) % map.GetLength(0);
                                break;
                            case ConsoleKey.LeftArrow:
                                newY = (j - 1 + map.GetLength(1)) % map.GetLength(1);
                                break;
                            case ConsoleKey.RightArrow:
                                newY = (j + 1) % map.GetLength(1);
                                break;
                        }

                        if (newMap[newX, newY] is Field)
                        {
                            newMap[newX, newY] = map[i, j];
                            newMap[i, j] = new Field();
                        }
                        if (newMap[newX, newY] is Boss boss)
                        {
                            var kazik = new Kazik();
                            kazik.BossBlackjack(hero, boss);

                            if (boss.Lives <= 0)
                            {
                                newMap[newX, newY] = map[i, j];
                                newMap[i, j] = new Field();
                            }
                            else
                            {
                            }
                        }
                        else if (newMap[newX, newY] is HealthPoint)
                        {
                            newMap[newX, newY] = map[i, j];
                            ((Hero)newMap[newX, newY]).HP += 10;
                            newMap[i, j] = new Field();
                        }
                        else if (newMap[newX, newY] is Enemy enemy)
                        {
                            enemy.HP -= hero.Damage;
                            hero.HP -= enemy.Damage;

                            if (((Hero)map[i, j]).HP <= 0)
                            {
                                Console.Clear();
                                Console.WriteLine("You Died!");
                                Environment.Exit(0);
                            }
                            else if (enemy.HP <= 0)
                            {
                                newMap[newX, newY] = map[i, j];
                                newMap[i, j] = new Field();
                            }
                        }
                        else if (newMap[newX, newY] is Casino)
                        {
                            ((Casino)newMap[newX, newY]).Interaction();
                        }
                        else if(newMap[newX, newY] is Shop)
                        { 
                            ((Shop)newMap[newX, newY]).Interaction();
                        }
                        else if (newMap[newX, newY] is Door)
                        {
                            if (MapLevel < 9)
                            {
                                var oldHero = FindHero() as Hero;
                                int oldHP = oldHero?.HP ?? 100;
                                int oldBalance = oldHero?.Balance ?? 1000;
                                int oldDamage = oldHero?.Damage ?? 10;
                                MapLevel++;
                                GenerateLevel(MapLevel);
                                Console.Clear();
                                Console.WriteLine($"Переход на уровень {MapLevel}...");
                                Thread.Sleep(1000);

                                int centerX = map.GetLength(0) / 2;
                                int centerY = map.GetLength(1) / 2;
                                map[centerX, centerY] = new Hero(centerX, centerY);
                                map[centerX, centerY] = new Hero(centerX, centerY)
                                {
                                    HP = oldHP,
                                    Balance = oldBalance,
                                    Damage = oldDamage
                                };
                            }
                            else
                            {
                                Console.Clear();
                                Console.ForegroundColor = ConsoleColor.Green;
                                Console.WriteLine("Поздравляем! Вы завершили все уровни и победили!");
                                Console.ResetColor();
                                Console.WriteLine("Нажмите любую клавишу для выхода...");
                                Console.ReadKey();
                                Environment.Exit(0);
                            }

                            Array.Copy(map, newMap, map.Length);
                            Array.Copy(newMap, map, map.Length);
                            return;
                        }
                    }
                }
            }

            Array.Copy(newMap, map, map.Length);
        }

        public void LoadGame(GameData data)
        {
            map = new MapObject[data.Width, data.Height];

            for (int i = 0; i < data.Width; i++)
                for (int j = 0; j < data.Height; j++)
                    map[i, j] = new Field();

            foreach (var item in data.Items)
            {
                switch (item.Type)
                {
                    case nameof(Wall):
                        map[item.X, item.Y] = new Wall();
                        break;
                    case nameof(Tree):
                        map[item.X, item.Y] = new Tree();
                        break;
                    case nameof(HealthPoint):
                        map[item.X, item.Y] = new HealthPoint();
                        break;
                    case nameof(Casino):
                        map[item.X, item.Y] = new Casino();
                        break;
                    case nameof(Enemy):
                        map[item.X, item.Y] = new Enemy(item.X, item.Y);
                        break;
                    case nameof(Door):
                        map[item.X, item.Y] = new Door();
                        doorExists = true;
                        doorX = item.X;
                        doorY = item.Y;
                        break;
                    case nameof(Shop):
                        map[item.X, item.Y] = new Shop();
                        break;
                }
            }

            map[data.HeroX, data.HeroY] =
                new Hero(data.HeroX, data.HeroY)
                {
                    HP = data.HeroHP,
                    Balance = data.HeroBalance,
                    Damage = data.HeroDamage
                };

            MapLevel = data.MapLevel <= 0 ? 1 : data.MapLevel;
        }

        public GameData GetGameData()
        {
            GameData data = new GameData
            {
                Width = map.GetLength(0),
                Height = map.GetLength(1),
                MapLevel = this.MapLevel
            };

            for (int i = 0; i < map.GetLength(0); i++)
            {
                for (int j = 0; j < map.GetLength(1); j++)
                {
                    if (map[i, j] is Hero h)
                    {
                        data.HeroX = i;
                        data.HeroY = j;
                        data.HeroHP = h.HP;
                        data.HeroBalance = h.Balance;
                        data.HeroDamage = h.Damage;
                    }
                    else if (map[i, j] is not Field)
                    {
                        data.Items.Add(new MapItem
                        {
                            Type = map[i, j].GetType().Name,
                            X = i,
                            Y = j
                        });
                    }
                }
            }

            return data;
        }

        public int CountEnemies()
        {
            int Count = 0;
            for (int i = 0; i < map.GetLength(0); i++)
            {
                for (int j = 0; j < map.GetLength(1); j++)
                {
                    if (map[i, j] is Enemy)
                    {
                        Count++;
                    }
                }
            }
            return Count;
        }
    }
}
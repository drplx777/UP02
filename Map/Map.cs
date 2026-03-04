using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Threading.Tasks.Dataflow;
using ConsoleApp129.Save;
namespace ConsoleApp129
{
    internal class Map
    {
        Random rand = new Random();
        MapObject[,] map = new MapObject[25, 25];

       
        public void Map_generation()
        {
            for (int i = 0; i < map.GetLength(0); i++)
            {
                for (int j = 0; j < map.GetLength(1); j++)
                {
                    int A = rand.Next(100);
                    map[i, j] = new Field();

                    if (A > 1 && A<6)
                    {
                        map[i, j] = new Wall();
                    }
                    if (A < 1)
                    {
                        map[i, j] = new Enemy(i, j);
                    }
                    if (A > 5 && A < 20)
                    {
                        map[i, j] = new Tree();
                    }
                    if (A > 5 && A < 10)
                    {
                        map[i,j] = new HealthPoint();
                    }
                    if (i == 10 && j == 10)
                    {
                        map[i, j] = new Casino();
                    }
                    if (i == map.GetLength(0) / 2 && j == map.GetLength(1) / 2)
                    {
                        map[i, j] = new Hero(i, j);
                    }

                }
            }
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
            if (hero != null)
            {
                Console.WriteLine();
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine($"HP героя: {hero.HP}");
                Console.ResetColor();
            }
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
                    if (map[i, j] is Hero)
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
                        if (newMap[newX, newY] is HealthPoint)
                        {
                            newMap[newX, newY] = map[i, j];
                            ((Hero)newMap[newX, newY]).HP += 10;
                            newMap[i, j] = new Field();
                        }
                        if (newMap[newX, newY] is Enemy)
                        {
                            newMap[newX, newY] = map[i, j];
                            ((Hero)map[i, j]).HP -= 10;
                            if (((Hero)map[i, j]).HP <= 0)
                            {
                                Console.Clear();
                                Console.WriteLine("You Died!");
                                Environment.Exit(0);
                            }
                            newMap[i, j] = new Field();
                        }
                        if (newMap[newX, newY] is Casino)
                        {
                            ((Casino)newMap[newX, newY]).Interaction();
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
                }
            }
            map[data.HeroX, data.HeroY] =
                new Hero(data.HeroX, data.HeroY)
                {
                    HP = data.HeroHP
                };
        }
        public GameData GetGameData(){
            GameData data = new GameData
            {
                Width = map.GetLength(0),
                Height = map.GetLength(1)
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
    }
}

using System;
using System.Buffers;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Threading.Tasks.Dataflow;
using ConsoleApp129.Maps;
namespace ConsoleApp129
{
    class MapLevel2 : Imap
    {
        Random rand = new Random();
        public MapObject[,] Map_generation()
        {
            Map map = new Map();
            MapObject[,]map2 = new MapObject[25,25];
            for (int i = 0; i < map2.GetLength(0); i++)
            {
                for (int j = 0; j < map2.GetLength(1); j++)
                {
                    int A = rand.Next(100);
                    map2[i, j] = new Field();

                    if (A > 1 && A<6)
                    {
                        map2[i, j] = new Wall();
                    }
                    if (A < 5)
                    {
                        map2[i, j] = new Enemy(i, j);
                    }
                    if (A > 5 && A < 20)
                    {
                        map2[i, j] = new Tree();
                    }
                    if (A > 5 && A < 10)
                    {
                        map2[i,j] = new HealthPoint();
                    }
                    if (i == 10 && j == 10)
                    {
                        map2[i, j] = new Casino();
                    }
                    if (i == map2.GetLength(0) / 2 && j == map2.GetLength(1) / 2)
                    {
                        map2[i, j] = new Hero(i, j);
                    }

                }
            }
            return map2;
        }
    }
}
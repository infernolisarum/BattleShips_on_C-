using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleShips
{
    class Program
    {
        static void Main(string[] args)
        {
            FieldGeneration myField = new FieldGeneration();
            FieldGeneration enemyField = new FieldGeneration();
            for (int i = 0; i < 10; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    Console.Write("{0} ", myField.getField()[i, j]);
                }
                Console.Write("\t");
                for (int k = 0; k < 10; k++)
                {
                    Console.Write("{0} ", enemyField.getField()[i, k]);
                }
                Console.WriteLine();
            }
            Console.ReadKey();
        }
    }

    class FieldGeneration
    {
        private int[,] field;

        private void CreateField()
        {
            field = new int[10, 10];
            for (int i = 0; i < 10; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    field[i, j] = 0;
                }
            }
        }

        public int[,] getField()
        {
            CreateField();
            DeployingOnField();
            return field;
        }

        private void DeployingOnField()
        {
            Random rnd = new Random();
            Ship_Design[] ships = new Ship_Design[10];
            ships[0] = new Ship_Design(4);
            ships[1] = new Ship_Design(3);
            ships[2] = new Ship_Design(3);
            ships[3] = new Ship_Design(2);
            ships[4] = new Ship_Design(2);
            ships[5] = new Ship_Design(2);
            ships[6] = new Ship_Design(1);
            ships[7] = new Ship_Design(1);
            ships[8] = new Ship_Design(1);
            ships[9] = new Ship_Design(1);
            for (int j = 0; j < 10; j++)
            {
                int countDeck = ships[j].GetValueShip();
                for (int i = 0; i < 5; i++)
                {
                    int X = rnd.Next(0, 9);
                    int Y = rnd.Next(0, 9);
                    if (DirectionRIGHT(countDeck, X, Y) == true) break;
                    if (DirectionLEFT(countDeck, X, X) == true) break;
                    if (DirectionUP(countDeck, X, Y) == true) break;
                    if (DirectionDOWN(countDeck, X, Y) == true) break;
                }
            }
        }

        private bool DirectionRIGHT(int dirCountDeck, int dirX, int dirY)
        {
            if (dirY + (dirCountDeck - 1) > 9) return false;
            int sectionCount = 0;
            for (int i = 0; i < dirCountDeck; i++)
            {
                if (CheckArea(dirX, dirY + i) == 0)
                {
                    sectionCount++;
                }
            }

            if (sectionCount == dirCountDeck)
            {
                for (int i = 0; i < dirCountDeck; i++)
                {
                    Deploying(dirCountDeck, dirX, dirY + i);
                }
                return true;
            }
            return false;
        }

        private bool DirectionLEFT(int dirCountDeck, int dirX, int dirY)
        {
            if (dirY - (dirCountDeck - 1) < 0) return false;
            int sectionCount = 0;
            for (int i = 0; i < dirCountDeck; i++)
            {
                if (CheckArea(dirX, dirY - i) == 0)
                {
                    sectionCount++;
                }
            }

            if (sectionCount == dirCountDeck)
            {
                for (int i = 0; i < dirCountDeck; i++)
                {
                    Deploying(dirCountDeck, dirX, dirY - i);
                }
                return true;
            }
            return false;
        }

        private bool DirectionUP(int dirCountDeck, int dirX, int dirY)
        {
            if (dirX - (dirCountDeck - 1) < 0) return false;
            int sectionCount = 0;
            for (int i = 0; i < dirCountDeck; i++)
            {
                if (CheckArea(dirX - i, dirY) == 0)
                {
                    sectionCount++;
                }
            }

            if (sectionCount == dirCountDeck)
            {
                for (int i = 0; i < dirCountDeck; i++)
                {
                    Deploying(dirCountDeck, dirX - i, dirY);
                }
                return true;
            }
            return false;
        }

        private bool DirectionDOWN(int dirCountDeck, int dirX, int dirY)
        {
            if (dirX + (dirCountDeck - 1) > 9) return false;
            int sectionCount = 0;
            for (int i = 0; i < dirCountDeck; i++)
            {
                if (CheckArea(dirX + i, dirY) == 0)
                {
                    sectionCount++;
                }
            }

            if (sectionCount == dirCountDeck)
            {
                for (int i = 0; i < dirCountDeck; i++)
                {
                    Deploying(dirCountDeck, dirX + i, dirY);
                }
                return true;
            }
            return false;
        }

        public void Deploying(int value, int x, int y)
        {
            field[x, y] = value;//Ship.SetLocation(x, y);
        }

        private int CheckArea(int x, int y)
        {
            int[] sequenceParam = new int[] { x, y, x + 1, y, x + 1, y + 1, x, y + 1, x - 1, y + 1, x - 1, y, x - 1, y - 1, x, y - 1, x + 1, y - 1 };
            int bufInt = 0;
            for (int i = 0; i < 18; i++)
            {
                int bufX = sequenceParam[i];
                ++i;
                int bufY = sequenceParam[i];
                bufInt += InArr(bufX, bufY);
            }
            return bufInt;
        }

        private int InArr(int x, int y)
        {
            if ((x > 9 || x < 0) || (y > 9 || y < 0))
            {
                return 0;
            }
            else
            {
                return field[x, y];
            }
        }
    }

    class Ship_Design
    {
        private int valueDeck;
        protected int[] coordinates;

        public Ship_Design(int data)
        {
            this.valueDeck = data;
            coordinates = new int[data];
        }

        public int GetValueShip()
        {
            return valueDeck;
        }

        public void SetCoordinates(int[] data)
        {
            this.coordinates = data;
        }

        public int[] GetCoordinates()
        {
            return coordinates;
        }
    }
}

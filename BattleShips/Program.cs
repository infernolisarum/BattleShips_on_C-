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

            Console.WriteLine();
            foreach (Ship_Design ship in myField.getShipsOnField())
            {
                foreach (int i in ship.GetCoordinates())
                    Console.Write(i);
                Console.WriteLine();
            }
            Console.ReadKey();
        }
    }

    class FieldGeneration : All_Ships
    {
        private int[,] field;

        public FieldGeneration()
        {
            createField();
            deployingOnField();
        }

        private void createField()
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
            return field;
        }

        public Ship_Design[] getShipsOnField()
        {
            return getAllShips();
        }

        private void deployingOnField()
        {
            Ship_Design[] ships = getAllShips();
            Random rnd = new Random();
            foreach(Ship_Design ship in ships)
            {
                for (int i = 0; i < 30; i++)
                {
                    int X = rnd.Next(0, 9);
                    int Y = rnd.Next(0, 9);
                    if (directionRIGHT(ship, X, Y) == true) break;
                    if (directionLEFT(ship, X, X) == true) break;
                    if (directionUP(ship, X, Y) == true) break;
                    if (directionDOWN(ship, X, Y) == true) break;
                }
            }
        }

        private bool directionRIGHT(Ship_Design ship, int x, int y)
        {
            if (y + (ship.GetValueShip() - 1) > 9) return false;
            int sectionCount = 0;
            for (int i = 0; i < ship.GetValueShip(); i++)
            {
                if (checkArea(x, y + i) == 0)
                {
                    sectionCount++;
                }
            }

            if (sectionCount == ship.GetValueShip())
            {
                for (int i = 0; i < ship.GetValueShip(); i++)
                {
                    marking(ship.GetValueShip(), x, y + i);
                    ship.SetCoordinates(x);
                    ship.SetCoordinates(y + i);
                }
                return true;
            }
            return false;
        }

        private bool directionLEFT(Ship_Design ship, int x, int y)
        {
            if (y - (ship.GetValueShip() - 1) < 0) return false;
            int sectionCount = 0;
            for (int i = 0; i < ship.GetValueShip(); i++)
            {
                if (checkArea(x, y - i) == 0)
                {
                    sectionCount++;
                }
            }

            if (sectionCount == ship.GetValueShip())
            {
                for (int i = 0; i < ship.GetValueShip(); i++)
                {
                    marking(ship.GetValueShip(), x, y - i);
                    ship.SetCoordinates(x);
                    ship.SetCoordinates(y - i);
                }
                return true;
            }
            return false;
        }

        private bool directionUP(Ship_Design ship, int x, int y)
        {
            if (x - (ship.GetValueShip() - 1) < 0) return false;
            int sectionCount = 0;
            for (int i = 0; i < ship.GetValueShip(); i++)
            {
                if (checkArea(x - i, y) == 0)
                {
                    sectionCount++;
                }
            }

            if (sectionCount == ship.GetValueShip())
            {
                for (int i = 0; i < ship.GetValueShip(); i++)
                {
                    marking(ship.GetValueShip(), x - i, y);
                    ship.SetCoordinates(x - i);
                    ship.SetCoordinates(y);
                }
                return true;
            }
            return false;
        }

        private bool directionDOWN(Ship_Design ship, int x, int y)
        {
            if (x + (ship.GetValueShip() - 1) > 9) return false;
            int sectionCount = 0;
            for (int i = 0; i < ship.GetValueShip(); i++)
            {
                if (checkArea(x + i, y) == 0)
                {
                    sectionCount++;
                }
            }

            if (sectionCount == ship.GetValueShip())
            {
                for (int i = 0; i < ship.GetValueShip(); i++)
                {
                    marking(ship.GetValueShip(), x + i, y);
                    ship.SetCoordinates(x + i);
                    ship.SetCoordinates(y);
                }
                return true;
            }
            return false;
        }

        private int checkArea(int x, int y)
        {
            int[] sequenceParam = new int[] { x, y, x + 1, y, x + 1, y + 1, x, y + 1, x - 1, y + 1, x - 1, y, x - 1, y - 1, x, y - 1, x + 1, y - 1 };
            int bufInt = 0;
            for (int i = 0; i < 18; i++)
            {
                int bufX = sequenceParam[i];
                ++i;
                int bufY = sequenceParam[i];
                bufInt += inArr(bufX, bufY);
            }
            return bufInt;
        }

        private int inArr(int x, int y)
        {
            if (x > 9 || x < 0 || y > 9 || y < 0)
            {
                return 0;
            }
            else
            {
                return field[x, y];
            }
        }

        private void marking(int cellValue, int mx, int my)
        {
            field[mx, my] = cellValue;
        }
    }

    class Ship_Design
    {
        private int valueDeck;
        private int buffer = 0;
        protected int[] coordinates;

        public Ship_Design(int data)
        {
            this.valueDeck = data;
            coordinates = new int[data * 2];
        }

        public int GetValueShip()
        {
            return valueDeck;
        }

        public void SetCoordinates(int number)
        {
            coordinates[buffer] = number;
            ++buffer;
        }

        public int[] GetCoordinates()
        {
            return coordinates;
        }
    }

    class All_Ships
    {
        protected Ship_Design[] ships = new Ship_Design[10];
        
        public All_Ships()
        {
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
        }

        protected Ship_Design[] getAllShips()
        {
            return ships;
        }
    }
}

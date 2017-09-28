﻿using System;
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
            Field_Generation myField = new Field_Generation();
            Field_Generation enemyField = new Field_Generation();
            Session session1 = new Session(myField, enemyField);
            session1.battleLog();
            Console.ReadKey();
        }
    }

    class Session : The_Battle
    {
        private Field_Generation S_myField;
        private Field_Generation S_enemyField;

        public Session(Field_Generation mField, Field_Generation eField)
        {
            this.S_myField = mField;
            this.S_enemyField = eField;
            setMyShips(S_myField.getShipsOnField());
            setEnemyShips(S_enemyField.getShipsOnField());
        }

        private void drawBattlefield()
        {
            for (int i = 0; i < 10; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    Console.Write("{0} ", S_myField.getField()[i, j]);
                }
                Console.Write("\t");
                for (int k = 0; k < 10; k++)
                {
                    Console.Write("{0} ", S_enemyField.getField()[i, k]);
                }
                Console.WriteLine();
            }
            Console.WriteLine();
        }

        public void battleLog()
        {
            for (int i = 0; i < 200; i++)
            {
                Console.Clear();
                drawBattlefield();
                if (myTurn() == false)
                {
                    battleLog();
                }
                if (checkEnemyShips() >= 10)
                    youWin();
                adversary();
                if (checkMyShips() >= 10)
                    winBot();
            }
        }

        private bool myTurn()
        {
            try
            {
                Console.WriteLine("Введите значение по оси Х (0 – 9)");
                int x = Convert.ToInt32(Console.ReadLine());
                Console.WriteLine("Введите значение по оси Y (0 – 9)");
                int y = Convert.ToInt32(Console.ReadLine());
                if (checkDataX_Y(Math.Abs(x), Math.Abs(y)) == true)
                {
                    myShot(Math.Abs(x), Math.Abs(y));
                    return true;
                }
            }
            catch
            {
                Console.WriteLine("Вы ввели неправильные данные!");
                Console.ReadKey();
                return false;
            }
            return false;
        }

        private void adversary()
        {
            Console.WriteLine("Ходит ваш противник.");
            Random ran = new Random();
            int x = ran.Next(0, 9);
            int y = ran.Next(0, 9);
            if (enemyShot(x, y) == true)
            {
                if(checkMyShips() >= 10)
                {
                    winBot();
                }
            }
        }

        private void youWin()
        {
            Console.Clear();
            Console.Beep(); Console.Beep(); Console.Beep(); Console.Beep(); Console.Beep();
            Console.WriteLine("Вы выйграли.");
            Console.ReadKey();
            Environment.Exit(0);
        }

        private void winBot()
        {
            Console.Clear();
            Console.Beep(); Console.Beep(); Console.Beep(); Console.Beep(); Console.Beep();
            Console.WriteLine("Вы проиграли.");
            Console.ReadKey();
            Environment.Exit(0);
        }

        private bool checkDataX_Y(int x, int y)
        {
            if (x > 9 || y > 9) return false;
            return true;
        }

        public void timer() { }//!!!
    }

    class The_Battle
    {
        Ship_Design[] MyShips;
        Ship_Design[] EnemyShips;
        private int[] myShots;
        private int[] enemyShots;
        private int myArrLen = 0;
        private int enemyArrLen = 0;

        public The_Battle()
        {
            myShots = new int[200];
            enemyShots = new int[200];
            for(int i = 0; i < 200; i++)
            {
                myShots[i] = -1;
                enemyShots[i] = -1;
            }
        }

        protected void setMyShips(Ship_Design[] myShips)
        {
            MyShips = myShips;
        }

        protected void setEnemyShips(Ship_Design[] enemyShips)
        {
            EnemyShips = enemyShips;
        }

        protected void myShot(int x, int y)
        {
            for(int i = 0; i < myShots.Length;)
            {
                if((x != myShots[i])&&(y != myShots[++i]))
                {
                    i++;
                }
                else
                {
                    break;
                }
            }
            recordingMyShots(x, y);
            foreach (Ship_Design ship in EnemyShips)
            {
                for (int j = 0; j < ship.GetCoordinates().Length; j++)
                {
                    if ((ship.GetCoordinates()[j] == x) && (ship.GetCoordinates()[++j] == y))
                    {
                        ship.setHit();
                        Console.Beep();
                    }
                }
            }
        }

        protected int[] getEnemyShots()
        {
            return enemyShots;
        }

        protected bool enemyShot(int x, int y)
        {
            for (int i = 0; i < getEnemyShots().Length;)
            {
                if ((x != getEnemyShots()[i]) && (y != getEnemyShots()[++i]))
                {
                    ++i;
                }
                else
                {
                    return false;
                }
            }
            recordingEnemyShots(x, y);
            foreach (Ship_Design ship in MyShips)
            {
                for (int i = 0; i < ship.GetCoordinates().Length; i++)
                {
                    if ((ship.GetCoordinates()[i] == x) && (ship.GetCoordinates()[++i] == y))//!!!
                         //System.IndexOutOfRangeException: "Индекс находился вне границ массива."
                    {
                        ship.setHit();
                        Console.Beep();
                    }
                }
            }
            return true;
        }

        protected int checkMyShips()
        {
            int destroyedShip = 0;
            foreach (Ship_Design ship in MyShips)
            {
                destroyedShip += ship.getCurrentStatus();
            }
            return destroyedShip;
        }

        protected int checkEnemyShips()
        {
            int destroyedShips = 0;
            foreach(Ship_Design ship in EnemyShips)
            {
                destroyedShips += ship.getCurrentStatus();
            }
            return destroyedShips;
        }

        private void recordingMyShots(int x, int y)
        {
            myShots[myArrLen] = x;
            ++myArrLen;
            myShots[myArrLen] = y;
            ++myArrLen;
        }

        private void recordingEnemyShots(int x, int y)
        {
            enemyShots[enemyArrLen] = x;
            ++enemyArrLen;
            enemyShots[enemyArrLen] = y;
            ++enemyArrLen;
        }
    }

    class Field_Generation : All_Ships
    {
        private int[,] field;

        public Field_Generation()
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
        private int hit = 0;
        private int status = 0;
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

        public void setHit()
        {
            hit++;
            if (hit == valueDeck)
                status++;
        }

        public int getCurrentStatus()
        {
            return status;
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

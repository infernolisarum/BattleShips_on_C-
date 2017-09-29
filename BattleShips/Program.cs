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
            Field_Creation myField = new Field_Creation();
            Field_Creation enemyField = new Field_Creation();
            Session session1 = new Session(myField, enemyField);
            session1.battleLog();
            Console.ReadKey();
        }
    }

    class Session : The_Battle
    {
        private char[,] myCharField = new char[10, 10];
        private char[,] enCharField = new char[10, 10];
        private Field_Creation S_myField;
        private Field_Creation S_enemyField;

        public Session(Field_Creation mField, Field_Creation eField)
        {
            this.S_myField = mField;
            this.S_enemyField = eField;
            setMyShips(S_myField.getShipsOnField());
            setEnemyShips(S_enemyField.getShipsOnField());
            for (int i = 0; i < 10; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    myCharField[i, j] = '0';
                    enCharField[i, j] = '0';
                }
            }
            superpositionArrays();
        }

        private void superpositionArrays()
        {
            for (int i = 0; i < 10; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    if (S_myField.getField()[i, j] == 0) myCharField[i, j] = '0';
                    else if (S_myField.getField()[i, j] == 1) myCharField[i, j] = '1';
                    else if (S_myField.getField()[i, j] == 2) myCharField[i, j] = '2';
                    else if (S_myField.getField()[i, j] == 3) myCharField[i, j] = '3';
                    else if (S_myField.getField()[i, j] == 4) myCharField[i, j] = '4';
                }
            }
        }

        private void enMapCorrective(int x, int y)
        {
            if (S_enemyField.getField()[x, y] == 1) enCharField[x, y] = '1';
            else if (S_enemyField.getField()[x, y] == 2) enCharField[x, y] = '2';
            else if (S_enemyField.getField()[x, y] == 3) enCharField[x, y] = '3';
            else if (S_enemyField.getField()[x, y] == 4) enCharField[x, y] = '4';
            else enCharField[x, y] = '#';
        }

        private void myMapCorrective(int x, int y)
        {
            if (S_myField.getField()[x, y] == 1) myCharField[x, y] = 'X';
            else if (S_myField.getField()[x, y] == 2) myCharField[x, y] = 'X';
            else if (S_myField.getField()[x, y] == 3) myCharField[x, y] = 'X';
            else if (S_myField.getField()[x, y] == 4) myCharField[x, y] = 'X';
            else myCharField[x, y] = '#';
        }

        private void drawBattlefield()
        {
            for (int i = 0; i < 10; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    Console.Write(myCharField[i, j] + " ");
                }
                Console.Write("\t");
                for (int k = 0; k < 10; k++)
                {
                    Console.Write(enCharField[i, k] + " ");
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
            int x, y;
            try
            {
                Console.WriteLine("Введите значение по оси Х [0 – 9]");
                x = Convert.ToInt32(Console.ReadLine());
                Console.WriteLine("Введите значение по оси Y [0 – 9]");
                y = Convert.ToInt32(Console.ReadLine());
            }
            catch
            {
                Console.WriteLine("Вы ввели неправильные данные!");
                Console.ReadKey();
                return false;
            }
            if (checkDataX_Y(Math.Abs(x), Math.Abs(y)) == true)
            {
                myShot(Math.Abs(x), Math.Abs(y));
                enMapCorrective(x, y);
                return true;
            }
            else
            {
                Console.WriteLine("Вы ввели неправильные данные!");
                Console.ReadKey();
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
                myMapCorrective(x, y);
                if (checkMyShips() >= 10)
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
            if ((x >= 0 && x < 10) && (y >= 0 && y < 10)) return true;
            return false;
        }
    }

    class The_Battle
    {
        Ship_Design[] MyShips;
        Ship_Design[] EnemyShips;
        private int[] historyMyShots;
        private int[] historyEnemyShots;
        private int myArrLen = 0;
        private int enemyArrLen = 0;

        public The_Battle()
        {
            historyMyShots = new int[200];
            historyEnemyShots = new int[200];
            for(int i = 0; i < 200; i++)
            {
                historyMyShots[i] = -1;
                historyEnemyShots[i] = -1;
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
            for(int i = 0; i < historyMyShots.Length;)
            {
                if((x != historyMyShots[i])&&(y != historyMyShots[++i]))
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
                for (int i = 0, j = 1; i < ship.GetCoordinates().Length / 2; i++, j++)
                {
                    if ((ship.GetCoordinates()[i] == x) && (ship.GetCoordinates()[j] == y))
                    {
                        ship.setHit();
                        Console.Beep();
                        break;
                    }
                }
            }
        }

        protected int[] getAllEnemyShots()
        {
            return historyEnemyShots;
        }

        protected bool enemyShot(int x, int y)
        {
            for (int i = 0; i < getAllEnemyShots().Length;)
            {
                if ((x != getAllEnemyShots()[i]) && (y != getAllEnemyShots()[++i]))
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
                for (int i = 0, j = 1; i < ship.GetCoordinates().Length / 2; i++, j++)
                {
                    if ((ship.GetCoordinates()[i] == x) && (ship.GetCoordinates()[j] == y))
                    {
                        ship.setHit();
                        Console.Beep();
                        return true;
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
            historyMyShots[myArrLen] = x;
            ++myArrLen;
            historyMyShots[myArrLen] = y;
            ++myArrLen;
        }

        private void recordingEnemyShots(int x, int y)
        {
            historyEnemyShots[enemyArrLen] = x;
            ++enemyArrLen;
            historyEnemyShots[enemyArrLen] = y;
            ++enemyArrLen;
        }
    }

    class Field_Creation : All_Ships
    {
        private int[,] field;

        public Field_Creation()
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

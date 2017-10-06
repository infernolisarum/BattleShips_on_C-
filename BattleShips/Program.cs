using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleShips
{
    class Program
    {
        public static Random rnd = new Random();
        static void Main(string[] args)
        {
            Session session = new Session();
            session.battleLog();
            Console.ReadKey();
        }
    }

    class Session : The_Battle
    {
        int[] completedLong_Y_arrEnShots = new int[10];
        int[,] allEnemyShots = new int[10, 10];
        private char[,] myCharField = new char[10, 10];
        private char[,] enCharField = new char[10, 10];
        private Fields_Creation S_myField = new Fields_Creation();

        public Session()
        {
            setMyShips(S_myField.getShipsOnField1());
            setEnemyShips(S_myField.getShipsOnField2());
            for (int i = 0; i < 10; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    completedLong_Y_arrEnShots[j] = 10;
                    allEnemyShots[i, j] = 10;
                    myCharField[i, j] = '*';
                    enCharField[i, j] = '*';
                }
            }
            superpositionMyArrays();
        }

        private void superpositionMyArrays()
        {
            for (int i = 0; i < 10; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    if (S_myField.getField1()[i, j] == 1) myCharField[i, j] = '1';
                    if (S_myField.getField1()[i, j] == 2) myCharField[i, j] = '2';
                    if (S_myField.getField1()[i, j] == 3) myCharField[i, j] = '3';
                    if (S_myField.getField1()[i, j] == 4) myCharField[i, j] = '4';
                }
            }
        }

        private void enMapCorrective(int x, int y)
        {
            if (S_myField.getField2()[x, y] == 1) enCharField[x, y] = '1';
            else if (S_myField.getField2()[x, y] == 2) enCharField[x, y] = '2';
            else if (S_myField.getField2()[x, y] == 3) enCharField[x, y] = '3';
            else if (S_myField.getField2()[x, y] == 4) enCharField[x, y] = '4';
            else enCharField[x, y] = '#';
        }

        private void myMapCorrective(int x, int y)
        {
            if (S_myField.getField1()[x, y] == 1) myCharField[x, y] = 'X';
            else if (S_myField.getField1()[x, y] == 2) myCharField[x, y] = 'X';
            else if (S_myField.getField1()[x, y] == 3) myCharField[x, y] = 'X';
            else if (S_myField.getField1()[x, y] == 4) myCharField[x, y] = 'X';
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
                {
                    youWin();
                }
                if (checkMyShips() >= 10)
                {
                    winBot();
                }
                for (;adversary() != true;)
                {
                    adversary();
                }
                if (checkMyShips() >= 10)
                {
                    winBot();
                }
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

        private bool adversary()
        {
            Console.WriteLine("Ходит ваш противник.");
            int x = Program.rnd.Next(0, 10);
            for(int i = 0; i < 10; i++)
            {
                if (x == completedLong_Y_arrEnShots[i]) x = Program.rnd.Next(0, 10);
            }
            int y;
            y = generationShotCoordinates(x, allEnemyShots);
            if (y == 10) return false;
            enemyShot(x, y);
            myMapCorrective(x, y);
            return true;
        }

        private int generationShotCoordinates(int x, int[,] allEnemyShots)
        {
            int counterCompletedLong = 0;
            int y;
            y = Program.rnd.Next(0, 10);
            if (allEnemyShots[x, y] != 10)
            {
                int counter = 0;
                for (int i = 0; i < 10; i++)
                {
                    if (allEnemyShots[x, i] == 10)
                    {
                        y = i;
                        allEnemyShots[x, y] = y;
                        return y;
                    }
                    else ++counter;
                }
                if (counter == 10)
                {
                    completedLong_Y_arrEnShots[counterCompletedLong] = x;
                    ++counterCompletedLong;
                    return y = 10;
                }
                for (;allEnemyShots[x, y] != 10;)
                {
                    if (y < 9) ++y;
                    else --y;
                    if (y < 0) return y = 10;
                }
            }
            allEnemyShots[x, y] = y;
            return y;
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
        private int myArrLen = 0;

        public The_Battle()
        {
            historyMyShots = new int[200];
            for(int i = 0; i < 200; i++)
            {
                historyMyShots[i] = -1;
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

        private void setHistoryMyShots(int x, int y)
        {
            historyMyShots[myArrLen] = x;
            myArrLen++;
            historyMyShots[myArrLen] = y;
            myArrLen++;
        }

        protected void myShot(int x, int y)
        {
            for(int i = 0; i < historyMyShots.Length; i++)
            {
                if((x == historyMyShots[i])&&(y == historyMyShots[++i]))
                {
                    break;
                }
            }
            foreach (Ship_Design ship in EnemyShips)
            {
                for (int i = 0; i < ship.GetCoordinates().Length - 1; i++)
                {
                    if ((ship.GetCoordinates()[i] == x) && (ship.GetCoordinates()[++i] == y))
                    {
                        setHistoryMyShots(x, y);
                        ship.setHit();
                        Console.Beep();
                        break;
                    }
                }
            }
        }

        protected bool enemyShot(int x, int y)
        {
            foreach (Ship_Design ship in MyShips)
            {
                for (int i = 0; i < ship.GetCoordinates().Length - 1; i++)
                {
                    if ((ship.GetCoordinates()[i] == x) && (ship.GetCoordinates()[++i] == y))
                    {
                        ship.setHit();
                        Console.Beep();
                        return true;
                    }
                }
            }
            return false;
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
    }

    class Fields_Creation : All_Ships
    {
        private int[,] field1;
        private int[,] field2;

        public Fields_Creation()
        {
            createFields();
            deployingOnFields();
        }

        public void createFields()
        {
            field1 = new int[10, 10];
            field2 = new int[10, 10];
            for (int i = 0; i < 10; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    field1[i, j] = 0;
                    field2[i, j] = 0;
                }
            }
        }

        public int[,] getField1()
        {
            return field1;
        }

        public int[,] getField2()
        {
            return field2;
        }

        public Ship_Design[] getShipsOnField1()
        {
            return getAllShips1();
        }

        public Ship_Design[] getShipsOnField2()
        {
            return getAllShips2();
        }

        private void deployingOnFields()
        {
            Ship_Design[] ships1 = getAllShips1();
            Ship_Design[] ships2 = getAllShips2();
            foreach(Ship_Design ship in ships1)
            {
                for (int i = 0; i < 30; i++)
                {
                    int X = Program.rnd.Next(0, 10);
                    int Y = Program.rnd.Next(0, 10);
                    if (directionRIGHT_1(ship, X, Y) == true) break;
                    if (directionLEFT_1(ship, X, X) == true) break;
                    if (directionUP_1(ship, X, Y) == true) break;
                    if (directionDOWN_1(ship, X, Y) == true) break;
                }
            }
            foreach (Ship_Design ship in ships2)
            {
                for (int i = 0; i < 30; i++)
                {
                    int X = Program.rnd.Next(0, 10);
                    int Y = Program.rnd.Next(0, 10);
                    if (directionRIGHT_2(ship, X, Y) == true) break;
                    if (directionLEFT_2(ship, X, X) == true) break;
                    if (directionUP_2(ship, X, Y) == true) break;
                    if (directionDOWN_2(ship, X, Y) == true) break;
                }
            }
        }

        private bool directionRIGHT_1(Ship_Design ship, int x, int y)
        {
            if (y + (ship.GetValueShip() - 1) > 9) return false;
            int sectionCount = 0;
            for (int i = 0; i < ship.GetValueShip(); i++)
            {
                if (checkArea1(x, y + i) == 0)
                {
                    sectionCount++;
                }
            }

            if (sectionCount == ship.GetValueShip())
            {
                for (int i = 0; i < ship.GetValueShip(); i++)
                {
                    marking1(ship.GetValueShip(), x, y + i);
                    ship.SetCoordinates(x);
                    ship.SetCoordinates(y + i);
                }
                return true;
            }
            return false;
        }

        private bool directionRIGHT_2(Ship_Design ship, int x, int y)
        {
            if (y + (ship.GetValueShip() - 1) > 9) return false;
            int sectionCount = 0;
            for (int i = 0; i < ship.GetValueShip(); i++)
            {
                if (checkArea2(x, y + i) == 0)
                {
                    sectionCount++;
                }
            }

            if (sectionCount == ship.GetValueShip())
            {
                for (int i = 0; i < ship.GetValueShip(); i++)
                {
                    marking2(ship.GetValueShip(), x, y + i);
                    ship.SetCoordinates(x);
                    ship.SetCoordinates(y + i);
                }
                return true;
            }
            return false;
        }

        private bool directionLEFT_1(Ship_Design ship, int x, int y)
        {
            if (y - (ship.GetValueShip() - 1) < 0) return false;
            int sectionCount = 0;
            for (int i = 0; i < ship.GetValueShip(); i++)
            {
                if (checkArea1(x, y - i) == 0)
                {
                    sectionCount++;
                }
            }

            if (sectionCount == ship.GetValueShip())
            {
                for (int i = 0; i < ship.GetValueShip(); i++)
                {
                    marking1(ship.GetValueShip(), x, y - i);
                    ship.SetCoordinates(x);
                    ship.SetCoordinates(y - i);
                }
                return true;
            }
            return false;
        }

        private bool directionLEFT_2(Ship_Design ship, int x, int y)
        {
            if (y - (ship.GetValueShip() - 1) < 0) return false;
            int sectionCount = 0;
            for (int i = 0; i < ship.GetValueShip(); i++)
            {
                if (checkArea2(x, y - i) == 0)
                {
                    sectionCount++;
                }
            }

            if (sectionCount == ship.GetValueShip())
            {
                for (int i = 0; i < ship.GetValueShip(); i++)
                {
                    marking2(ship.GetValueShip(), x, y - i);
                    ship.SetCoordinates(x);
                    ship.SetCoordinates(y - i);
                }
                return true;
            }
            return false;
        }

        private bool directionUP_1(Ship_Design ship, int x, int y)
        {
            if (x - (ship.GetValueShip() - 1) < 0) return false;
            int sectionCount = 0;
            for (int i = 0; i < ship.GetValueShip(); i++)
            {
                if (checkArea1(x - i, y) == 0)
                {
                    sectionCount++;
                }
            }

            if (sectionCount == ship.GetValueShip())
            {
                for (int i = 0; i < ship.GetValueShip(); i++)
                {
                    marking1(ship.GetValueShip(), x - i, y);
                    ship.SetCoordinates(x - i);
                    ship.SetCoordinates(y);
                }
                return true;
            }
            return false;
        }

        private bool directionUP_2(Ship_Design ship, int x, int y)
        {
            if (x - (ship.GetValueShip() - 1) < 0) return false;
            int sectionCount = 0;
            for (int i = 0; i < ship.GetValueShip(); i++)
            {
                if (checkArea2(x - i, y) == 0)
                {
                    sectionCount++;
                }
            }

            if (sectionCount == ship.GetValueShip())
            {
                for (int i = 0; i < ship.GetValueShip(); i++)
                {
                    marking2(ship.GetValueShip(), x - i, y);
                    ship.SetCoordinates(x - i);
                    ship.SetCoordinates(y);
                }
                return true;
            }
            return false;
        }

        private bool directionDOWN_1(Ship_Design ship, int x, int y)
        {
            if (x + (ship.GetValueShip() - 1) > 9) return false;
            int sectionCount = 0;
            for (int i = 0; i < ship.GetValueShip(); i++)
            {
                if (checkArea1(x + i, y) == 0)
                {
                    sectionCount++;
                }
            }

            if (sectionCount == ship.GetValueShip())
            {
                for (int i = 0; i < ship.GetValueShip(); i++)
                {
                    marking1(ship.GetValueShip(), x + i, y);
                    ship.SetCoordinates(x + i);
                    ship.SetCoordinates(y);
                }
                return true;
            }
            return false;
        }

        private bool directionDOWN_2(Ship_Design ship, int x, int y)
        {
            if (x + (ship.GetValueShip() - 1) > 9) return false;
            int sectionCount = 0;
            for (int i = 0; i < ship.GetValueShip(); i++)
            {
                if (checkArea2(x + i, y) == 0)
                {
                    sectionCount++;
                }
            }

            if (sectionCount == ship.GetValueShip())
            {
                for (int i = 0; i < ship.GetValueShip(); i++)
                {
                    marking2(ship.GetValueShip(), x + i, y);
                    ship.SetCoordinates(x + i);
                    ship.SetCoordinates(y);
                }
                return true;
            }
            return false;
        }

        private int checkArea1(int x, int y)
        {
            int[] sequenceParam = new int[] { x, y, x + 1, y, x + 1, y + 1, x, y + 1, x - 1, y + 1, x - 1, y,
                x - 1, y - 1, x, y - 1, x + 1, y - 1 };
            int bufInt = 0;
            for (int i = 0; i < 18; i++)
            {
                int bufX = sequenceParam[i];
                ++i;
                int bufY = sequenceParam[i];
                bufInt += inArr1(bufX, bufY);
            }
            return bufInt;
        }

        private int checkArea2(int x, int y)
        {
            int[] sequenceParam = new int[] { x, y, x + 1, y, x + 1, y + 1, x, y + 1, x - 1, y + 1, x - 1, y, x - 1, y - 1, x, y - 1, x + 1, y - 1 };
            int bufInt = 0;
            for (int i = 0; i < 18; i++)
            {
                int bufX = sequenceParam[i];
                ++i;
                int bufY = sequenceParam[i];
                bufInt += inArr2(bufX, bufY);
            }
            return bufInt;
        }

        private int inArr1(int x, int y)
        {
            if (x > 9 || x < 0 || y > 9 || y < 0)
            {
                return 0;
            }
            else
            {
                return field1[x, y];
            }
        }

        private int inArr2(int x, int y)
        {
            if (x > 9 || x < 0 || y > 9 || y < 0)
            {
                return 0;
            }
            else
            {
                return field2[x, y];
            }
        }

        private void marking1(int cellValue, int mx, int my)
        {
            field1[mx, my] = cellValue;
        }

        private void marking2(int cellValue, int mx, int my)
        {
            field2[mx, my] = cellValue;
        }
    }

    class Ship_Design
    {
        private int valueDeck;
        private int buffer = 0;
        public int hit = 0;
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
            ++hit;
        }

        public int getCurrentStatus()
        {
            if (hit == valueDeck)
                return status = 1;
            return status;
        }
    }

    class All_Ships
    {
        protected Ship_Design[] ships1 = new Ship_Design[10];
        protected Ship_Design[] ships2 = new Ship_Design[10];
        
        public All_Ships()
        {
            ships1[0] = new Ship_Design(4);
            ships1[1] = new Ship_Design(3);
            ships1[2] = new Ship_Design(3);
            ships1[3] = new Ship_Design(2);
            ships1[4] = new Ship_Design(2);
            ships1[5] = new Ship_Design(2);
            ships1[6] = new Ship_Design(1);
            ships1[7] = new Ship_Design(1);
            ships1[8] = new Ship_Design(1);
            ships1[9] = new Ship_Design(1);

            ships2[0] = new Ship_Design(4);
            ships2[1] = new Ship_Design(3);
            ships2[2] = new Ship_Design(3);
            ships2[3] = new Ship_Design(2);
            ships2[4] = new Ship_Design(2);
            ships2[5] = new Ship_Design(2);
            ships2[6] = new Ship_Design(1);
            ships2[7] = new Ship_Design(1);
            ships2[8] = new Ship_Design(1);
            ships2[9] = new Ship_Design(1);
        }

        protected Ship_Design[] getAllShips1()
        {
            return ships1;
        }

        protected Ship_Design[] getAllShips2()
        {
            return ships2;
        }
    }
}

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
            Compiler compiler = new Compiler();
            compiler.battleLog();
            Console.ReadKey();
        }
    }
    class Compiler
    {
        private int globalX;
        private int globalY;
        private int status = 0;
        private int[,] myMap;
        private int[,] enMap;
        private char[,] myBF = new char[10, 10];
        private char[,] enBF = new char[10, 10];
        public int myHit = 0;
        public int enHit = 0;
        private static Fields_Creation newField = new Fields_Creation();

        public Compiler()
        {
            myMap = newField.getField1();
            enMap = newField.getField2();
            for(int i = 0; i < 10; i++)
            {
                for(int j = 0; j < 10; j++)
                {
                    enBF[i, j] = '*';
                    if (myMap[i, j] == 1) myBF[i, j] = '1';
                    else if (myMap[i, j] == 2) myBF[i, j] = '2';
                    else if (myMap[i, j] == 3) myBF[i, j] = '3';
                    else if (myMap[i, j] == 4) myBF[i, j] = '4';
                    else myBF[i, j] = '*';
                }
            }
        }

        private void myInterface()
        {
            int x;
            int y;
            try
            {
                Console.WriteLine("Введите значение по оси Х [0 – 9]");
                x = Convert.ToInt32(Console.ReadLine());
                Console.WriteLine("Введите значение по оси Y [0 – 9]");
                y = Convert.ToInt32(Console.ReadLine());
                if(x < 0 || x > 9 || y < 0 || y > 9)
                {
                    Console.WriteLine("Вы ввели не верное значение.");
                    myInterface();
                }
                if (enBF[x, y] != '*') return;
                else
                {
                    if (enMap[x, y] == 1)
                    {
                        enBF[x, y] = '1';
                        enHit++;
                        Console.Beep();
                    }
                    else if (enMap[x, y] == 2)
                    {
                        enBF[x, y] = '2';
                        enHit++;
                        Console.Beep();
                    }
                    else if (enMap[x, y] == 3)
                    {
                        enBF[x, y] = '3';
                        enHit++;
                        Console.Beep();
                    }
                    else if (enMap[x, y] == 4)
                    {
                        enBF[x, y] = '4';
                        enHit++;
                        Console.Beep();
                    }
                    else
                    {
                        enBF[x, y] = '#';
                    }
                }
            }
            catch
            {
                Console.WriteLine("Вы ввели не верное значение.");
                myInterface();
            }
            
        }

        private void enShot()
        {
            if (status == 0)
            {
                globalX = Program.rnd.Next(0, 10);
                globalY = Program.rnd.Next(0, 10);
            }
            if (myBF[globalX, globalY] == 'X' || myBF[globalX, globalY] == '#') { enShot(); }
            else
            {
                if(myMap[globalX, globalY] == 1)
                {
                    finishHim(1, globalX, globalY);
                }
                else if (myMap[globalX, globalY] == 2)
                {
                    finishHim(2, globalX, globalY);
                }
                else if (myMap[globalX, globalY] == 3)
                {
                    finishHim(3, globalX, globalY);
                }
                else if (myMap[globalX, globalY] == 4)
                {
                    finishHim(4, globalX, globalY);
                }
                else
                {
                    status = 0;
                    myBF[globalX, globalY] = '#';
                }
            }
        }

        private void finishHim(int val, int x, int y)
        {
            if(val == 1)
            {
                myHit++;
                Console.Beep();
                myBF[x, y] = 'X';
                if(x - 1 >= 0) myBF[x -1, y] = '#';
                if(x - 1 >= 0 && y + 1 < 10) myBF[x - 1, y+1] = '#';
                if (y + 1 < 10) myBF[x, y+1] = '#';
                if (x + 1 < 10 && y + 1 < 10) myBF[x + 1, y+1] = '#';
                if (x + 1 < 10) myBF[x + 1, y] = '#';
                if (x + 1 < 10 && y - 1 >= 0) myBF[x + 1, y-1] = '#';
                if (y - 1 >= 0) myBF[x, y - 1] = '#';
                if (x - 1 >= 0 && y - 1 >= 0) myBF[x - 1, y - 1] = '#';
            }
            else if(val == 2)
            {
                if (x - 1 >= 0 && myMap[x - 1, y] == 2)
                {
                    myBF[x, y] = 'X';
                    myHit++;
                    Console.Beep();
                    globalX = x - 1;
                    globalY = y;
                    status++;
                    if (status == 2)
                        status = 0;
                }
                else if (x + 1 < 10 && myMap[x + 1, y] == 2)
                {
                    myBF[x, y] = 'X';
                    myHit++;
                    Console.Beep();
                    globalX = x + 1;
                    globalY = y;
                    status++;
                    if (status == 2)
                        status = 0;
                }
                else if (y - 1 >= 0 && myMap[x, y - 1] == 2)
                {
                    myBF[x, y] = 'X';
                    myHit++;
                    Console.Beep();
                    globalX = x;
                    globalY = y - 1;
                    status++;
                    if (status == 2)
                        status = 0;
                }
                else if (y + 1 < 10 && myMap[x, y + 1] == 2)
                {
                    myBF[x, y] = 'X';
                    myHit++;
                    Console.Beep();
                    globalX = x;
                    globalY = y + 1;
                    status++;
                    if (status == 2)
                        status = 0;
                }

                else status = 0;
            }
            else if(val == 3)
            {
                if (x - 1 < 0 && myMap[x + 1, y] == 3)
                {
                    status++;
                    myBF[x, y] = 'X';
                    myHit++;
                    Console.Beep();
                    if (status == 3)
                    {
                        status = 0;
                        return;
                    }
                    globalX = x + status;
                    globalY = y;
                }
                else if (x + 1 > 9 && myMap[x - 1, y] == 3)
                {
                    status++;
                    myBF[x, y] = 'X';
                    myHit++;
                    Console.Beep();
                    if (status == 3)
                    {
                        status = 0;
                        return;
                    }
                    globalX = x - status;
                    globalY = y;
                }
                else if (y - 1 < 0 && myMap[x, y + 1] == 3)
                {
                    status++;
                    myBF[x, y] = 'X';
                    myHit++;
                    Console.Beep();
                    if (status == 3)
                    {
                        status = 0;
                        return;
                    }
                    globalX = x;
                    globalY = y + status;
                }
                else if (y + 1 > 9 && myMap[x, y - 1] == 3)
                {
                    status++;
                    myBF[x, y] = 'X';
                    myHit++;
                    Console.Beep();
                    if (status == 3)
                    {
                        status = 0;
                        return;
                    }
                    globalX = x;
                    globalY = y - status;
                }

                else if (x - 1 >= 0 && myMap[x - 1, y] != 3 && x + 1 < 10 && myMap[x + 1, y] == 3)
                {
                    status++;
                    myBF[x, y] = 'X';
                    myHit++;
                    Console.Beep();
                    if (status == 3)
                    {
                        status = 0;
                        return;
                    }
                    globalX = x + status;
                    globalY = y;
                }
                else if (x + 1 < 10 && myMap[x + 1, y] != 3 && x - 1 >= 0 && myMap[x - 1, y] == 3)
                {
                    status++;
                    myBF[x, y] = 'X';
                    myHit++;
                    Console.Beep();
                    if (status == 3)
                    {
                        status = 0;
                        return;
                    }
                    globalX = x - status;
                    globalY = y;
                }
                else if (y - 1 >= 0 && myMap[x, y - 1] != 3 && y + 1 < 10 && myMap[x, y + 1] == 3)
                {
                    status++;
                    myBF[x, y] = 'X';
                    myHit++;
                    Console.Beep();
                    if (status == 3)
                    {
                        status = 0;
                        return;
                    }
                    globalX = x;
                    globalY = y + status;
                }
                else if (y + 1 < 10 && myMap[x, y + 1] != 3 && y - 1 >= 0 && myMap[x, y - 1] == 3)
                {
                    status++;
                    myBF[x, y] = 'X';
                    myHit++;
                    Console.Beep();
                    if (status == 3)
                    {
                        status = 0;
                        return;
                    }
                    globalX = x;
                    globalY = y - status;
                }

                else if (x - 1 >= 0 && y - 1 < 0 || y + 1 > 9)
                {
                    status++;
                    myBF[x, y] = 'X';
                    myHit++;
                    Console.Beep();
                    if (status == 3)
                    {
                        status = 0;
                        return;
                    }
                    globalX = x - 1;
                    globalY = y;
                }
                else if (x + 1 < 10 && y - 1 < 0 || y + 1 > 9)
                {
                    status++;
                    myBF[x, y] = 'X';
                    myHit++;
                    Console.Beep();
                    if (status == 3)
                    {
                        status = 0;
                        return;
                    }
                    globalX = x + 1;
                    globalY = y;
                }
                else if (y - 1 >= 0 && x - 1 < 0 || x + 1 > 9)
                {
                    status++;
                    myBF[x, y] = 'X';
                    myHit++;
                    Console.Beep();
                    if (status == 3)
                    {
                        status = 0;
                        return;
                    }
                    globalX = x;
                    globalY = y - 1;
                }
                else if (y + 1 < 10 && x - 1 < 0 || x + 1 > 9)
                {
                    status++;
                    myBF[x, y] = 'X';
                    myHit++;
                    Console.Beep();
                    if (status == 3)
                    {
                        status = 0;
                        return;
                    }
                    globalX = x;
                    globalY = y + 1;
                }

                else if (x - 1 >= 0 && myMap[x, y - 1] != 3 && myMap[x, y + 1] != 3)
                {
                    status++;
                    myBF[x, y] = 'X';
                    myHit++;
                    Console.Beep();
                    if (status == 3)
                    {
                        status = 0;
                        return;
                    }
                    globalX = x - 1;
                    globalY = y;
                }
                else if (x + 1 < 10 && myMap[x, y - 1] != 3 && myMap[x, y + 1] != 3)
                {
                    status++;
                    myBF[x, y] = 'X';
                    myHit++;
                    Console.Beep();
                    if (status == 3)
                    {
                        status = 0;
                        return;
                    }
                    globalX = x + 1;
                    globalY = y;
                }
                else if (y - 1 >= 0 && myBF[x, y - 1] != 'X' && myMap[x + 1, y] != 3 && myMap[x - 1, y] != 3)
                {
                    status++;
                    myBF[x, y] = 'X';
                    myHit++;
                    Console.Beep();
                    if (status == 3)
                    {
                        status = 0;
                        return;
                    }
                    globalX = x;
                    globalY = y - 1;
                }
                else if (y + 1 < 10 && myBF[x, y + 1] != 'X' && myMap[x + 1, y] != 3 && myMap[x - 1, y] != 3)
                {
                    status++;
                    myBF[x, y] = 'X';
                    myHit++;
                    Console.Beep();
                    if (status == 3)
                    {
                        status = 0;
                        return;
                    }
                    globalX = x;
                    globalY = y + 1;
                }

                else status = 0;
            }
            else if (val == 4)
            {
                if (x - 1 < 0 && myMap[x + 1, y] == 4)
                {
                    status++;
                    myBF[x, y] = 'X';
                    myHit++;
                    Console.Beep();
                    if (status == 4)
                    {
                        status = 0;
                        return;
                    }
                    globalX = x + status;
                    globalY = y;
                }
                else if (x + 1 > 9 && myMap[x - 1, y] == 4)
                {
                    status++;
                    myBF[x, y] = 'X';
                    myHit++;
                    Console.Beep();
                    if (status == 4)
                    {
                        status = 0;
                        return;
                    }
                    globalX = x - status;
                    globalY = y;
                }
                else if (y - 1 < 0 && myMap[x, y + 1] == 4)
                {
                    status++;
                    myBF[x, y] = 'X';
                    myHit++;
                    Console.Beep();
                    if (status == 4)
                    {
                        status = 0;
                        return;
                    }
                    globalX = x;
                    globalY = y + status;
                }
                else if (y + 1 > 9 && myMap[x, y - 1] == 4)
                {
                    status++;
                    myBF[x, y] = 'X';
                    myHit++;
                    Console.Beep();
                    if (status == 4)
                    {
                        status = 0;
                        return;
                    }
                    globalX = x;
                    globalY = y - status;
                }

                else if (x - 1 >= 0 && myMap[x - 1, y] != 4 && x + 1 < 10 && myMap[x + 1, y] == 4)
                {
                    status++;
                    myBF[x, y] = 'X';
                    myHit++;
                    Console.Beep();
                    if (status == 4)
                    {
                        status = 0;
                        return;
                    }
                    globalX = x + status;
                    globalY = y;
                }
                else if (x + 1 < 10 && myMap[x + 1, y] != 4 && x - 1 >= 0 && myMap[x - 1, y] == 4)
                {
                    status++;
                    myBF[x, y] = 'X';
                    myHit++;
                    Console.Beep();
                    if (status == 4)
                    {
                        status = 0;
                        return;
                    }
                    globalX = x - status;
                    globalY = y;
                }
                else if (y - 1 >= 0 && myMap[x, y - 1] != 4 && y + 1 < 10 && myMap[x, y + 1] == 4)
                {
                    status++;
                    myBF[x, y] = 'X';
                    myHit++;
                    Console.Beep();
                    if (status == 4)
                    {
                        status = 0;
                        return;
                    }
                    globalX = x;
                    globalY = y + status;
                }
                else if (y + 1 < 10 && myMap[x, y + 1] != 4 && y - 1 >= 0 && myMap[x, y - 1] == 4)
                {
                    status++;
                    myBF[x, y] = 'X';
                    myHit++;
                    Console.Beep();
                    if (status == 4)
                    {
                        status = 0;
                        return;
                    }
                    globalX = x;
                    globalY = y - status;
                }

                else if (x - 1 >= 0 && y - 1 < 0 || y + 1 > 9)
                {
                    status++;
                    myBF[x, y] = 'X';
                    myHit++;
                    Console.Beep();
                    if (status == 4)
                    {
                        status = 0;
                        return;
                    }
                    globalX = x - 1;
                    globalY = y;
                }
                else if (x + 1 < 10 && y - 1 < 0 || y + 1 > 9)
                {
                    status++;
                    myBF[x, y] = 'X';
                    myHit++;
                    Console.Beep();
                    if (status == 4)
                    {
                        status = 0;
                        return;
                    }
                    globalX = x + 1;
                    globalY = y;
                }
                else if (y - 1 >= 0 && x - 1 < 0 || x + 1 > 9)
                {
                    status++;
                    myBF[x, y] = 'X';
                    myHit++;
                    Console.Beep();
                    if (status == 4)
                    {
                        status = 0;
                        return;
                    }
                    globalX = x;
                    globalY = y - 1;
                }
                else if (y + 1 < 10 && x - 1 < 0 || x + 1 > 9)
                {
                    status++;
                    myBF[x, y] = 'X';
                    myHit++;
                    Console.Beep();
                    if (status == 4)
                    {
                        status = 0;
                        return;
                    }
                    globalX = x;
                    globalY = y + 1;
                }

                else if (x - 1 >= 0 && myMap[x, y - 1] != 4 && myMap[x, y + 1] != 4)
                {
                    status++;
                    myBF[x, y] = 'X';
                    myHit++;
                    Console.Beep();
                    if (status == 4)
                    {
                        status = 0;
                        return;
                    }
                    globalX = x - 1;
                    globalY = y;
                }
                else if (x + 1 < 10 && myMap[x, y - 1] != 4 && myMap[x, y + 1] != 4)
                {
                    status++;
                    myBF[x, y] = 'X';
                    myHit++;
                    Console.Beep();
                    if (status == 4)
                    {
                        status = 0;
                        return;
                    }
                    globalX = x + 1;
                    globalY = y;
                }
                else if (y - 1 >= 0 && myBF[x, y - 1] != 'X' && myMap[x + 1, y] != 4 && myMap[x - 1, y] != 4)
                {
                    status++;
                    myBF[x, y] = 'X';
                    myHit++;
                    Console.Beep();
                    if (status == 4)
                    {
                        status = 0;
                        return;
                    }
                    globalX = x;
                    globalY = y - 1;
                }
                else if (y + 1 < 10 && myBF[x, y + 1] != 'X' && myMap[x + 1, y] != 4 && myMap[x - 1, y] != 4)
                {
                    status++;
                    myBF[x, y] = 'X';
                    myHit++;
                    Console.Beep();
                    if (status == 4)
                    {
                        status = 0;
                        return;
                    }
                    globalX = x;
                    globalY = y + 1;
                }

                else status = 0;
            }
        }

        public void battleLog()
        {
            while (enHit != 20 || myHit != 20)
            {
                Console.Clear();
                for (int i = 0; i < 10; i++)
                {
                    for (int j = 0; j < 10; j++)
                    {
                        Console.Write(myBF[i, j] + " ");
                    }
                    Console.Write("\t");
                    for (int k = 0; k < 10; k++)
                    {
                        Console.Write(enBF[i, k] + " ");
                    }
                    Console.WriteLine();
                }
                Console.WriteLine(enHit);
                Console.WriteLine(myHit);
                myInterface();
                if (enHit == 20)
                {
                    Console.Clear();
                    Console.Beep(); Console.Beep(); Console.Beep(); Console.Beep(); Console.Beep();
                    Console.WriteLine("Поздравляю, вы победили!");
                    Console.ReadKey();
                    Environment.Exit(0);

                }
                enShot();
                if (myHit == 20)
                {
                    Console.Clear();
                    Console.Beep(); Console.Beep(); Console.Beep(); Console.Beep(); Console.Beep();
                    Console.WriteLine("Вы проиграли.");
                    Console.ReadKey();
                    Environment.Exit(0);

                }
            }
        }
    }

    partial class Fields_Creation
    {
        private int[,] field1;

        public int[,] getField1()
        {
            return field1;
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
                bufInt += checkingContentArr1(bufX, bufY);
            }
            return bufInt;
        }

        private int checkingContentArr1(int x, int y)
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

        private void marking1(int cellValue, int mx, int my)
        {
            field1[mx, my] = cellValue;
        }
    }

    partial class Fields_Creation
    {
        private int[,] field2;

        public int[,] getField2()
        {
            return field2;
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

        private int checkArea2(int x, int y)
        {
            int[] sequenceParam = new int[] { x, y, x + 1, y, x + 1, y + 1, x, y + 1, x - 1, y + 1, x - 1, y, x - 1, y - 1, x, y - 1, x + 1, y - 1 };
            int bufInt = 0;
            for (int i = 0; i < 18; i++)
            {
                int bufX = sequenceParam[i];
                ++i;
                int bufY = sequenceParam[i];
                bufInt += checkingContentArr2(bufX, bufY);
            }
            return bufInt;
        }

        private int checkingContentArr2(int x, int y)
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

        private void marking2(int cellValue, int mx, int my)
        {
            field2[mx, my] = cellValue;
        }
    }

    partial class Fields_Creation : All_Ships
    {
        public Fields_Creation()
        {
            createFields();
            deployingOnFields();
        }

        private void createFields()
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

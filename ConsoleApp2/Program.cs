using System;
using System.CodeDom;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp2
{
    class Program
    {
        static void Main(string[] args)
        {

            StartGame();

            Console.ReadLine();
        }

        private static void StartGame()
        {
            Console.WriteLine("Игра крестики нолики");
            bool gemer = true; // первые  ходят крести
            string message;
            int[,] mas = new int[3, 3];

            while (true)
            {
                if (isWin(mas, out message))
                {
                    Console.WriteLine("игра окончина {0} ", message);
                    break;
                }

                PrintBord(mas); // печатать  доску
                mas = GoTo(mas, gemer); // Ходить  
                PrintBord(mas); // печатать  доску

                gemer = !gemer; // поменяем ход

            };
        }

        private static int[,] GoTo(int[,] mas, bool gemer)
        {
            if (gemer == true) // крестики
            {
                Console.WriteLine("Ходят  крестики");
            }
            else
            {
                Console.WriteLine("Ходят  нолики");
            }

            Console.WriteLine("укажите координату");

            string xod = Console.ReadLine();

            int i; int j;


            if (isXod(xod, mas.GetLongLength(0), mas.GetLongLength(1), out i, out j)) // если координаты верные 
            {
                if (isXodBord(i, j, mas)) // если ход  возможен  
                {
                    if (gemer == true) // если крестики
                    {
                        mas[i, j] = 1;
                    }
                    else // если нолики
                    {
                        mas[i, j] = 2;
                    }
                }
                else
                {
                    Console.WriteLine(" координата занята ");
                    GoTo(mas, gemer);
                }
            }
            else
            {
                Console.WriteLine("координата неверная");
                GoTo(mas, gemer);
            }

            return mas;

        }

        private static bool isXodBord(int i, int j, int[,] mas)
        {

            if (mas[i, j] == 0)
            {
                return true;
            }
            else
            {
                return false;
            }

        }

        private static bool isXod(string xod, long y, long x, out int i, out int j)
        {
            int a = 0; int b = 0; i = 0; j = 0;

            string[] vs = xod.Split(' ');

            try
            {
                a = Convert.ToInt32(vs[0]) - 1;
                b = Convert.ToInt32(vs[1]) - 1;
            }
            catch
            {
                return false;
            }

            if (a >= 0 && a <= y && b >= 0 && b <= x)
            {
                i = a;
                j = b;
                return true;
            }
            return false;

        }

        private static bool isWin(int[,] mas, out string name)
        {
            name = "";

            // строки
            for (int i = 0; i < mas.GetLength(0); i++) // есть ли ход 
            {
                int sum = 0;
                bool flag = true;

                for (int j = 0; j < mas.GetLength(1); j++)
                {
                    if  (mas[i,j] == 0)
                    {
                        flag = false;
                        break;
                    }
                    sum += mas[i, j];
                }

                if (flag == false)
                {
                    continue;
                }

                if ((double)sum / mas.GetLength(1) == 1 )
                {
                    name = "Победили крестики"; 
                    return true;
                }

                if ((double) sum / mas.GetLength(1) == 2)
                {
                    name = "Победили нолики"; 
                    return true;
                }

            }
            // столбцы
            for (int i = 0; i < mas.GetLength(1); i++) // есть ли ход 
            {
                int sum = 0;
                bool flag = true;

                for (int j = 0; j < mas.GetLength(0); j++)
                {
                    if (mas[j, i] == 0)
                    {
                        flag = false;
                        break;
                    }
                    sum += mas[j, i];
                }

                if (flag == false)
                {
                    continue;
                }

                if ((double)sum / mas.GetLength(0) == 1)
                {
                    name = "Победили крестики";
                    return true;
                }

                if ((double)sum / mas.GetLength(0) == 2)
                {
                    name = "Победили нолики";
                    return true;
                }

            }

            // диагонали

            int sumD = 0;
            int SumDobr = 0;

            for (int i = 0; i < mas.GetLength(0); i++) // есть 
            {
                if (mas[i,i] == 0)
                {
                    break;
                }

                sumD += mas[i, i];
            }

            for (int i = 0; i < mas.GetLength(0); i++) // есть 
            {
                if (mas[(mas.GetLength(0)-1)- i, (mas.GetLength(0) - 1) - i] == 0)
                {
                    break;
                }

                sumD += mas[(mas.GetLength(0) - 1) - i, (mas.GetLength(0) - 1) - i];
            }


            if ((double)sumD / mas.GetLength(0) == 1 || (double)SumDobr / mas.GetLength(0)==1)
            {
                name = "Победили крестики";
                return true;
            }

            if ((double)sumD / mas.GetLength(0) == 2 || (double)SumDobr / mas.GetLength(0) == 2)
            {
                name = "Победили нолики";
                return true;
            }

            for (int i = 0; i < mas.GetLength(0); i++) // есть ли ход 
            {
                for (int j = 0; j < mas.GetLength(1); j++)
                {
                    if (mas[i, j] == 0)
                    {
                        return false;
                    }
                }
            }
            
            name = "Ничья";
                return false;
        }

        private static void PrintBord(int[,] mas)
        {
               Console.Clear();

               for (int i = 0; i <= mas.GetLength(1); i++)
               {
                  if (i == 0)
                  {
                     Console.Write("   "); continue;
                  }

                    Console.Write(" {0}|", i);
               }
               Console.WriteLine();

              for (int i = 0; i < mas.GetLength(0); i++)
              {
                 for (int j = 0; j < mas.GetLength(0); j++)
                 {
                     if (j == 0)
                     {
                         Console.Write(" {0}|", i + 1);
                     }

                     if (mas[i, j] == 0)
                        {
                            Console.Write(" - ");
                        }

                     if (mas[i, j] == 1)
                        {
                            Console.Write(" X ");
                        }

                     if (mas[i, j] == 2)
                        {
                            Console.Write(" O ");
                        }
                 }
                 Console.WriteLine();
              }
             Console.WriteLine("Введите координату по  принцепу  строка  и  столбец (например 0 0)");
        }
    }
}

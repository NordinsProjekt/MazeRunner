using System;
using System.Threading;

namespace MazeRunner
{
    class Program
    {
        static int n = 25;
        static int[,] cube = BuildLabyrinthChaos(n, n);
        static bool[,] cube_facit = new bool[n, n];
        static string exit = "";
        static string start = "";
        static bool found = false;
        static int rowLength = cube.GetLength(0);
        static int colLength = cube.GetLength(1);
        static void Main(string[] args)
        {
            TheCube();
        }
        static void TheCube()
        {
            //MazeSolver via recursion
            //Visar hur programmet ritar grafiskt.
            for (int col = 0; col < colLength; col++)
            {
                if (cube[0, col].Equals(2))
                {
                    start = "0," + col.ToString();
                    cube_facit[0, col] = true;
                    RushToExit(1, col, "down");
                    if (found)
                        break;
                    //Console.WriteLine("Position: " + start.ToString() + " misslyckades");
                }
            }
            PrintTabellSame(ConsoleColor.Red);
            Console.WriteLine("Start pos: " + start.ToString());
            Console.WriteLine("Exit pos: " + exit.ToString());
        }
        static void PrintTabellSame(ConsoleColor userColor = ConsoleColor.White)
        {
            Console.SetCursorPosition(0, 0);
            Console.WriteLine("Skriver ut Matris");
            for (int i = 0; i < rowLength; i++)
            {
                for (int j = 0; j < colLength; j++)
                {
                    if (cube_facit[i, j])
                    {
                        Console.ForegroundColor = userColor;
                        Console.Write("{0,2}", cube[i, j]);
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Gray;
                        Console.Write("{0,2}", cube[i, j]);
                    }

                }
                Console.Write("\n");
            }
            Console.ForegroundColor = ConsoleColor.White;
        }
        static void RushToExit(int row, int col, string pos)
        {
            if (row < 0 || col < 0 || col > colLength - 1 || row > rowLength - 1 || cube_facit[row, col] == true || found == true)
                return;
            if (!cube[row, col].Equals(2))
                return;
            cube_facit[row, col] = true;
            if (row >= rowLength - 1 && cube[row, col].Equals(2)) //Hittade vi ut?
            {
                exit = row.ToString() + "," + col.ToString();
                found = true;
                return;
            }
            RitaUppRealtid();
            switch (pos)
            {
                case "down":
                    RushToExit(row + 1, col, "down");
                    RushToExit(row, col + 1, "right");
                    RushToExit(row, col - 1, "left");
                    break;
                case "left":
                    RushToExit(row, col - 1, "left");
                    RushToExit(row + 1, col, "down");
                    break;
                case "right":
                    RushToExit(row, col + 1, "right");
                    RushToExit(row + 1, col, "down");
                    break;
            }
        }
        public static int[,] BuildLabyrinthChaos(int n, int m)
        {
            Random rnd = new Random();

            int[,] Labyrinth = new int[n, m];
            for (int row = 0; row < n; row++)
                for (int col = 0; col < m; col++)
                {
                    if (col == 0 || col == m - 1)
                        Labyrinth[row, col] = 1;
                    else
                    {
                        if (rnd.Next(1, 3 + 1) == 1)
                            Labyrinth[row, col] = 1;
                        else
                            Labyrinth[row, col] = 2;
                    }
                }
            return Labyrinth;
        }
        static void RitaUppRealtid()
        {
            Thread.Sleep(50);
            PrintTabellSame(ConsoleColor.Red);
        }
    }
}

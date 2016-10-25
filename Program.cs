using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication1
{
    class Program
    {
        static void Main(string[] args)
        {
            Sudoku sudo = new Sudoku();
            sudo.Solve();
            Console.ReadLine();
        }
    }

    class Sudoku
    {
        int[,] sudoku;

        public Sudoku()
        {
            sudoku = new int[9, 9] { {3,0,0,0,1,0,0,5,0},
                                     {0,0,9,0,0,0,4,6,1},
                                     {4,6,0,0,0,0,0,0,0},
                                     {5,3,0,7,0,2,0,4,0},
                                     {0,7,0,0,0,0,0,3,0},
                                     {0,8,0,1,0,4,0,7,5},
                                     {0,0,0,0,0,0,0,8,6},
                                     {6,1,5,0,0,0,2,0,0},
                                     {0,4,0,0,2,0,0,0,3} };
        }

        public void Solve()
        {
            if (SolveSudoku(sudoku) == true)
                PrintSudoku(sudoku);
        }

        bool SolveSudoku(int[,] grid)
        {
            int row=0, col=0;

            if (!FindUnassignedLocation(grid, ref row, ref col))
            {
                return true;
            }

            for (int num = 1; num <= 9; num++)
            {
                if (isSafe(grid, row, col, num))
                {
                    grid[row,col] = num;

                    if (SolveSudoku(grid))
                    {                        
                        return true;
                    }
                    grid[row,col] = 0;
                }
            }
            return false; // this triggers backtracking
        }

        bool FindUnassignedLocation(int[,] grid, ref int row, ref int col)
        {
            for (row = 0; row < 9; row++)
                for (col = 0; col < 9; col++)
                    if  (grid[row,col] == 0)
                        return true;
            return false;
        }

        bool SquareHasNumber (int[,] copy, int x, int y, int num)
        {
            //int xs = x;
            //int ys = y;
            //while (y <= ys + 2)
            //{
            //    x = xs;
            //    while (x <= xs + 2)
            //    {
            //        if (copy[x, y] == num)
            //        {
            //            return true;
            //        }
            //        x++;
            //    }
            //    y++;
            //}
            //return false;
            for (int row = 0; row < 3; row++)
                for (int col = 0; col < 3; col++)
                    if (copy[row + x,col + y] == num)
                        return true;
            return false;
        }

        bool RowHasNumber (int[,] copy, int y, int num)
        {
            for(int i =0; i<9; i++)
            {
                if(copy[y,i]== num)
                {
                    return true;
                }
            }
            return false;
        }

        bool ColumnHasNumber (int[,] copy, int x, int num)
        {
            for (int i = 0; i < 9; i++)
            {
                if (copy[i, x] == num)
                {
                    return true;
                }
            }
            return false;
        }

        bool isSafe(int[,] grid, int row, int col, int num)
        {
            return !RowHasNumber(grid, row, num) &&
                   !ColumnHasNumber(grid, col, num) &&
                   !SquareHasNumber(grid, row - row % 3, col - col % 3, num);
        }

        void PrintSudoku(int[,] sudoku)
        {
            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    Console.Write (" " +sudoku[i, j] + " ");
                }
                Console.WriteLine();
            }
        }
    }
}

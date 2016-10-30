using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SudokuSolver
{
    class Sudoku: INotifyPropertyChanged
    {
        private int[][] _sudoku;

        public int[][] sudoku
        {
            get
            {
                return _sudoku;
            }
            set
            {
                _sudoku = value;
                OnPropertyChanged("sudoku");
            }
        }

        public Sudoku()
        {
            sudoku = new int[9][]{   new int[9]{3,0,0,0,1,0,0,5,0},
                                     new int[9]{0,0,9,0,0,0,4,6,1},
                                     new int[9]{4,6,0,0,0,0,0,0,0},
                                     new int[9]{5,3,0,7,0,2,0,4,0},
                                     new int[9]{0,7,0,0,0,0,0,3,0},
                                     new int[9]{0,8,0,1,0,4,0,7,5},
                                     new int[9]{0,0,0,0,0,0,0,8,6},
                                     new int[9]{6,1,5,0,0,0,2,0,0},
                                     new int[9]{0,4,0,0,2,0,0,0,3} };
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged(string PropertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(PropertyName));
        }

        public void Solve()
        {
            SolveSudoku(sudoku);
        }

        public int[][] SolveReturn()
        {
            return _sudoku;
        }

        bool SolveSudoku(int[][] grid)
        {
            int row = 0, col = 0;

            if (!FindUnassignedLocation(grid, ref row, ref col))
            {
                return true;
            }

            for (int num = 1; num <= 9; num++)
            {
                if (isSafe(grid, row, col, num))
                {
                    grid[row][col] = num;

                    if (SolveSudoku(grid))
                    {
                        return true;
                    }
                    grid[row][col] = 0;
                }
            }
            return false; // this triggers backtracking
        }

        bool FindUnassignedLocation(int[][] grid, ref int row, ref int col)
        {
            for (row = 0; row < 9; row++)
                for (col = 0; col < 9; col++)
                    if (grid[row][col] == 0)
                        return true;
            return false;
        }

        bool SquareHasNumber(int[][] copy, int x, int y, int num)
        {
            for (int row = 0; row < 3; row++)
                for (int col = 0; col < 3; col++)
                    if (copy[row + x][col + y] == num)
                        return true;
            return false;
        }

        bool RowHasNumber(int[][] copy, int y, int num)
        {
            for (int i = 0; i < 9; i++)
            {
                if (copy[y][i] == num)
                {
                    return true;
                }
            }
            return false;
        }

        bool ColumnHasNumber(int[][] copy, int x, int num)
        {
            for (int i = 0; i < 9; i++)
            {
                if (copy[i][x] == num)
                {
                    return true;
                }
            }
            return false;
        }

        bool isSafe(int[][] grid, int row, int col, int num)
        {
            return !RowHasNumber(grid, row, num) &&
                   !ColumnHasNumber(grid, col, num) &&
                   !SquareHasNumber(grid, row - row % 3, col - col % 3, num);
        }

    }
}

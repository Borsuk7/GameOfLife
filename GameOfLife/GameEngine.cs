using System;
using System.Collections.Generic;
using System.Text;

namespace GameOfLife
{
    class GameEngine
    {

        public uint CurrentGeneration { get; private set; }
        private bool[,] field;
        private readonly int rows;
        private readonly int cols;        

        public GameEngine(int rows, int cols, int density)
        {
            this.rows = rows;
            this.cols = cols;
            field = new bool[cols, rows];
            var random = new Random();

            for (var x = 0; x < cols; x++)
            {
                for (var y = 0; y < rows; y++)
                {
                    field[x, y] = random.Next(density) == 0;
                }
            }
        }
        public void NextGeneration()
        {
            var newField = new bool[cols, rows];

            for (var x = 0; x < cols; x++)
            {
                for (var y = 0; y < rows; y++)
                {
                    var neighboursCount = CountNeighbours(x, y);
                    var hasLife = field[x, y];

                    if (!hasLife && neighboursCount == 3)
                        newField[x, y] = true;
                    else if (hasLife && (neighboursCount < 2 || neighboursCount > 3))

                        newField[x, y] = false;
                    else
                        newField[x, y] = field[x, y];

                }
            }
            field = newField;
            CurrentGeneration++;

        }
        public bool[,] GetCurrentGeneration()
        {
            var result = new bool[cols, rows];

            for (var x = 0; x < cols; x++)
            {
                for (var y = 0; y < rows; y++)
                {
                    result[x, y] = field[x, y];
                }

            }
            return result;
        }
        private int CountNeighbours(int x, int y)
        {
            var count = 0;

            for (var i = -1; i < 2; i++)
            {
                for (var j = -1; j < 2; j++)
                {
                    var col = (x + i + cols) % cols;
                    var row = (y + j + rows) % rows;

                    var isSelfChecking = col == x && row == y;
                    var hasLife = field[col, row];

                    if (hasLife && !isSelfChecking)
                        count++;
                }

            }
            return count;
        }
        private bool ValidateCellPosition(int x, int y) => x >= 0 && y >= 0 && x < cols && y < rows;
        private void UpdateCell(int x, int y, bool state)
        {
            if (ValidateCellPosition(x, y))
                field[x, y] = state;
        }
        public void AddCell(int x, int y)
        {
            UpdateCell(x, y, true);
        }
        public void RemoveCell(int x, int y)
        {
            UpdateCell(x, y, false);
        }



    }
}



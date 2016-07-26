using CPT373_AS2.Models;
using System;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CPT373_AS2
{
    public class GameOfLife : IGameOfLife
    {
        public GameOfLife(int height, int width)
        {
            if (validateParamters(height) == 1 ||
                validateParamters(width) == 1)
            {
                throw new ArgumentException
                    ("Invalid Game dimension parameter");
            }
            else
            {
                Height = height;
                Width = width;
            }
            initialiseCells();
        }

        public int Height { get; set; }
        public int Width { get; set; }
        public Cell[][] Cells { get; set; }

        private int validateParamters(int p)
        {
            if (p <= 0)
            {
                return 1;
            }
            return 0;
        }

        // initialise the Game such that all cells
        // are blank i.e.'dead'
        private void initialiseCells()
        {
            Cells = new Cell[Height][];

            for (int i = 0; i < Cells.Length; i++)
            {
                Cells[i] = new Cell[Width];
                for (int j = 0; j < Cells[i].Length; j++)
                {
                    Cells[i][j] = Cell.Dead;
                }
            }
        }

        public void InsertTemplate
            (ITemplate template,
            int templateX,
            int templateY)
        {
            int l = templateY;

            for (int i = 0; i < template.Cells.Length; i++, l++)
            {
                int k = templateX;
                for (int j = 0; j < template.Cells[i].Length; j++, k++)
                {
                    Cells[l][k] = template.Cells[i][j];
                }
            }

        }

        // PlayGame() code adapted from CPT373/TaskDemo
        public void PlayGame()
        {
            CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();
            Task playGameTask = PlayGameAsync(cancellationTokenSource.Token);

            // Wait for user to stop the 
            Console.ReadKey(true);

            cancellationTokenSource.Cancel();
            try
            {
                playGameTask.Wait(cancellationTokenSource.Token);
            }
            catch (OperationCanceledException)
            { }

            Console.WriteLine("Game stopped.");

            // call method to serialise the Game into JSON
            Utility.saveGame(this);
        }

        // PlayGameAsync() code adapted from CPT373/TaskDemo 
        private async Task PlayGameAsync
            (CancellationToken cancellationToken)
        {
            while (!cancellationToken.IsCancellationRequested)
            {
                Console.Clear();
                Console.Write(ToString());
                Console.WriteLine("Press any key to stop ..");

                try
                {
                    await Task.Delay(TimeSpan.FromSeconds(1), cancellationToken);
                    TakeTurn();
                }
                catch (TaskCanceledException)
                { }
            }
        }

        public void TakeTurn()
        {

            // create a copy of the 'Cells' array so
            // that changes are not made to the original array
            // this will make the changes 'simultaneous'
            // http://stackoverflow.com/questions/4670720/extremely-fast-way-to-clone-the-values-of-a-jagged-array-into-a-second-array
            Cell[][] cellsCopy = Cells.Select(s => s.ToArray()).ToArray();

            for (int y = 0; y < Cells.Length; y++)
            {
                for (int x = 0; x < Cells[y].Length; x++)
                {
                    // we need to create a 2D array containing the 'target' cell
                    // and its neighbours
                    // there are multiple cases where the target cell
                    // is on an edge or corner of the game
                    // if these are not handled the result will be an
                    // out of bounds exception

                    // vertical offset
                    int yOffset = 1;
                    // horizontal offset
                    int xOffset = 1;
                    // length of the tempcells arrays under 'normal'
                    // conditions
                    int tempCellsOuterLength = 3;
                    int tempCellsInnerLength = 3;

                    // we want the for loops to increment
                    // or decrement according to the target cell
                    // position
                    // http://stackoverflow.com/questions/13437534/inline-if-statament-to-determine-i-or-i-increment-in-for-loop
                    // '0' is positive
                    // '1' is negative
                    int xLoopCounter = 0;
                    int yLoopCounter = 0;

                    // when the cells are on the edge there is no offset
                    // the start will always be [y][x]
                    // we need also need to adjust the length of the
                    // arrays in 'tempCells' depending on the
                    // target cell position

                    if (x == 0)
                    {
                        xOffset = 0;
                        xLoopCounter = 0;
                        if (y == 0)
                        {
                            yOffset = 0;
                            tempCellsOuterLength = 2;
                            tempCellsInnerLength = 2;
                            yLoopCounter = 0;

                        }
                        if (y == (Cells.Length - 1))
                        {
                            yOffset = 0;
                            tempCellsOuterLength = 2;
                            tempCellsInnerLength = 2;
                            yLoopCounter = 1;
                        }
                        if (y > 0)
                        {
                            tempCellsInnerLength = 2;
                        }
                    }
                    if (x == (Cells[y].Length - 1))
                    {
                        xOffset = 0;
                        xLoopCounter = 1;

                        if (y == 0)
                        {
                            yOffset = 0;
                            yLoopCounter = 0;
                            tempCellsOuterLength = 2;
                            tempCellsInnerLength = 2;
                        }
                        if (y == (Cells.Length - 1))
                        {
                            yOffset = 0;
                            yLoopCounter = 1;
                            tempCellsOuterLength = 2;
                            tempCellsInnerLength = 2;
                        }
                        if (y > 0)
                        {
                            tempCellsInnerLength = 2;
                        }
                    }
                    else if (x > 0)
                    {
                        xOffset = 1;
                        if (y == (Cells.Length - 1))
                        {
                            yOffset = 0;
                            tempCellsInnerLength = 3;
                            tempCellsOuterLength = 2;
                            yLoopCounter = 1;
                        }
                        if (y == 0)
                        {
                            yOffset = 0;
                            tempCellsInnerLength = 3;
                            tempCellsOuterLength = 2;
                            yLoopCounter = 0;
                        }
                    }

                    // copy the offset variables so we can
                    // reset them
                    int xOffsetCopy = xOffset;
                    int yOffsetCopy = yOffset;

                    int tempCellsY;
                    int tempCellsX;

                    int noOfAlive = 0;
                    int noOfDead = 0;


                    Cell[][] tempCells = new Cell[tempCellsOuterLength][];

                    for (tempCellsY = 0; tempCellsY
                        < tempCells.Length; tempCellsY++,
                        yOffset += yLoopCounter == 0 ? -1 : 1)
                    {
                        tempCells[tempCellsY] = new Cell[tempCellsInnerLength];
                        xOffset = xOffsetCopy;

                        for (tempCellsX = 0; tempCellsX
                            < tempCells[tempCellsY].Length; tempCellsX++,
                            xOffset += xLoopCounter == 0 ? -1 : 1)
                        {
                            tempCells[tempCellsY][tempCellsX] =
                                Cells[y - yOffset][x - xOffset];
                        }
                    }
                    // count number of either 'dead' or 'alive' cells in the
                    // tempcells array depending on the target cell value

                    for (int outer = 0; outer < tempCells.Length; outer++)
                    {
                        for (int inner = 0; inner
                            < tempCells[outer].Length; inner++)
                        {

                            if (tempCells[outer][inner] == Cell.Alive)
                            {
                                noOfAlive++;
                            }
                            else
                            {
                                noOfDead++;
                            }
                        }
                    }

                    // we do not want to count the target cell
                    if (Cells[y][x] == Cell.Alive)
                    {
                        noOfAlive -= 1;
                    }
                    else
                    {
                        noOfDead -= 1;
                    }

                    // evaluate the result against the rules
                    // set the new cell value accordingly

                    if (Cells[y][x] == Cell.Alive)
                    {
                        if (noOfAlive < 2 || noOfAlive > 3)
                        {
                            cellsCopy[y][x] = Cell.Dead;
                        }

                    }
                    if (Cells[y][x] == Cell.Dead)
                    {
                        if (noOfAlive == 3)
                        {
                            cellsCopy[y][x] = Cell.Alive;
                        }
                    }
                }
            }
            Cells = cellsCopy.Select(s => s.ToArray()).ToArray();
        }

        public override string ToString()
        {
            string output;
            return output = Utility.displayCells(Cells);
        }
    }
}

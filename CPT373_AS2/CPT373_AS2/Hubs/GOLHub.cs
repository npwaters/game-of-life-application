using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;

using CPT373_AS2.Models;
using System.Text;
using System.Web.Mvc;
using Newtonsoft.Json;
using System.Threading;
using System.Threading.Tasks;

namespace CPT373_AS2
{
    public class GolHub : Hub
    {
        public void Hello()
        {
            Clients.All.hello();
        }


        // TODO:
        // implement all playgame code in the Hub??
        // create a method that takes the 'stopgame' js event
        // from the view
        // the PlayGame method will check a bool in that method
        // in a while loop to simulate 'console.readkey(true)'

        private static char[][] Cells { get; set; }
        private bool GameRunning { get; set; }

        public void StopActiveGame(bool running)
        {
            GameRunning = running;
        }


        private void ConvertCells(string cellsString)
        {
            // split the Cells string
            string[] cells = cellsString.Split(new string[]
                { Environment.NewLine },
                StringSplitOptions.None);

            Cells = new char[cells.Length][];

            //int l = templateY;

            for (int i = 0; i < cells.Length; i++)
            {
                //int k = templateX;

                Cells[i] = new char[cells[i].Length];
                for (int j = 0; j < cells[i].Length; j++)
                {
                    Cells[i][j] = cells[i][j];
                }
            }
        }

        private string formatCells()
        {
            StringBuilder output = new StringBuilder();

            for (int i = 0; i < Cells.Length; i++)
            {
                for (int j = 0; j < Cells[i].Length; j++)
                {
                    if (Cells[i][j] == 'O')
                    {
                        output.Append('\u2588');
                    }
                    else
                    {
                        output.Append("\u0020");
                    }
                }
                output.AppendLine();
            }
            return output.ToString();
        }

        // call the hub client method (send the new cell info):
        // create a hub method that calls the client
        // call the hub method from the Game 'playgame' routines


        public void PlayActiveGame(UserGame game)
        {

            // 

            game.GameRunning = true;
            ConvertCells(game.Cells);

            CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();
            Task playGameTask = PlayGameAsync(cancellationTokenSource.Token);


            //StringBuilder output = new StringBuilder();
            //output.Append("\u0020");
            //output.Append("\u0020");
            //output.Append("\u0020");
            //output.AppendLine();
            //output.Append('\u2588');
            //output.Append('\u2588');
            //output.Append('\u2588');
            //output.AppendLine();
            //output.Append("\u0020");
            //output.Append("\u0020");
            //output.Append("\u0020");


            //game.Cells = "XXX\r\nOOO\r\nXXX";

            //Clients.Caller.addTurnToPage(JsonConvert.SerializeObject(game));
            //MvcHtmlString cells = new MvcHtmlString(output.ToString());

            //string cells = output.ToString();
            //updateHubClient(cells);
            //Clients.Caller.addTurnToPage(cells);
            //Clients.Caller.addTurnToPage(JsonConvert.SerializeObject(cells));
        }



        private async Task PlayGameAsync
            (CancellationToken cancellationToken)
        {
            while (!cancellationToken.IsCancellationRequested)
            {
                string cellsAsString = formatCells();
                updateHubClient(cellsAsString);

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
            char[][] cellsCopy = Cells.Select(s => s.ToArray()).ToArray();

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


                    char[][] tempCells = new char[tempCellsOuterLength][];

                    for (tempCellsY = 0; tempCellsY
                        < tempCells.Length; tempCellsY++,
                        yOffset += yLoopCounter == 0 ? -1 : 1)
                    {
                        tempCells[tempCellsY] = new char[tempCellsInnerLength];
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

                            if (tempCells[outer][inner] == 'O')
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
                    if (Cells[y][x] == 'O')
                    {
                        noOfAlive -= 1;
                    }
                    else
                    {
                        noOfDead -= 1;
                    }

                    // evaluate the result against the rules
                    // set the new cell value accordingly

                    if (Cells[y][x] == 'O')
                    {
                        if (noOfAlive < 2 || noOfAlive > 3)
                        {
                            cellsCopy[y][x] = 'X';
                        }

                    }
                    if (Cells[y][x] == 'X')
                    {
                        if (noOfAlive == 3)
                        {
                            cellsCopy[y][x] = 'O';
                        }
                    }
                }
            }
            Cells = cellsCopy.Select(s => s.ToArray()).ToArray();
        }

        public static string CellsAsString
            ()
        {
            StringBuilder output = new StringBuilder();

            for (int y = 0; y < Cells.Length; y++)
            {
                for (int x = 0; x < Cells[y].Length; x++)
                {
                    if (Cells[y][x] == 'O')
                    {
                        output.Append("O");
                    }
                    else
                    {
                        output.Append("X");
                    }
                }
                if (y < (Cells.Length - 1))
                {
                    output.AppendLine();
                }


            }
            return output.ToString();
        }

        public void updateHubClient(string cells)
        {
            Clients.Caller.addTurnToPage(cells);
        }
    }
}
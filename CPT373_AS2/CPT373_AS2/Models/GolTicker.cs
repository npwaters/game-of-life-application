using CPT373_AS2.Hubs;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Web;

using Newtonsoft.Json;

namespace CPT373_AS2.Models
{
    public class GolTicker
    {
        // MVCSignalRSample
        // Singleton instance
        private readonly static Lazy<GolTicker> _instance = new Lazy<GolTicker>(
            () => new GolTicker(GlobalHost.ConnectionManager.GetHubContext<GolTickerHub>().Clients));

        private readonly object _golStateLock = new object();
        private readonly object _updateGolCellsLock = new object();

        private static char[][] Cells { get; set; }
        private UserGame Game { get; set; }

        private readonly TimeSpan _updateInterval = TimeSpan.FromSeconds(1);

        private Timer _timer;
        private volatile bool _updatingGolCells;
        private volatile GolState _golState;

        // construtor
        private GolTicker(IHubConnectionContext<dynamic> clients)
        {
            Clients = clients;
        }

        public static GolTicker Instance
        {
            get
            {
                return _instance.Value;
            }
        }

        private IHubConnectionContext<dynamic> Clients
        {
            get;
            set;
        }

        public GolState GolState
        {
            get { return _golState; }
            private set { _golState = value; }
        }

        public void StartGame(UserGame game)
        {
            lock (_golStateLock)
            {
                if (GolState != GolState.Running)
                {
                    Game = game;
                    //ConvertCells(game.Cells);

                    _timer = new Timer(UpdateGolCells, null, _updateInterval, _updateInterval);

                    GolState = GolState.Running;

                    //BroadcastGolStateChange(GolState.Running);
                }
            }
        }

        private void UpdateGolCells(object state)
        {
            lock (_updateGolCellsLock)
            {
                if (!_updatingGolCells)
                {
                    _updatingGolCells = true;

                    //TakeTurn();
                    var cells = Game.TakeTurn();

                    //string cellsAsString = formatCells();
                    //updateHubClient(cellsAsString);
                    updateHubClient(formatCells(Game.getGameCells()));

                    _updatingGolCells = false;

                }
            }



        }

        private void updateHubClient(string cells)
        {
            Clients.All.addTurnToPage(cells);

            //throw new NotImplementedException();
        }



        public void StopGame()
        {
            lock (_golStateLock)
            {
                if (GolState == GolState.Running)
                {
                    if (_timer != null)
                    {
                        _timer.Dispose();
                    }
                    GolState = GolState.Stopped;


                    // convert the Game to JSON
                    //string currentGame = JsonConvert.SerializeObject(Game);
                    // send the Game to the client
                    UserGame currentGame = Game;

                    var jsonGame = JsonConvert.SerializeObject(currentGame,
                        new JsonSerializerSettings
                        { PreserveReferencesHandling = PreserveReferencesHandling.All });
                    Clients.All.updateGame(jsonGame);
                    Clients.All.UpdateStoppedSessionGame(jsonGame);

                    //BroadcastGolStateChange(GolState.Stopped);
                }

            }
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


        public static string CellsAsString()
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

        private string formatCells(char[][] gameCells)
        {
            StringBuilder output = new StringBuilder();

            for (int i = 0; i < gameCells.Length; i++)
            {
                for (int j = 0; j < gameCells[i].Length; j++)
                {
                    if (gameCells[i][j] == 'O')
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


    }

    public enum GolState
    {
        Stopped,
        Running
    }
}
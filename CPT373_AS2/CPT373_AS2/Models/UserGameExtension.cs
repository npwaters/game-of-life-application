using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using CPT373_AS2.Models;
using System.Text;
using System.Web.Script.Serialization;
using CPT373_AS2.ValidationAttributes;
using System.ComponentModel.DataAnnotations;

namespace CPT373_AS2.Models
{
    public partial class UserGame
    {

        // constructor to initialise the cells string
        //public UserGame()
        //{
        //    initialiseCells();
        //}

        //public string Cells { get; set; }

        
        // default constructor
        public UserGame()
        {

        }        
            
           // copy constructor
        public UserGame(UserGame sourceGame)
        {
            Name = sourceGame.Name;
            Height = sourceGame.Height;
            Width = sourceGame.Width;
            Cells = sourceGame.Cells;
        }
        

        char[][] GameCells;

        //[ScriptIgnore]
        //public virtual User User { get; set; }

        public int UserGameSessionID { get; set; }
        //public UserTemplate Template { get; set; }
        public int templateHeight { get; set; }
        public int templateWidth { get; set; }

        public bool GameRunning { get; set; }


        public void initialiseCells()
        {

            


            GameCells = new char[Height][];

            for (int i = 0; i < GameCells.Length; i++)
            {
                GameCells[i] = new char[Width];
                for (int j = 0; j < GameCells[i].Length; j++)
                {
                    GameCells[i][j] = 'X';
                }
            }
        }

        public void InsertTemplate
            (UserTemplate template,
            int templateX,
            int templateY)
        {

            // split the Cells string
            string[] cells = template.Cells.Split(new string[]
                { Environment.NewLine },
                StringSplitOptions.None);



            int l = templateY;

            for (int i = 0; i < cells.Length; i++, l++)
            {
                int k = templateX;
                for (int j = 0; j < cells[i].Length; j++, k++)
                {
                    GameCells[l][k] = cells[i][j];
                }
            }

            Cells = CellsAsString(GameCells);

        }


        public static string CellsAsString
            (char[][] gameCells)
        {
            StringBuilder output = new StringBuilder();

            for (int y = 0; y < gameCells.Length; y++)
            {
                for (int x = 0; x < gameCells[y].Length; x++)
                {
                    if (gameCells[y][x] == 'O')
                    {
                        output.Append("O");
                    }
                    else
                    {
                        output.Append("X");
                    }
                }
                if (y < (gameCells.Length -1))
                {
                    output.AppendLine();
                }

                
            }
            return output.ToString();
        }



        // TODO:
        // implement Play Game

        private string formatCells()
        {
            StringBuilder output = new StringBuilder();

            for (int i = 0; i < GameCells.Length; i++)
            {
                for (int j = 0; j < GameCells[i].Length; j++)
                {
                    if (GameCells[i][j] == 'O')
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


        public char[][] getGameCells()
        {
            return GameCells;
        }

        public string TakeTurn()
        {
            ConvertCells(Cells);
            
            
            // create a copy of the 'GameCells' array so
            // that changes are not made to the original array
            // this will make the changes 'simultaneous'
            // http://stackoverflow.com/questions/4670720/extremely-fast-way-to-clone-the-values-of-a-jagged-array-into-a-second-array
            char[][] cellsCopy = GameCells.Select(s => s.ToArray()).ToArray();

            for (int y = 0; y < GameCells.Length; y++)
            {
                for (int x = 0; x < GameCells[y].Length; x++)
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
                        if (y == (GameCells.Length - 1))
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
                    if (x == (GameCells[y].Length - 1))
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
                        if (y == (GameCells.Length - 1))
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
                        if (y == (GameCells.Length - 1))
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
                                GameCells[y - yOffset][x - xOffset];
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
                    if (GameCells[y][x] == 'O')
                    {
                        noOfAlive -= 1;
                    }
                    else
                    {
                        noOfDead -= 1;
                    }

                    // evaluate the result against the rules
                    // set the new cell value accordingly

                    if (GameCells[y][x] == 'O')
                    {
                        if (noOfAlive < 2 || noOfAlive > 3)
                        {
                            cellsCopy[y][x] = 'X';
                        }

                    }
                    if (GameCells[y][x] == 'X')
                    {
                        if (noOfAlive == 3)
                        {
                            cellsCopy[y][x] = 'O';
                        }
                    }
                }
            }
            GameCells = cellsCopy.Select(s => s.ToArray()).ToArray();

            // convert GameCells to string
            // set Cells
            Cells = CellsAsString(GameCells);
            return CellsAsString(GameCells);

        }

        private void ConvertCells(string cellsString)
        {
            // split the GameCells string
            string[] cells = cellsString.Split(new string[]
                { Environment.NewLine },
                StringSplitOptions.None);

            GameCells = new char[cells.Length][];

            //int l = templateY;

            for (int i = 0; i < cells.Length; i++)
            {
                //int k = templateX;

                GameCells[i] = new char[cells[i].Length];
                for (int j = 0; j < cells[i].Length; j++)
                {
                    GameCells[i][j] = cells[i][j];
                }
            }
        }


    }
}
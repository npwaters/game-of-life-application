using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using CPT373_AS2.Models;
using System.Text;

namespace CPT373_AS2.Models
{
    public partial class UserGame
    {

        // constructor to initialise the cells string
        //public UserGame()
        //{
        //    initialiseCells();
        //}
        char[][] gameCells;

        public int UserGameSessionID { get; set; }



        public void initialiseCells()
        {

            


            gameCells = new char[Height][];

            for (int i = 0; i < gameCells.Length; i++)
            {
                gameCells[i] = new char[Width];
                for (int j = 0; j < gameCells[i].Length; j++)
                {
                    gameCells[i][j] = 'X';
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
                    gameCells[l][k] = cells[i][j];
                }
            }

            Cells = CellsAsString(gameCells);

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


    }
}
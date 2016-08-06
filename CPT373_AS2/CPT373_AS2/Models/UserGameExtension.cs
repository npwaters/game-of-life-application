using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using CPT373_AS2.Models;

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

        //public void InsertTemplate
        //    (UserTemplate template,
        //    int templateX,
        //    int templateY)
        //{
        //    int l = templateY;

        //    for (int i = 0; i < template.Cells.Length; i++, l++)
        //    {
        //        int k = templateX;
        //        for (int j = 0; j < template.Cells[i].Length; j++, k++)
        //        {
        //            gameCells[l][k] = template.Cells[i][j];
        //        }
        //    }

        //}


    }
}
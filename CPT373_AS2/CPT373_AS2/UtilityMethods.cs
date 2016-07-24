using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace CPT373_AS2
{
    public class UtilityMethods
    {
        public static string DisplayCells
            (string cells)
        {
            StringBuilder output = new StringBuilder();

            //for (int y = 0; y < cells.Length; y++)
            //{
            //    for (int x = 0; x < cells[y].Length; x++)
            //    {
            //        if (cells[y][x] == Cell.Alive)
            //        {
            //            output.Append('\u2588');
            //        }
            //        else
            //        {
            //            output.Append(" ");
            //        }
            //    }
            //    output.AppendLine();
            //}
            return output.ToString();
        }
    }
}
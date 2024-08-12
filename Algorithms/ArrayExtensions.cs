using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBConnectedFinalProjectThing.Algorithms
{
    public static class ArrayExtensions
    {
        public static char[] GetRow(this char[,] array, int rowIndex)
        {
            int width = array.GetLength(1);
            char[] row = new char[width];
            for (int i = 0; i < width; i++)
            {
                row[i] = array[rowIndex, i];
            }
            return row;
        }
    }
}

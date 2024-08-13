using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBConnectedFinalProjectThing.Algorithms
{
    public static class VerticalChecker
    {
        public static Dictionary<(int, int), int> CheckColumnForSpaces(char[,] array, int columnIndex)
        {
            if (columnIndex < 0 || columnIndex >= array.GetLength(1))
            {
                throw new ArgumentOutOfRangeException(nameof(columnIndex), "Column index is out of range.");
            }

            int columnLength = array.GetLength(0);

            if (columnLength <= 2)
            {
                throw new ArgumentException("Column length must be greater than 2 to check for spaces between indices.");
            }

            bool prevBlock = false;
            bool minSpaceDone = false;
            int track = 0;
            int trackReal = 0;
            int minSpace = columnLength / 20;
            Dictionary<(int, int), int> exportDict = new Dictionary<(int, int), int>();

            for (int i = 1; i < columnLength - 1; i++)
            {
                if (i == columnLength - 2)
                {
                    if (minSpaceDone == false)
                    {
                        continue;
                    }
                    else if (array[i, columnIndex] == ' ' && minSpaceDone && i == columnLength - 2)
                    {
                        track = 0;
                        trackReal++;
                        exportDict.Add((i - trackReal, columnIndex), trackReal);
                        trackReal = 0;
                        continue;
                    }
                }

                if (array[i, columnIndex] == ' ' && minSpaceDone == false && track < minSpace)
                {
                    track++;
                    continue;
                }
                else if (array[i, columnIndex] == ' ' && minSpaceDone == false && track >= minSpace)
                {
                    track++;
                    trackReal++;
                    minSpaceDone = true;
                    continue;
                }
                else if (array[i, columnIndex] == ' ' && minSpaceDone)
                {
                    trackReal++;
                    continue;
                }
                else if (array[i, columnIndex] != ' ' && prevBlock)
                {
                    continue;
                }
                else if (array[i, columnIndex] != ' ' && prevBlock == false)
                {
                    exportDict.Add((i - trackReal, columnIndex), trackReal);
                    track = 0;
                    trackReal = 0;
                    prevBlock = true;
                }
            }
            return exportDict;
        }
    }
}

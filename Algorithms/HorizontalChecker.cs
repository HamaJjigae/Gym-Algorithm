using Android.Locations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBConnectedFinalProjectThing.Algorithms
{
    public static class HorizontalChecker
    {
        public static List<int> CheckRowForSpaces(char[,] array, int rowIndex)
        {
            if (rowIndex < 0 || rowIndex >= array.GetLength(0))
            {
                throw new ArgumentOutOfRangeException(nameof(rowIndex), "Row index is out of range.");
            }

            int rowLength = array.GetLength(1);

            if (rowLength <= 2)
            {
                throw new ArgumentException("Row length must be greater than 2 to check for spaces between indices.");
            }
            bool prevBlock = false;
            bool minSpaceDone = false;
            int track = 0;
            int trackReal = 0;
            int minSpace = rowLength / 20;
            List<int> exportList = new List<int>();
            for (int i = 1; i < rowLength - 1; i++)
            {
                if (i == rowLength - 2)
                {
                    if (array[rowIndex, i] == ' ' && minSpaceDone == false)
                    {
                        if (track >= minSpace)
                        {
                            trackReal++;
                        }
                        exportList.Add(trackReal);
                    }
                    else if (array[rowIndex, i] == ' ' && minSpaceDone)
                    {
                        trackReal++;
                        exportList.Add(trackReal);
                    }
                }

                if (array[rowIndex, i] == ' ' && minSpaceDone == false && track < minSpace)
                {
                    track++;
                    continue;
                }
                else if (array[rowIndex, i] == ' ' && minSpaceDone == false && track >= minSpace && i == rowLength - 2)
                {
                    track = 0;
                    trackReal++;
                    exportList.Add(trackReal);
                    trackReal = 0;
                    continue;
                }
                else if (array[rowIndex, i] == ' ' && minSpaceDone == false && track >= minSpace)
                {
                    track++;
                    trackReal++;
                    minSpaceDone = true;
                    continue;
                }
                else if (array[rowIndex, i] == ' ' && minSpaceDone && i == rowLength - 2)
                {
                    track = 0;
                    trackReal++;
                    exportList.Add(trackReal);
                    trackReal = 0;
                    continue;
                }
                else if (array[rowIndex, i] == ' ' && minSpaceDone)
                {
                    trackReal++;
                    continue;
                }
                else if (array[rowIndex, i] != ' ' && prevBlock)
                {
                    continue;
                }
                else if (array[rowIndex, i] != ' ' && prevBlock == false)
                {
                    exportList.Add(trackReal);
                    track = 0;
                    trackReal = 0;
                    prevBlock = true;
                }
            }
            return exportList;
        }
    }
}
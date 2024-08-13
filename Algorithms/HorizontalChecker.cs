﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBConnectedFinalProjectThing.Algorithms
{
    public static class HorizontalChecker
    {
        public static Dictionary<(int, int), int> CheckRowForSpaces(char[,] array, int rowIndex)
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
            Dictionary<(int, int), int> exportDict = new Dictionary<(int, int), int>();
            for (int i = 1; i < rowLength - 1; i++)
            {
                if (i == rowLength - 2)
                {
                    if (minSpaceDone == false)
                    {
                        continue;
                    }
                    else if (array[rowIndex, i] == ' ' && minSpaceDone && i == rowLength - 2)
                    {
                        track = 0;
                        trackReal++;
                        exportDict.Add((rowIndex, i - trackReal), trackReal);
                        trackReal = 0;
                        continue;
                    }
                }
                if (array[rowIndex, i] == ' ' && minSpaceDone == false && track < minSpace)
                {
                    track++;
                    continue;
                }
                else if (array[rowIndex, i] == ' ' && minSpaceDone == false && track >= minSpace)
                {
                    track++;
                    trackReal++;
                    minSpaceDone = true;
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
                    exportDict.Add((rowIndex, i - trackReal), trackReal);
                    track = 0;
                    trackReal = 0;
                    prevBlock = true;
                }
            }
            return exportDict;
        }
    }
}
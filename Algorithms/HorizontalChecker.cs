using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBConnectedFinalProjectThing.Algorithms
{
    public class HorizontalChecker
    {
        public static Dictionary<(int, int), int> CheckRowForSpaces(char[,] array)
        {
            int numRows = array.GetLength(0);
            int numCols = array.GetLength(1);
            bool prevBlock = false;
            bool minSpaceDone = false;
            int track = 0;
            int trackReal = 0;
            int minSpace = numCols / 20;
            Dictionary<(int, int), int> exportDict = new Dictionary<(int, int), int>();
            for (int k = 0; k < numRows; k++)
            {
                prevBlock = true;
                minSpaceDone = false;
                trackReal = 0;
                track = 0;
                for (int i = 1; i < numCols; i++)
                {
                    if (array[k, i] == ' ' && minSpaceDone == false && track < minSpace)
                    {
                        track++;
                        prevBlock = false;
                        continue;
                    }
                    else if (array[k, i] == ' ' && minSpaceDone == false && track >= minSpace)
                    {
                        track++;
                        trackReal++;
                        minSpaceDone = true;
                        prevBlock = false;
                        continue;
                    }
                    else if (array[k, i] == ' ' && minSpaceDone)
                    {
                        trackReal++;
                        prevBlock = false;
                        continue;
                    }
                    else if (array[k, i] != ' ' && prevBlock)
                    {
                        continue;
                    }
                    else if (array[k, i] != ' ' && prevBlock == false)
                    {
                        if (IsClear(array, k, i - trackReal))
                        {
                            exportDict.Add((k, i - trackReal), trackReal - minSpace);
                            track = 0;
                            trackReal = 0;
                            prevBlock = true;
                            minSpaceDone = false;
                            continue;
                        }
                        else
                        {
                            track = 0;
                            trackReal = 0;
                            prevBlock = true;
                            minSpaceDone = false;
                            continue;
                        }
                    }
                }
            }
            return exportDict;
        }
        private static bool IsClear(char[,] array, int y, int x)
        {
            int numRows = array.GetLength(0);
            int numCols = array.GetLength(1);
            int min = numRows / 20;

            if (y - min >= 0 && y + min < numRows)
            {
                if (x >= 0 && x < numCols)
                {
                    for (int i = 0; i <= min; i++)
                    {
                        if (array[y - i, x] != ' ' || array[y + i, x] != ' ')
                        {
                            return false;
                        }
                    }
                    return true;
                }
            }
            return false;
        }
    }
}
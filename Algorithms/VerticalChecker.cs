using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBConnectedFinalProjectThing.Algorithms
{
    public class VerticalChecker
    {
        public static Dictionary<(int, int), int> CheckColumnForSpaces(char[,] array)
        {
            int numRows = array.GetLength(0);
            int numCols = array.GetLength(1);
            bool prevBlock = false;
            bool minSpaceDone = false;
            int track = 0;
            int trackReal = 0;
            int minSpace = numRows / 20;
            Dictionary<(int, int), int> exportDict = new Dictionary<(int, int), int>();
            for (int k = 0; k < numCols; k++)
            {
                prevBlock = true;
                minSpaceDone = false;
                trackReal = 0;
                track = 0;
                for (int i = 1; i < numRows; i++)
                {
                    if (array[i, k] == ' ' && minSpaceDone == false && track < minSpace)
                    {
                        track++;
                        prevBlock = false;
                        continue;
                    }
                    else if (array[i, k] == ' ' && minSpaceDone == false && track >= minSpace)
                    {
                        track++;
                        trackReal++;
                        minSpaceDone = true;
                        prevBlock = false;
                        continue;
                    }
                    else if (array[i, k] == ' ' && minSpaceDone)
                    {
                        trackReal++;
                        prevBlock = false;
                        continue;
                    }
                    else if (array[i, k] != ' ' && prevBlock)
                    {
                        continue;
                    }
                    else if (array[i, k] != ' ' && prevBlock == false)
                    {
                        if (IsClear(array, i - trackReal, k))
                        {
                            exportDict.Add((i - trackReal, k), trackReal - minSpace);
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

            int min = numCols / 20;

            if (x - min >= 0 && x + min < numCols)
            {
                if (y >= 0 && y < numRows)
                {
                    for (int i = 0; i <= min; i++)
                    {
                        if (array[y, x - i] != ' ' || array[y, x + i] != ' ')
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

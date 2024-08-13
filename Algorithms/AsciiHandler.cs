using DBConnectedFinalProjectThing.Services;
using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DBConnectedFinalProjectThing.Algorithms;


namespace DBConnectedFinalProjectThing.Algorithms
{
    public class AsciiHandler
    {
        public char[,] PerimeterBox(int height, int width)
        {
            int realWidth = width + 2;
            int realHeight = height + 2;
            char[,] boxArray = new char[realHeight, realWidth];

            for (int row = 0; row < realHeight; row++)
            {
                for (int col = 0; col < realWidth; col++)
                {
                    if (row == 0 || row == realHeight - 1 || col == 0 || col == realWidth - 1)
                    {
                        boxArray[row, col] = '*';
                    }
                    else
                    {
                        boxArray[row, col] = ' ';
                    }
                }
            }
            return boxArray;
        }
        public void PrintBox(char[,] boxArray)
        {
            int realHeight = boxArray.GetLength(0);
            int realWidth = boxArray.GetLength(1);

            for (int row = 0; row < realHeight; row++)
            {
                for (int col = 0; col < realWidth; col++)
                {
                    Console.Write(boxArray[row, col]);
                }
                Console.WriteLine();
            }
        }
        public string EquipmentSorting(char[,] boxArray, List<Equipment> equipmentList)
        {
            int realWidth = boxArray.GetLength(1); //this is columns
            int realHeight = boxArray.GetLength(0); //this is rows
            if (realWidth % 2 == 0)
            {
                int centerXLow = ((realWidth / 2) - 1);
                int centerXHigh = (realWidth / 2);
                for (int i = 1; i < realHeight - 1; i++)
                {
                    boxArray[i, centerXLow] = '.';
                    boxArray[i, centerXHigh] = '.';
                }    
            }
            else
            {
                int centerX = realWidth / 2;
                for (int i = 1; i < realHeight - 1; i ++)
                {
                    boxArray[i, centerX] = '.';
                }
            }
            if (realHeight % 2 == 0)
            {
                int centerYLow = ((realHeight / 2) - 1);
                int centerYHigh = (realHeight / 2);
                for (int i = 1; i < realWidth -1; i++)
                {
                    boxArray[centerYLow, i] = '.';
                    boxArray[centerYHigh, i] = '.';
                }
            }
            else
            {
                int centerY = realHeight / 2;
                for (int i = 1; i < realWidth - 1; i++)
                {
                    boxArray[centerY, i] = '.';
                }
            }
        }
    }
}

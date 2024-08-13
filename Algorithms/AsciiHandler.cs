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
        public (char[,], List<string>, List<string>) EquipmentSorting(char[,] boxArray, List<Equipment> equipmentList)
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
                for (int i = 1; i < realHeight - 1; i++)
                {
                    boxArray[i, centerX] = '.';
                }
            }
            if (realHeight % 2 == 0)
            {
                int centerYLow = ((realHeight / 2) - 1);
                int centerYHigh = (realHeight / 2);
                for (int i = 1; i < realWidth - 1; i++)
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
            List<string> Legend = new List<string>();
            List<string> Unused = new List<string>();
            foreach (Equipment equipment in equipmentList)
            {
                Dictionary<(int, int), int> horizontalDict = HorizontalChecker.CheckRowForSpaces(boxArray);
                Dictionary<(int, int), int> verticalDict = VerticalChecker.CheckColumnForSpaces(boxArray);
                (int, int) foundKeyH = (0, 0);
                int foundValueH = 0;
                (int, int) foundKeyV = (0, 0);
                int foundValueV = 0;
                int unitSize = equipment.UnitSize;
                foreach (var kvp in horizontalDict)
                {
                    if (kvp.Value >= unitSize)
                    {
                        foundKeyH = kvp.Key;
                        foundValueH = kvp.Value;
                        break;
                    }
                }
                foreach (var kvp in verticalDict)
                {
                    if (kvp.Value >= unitSize)
                    {
                        foundKeyV = kvp.Key;
                        foundValueV = kvp.Value;
                        break;
                    }
                }
                if (foundKeyH != (0, 0))
                {
                    var key = foundKeyH;
                    for (int i = 0; i < unitSize; i++)
                    {
                        if (equipment.EquipmentCode == null)
                        {
                            throw new InvalidOperationException("EquipmentCode is null.");
                        }
                        else if (equipment.EquipmentCode.Length == 0)
                        {
                            throw new InvalidOperationException("EquipmentCode is empty.");
                        }
                        else
                        {
                            boxArray[key.Item1, key.Item2 + i] = equipment.EquipmentCode[0];
                        }
                    }
                    if (foundValueH - unitSize == 0)
                    {
                        horizontalDict.Remove(foundKeyH);
                    }
                    else
                    {
                        var newKey = (key.Item1, key.Item2 + unitSize);
                        var newValue = foundValueH - unitSize;
                        horizontalDict.Remove(foundKeyH);
                        horizontalDict.TryAdd(newKey, newValue);
                    }
                    Legend.Add($"{equipment.EquipmentCode} = {equipment.Name}");
                }
                else if (foundKeyV != (0, 0))
                {
                    var key = foundKeyV;
                    for (int i = 0; i < unitSize; i++)
                    {
                        if (equipment.EquipmentCode == null)
                        {
                            throw new InvalidOperationException("EquipmentCode is null.");
                        }
                        else if (equipment.EquipmentCode.Length == 0)
                        {
                            throw new InvalidOperationException("EquipmentCode is empty.");
                        }
                        else
                        {
                            boxArray[key.Item1 + i, key.Item2] = equipment.EquipmentCode[0];
                        }
                    }
                    if (foundValueV - unitSize == 0)
                    {
                        verticalDict.Remove(foundKeyV);
                    }
                    else
                    {
                        var newKey = (key.Item1 + unitSize, key.Item2);
                        var newValue = foundValueV - unitSize;
                        verticalDict.Remove(foundKeyV);
                        verticalDict.TryAdd(newKey, newValue);
                    }
                    Legend.Add($"{equipment.EquipmentCode} = {equipment.Name}");
                }
                else { Unused.Add($"{equipment.EquipmentCode} = {equipment.Name}"); }
            }
            return (boxArray, Legend, Unused);
        }
    }
}
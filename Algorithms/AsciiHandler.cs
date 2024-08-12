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
        public string PerimeterBox(int height, int width)
        {
            StringBuilder bobTheBuilder = new StringBuilder();

            bobTheBuilder.AppendLine(string.Join(" ", Enumerable.Repeat('*', width + 2)));

            for (int i = 0; i < height; i++)
            {
                bobTheBuilder.AppendLine($"*{new string(' ', width)}*");
            }

            bobTheBuilder.AppendLine(string.Join(" ", Enumerable.Repeat('*', width + 2)));

            return bobTheBuilder.ToString();
        }
        public string EquipmentSorting(string boxStructure, List<Equipment> equipmentList, int height, int width)
        {
            int realWidth = width * 2 - 2;
            int realHeight = height - 2;

            int totalUnitSize = equipmentList.Sum(e => e.UnitSize);
            if (totalUnitSize > realWidth * realHeight)
            {
                throw new InvalidOperationException("The combined unit size of the equipment exceeds the maximum area.");
            }

            var lines = boxStructure.Split(new[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);
            char[,] grid = new char[realHeight, realWidth];

            for (int i = 0; i < realHeight; i++)
            {
                for (int k = 0; k < realWidth; k++)
                {
                    grid[i, k] = ' ';
                }
            }

            for (int i = 0; i < realHeight; i++)
            {
                lines[i + 1] = $"{new string(grid.GetRow(i))}*";
            }

            var usedEquipment = new HashSet<int>();
            int equipmentIndex = 0;
            int rowIndex = 1;
            int colIndex = 1;

            while (equipmentIndex < equipmentList.Count && rowIndex < lines.Length - 1)
            {
                var equipment = equipmentList[equipmentIndex];

                if (usedEquipment.Contains(equipment.EquipmentId))
                {
                    equipmentIndex++;
                    continue;
                }

                string equipmentIdString = equipment.EquipmentId.ToString();
                int unitSize = equipment.UnitSize;

                char[] rowChars = lines[rowIndex].ToCharArray();
                while (unitSize > 0 && colIndex < rowChars.Length - 1)
                {
                    if (rowChars[colIndex] == ' ')
                    {
                        rowChars[colIndex] = equipmentIdString[0];
                        unitSize--;
                    }
                    colIndex++;
                }

                lines[rowIndex] = new string(rowChars);

                if (unitSize == 0)
                {
                    usedEquipment.Add(equipment.EquipmentId);
                    equipmentIndex++;
                }

                rowIndex++;
                colIndex = 1;
            }

            return string.Join(Environment.NewLine, lines);
        }
    }
}

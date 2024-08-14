using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;

namespace DBConnectedFinalProjectThing.Services
{
    public class EquipmentService
    {
        private List<Equipment> equipmentList;
        private const string FilePath = @"C:\Users\matth\source\repos\DBConnectedFinalProjectThing\Resources\DB\equipmentData.txt";

        public EquipmentService()
        {
            try
            {
                equipmentList = new List<Equipment>();
                LoadEquipmentData();
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"An error occurred while initializing EquipmentService: {ex.Message}");
                throw;
            }
        }

        private void LoadEquipmentData()
        {
            try
            {
                if (File.Exists(FilePath))
                {
                    var lines = File.ReadAllLines(FilePath);
                    foreach (var line in lines)
                    {
                        var parts = line.Split(',');
                        int equipmentId = int.Parse(parts[0]);
                        int unitSize = int.Parse(parts[1]);
                        string name = parts[2];
                        bool active = bool.Parse(parts[3]);
                        AddEquipment(equipmentId, unitSize, name, active);
                    }
                }
                else
                {
                    SaveEquipmentData();
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"An error occurred while loading equipment data: {ex.Message}");
                throw;
            }
        }

        public void SaveEquipmentData()
        {
            try
            {
                var lines = equipmentList.Select(e => $"{e.EquipmentId},{e.UnitSize},{e.Name},{e.Active}");
                File.WriteAllLines(FilePath, lines);
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"An error occurred while saving equipment data: {ex.Message}");
                throw;
            }
        }

        public List<Equipment> GetAllEquipment()
        {
            try
            {
                return equipmentList;
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"An error occurred while retrieving all equipment: {ex.Message}");
                throw;
            }
        }

        public void AddEquipment(int equipmentId, int unitSize, string name, bool active = true)
        {
            try
            {
                if (unitSize <= 0)
                {
                    throw new ArgumentException("UnitSize must be greater than zero.", nameof(unitSize));
                }

                Equipment equipment;
                if (unitSize <= 2)
                {
                    equipment = new SmallUnit(equipmentId, unitSize, name, active);
                }
                else if (unitSize <= 4)
                {
                    equipment = new MediumUnit(equipmentId, unitSize, name, active);
                }
                else if (unitSize <= 6)
                {
                    equipment = new LargeUnit(equipmentId, unitSize, name, active);
                }
                else
                {
                    equipment = new VeryLargeUnit(equipmentId, unitSize, name, active);
                }

                equipmentList.Add(equipment);
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"An error occurred while adding equipment: {ex.Message}");
                throw;
            }
        }

        public void RemoveEquipment(int equipmentId)
        {
            try
            {
                var equipmentToRemove = equipmentList.FirstOrDefault(e => e.EquipmentId == equipmentId);
                if (equipmentToRemove == null)
                {
                    throw new KeyNotFoundException($"Equipment with ID {equipmentId} not found");
                }

                equipmentList.Remove(equipmentToRemove);

                for (int i = 0; i < equipmentList.Count; i++)
                {
                    equipmentList[i].EquipmentId = i + 1;
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"An error occurred while removing equipment: {ex.Message}");
                throw;
            }
        }

        public void ToggleActive(int equipmentId)
        {
            try
            {
                var equipmentToggled = equipmentList.FirstOrDefault(e => e.EquipmentId == equipmentId);
                if (equipmentToggled == null)
                {
                    throw new KeyNotFoundException($"Equipment with ID {equipmentId} not found");
                }
                equipmentToggled.Active = !equipmentToggled.Active;
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"An error occurred while toggling the active status: {ex.Message}");
                throw;
            }
        }

        public void SetActiveStatus(int equipmentId, bool isActive)
        {
            var equipment = equipmentList.FirstOrDefault(e => e.EquipmentId == equipmentId);
            if (equipment != null)
            {
                equipment.Active = isActive;
            }
            else
            {
                throw new KeyNotFoundException($"Equipment with ID {equipmentId} not found");
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace DBConnectedFinalProjectThing.Services
{
    public class EquipmentService
    {
        private List<Equipment> equipmentList;

        public EquipmentService()
        {
            try
            {
                equipmentList = new List<Equipment>();
                PopulateEquipmentData();
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"An error occurred while initializing EquipmentService: {ex.Message}");
                throw;
            }
        }

        private void PopulateEquipmentData()
        {
            try
            {
                AddEquipment(1, 1, "Small Bench", true);
                AddEquipment(2, 2, "Dumbbell Rack", true);
                AddEquipment(3, 1, "Kettlebell Set", true);
                AddEquipment(4, 3, "Treadmill", true);
                AddEquipment(5, 4, "Exercise Bike", true);
                AddEquipment(6, 3, "Rowing Machine", true);
                AddEquipment(7, 5, "Smith Machine", true);
                AddEquipment(8, 6, "Power Rack", true);
                AddEquipment(9, 5, "Leg Press Machine", true);
                AddEquipment(10, 8, "Multi-Gym Station", true);
                AddEquipment(11, 9, "Cable Cross Over", true);
                AddEquipment(12, 10, "Functional Trainer", true);
                AddEquipment(13, 2, "Unusual Equipment A", false);
                AddEquipment(14, 1, "Experimental Device B", false);
                AddEquipment(15, 4, "Rare Equipment C", true);
                AddEquipment(16, 1, "Medicine Ball Rack", true);
                AddEquipment(17, 3, "Elliptical Trainer", true);
                AddEquipment(18, 6, "Seated Leg Curl", true);
                AddEquipment(19, 8, "CrossFit Rig", true);
                AddEquipment(20, 5, "Prototype Equipment D", false);
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"An error occurred while populating equipment data: {ex.Message}");
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
        public void AddEquipment(int equipmentId, int unitSize, string name, bool active)
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
                var equipmentRemoved = equipmentList.FirstOrDefault(e => e.EquipmentId == equipmentId);
                if (equipmentRemoved == null)
                {
                    throw new KeyNotFoundException($"Equipment with ID {equipmentId} not found");
                }
                equipmentList.Remove(equipmentRemoved);
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
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBConnectedFinalProjectThing.Services
{
    public abstract class Equipment
    {
        private int equipmentId;
        private int unitSize;
        private string? name;
        private bool active;
        private string? equipmentCode;

        public int EquipmentId
        {
            get { return equipmentId; }
            set
            {
                equipmentId = value;
                equipmentCode = GenerateEquipmentCode(equipmentId);
            }
        }
        public int UnitSize
        {
            get { return unitSize; }
            set { unitSize = value; }
        }
        public string? Name
        {
            get { return name; }
            set { name = value; }
        }
        public bool Active
        {
            get { return active; }
            set { active = value; }
        }
        public string? EquipmentCode
        {
            get { return equipmentCode; }
        }

        public Equipment(int equipmentId, int unitSize, string? name, bool active = true)
        {
            EquipmentId = equipmentId;
            UnitSize = unitSize;
            Name = name;
            Active = active;
        }

        private string? GenerateEquipmentCode(int id)
        {
            const string lowercase = "abcdefghijklmnopqrstuvwxyz";
            const string uppercase = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            string alphabet = lowercase + uppercase;

            int index = id % alphabet.Length;

            return alphabet[index].ToString();
        }
    }
}

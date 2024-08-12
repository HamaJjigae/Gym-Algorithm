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

        public int EquipmentId
        {
            get { return equipmentId; }
            set { equipmentId = value; }
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

        public Equipment(int equipmentId, int unitSize, string? name, bool active)
        {
            EquipmentId = equipmentId;
            UnitSize = unitSize;
            Name = name;
            Active = active;
        }
    }
}

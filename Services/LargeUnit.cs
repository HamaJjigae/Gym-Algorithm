using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBConnectedFinalProjectThing.Services
{
    internal class LargeUnit : Equipment
    {
        public LargeUnit(int equipmentId, int unitSize, string? name, bool active)
            : base(equipmentId, unitSize, name, active)
        {
        }
    }
}

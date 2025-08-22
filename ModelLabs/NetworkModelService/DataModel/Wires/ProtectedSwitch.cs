using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FTN.Common;
using FTN.Services.NetworkModelService.DataModel.Core;

namespace FTN.Services.NetworkModelService.DataModel.Wires
{
    public class ProtectedSwitch : Switch
    {
        private float breakingCapacity;
        public ProtectedSwitch(long globalId) : base(globalId)
        {
        }

        public float BreakingCapacity
        { 
            get { return breakingCapacity; } 
            set { breakingCapacity = value; } 
        }

        public override bool Equals(object obj)
        {
            if (base.Equals(obj))
            {
                ProtectedSwitch x = (ProtectedSwitch)obj;
                return ((x.breakingCapacity == this.breakingCapacity));
            }
            else
            {
                return false;
            }
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        #region IAccess implementation

        public override bool HasProperty(ModelCode property)
        {
            switch (property)
            {
                case ModelCode.PR_SWITCH_BR_CAPACITY:

                    return true;
                default:
                    return base.HasProperty(property);
            }
        }

        public override void GetProperty(Property property)
        {
            switch (property.Id)
            {
                case ModelCode.PR_SWITCH_BR_CAPACITY:
                    property.SetValue(breakingCapacity);
                    break;

                default:
                    base.GetProperty(property);
                    break;
            }
        }

        public override void SetProperty(Property property)
        {
            switch (property.Id)
            {
                case ModelCode.PR_SWITCH_BR_CAPACITY:
                    breakingCapacity = property.AsFloat();
                    break;

                default:
                    base.SetProperty(property);
                    break;
            }
        }

        #endregion IAccess implementation
    }
}

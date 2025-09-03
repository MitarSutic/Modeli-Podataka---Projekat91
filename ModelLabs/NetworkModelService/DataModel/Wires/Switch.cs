using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FTN.Common;
using FTN.Services.NetworkModelService.DataModel.Core;

namespace FTN.Services.NetworkModelService.DataModel.Wires
{
    public class Switch : ConductingEquipment
    {
        private bool normalOpen;
        private float ratedCurrent;
        private bool retained;
        private int switchOnCount;
        private DateTime switchOnDate;
        private List<long> switchSchedules;
        public Switch(long globalId) : base(globalId)
        {
        }

        public bool NormalOpen
        {
            get { return normalOpen; }
            set { normalOpen = value; }
        }

        public float RatedCurrent
        {
            get { return ratedCurrent; }
            set { ratedCurrent = value; }
        }

        public bool Retained
        {
            get { return retained; }
            set { retained = value; }
        }

        public int SwitchOnCount
        {
            get { return switchOnCount; }
            set { switchOnCount = value; }
        }

        public DateTime SwitchOnDate
        {
            get { return switchOnDate; }
            set { switchOnDate = value; }
        }

        public List<long> SwitchSchedule
        {
            get { return switchSchedules; }
            set { switchSchedules = value; }
        }

        public override bool Equals(object obj)
        {
            if (base.Equals(obj))
            {
                Switch x = (Switch)obj;
                return ((x.normalOpen == this.normalOpen) &&
                        (x.ratedCurrent == this.ratedCurrent) &&
                        (x.retained == this.retained) &&
                        (x.switchOnCount == this.switchOnCount) &&
                        (x.switchOnDate == this.switchOnDate) &&
                        CompareHelper.CompareLists(x.SwitchSchedule, SwitchSchedule));
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
                case ModelCode.SWITCH_NORMAL_OPEN:
                case ModelCode.SWITCH_RATED_CURRENT:
                case ModelCode.SWITCH_RETAINED:
                case ModelCode.SWITCH_SWITCH_ON_COUNT:
                case ModelCode.SWITCH_SWITCH_ON_DATE:
                case ModelCode.SWITCH_SWITCH_SCHEDULES:

                    return true;
                default:
                    return base.HasProperty(property);
            }
        }

        public override void GetProperty(Property property)
        {
            switch (property.Id)
            {
                case ModelCode.SWITCH_NORMAL_OPEN:
                    property.SetValue(normalOpen);
                    break;

                case ModelCode.SWITCH_RATED_CURRENT:
                    property.SetValue(ratedCurrent);
                    break;

                case ModelCode.SWITCH_RETAINED:
                    property.SetValue(retained);
                    break;

                case ModelCode.SWITCH_SWITCH_ON_COUNT:
                    property.SetValue(switchOnCount);
                    break;

                case ModelCode.SWITCH_SWITCH_ON_DATE:
                    property.SetValue(switchOnDate);
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
                case ModelCode.SWITCH_NORMAL_OPEN:
                    normalOpen = property.AsBool();
                    break;

                case ModelCode.SWITCH_RATED_CURRENT:
                    ratedCurrent = property.AsFloat();
                    break;

                case ModelCode.SWITCH_RETAINED:
                    retained = property.AsBool();
                    break;

                case ModelCode.SWITCH_SWITCH_ON_COUNT:
                    switchOnCount = property.AsInt();
                    break;

                case ModelCode.SWITCH_SWITCH_ON_DATE:
                    switchOnDate = property.AsDateTime();
                    break;

                default:
                    base.SetProperty(property);
                    break;
            }
        }

        #endregion IAccess implementation

        public override bool IsReferenced
        {
            get
            {
                return (switchSchedules.Count > 0)|| base.IsReferenced;
            }
        }

        public override void GetReferences(Dictionary<ModelCode, List<long>> references, TypeOfReference refType)
        {

            if (switchSchedules != null && switchSchedules.Count != 0 && (refType == TypeOfReference.Target || refType == TypeOfReference.Both))
            {
                references[ModelCode.SWITCH_SWITCH_SCHEDULES] = switchSchedules.GetRange(0, switchSchedules.Count);
            }

            base.GetReferences(references, refType);
        }

        public override void AddReference(ModelCode referenceId, long globalId)
        {
            switch (referenceId)
            {
                case ModelCode.SWITCH_SWITCH_SCHEDULES:
                    switchSchedules.Add(globalId);
                    break;
                default:
                    base.AddReference(referenceId, globalId);
                    break;
            }
        }

        public override void RemoveReference(ModelCode referenceId, long globalId)
        {
            switch (referenceId)
            {
                case ModelCode.SWITCH_SWITCH_SCHEDULES:

                    if (switchSchedules.Contains(globalId))
                    {
                        switchSchedules.Remove(globalId);
                    }
                    else
                    {
                        CommonTrace.WriteTrace(CommonTrace.TraceWarning, "Entity (GID = 0x{0:x16}) doesn't contain reference 0x{1:x16}.", this.GlobalId, globalId);
                    }

                    break;

                default:
                    base.RemoveReference(referenceId, globalId);
                    break;
            }
        }
    }
}

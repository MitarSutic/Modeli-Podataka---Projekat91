using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FTN.Common;
using FTN.Services.NetworkModelService.DataModel.Core;

namespace FTN.Services.NetworkModelService.DataModel.LoadModel
{
    public class Season : IdentifiedObject
    {
        private DateTime endDate;
        private DateTime startDate;
        private List<long> seasonDayTypeSchedule = new List<long>(); 
        public Season(long globalId) : base(globalId)
        {
        }

        public DateTime EndDate
        { 
            get { return endDate; } 
            set { endDate = value; } 
        }

        public DateTime StartDate
        {
            get { return startDate; }
            set { startDate = value; }
        }

        public List<long> SeasonDayTypeSchedule
        {
            get { return seasonDayTypeSchedule; }
            set { seasonDayTypeSchedule = value; }
        }

        public override bool Equals(object obj)
        {
            if (base.Equals(obj))
            {
                Season x = (Season)obj;
                return ((x.endDate == this.endDate) &&
                        (x.startDate == this.startDate)) &&
                        CompareHelper.CompareLists(x.seasonDayTypeSchedule,this.seasonDayTypeSchedule);
            }
            else
            {
                return false;
            }
        }

        #region IAccess implementation

        public override bool HasProperty(ModelCode property)
        {
            switch (property)
            {
                case ModelCode.EQUIPMENT_AGGREGATE:
                case ModelCode.EQUIPMENT_NORMALLY_IN_SERVICE:
                case ModelCode.SEASON_SDTS:

                    return true;
                default:
                    return base.HasProperty(property);
            }
        }

        public override void GetProperty(Property property)
        {
            switch (property.Id)
            {
                case ModelCode.SEASON_END_DATE:
                    property.SetValue(endDate);
                    break;

                case ModelCode.SEASON_START_DATE:
                    property.SetValue(startDate);
                    break;

                case ModelCode.SEASON_SDTS:
                    property.SetValue(seasonDayTypeSchedule);
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
                case ModelCode.SEASON_END_DATE:
                    endDate = property.AsDateTime();
                    break;

                case ModelCode.SEASON_START_DATE:
                    startDate = property.AsDateTime();
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
                return (seasonDayTypeSchedule.Count > 0) || base.IsReferenced;
            }
        }

        public override void GetReferences(Dictionary<ModelCode, List<long>> references, TypeOfReference refType)
        {

            if (seasonDayTypeSchedule != null && seasonDayTypeSchedule.Count != 0 && (refType == TypeOfReference.Target || refType == TypeOfReference.Both))
            {
                references[ModelCode.SEASON_SDTS] = seasonDayTypeSchedule.GetRange(0, seasonDayTypeSchedule.Count);
            }

            base.GetReferences(references, refType);
        }

        public override void AddReference(ModelCode referenceId, long globalId)
        {
            switch (referenceId)
            {
                case ModelCode.SEASON_DTS_SEASON:
                    seasonDayTypeSchedule.Add(globalId);
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
                case ModelCode.SEASON_DTS_SEASON:

                    if (seasonDayTypeSchedule.Contains(globalId))
                    {
                        seasonDayTypeSchedule.Remove(globalId);
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

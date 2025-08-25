using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FTN.Common;
using FTN.Services.NetworkModelService.DataModel.Core;

namespace FTN.Services.NetworkModelService.DataModel.IES_Projects
{
    public class SeasonDayTypeSchedule : RegularIntervalSchedule
    {
        private long dayType = 0;
        private long season = 0;

        public long DayType
        {
            get { return dayType; } 
            set { dayType = value; } 
        }

        public long Season
        {
            get { return season; }
            set { season = value; }
        }
        public SeasonDayTypeSchedule(long globalId) : base(globalId)
        {
        }

        public override bool Equals(object obj)
        {
            if (base.Equals(obj))
            {
                SeasonDayTypeSchedule x = (SeasonDayTypeSchedule)obj;
                return ((x.DayType == this.DayType) &&
                        (x.Season == this.Season));
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

        public override bool HasProperty(ModelCode property)
        {
            switch (property)
            {
                case ModelCode.SEASON_DTS_DAYTYPE:
                case ModelCode.SEASON_DTS_SEASON:
                    return true;
                default:
                    return base.HasProperty(property);
            }
        }

        public override void GetProperty(Property property)
        {
            switch (property.Id)
            {
                case ModelCode.SEASON_DTS_DAYTYPE:
                    property.SetValue(dayType);
                    break;
                case ModelCode.SEASON_DTS_SEASON:
                    property.SetValue(season);
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
                case ModelCode.SEASON_DTS_DAYTYPE:
                    dayType = property.AsReference();
                    break;
                case ModelCode.SEASON_DTS_SEASON:
                    season = property.AsReference();
                    break;
                default:
                    base.SetProperty(property);
                    break;
            }
        }

        public override void GetReferences(Dictionary<ModelCode, List<long>> references, TypeOfReference refType)
        {
            if (dayType != 0 && (refType == TypeOfReference.Reference || refType == TypeOfReference.Both))
            {
                references[ModelCode.SEASON_DTS_DAYTYPE] = new List<long> { dayType };
            }

            if (season != 0 && (refType == TypeOfReference.Reference || refType == TypeOfReference.Both))
            {
                references[ModelCode.SEASON_DTS_DAYTYPE] = new List<long> { season };
            }

            base.GetReferences(references, refType);
        }        
    }
}

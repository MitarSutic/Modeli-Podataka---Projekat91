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

        public override bool Equals(object obj)
        {
            if (base.Equals(obj))
            {
                Season x = (Season)obj;
                return ((x.endDate == this.endDate) &&
                        (x.startDate == this.startDate));
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
    }
}

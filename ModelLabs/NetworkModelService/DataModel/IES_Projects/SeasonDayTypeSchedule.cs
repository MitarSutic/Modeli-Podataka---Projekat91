using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FTN.Services.NetworkModelService.DataModel.Core;

namespace FTN.Services.NetworkModelService.DataModel.IES_Projects
{
    public class SeasonDayTypeSchedule : RegularIntervalSchedule
    {
        public SeasonDayTypeSchedule(long globalId) : base(globalId)
        {
        }
    }
}

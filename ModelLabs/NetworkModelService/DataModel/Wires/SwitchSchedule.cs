using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FTN.Services.NetworkModelService.DataModel.IES_Projects;

namespace FTN.Services.NetworkModelService.DataModel.Wires
{
    public class SwitchSchedule : SeasonDayTypeSchedule
    {
        public SwitchSchedule(long globalId) : base(globalId)
        {
        }
    }
}

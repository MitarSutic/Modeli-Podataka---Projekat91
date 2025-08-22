using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FTN.Services.NetworkModelService.DataModel.Wires;

namespace FTN.Services.NetworkModelService.DataModel.IES_Projects
{
    public class LoadBreakerSwitch : ProtectedSwitch
    {
        public LoadBreakerSwitch(long globalId) : base(globalId)
        {
        }
    }
}

﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FTN.Services.NetworkModelService.DataModel.Core
{
    public class RegularIntervalSchedule : RegularTimePoint
    {
        public RegularIntervalSchedule(long globalId) : base(globalId)
        {
        }
    }
}

namespace FTN.ESI.SIMES.CIM.CIMAdapter.Importer
{
    using System;
    using FTN.Common;

	/// <summary>
	/// PowerTransformerConverter has methods for populating
	/// ResourceDescription objects using PowerTransformerCIMProfile_Labs objects.
	/// </summary>
	public static class PowerTransformerConverter
	{

		#region Populate ResourceDescription
		public static void PopulateIdentifiedObjectProperties(FTN.IdentifiedObject cimIdentifiedObject, ResourceDescription rd)
		{
			if ((cimIdentifiedObject != null) && (rd != null))
			{
				if (cimIdentifiedObject.MRIDHasValue)
				{
					rd.AddProperty(new Property(ModelCode.IDOBJ_MRID, cimIdentifiedObject.MRID));
				}
				if (cimIdentifiedObject.NameHasValue)
				{
					rd.AddProperty(new Property(ModelCode.IDOBJ_NAME, cimIdentifiedObject.Name));
				}
				if (cimIdentifiedObject.AliasNameHasValue)
				{
					rd.AddProperty(new Property(ModelCode.IDOBJ_ALIAS_NAME, cimIdentifiedObject.AliasName));
				}
			}
		}

        internal static void PopulateBreakerProperties(Breaker cimBreaker, ResourceDescription rd)
        {
            if ((cimBreaker != null) && (rd != null))
            {
				PopulateProtectedSwitchProperties(cimBreaker, rd);
                if (cimBreaker.InTransitTimeHasValue)
                {
                    rd.AddProperty(new Property(ModelCode.BREAKER_ITT, cimBreaker.InTransitTime));
                }
            }
        }

        internal static void PopulateRecloserProperties(Recloser cimRecloser, ResourceDescription rd)
        {
            PopulateProtectedSwitchProperties(cimRecloser, rd);
        }

        internal static void PopulateLoadBreakProperties(LoadBreakSwitch cimLoadBreakSwitch, ResourceDescription rd)
        {
			PopulateProtectedSwitchProperties(cimLoadBreakSwitch, rd);
        }

        internal static void PopulateSwitchScheduleProperties(SwitchSchedule cimSwitchSchedule, ResourceDescription rd, ImportHelper importHelper, TransformAndLoadReport report)
        {
            if ((cimSwitchSchedule != null) && (rd != null))
            {
				PopulateSeasonDayTypeScheduleProperties(cimSwitchSchedule, rd, importHelper, report);
                if (cimSwitchSchedule.SwitchHasValue)
                {
                    long gid = importHelper.GetMappedGID(cimSwitchSchedule.Switch.ID);
                    if (gid < 0)
                    {
                        report.Report.Append("WARNING: Convert ").Append(cimSwitchSchedule.GetType().ToString()).Append(" rdfID = \"").Append(cimSwitchSchedule.ID);
                        report.Report.Append("\" - Failed to set reference to Switch: rdfID \"").Append(cimSwitchSchedule.Switch.ID).AppendLine(" \" is not mapped to GID!");
                    }
                    rd.AddProperty(new Property(ModelCode.SWITCH_SCHEDULE_SWITCH, gid));
                }
            }
        }

        internal static void PopulateSeasonProperties(Season cimSeason, ResourceDescription rd)
        {
            if ((cimSeason != null) && (rd != null))
            {
				PopulateIdentifiedObjectProperties(cimSeason, rd);	
                if (cimSeason.EndDateHasValue)
                {
                    rd.AddProperty(new Property(ModelCode.SEASON_END_DATE, cimSeason.EndDate));
                }
                if (cimSeason.StartDateHasValue)
                {
                    rd.AddProperty(new Property(ModelCode.SEASON_START_DATE, cimSeason.StartDate));
                }
            }
        }

        internal static void PopulateDayTypeProperties(DayType cimDayType, ResourceDescription rd)
        {
            PopulateIdentifiedObjectProperties (cimDayType, rd);
        }

        internal static void PopulateProtectedSwitchProperties(ProtectedSwitch cimProtectedSwitch, ResourceDescription rd)
        {
            if ((cimProtectedSwitch != null) && (rd != null))
            {
                PopulateSwitchProperties(cimProtectedSwitch, rd);
                if (cimProtectedSwitch.BreakingCapacityHasValue)
                {
                    rd.AddProperty(new Property(ModelCode.PR_SWITCH_BR_CAPACITY, cimProtectedSwitch.BreakingCapacity));
                }
            }
        }
        
		internal static void PopulateSwitchProperties(Switch cimSwitch, ResourceDescription rd)
		{
            if ((cimSwitch != null) && (rd != null))
            {
                PopulateConductingEquipmentProperties(cimSwitch, rd);
                if (cimSwitch.NormalOpenHasValue)
                {
                    rd.AddProperty(new Property(ModelCode.SWITCH_NORMAL_OPEN, cimSwitch.NormalOpen));
                }
                if (cimSwitch.RatedCurrentHasValue)
                {
                    rd.AddProperty(new Property(ModelCode.SWITCH_RATED_CURRENT, cimSwitch.RatedCurrent));
                }
                if (cimSwitch.RetainedHasValue)
                {
                    rd.AddProperty(new Property(ModelCode.SWITCH_RETAINED, cimSwitch.Retained));
                }
                if (cimSwitch.SwitchOnCountHasValue)
                {
                    rd.AddProperty(new Property(ModelCode.SWITCH_SWITCH_ON_COUNT, cimSwitch.SwitchOnCount));
                }
                if (cimSwitch.SwitchOnDateHasValue)
                {
                    rd.AddProperty(new Property(ModelCode.SWITCH_SWITCH_ON_DATE, cimSwitch.SwitchOnDate));
                }
            }
        }

        internal static void PopulateConductingEquipmentProperties(ConductingEquipment cimConductingEquipment, ResourceDescription rd)
		{
			PopulateEquipmentProperties(cimConductingEquipment, rd);
		}

        internal static void PopulateEquipmentProperties(Equipment cimEquipment, ResourceDescription rd)
		{
            if ((cimEquipment != null) && (rd != null))
            {
                PopulatePowerSystemResourceProperties(cimEquipment, rd);
                if (cimEquipment.AggregateHasValue)
                {
                    rd.AddProperty(new Property(ModelCode.EQUIPMENT_AGGREGATE, cimEquipment.Aggregate));
                }
                if (cimEquipment.NormallyInServiceHasValue)
                {
                    rd.AddProperty(new Property(ModelCode.EQUIPMENT_NORMALLY_IN_SERVICE, cimEquipment.NormallyInService));
                }
            }
        }
        public static void PopulatePowerSystemResourceProperties(FTN.PowerSystemResource cimPowerSystemResource, ResourceDescription rd)
		{
			PopulateIdentifiedObjectProperties(cimPowerSystemResource, rd);
		}

        public static void PopulateBasicIntervalScheduleProperties(FTN.BasicIntervalSchedule cimBasicIntervalSchedule, ResourceDescription rd)
		{
            if ((cimBasicIntervalSchedule != null) && (rd != null))
            {
                PopulateIdentifiedObjectProperties(cimBasicIntervalSchedule, rd);
                if (cimBasicIntervalSchedule.StartTimeHasValue)
                {
                    rd.AddProperty(new Property(ModelCode.BIS_START_TIME, cimBasicIntervalSchedule.StartTime));
                }
                if (cimBasicIntervalSchedule.Value1MultiplierHasValue)
                {
                    rd.AddProperty(new Property(ModelCode.BIS_VALUE1_MULTIPLIER, (short)GetDMSUnitMultiplier(cimBasicIntervalSchedule.Value1Multiplier)));
                }
                if (cimBasicIntervalSchedule.Value1UnitHasValue)
                {
                    rd.AddProperty(new Property(ModelCode.BIS_VALUE1_UNIT,(short)GetDMSUnitSymbol(cimBasicIntervalSchedule.Value1Unit)));
                }
                if (cimBasicIntervalSchedule.Value1MultiplierHasValue)
                {
                    rd.AddProperty(new Property(ModelCode.BIS_VALUE2_MULTIPLIER, (short)GetDMSUnitMultiplier(cimBasicIntervalSchedule.Value2Multiplier)));
                }
                if (cimBasicIntervalSchedule.Value1UnitHasValue)
                {
                    rd.AddProperty(new Property(ModelCode.BIS_VALUE2_UNIT, (short)GetDMSUnitSymbol(cimBasicIntervalSchedule.Value2Unit)));
                }
            }
        }

        internal static void PopulateSeasonDayTypeScheduleProperties(SeasonDayTypeSchedule cimSeasonDayTypeSchedule, ResourceDescription rd ,ImportHelper importHelper, TransformAndLoadReport report)
        {
            if ((cimSeasonDayTypeSchedule != null) && (rd != null))
            {
                PopulateRegularIntervalScheduleProperties(cimSeasonDayTypeSchedule, rd);
                if (cimSeasonDayTypeSchedule.SeasonHasValue)
                {
                    long gid = importHelper.GetMappedGID(cimSeasonDayTypeSchedule.Season.ID);
                    if (gid < 0)
                    {
                        report.Report.Append("WARNING: Convert ").Append(cimSeasonDayTypeSchedule.GetType().ToString()).Append(" rdfID = \"").Append(cimSeasonDayTypeSchedule.ID);
                        report.Report.Append("\" - Failed to set reference to Season: rdfID \"").Append(cimSeasonDayTypeSchedule.Season.ID).AppendLine(" \" is not mapped to GID!");
                    }
                    rd.AddProperty(new Property(ModelCode.SEASON_DTS_SEASON, gid));
                }
                if (cimSeasonDayTypeSchedule.DayTypeHasValue)
                {
                    long gid = importHelper.GetMappedGID(cimSeasonDayTypeSchedule.DayType.ID);
                    if (gid < 0)
                    {
                        report.Report.Append("WARNING: Convert ").Append(cimSeasonDayTypeSchedule.GetType().ToString()).Append(" rdfID = \"").Append(cimSeasonDayTypeSchedule.ID);
                        report.Report.Append("\" - Failed to set reference to Daytype: rdfID \"").Append(cimSeasonDayTypeSchedule.DayType.ID).AppendLine(" \" is not mapped to GID!");
                    }
                    rd.AddProperty(new Property(ModelCode.SEASON_DTS_DAYTYPE, gid));
                }
            }
        }

        internal static void PopulateRegularIntervalScheduleProperties( RegularIntervalSchedule cimRegularIntervalSchedule, ResourceDescription rd)
        {
            PopulateBasicIntervalScheduleProperties(cimRegularIntervalSchedule, rd);
        }

        internal static void PopulateRegularTimePointProperties(RegularTimePoint cimRegularTimePoint, ResourceDescription rd, ImportHelper importHelper, TransformAndLoadReport report)
        {
            if ((cimRegularTimePoint != null) && (rd != null))
            {
                PowerTransformerConverter.PopulateIdentifiedObjectProperties(cimRegularTimePoint, rd);

                if (cimRegularTimePoint.SequenceNumberHasValue)
                {
                    rd.AddProperty(new Property(ModelCode.REG_TIME_POINT_SEQ_NUM, cimRegularTimePoint.SequenceNumber));
                }
                if (cimRegularTimePoint.Value1HasValue)
                {
                    rd.AddProperty(new Property(ModelCode.REG_TIME_POINT_VAL1, cimRegularTimePoint.Value1));
                }
                if (cimRegularTimePoint.Value2HasValue)
                {
                    rd.AddProperty(new Property(ModelCode.REG_TIME_POINT_VAL2, cimRegularTimePoint.Value2));
                }
                if (cimRegularTimePoint.IntervalScheduleHasValue)
                {
                    long gid = importHelper.GetMappedGID(cimRegularTimePoint.IntervalSchedule.ID);
                    if (gid < 0)
                    {
                        report.Report.Append("WARNING: Convert ").Append(cimRegularTimePoint.GetType().ToString()).Append(" rdfID = \"").Append(cimRegularTimePoint.ID);
                        report.Report.Append("\" - Failed to set reference to Interval Schedule: rdfID \"").Append(cimRegularTimePoint.IntervalSchedule.ID).AppendLine(" \" is not mapped to GID!");
                    }
                    rd.AddProperty(new Property(ModelCode.REG_TIME_POINT_INTERVAL_SCHEDULE, gid));
                }
            }
        }
        #endregion Populate ResourceDescription

        #region Enums convert
        public static UnitMultiplier GetDMSUnitMultiplier(FTN.UnitMultiplier unitMultiplier)
		{
			switch (unitMultiplier)
			{
                case FTN.UnitMultiplier.c:
					return UnitMultiplier.c;
                case FTN.UnitMultiplier.d:
                    return UnitMultiplier.d;
                case FTN.UnitMultiplier.m:
                    return UnitMultiplier.m;
                case FTN.UnitMultiplier.n:
                    return UnitMultiplier.n;
                case FTN.UnitMultiplier.k:
                    return UnitMultiplier.k;
                case FTN.UnitMultiplier.G:
                    return UnitMultiplier.G;
                case FTN.UnitMultiplier.M:
                    return UnitMultiplier.M;
                case FTN.UnitMultiplier.micro:
                    return UnitMultiplier.micro;
                case FTN.UnitMultiplier.p:
                    return UnitMultiplier.p;
                case FTN.UnitMultiplier.T:
                    return UnitMultiplier.T;
                case FTN.UnitMultiplier.none:
                    return UnitMultiplier.none;

                default: return UnitMultiplier.c;
			}
		}

        public static UnitSymbol GetDMSUnitSymbol(FTN.UnitSymbol unitSymbol)
        {
            switch (unitSymbol)
            {
                case FTN.UnitSymbol.A:
                    return UnitSymbol.A;
                case FTN.UnitSymbol.F:
                    return UnitSymbol.F;
                case FTN.UnitSymbol.H:
                    return UnitSymbol.H;
                case FTN.UnitSymbol.Hz:
                    return UnitSymbol.Hz;
                case FTN.UnitSymbol.J:
                    return UnitSymbol.J;
                case FTN.UnitSymbol.N:
                    return UnitSymbol.N;
                case FTN.UnitSymbol.Pa:
                    return UnitSymbol.Pa;
                case FTN.UnitSymbol.S:
                    return UnitSymbol.S;
                case FTN.UnitSymbol.V:
                    return UnitSymbol.V;
                case FTN.UnitSymbol.VA:
                    return UnitSymbol.VA;
                case FTN.UnitSymbol.VAh:
                    return UnitSymbol.VAh;
                case FTN.UnitSymbol.VAr:
                    return UnitSymbol.VAr;
                case FTN.UnitSymbol.W:
                    return UnitSymbol.W;
                case FTN.UnitSymbol.Wh:
                    return UnitSymbol.Wh;
                case FTN.UnitSymbol.deg:
                    return UnitSymbol.deg;
                case FTN.UnitSymbol.degC:
                    return UnitSymbol.degC;
                case FTN.UnitSymbol.g:
                    return UnitSymbol.g;
                case FTN.UnitSymbol.h:
                    return UnitSymbol.h;
                case FTN.UnitSymbol.m:
                    return UnitSymbol.m;
                case FTN.UnitSymbol.m2:
                    return UnitSymbol.m2;
                case FTN.UnitSymbol.m3:
                    return UnitSymbol.m3;
                case FTN.UnitSymbol.min:
                    return UnitSymbol.min;
                case FTN.UnitSymbol.none:
                    return UnitSymbol.none;
                case FTN.UnitSymbol.ohm:
                    return UnitSymbol.ohm;
                case FTN.UnitSymbol.rad:
                    return UnitSymbol.rad;
                case FTN.UnitSymbol.s:
                    return UnitSymbol.s;
                default: return UnitSymbol.A;
            }
        }
        #endregion Enums convert
    }
}

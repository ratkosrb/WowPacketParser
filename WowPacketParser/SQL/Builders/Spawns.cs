using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using WowPacketParser.Enums;
using WowPacketParser.Enums.Version;
using WowPacketParser.Misc;
using WowPacketParser.Store;
using WowPacketParser.Store.Objects;

namespace WowPacketParser.SQL.Builders
{
    [BuilderClass]
    public static class Spawns
    {
        private static bool GetTransportMap(WoWObject @object, out int mapId)
        {
            mapId = -1;

            WoWObject transport;
            if (!Storage.Objects.TryGetValue(@object.Movement.TransportGuid, out transport))
                return false;

            if (transport.Type != ObjectType.GameObject)
                return false;

            if (SQLConnector.Enabled)
            {
                var transportTemplates = SQLDatabase.Get(new RowList<GameObjectTemplate> { new GameObjectTemplate { Entry = (uint)transport.ObjectData.EntryID } });
                if (transportTemplates.Count == 0)
                    return false;

                mapId = transportTemplates.First().Data.Data[6].GetValueOrDefault();
            }

            return true;
        }

        [BuilderMethod(Units = true)]
        public static string Creature(Dictionary<WowGuid, Unit> units)
        {
            if (units.Count == 0)
                return string.Empty;

            if (!Settings.SqlTables.creature)
                return string.Empty;

            uint count = 0;
            uint maxDbGuid = 0;
            uint threatTargetsCounter = 1;
            var rows = new RowList<Creature>();
            var powerValuesRows = new RowList<CreaturePowerValues>();
            var guidValuesRows = new RowList<CreatureGuidValues>();
            var addonRows = new RowList<CreatureAddon>();
            var interactRows = new RowList<CreatureClientInteract>();
            var flightSplineSyncRows = new RowList<CreatureFlightSplineSync>();
            var create1Rows = new RowList<CreatureCreate1>();
            var create2Rows = new RowList<CreatureCreate2>();
            var destroyRows = new RowList<CreatureDestroy>();
            var movementRows = new RowList<ServerSideMovement>();
            var movementCombatRows = new RowList<ServerSideMovement>();
            var movementCombatSplineRows = new RowList<ServerSideMovementSpline>();
            var movementSplineRows = new RowList<ServerSideMovementSpline>();
            var petNameRows = new RowList<CreaturePetName>();
            var updateAurasRows = new RowList<CreatureAurasUpdate>();
            var updateThreatRows = new RowList<CreatureThreatUpdate>();
            var updateThreatTargetRows = new RowList<CreatureThreatUpdateTarget>();
            var updateValuesRows = new RowList<CreatureValuesUpdate>();
            var updateSpeedRows = new RowList<CreatureSpeedUpdate>();
            var threatClearRows = new RowList<CreatureThreatClear>();
            var threatRemoveRows = new RowList<CreatureThreatRemove>();
            var attackLogRows = new RowList<UnitMeleeAttackLog>();
            var attackStartRows = new RowList<CreatureAttackToggle>();
            var attackStopRows = new RowList<CreatureAttackToggle>();
            var updateEquipmentValuesRows = new RowList<CreatureEquipmentValuesUpdate>();
            var updateGuidValuesRows = new RowList<CreatureGuidValuesUpdate>();
            var updatePowerValuesRows = new RowList<CreaturePowerValuesUpdate>();
            var emoteRows = new RowList<CreatureEmote>();
            foreach (var unit in units)
            {
                Unit creature = unit.Value;

                if (Settings.AreaFilters.Length > 0)
                    if (!(creature.Area.ToString(CultureInfo.InvariantCulture).MatchesFilters(Settings.AreaFilters)))
                        continue;

                if (Settings.MapFilters.Length > 0)
                    if (!(creature.Map.ToString(CultureInfo.InvariantCulture).MatchesFilters(Settings.MapFilters)))
                        continue;

                uint entry = (uint)creature.ObjectDataOriginal.EntryID;
                if (entry == 0)
                    continue;   // broken entry, nothing to spawn

                if (creature.IsTemporarySpawn() && !Settings.SaveTempSpawns)
                    continue;

                if ((unit.Key.GetHighType() == HighGuidType.Pet) && !Settings.SavePets)
                    continue;

                Row<Creature> row = new Row<Creature>();
                row.Data.GUID = "@CGUID+" + creature.DbGuid;
                row.Data.OriginalID = unit.Key.GetEntry();
                row.Data.ID = entry;
                row.Data.TotalObservationTime = creature.TotalObservedTime;
                row.Data.LongestObservationTime = creature.LongestObservedTime;

                bool badTransport = false;
                if (!creature.WasOriginallyOnTransport() || Settings.SaveTransports)
                    row.Data.Map = creature.Map;
                else
                {
                    int mapId;
                    badTransport = !GetTransportMap(creature, out mapId);
                    if (mapId != -1)
                        row.Data.Map = (uint)mapId;
                }

                if (creature.WasOriginallyOnTransport())
                    row.Data.IsOnTransport = 1;

                row.Data.AreaID = 0;
                if (creature.Area != -1)
                    row.Data.AreaID = (uint)creature.Area;

                row.Data.ZoneID = 0;
                if (creature.Zone != -1)
                    row.Data.ZoneID = (uint)creature.Zone;

                row.Data.SpawnMask = (uint)creature.GetDefaultSpawnMask();

                if (ClientVersion.AddedInVersion(ClientVersionBuild.V7_0_3_22248))
                {
                    string data = string.Join(",", creature.GetDefaultSpawnDifficulties());
                    if (string.IsNullOrEmpty(data))
                        data = "0";

                    row.Data.SpawnDifficulties = data;
                }

                row.Data.PhaseMask = creature.PhaseMask;

                if (ClientVersion.AddedInVersion(ClientVersionBuild.V4_3_4_15595) && creature.Phases != null)
                {
                    string data = string.Join(" - ", creature.Phases);
                    if (string.IsNullOrEmpty(data) || Settings.ForcePhaseZero)
                        data = "0";

                    row.Data.PhaseID = data;
                }

                if (!creature.WasOriginallyOnTransport() || Settings.SaveTransports)
                {
                    row.Data.PositionX = creature.OriginalMovement.Position.X;
                    row.Data.PositionY = creature.OriginalMovement.Position.Y;
                    row.Data.PositionZ = creature.OriginalMovement.Position.Z;
                    row.Data.Orientation = creature.OriginalMovement.Orientation;
                }
                else
                {
                    row.Data.PositionX = creature.OriginalMovement.TransportOffset.X;
                    row.Data.PositionY = creature.OriginalMovement.TransportOffset.Y;
                    row.Data.PositionZ = creature.OriginalMovement.TransportOffset.Z;
                    row.Data.Orientation = creature.OriginalMovement.TransportOffset.O;
                }

                //row.Data.SpawnTimeSecs = creature.GetDefaultSpawnTime(creature.DifficultyID);
                row.Data.MovementType = creature.Movement.HasWpsOrRandMov ? 1u : 0u;
                row.Data.WaypointCount = 0;
                row.Data.WanderDistance = 0;

                // set some defaults
                Store.Objects.UpdateFields.IUnitData unitData = creature.UnitDataOriginal != null ? creature.UnitDataOriginal : creature.UnitData;
                row.Data.PhaseGroup = 0;
                row.Data.MovementFlags = creature.OriginalMovement.Flags;
                row.Data.IsSpawn = creature.FirstCreateType;
                row.Data.Hover = (byte)(creature.OriginalMovement.Hover ? 1 : 0);
                row.Data.TemporarySpawn = (byte)(creature.IsTemporarySpawn() ? 1 : 0);
                row.Data.IsPet = (byte)((unit.Key.GetHighType() == HighGuidType.Pet) ? 1 : 0);
                row.Data.IsVehicle = (byte)((unit.Key.GetHighType() == HighGuidType.Vehicle) ? 1 : 0);
                row.Data.SummonSpell = (uint)unitData.CreatedBySpell;
                row.Data.Scale = creature.ObjectDataOriginal.Scale;
                row.Data.DisplayID = (uint)unitData.DisplayID;
                row.Data.NativeDisplayID = (uint)unitData.NativeDisplayID;
                row.Data.MountDisplayID = (uint)unitData.MountDisplayID;
                row.Data.ClassId = unitData.ClassId;
                row.Data.Gender = unitData.Sex;
                row.Data.FactionTemplate = (uint)unitData.FactionTemplate;
                row.Data.Level = (uint)unitData.Level;
                row.Data.NpcFlag = unitData.NpcFlags[0];
                row.Data.UnitFlag = unitData.Flags;
                row.Data.UnitFlag2 = unitData.Flags2;
                row.Data.DynamicFlags = creature.GetDynamicFlagsOriginal();
                row.Data.CurHealth = (uint)unitData.Health;
                row.Data.MaxHealth = (uint)unitData.MaxHealth;
                row.Data.PowerType = unitData.DisplayPower;

                if (ClientVersion.AddedInVersion(ClientVersionBuild.V4_1_0a_14007))
                {
                    // power indexes are class specific
                    row.Data.CurrentPower = (uint)unitData.Mana;
                    row.Data.MaxPower = (uint)unitData.MaxMana;
                }
                else
                {
                    row.Data.CurrentPower = (uint)unitData.Power[(int)row.Data.PowerType];
                    row.Data.MaxPower = (uint)unitData.MaxPower[(int)row.Data.PowerType];
                }
                
                row.Data.CurrentMana = (uint)unitData.Mana;
                row.Data.AuraState = unitData.AuraState;
                row.Data.EmoteState = (uint)unitData.EmoteState;
                row.Data.StandState = unitData.StandState;
                //row.Data.PetTalentPoints = unitData.PetTalentPoints;
                row.Data.VisFlags = unitData.VisFlags;
                row.Data.AnimTier = unitData.AnimTier;
                row.Data.SheatheState = unitData.SheatheState;
                row.Data.MiscFlags = unitData.MiscFlags;
                row.Data.DebuffLimit = unitData.DebuffLimit;
                row.Data.PvpFlags = unitData.PvpFlags;
                //row.Data.PetFlags = unitData.PetFlags;
                row.Data.ShapeshiftForm = unitData.ShapeshiftForm;
                row.Data.SpeedWalk = creature.OriginalMovement.WalkSpeed / MovementInfo.DEFAULT_WALK_SPEED;
                row.Data.SpeedRun = creature.OriginalMovement.RunSpeed / MovementInfo.DEFAULT_RUN_SPEED;
                row.Data.SpeedRunBack = creature.OriginalMovement.RunBackSpeed / MovementInfo.DEFAULT_RUN_BACK_SPEED;
                row.Data.SpeedSwim = creature.OriginalMovement.SwimSpeed / MovementInfo.DEFAULT_SWIM_SPEED;
                row.Data.SpeedSwimBack = creature.OriginalMovement.SwimBackSpeed / MovementInfo.DEFAULT_SWIM_BACK_SPEED;
                row.Data.SpeedFly = creature.OriginalMovement.FlightSpeed / MovementInfo.DEFAULT_FLY_SPEED;
                row.Data.SpeedFlyBack = creature.OriginalMovement.FlightBackSpeed / MovementInfo.DEFAULT_FLY_BACK_SPEED;
                row.Data.BoundingRadius = unitData.BoundingRadius;
                row.Data.CombatReach = unitData.CombatReach;
                row.Data.ModMeleeHaste = unitData.ModHaste;
                row.Data.MainHandAttackTime = unitData.AttackRoundBaseTime[0];
                row.Data.OffHandAttackTime = unitData.AttackRoundBaseTime[1];
                row.Data.MainHandSlotItem = (uint)unitData.VirtualItems[0].ItemID;
                row.Data.OffHandSlotItem = (uint)unitData.VirtualItems[1].ItemID;
                row.Data.RangedSlotItem = (uint)unitData.VirtualItems[2].ItemID;
                row.Data.ChannelSpellId = (uint)unitData.ChannelData.SpellID;
                row.Data.ChannelVisualId = (uint)unitData.ChannelData.SpellXSpellVisualID;

                row.Data.SniffId = "@SNIFFID+" + creature.SourceSniffId;
                row.Data.SniffBuild = creature.SourceSniffBuild;

                row.Data.Auras = creature.GetOriginalAurasString(false);

                if (Settings.SqlTables.creature_guid_values)
                {
                    if (!unitData.Charm.IsEmpty() ||
                        !unitData.Summon.IsEmpty() ||
                        !unitData.CharmedBy.IsEmpty() ||
                        !unitData.SummonedBy.IsEmpty() ||
                        !unitData.CreatedBy.IsEmpty() ||
                        !unitData.DemonCreator.IsEmpty() ||
                        !unitData.Target.IsEmpty())
                    {
                        Row<CreatureGuidValues> guidsRow = new Row<CreatureGuidValues>();
                        guidsRow.Data.GUID = "@CGUID+" + creature.DbGuid;
                        Storage.GetObjectDbGuidEntryType(unitData.Charm, out guidsRow.Data.CharmGuid, out guidsRow.Data.CharmId, out guidsRow.Data.CharmType);
                        Storage.GetObjectDbGuidEntryType(unitData.Summon, out guidsRow.Data.SummonGuid, out guidsRow.Data.SummonId, out guidsRow.Data.SummonType);
                        Storage.GetObjectDbGuidEntryType(unitData.CharmedBy, out guidsRow.Data.CharmedByGuid, out guidsRow.Data.CharmedById, out guidsRow.Data.CharmedByType);
                        Storage.GetObjectDbGuidEntryType(unitData.SummonedBy, out guidsRow.Data.SummonedByGuid, out guidsRow.Data.SummonedById, out guidsRow.Data.SummonedByType);
                        Storage.GetObjectDbGuidEntryType(unitData.CreatedBy, out guidsRow.Data.CreatedByGuid, out guidsRow.Data.CreatedById, out guidsRow.Data.CreatedByType);
                        Storage.GetObjectDbGuidEntryType(unitData.DemonCreator, out guidsRow.Data.DemonCreatorGuid, out guidsRow.Data.DemonCreatorId, out guidsRow.Data.DemonCreatorType);
                        Storage.GetObjectDbGuidEntryType(unitData.Target, out guidsRow.Data.TargetGuid, out guidsRow.Data.TargetId, out guidsRow.Data.TargetType);
                        guidValuesRows.Add(guidsRow);
                    } 
                }

                if (Settings.SqlTables.creature_power_values)
                {
                    var powers = unitData.Power;
                    var maxPowers = unitData.MaxPower;
                    for (int i = 0; i < ClientVersion.GetPowerCountForClientVersion(); i++)
                    {
                        if (powers[i] != 0 || maxPowers[i] != 0)
                        {
                            Row<CreaturePowerValues> powerRow = new Row<CreaturePowerValues>();
                            powerRow.Data.GUID = "@CGUID+" + creature.DbGuid;
                            powerRow.Data.PowerType = (uint)i;
                            powerRow.Data.CurrentPower = (uint)powers[i];
                            powerRow.Data.MaxPower = (uint)maxPowers[i];
                            powerValuesRows.Add(powerRow);
                        }
                    }
                }

                if (Settings.SqlTables.client_creature_interact)
                {
                    if (Storage.CreatureClientInteractTimes.ContainsKey(unit.Key))
                    {
                        foreach (var interactTime in Storage.CreatureClientInteractTimes[unit.Key])
                        {
                            var interactRow = new Row<CreatureClientInteract>();
                            interactRow.Data.GUID = "@CGUID+" + creature.DbGuid;
                            interactRow.Data.UnixTimeMs = (ulong)Utilities.GetUnixTimeMsFromDateTime(interactTime);
                            interactRows.Add(interactRow);
                        }
                    }
                }

                if (Settings.SqlTables.creature_create1_time)
                {
                    if (Storage.ObjectCreate1Times.ContainsKey(unit.Key))
                    {
                        foreach (var createTime in Storage.ObjectCreate1Times[unit.Key])
                        {
                            var create1Row = new Row<CreatureCreate1>();
                            create1Row.Data.GUID = "@CGUID+" + creature.DbGuid;
                            create1Row.Data.Map = createTime.Map;
                            create1Row.Data.PositionX = createTime.MoveInfo.Position.X;
                            create1Row.Data.PositionY = createTime.MoveInfo.Position.Y;
                            create1Row.Data.PositionZ = createTime.MoveInfo.Position.Z;
                            create1Row.Data.Orientation = createTime.MoveInfo.Orientation;
                            create1Row.Data.VehicleId = createTime.MoveInfo.VehicleId;
                            create1Row.Data.VehicleOrientation = createTime.MoveInfo.VehicleOrientation;
                            if (createTime.MoveInfo.TransportGuid != null && !createTime.MoveInfo.TransportGuid.IsEmpty())
                            {
                                Storage.GetObjectDbGuidEntryType(createTime.MoveInfo.TransportGuid, out create1Row.Data.TransportGuid, out create1Row.Data.TransportId, out create1Row.Data.TransportType);
                                create1Row.Data.TransportPositionX = createTime.MoveInfo.TransportOffset.X;
                                create1Row.Data.TransportPositionY = createTime.MoveInfo.TransportOffset.Y;
                                create1Row.Data.TransportPositionZ = createTime.MoveInfo.TransportOffset.Z;
                                create1Row.Data.TransportOrientation = createTime.MoveInfo.TransportOffset.O;
                                create1Row.Data.TransportTime = createTime.MoveInfo.TransportTime;
                                create1Row.Data.TransportSeat = createTime.MoveInfo.TransportSeat;
                            }
                            create1Row.Data.MoveTime = createTime.MoveInfo.MoveTime;
                            create1Row.Data.MoveFlags = createTime.MoveInfo.Flags;
                            create1Row.Data.MoveFlags2 = createTime.MoveInfo.Flags2;
                            create1Row.Data.SwimPitch = createTime.MoveInfo.SwimPitch;
                            create1Row.Data.FallTime = createTime.MoveInfo.FallTime;
                            create1Row.Data.JumpHorizontalSpeed = createTime.MoveInfo.JumpHorizontalSpeed;
                            create1Row.Data.JumpVerticalSpeed = createTime.MoveInfo.JumpVerticalSpeed;
                            create1Row.Data.JumpCosAngle = createTime.MoveInfo.JumpCosAngle;
                            create1Row.Data.JumpSinAngle = createTime.MoveInfo.JumpSinAngle;
                            create1Row.Data.SplineElevation = createTime.MoveInfo.SplineElevation;
                            create1Row.Data.UnixTimeMs = createTime.UnixTimeMs;
                            create1Rows.Add(create1Row);
                        }
                    }
                }

                if (Settings.SqlTables.creature_create2_time)
                {
                    if (Storage.ObjectCreate2Times.ContainsKey(unit.Key))
                    {
                        foreach (var createTime in Storage.ObjectCreate2Times[unit.Key])
                        {
                            var create2Row = new Row<CreatureCreate2>();
                            create2Row.Data.GUID = "@CGUID+" + creature.DbGuid;
                            create2Row.Data.Map = createTime.Map;
                            create2Row.Data.PositionX = createTime.MoveInfo.Position.X;
                            create2Row.Data.PositionY = createTime.MoveInfo.Position.Y;
                            create2Row.Data.PositionZ = createTime.MoveInfo.Position.Z;
                            create2Row.Data.Orientation = createTime.MoveInfo.Orientation;
                            create2Row.Data.VehicleId = createTime.MoveInfo.VehicleId;
                            create2Row.Data.VehicleOrientation = createTime.MoveInfo.VehicleOrientation;
                            if (createTime.MoveInfo.TransportGuid != null && !createTime.MoveInfo.TransportGuid.IsEmpty())
                            {
                                Storage.GetObjectDbGuidEntryType(createTime.MoveInfo.TransportGuid, out create2Row.Data.TransportGuid, out create2Row.Data.TransportId, out create2Row.Data.TransportType);
                                create2Row.Data.TransportPositionX = createTime.MoveInfo.TransportOffset.X;
                                create2Row.Data.TransportPositionY = createTime.MoveInfo.TransportOffset.Y;
                                create2Row.Data.TransportPositionZ = createTime.MoveInfo.TransportOffset.Z;
                                create2Row.Data.TransportOrientation = createTime.MoveInfo.TransportOffset.O;
                                create2Row.Data.TransportTime = createTime.MoveInfo.TransportTime;
                                create2Row.Data.TransportSeat = createTime.MoveInfo.TransportSeat;
                            }
                            create2Row.Data.MoveTime = createTime.MoveInfo.MoveTime;
                            create2Row.Data.MoveFlags = createTime.MoveInfo.Flags;
                            create2Row.Data.MoveFlags2 = createTime.MoveInfo.Flags2;
                            create2Row.Data.SwimPitch = createTime.MoveInfo.SwimPitch;
                            create2Row.Data.FallTime = createTime.MoveInfo.FallTime;
                            create2Row.Data.JumpHorizontalSpeed = createTime.MoveInfo.JumpHorizontalSpeed;
                            create2Row.Data.JumpVerticalSpeed = createTime.MoveInfo.JumpVerticalSpeed;
                            create2Row.Data.JumpCosAngle = createTime.MoveInfo.JumpCosAngle;
                            create2Row.Data.JumpSinAngle = createTime.MoveInfo.JumpSinAngle;
                            create2Row.Data.SplineElevation = createTime.MoveInfo.SplineElevation;
                            create2Row.Data.UnixTimeMs = createTime.UnixTimeMs;
                            create2Rows.Add(create2Row);
                        }
                    }
                }

                if (Settings.SqlTables.creature_destroy_time)
                {
                    if (Storage.ObjectDestroyTimes.ContainsKey(unit.Key))
                    {
                        foreach (var createTime in Storage.ObjectDestroyTimes[unit.Key])
                        {
                            var destroyRow = new Row<CreatureDestroy>();
                            destroyRow.Data.GUID = "@CGUID+" + creature.DbGuid;
                            destroyRow.Data.UnixTimeMs = (ulong)Utilities.GetUnixTimeMsFromDateTime(createTime);
                            destroyRows.Add(destroyRow);
                        }
                    }
                }

                if (Settings.SqlTables.creature_auras_update)
                {
                    if (Storage.UnitAurasUpdates.ContainsKey(unit.Key))
                    {
                        uint updateId = 0;
                        foreach (var update in Storage.UnitAurasUpdates[unit.Key])
                        {
                            updateId++;

                            if (update.Auras == null)
                            {
                                var updateRow = new Row<CreatureAurasUpdate>();
                                updateRow.Data.GUID = "@CGUID+" + creature.DbGuid;
                                updateRow.Data.UpdateId = updateId;
                                updateRow.Data.IsFullUpdate = update.IsFullUpdate;
                                updateRow.Data.Slot = -1;
                                updateRow.Data.UnixTimeMs = (ulong)Utilities.GetUnixTimeMsFromDateTime(update.Time);
                                updateAurasRows.Add(updateRow);
                                continue;
                            }

                            foreach (var aura in update.Auras)
                            {
                                var updateRow = new Row<CreatureAurasUpdate>();
                                updateRow.Data.GUID = "@CGUID+" + creature.DbGuid;
                                updateRow.Data.UpdateId = updateId;
                                updateRow.Data.IsFullUpdate = update.IsFullUpdate;
                                updateRow.Data.Slot = (int)aura.Slot;
                                updateRow.Data.SpellId = aura.SpellId;
                                updateRow.Data.VisualId = aura.VisualId;
                                updateRow.Data.AuraFlags = aura.AuraFlags;
                                updateRow.Data.ActiveFlags = aura.ActiveFlags;
                                updateRow.Data.Level = aura.Level;
                                updateRow.Data.Charges = aura.Charges;
                                updateRow.Data.ContentTuningId = aura.ContentTuningId;
                                updateRow.Data.Duration = aura.Duration;
                                updateRow.Data.MaxDuration = aura.MaxDuration;
                                Storage.GetObjectDbGuidEntryType(aura.CasterGuid, out updateRow.Data.CasterGuid, out updateRow.Data.CasterId, out updateRow.Data.CasterType);
                                updateRow.Data.UnixTimeMs = (ulong)Utilities.GetUnixTimeMsFromDateTime(update.Time);
                                updateAurasRows.Add(updateRow);
                            }
                        }
                    }
                }

                if (Settings.SqlTables.creature_threat_clear)
                {
                    if (Storage.CreatureThreatClears.ContainsKey(unit.Key))
                    {
                        foreach (var threatClear in Storage.CreatureThreatClears[unit.Key])
                        {
                            var threatClearRow = new Row<CreatureThreatClear>();
                            threatClearRow.Data = threatClear;
                            threatClearRow.Data.GUID = "@CGUID+" + creature.DbGuid;
                            threatClearRow.Data.UnixTimeMs = (ulong)Utilities.GetUnixTimeMsFromDateTime(threatClearRow.Data.Time);
                            threatClearRows.Add(threatClearRow);
                        }
                    }
                }

                if (Settings.SqlTables.creature_threat_remove)
                {
                    if (Storage.CreatureThreatRemoves.ContainsKey(unit.Key))
                    {
                        foreach (var threatRemove in Storage.CreatureThreatRemoves[unit.Key])
                        {
                            var threatRemoveRow = new Row<CreatureThreatRemove>();
                            threatRemoveRow.Data = threatRemove;
                            threatRemoveRow.Data.GUID = "@CGUID+" + creature.DbGuid;
                            Storage.GetObjectDbGuidEntryType(threatRemove.TargetGUID, out threatRemoveRow.Data.TargetGuid, out threatRemoveRow.Data.TargetId, out threatRemoveRow.Data.TargetType);
                            threatRemoveRow.Data.UnixTimeMs = (ulong)Utilities.GetUnixTimeMsFromDateTime(threatRemoveRow.Data.Time);
                            threatRemoveRows.Add(threatRemoveRow);
                        }
                    }
                }

                if (Settings.SqlTables.creature_threat_update)
                {
                    if (Storage.CreatureThreatUpdates.ContainsKey(unit.Key))
                    {
                        foreach (var update in Storage.CreatureThreatUpdates[unit.Key])
                        {
                            var updateRow = new Row<CreatureThreatUpdate>();
                            updateRow.Data = update;
                            updateRow.Data.GUID = "@CGUID+" + creature.DbGuid;
                            updateRow.Data.UnixTimeMs = (ulong)Utilities.GetUnixTimeMsFromDateTime(updateRow.Data.Time);

                            if (updateRow.Data.TargetsCount > 0)
                            {
                                updateRow.Data.TargetListId = threatTargetsCounter++;
                                foreach (var target in updateRow.Data.TargetsList)
                                {
                                    var targetRow = new Row<CreatureThreatUpdateTarget>();
                                    targetRow.Data.TargetListId = updateRow.Data.TargetListId;
                                    Storage.GetObjectDbGuidEntryType(target.Item1, out targetRow.Data.TargetGuid, out targetRow.Data.TargetId, out targetRow.Data.TargetType);
                                    targetRow.Data.Threat = target.Item2;
                                    updateThreatTargetRows.Add(targetRow);
                                }
                            }
                            updateThreatRows.Add(updateRow);
                        }
                    }
                }

                if (Settings.SqlTables.creature_values_update)
                {
                    if (Storage.UnitValuesUpdates.ContainsKey(unit.Key))
                    {
                        foreach (var update in Storage.UnitValuesUpdates[unit.Key])
                        {
                            var updateRow = new Row<CreatureValuesUpdate>();
                            updateRow.Data = update;
                            updateRow.Data.GUID = "@CGUID+" + creature.DbGuid;
                            updateValuesRows.Add(updateRow);
                        }
                    }
                }

                if (Settings.SqlTables.creature_power_values_update)
                {
                    if (Storage.UnitPowerValuesUpdates.ContainsKey(unit.Key))
                    {
                        foreach (var update in Storage.UnitPowerValuesUpdates[unit.Key])
                        {
                            var updateRow = new Row<CreaturePowerValuesUpdate>();
                            updateRow.Data = update;
                            updateRow.Data.GUID = "@CGUID+" + creature.DbGuid;
                            updatePowerValuesRows.Add(updateRow);
                        }
                    }
                }

                if (Settings.SqlTables.creature_speed_update)
                {
                    if (Storage.UnitSpeedUpdates.ContainsKey(unit.Key))
                    {
                        foreach (var update in Storage.UnitSpeedUpdates[unit.Key])
                        {
                            var updateRow = new Row<CreatureSpeedUpdate>();
                            updateRow.Data = update;
                            updateRow.Data.GUID = "@CGUID+" + creature.DbGuid;
                            updateSpeedRows.Add(updateRow);
                        }
                    }
                }

                bool isFlyingOrCyclic = creature.HasOnlyCyclicMovement;
                row.Data.WaypointCount = creature.TotalMovementsCount;
                row.Data.UniqueWaypointCount = (uint)creature.UniqueWaypoints.Count;
                row.Data.WanderDistance = creature.MaxTravelDistanceFromSpawn;

                // Likely to be waypoints if distance is big
                if (row.Data.WanderDistance > 20)
                    row.Data.MovementType = (uint)(isFlyingOrCyclic ? 3 : 2);

                if (Settings.SqlTables.creature_movement_server && creature.Waypoints != null &&
                   (row.Data.MovementType >= 2 || Settings.TargetedDbType == TargetedDbType.WPP))
                {
                    if (creature.WaypointSplines != null)
                    {
                        foreach (ServerSideMovementSpline waypoint in creature.WaypointSplines)
                        {
                            var movementSplineRow = new Row<ServerSideMovementSpline>();
                            movementSplineRow.Data = waypoint;
                            movementSplineRow.Data.GUID = "@CGUID+" + creature.DbGuid;
                            movementSplineRow.Comment += StoreGetters.GetName(StoreNameType.Unit, (int)unit.Key.GetEntry(), false);
                            movementSplineRows.Add(movementSplineRow);
                        }
                    }

                    foreach (ServerSideMovement waypoint in creature.Waypoints)
                    {
                        var movementRow = new Row<ServerSideMovement>();
                        movementRow.Data = waypoint;
                        movementRow.Data.GUID = "@CGUID+" + creature.DbGuid;
                        if (waypoint.TransportGuid != null && !waypoint.TransportGuid.IsEmpty())
                            Storage.GetObjectDbGuidEntryType(waypoint.TransportGuid, out movementRow.Data.TransportGUID, out movementRow.Data.TransportId, out movementRow.Data.TransportType);
                        movementRow.Comment += StoreGetters.GetName(StoreNameType.Unit, (int)unit.Key.GetEntry(), false);
                        movementRows.Add(movementRow);
                    }
                }

                if (row.Data.MovementType == 1 && row.Data.WanderDistance == 0 &&
                    Settings.TargetedDbType != TargetedDbType.WPP)
                    row.Data.WanderDistance = 10;

                var addonRow = new Row<CreatureAddon>();
                if (Settings.SqlTables.creature_addon)
                {
                    addonRow.Data.GUID = "@CGUID+" + creature.DbGuid;
                    addonRow.Data.PathID = row.Data.MovementType >= 2 ? "@CGUID+" + creature.DbGuid : "0";
                    addonRow.Data.Mount = (uint)unitData.MountDisplayID;
                    addonRow.Data.Bytes1 = creature.Bytes1;
                    addonRow.Data.Bytes2 = creature.Bytes2;
                    addonRow.Data.SheathState = unitData.SheatheState;
                    addonRow.Data.PvpFlags = unitData.PvpFlags;
                    addonRow.Data.Emote = (uint)unitData.EmoteState;
                    addonRow.Data.MoveFlags = (uint)creature.OriginalMovement.Flags;
                    addonRow.Data.Auras = creature.GetOriginalAurasString(true);
                    addonRow.Data.AIAnimKit = creature.AIAnimKit.GetValueOrDefault(0);
                    addonRow.Data.MovementAnimKit = creature.MovementAnimKit.GetValueOrDefault(0);
                    addonRow.Data.MeleeAnimKit = creature.MeleeAnimKit.GetValueOrDefault(0);
                    addonRow.Data.VisibilityDistanceType = creature.VisibilityDistanceType;
                    addonRow.Comment += StoreGetters.GetName(StoreNameType.Unit, (int)unit.Key.GetEntry(), false);
                    addonRows.Add(addonRow);
                }

                if (Settings.SqlTables.creature_movement_server_combat &&
                    creature.CombatMovements != null)
                {
                    if (creature.CombatMovementSplines != null)
                    {
                        foreach (ServerSideMovementSpline waypoint in creature.CombatMovementSplines)
                        {
                            var movementSplineRow = new Row<ServerSideMovementSpline>();
                            movementSplineRow.Data = waypoint;
                            movementSplineRow.Data.GUID = "@CGUID+" + creature.DbGuid;
                            movementSplineRow.Comment += StoreGetters.GetName(StoreNameType.Unit, (int)unit.Key.GetEntry(), false);
                            movementCombatSplineRows.Add(movementSplineRow);
                        }
                    }

                    foreach (ServerSideMovement waypoint in creature.CombatMovements)
                    {
                        if (waypoint == null)
                            break;

                        var movementCombatRow = new Row<ServerSideMovement>();
                        movementCombatRow.Data = waypoint;
                        movementCombatRow.Data.GUID = "@CGUID+" + creature.DbGuid;
                        if (waypoint.TransportGuid != null && !waypoint.TransportGuid.IsEmpty())
                            Storage.GetObjectDbGuidEntryType(waypoint.TransportGuid, out movementCombatRow.Data.TransportGUID, out movementCombatRow.Data.TransportId, out movementCombatRow.Data.TransportType);
                        movementCombatRow.Comment += StoreGetters.GetName(StoreNameType.Unit, (int)unit.Key.GetEntry(), false);
                        movementCombatRows.Add(movementCombatRow);
                    }
                }

                if (Settings.SqlTables.creature_emote)
                {
                    if (Storage.Emotes.ContainsKey(unit.Key))
                    {
                        foreach (var emote in Storage.Emotes[unit.Key])
                        {
                            var emoteRow = new Row<CreatureEmote>();
                            emoteRow.Data = emote;
                            emoteRow.Data.GUID = "@CGUID+" + creature.DbGuid;
                            emoteRows.Add(emoteRow);
                        }
                    }
                }

                if (Settings.SqlTables.creature_attack_log)
                {
                    if (Storage.UnitAttackLogs.ContainsKey(unit.Key))
                    {
                        foreach (var attack in Storage.UnitAttackLogs[unit.Key])
                        {
                            var attackLogRow = new Row<UnitMeleeAttackLog>();
                            attackLogRow.Data = attack;
                            attackLogRow.Data.GUID = "@CGUID+" + creature.DbGuid;
                            Storage.GetObjectDbGuidEntryType(attack.Victim, out attackLogRow.Data.VictimGuid, out attackLogRow.Data.VictimId, out attackLogRow.Data.VictimType);
                            attackLogRow.Data.UnixTimeMs = (ulong)Utilities.GetUnixTimeMsFromDateTime(attack.Time);
                            attackLogRows.Add(attackLogRow);
                        }
                    }
                }

                if (Settings.SqlTables.creature_attack_start)
                {
                    if (Storage.UnitAttackStartTimes.ContainsKey(unit.Key))
                    {
                        foreach (var attack in Storage.UnitAttackStartTimes[unit.Key])
                        {
                            var attackStartRow = new Row<CreatureAttackToggle>();

                            attackStartRow.Data.GUID = "@CGUID+" + creature.DbGuid;
                            Storage.GetObjectDbGuidEntryType(attack.victim, out attackStartRow.Data.VictimGuid, out attackStartRow.Data.VictimId, out attackStartRow.Data.VictimType);
                            attackStartRow.Data.UnixTimeMs = (ulong)Utilities.GetUnixTimeMsFromDateTime(attack.time);
                            attackStartRows.Add(attackStartRow);
                        }
                    }
                }

                if (Settings.SqlTables.creature_attack_stop)
                {
                    if (Storage.UnitAttackStopTimes.ContainsKey(unit.Key))
                    {
                        foreach (var attack in Storage.UnitAttackStopTimes[unit.Key])
                        {
                            var attackStopRow = new Row<CreatureAttackToggle>();

                            attackStopRow.Data.GUID = "@CGUID+" + creature.DbGuid;
                            Storage.GetObjectDbGuidEntryType(attack.victim, out attackStopRow.Data.VictimGuid, out attackStopRow.Data.VictimId, out attackStopRow.Data.VictimType);
                            attackStopRow.Data.UnixTimeMs = (ulong)Utilities.GetUnixTimeMsFromDateTime(attack.time);
                            attackStopRows.Add(attackStopRow);
                        }
                    }
                }

                if (Settings.SqlTables.creature_flight_spline_sync)
                {
                    if (Storage.CreatureFlightSplineSyncs.ContainsKey(unit.Key))
                    {
                        foreach (var update in Storage.CreatureFlightSplineSyncs[unit.Key])
                        {
                            Row<CreatureFlightSplineSync> updateRow = new Row<CreatureFlightSplineSync>();
                            updateRow.Data = update;
                            updateRow.Data.GUID = "@CGUID+" + creature.DbGuid;
                            flightSplineSyncRows.Add(updateRow);
                        }
                    }
                }

                if (Settings.SqlTables.creature_equipment_values_update)
                {
                    if (Storage.UnitEquipmentValuesUpdates.ContainsKey(unit.Key))
                    {
                        foreach (var update in Storage.UnitEquipmentValuesUpdates[unit.Key])
                        {
                            Row<CreatureEquipmentValuesUpdate> updateRow = new Row<CreatureEquipmentValuesUpdate>();
                            updateRow.Data = update;
                            updateRow.Data.GUID = "@CGUID+" + creature.DbGuid;
                            updateRow.Data.UnixTimeMs = (ulong)Utilities.GetUnixTimeMsFromDateTime(update.time);
                            updateEquipmentValuesRows.Add(updateRow);
                        }
                    }
                }

                if (Settings.SqlTables.creature_guid_values_update)
                {
                    if (Storage.UnitGuidValuesUpdates.ContainsKey(unit.Key))
                    {
                        foreach (var update in Storage.UnitGuidValuesUpdates[unit.Key])
                        {
                            Row<CreatureGuidValuesUpdate> updateRow = new Row<CreatureGuidValuesUpdate>();
                            updateRow.Data.GUID = "@CGUID+" + creature.DbGuid;
                            updateRow.Data.FieldName = update.FieldName;
                            Storage.GetObjectDbGuidEntryType(update.guid, out updateRow.Data.ObjectGuid, out updateRow.Data.ObjectId, out updateRow.Data.ObjectType);
                            updateRow.Data.UnixTimeMs = (ulong)Utilities.GetUnixTimeMsFromDateTime(update.time);
                            updateGuidValuesRows.Add(updateRow);
                        }
                    }
                }

                if (Settings.SqlTables.creature_pet_name)
                {
                    string petName = StoreGetters.GetName(unit.Key);
                    if (petName != null)
                    {
                        var petNameRow = new Row<CreaturePetName>();
                        petNameRow.Data.GUID = "@CGUID+" + creature.DbGuid;
                        petNameRow.Data.Name = petName;
                        petNameRows.Add(petNameRow);
                    }
                }

                if (creature.WasOriginallyOnTransport() && badTransport)
                {
                    row.CommentOut = true;
                    row.Comment += " - !!! on transport - transport template not found !!!";
                    if (Settings.SqlTables.creature_addon)
                    {
                        addonRow.CommentOut = true;
                        addonRow.Comment += " - !!! on transport - transport template not found !!!";
                    }
                }
                else
                {
                    ++count;

                    if (creature.DbGuid > maxDbGuid)
                        maxDbGuid = creature.DbGuid;
                }

                if (creature.Movement.HasWpsOrRandMov)
                    row.Comment += " (possible waypoints or random movement)";

                rows.Add(row);
            }

            if (count == 0)
                return string.Empty;

            StringBuilder result = new StringBuilder();

            if (rows.Count != 0)
            {
                // delete query for GUIDs
                var delete = new SQLDelete<Creature>(Tuple.Create("@CGUID+0", "@CGUID+" + maxDbGuid));
                result.Append(delete.Build());
                var sql = new SQLInsert<Creature>(rows, false);
                result.Append(sql.Build());
                result.AppendLine();
            }

            if (Settings.SqlTables.creature_addon && addonRows.Count != 0)
            {
                var addonDelete = new SQLDelete<CreatureAddon>(Tuple.Create("@CGUID+0", "@CGUID+" + maxDbGuid));
                result.Append(addonDelete.Build());
                var addonSql = new SQLInsert<CreatureAddon>(addonRows, false);
                result.Append(addonSql.Build());
                result.AppendLine();
            }

            if (Settings.SqlTables.creature_guid_values && guidValuesRows.Count != 0)
            {
                var guidValuesDelete = new SQLDelete<CreatureGuidValues>(Tuple.Create("@CGUID+0", "@CGUID+" + maxDbGuid));
                result.Append(guidValuesDelete.Build());
                var guidValuesSql = new SQLInsert<CreatureGuidValues>(guidValuesRows, false);
                result.Append(guidValuesSql.Build());
                result.AppendLine();
            }

            if (Settings.SqlTables.creature_power_values && powerValuesRows.Count != 0)
            {
                var powerValuesDelete = new SQLDelete<CreaturePowerValues>(Tuple.Create("@CGUID+0", "@CGUID+" + maxDbGuid));
                result.Append(powerValuesDelete.Build());
                var powerValuesSql = new SQLInsert<CreaturePowerValues>(powerValuesRows, false);
                result.Append(powerValuesSql.Build());
                result.AppendLine();
            }

            if (Settings.SqlTables.client_creature_interact && interactRows.Count != 0)
            {
                var interactSql = new SQLInsert<CreatureClientInteract>(interactRows, false);
                result.Append(interactSql.Build());
                result.AppendLine();
            }

            if (Settings.SqlTables.creature_create1_time && create1Rows.Count != 0)
            {
                var createSql = new SQLInsert<CreatureCreate1>(create1Rows, false);
                result.Append(createSql.Build());
                result.AppendLine();
            }

            if (Settings.SqlTables.creature_create2_time && create2Rows.Count != 0)
            {
                var createSql = new SQLInsert<CreatureCreate2>(create2Rows, false);
                result.Append(createSql.Build());
                result.AppendLine();
            }

            if (Settings.SqlTables.creature_destroy_time && destroyRows.Count != 0)
            {
                var destroySql = new SQLInsert<CreatureDestroy>(destroyRows, false);
                result.Append(destroySql.Build());
                result.AppendLine();
            }

            if (Settings.SqlTables.creature_auras_update && updateAurasRows.Count != 0)
            {
                var updateSql = new SQLInsert<CreatureAurasUpdate>(updateAurasRows, false);
                result.Append(updateSql.Build());
                result.AppendLine();
            }

            if (Settings.SqlTables.creature_threat_clear && threatClearRows.Count != 0)
            {
                var updateSql = new SQLInsert<CreatureThreatClear>(threatClearRows, false);
                result.Append(updateSql.Build());
                result.AppendLine();
            }

            if (Settings.SqlTables.creature_threat_remove && threatRemoveRows.Count != 0)
            {
                var updateSql = new SQLInsert<CreatureThreatRemove>(threatRemoveRows, false);
                result.Append(updateSql.Build());
                result.AppendLine();
            }

            if (Settings.SqlTables.creature_threat_update && updateThreatRows.Count != 0)
            {
                var updateSql = new SQLInsert<CreatureThreatUpdate>(updateThreatRows, false);
                result.Append(updateSql.Build());
                result.AppendLine();

                var targetSql = new SQLInsert<CreatureThreatUpdateTarget>(updateThreatTargetRows, false);
                result.Append(targetSql.Build());
                result.AppendLine();
            }

            if (Settings.SqlTables.creature_values_update && updateValuesRows.Count != 0)
            {
                var updateSql = new SQLInsert<CreatureValuesUpdate>(updateValuesRows, false);
                result.Append(updateSql.Build());
                result.AppendLine();
            }

            if (Settings.SqlTables.creature_guid_values_update && updateGuidValuesRows.Count != 0)
            {
                var updateSql = new SQLInsert<CreatureGuidValuesUpdate>(updateGuidValuesRows, false, false);
                result.Append(updateSql.Build());
                result.AppendLine();
            }

            if (Settings.SqlTables.creature_power_values_update && updatePowerValuesRows.Count != 0)
            {
                var updateSql = new SQLInsert<CreaturePowerValuesUpdate>(updatePowerValuesRows, false, false);
                result.Append(updateSql.Build());
                result.AppendLine();
            }

            if (Settings.SqlTables.creature_speed_update && updateSpeedRows.Count != 0)
            {
                var updateSql = new SQLInsert<CreatureSpeedUpdate>(updateSpeedRows, false);
                result.Append(updateSql.Build());
                result.AppendLine();
            }

            if (Settings.SqlTables.creature_movement_client)
            {
                uint moveCounter = 0;
                var movementClientRows = new RowList<ClientSideMovement>();
                foreach (var movement in Storage.PlayerMovements)
                {
                    if (movement.Guid.GetObjectType() != ObjectType.Unit)
                        continue;

                    WoWObject obj;
                    if (!Storage.Objects.TryGetValue(movement.Guid, out obj))
                        continue;

                    Unit creature = obj as Unit;
                    if (creature == null)
                        continue;

                    Row<ClientSideMovement> clientMovementRow = new Row<ClientSideMovement>();
                    clientMovementRow.Data.Guid = "@CGUID+" + creature.DbGuid;
                    clientMovementRow.Data.MoveFlags = movement.MoveInfo.Flags;
                    clientMovementRow.Data.MoveFlags2 = movement.MoveInfo.Flags2;
                    clientMovementRow.Data.MoveTime = movement.MoveInfo.MoveTime;
                    clientMovementRow.Data.Map = movement.Map;
                    clientMovementRow.Data.PositionX = movement.MoveInfo.Position.X;
                    clientMovementRow.Data.PositionY = movement.MoveInfo.Position.Y;
                    clientMovementRow.Data.PositionZ = movement.MoveInfo.Position.Z;
                    clientMovementRow.Data.Orientation = movement.MoveInfo.Orientation;
                    if (movement.MoveInfo.TransportGuid != null && !movement.MoveInfo.TransportGuid.IsEmpty())
                    {
                        Storage.GetObjectDbGuidEntryType(movement.MoveInfo.TransportGuid, out clientMovementRow.Data.TransportGuid, out clientMovementRow.Data.TransportId, out clientMovementRow.Data.TransportType);
                        clientMovementRow.Data.TransportPositionX = movement.MoveInfo.TransportOffset.X;
                        clientMovementRow.Data.TransportPositionY = movement.MoveInfo.TransportOffset.Y;
                        clientMovementRow.Data.TransportPositionZ = movement.MoveInfo.TransportOffset.Z;
                        clientMovementRow.Data.TransportOrientation = movement.MoveInfo.TransportOffset.O;
                        clientMovementRow.Data.TransportTime = movement.MoveInfo.TransportTime;
                        clientMovementRow.Data.TransportSeat = movement.MoveInfo.TransportSeat;
                    }
                    clientMovementRow.Data.SwimPitch = movement.MoveInfo.SwimPitch;
                    clientMovementRow.Data.FallTime = movement.MoveInfo.FallTime;
                    clientMovementRow.Data.JumpHorizontalSpeed = movement.MoveInfo.JumpHorizontalSpeed;
                    clientMovementRow.Data.JumpVerticalSpeed = movement.MoveInfo.JumpVerticalSpeed;
                    clientMovementRow.Data.JumpCosAngle = movement.MoveInfo.JumpCosAngle;
                    clientMovementRow.Data.JumpSinAngle = movement.MoveInfo.JumpSinAngle;
                    clientMovementRow.Data.SplineElevation = movement.MoveInfo.SplineElevation;
                    clientMovementRow.Data.PacketId = moveCounter++;
                    clientMovementRow.Data.Opcode = Opcodes.GetOpcodeName(movement.Opcode, movement.OpcodeDirection);
                    clientMovementRow.Data.UnixTimeMs = (ulong)Utilities.GetUnixTimeMsFromDateTime(movement.Time);
                    movementClientRows.Add(clientMovementRow);
                }

                if (movementClientRows.Count != 0)
                {
                    var clientMovementSql = new SQLInsert<ClientSideMovement>(movementClientRows, false, false, "creature_movement_client");
                    result.Append(clientMovementSql.Build());
                    result.AppendLine();
                }
            }

            if (Settings.SqlTables.creature_movement_server && movementRows.Count != 0)
            {
                // creature_movement_server
                var movementSql = new SQLInsert<ServerSideMovement>(movementRows, false);
                result.Append(movementSql.Build());
                result.AppendLine();

                if (Settings.TargetedDbType == TargetedDbType.WPP)
                {
                    // creature_movement_server_spline
                    var movementSplineSql = new SQLInsert<ServerSideMovementSpline>(movementSplineRows, false);
                    result.Append(movementSplineSql.Build());
                    result.AppendLine();
                }
            }

            if (Settings.SqlTables.creature_movement_server_combat && movementCombatRows.Count != 0)
            {
                // creature_movement_server_combat
                var movementSql = new SQLInsert<ServerSideMovement>(movementCombatRows, false, false, "creature_movement_server_combat");
                result.Append(movementSql.Build());
                result.AppendLine();

                if (Settings.TargetedDbType == TargetedDbType.WPP)
                {
                    // creature_movement_server_combat_spline
                    var movementSplineSql = new SQLInsert<ServerSideMovementSpline>(movementCombatSplineRows, false, false, "creature_movement_server_combat_spline");
                    result.Append(movementSplineSql.Build());
                    result.AppendLine();
                }
            }

            if (Settings.SqlTables.creature_pet_name && petNameRows.Count != 0)
            {
                var petNameSql = new SQLInsert<CreaturePetName>(petNameRows, false);
                result.Append(petNameSql.Build());
                result.AppendLine();
            }

            if (Settings.SqlTables.creature_emote && emoteRows.Count != 0)
            {
                var emoteSql = new SQLInsert<CreatureEmote>(emoteRows, false);
                result.Append(emoteSql.Build());
                result.AppendLine();
            }

            if (Settings.SqlTables.creature_attack_log && attackLogRows.Count != 0)
            {
                var attackSql = new SQLInsert<UnitMeleeAttackLog>(attackLogRows, false, false, "creature_attack_log");
                result.Append(attackSql.Build());
                result.AppendLine();
            }

            if (Settings.SqlTables.creature_attack_start && attackStartRows.Count != 0)
            {
                var attackSql = new SQLInsert<CreatureAttackToggle>(attackStartRows, false, false, "creature_attack_start");
                result.Append(attackSql.Build());
                result.AppendLine();
            }

            if (Settings.SqlTables.creature_attack_stop && attackStopRows.Count != 0)
            {
                var attackSql = new SQLInsert<CreatureAttackToggle>(attackStopRows, false, false, "creature_attack_stop");
                result.Append(attackSql.Build());
                result.AppendLine();
            }

            if (Settings.SqlTables.creature_equipment_values_update && updateEquipmentValuesRows.Count != 0)
            {
                var updateSql = new SQLInsert<CreatureEquipmentValuesUpdate>(updateEquipmentValuesRows, false, false);
                result.Append(updateSql.Build());
                result.AppendLine();
            }

            if (Settings.SqlTables.creature_flight_spline_sync && flightSplineSyncRows.Count != 0)
            {
                var updateSql = new SQLInsert<CreatureFlightSplineSync>(flightSplineSyncRows, false, false);
                result.Append(updateSql.Build());
                result.AppendLine();
            }

            return result.ToString();
        }

        [BuilderMethod(Gameobjects = true)]
        public static string GameObject(Dictionary<WowGuid, GameObject> gameObjects)
        {
            if (gameObjects.Count == 0)
                return string.Empty;

            if (!Settings.SqlTables.gameobject)
                return string.Empty;

            uint count = 0;
            uint maxDbGuid = 0;
            var rows = new RowList<GameObjectSpawn>();
            var addonRows = new RowList<GameObjectAddon>();
            var create1Rows = new RowList<GameObjectCreate1>();
            var create2Rows = new RowList<GameObjectCreate2>();
            var customAnimRows = new RowList<GameObjectCustomAnim>();
            var despawnAnimRows = new RowList<GameObjectDespawnAnim>();
            var destroyRows = new RowList<GameObjectDestroy>();
            var updateRows = new RowList<GameObjectUpdate>();
            var useRows = new RowList<GameObjectClientUse>();
            foreach (var gameobject in gameObjects)
            {
                Row<GameObjectSpawn> row = new Row<GameObjectSpawn>();

                GameObject go = gameobject.Value;

                if (Settings.AreaFilters.Length > 0)
                    if (!(go.Area.ToString(CultureInfo.InvariantCulture).MatchesFilters(Settings.AreaFilters)))
                        continue;

                if (Settings.MapFilters.Length > 0)
                    if (!(go.Map.ToString(CultureInfo.InvariantCulture).MatchesFilters(Settings.MapFilters)))
                        continue;

                uint entry = (uint)go.ObjectDataOriginal.EntryID;
                if (entry == 0)
                    continue;   // broken entry, nothing to spawn

                if (go.IsTemporarySpawn() && !Settings.SaveTempSpawns)
                    continue;

                if (go.IsTransport() && !Settings.SaveTransports)
                    continue;

                bool badTransport = false;

                row.Data.GUID = "@OGUID+" + go.DbGuid;
                row.Data.OriginalID = gameobject.Key.GetEntry();
                row.Data.ID = entry;
                row.Data.TotalObservationTime = go.TotalObservedTime;
                row.Data.LongestObservationTime = go.LongestObservedTime;

                if (!go.WasOriginallyOnTransport() || Settings.SaveTransports)
                    row.Data.Map = go.Map;
                else
                {
                    int mapId;
                    badTransport = !GetTransportMap(go, out mapId);
                    if (mapId != -1)
                        row.Data.Map = (uint)mapId;
                }

                if (go.WasOriginallyOnTransport())
                    row.Data.IsOnTransport = 1;

                row.Data.ZoneID = 0;
                row.Data.AreaID = 0;

                if (go.Area != -1)
                    row.Data.AreaID = (uint)go.Area;

                if (go.Zone != -1)
                    row.Data.ZoneID = (uint)go.Zone;

                row.Data.SpawnMask = (uint)go.GetDefaultSpawnMask();

                if (ClientVersion.AddedInVersion(ClientVersionBuild.V7_0_3_22248))
                {
                    string data = string.Join(",", go.GetDefaultSpawnDifficulties());
                    if (string.IsNullOrEmpty(data))
                        data = "0";

                    row.Data.SpawnDifficulties = data;
                }

                row.Data.PhaseMask = go.PhaseMask;

                if (ClientVersion.AddedInVersion(ClientVersionBuild.V4_3_4_15595) && go.Phases != null)
                {
                    string data = string.Join(" - ", go.Phases);
                    if (string.IsNullOrEmpty(data) || Settings.ForcePhaseZero)
                        data = "0";

                    row.Data.PhaseID = data;
                }

                if (!go.WasOriginallyOnTransport() || Settings.SaveTransports)
                {
                    row.Data.PositionX = go.OriginalMovement.Position.X;
                    row.Data.PositionY = go.OriginalMovement.Position.Y;
                    row.Data.PositionZ = go.OriginalMovement.Position.Z;
                    row.Data.Orientation = go.OriginalMovement.Orientation;
                }
                else
                {
                    row.Data.PositionX = go.OriginalMovement.TransportOffset.X;
                    row.Data.PositionY = go.OriginalMovement.TransportOffset.Y;
                    row.Data.PositionZ = go.OriginalMovement.TransportOffset.Z;
                    row.Data.Orientation = go.OriginalMovement.TransportOffset.O;
                }

                var rotation = go.GetStaticRotation();
                row.Data.Rotation = new float?[] { rotation.X, rotation.Y, rotation.Z, rotation.W };

                bool add = false;
                var addonRow = new Row<GameObjectAddon>();
                if (Settings.SqlTables.gameobject_addon)
                {
                    addonRow.Data.GUID = "@OGUID+" + go.DbGuid;

                    var parentRotation = go.GetParentRotation();

                    if (parentRotation != null)
                    {
                        addonRow.Data.parentRot0 = parentRotation.Value.X;
                        addonRow.Data.parentRot1 = parentRotation.Value.Y;
                        addonRow.Data.parentRot2 = parentRotation.Value.Z;
                        addonRow.Data.parentRot3 = parentRotation.Value.W;

                        if (addonRow.Data.parentRot0 != 0.0f ||
                            addonRow.Data.parentRot1 != 0.0f ||
                            addonRow.Data.parentRot2 != 0.0f ||
                            addonRow.Data.parentRot3 != 1.0f)
                            add = true;
                    }

                    addonRow.Data.WorldEffectID = go.WorldEffectID.GetValueOrDefault(0);
                    addonRow.Data.AIAnimKitID = go.AIAnimKitID.GetValueOrDefault(0);

                    if (go.WorldEffectID != null || go.AIAnimKitID != null)
                        add = true;

                    addonRow.Comment += StoreGetters.GetName(StoreNameType.GameObject, (int)gameobject.Key.GetEntry(), false);

                    if (add)
                        addonRows.Add(addonRow);
                }

                if (Settings.SqlTables.gameobject_create1_time)
                {
                    if (Storage.ObjectCreate1Times.ContainsKey(gameobject.Key))
                    {
                        foreach (var createTime in Storage.ObjectCreate1Times[gameobject.Key])
                        {
                            var create1Row = new Row<GameObjectCreate1>();
                            create1Row.Data.GUID = "@OGUID+" + go.DbGuid;
                            create1Row.Data.Map = createTime.Map;
                            create1Row.Data.PositionX = createTime.MoveInfo.Position.X;
                            create1Row.Data.PositionY = createTime.MoveInfo.Position.Y;
                            create1Row.Data.PositionZ = createTime.MoveInfo.Position.Z;
                            create1Row.Data.Orientation = createTime.MoveInfo.Orientation;
                            if (createTime.MoveInfo.TransportGuid != null && !createTime.MoveInfo.TransportGuid.IsEmpty())
                            {
                                create1Row.Data.TransportGuid = Storage.GetObjectDbGuid(createTime.MoveInfo.TransportGuid);
                                create1Row.Data.TransportPositionX = createTime.MoveInfo.TransportOffset.X;
                                create1Row.Data.TransportPositionY = createTime.MoveInfo.TransportOffset.Y;
                                create1Row.Data.TransportPositionZ = createTime.MoveInfo.TransportOffset.Z;
                                create1Row.Data.TransportOrientation = createTime.MoveInfo.TransportOffset.O;
                            }
                            create1Row.Data.TransportPathTimer = createTime.MoveInfo.TransportPathTimer;
                            create1Row.Data.UnixTimeMs = createTime.UnixTimeMs;
                            create1Rows.Add(create1Row);
                        }
                    }
                }

                if (Settings.SqlTables.gameobject_create2_time)
                {
                    if (Storage.ObjectCreate2Times.ContainsKey(gameobject.Key))
                    {
                        foreach (var createTime in Storage.ObjectCreate2Times[gameobject.Key])
                        {
                            var create2Row = new Row<GameObjectCreate2>();
                            create2Row.Data.GUID = "@OGUID+" + go.DbGuid;
                            create2Row.Data.Map = createTime.Map;
                            create2Row.Data.PositionX = createTime.MoveInfo.Position.X;
                            create2Row.Data.PositionY = createTime.MoveInfo.Position.Y;
                            create2Row.Data.PositionZ = createTime.MoveInfo.Position.Z;
                            create2Row.Data.Orientation = createTime.MoveInfo.Orientation;
                            if (createTime.MoveInfo.TransportGuid != null && !createTime.MoveInfo.TransportGuid.IsEmpty())
                            {
                                create2Row.Data.TransportGuid = Storage.GetObjectDbGuid(createTime.MoveInfo.TransportGuid);
                                create2Row.Data.TransportPositionX = createTime.MoveInfo.TransportOffset.X;
                                create2Row.Data.TransportPositionY = createTime.MoveInfo.TransportOffset.Y;
                                create2Row.Data.TransportPositionZ = createTime.MoveInfo.TransportOffset.Z;
                                create2Row.Data.TransportOrientation = createTime.MoveInfo.TransportOffset.O;
                            }
                            create2Row.Data.TransportPathTimer = createTime.MoveInfo.TransportPathTimer;
                            create2Row.Data.UnixTimeMs = createTime.UnixTimeMs;
                            create2Rows.Add(create2Row);
                        }
                    }
                }

                if (Settings.SqlTables.gameobject_custom_anim)
                {
                    if (Storage.GameObjectCustomAnims.ContainsKey(gameobject.Key))
                    {
                        foreach (var animTime in Storage.GameObjectCustomAnims[gameobject.Key])
                        {
                            var customAnimRow = new Row<GameObjectCustomAnim>();
                            customAnimRow.Data = animTime;
                            customAnimRow.Data.GUID = "@OGUID+" + go.DbGuid;
                            customAnimRows.Add(customAnimRow);
                        }
                    }
                }

                if (Settings.SqlTables.gameobject_despawn_anim)
                {
                    if (Storage.GameObjectDespawnAnims.ContainsKey(gameobject.Key))
                    {
                        foreach (var animTime in Storage.GameObjectDespawnAnims[gameobject.Key])
                        {
                            var despawnAnimRow = new Row<GameObjectDespawnAnim>();
                            despawnAnimRow.Data.GUID = "@OGUID+" + go.DbGuid;
                            despawnAnimRow.Data.UnixTimeMs = (ulong)Utilities.GetUnixTimeMsFromDateTime(animTime);
                            despawnAnimRows.Add(despawnAnimRow);
                        }
                    }
                }

                if (Settings.SqlTables.gameobject_destroy_time)
                {
                    if (Storage.ObjectDestroyTimes.ContainsKey(gameobject.Key))
                    {
                        foreach (var destroyTime in Storage.ObjectDestroyTimes[gameobject.Key])
                        {
                            var destroyRow = new Row<GameObjectDestroy>();
                            destroyRow.Data.GUID = "@OGUID+" + go.DbGuid;
                            destroyRow.Data.UnixTimeMs = (ulong)Utilities.GetUnixTimeMsFromDateTime(destroyTime);
                            destroyRows.Add(destroyRow);
                        }
                    }
                }

                if (Settings.SqlTables.gameobject_values_update)
                {
                    if (Storage.GameObjectUpdates.ContainsKey(gameobject.Key))
                    {
                        foreach (var update in Storage.GameObjectUpdates[gameobject.Key])
                        {
                            var updateRow = new Row<GameObjectUpdate>();
                            updateRow.Data = update;
                            updateRow.Data.GUID = "@OGUID+" + go.DbGuid;
                            updateRows.Add(updateRow);
                        }
                    }
                }

                if (Settings.SqlTables.client_gameobject_use)
                {
                    if (Storage.GameObjectClientUseTimes.ContainsKey(gameobject.Key))
                    {
                        foreach (var useTime in Storage.GameObjectClientUseTimes[gameobject.Key])
                        {
                            var useRow = new Row<GameObjectClientUse>();
                            useRow.Data.GUID = "@OGUID+" + go.DbGuid;
                            useRow.Data.UnixTimeMs = (ulong)Utilities.GetUnixTimeMsFromDateTime(useTime);
                            useRows.Add(useRow);
                        }
                    }
                }
                Storage.GetObjectDbGuidEntryType(go.GameObjectDataOriginal.CreatedBy, out row.Data.CreatedByGuid, out row.Data.CreatedById, out row.Data.CreatedByType);
                //row.Data.SpawnTimeSecs = go.GetDefaultSpawnTime(go.DifficultyID);
                row.Data.DisplayID = (uint)go.GameObjectDataOriginal.DisplayID;
                row.Data.Level = (uint)go.GameObjectDataOriginal.Level;
                row.Data.Faction = (uint)go.GameObjectDataOriginal.FactionTemplate;
                row.Data.Flags = go.GameObjectDataOriginal.Flags;
                row.Data.DynamicFlags = go.GetDynamicFlagsOriginal();
                row.Data.PathProgress = go.GetPathProgressOriginal();
                row.Data.State = (uint)go.GameObjectDataOriginal.State;
                row.Data.Type = (uint)go.GameObjectDataOriginal.TypeID;
                row.Data.ArtKit = go.GameObjectDataOriginal.ArtKit;
                row.Data.AnimProgress = go.GameObjectDataOriginal.AnimProgress;
                row.Data.CustomParam = go.GameObjectDataOriginal.CustomParam;
                row.Data.SniffId = "@SNIFFID+" + go.SourceSniffId;
                row.Data.SniffBuild = go.SourceSniffBuild;

                // set some defaults
                row.Data.PhaseGroup = 0;
                row.Data.IsSpawn = go.FirstCreateType;
                row.Data.TemporarySpawn = (byte)(go.IsTemporarySpawn() ? 1 : 0);
                row.Data.IsTransport = (byte)((gameobject.Key.GetHighType() == HighGuidType.Transport) ? 1 : 0);

                if (go.WasOriginallyOnTransport() && badTransport)
                {
                    row.CommentOut = true;
                    row.Comment += " - !!! on transport - transport template not found !!!";
                    if (Settings.SqlTables.gameobject_addon)
                    {
                        addonRow.CommentOut = true;
                        addonRow.Comment += " - !!! on transport - transport template not found !!!";
                    }
                }
                else
                {
                    ++count;

                    if (go.DbGuid > maxDbGuid)
                        maxDbGuid = go.DbGuid;
                }

                rows.Add(row);
            }

            if (count == 0)
                return String.Empty;

            StringBuilder result = new StringBuilder();

            if (rows.Count != 0)
            {
                // delete query for GUIDs
                var delete = new SQLDelete<GameObjectSpawn>(Tuple.Create("@OGUID+0", "@OGUID+" + maxDbGuid));
                result.Append(delete.Build());
                var sql = new SQLInsert<GameObjectSpawn>(rows, false);
                result.Append(sql.Build());
                result.AppendLine();
            }

            if (Settings.SqlTables.gameobject_addon && addonRows.Count != 0)
            {
                var addonDelete = new SQLDelete<GameObjectAddon>(Tuple.Create("@OGUID+0", "@OGUID+" + maxDbGuid));
                result.Append(addonDelete.Build());
                var addonSql = new SQLInsert<GameObjectAddon>(addonRows, false);
                result.Append(addonSql.Build());
                result.AppendLine();
            }

            if (Settings.SqlTables.gameobject_create1_time && create1Rows.Count != 0)
            {
                var createSql = new SQLInsert<GameObjectCreate1>(create1Rows, false);
                result.Append(createSql.Build());
                result.AppendLine();
            }

            if (Settings.SqlTables.gameobject_create2_time && create2Rows.Count != 0)
            {
                var createSql = new SQLInsert<GameObjectCreate2>(create2Rows, false);
                result.Append(createSql.Build());
                result.AppendLine();
            }

            if (Settings.SqlTables.gameobject_custom_anim && customAnimRows.Count != 0)
            {
                var animSql = new SQLInsert<GameObjectCustomAnim>(customAnimRows, false);
                result.Append(animSql.Build());
                result.AppendLine();
            }

            if (Settings.SqlTables.gameobject_despawn_anim && despawnAnimRows.Count != 0)
            {
                var animSql = new SQLInsert<GameObjectDespawnAnim>(despawnAnimRows, false);
                result.Append(animSql.Build());
                result.AppendLine();
            }

            if (Settings.SqlTables.gameobject_destroy_time && destroyRows.Count != 0)
            {
                var destroySql = new SQLInsert<GameObjectDestroy>(destroyRows, false);
                result.Append(destroySql.Build());
                result.AppendLine();
            }

            if (Settings.SqlTables.gameobject_values_update && updateRows.Count != 0)
            {
                var updateSql = new SQLInsert<GameObjectUpdate>(updateRows, false);
                result.Append(updateSql.Build());
                result.AppendLine();
            }

            if (Settings.SqlTables.client_gameobject_use && useRows.Count != 0)
            {
                var useSql = new SQLInsert<GameObjectClientUse>(useRows, false);
                result.Append(useSql.Build());
                result.AppendLine();
            }

            return result.ToString();
        }

        [BuilderMethod()]
        public static string DynamicObject()
        {
            if (!Settings.SqlTables.dynamicobject)
                return string.Empty;

            uint maxDbGuid = 0;
            var rows = new RowList<DynamicObjectSpawn>();
            var create1Rows = new RowList<DynamicObjectCreate1>();
            var create2Rows = new RowList<DynamicObjectCreate2>();
            var destroyRows = new RowList<DynamicObjectDestroy>();

            foreach (var wowObject in Storage.Objects)
            {
                if (wowObject.Value.Item1.Type != ObjectType.DynamicObject)
                    continue;

                DynamicObject dynObject = wowObject.Value.Item1 as DynamicObject;
                if (dynObject == null)
                    continue;

                Row<DynamicObjectSpawn> row = new Row<DynamicObjectSpawn>();
                row.Data.GUID = "@DGUID+" + dynObject.DbGuid;

                row.Data.Map = dynObject.Map;
                row.Data.PositionX = dynObject.OriginalMovement.Position.X;
                row.Data.PositionY = dynObject.OriginalMovement.Position.Y;
                row.Data.PositionZ = dynObject.OriginalMovement.Position.Z;
                row.Data.Orientation = dynObject.OriginalMovement.Orientation;

                Storage.GetObjectDbGuidEntryType(dynObject.DynamicObjectDataOriginal.Caster, out row.Data.CasterGuid, out row.Data.CasterId, out row.Data.CasterType);
                row.Data.SpellId = (uint)dynObject.DynamicObjectDataOriginal.SpellID;
                row.Data.VisualId = (uint)dynObject.DynamicObjectDataOriginal.SpellXSpellVisualID;
                row.Data.Radius = dynObject.DynamicObjectDataOriginal.Radius;
                row.Data.Type = dynObject.DynamicObjectDataOriginal.Type;
                row.Data.CastTime = dynObject.DynamicObjectDataOriginal.CastTime;

                rows.Add(row);

                if (Settings.SqlTables.dynamicobject_create1_time)
                {
                    if (Storage.ObjectCreate1Times.ContainsKey(wowObject.Key))
                    {
                        foreach (var createTime in Storage.ObjectCreate1Times[wowObject.Key])
                        {
                            var create1Row = new Row<DynamicObjectCreate1>();
                            create1Row.Data.GUID = "@DGUID+" + dynObject.DbGuid;
                            create1Row.Data.Map = createTime.Map;
                            create1Row.Data.PositionX = createTime.MoveInfo.Position.X;
                            create1Row.Data.PositionY = createTime.MoveInfo.Position.Y;
                            create1Row.Data.PositionZ = createTime.MoveInfo.Position.Z;
                            create1Row.Data.Orientation = createTime.MoveInfo.Orientation;
                            if (createTime.MoveInfo.TransportGuid != null && !createTime.MoveInfo.TransportGuid.IsEmpty())
                            {
                                create1Row.Data.TransportGuid = Storage.GetObjectDbGuid(createTime.MoveInfo.TransportGuid);
                                create1Row.Data.TransportPositionX = createTime.MoveInfo.TransportOffset.X;
                                create1Row.Data.TransportPositionY = createTime.MoveInfo.TransportOffset.Y;
                                create1Row.Data.TransportPositionZ = createTime.MoveInfo.TransportOffset.Z;
                                create1Row.Data.TransportOrientation = createTime.MoveInfo.TransportOffset.O;
                            }
                            create1Row.Data.UnixTimeMs = createTime.UnixTimeMs;
                            create1Rows.Add(create1Row);
                        }
                    }
                }

                if (Settings.SqlTables.dynamicobject_create2_time)
                {
                    if (Storage.ObjectCreate2Times.ContainsKey(wowObject.Key))
                    {
                        foreach (var createTime in Storage.ObjectCreate2Times[wowObject.Key])
                        {
                            var create2Row = new Row<DynamicObjectCreate2>();
                            create2Row.Data.GUID = "@DGUID+" + dynObject.DbGuid;
                            create2Row.Data.Map = createTime.Map;
                            create2Row.Data.PositionX = createTime.MoveInfo.Position.X;
                            create2Row.Data.PositionY = createTime.MoveInfo.Position.Y;
                            create2Row.Data.PositionZ = createTime.MoveInfo.Position.Z;
                            create2Row.Data.Orientation = createTime.MoveInfo.Orientation;
                            if (createTime.MoveInfo.TransportGuid != null && !createTime.MoveInfo.TransportGuid.IsEmpty())
                            {
                                create2Row.Data.TransportGuid = Storage.GetObjectDbGuid(createTime.MoveInfo.TransportGuid);
                                create2Row.Data.TransportPositionX = createTime.MoveInfo.TransportOffset.X;
                                create2Row.Data.TransportPositionY = createTime.MoveInfo.TransportOffset.Y;
                                create2Row.Data.TransportPositionZ = createTime.MoveInfo.TransportOffset.Z;
                                create2Row.Data.TransportOrientation = createTime.MoveInfo.TransportOffset.O;
                            }
                            create2Row.Data.UnixTimeMs = createTime.UnixTimeMs;
                            create2Rows.Add(create2Row);
                        }
                    }
                }

                if (Settings.SqlTables.dynamicobject_destroy_time)
                {
                    if (Storage.ObjectDestroyTimes.ContainsKey(wowObject.Key))
                    {
                        foreach (var destroyTime in Storage.ObjectDestroyTimes[wowObject.Key])
                        {
                            var destroyRow = new Row<DynamicObjectDestroy>();
                            destroyRow.Data.GUID = "@DGUID+" + dynObject.DbGuid;
                            destroyRow.Data.UnixTimeMs = (ulong)Utilities.GetUnixTimeMsFromDateTime(destroyTime);
                            destroyRows.Add(destroyRow);
                        }
                    }
                }

                if (maxDbGuid < dynObject.DbGuid)
                    maxDbGuid = dynObject.DbGuid;
            }

            StringBuilder result = new StringBuilder();

            if (rows.Count != 0)
            {
                // delete query for GUIDs
                var delete = new SQLDelete<DynamicObjectSpawn>(Tuple.Create("@DGUID+0", "@DGUID+" + maxDbGuid));
                result.Append(delete.Build());
                var sql = new SQLInsert<DynamicObjectSpawn>(rows, false);
                result.Append(sql.Build());
                result.AppendLine();
            }

            if (Settings.SqlTables.dynamicobject_create1_time && create1Rows.Count != 0)
            {
                var createSql = new SQLInsert<DynamicObjectCreate1>(create1Rows, false);
                result.Append(createSql.Build());
                result.AppendLine();
            }

            if (Settings.SqlTables.dynamicobject_create2_time && create2Rows.Count != 0)
            {
                var createSql = new SQLInsert<DynamicObjectCreate2>(create2Rows, false);
                result.Append(createSql.Build());
                result.AppendLine();
            }

            if (Settings.SqlTables.dynamicobject_destroy_time && destroyRows.Count != 0)
            {
                var destroySql = new SQLInsert<DynamicObjectDestroy>(destroyRows, false);
                result.Append(destroySql.Build());
                result.AppendLine();
            }

            return result.ToString();
        }
    }
}

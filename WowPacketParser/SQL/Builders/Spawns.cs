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
            var rows = new RowList<Creature>();
            var guidValuesRows = new RowList<CreatureGuidValues>();
            var addonRows = new RowList<CreatureAddon>();
            var interactRows = new RowList<CreatureClientInteract>();
            var create1Rows = new RowList<CreatureCreate1>();
            var create2Rows = new RowList<CreatureCreate2>();
            var destroyRows = new RowList<CreatureDestroy>();
            var movementRows = new RowList<ServerSideMovement>();
            var movementCombatRows = new RowList<ServerSideMovement>();
            var movementCombatSplineRows = new RowList<ServerSideMovementSpline>();
            var movementSplineRows = new RowList<ServerSideMovementSpline>();
            var movementClientRows = new RowList<ClientSideMovement>();
            var updateAurasRows = new RowList<CreatureAurasUpdate>();
            var updateValuesRows = new RowList<CreatureValuesUpdate>();
            var updateSpeedRows = new RowList<CreatureSpeedUpdate>();
            var attackLogRows = new RowList<UnitMeleeAttackLog>();
            var attackStartRows = new RowList<CreatureAttackToggle>();
            var attackStopRows = new RowList<CreatureAttackToggle>();
            var updateEquipmentValuesRows = new RowList<CreatureEquipmentValuesUpdate>();
            var updateGuidValuesRows = new RowList<CreatureGuidValuesUpdate>();
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

                uint entry = (uint)creature.ObjectData.EntryID;
                if (entry == 0)
                    continue;   // broken entry, nothing to spawn

                if (creature.IsTemporarySpawn() && !Settings.SaveTempSpawns)
                    continue;

                if ((unit.Key.GetHighType() == HighGuidType.Pet) && !Settings.SavePets)
                    continue;

                uint movementType = 0;
                uint spawnDist = 0;

                if (creature.Movement.HasWpsOrRandMov)
                {
                    movementType = 1;
                    spawnDist = 10;
                }

                Row<Creature> row = new Row<Creature>();
                row.Data.GUID = "@CGUID+" + creature.DbGuid;
                row.Data.ID = entry;

                bool badTransport = false;
                if (!creature.IsOnTransport())
                    row.Data.Map = creature.Map;
                else
                {
                    int mapId;
                    badTransport = !GetTransportMap(creature, out mapId);
                    if (mapId != -1)
                        row.Data.Map = (uint)mapId;
                }

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
                    if (string.IsNullOrEmpty(data))
                        data = "0";

                    row.Data.PhaseID = data;
                }

                if (!creature.WasOriginallyOnTransport())
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
                row.Data.WanderDistance = spawnDist;
                row.Data.MovementType = movementType;

                // set some defaults
                Store.Objects.UpdateFields.IUnitData unitData = creature.UnitDataOriginal != null ? creature.UnitDataOriginal : creature.UnitData;
                row.Data.PhaseGroup = 0;
                row.Data.Hover = (byte)(creature.OriginalMovement.Hover ? 1 : 0);
                row.Data.TemporarySpawn = (byte)(creature.IsTemporarySpawn() ? 1 : 0);
                row.Data.IsPet = (byte)((unit.Key.GetHighType() == HighGuidType.Pet) ? 1 : 0);
                row.Data.SummonSpell = (uint)unitData.CreatedBySpell;
                row.Data.Scale = creature.ObjectDataOriginal.Scale;
                row.Data.DisplayID = (uint)unitData.DisplayID;
                row.Data.NativeDisplayID = (uint)unitData.NativeDisplayID;
                row.Data.MountDisplayID = (uint)unitData.MountDisplayID;
                row.Data.FactionTemplate = (uint)unitData.FactionTemplate;
                row.Data.Level = (uint)unitData.Level;
                row.Data.NpcFlag = unitData.NpcFlags[0];
                row.Data.UnitFlag = unitData.Flags;
                row.Data.UnitFlag2 = unitData.Flags2;
                row.Data.CurHealth = (uint)unitData.CurHealth;
                row.Data.CurMana = (uint)unitData.CurMana;
                row.Data.MaxHealth = (uint)unitData.MaxHealth;
                row.Data.MaxMana = (uint)unitData.MaxMana;
                row.Data.AuraState = unitData.AuraState;
                row.Data.EmoteState = (uint)unitData.EmoteState;
                row.Data.StandState = unitData.StandState;
                row.Data.PetTalentPoints = unitData.PetTalentPoints;
                row.Data.VisFlags = unitData.VisFlags;
                row.Data.AnimTier = unitData.AnimTier;
                row.Data.SheatheState = unitData.SheatheState;
                row.Data.PvpFlags = unitData.PvpFlags;
                row.Data.PetFlags = unitData.PetFlags;
                row.Data.ShapeshiftForm = unitData.ShapeshiftForm;
                row.Data.SpeedWalk = creature.OriginalMovement.WalkSpeed / MovementInfo.DEFAULT_WALK_SPEED;
                row.Data.SpeedRun = creature.OriginalMovement.RunSpeed / MovementInfo.DEFAULT_RUN_SPEED;
                row.Data.BoundingRadius = unitData.BoundingRadius;
                row.Data.CombatReach = unitData.CombatReach;
                row.Data.ModMeleeHaste = unitData.ModHaste;
                row.Data.ModRangedHaste = unitData.ModRangedHaste;
                row.Data.BaseAttackTime = unitData.AttackRoundBaseTime[0];
                row.Data.RangedAttackTime = unitData.RangedAttackRoundBaseTime;
                row.Data.MainHandSlotItem = (uint)unitData.VirtualItems[0].ItemID;
                row.Data.OffHandSlotItem = (uint)unitData.VirtualItems[1].ItemID;
                row.Data.RangedSlotItem = (uint)unitData.VirtualItems[2].ItemID;

                row.Data.SniffId = creature.SourceSniffId;
                row.Data.SniffBuild = creature.SourceSniffBuild;

                row.Data.Auras = creature.GetAurasString(false);

                var addonRow = new Row<CreatureAddon>();
                if (Settings.SqlTables.creature_addon)
                {
                    addonRow.Data.GUID = "@CGUID+" + creature.DbGuid;
                    addonRow.Data.Mount = (uint)unitData.MountDisplayID;
                    addonRow.Data.Bytes1 = creature.Bytes1;
                    addonRow.Data.Bytes2 = creature.Bytes2;
                    addonRow.Data.Emote = (uint)unitData.EmoteState;
                    addonRow.Data.Auras = creature.GetAurasString(true);
                    addonRow.Data.AIAnimKit = creature.AIAnimKit.GetValueOrDefault(0);
                    addonRow.Data.MovementAnimKit = creature.MovementAnimKit.GetValueOrDefault(0);
                    addonRow.Data.MeleeAnimKit = creature.MeleeAnimKit.GetValueOrDefault(0);
                    addonRow.Comment += StoreGetters.GetName(StoreNameType.Unit, (int)unit.Key.GetEntry(), false);
                    addonRows.Add(addonRow);
                }

                if (Settings.SqlTables.creature_guid_values)
                {
                    if (!unitData.Charm.IsEmpty() ||
                        !unitData.Summon.IsEmpty() ||
                        !unitData.CharmedBy.IsEmpty() ||
                        !unitData.CreatedBy.IsEmpty() ||
                        !unitData.SummonedBy.IsEmpty() ||
                        !unitData.Target.IsEmpty())
                    {
                        Row<CreatureGuidValues> guidsRow = new Row<CreatureGuidValues>();
                        guidsRow.Data.GUID = "@CGUID+" + creature.DbGuid;
                        Storage.GetObjectDbGuidEntryType(unitData.Charm, out guidsRow.Data.CharmGuid, out guidsRow.Data.CharmId, out guidsRow.Data.CharmType);
                        Storage.GetObjectDbGuidEntryType(unitData.Summon, out guidsRow.Data.SummonGuid, out guidsRow.Data.SummonId, out guidsRow.Data.SummonType);
                        Storage.GetObjectDbGuidEntryType(unitData.CharmedBy, out guidsRow.Data.CharmedByGuid, out guidsRow.Data.CharmedById, out guidsRow.Data.CharmedByType);
                        Storage.GetObjectDbGuidEntryType(unitData.CreatedBy, out guidsRow.Data.CreatedByGuid, out guidsRow.Data.CreatedById, out guidsRow.Data.CreatedByType);
                        Storage.GetObjectDbGuidEntryType(unitData.SummonedBy, out guidsRow.Data.SummonedByGuid, out guidsRow.Data.SummonedById, out guidsRow.Data.SummonedByType);
                        Storage.GetObjectDbGuidEntryType(unitData.Target, out guidsRow.Data.TargetGuid, out guidsRow.Data.TargetId, out guidsRow.Data.TargetType);
                        guidValuesRows.Add(guidsRow);
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
                            create1Row.Data.PositionX = createTime.PositionX;
                            create1Row.Data.PositionY = createTime.PositionY;
                            create1Row.Data.PositionZ = createTime.PositionZ;
                            create1Row.Data.Orientation = createTime.Orientation;
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
                            create2Row.Data.PositionX = createTime.PositionX;
                            create2Row.Data.PositionY = createTime.PositionY;
                            create2Row.Data.PositionZ = createTime.PositionZ;
                            create2Row.Data.Orientation = createTime.Orientation;
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
                            foreach (var aura in update.Item1)
                            {
                                var updateRow = new Row<CreatureAurasUpdate>();
                                updateRow.Data.GUID = "@CGUID+" + creature.DbGuid;
                                updateRow.Data.UpdateId = updateId;
                                updateRow.Data.Slot = aura.Slot;
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
                                updateRow.Data.UnixTimeMs = (ulong)Utilities.GetUnixTimeMsFromDateTime(update.Item2);
                                updateAurasRows.Add(updateRow);
                            }
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

                if (Settings.SqlTables.player_movement_client)
                {
                    foreach (var movement in Storage.PlayerMovements)
                    {
                        if (movement.guid != unit.Key)
                            continue;

                        Row<ClientSideMovement> clientMovementRow = new Row<ClientSideMovement>();
                        clientMovementRow.Data.Guid = "@CGUID+" + creature.DbGuid;
                        clientMovementRow.Data.MoveFlags = movement.MoveFlags;
                        clientMovementRow.Data.MoveTime = movement.MoveTime;
                        clientMovementRow.Data.Map = movement.Map;
                        clientMovementRow.Data.PositionX = movement.Position.X;
                        clientMovementRow.Data.PositionY = movement.Position.Y;
                        clientMovementRow.Data.PositionZ = movement.Position.Z;
                        clientMovementRow.Data.Orientation = movement.Position.O;
                        clientMovementRow.Data.Opcode = Opcodes.GetOpcodeName(movement.Opcode, movement.OpcodeDirection);
                        clientMovementRow.Data.UnixTimeMs = (ulong)Utilities.GetUnixTimeMsFromDateTime(movement.Time);
                        movementClientRows.Add(clientMovementRow);
                    }
                }

                if (Settings.SqlTables.creature_movement_server &&
                        creature.Waypoints != null &&
                        creature.OriginalMovement.Position != null)
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

                    float maxDistanceFromSpawn = 0;
                    foreach (ServerSideMovement waypoint in creature.Waypoints)
                    {
                        if (waypoint == null)
                            break;

                        // Get max wander distance
                        float distanceFromSpawn = Utilities.GetDistance3D(creature.OriginalMovement.Position.X, creature.OriginalMovement.Position.Y, creature.OriginalMovement.Position.Z, waypoint.StartPositionX, waypoint.StartPositionY, waypoint.StartPositionZ);
                        if (distanceFromSpawn > maxDistanceFromSpawn)
                            maxDistanceFromSpawn = distanceFromSpawn;

                        var movementRow = new Row<ServerSideMovement>();
                        movementRow.Data = waypoint;
                        movementRow.Data.GUID = "@CGUID+" + creature.DbGuid;
                        movementRow.Comment += StoreGetters.GetName(StoreNameType.Unit, (int)unit.Key.GetEntry(), false);
                        movementRows.Add(movementRow);
                    }
                    row.Data.WanderDistance = maxDistanceFromSpawn;
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

                // Likely to be waypoints if distance is big
                if (row.Data.WanderDistance > 20)
                    row.Data.MovementType = 2;

                if (creature.IsOnTransport() && badTransport)
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
            // delete query for GUIDs
            var delete = new SQLDelete<Creature>(Tuple.Create("@CGUID+0", "@CGUID+" + maxDbGuid));
            result.Append(delete.Build());
            var sql = new SQLInsert<Creature>(rows, false);
            result.Append(sql.Build());
            result.AppendLine();

            if (Settings.SqlTables.creature_addon)
            {
                var addonDelete = new SQLDelete<CreatureAddon>(Tuple.Create("@CGUID+0", "@CGUID+" + maxDbGuid));
                result.Append(addonDelete.Build());
                var addonSql = new SQLInsert<CreatureAddon>(addonRows, false);
                result.Append(addonSql.Build());
                result.AppendLine();
            }

            if (Settings.SqlTables.creature_guid_values)
            {
                var guidValuesDelete = new SQLDelete<CreatureGuidValues>(Tuple.Create("@CGUID+0", "@CGUID+" + maxDbGuid));
                result.Append(guidValuesDelete.Build());
                var guidValuesSql = new SQLInsert<CreatureGuidValues>(guidValuesRows, false);
                result.Append(guidValuesSql.Build());
                result.AppendLine();
            }

            if (Settings.SqlTables.client_creature_interact)
            {
                var interactSql = new SQLInsert<CreatureClientInteract>(interactRows, false);
                result.Append(interactSql.Build());
                result.AppendLine();
            }

            if (Settings.SqlTables.creature_create1_time)
            {
                var createSql = new SQLInsert<CreatureCreate1>(create1Rows, false);
                result.Append(createSql.Build());
                result.AppendLine();
            }

            if (Settings.SqlTables.creature_create2_time)
            {
                var createSql = new SQLInsert<CreatureCreate2>(create2Rows, false);
                result.Append(createSql.Build());
                result.AppendLine();
            }

            if (Settings.SqlTables.creature_destroy_time)
            {
                var destroySql = new SQLInsert<CreatureDestroy>(destroyRows, false);
                result.Append(destroySql.Build());
                result.AppendLine();
            }

            if (Settings.SqlTables.creature_auras_update)
            {
                var updateSql = new SQLInsert<CreatureAurasUpdate>(updateAurasRows, false);
                result.Append(updateSql.Build());
                result.AppendLine();
            }

            if (Settings.SqlTables.creature_values_update)
            {
                var updateSql = new SQLInsert<CreatureValuesUpdate>(updateValuesRows, false);
                result.Append(updateSql.Build());
                result.AppendLine();
            }

            if (Settings.SqlTables.creature_speed_update)
            {
                var updateSql = new SQLInsert<CreatureSpeedUpdate>(updateSpeedRows, false);
                result.Append(updateSql.Build());
                result.AppendLine();
            }

            if (Settings.SqlTables.creature_movement_client)
            {
                var clientMovementSql = new SQLInsert<ClientSideMovement>(movementClientRows, false, false, "creature_movement_client");
                result.Append(clientMovementSql.Build());
                result.AppendLine();
            }

            if (Settings.SqlTables.creature_movement_server)
            {
                // creature_movement_server
                var movementSql = new SQLInsert<ServerSideMovement>(movementRows, false);
                result.Append(movementSql.Build());
                result.AppendLine();

                // creature_movement_server_spline
                var movementSplineSql = new SQLInsert<ServerSideMovementSpline>(movementSplineRows, false);
                result.Append(movementSplineSql.Build());
                result.AppendLine();
            }

            if (Settings.SqlTables.creature_movement_server_combat)
            {
                // creature_movement_server_combat
                var movementSql = new SQLInsert<ServerSideMovement>(movementCombatRows, false, false, "creature_movement_server_combat");
                result.Append(movementSql.Build());
                result.AppendLine();

                // creature_movement_server_combat_spline
                var movementSplineSql = new SQLInsert<ServerSideMovementSpline>(movementCombatSplineRows, false, false, "creature_movement_server_combat_spline");
                result.Append(movementSplineSql.Build());
                result.AppendLine();
            }

            if (Settings.SqlTables.creature_emote)
            {
                var emoteSql = new SQLInsert<CreatureEmote>(emoteRows, false);
                result.Append(emoteSql.Build());
                result.AppendLine();
            }

            if (Settings.SqlTables.creature_attack_log)
            {
                var attackSql = new SQLInsert<UnitMeleeAttackLog>(attackLogRows, false, false, "creature_attack_log");
                result.Append(attackSql.Build());
                result.AppendLine();
            }

            if (Settings.SqlTables.creature_attack_start)
            {
                var attackSql = new SQLInsert<CreatureAttackToggle>(attackStartRows, false, false, "creature_attack_start");
                result.Append(attackSql.Build());
                result.AppendLine();
            }

            if (Settings.SqlTables.creature_attack_stop)
            {
                var attackSql = new SQLInsert<CreatureAttackToggle>(attackStopRows, false, false, "creature_attack_stop");
                result.Append(attackSql.Build());
                result.AppendLine();
            }

            if (Settings.SqlTables.creature_equipment_values_update)
            {
                var updateSql = new SQLInsert<CreatureEquipmentValuesUpdate>(updateEquipmentValuesRows, false, false);
                result.Append(updateSql.Build());
                result.AppendLine();
            }

            if (Settings.SqlTables.creature_guid_values_update)
            {
                var updateSql = new SQLInsert<CreatureGuidValuesUpdate>(updateGuidValuesRows, false, false);
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
            var rows = new RowList<GameObjectModel>();
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
                Row<GameObjectModel> row = new Row<GameObjectModel>();

                GameObject go = gameobject.Value;

                if (Settings.AreaFilters.Length > 0)
                    if (!(go.Area.ToString(CultureInfo.InvariantCulture).MatchesFilters(Settings.AreaFilters)))
                        continue;

                if (Settings.MapFilters.Length > 0)
                    if (!(go.Map.ToString(CultureInfo.InvariantCulture).MatchesFilters(Settings.MapFilters)))
                        continue;

                uint entry = (uint)go.ObjectData.EntryID;
                if (entry == 0)
                    continue;   // broken entry, nothing to spawn

                if (go.IsTemporarySpawn() && !Settings.SaveTempSpawns)
                    continue;

                bool badTransport = false;

                row.Data.GUID = "@OGUID+" + go.DbGuid;

                row.Data.ID = entry;
                if (!go.IsOnTransport())
                    row.Data.Map = go.Map;
                else
                {
                    int mapId;
                    badTransport = !GetTransportMap(go, out mapId);
                    if (mapId != -1)
                        row.Data.Map = (uint)mapId;
                }

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
                    row.Data.PhaseID = string.Join(" - ", go.Phases);

                if (!go.IsOnTransport())
                {
                    row.Data.PositionX = go.Movement.Position.X;
                    row.Data.PositionY = go.Movement.Position.Y;
                    row.Data.PositionZ = go.Movement.Position.Z;
                    row.Data.Orientation = go.Movement.Orientation;
                }
                else
                {
                    row.Data.PositionX = go.Movement.TransportOffset.X;
                    row.Data.PositionY = go.Movement.TransportOffset.Y;
                    row.Data.PositionZ = go.Movement.TransportOffset.Z;
                    row.Data.Orientation = go.Movement.TransportOffset.O;
                }

                var rotation = go.GetStaticRotation();
                row.Data.Rotation = new float?[] { rotation.X, rotation.Y, rotation.Z, rotation.W };

                bool add = true;
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

                        if (addonRow.Data.parentRot0 == 0.0f &&
                            addonRow.Data.parentRot1 == 0.0f &&
                            addonRow.Data.parentRot2 == 0.0f &&
                            addonRow.Data.parentRot3 == 1.0f)
                            add = false;
                    }
                    else
                        add = false;

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
                            create1Row.Data.PositionX = createTime.PositionX;
                            create1Row.Data.PositionY = createTime.PositionY;
                            create1Row.Data.PositionZ = createTime.PositionZ;
                            create1Row.Data.Orientation = createTime.Orientation;
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
                            create2Row.Data.PositionX = createTime.PositionX;
                            create2Row.Data.PositionY = createTime.PositionY;
                            create2Row.Data.PositionZ = createTime.PositionZ;
                            create2Row.Data.Orientation = createTime.Orientation;
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
                row.Data.AnimProgress = go.GameObjectDataOriginal.AnimProgress;
                row.Data.State = (uint)go.GameObjectDataOriginal.State;
                row.Data.Faction = (uint)go.GameObjectDataOriginal.FactionTemplate;
                row.Data.Flags = go.GameObjectDataOriginal.Flags;
                row.Data.Level = (uint)go.GameObjectDataOriginal.Level;
                row.Data.SniffId = go.SourceSniffId;
                row.Data.SniffBuild = go.SourceSniffBuild;

                // set some defaults
                row.Data.PhaseGroup = 0;
                row.Data.TemporarySpawn = (byte)(go.IsTemporarySpawn() ? 1 : 0);

                if (go.IsTransport())
                {
                    row.CommentOut = true;
                    row.Comment += " - !!! transport !!!";
                    if (Settings.SqlTables.gameobject_addon)
                    {
                        addonRow.CommentOut = true;
                        addonRow.Comment += " - !!! transport !!!";
                    }
                }
                else if (go.IsOnTransport() && badTransport)
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
            // delete query for GUIDs
            var delete = new SQLDelete<GameObjectModel>(Tuple.Create("@OGUID+0", "@OGUID+" + maxDbGuid));
            result.Append(delete.Build());

            var sql = new SQLInsert<GameObjectModel>(rows, false);
            result.Append(sql.Build());

            if (Settings.SqlTables.gameobject_addon)
            {
                var addonDelete = new SQLDelete<GameObjectAddon>(Tuple.Create("@OGUID+0", "@OGUID+" + maxDbGuid));
                result.Append(addonDelete.Build());
                var addonSql = new SQLInsert<GameObjectAddon>(addonRows, false);
                result.Append(addonSql.Build());
            }

            if (Settings.SqlTables.gameobject_create1_time)
            {
                var createSql = new SQLInsert<GameObjectCreate1>(create1Rows, false);
                result.Append(createSql.Build());
            }

            if (Settings.SqlTables.gameobject_create2_time)
            {
                var createSql = new SQLInsert<GameObjectCreate2>(create2Rows, false);
                result.Append(createSql.Build());
            }

            if (Settings.SqlTables.gameobject_custom_anim)
            {
                var animSql = new SQLInsert<GameObjectCustomAnim>(customAnimRows, false);
                result.Append(animSql.Build());
            }

            if (Settings.SqlTables.gameobject_despawn_anim)
            {
                var animSql = new SQLInsert<GameObjectDespawnAnim>(despawnAnimRows, false);
                result.Append(animSql.Build());
            }

            if (Settings.SqlTables.gameobject_destroy_time)
            {
                var destroySql = new SQLInsert<GameObjectDestroy>(destroyRows, false);
                result.Append(destroySql.Build());
            }

            if (Settings.SqlTables.gameobject_values_update)
            {
                var updateSql = new SQLInsert<GameObjectUpdate>(updateRows, false);
                result.Append(updateSql.Build());
            }

            if (Settings.SqlTables.client_gameobject_use)
            {
                var useSql = new SQLInsert<GameObjectClientUse>(useRows, false);
                result.Append(useSql.Build());
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

                bool badTransport = false;
                if (!dynObject.IsOnTransport())
                    row.Data.Map = dynObject.Map;
                else
                {
                    int mapId;
                    badTransport = !GetTransportMap(dynObject, out mapId);
                    if (mapId != -1)
                        row.Data.Map = (uint)mapId;
                }

                if (!dynObject.IsOnTransport())
                {
                    row.Data.PositionX = dynObject.OriginalMovement.Position.X;
                    row.Data.PositionY = dynObject.OriginalMovement.Position.Y;
                    row.Data.PositionZ = dynObject.OriginalMovement.Position.Z;
                    row.Data.Orientation = dynObject.OriginalMovement.Orientation;
                }
                else
                {
                    row.Data.PositionX = dynObject.OriginalMovement.TransportOffset.X;
                    row.Data.PositionY = dynObject.OriginalMovement.TransportOffset.Y;
                    row.Data.PositionZ = dynObject.OriginalMovement.TransportOffset.Z;
                    row.Data.Orientation = dynObject.OriginalMovement.TransportOffset.O;
                }

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
                            create1Row.Data.PositionX = createTime.PositionX;
                            create1Row.Data.PositionY = createTime.PositionY;
                            create1Row.Data.PositionZ = createTime.PositionZ;
                            create1Row.Data.Orientation = createTime.Orientation;
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
                            create2Row.Data.PositionX = createTime.PositionX;
                            create2Row.Data.PositionY = createTime.PositionY;
                            create2Row.Data.PositionZ = createTime.PositionZ;
                            create2Row.Data.Orientation = createTime.Orientation;
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

            // delete query for GUIDs
            var delete = new SQLDelete<DynamicObjectSpawn>(Tuple.Create("@DGUID+0", "@DGUID+" + maxDbGuid));
            result.Append(delete.Build());

            var sql = new SQLInsert<DynamicObjectSpawn>(rows, false);
            result.Append(sql.Build());

            if (Settings.SqlTables.dynamicobject_create1_time)
            {
                var createSql = new SQLInsert<DynamicObjectCreate1>(create1Rows, false);
                result.Append(createSql.Build());
            }

            if (Settings.SqlTables.dynamicobject_create2_time)
            {
                var createSql = new SQLInsert<DynamicObjectCreate2>(create2Rows, false);
                result.Append(createSql.Build());
            }

            if (Settings.SqlTables.dynamicobject_destroy_time)
            {
                var destroySql = new SQLInsert<DynamicObjectDestroy>(destroyRows, false);
                result.Append(destroySql.Build());
            }

            return result.ToString();
        }
    }
}

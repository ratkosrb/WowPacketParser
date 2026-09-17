using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Store.Objects.UpdateFields;
using WowPacketParser.Store.Objects.UpdateFields.LegacyImplementation;

namespace WowPacketParser.Store.Objects
{
    public class Unit : WoWObject
    {
        public static uint UnitGuidCounter = 0;
        public uint DbGuid;

        public List<Aura> Auras;
        public List<Aura> AurasOriginal;
        public HashSet<uint> TemplateAuras;
        public List<ServerSideMovement> Waypoints;
        public List<Vector3> UniqueWaypoints;
        public List<ServerSideMovement> CombatMovements;
        public List<ServerSideMovementSpline> WaypointSplines;
        public List<ServerSideMovementSpline> CombatMovementSplines;

        // OOC movement data
        public uint TotalMovementsCount = 0;
        public float MaxTravelDistanceFromSpawn = 0.0f;
        public bool HasOnlyCyclicMovement = true;

        // Spell timer calculation
        public DateTime? EnterCombatTime;
        public bool DontSaveCombatSpellTimers;

        public ushort? AIAnimKit;
        public ushort? MovementAnimKit;
        public ushort? MeleeAnimKit;

        // Fields from UPDATE_FIELDS
        public uint Bytes1;
        public UnitDynamicFlags? DynamicFlags;
        public UnitDynamicFlagsWOD? DynamicFlagsWod;
        public uint Bytes2;

        public IUnitData UnitData;
        public IUnitData UnitDataOriginal;

        public Unit(bool isCreature = true) : base()
        {
            UnitData = new UnitData(this, false);
            UnitDataOriginal = new UnitData(this, true);
            CombatMovements = new List<ServerSideMovement>();
            CombatMovementSplines = new List<ServerSideMovementSpline>();

            if (isCreature)
            {
                DbGuid = ++UnitGuidCounter;
                TemplateAuras = new HashSet<uint>();
                Waypoints = new List<ServerSideMovement>();
                UniqueWaypoints = new List<Vector3>();
                WaypointSplines = new List<ServerSideMovementSpline>();

                EnterCombatTime = null;
                DontSaveCombatSpellTimers = false;
            }   
        }

        public override bool IsTemporarySpawn()
        {
            if (ForceTemporarySpawn)
                return true;

            // If our unit got any of the following update fields set,
            // it's probably a temporary spawn
            return !UnitData.SummonedBy.IsEmpty() || !UnitData.CreatedBy.IsEmpty() || UnitData.CreatedBySpell != 0;
        }

        public bool IsInCombat()
        {
            return ((UnitData.Flags & (uint)UnitFlags.IsInCombat) != 0);
        }

        public override void LoadValuesFromUpdateFields()
        {
            Bytes1 = BitConverter.ToUInt32(new byte[] { UnitData.StandState, UnitData.PetTalentPoints, UnitData.VisFlags, UnitData.AnimTier }, 0);
            Bytes2 = BitConverter.ToUInt32(new byte[] { UnitData.SheatheState, UnitData.PvpFlags, UnitData.PetFlags, UnitData.ShapeshiftForm }, 0);
            if (ClientVersion.AddedInVersion(ClientType.WarlordsOfDraenor))
                DynamicFlagsWod = (UnitDynamicFlagsWOD)ObjectData.DynamicFlags;
            else
                DynamicFlags  = UpdateFields.GetEnum<UnitField, UnitDynamicFlags?>(UnitField.UNIT_DYNAMIC_FLAGS);
        }

        public uint GetDynamicFlags()
        {
            if (ClientVersion.AddedInVersion(ClientType.WarlordsOfDraenor))
                return ObjectData.DynamicFlags;

            return UnitData.DynamicFlags;
        }

        public uint GetDynamicFlagsOriginal()
        {
            if (ClientVersion.AddedInVersion(ClientType.WarlordsOfDraenor))
                return ObjectDataOriginal.DynamicFlags;

            return UnitDataOriginal.DynamicFlags;
        }

        public Aura GetAuraInSlot(uint slot)
        {
            if (Auras == null)
                return null;

            foreach (Aura aura in Auras)
            {
                if (aura.Slot == slot)
                    return aura;
            }

            return null;
        }

        private string AurasToString(List<Aura> auraList, bool noCaster)
        {
            string auras = string.Empty;
            if (auraList != null && auraList.Count != 0)
            {
                foreach (Aura aura in auraList)
                {
                    if (aura == null || aura.SpellId == 0)
                        continue;

                    if (noCaster)
                    {
                        // usually "template auras" do not have caster
                        if (ClientVersion.AddedInVersion(ClientType.MistsOfPandaria) ? !aura.AuraFlags.HasAnyFlag(AuraFlagMoP.NoCaster) : !aura.AuraFlags.HasAnyFlag(AuraFlag.NotCaster))
                            continue;
                    }

                    auras += aura.SpellId + " ";
                }
                auras = auras.TrimEnd(' ');
            }

            return auras;
        }

        public string GetAurasString(bool noCaster)
        {
            return AurasToString(Auras, noCaster);
        }
        public string GetOriginalAurasString(bool noCaster)
        {
            return AurasToString(AurasOriginal, noCaster);
        }
        public string GetTemplateAurasString()
        {
            return string.Join(" ", TemplateAuras);
        }

        public void ApplyAuraUpdates(List<Aura> updates)
        {
            if (Auras == null)
            {
                Auras = updates.Select(aura => aura.Clone()).ToList(); ;
                return;
            }

            foreach (Aura update in updates)
            {
                Aura aura = GetAuraInSlot((uint)update.Slot);
                if (aura == null)
                {
                    Auras.Add(update.Clone());
                    continue;
                }

                if (aura.SpellId != update.SpellId)
                    aura.SpellId = update.SpellId;
                if (aura.VisualId != update.VisualId)
                    aura.VisualId = update.VisualId;
                if (aura.AuraFlags != update.AuraFlags)
                    aura.AuraFlags = update.AuraFlags;
                if (aura.ActiveFlags != update.ActiveFlags)
                    aura.ActiveFlags = update.ActiveFlags;
                if (aura.Level != update.Level)
                    aura.Level = update.Level;
                if (aura.Charges != update.Charges)
                    aura.Charges = update.Charges;
                if (aura.ContentTuningId != update.ContentTuningId)
                    aura.ContentTuningId = update.ContentTuningId;
                if (aura.CasterGuid != update.CasterGuid)
                    aura.CasterGuid = update.CasterGuid;
                if (aura.MaxDuration != update.MaxDuration)
                    aura.MaxDuration = update.MaxDuration;
                if (aura.Duration != update.Duration)
                    aura.Duration = update.Duration;
            }
        }

        public bool HasAuraMatchingCriteria(Func<uint, bool> auraCheckFunc, bool skipPermanent = false)
        {
            if (Auras == null)
                return false;

            foreach (Aura aura in Auras)
            {
                if (aura == null || aura.SpellId == 0)
                    continue;

                if (skipPermanent && !aura.HasDuration())
                    continue;

                if (auraCheckFunc(aura.SpellId))
                    return true;
            }

            return false;
        }

        public string GetAurasStringMatchingCriteria(Func<uint, bool> auraCheckFunc)
        {
            if (Auras == null)
                return "";

            List<Aura> matchingAuras = new List<Aura>();
            foreach (Aura aura in Auras)
            {
                if (aura == null || aura.SpellId == 0)
                    continue;

                if (auraCheckFunc(aura.SpellId))
                    matchingAuras.Add(aura);
            }

            return AurasToString(matchingAuras, false);
        }

        public void CheckForTemplateAuras()
        {
            if (Auras == null)
                return;

            foreach (Aura aura in Auras)
            {
                if (aura == null)
                    continue;

                if (ClientVersion.AddedInVersion(ClientType.MistsOfPandaria) ? !aura.AuraFlags.HasAnyFlag(AuraFlagMoP.NoCaster) : !aura.AuraFlags.HasAnyFlag(AuraFlag.NotCaster))
                    continue;

                if (aura.HasDuration())
                    continue;

                TemplateAuras.Add(aura.SpellId);
            }
        }

        private void CheckMaxMovementDistance(Vector3 position)
        {
            // Get max wander distance
            float distanceFromSpawn = Utilities.GetDistance3D(OriginalMovement.Position.X, OriginalMovement.Position.Y, OriginalMovement.Position.Z, position.X, position.Y, position.Z);
            if (distanceFromSpawn > MaxTravelDistanceFromSpawn)
                MaxTravelDistanceFromSpawn = distanceFromSpawn;
        }

        private void CheckUniqueWaypoints(Vector3 position)
        {
            foreach (var wp in UniqueWaypoints)
            {
                if (Utilities.GetDistance3D(wp.X, wp.Y, wp.Z, position.X, position.Y, position.Z) < 0.1f)
                    return;
            }

            UniqueWaypoints.Add(position);
        }

        private void CheckCyclicMovement(uint splineFlags)
        {
            if (ClientVersion.RemovedInVersion(ClientVersionBuild.V2_0_1_6180))
            {
                if (!splineFlags.HasAnyFlag(SplineFlagVanilla.Flying | SplineFlagVanilla.Cyclic | SplineFlagVanilla.EnterCycle))
                    HasOnlyCyclicMovement = false;
            }
            else if (ClientVersion.RemovedInVersion(ClientVersionBuild.V3_0_2_9056))
            {
                if (!splineFlags.HasAnyFlag(SplineFlagTBC.Flying | SplineFlagTBC.Cyclic | SplineFlagTBC.EnterCycle))
                    HasOnlyCyclicMovement = false;
            }
            else if (ClientVersion.RemovedInVersion(ClientVersionBuild.V4_2_2_14545))
            {
                if (!splineFlags.HasAnyFlag(SplineFlag.Cyclic | SplineFlag.EnterCycle) &&
                    !((SplineFlag)splineFlags).HasFlag(SplineFlag.Flying | SplineFlag.CatmullRom | SplineFlag.UncompressedPath))
                    HasOnlyCyclicMovement = false;
            }
            else if (ClientVersion.RemovedInVersion(ClientVersionBuild.V4_3_4_15595))
            {
                if (!splineFlags.HasAnyFlag(SplineFlag422.Cyclic | SplineFlag422.EnterCycle) &&
                    !((SplineFlag422)splineFlags).HasFlag(SplineFlag422.Flying | SplineFlag422.CatmullRom | SplineFlag422.UncompressedPath))
                    HasOnlyCyclicMovement = false;
            }
            else if (ClientVersion.RemovedInVersion(ClientVersionBuild.V7_0_3_22248))
            {
                if (!splineFlags.HasAnyFlag(SplineFlag434.Cyclic | SplineFlag434.EnterCycle) &&
                    !((SplineFlag434)splineFlags).HasFlag(SplineFlag434.Flying | SplineFlag434.CatmullRom | SplineFlag434.UncompressedPath))
                    HasOnlyCyclicMovement = false;
            }
            else
            {
                if (!splineFlags.HasAnyFlag(SplineFlag703.Cyclic | SplineFlag703.EnterCycle) &&
                    !((SplineFlag703)splineFlags).HasFlag(SplineFlag703.Flying | SplineFlag703.CatmullRom | SplineFlag703.UncompressedPath))
                    HasOnlyCyclicMovement = false;
            }
        }

        public void AddWaypoint(ServerSideMovement movementData, Vector3 startPosition, DateTime packetTime)
        {
            // update current position to spline start
            if (Movement != null)
                Movement.Position = startPosition;

            List<ServerSideMovement> list = null;
            if ((Type == ObjectType.Unit) && ((UnitData.Flags & (uint)UnitFlags.IsInCombat) == 0))
            {
                if (Settings.SqlTables.creature && OriginalMovement != null)
                {
                    TotalMovementsCount++;
                    if (HasOnlyCyclicMovement)
                        CheckCyclicMovement(movementData.SplineFlags);
                    CheckMaxMovementDistance(startPosition);
                    CheckUniqueWaypoints(startPosition);
                    if (movementData.SplinePoints != null)
                    {
                        foreach (Vector3 vector in movementData.SplinePoints)
                        {
                            CheckMaxMovementDistance(vector);
                        }
                    }
                }

                if (!Settings.SqlTables.creature_movement_server)
                    return;

                list = Waypoints;
            }
            else
            {
                if (Type == ObjectType.Unit)
                {
                    if (!Settings.SqlTables.creature_movement_server_combat)
                        return;
                }
                else
                {
                    if (!Settings.SqlTables.player_movement_server)
                        return;
                }

                list = CombatMovements;
            }

            movementData.Point = (uint)list.Count + 1;
            movementData.StartPositionX = startPosition.X;
            movementData.StartPositionY = startPosition.Y;
            movementData.StartPositionZ = startPosition.Z;
            movementData.UnixTimeMs = (ulong)Utilities.GetUnixTimeMsFromDateTime(packetTime);

            if (movementData.SplineCount > 0 &&
                movementData.SplinePoints != null)
            {
                int index = (int)movementData.SplineCount - 1;
                movementData.EndPositionX = movementData.SplinePoints[index].X;
                movementData.EndPositionY = movementData.SplinePoints[index].Y;
                movementData.EndPositionZ = movementData.SplinePoints[index].Z;

                if (movementData.SplineCount > 1)
                {
                    List<ServerSideMovementSpline> splinesList = null;
                    if ((Type == ObjectType.Unit) && ((UnitData.Flags & (uint)UnitFlags.IsInCombat) == 0))
                        splinesList = WaypointSplines;
                    else
                        splinesList = CombatMovementSplines;

                    uint counter = 0;
                    foreach (Vector3 vector in movementData.SplinePoints)
                    {
                        counter++;
                        ServerSideMovementSpline spline = new ServerSideMovementSpline();
                        spline.ParentPoint = movementData.Point;
                        spline.SplinePoint = counter;
                        spline.PositionX = vector.X;
                        spline.PositionY = vector.Y;
                        spline.PositionZ = vector.Z;
                        splinesList.Add(spline);
                    }
                }
                movementData.SplinePoints = null; // free memory
            }
            list.Add(movementData);
        }

        public byte? VisibilityDistanceType
        {
            get
            {
                if (UnitData.Flags2 == 0)
                    return null;
                if (((UnitFlags2)UnitData.Flags2).HasFlag(UnitFlags2.InfiniteAOI))
                    return 5;
                if (((UnitFlags2)UnitData.Flags2).HasFlag(UnitFlags2.GiganticAOI))
                    return 4;
                if (((UnitFlags2)UnitData.Flags2).HasFlag(UnitFlags2.LargeAOI))
                    return 3;
                return 0;
            }
        }
    }
}

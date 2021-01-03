using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
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
        public List<ServerSideMovement> Waypoints;
        public List<ServerSideMovement> CombatMovements;
        public List<ServerSideMovementSpline> WaypointSplines;
        public List<ServerSideMovementSpline> CombatMovementSplines;

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
            UnitData = new UnitData(this);
            UnitDataOriginal = new OriginalUnitData(this);
            CombatMovements = new List<ServerSideMovement>();
            CombatMovementSplines = new List<ServerSideMovementSpline>();

            if (isCreature)
            {
                DbGuid = ++UnitGuidCounter;
                Waypoints = new List<ServerSideMovement>();
                WaypointSplines = new List<ServerSideMovementSpline>();
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

        public override void LoadValuesFromUpdateFields()
        {
            Bytes1 = BitConverter.ToUInt32(new byte[] { UnitData.StandState, UnitData.PetTalentPoints, UnitData.VisFlags, UnitData.AnimTier }, 0);
            Bytes2 = BitConverter.ToUInt32(new byte[] { UnitData.SheatheState, UnitData.PvpFlags, UnitData.PetFlags, UnitData.ShapeshiftForm }, 0);
            if (ClientVersion.AddedInVersion(ClientType.WarlordsOfDraenor))
                DynamicFlagsWod = (UnitDynamicFlagsWOD)ObjectData.DynamicFlags;
            else
                DynamicFlags  = UpdateFields.GetEnum<UnitField, UnitDynamicFlags?>(UnitField.UNIT_DYNAMIC_FLAGS);
        }

        public string GetAurasString(bool noCaster)
        {
            string auras = string.Empty;
            if (Auras != null && Auras.Count != 0)
            {
                foreach (Aura aura in Auras)
                {
                    if (aura == null)
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

        public void AddWaypoint(ServerSideMovement movementData, Vector3 startPosition, DateTime packetTime)
        {
            List<ServerSideMovement> list = null;
            if ((Type == ObjectType.Unit) && ((UnitData.Flags & (uint)UnitFlags.IsInCombat) == 0))
                list = Waypoints;
            else
                list = CombatMovements;

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
    }
}

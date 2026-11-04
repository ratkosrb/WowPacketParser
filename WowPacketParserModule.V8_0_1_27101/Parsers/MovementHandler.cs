using System.Linq;
using WowPacketParser.DBC;
using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Store;
using WowPacketParser.Store.Objects;
using WowPacketParser.Parsing;
using CoreParsers = WowPacketParser.Parsing.Parsers;
using SplineFacingType = WowPacketParserModule.V6_0_2_19033.Enums.SplineFacingType;
using SplineFlag = WowPacketParserModule.V7_0_3_22248.Enums.SplineFlag;
using System.Collections.Generic;

namespace WowPacketParserModule.V8_0_1_27101.Parsers
{
    public static class MovementHandler
    {
        public static void ReadMonsterSplineFilter(Packet packet, params object[] indexes)
        {
            var count = packet.ReadUInt32("MonsterSplineFilterKey", indexes);
            packet.ReadSingle("BaseSpeed", indexes);
            packet.ReadInt16("StartOffset", indexes);
            packet.ReadSingle("DistToPrevFilterKey", indexes);
            packet.ReadInt16("AddedToStart", indexes);

            for (int i = 0; i < count; i++)
            {
                packet.ReadInt16("IDx", indexes, i);
                packet.ReadUInt16("Speed", indexes, i);
            }

            packet.ResetBitReader();
            packet.ReadBits("FilterFlags", 2, indexes);
        }

        public static void ReadMonsterSplineSpellEffectExtraData(Packet packet, params object[] indexes)
        {
            packet.ReadPackedGuid128("TargetGUID", indexes);
            packet.ReadUInt32("SpellVisualID", indexes);
            packet.ReadUInt32("ProgressCurveID", indexes);
            packet.ReadUInt32("ParabolicCurveID", indexes);
            packet.ReadSingle("JumpGravity", indexes);
        }

        public static void ReadMonsterSplineJumpExtraData(ServerSideMovement monsterMove, Packet packet, params object[] indexes)
        {
            float verticalSpeed = packet.ReadSingle("JumpGravity", indexes);
            uint effectStartTime = packet.ReadUInt32("StartTime", indexes);
            packet.ReadUInt32("Duration", indexes);

            if (monsterMove != null)
            {
                monsterMove.VerticalSpeed = verticalSpeed;
                monsterMove.EffectStartTime = effectStartTime;
            }
        }

        public static void ReadMovementSpline(ServerSideMovement monsterMove, Packet packet, Vector3 pos, params object[] indexes)
        {
            uint splineFlags = (uint)packet.ReadUInt32E<SplineFlag>("Flags", indexes);
            if (monsterMove != null)
                monsterMove.SplineFlags = splineFlags;
            if (ClientVersion.RemovedInVersion(ClientType.Shadowlands))
            {
                byte animTier = packet.ReadByte("AnimTier", indexes);
                uint effectStartTime = packet.ReadUInt32("TierTransStartTime", indexes);
                if (monsterMove != null)
                {
                    monsterMove.AnimTier = animTier;
                    monsterMove.EffectStartTime = effectStartTime;
                }
            }
            packet.ReadInt32("Elapsed", indexes);
            uint moveTime = packet.ReadUInt32("MoveTime", indexes);
            if (monsterMove != null)
                monsterMove.MoveTime = moveTime;
            packet.ReadUInt32("FadeObjectTime", indexes);

            packet.ReadByte("Mode", indexes);
            if (ClientVersion.RemovedInVersion(ClientType.Shadowlands))
                packet.ReadByte("VehicleExitVoluntary", indexes);

            WowGuid transportGuid = packet.ReadPackedGuid128("TransportGUID", indexes);
            if (monsterMove != null)
                monsterMove.TransportGuid = transportGuid;
            sbyte seat = packet.ReadSByte("VehicleSeat", indexes);
            if (monsterMove != null)
                monsterMove.TransportSeat = seat;

            packet.ResetBitReader();

            var type = packet.ReadBitsE<SplineFacingType>("Face", 2, indexes);
            var pointsCount = packet.ReadBits("PointsCount", 16, indexes);
            if (ClientVersion.AddedInVersion(ClientType.Shadowlands))
            {
                packet.ReadBit("VehicleExitVoluntary", indexes);
                packet.ReadBit("Interpolate", indexes);
            }
            var packedDeltasCount = packet.ReadBits("PackedDeltasCount", 16, indexes);
            var totalPointsCount = pointsCount + packedDeltasCount;
            var hasSplineFilter = packet.ReadBit("HasSplineFilter", indexes);
            var hasSpellEffectExtraData = packet.ReadBit("HasSpellEffectExtraData", indexes);
            var hasJumpExtraData = packet.ReadBit("HasJumpExtraData", indexes);
            var hasAnimTier = false;
            if (ClientVersion.AddedInVersion(ClientType.Shadowlands))
                hasAnimTier = packet.ReadBit("HasAnimTierTransition", indexes);
            var hasUnk901 = false;
            if (ClientVersion.AddedInVersion(ClientType.Shadowlands) && !ClientVersion.IsClassicClientVersionBuild(ClientVersion.Build))
                hasUnk901 = packet.ReadBit("HasUnknown", indexes);

            if (hasSplineFilter)
                ReadMonsterSplineFilter(packet, indexes, "MonsterSplineFilter");

            float orientation = 100;
            switch (type)
            {
                case SplineFacingType.Spot:
                    var faceSpot = packet.ReadVector3("FaceSpot", indexes);
                    orientation = Utilities.GetAngle(pos.X, pos.Y, faceSpot.X, faceSpot.Y);
                    break;
                case SplineFacingType.Target:
                    orientation = packet.ReadSingle("FaceDirection", indexes);
                    packet.ReadPackedGuid128("FacingGUID", indexes);
                    break;
                case SplineFacingType.Angle:
                    orientation = packet.ReadSingle("FaceDirection", indexes);
                    break;
                default:
                    break;
            }

            if (monsterMove != null)
            {
                monsterMove.Orientation = orientation;
                monsterMove.SplineCount = totalPointsCount;
                if (totalPointsCount > 0)
                    monsterMove.SplinePoints = new List<Vector3>();
            }

            Vector3 endpos = new Vector3();
            List<Vector3> pointsList = (monsterMove != null) ? new List<Vector3>() : null;
            for (int i = 0; i < pointsCount; i++)
            {
                var spot = packet.ReadVector3();

                // client always taking first point
                if (i == 0)
                    endpos = spot;

                if (monsterMove != null)
                    pointsList.Add(spot);

                packet.AddValue("Points", spot, indexes, i);
            }

            var waypoints = new Vector3[packedDeltasCount];
            for (int i = 0; i < packedDeltasCount; i++)
            {
                var packedDeltas = packet.ReadPackedVector3();
                waypoints[i].X = packedDeltas.X;
                waypoints[i].Y = packedDeltas.Y;
                waypoints[i].Z = packedDeltas.Z;
            }

            if (hasSpellEffectExtraData)
                ReadMonsterSplineSpellEffectExtraData(packet, indexes, "MonsterSplineSpellEffectExtra");

            if (hasJumpExtraData)
                ReadMonsterSplineJumpExtraData(monsterMove, packet, indexes, "MonsterSplineJumpExtraData");

            if (hasAnimTier)
            {
                packet.ReadInt32("TierTransitionID", indexes);
                uint effectStartTime = packet.ReadUInt32("StartTime", indexes);
                packet.ReadInt32("EndTime", indexes);
                byte animTier = packet.ReadByte("AnimTier", indexes);
                if (monsterMove != null)
                {
                    monsterMove.AnimTier = animTier;
                    monsterMove.EffectStartTime = effectStartTime;
                }
            }

            if (hasUnk901)
            {
                for (var i = 0; i < 16; ++i)
                {
                    packet.ReadInt32("Unknown1", indexes, "Unknown901", i);
                    packet.ReadInt32("Unknown2", indexes, "Unknown901", i);
                    packet.ReadInt32("Unknown3", indexes, "Unknown901", i);
                    packet.ReadInt32("Unknown4", indexes, "Unknown901", i);
                }
            }

            // Calculate mid pos
            var mid = new Vector3
            {
                X = (pos.X + endpos.X) * 0.5f,
                Y = (pos.Y + endpos.Y) * 0.5f,
                Z = (pos.Z + endpos.Z) * 0.5f
            };

            for (var i = 0; i < packedDeltasCount; ++i)
            {
                var vec = new Vector3
                {
                    X = mid.X - waypoints[i].X,
                    Y = mid.Y - waypoints[i].Y,
                    Z = mid.Z - waypoints[i].Z
                };

                if (monsterMove != null)
                    monsterMove.SplinePoints.Add(vec);

                packet.AddValue("WayPoints", vec, indexes, i);
            }

            if (monsterMove != null)
            {
                foreach (var point in pointsList)
                {
                    monsterMove.SplinePoints.Add(point);
                }
            }
        }

        public static void ReadMovementMonsterSpline(ServerSideMovement monsterMove, Packet packet, Vector3 pos, params object[] indexes)
        {
            packet.ReadUInt32("Id", indexes);
            packet.ReadVector3("Destination", indexes);

            packet.ResetBitReader();

            packet.ReadBit("CrzTeleport", indexes);
            packet.ReadBits("StopDistanceTolerance", 3, indexes);

            ReadMovementSpline(monsterMove, packet, pos, indexes, "MovementSpline");
        }

        [Parser(Opcode.SMSG_ON_MONSTER_MOVE)]
        public static void HandleOnMonsterMove(Packet packet)
        {
            WowGuid guid = packet.ReadPackedGuid128("MoverGUID");
            var pos = packet.ReadVector3("Position");

            Unit obj = null;
            ServerSideMovement monsterMove = null;
            if (Storage.Objects != null && Storage.Objects.ContainsKey(guid))
            {
                obj = Storage.Objects[guid].Item1 as Unit;
                if (obj != null && obj.Movement != null)
                    obj.Movement.HasWpsOrRandMov = true;
                monsterMove = new ServerSideMovement();
            }

            ReadMovementMonsterSpline(monsterMove, packet, pos, "MovementMonsterSpline");

            if (monsterMove != null)
            {
                if (guid == Storage.CurrentActivePlayer)
                    Storage.CurrentMoveSplineExpireTime = packet.UnixTimeMs + (long)monsterMove.MoveTime;

                if (Settings.SaveTransports || (monsterMove.TransportGuid == null || monsterMove.TransportGuid.IsEmpty()))
                    obj.AddWaypoint(monsterMove, pos, packet.Time);
            }
        }

        [Parser(Opcode.SMSG_PHASE_SHIFT_CHANGE)]
        public static void HandlePhaseShift(Packet packet)
        {
            CoreParsers.MovementHandler.ActivePhases.Clear();
            packet.ReadPackedGuid128("Client");
            // PhaseShiftData
            packet.ReadInt32E<PhaseShiftFlags>("PhaseShiftFlags");
            var count = packet.ReadInt32("PhaseShiftCount");
            packet.ReadPackedGuid128("PersonalGUID");
            for (var i = 0; i < count; ++i)
            {
                var flags = packet.ReadUInt16E<PhaseFlags>("PhaseFlags", i);
                var id = packet.ReadUInt16();
                
                if (Settings.UseDBC && DBC.Phase.ContainsKey(id))
                {
                    packet.WriteLine($"[{i}] ID: {id} ({(DBCPhaseFlags)DBC.Phase[id].Flags})");
                }
                else
                    packet.AddValue("ID", id, i);

                CoreParsers.MovementHandler.ActivePhases.Add(id, true);
            }
            if (DBC.Phases.Any())
            {
                foreach (var phaseGroup in DBC.GetPhaseGroups(CoreParsers.MovementHandler.ActivePhases.Keys))
                    packet.WriteLine($"PhaseGroup: { phaseGroup } Phases: { string.Join(" - ", DBC.Phases[phaseGroup]) }");
            }
            var visibleMapIDsCount = packet.ReadInt32("VisibleMapIDsCount") / 2;
            for (var i = 0; i < visibleMapIDsCount; ++i)
                packet.ReadInt16<MapId>("VisibleMapID", i);
            var preloadMapIDCount = packet.ReadInt32("PreloadMapIDsCount") / 2;
            for (var i = 0; i < preloadMapIDCount; ++i)
                packet.ReadInt16<MapId>("PreloadMapID", i);
            var uiMapPhaseIdCount = packet.ReadInt32("UiMapPhaseIDsCount") / 2;
            for (var i = 0; i < uiMapPhaseIdCount; ++i)
                packet.ReadInt16("UiMapPhaseId", i);
        }

        [Parser(Opcode.SMSG_MOVE_UPDATE_MOD_MOVEMENT_FORCE_MAGNITUDE)]
        public static void HandleMoveUpdateModMovementForceMagnitude(Packet packet)
        {
            V7_0_3_22248.Parsers.MovementHandler.ReadMovementStats(packet, "MovementStats");
            packet.ReadSingle("Speed");
        }
    }
}

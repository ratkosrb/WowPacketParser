using System;
using System.Collections.Generic;
using WowPacketParser.DBC;
using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;
using WowPacketParser.Store;
using WowPacketParser.Store.Objects;

namespace WowPacketParserModule.V1_13_2_31446.Parsers
{
    public static class SpellHandler
    {
        public static void ReadSpellCastLogData(Packet packet, params object[] idx)
        {
            packet.ReadSByte("Unk1_13_2", idx);
        }

        public static void ReadSpellTargetData(SpellCastData dbdata, Packet packet, uint spellID, params object[] idx)
        {
            packet.ResetBitReader();

            packet.ReadBitsE<TargetFlag>("Flags", 25, idx);

            var hasSrcLoc = packet.ReadBit("HasSrcLocation", idx);
            var hasDstLoc = packet.ReadBit("HasDstLocation", idx);
            var hasOrient = packet.ReadBit("HasOrientation", idx);
            var hasMapID = packet.ReadBit("hasMapID ", idx);
            var nameLength = packet.ReadBits(7);

            dbdata.MainTargetGuid = packet.ReadPackedGuid128("Unit", idx);
            packet.ReadPackedGuid128("Item", idx);

            if (hasSrcLoc)
                dbdata.SrcPosition = V6_0_2_19033.Parsers.SpellHandler.ReadLocation(packet, "SrcLocation");

            if (hasDstLoc)
                dbdata.DstPosition = V6_0_2_19033.Parsers.SpellHandler.ReadLocation(packet, "DstLocation");

            float orientation = 0;
            if (hasOrient)
                orientation = packet.ReadSingle("Orientation", idx);
            dbdata.Orientation = orientation;

            int mapID = -1;
            if (hasMapID)
                mapID = (ushort)packet.ReadInt32("MapID", idx);

            if (dbdata.DstPosition != null && mapID != -1 && packet.Direction == Direction.ServerToClient)
                Storage.StoreSpellTargetPosition(spellID, mapID, dbdata.DstPosition.Value, orientation);

            packet.ReadWoWString("Name", nameLength, idx);
        }

        public static void ReadSpellCastRequest(Packet packet, params object[] idx)
        {
            packet.ReadPackedGuid128("CastID", idx);

            for (var i = 0; i < 2; i++)
                packet.ReadInt32("Misc", idx, i);

            var spellId = packet.ReadUInt32<SpellId>("SpellID", idx);
            packet.ReadInt32("SpellXSpellVisualID", idx);

            V6_0_2_19033.Parsers.SpellHandler.ReadMissileTrajectoryRequest(packet, idx, "MissileTrajectory");

            packet.ReadPackedGuid128("Guid", idx);

            packet.ResetBitReader();

            packet.ReadBits("SendCastFlags", 5, idx);
            var hasMoveUpdate = packet.ReadBit("HasMoveUpdate", idx);

            var weightCount = packet.ReadBits("WeightCount", 2, idx);

            SpellCastData temp = new SpellCastData();
            ReadSpellTargetData(temp, packet, spellId, idx, "Target");
            
            if (hasMoveUpdate)
                V7_0_3_22248.Parsers.MovementHandler.ReadMovementStats(packet, idx, "MoveUpdate");

            for (var i = 0; i < weightCount; ++i)
                V6_0_2_19033.Parsers.SpellHandler.ReadSpellWeight(packet, idx, "Weight", i);
        }

        public static void ReadSpellCastData(SpellCastData dbdata, Packet packet, params object[] idx)
        {
            dbdata.CasterGuid = packet.ReadPackedGuid128("CasterGUID", idx);
            dbdata.CasterUnitGuid = packet.ReadPackedGuid128("CasterUnit", idx);

            packet.ReadPackedGuid128("CastID", idx);
            packet.ReadPackedGuid128("OriginalCastID", idx);

            dbdata.SpellID = packet.ReadUInt32<SpellId>("SpellID", idx);
            dbdata.VisualID = packet.ReadUInt32("SpellXSpellVisualID", idx);

            dbdata.CastFlags = packet.ReadUInt32("CastFlags", idx);
            dbdata.CastFlagsEx = packet.ReadUInt32("CastFlagsEx", idx);
            dbdata.CastTime = packet.ReadUInt32("CastTime", idx);

            V6_0_2_19033.Parsers.SpellHandler.ReadMissileTrajectoryResult(packet, idx, "MissileTrajectory");

            packet.ReadByte("DestLocSpellCastIndex", idx);

            V6_0_2_19033.Parsers.SpellHandler.ReadCreatureImmunities(packet, idx, "Immunities");

            V6_0_2_19033.Parsers.SpellHandler.ReadSpellHealPrediction(packet, idx, "Predict");

            packet.ResetBitReader();

            var hitTargetsCount = packet.ReadBits("HitTargetsCount", 16, idx);
            dbdata.HitTargetsCount = hitTargetsCount;
            var missTargetsCount = packet.ReadBits("MissTargetsCount", 16, idx);
            dbdata.MissTargetsCount = missTargetsCount;
            var missStatusCount = packet.ReadBits("MissStatusCount", 16, idx);
            dbdata.MissReasonsCount = missStatusCount;
            var remainingPowerCount = packet.ReadBits("RemainingPowerCount", 9, idx);

            var hasRuneData = packet.ReadBit("HasRuneData", idx);
            var targetPointsCount = packet.ReadBits("TargetPointsCount", 16, idx);
            var hasAmmoDisplayId = packet.ReadBit("HasAmmoDisplayId", idx);
            var hasAmmoInventoryType = packet.ReadBit("HasAmmoInventoryType", idx);

            for (var i = 0; i < missStatusCount; ++i)
            {
                uint reason = V6_0_2_19033.Parsers.SpellHandler.ReadSpellMissStatus(packet, idx, "MissStatus", i);
                dbdata.AddMissReason(reason);
            }

            ReadSpellTargetData(dbdata, packet, dbdata.SpellID, idx, "Target");

            for (var i = 0; i < hitTargetsCount; ++i)
            {
                WowGuid hitTarget = packet.ReadPackedGuid128("HitTarget", idx, i);
                dbdata.AddHitTarget(hitTarget);
                Storage.StoreSpellScriptTarget(dbdata.SpellID, hitTarget);
            }

            for (var i = 0; i < missTargetsCount; ++i)
            {
                WowGuid missTarget = packet.ReadPackedGuid128("MissTarget", idx, i);
                dbdata.AddMissTarget(missTarget);
            }

            for (var i = 0; i < remainingPowerCount; ++i)
                V6_0_2_19033.Parsers.SpellHandler.ReadSpellPowerData(packet, idx, "RemainingPower", i);

            if (hasRuneData)
                V7_0_3_22248.Parsers.SpellHandler.ReadRuneData(packet, idx, "RemainingRunes");

            for (var i = 0; i < targetPointsCount; ++i)
                V6_0_2_19033.Parsers.SpellHandler.ReadLocation(packet, idx, "TargetPoints", i);

            if (hasAmmoDisplayId)
                dbdata.AmmoDisplayId = packet.ReadInt32("AmmoDisplayId", idx);

            if (hasAmmoInventoryType)
                dbdata.AmmoInventoryType = (int)packet.ReadInt32E<InventoryType>("AmmoInventoryType", idx);

            packet.AddSniffData(StoreNameType.Spell, (int)dbdata.SpellID, "CAST");
        }

        [Parser(Opcode.SMSG_SPELL_START)]
        public static void HandleSpellStart(Packet packet)
        {
            SpellCastData castData = new SpellCastData();
            ReadSpellCastData(castData, packet, "Cast");
            Storage.StoreSpellCastData(castData, CastDataType.Start, packet);
        }

        [Parser(Opcode.SMSG_SPELL_GO)]
        public static void HandleSpellGo(Packet packet)
        {
            SpellCastData castData = new SpellCastData();
            ReadSpellCastData(castData, packet, "Cast");

            packet.ResetBitReader();

            var unkBit = packet.ReadBit();
            if (unkBit)
                packet.ReadSByte("UnkSByte");

            Storage.StoreSpellCastData(castData, CastDataType.Go, packet);
        }

        [Parser(Opcode.CMSG_CAST_SPELL)]
        public static void HandleCastSpell(Packet packet)
        {
            ReadSpellCastRequest(packet, "Cast");
        }

        [Parser(Opcode.CMSG_PET_CAST_SPELL)]
        public static void HandlePetCastSpell(Packet packet)
        {
            packet.ReadPackedGuid128("PetGUID");
            ReadSpellCastRequest(packet, "Cast");
        }

        [Parser(Opcode.SMSG_SET_PCT_SPELL_MODIFIER)]
        [Parser(Opcode.SMSG_SET_FLAT_SPELL_MODIFIER)]
        public static void HandleSetSpellModifierFlat(Packet packet)
        {
            var modCount = packet.ReadUInt32("Modifier type count");

            for (var j = 0; j < modCount; ++j)
            {
                packet.ReadByteE<SpellModOp>("Spell Mod", j);

                var modTypeCount = packet.ReadUInt32("Count", j);
                for (var i = 0; i < modTypeCount; ++i)
                {
                    if (ClientVersion.AddedInVersion(ClientVersionBuild.V1_13_4_33598))
                        packet.ReadInt32("Amount", j, i);
                    else
                        packet.ReadSingle("Amount", j, i);
                    packet.ReadByte("Spell Mask bitpos", j, i);
                }
            }
        }

        [HasSniffData]
        [Parser(Opcode.SMSG_AURA_UPDATE)]
        public static void HandleAuraUpdate(Packet packet)
        {
            bool isFullUpdate = packet.ReadBit("UpdateAll");
            int countBits = ClientVersion.AddedInVersion(ClientVersionBuild.V1_13_3_32790) ? 8 : 7;
            var count = packet.ReadBits("AurasCount", countBits);

            var auras = new List<Aura>();
            for (var i = 0; i < count; ++i)
            {
                var aura = new Aura();

                aura.Slot = packet.ReadByte("Slot", i);

                packet.ResetBitReader();
                var hasAura = packet.ReadBit("HasAura", i);
                if (hasAura)
                {
                    packet.ReadPackedGuid128("CastID", i);
                    aura.SpellId = (uint)packet.ReadInt32<SpellId>("SpellID", i);
                    aura.VisualId = (uint)packet.ReadInt32("SpellXSpellVisualID", i);
                    aura.AuraFlags = (uint)packet.ReadByteE<AuraFlagMoP>("Flags", i);
                    aura.ActiveFlags = packet.ReadUInt32("ActiveFlags", i);
                    aura.Level = packet.ReadUInt16("CastLevel", i);
                    aura.Charges = packet.ReadByte("Applications", i);
                    aura.ContentTuningId = packet.ReadInt32("ContentTuningID", i);

                    packet.ResetBitReader();

                    var hasCastUnit = packet.ReadBit("HasCastUnit", i);
                    var hasDuration = packet.ReadBit("HasDuration", i);
                    var hasRemaining = packet.ReadBit("HasRemaining", i);

                    var hasTimeMod = packet.ReadBit("HasTimeMod", i);

                    var pointsCount = packet.ReadBits("PointsCount", 6, i);
                    var effectCount = packet.ReadBits("EstimatedPoints", 6, i);

                    var hasContentTuning = packet.ReadBit("HasContentTuning", i);

                    if (hasContentTuning)
                        V8_0_1_27101.Parsers.SpellHandler.ReadContentTuningParams(packet, i, "ContentTuning");

                    if (hasCastUnit)
                        aura.CasterGuid = packet.ReadPackedGuid128("CastUnit", i);

                    aura.Duration = hasDuration ? packet.ReadInt32("Duration", i) : 0;
                    aura.MaxDuration = hasRemaining ? packet.ReadInt32("Remaining", i) : 0;

                    if (hasTimeMod)
                        packet.ReadSingle("TimeMod");

                    for (var j = 0; j < pointsCount; ++j)
                        packet.ReadSingle("Points", i, j);

                    for (var j = 0; j < effectCount; ++j)
                        packet.ReadSingle("EstimatedPoints", i, j);

                    packet.AddSniffData(StoreNameType.Spell, (int)aura.SpellId, "AURA_UPDATE");
                }
                auras.Add(aura);
            }

            var guid = packet.ReadPackedGuid128("UnitGUID");
            Storage.StoreUnitAurasUpdate(guid, auras, packet.Time, isFullUpdate);
        }

        [Parser(Opcode.SMSG_SPELL_UPDATE_CHAIN_TARGETS)]
        public static void HandleUpdateChainTargets(Packet packet)
        {
            SpellUniqueChainUpdate uniqueData = new SpellUniqueChainUpdate();
            packet.ReadPackedGuid128("Caster GUID");
            packet.ReadUInt32("SpellXSpellVisualID");
            packet.ReadPackedGuid128("CastID");
            packet.ReadPackedGuid128("Target GUID");
            uniqueData.SpellId = packet.ReadUInt32<SpellId>("SpellID");
            uniqueData.SniffId = packet.SniffId;
            Storage.SpellUniqueChainUpdates.Add(uniqueData);
        }

        [Parser(Opcode.CMSG_SELF_RES)]
        public static void HandleSpellRes(Packet packet)
        {
            packet.ReadUInt32<SpellId>("SpellID");
        }
    }
}

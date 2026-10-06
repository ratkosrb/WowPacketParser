using System;
using System.Collections.Generic;
using WowPacketParser.DBC;
using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;
using WowPacketParser.Store;
using WowPacketParser.Store.Objects;

namespace WowPacketParserModule.V8_0_1_27101.Parsers
{
    public static class SpellHandler
    {
        public static void ReadTalentGroupInfo(Packet packet, params object[] idx)
        {
            packet.ReadInt32("SpecId", idx);

            var talentIDsCount = packet.ReadUInt32("TalentIDsCount", idx);
            var pvpTalentIDsCount = packet.ReadUInt32("PvPTalentIDsCount", idx);

            for (var i = 0; i < talentIDsCount; ++i)
                packet.ReadUInt16("TalentID", idx, i);

            for (var i = 0; i < pvpTalentIDsCount; ++i)
            {
                packet.ReadUInt16("PvPTalentID", idx, i);
                packet.ReadByte("Slot", idx, i);
            }
        }

        public static void ReadTalentInfoUpdate(Packet packet, params object[] idx)
        {
            packet.ReadByte("ActiveGroup", idx);
            packet.ReadInt32("PrimarySpecialization", idx);

            var talentGroupsCount = packet.ReadUInt32("TalentGroupsCount", idx);
            for (var i = 0; i < talentGroupsCount; ++i)
                ReadTalentGroupInfo(packet, idx, "TalentGroupsCount", i);
        }

        [Parser(Opcode.SMSG_UPDATE_TALENT_DATA)]
        public static void ReadUpdateTalentData(Packet packet)
        {
            ReadTalentInfoUpdate(packet, "Info");
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

            dbdata.AmmoDisplayId = packet.ReadInt32("Ammo.DisplayID", idx);

            packet.ReadByte("DestLocSpellCastIndex", idx);

            V6_0_2_19033.Parsers.SpellHandler.ReadCreatureImmunities(packet, idx, "Immunities");

            V6_0_2_19033.Parsers.SpellHandler.ReadSpellHealPrediction(packet, idx, "Predict");

            packet.ResetBitReader();

            var hitTargetsCount = packet.ReadBits("HitTargetsCount", 16, idx);
            dbdata.HitTargetsCount = hitTargetsCount;
            var missTargetsCount = packet.ReadBits("MissTargetsCount", 16, idx);
            dbdata.MissTargetsCount = missTargetsCount;
            uint hitStatusCount = 0;
            if (ClientVersion.AddedInVersion(ClientVersionBuild.V8_1_0_28724))
                hitStatusCount = packet.ReadBits("HitStatusCount", 16, idx);
            var missStatusCount = packet.ReadBits("MissStatusCount", 16, idx);
            dbdata.MissReasonsCount = missStatusCount;
            var remainingPowerCount = packet.ReadBits("RemainingPowerCount", 9, idx);

            var hasRuneData = packet.ReadBit("HasRuneData", idx);
            var targetPointsCount = packet.ReadBits("TargetPointsCount", 16, idx);

            for (var i = 0; i < missStatusCount; ++i)
            {
                uint reason = V6_0_2_19033.Parsers.SpellHandler.ReadSpellMissStatus(packet, idx, "MissStatus", i);
                dbdata.AddMissReason(reason);
            }

            V7_0_3_22248.Parsers.SpellHandler.ReadSpellTargetData(dbdata, packet, dbdata.SpellID, idx, "Target");

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

            if (ClientVersion.AddedInVersion(ClientVersionBuild.V8_1_0_28724))
                for (var i = 0; i < hitStatusCount; ++i)
                    packet.ReadByte("HitStatus", idx, i);

            for (var i = 0; i < remainingPowerCount; ++i)
                V6_0_2_19033.Parsers.SpellHandler.ReadSpellPowerData(packet, idx, "RemainingPower", i);

            if (hasRuneData)
                V7_0_3_22248.Parsers.SpellHandler.ReadRuneData(packet, idx, "RemainingRunes");

            for (var i = 0; i < targetPointsCount; ++i)
                V6_0_2_19033.Parsers.SpellHandler.ReadLocation(packet, idx, "TargetPoints", i);

            packet.AddSniffData(StoreNameType.Spell, (int)dbdata.SpellID, "CAST");
        }

        public static void ReadSpellCastLogData(Packet packet, params object[] idx)
        {
            if (!packet.CanRead())
                return;

            packet.ReadInt64("Health", idx);
            packet.ReadInt32("AttackPower", idx);
            packet.ReadInt32("SpellPower", idx);
            packet.ReadInt32("Armor", idx);

            packet.ResetBitReader();

            var spellLogPowerDataCount = packet.ReadBits("SpellLogPowerData", 9, idx);

            // SpellLogPowerData
            for (var i = 0; i < spellLogPowerDataCount; ++i)
            {
                packet.ReadInt32("PowerType", idx, i);
                packet.ReadInt32("Amount", idx, i);
                packet.ReadInt32("Cost", idx, i);
            }
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

            var hasLogData = packet.ReadBit();
            if (hasLogData)
                ReadSpellCastLogData(packet, "LogData");

            Storage.StoreSpellCastData(castData, CastDataType.Go, packet);
        }

        public static void ReadContentTuningParams(Packet packet, params object[] idx)
        {
            packet.ResetBitReader();

            packet.ReadInt16("PlayerLevelDelta", idx);
            packet.ReadUInt16("PlayerItemLevel", idx);
            if (ClientVersion.AddedInVersion(ClientVersionBuild.V8_1_0_28724) && !ClientVersion.IsClassicClientVersionBuild(ClientVersion.Build))
                packet.ReadUInt16("TargetItemLevel", idx);
            packet.ReadUInt16("ScalingHealthItemLevelCurveID", idx);
            packet.ReadByte("TargetLevel", idx);
            packet.ReadByte("Expansion", idx);
            packet.ReadByte("TargetMinScalingLevel", idx);
            packet.ReadByte("TargetMaxScalingLevel", idx);
            packet.ReadSByte("TargetScalingLevelDelta", idx);
            packet.ReadBits("Type", 4, idx);
            packet.ReadBit("ScalesWithItemLevel", idx);
        }

        [HasSniffData]
        [Parser(Opcode.SMSG_AURA_UPDATE)]
        public static void HandleAuraUpdate(Packet packet)
        {
            bool isFullUpdate = packet.ReadBit("UpdateAll");
            var count = packet.ReadBits("AurasCount", 9);

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
                        ReadContentTuningParams(packet, i, "ContentTuning");

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

        [Parser(Opcode.SMSG_PLAY_SPELL_VISUAL)]
        public static void HandleCastVisual(Packet packet)
        {
            packet.ReadPackedGuid128("Source");
            packet.ReadPackedGuid128("Target");
            packet.ReadPackedGuid128("Transport");

            packet.ReadVector3("TargetPosition");

            packet.ReadInt32("SpellVisualID");
            packet.ReadSingle("TravelSpeed");
            if (ClientVersion.AddedInVersion(ClientVersionBuild.V8_1_0_28724))
                packet.ReadUInt16("HitReason");
            packet.ReadUInt16("MissReason");
            packet.ReadUInt16("ReflectStatus");

            packet.ReadSingle("LaunchDelay");
            packet.ReadSingle("MinDuration");

            packet.ReadBit("SpeedAsTime");
        }

        [Parser(Opcode.SMSG_PLAY_ORPHAN_SPELL_VISUAL)]
        public static void HandlePlayOrphanSpellVisual(Packet packet)
        {
            packet.ReadVector3("SourceLocation");
            packet.ReadVector3("SourceOrientation");
            packet.ReadVector3("TargetLocation");
            packet.ReadPackedGuid128("Target");
            packet.ReadInt32("SpellVisualID");
            packet.ReadSingle("TravelSpeed");
            packet.ReadSingle("LaunchDelay");
            packet.ReadSingle("MinDuration");
            packet.ReadBit("SpeedAsTime");
        }

        [Parser(Opcode.CMSG_CANCEL_CHANNELLING)]
        public static void HandleCancelChanneling(Packet packet)
        {
            packet.ReadInt32<SpellId>("SpellID");
            packet.ReadInt32("Reason");
        }

        [Parser(Opcode.SMSG_ADD_LOSS_OF_CONTROL)]
        public static void HandleAddLossOfControl(Packet packet)
        {
            packet.ReadPackedGuid128("Victim");
            packet.ReadInt32<SpellId>("SpellID");
            packet.ReadPackedGuid128("Caster");

            packet.ReadUInt32("Duration");
            packet.ReadUInt32("DurationRemaining");
            packet.ReadUInt32E<SpellSchoolMask>("LockoutSchoolMask");

            packet.ReadByteE<SpellMechanic>("Mechanic");
            packet.ReadByte("Type");
        }

        [Parser(Opcode.SMSG_SEND_SPELL_CHARGES)] // already since 7.?.?
        public static void HandleSendSpellCharges(Packet packet)
        {
            var int4 = packet.ReadInt32("SpellChargeEntryCount");
            for (int i = 0; i < int4; i++)
            {
                packet.ReadUInt32("Category", i);
                packet.ReadUInt32("NextRecoveryTime", i);
                packet.ReadSingle("ChargeModRate", i);
                packet.ReadByte("ConsumedCharges", i);
            }
        }

        [Parser(Opcode.SMSG_RESUME_CAST)]
        public static void HandleResumeCast(Packet packet)
        {
            packet.ReadPackedGuid128("CasterGUID");
            packet.ReadInt32("SpellXSpellVisualID");
            packet.ReadPackedGuid128("CastID");
            packet.ReadPackedGuid128("Target");
            packet.ReadInt32<SpellId>("SpellID");
        }

        [Parser(Opcode.SMSG_INTERRUPT_POWER_REGEN)]
        public static void HandleInterruptPowerRegen(Packet packet)
        {
            packet.ReadUInt32("PowerTypeEnum");
        }
    }
}

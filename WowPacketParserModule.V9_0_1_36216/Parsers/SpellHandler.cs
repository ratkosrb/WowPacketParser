using System.Collections.Generic;
using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;
using WowPacketParser.Store;
using WowPacketParser.Store.Objects;

namespace WowPacketParserModule.V9_0_1_36216.Parsers
{
    public static class SpellHandler
    {
        public static void ReadSpellCastVisual(out uint VisualID, Packet packet, params object[] indexes)
        {
            VisualID = (uint)packet.ReadInt32("SpellXSpellVisualID", indexes);
            packet.ReadInt32("ScriptVisualID", indexes);
        }

        public static void ReadOptionalReagent(Packet packet, params object[] indexes)
        {
            packet.ReadInt32<ItemId>("ItemID", indexes);
            packet.ReadInt32("Slot", indexes);
            packet.ReadInt32("Count", indexes);
        }

        public static void ReadOptionalCurrency(Packet packet, params object[] indexes)
        {
            packet.ReadInt32<ItemId>("ItemID", indexes);
            packet.ReadInt32("Slot", indexes);
            packet.ReadInt32("Count", indexes);
        }

        public static void ReadSpellCastRequest(Packet packet, params object[] idx)
        {
            packet.ReadPackedGuid128("CastID", idx);

            for (var i = 0; i < 2; i++)
                packet.ReadInt32("Misc", idx, i);

            var spellId = packet.ReadUInt32<SpellId>("SpellID", idx);

            uint temp;
            ReadSpellCastVisual(out temp, packet, idx);

            V6_0_2_19033.Parsers.SpellHandler.ReadMissileTrajectoryRequest(packet, idx, "MissileTrajectory");

            packet.ReadPackedGuid128("Guid", idx);

            var optionalReagentCount = packet.ReadUInt32("OptionalReagentCount", idx);
            var optionalCurrenciesCount = packet.ReadUInt32("OptionalCurrenciesCount", idx);

            for (var i = 0; i < optionalReagentCount; ++i)
                ReadOptionalReagent(packet, idx, "OptionalReagent", i);

            for (var j = 0; j < optionalCurrenciesCount; ++j)
                ReadOptionalCurrency(packet, idx, "OptionalCurrency", j);


            packet.ResetBitReader();
            packet.ReadBits("SendCastFlags", 5, idx);
            var hasMoveUpdate = packet.ReadBit("HasMoveUpdate", idx);

            var weightCount = packet.ReadBits("WeightCount", 2, idx);

            SpellCastData temp2 = new SpellCastData();
            V7_0_3_22248.Parsers.SpellHandler.ReadSpellTargetData(temp2, packet, spellId, idx, "Target");

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
            ReadSpellCastVisual(out dbdata.VisualID, packet, idx, "Visual");

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
            var hitStatusCount = packet.ReadBits("HitStatusCount", 16, idx);
            var missStatusCount = packet.ReadBits("MissStatusCount", 16, idx);
            var remainingPowerCount = packet.ReadBits("RemainingPowerCount", 9, idx);

            var hasRuneData = packet.ReadBit("HasRuneData", idx);
            var targetPointsCount = packet.ReadBits("TargetPointsCount", 16, idx);

            for (var i = 0; i < missStatusCount; ++i)
                V6_0_2_19033.Parsers.SpellHandler.ReadSpellMissStatus(packet, idx, "MissStatus", i);

            V8_0_1_27101.Parsers.SpellHandler.ReadSpellTargetData(dbdata, packet, dbdata.SpellID, idx, "Target");

            for (var i = 0; i < hitTargetsCount; ++i)
            {
                WowGuid hitTarget = packet.ReadPackedGuid128("HitTarget", idx, i);
                dbdata.AddHitTarget(hitTarget);
            }

            for (var i = 0; i < missTargetsCount; ++i)
            {
                WowGuid missTarget = packet.ReadPackedGuid128("MissTarget", idx, i);
                dbdata.AddMissTarget(missTarget);
            }

            for (var i = 0; i < hitStatusCount; ++i)
                packet.ReadByte("HitStatus", idx, i);

            for (var i = 0; i < remainingPowerCount; ++i)
                V6_0_2_19033.Parsers.SpellHandler.ReadSpellPowerData(packet, idx, "RemainingPower", i);

            if (hasRuneData)
                V7_0_3_22248.Parsers.SpellHandler.ReadRuneData(packet, idx, "RemainingRunes");

            for (var i = 0; i < targetPointsCount; ++i)
                V6_0_2_19033.Parsers.SpellHandler.ReadLocation(packet, idx, "TargetPoints", i);
        }

        [Parser(Opcode.SMSG_SPELL_START)]
        public static void HandleSpellStart(Packet packet)
        {
            SpellCastData castData = new SpellCastData();
            ReadSpellCastData(castData, packet, "Cast");
            Storage.StoreSpellCastData(castData, Storage.SpellCastStart, packet);
        }

        [Parser(Opcode.SMSG_SPELL_GO)]
        public static void HandleSpellGo(Packet packet)
        {
            SpellCastData castData = new SpellCastData();
            ReadSpellCastData(castData, packet, "Cast");

            packet.ResetBitReader();

            var hasLogData = packet.ReadBit();
            if (hasLogData)
                V8_0_1_27101.Parsers.SpellHandler.ReadSpellCastLogData(packet, "LogData");

            Storage.StoreSpellCastData(castData, Storage.SpellCastGo, packet);
        }

        [HasSniffData]
        [Parser(Opcode.SMSG_AURA_UPDATE)]
        public static void HandleAuraUpdate(Packet packet)
        {
            packet.ReadBit("UpdateAll");
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
                    ReadSpellCastVisual(out aura.VisualId, packet, i, "Visual");
                    aura.AuraFlags = (uint)packet.ReadUInt16E<AuraFlagMoP>("Flags", i);
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
                        CombatLogHandler.ReadContentTuningParams(packet, i, "ContentTuning");

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
            Storage.StoreUnitAurasUpdate(guid, auras, packet.Time);
        }

        [Parser(Opcode.SMSG_CAST_FAILED)]
        public static void HandleCastFailed(Packet packet)
        {
            packet.ReadPackedGuid128("CastID");
            packet.ReadInt32<SpellId>("SpellID");
            uint temp;
            ReadSpellCastVisual(out temp, packet, "Visual");
            packet.ReadInt32("Reason");
            packet.ReadInt32("FailedArg1");
            packet.ReadInt32("FailedArg2");
        }

        [Parser(Opcode.SMSG_SPELL_FAILURE)]
        public static void HandleSpellFailure(Packet packet)
        {
            SpellCastFailed failData = new SpellCastFailed();
            failData.Guid = packet.ReadPackedGuid128("CasterUnit");
            packet.ReadPackedGuid128("CastID");
            failData.SpellId = (uint)packet.ReadInt32<SpellId>("SpellID");
            ReadSpellCastVisual(out failData.VisualId, packet, "Visual");
            failData.Reason = (uint)packet.ReadInt16E<SpellCastFailureReason>("Reason");
            failData.Time = packet.Time;
            Storage.SpellCastFailed.Add(failData);
        }

        [Parser(Opcode.SMSG_SPELL_FAILED_OTHER)]
        public static void HandleSpellFailedOther(Packet packet)
        {
            SpellCastFailed failData = new SpellCastFailed();
            failData.Guid = packet.ReadPackedGuid128("CasterUnit");
            packet.ReadPackedGuid128("CastID");
            failData.SpellId = packet.ReadUInt32<SpellId>("SpellID");
            ReadSpellCastVisual(out failData.VisualId, packet, "Visual");
            failData.Reason = (uint)packet.ReadByteE<SpellCastFailureReason>("Reason");
            failData.Time = packet.Time;
            Storage.SpellCastFailed.Add(failData);
        }

        [Parser(Opcode.SMSG_LEARNED_SPELLS)]
        public static void HandleLearnedSpells(Packet packet)
        {
            var spellCount = packet.ReadUInt32("SpellCount");
            var favoriteSpellCount = packet.ReadUInt32("FavoriteSpellCount");
            packet.ReadUInt32("SpecializationID");

            for (var i = 0; i < spellCount; ++i)
                packet.ReadInt32<SpellId>("SpellID", i);

            for (var i = 0; i < favoriteSpellCount; ++i)
                packet.ReadInt32<SpellId>("FavoriteSpellID", i);

            packet.ReadBit("SuppressMessaging");
        }

        [Parser(Opcode.CMSG_CAST_SPELL)]
        public static void HandleCastSpell(Packet packet)
        {
            ReadSpellCastRequest(packet, "Cast");
        }

        [Parser(Opcode.SMSG_SPELL_CHANNEL_START, ClientVersionBuild.V7_2_0_23826)]
        public static void HandleSpellChannelStart(Packet packet)
        {
            SpellChannelStart channel = new SpellChannelStart();
            channel.Guid = packet.ReadPackedGuid128("CasterGUID");
            channel.SpellId = (uint)packet.ReadInt32<SpellId>("SpellID");
            ReadSpellCastVisual(out channel.VisualId, packet, "Visual");
            channel.Duration = packet.ReadInt32("ChannelDuration");

            var hasInterruptImmunities = packet.ReadBit("HasInterruptImmunities");
            var hasHealPrediction = packet.ReadBit("HasHealPrediction");

            if (hasInterruptImmunities)
                V6_0_2_19033.Parsers.SpellHandler.ReadSpellChannelStartInterruptImmunities(packet, "InterruptImmunities");

            if (hasHealPrediction)
                V6_0_2_19033.Parsers.SpellHandler.ReadSpellTargetedHealPrediction(packet, "HealPrediction");

            channel.Time = packet.Time;
            Storage.SpellChannelStart.Add(channel);
        }

        [Parser(Opcode.SMSG_PLAY_SPELL_VISUAL_KIT)]
        public static void HandleCastVisualKit(Packet packet)
        {
            PlaySpellVisualKit visualKitData = new PlaySpellVisualKit();
            visualKitData.Guid = packet.ReadPackedGuid128("Unit");
            visualKitData.KitId = (uint)packet.ReadInt32("KitRecID");
            visualKitData.KitType = (uint)packet.ReadInt32("KitType");
            visualKitData.Duration = packet.ReadUInt32("Duration");
            packet.ReadBit("MountedVisual");
            visualKitData.Time = packet.Time;
            Storage.SpellPlayVisualKit.Add(visualKitData);
        }

        [Parser(Opcode.CMSG_UPDATE_SPELL_VISUAL)]
        public static void HandleUpdateSpellVisual(Packet packet)
        {
            packet.ReadUInt32("SpellID");
            uint temp;
            ReadSpellCastVisual(out temp, packet, "Visual");
            packet.ReadPackedGuid128("GUID");
        }

        [Parser(Opcode.SMSG_RESUME_CAST)]
        public static void HandleResumeCast(Packet packet)
        {
            packet.ReadPackedGuid128("CasterGUID");
            uint temp;
            ReadSpellCastVisual(out temp, packet, "Visual");
            packet.ReadPackedGuid128("CastID");
            packet.ReadPackedGuid128("Target");
            packet.ReadInt32<SpellId>("SpellID");

        }

        [Parser(Opcode.SMSG_RESUME_CAST_BAR)]
        public static void HandleResumeCastBar(Packet packet)
        {
            packet.ReadPackedGuid128("Guid");
            packet.ReadPackedGuid128("Target");

            packet.ReadUInt32<SpellId>("SpellID");
            uint temp;
            ReadSpellCastVisual(out temp, packet, "Visual");
            packet.ReadUInt32("TimeRemaining");
            packet.ReadUInt32("TotalTime");

            var result = packet.ReadBit("HasInterruptImmunities");
            if (result)
            {
                packet.ReadUInt32("SchoolImmunities");
                packet.ReadUInt32("Immunities");
            }
        }

        [Parser(Opcode.SMSG_MIRROR_IMAGE_CREATURE_DATA)]
        public static void HandleGetMirrorImageData(Packet packet)
        {
            packet.ReadPackedGuid128("UnitGUID");
            packet.ReadInt32("DisplayID");
            packet.ReadInt32("SpellVisualKitID");
        }

        [Parser(Opcode.SMSG_MIRROR_IMAGE_COMPONENTED_DATA)]
        public static void HandleMirrorImageData(Packet packet)
        {
            packet.ReadPackedGuid128("UnitGUID");
            packet.ReadInt32("DisplayID");
            packet.ReadInt32("SpellVisualKitID");

            packet.ReadByte("RaceID");
            packet.ReadByte("Gender");
            packet.ReadByte("ClassID");
            var customizationCount = packet.ReadUInt32();
            packet.ReadPackedGuid128("GuildGUID");
            var itemDisplayCount = packet.ReadInt32("ItemDisplayCount");

            for (var j = 0u; j < customizationCount; ++j)
                CharacterHandler.ReadChrCustomizationChoice(packet, "Customizations", j);

            for (var i = 0; i < itemDisplayCount; i++)
                packet.ReadInt32("ItemDisplayID", i);
        }

        [Parser(Opcode.CMSG_PET_CAST_SPELL)]
        public static void HandlePetCastSpell(Packet packet)
        {
            packet.ReadPackedGuid128("PetGUID");
            ReadSpellCastRequest(packet, "Cast");
        }

        [Parser(Opcode.SMSG_INTERRUPT_POWER_REGEN)]
        public static void HandleInterruptPowerRegen(Packet packet)
        {
            packet.ReadUInt32E<PowerType>("PowerType");
        }
    }
}

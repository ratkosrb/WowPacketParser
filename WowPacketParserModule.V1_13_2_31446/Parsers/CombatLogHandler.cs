using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;
using WowPacketParser.Store;
using WowPacketParser.Store.Objects;

namespace WowPacketParserModule.V1_13_2_31446.Parsers
{
    public static class CombatLogHandler
    {
        public static void ReadCombatLogContentTuning(Packet packet, params object[] idx)
        {
            packet.ReadByte("Type", idx);
            packet.ReadByte("TargetLevel", idx);
            packet.ReadByte("Expansion", idx);
            packet.ReadByte("TargetMinScalingLevel", idx);
            packet.ReadByte("TargetMaxScalingLevel", idx);
            packet.ReadInt16("PlayerLevelDelta", idx);
            packet.ReadSByte("TargetScalingLevelDelta", idx);
            packet.ReadUInt16("PlayerItemLevel", idx);
            packet.ReadUInt16("ScalingHealthItemLevelCurveID", idx);
            packet.ReadByte("ScalesWithItemLevel", idx);
        }

        public static UnitMeleeAttackLog ReadAttackRoundInfo(Packet packet, params object[] indexes)
        {
            UnitMeleeAttackLog attackData = new UnitMeleeAttackLog();
            var hitInfo = packet.ReadInt32E<SpellHitInfo>("HitInfo", indexes);
            attackData.HitInfo = (uint)hitInfo;

            attackData.Attacker = packet.ReadPackedGuid128("AttackerGUID", indexes);
            attackData.Victim = packet.ReadPackedGuid128("TargetGUID", indexes);

            attackData.Damage = packet.ReadInt32("Damage", indexes);
            attackData.OriginalDamage = packet.ReadInt32("OriginalDamage", indexes);
            attackData.OverkillDamage = packet.ReadInt32("OverDamage", indexes);

            attackData.SubDamageCount = packet.ReadByte("Sub Damage Count", indexes);
            for (int i = 0; i < attackData.SubDamageCount; i++)
            {
                attackData.TotalSchoolMask |= (uint)packet.ReadInt32("SchoolMask", indexes);
                packet.ReadSingle("FloatDamage", indexes);
                packet.ReadInt32("IntDamage", indexes);

                if (hitInfo.HasAnyFlag(SpellHitInfo.HITINFO_PARTIAL_ABSORB | SpellHitInfo.HITINFO_FULL_ABSORB))
                    attackData.TotalAbsorbedDamage += packet.ReadInt32("DamageAbsorbed", indexes);

                if (hitInfo.HasAnyFlag(SpellHitInfo.HITINFO_PARTIAL_RESIST | SpellHitInfo.HITINFO_FULL_RESIST))
                    attackData.TotalResistedDamage += packet.ReadInt32("DamageResisted", indexes);
            }

            VictimStates victimStates = packet.ReadByteE<VictimStates>("VictimState", indexes);
            attackData.VictimState = (uint)victimStates;
            if (victimStates == VictimStates.VICTIMSTATE_UNK32)
            {
                packet.ReadInt32("UnkInt1", indexes);
                packet.ReadInt32("UnkInt2", indexes);
                packet.ReadInt32("UnkInt3", indexes);
            }

            attackData.AttackerState = packet.ReadInt32("AttackerState", indexes);
            attackData.SpellId = (uint)packet.ReadInt32<SpellId>("MeleeSpellID", indexes);

            if (hitInfo.HasAnyFlag(SpellHitInfo.HITINFO_BLOCK))
                attackData.BlockedDamage = packet.ReadInt32("BlockAmount", indexes);

            if (hitInfo.HasAnyFlag(SpellHitInfo.HITINFO_RAGE_GAIN))
                packet.ReadInt32("RageGained", indexes);

            if (hitInfo.HasAnyFlag(SpellHitInfo.HITINFO_DEBUG))
            {
                packet.ReadInt32("Unk Attacker State 3 1", indexes);
                packet.ReadSingle("Unk Attacker State 3 2", indexes);
                packet.ReadSingle("Unk Attacker State 3 3", indexes);
                packet.ReadSingle("Unk Attacker State 3 4", indexes);
                packet.ReadSingle("Unk Attacker State 3 5", indexes);
                packet.ReadSingle("Unk Attacker State 3 6", indexes);
                packet.ReadSingle("Unk Attacker State 3 7", indexes);
                packet.ReadSingle("Unk Attacker State 3 8", indexes);
                packet.ReadSingle("Unk Attacker State 3 9", indexes);
                packet.ReadSingle("Unk Attacker State 3 10", indexes);
                packet.ReadSingle("Unk Attacker State 3 11", indexes);
                packet.ReadInt32("Unk Attacker State 3 12", indexes);
            }

            if (hitInfo.HasAnyFlag(SpellHitInfo.HITINFO_BLOCK | SpellHitInfo.HITINFO_UNK12))
                packet.ReadSingle("Unk Float", indexes);

            ReadCombatLogContentTuning(packet, indexes, "ContentTuning");
            return attackData;
        }

        [Parser(Opcode.SMSG_ATTACKER_STATE_UPDATE)]
        public static void HandleAttackerStateUpdate(Packet packet)
        {
            var unkBit = packet.ReadBit("UnkBit");

            if (unkBit)
                packet.ReadSByte("UnkSByte");

            packet.ReadInt32("Size");

            UnitMeleeAttackLog attackData = ReadAttackRoundInfo(packet, "AttackRoundInfo");
            attackData.Time = packet.Time;
            Storage.StoreUnitAttackLog(attackData);
        }

        [Parser(Opcode.SMSG_SPELL_NON_MELEE_DAMAGE_LOG)]
        public static void HandleSpellNonMeleeDmgLog(Packet packet)
        {
            packet.ReadPackedGuid128("Me");
            packet.ReadPackedGuid128("CasterGUID");
            packet.ReadPackedGuid128("CastID");

            packet.ReadInt32<SpellId>("SpellID");
            packet.ReadInt32("SpellXSpellVisualID");
            packet.ReadInt32("Damage");
            packet.ReadInt32("OriginalDamage");
            packet.ReadInt32("OverKill");

            packet.ReadByte("SchoolMask");

            packet.ReadInt32("Absorbed");
            packet.ReadInt32("Resisted");
            packet.ReadInt32("ShieldBlock");

            packet.ResetBitReader();

            packet.ReadBit("Periodic");

            packet.ReadBitsE<AttackerStateFlags>("Flags", 7);

            var hasDebugData = packet.ReadBit("HasDebugData");
            var hasLogData = packet.ReadBit("HasLogData");
            var hasContentTuning = packet.ReadBit("HasContentTuning");

            if (hasContentTuning)
                V8_0_1_27101.Parsers.SpellHandler.ReadContentTuningParams(packet, "ContentTuning");

            if (hasDebugData)
                V8_0_1_27101.Parsers.CombatLogHandler.ReadSpellNonMeleeDebugData(packet, "DebugData");

            if (hasLogData)
                SpellHandler.ReadSpellCastLogData(packet, "SpellCastLogData");
        }

        [Parser(Opcode.SMSG_ATTACK_SWING_LANDED_LOG)]
        public static void HandleAttackswingLandedLog(Packet packet)
        {
            packet.ReadByte("UnkByte");
        }
    }
}

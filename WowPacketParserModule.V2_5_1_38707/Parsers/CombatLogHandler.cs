using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;
using WowPacketParser.Store;
using WowPacketParser.Store.Objects;

namespace WowPacketParserModule.V2_5_1_38835.Parsers
{
    public static class CombatLogHandler
    {
        [Parser(Opcode.SMSG_ATTACKER_STATE_UPDATE, ClientVersionBuild.V2_5_1_38598, ClientVersionBuild.V2_5_2_39570)]
        public static void HandleAttackerStateUpdateOld(Packet packet)
        {
            WowPacketParserModule.V1_13_2_31446.Parsers.CombatLogHandler.HandleAttackerStateUpdate(packet);
        }

        [Parser(Opcode.SMSG_ATTACKER_STATE_UPDATE, ClientVersionBuild.V2_5_2_39570)]
        public static void HandleAttackerStateUpdate(Packet packet)
        {
            var hasLogData = packet.ReadBit("HasLogData");

            if (hasLogData)
                V8_0_1_27101.Parsers.SpellHandler.ReadSpellCastLogData(packet);

            packet.ReadInt32("Size");

            UnitMeleeAttackLog attackData = ReadAttackRoundInfo(packet, "AttackRoundInfo");
            attackData.Time = packet.Time;
            Storage.StoreUnitAttackLog(attackData);
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

            attackData.VictimState = (uint)packet.ReadByteE<VictimStates>("VictimState", indexes);
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

        [Parser(Opcode.SMSG_SPELL_PERIODIC_AURA_LOG)]
        public static void HandleSpellPeriodicAuraLog(Packet packet)
        {
            V8_0_1_27101.Parsers.CombatLogHandler.HandleSpellPeriodicAuraLog(packet);
        }

        public static void ReadCombatLogContentTuning(Packet packet, params object[] idx)
        {
            packet.ReadByte("Type", idx);
            packet.ReadByte("TargetLevel", idx);
            packet.ReadByte("Expansion", idx);
            packet.ReadInt16("PlayerLevelDelta", idx);
            packet.ReadSingle("PlayerItemLevel", idx);
            packet.ReadSingle("TargetItemLevel", idx);
        }

        [Parser(Opcode.SMSG_SPELL_NON_MELEE_DAMAGE_LOG)]
        public static void HandleSpellNonMeleeDmgLog(Packet packet)
        {
            WowPacketParserModule.V8_0_1_27101.Parsers.CombatLogHandler.HandleSpellNonMeleeDmgLog(packet);
        }
    }
}
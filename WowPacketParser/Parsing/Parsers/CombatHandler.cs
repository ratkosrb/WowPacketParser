using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Store;
using WowPacketParser.Store.Objects;

namespace WowPacketParser.Parsing.Parsers
{
    public static class CombatHandler
    {
        [Parser(Opcode.CMSG_ATTACK_SWING)]
        public static void HandleAttackSwing(Packet packet)
        {
            packet.ReadGuid("GUID");
        }

        [Parser(Opcode.SMSG_DUEL_REQUESTED)]
        public static void HandleDuelRequested(Packet packet)
        {
            packet.ReadGuid("Flag GUID");
            packet.ReadGuid("Opponent GUID");
        }

        [Parser(Opcode.SMSG_DUEL_COMPLETE)]
        public static void HandleDuelComplete(Packet packet)
        {
            packet.ReadBool("Abnormal finish");
        }

        [Parser(Opcode.SMSG_DUEL_WINNER)]
        public static void HandleDuelWinner(Packet packet)
        {
            packet.ReadBool("Abnormal finish");
            if (ClientVersion.AddedInVersion(ClientVersionBuild.V4_2_2_14545)) // Probably earlier
            {
                packet.ReadCString("Name");
                packet.ReadCString("Opponent Name");
            }
            else
            {
                packet.ReadCString("Opponent Name");
                packet.ReadCString("Name");
            }
        }

        [Parser(Opcode.SMSG_DUEL_COUNTDOWN)]
        public static void HandleDuelCountDown(Packet packet)
        {
            packet.ReadInt32("Timer");
        }

        [Parser(Opcode.SMSG_RESET_RANGED_COMBAT_TIMER)]
        public static void HandleResetRangedCombatTimer(Packet packet)
        {
            packet.ReadInt32("Timer");
        }

        [Parser(Opcode.CMSG_TOGGLE_PVP)]
        public static void HandleTogglePvP(Packet packet)
        {
            // this opcode can be used in two ways: Either set new status explicitly or toggle old status
            if (packet.CanRead())
                packet.ReadBool("Enable");
        }

        [Parser(Opcode.SMSG_PVP_CREDIT)]
        public static void HandlePvPCredit(Packet packet)
        {
            packet.ReadUInt32("Honor");
            packet.ReadGuid("TargetGUID");
            packet.ReadInt32("Rank");
        }

        [Parser(Opcode.CMSG_SET_SHEATHED)]
        public static void HandleSetSheathed(Packet packet)
        {
            packet.ReadInt32E<SheathState>("Sheath");
        }

        [Parser(Opcode.SMSG_PARTY_KILL_LOG)]
        public static void HandlePartyKillLog(Packet packet)
        {
            packet.ReadGuid("Player GUID");
            packet.ReadGuid("Victim GUID");
        }

        [Parser(Opcode.SMSG_AI_REACTION)]
        public static void HandleAIReaction(Packet packet)
        {
            packet.ReadGuid("GUID");
            packet.ReadInt32E<AIReaction>("Reaction");
        }

        [Parser(Opcode.SMSG_UPDATE_COMBO_POINTS)]
        public static void HandleUpdateComboPoints(Packet packet)
        {
            packet.ReadPackedGuid("GUID");
            packet.ReadByte("Combo Points");
        }

        [Parser(Opcode.SMSG_ENVIRONMENTAL_DAMAGE_LOG)]
        public static void HandleEnvirenmentalDamageLog(Packet packet)
        {
            packet.ReadGuid("GUID");
            packet.ReadByteE<EnvironmentDamage>("Type");
            packet.ReadInt32("Damage");
            if (ClientVersion.AddedInVersion(1, 7, 0))
            {
                packet.ReadInt32("Absorb");
                packet.ReadInt32("Resist");
            }
        }

        [Parser(Opcode.SMSG_CANCEL_AUTO_REPEAT)]
        public static void HandleCancelAutoRepeat(Packet packet)
        {
            if (ClientVersion.AddedInVersion(ClientVersionBuild.V3_0_2_9056))
                packet.ReadPackedGuid("Target GUID");
        }

        [Parser(Opcode.SMSG_ATTACK_START)]
        public static void HandleAttackStartStart(Packet packet)
        {
            WowGuid attackerGuid = packet.ReadGuid("AttackerGUID");
            WowGuid victimGuid = packet.ReadGuid("VictimGUID");
            Storage.StoreUnitAttackToggle(attackerGuid, victimGuid, packet.Time, true);
        }

        [Parser(Opcode.SMSG_ATTACK_STOP)]
        public static void HandleAttackStartStop(Packet packet)
        {
            WowGuid attackerGuid = ClientVersion.AddedInVersion(1, 9, 0) ? packet.ReadPackedGuid("AttackerGUID") : packet.ReadGuid("AttackerGUID");
            WowGuid victimGuid = ClientVersion.AddedInVersion(1, 9, 0) ? packet.ReadPackedGuid("VictimGUID") : packet.ReadGuid("VictimGUID");
            packet.ReadInt32("NowDead"); // Blocks clientside facing when set to 1
            Storage.StoreUnitAttackToggle(attackerGuid, victimGuid, packet.Time, false);
        }

        [Parser(Opcode.SMSG_COMBAT_EVENT_FAILED)]
        public static void HandleCombatEventFailed(Packet packet)
        {
            packet.ReadPackedGuid("AttackerGUID");
            packet.ReadPackedGuid("VictimGUID");
            packet.ReadInt32("NowDead"); // Blocks clientside facing when set to 1
        }

        [Parser(Opcode.SMSG_ATTACKER_STATE_UPDATE, ClientVersionBuild.V4_0_6_13596)]
        public static void HandleAttackerStateUpdate406(Packet packet)
        {
            UnitMeleeAttackLog attackData = new UnitMeleeAttackLog();
            var hitInfo = packet.ReadInt32E<SpellHitInfo>("HitInfo");
            attackData.HitInfo = (uint)hitInfo;
            attackData.Attacker = packet.ReadPackedGuid("AttackerGUID");
            attackData.Victim = packet.ReadPackedGuid("TargetGUID");
            attackData.Damage = packet.ReadInt32("Damage");
            attackData.OverkillDamage = packet.ReadInt32("OverDamage");

            attackData.SubDamageCount = packet.ReadByte("SubDamageCount");
            for (var i = 0; i < attackData.SubDamageCount; ++i)
            {
                attackData.TotalSchoolMask |= (uint)packet.ReadInt32("SchoolMask", i);
                packet.ReadSingle("FloatDamage", i);
                packet.ReadInt32("IntDamage", i);

                if (hitInfo.HasAnyFlag(SpellHitInfo.HITINFO_PARTIAL_ABSORB | SpellHitInfo.HITINFO_FULL_ABSORB))
                    attackData.TotalAbsorbedDamage += packet.ReadInt32("DamageAbsorbed", i);

                if (hitInfo.HasAnyFlag(SpellHitInfo.HITINFO_PARTIAL_RESIST | SpellHitInfo.HITINFO_FULL_RESIST))
                    attackData.TotalResistedDamage += packet.ReadInt32("DamageResisted", i);
            }

            attackData.VictimState = (uint)packet.ReadByteE<VictimStates>("VictimState");
            attackData.AttackerState = packet.ReadInt32("AttackerState");

            attackData.SpellId = (uint)packet.ReadInt32<SpellId>("MeleeSpellID");

            if (hitInfo.HasAnyFlag(SpellHitInfo.HITINFO_BLOCK))
                attackData.BlockedDamage = packet.ReadInt32("BlockAmount");

            if (hitInfo.HasAnyFlag(SpellHitInfo.HITINFO_RAGE_GAIN))
                packet.ReadInt32("RageGained");

            if (hitInfo.HasAnyFlag(SpellHitInfo.HITINFO_DEBUG))
            {
                packet.ReadInt32("Unk Attacker State 3 1");
                packet.ReadSingle("Unk Attacker State 3 2");
                packet.ReadSingle("Unk Attacker State 3 3");
                packet.ReadSingle("Unk Attacker State 3 4");
                packet.ReadSingle("Unk Attacker State 3 5");
                packet.ReadSingle("Unk Attacker State 3 6");
                packet.ReadSingle("Unk Attacker State 3 7");
                packet.ReadSingle("Unk Attacker State 3 8");
                packet.ReadSingle("Unk Attacker State 3 9");
                packet.ReadSingle("Unk Attacker State 3 10");
                packet.ReadSingle("Unk Attacker State 3 11");
                packet.ReadInt32("Unk Attacker State 3 12");
                packet.ReadInt32("Unk Attacker State 3 13");
            }

            if (ClientVersion.AddedInVersion(ClientVersionBuild.V5_1_0_16309))
                if (hitInfo.HasAnyFlag(SpellHitInfo.HITINFO_BLOCK | SpellHitInfo.HITINFO_UNK12))
                    packet.ReadSingle("Unk Float");

            attackData.Time = packet.Time;
            Storage.StoreUnitAttackLog(attackData);
        }

        [Parser(Opcode.SMSG_ATTACKER_STATE_UPDATE, ClientVersionBuild.Zero, ClientVersionBuild.V1_10_0_5140)]
        public static void HandleAttackerStateUpdateVanilla(Packet packet)
        {
            UnitMeleeAttackLog attackData = new UnitMeleeAttackLog();
            var hitInfo = packet.ReadInt32E<SpellHitInfoVanilla>("HitInfo");
            attackData.HitInfo = (uint)hitInfo;
            attackData.Attacker = ClientVersion.AddedInVersion(1, 9, 0) ? packet.ReadPackedGuid("AttackerGUID") : packet.ReadGuid("AttackerGUID");
            attackData.Victim = ClientVersion.AddedInVersion(1, 9, 0) ? packet.ReadPackedGuid("TargetGUID") : packet.ReadGuid("TargetGUID");
            attackData.Damage = packet.ReadInt32("Damage");

            attackData.SubDamageCount = packet.ReadByte("Sub Damage Count");
            for (int i = 0; i < attackData.SubDamageCount; i++)
            {
                attackData.TotalSchoolMask |= (uint)packet.ReadInt32("SchoolMask", i);
                packet.ReadSingle("Float Damage", i);
                packet.ReadInt32("Int Damage", i);
                attackData.TotalAbsorbedDamage += packet.ReadInt32("Damage Absorbed", i);
                if (ClientVersion.AddedInVersion(1, 6, 0))
                    attackData.TotalResistedDamage += packet.ReadInt32("Damage Resisted", i);
            }

            attackData.VictimState = (uint)packet.ReadInt32E<VictimStates>("VictimState");
            attackData.AttackerState = packet.ReadInt32("AttackerState");
            if (ClientVersion.RemovedInVersion(1, 6, 0))
                packet.ReadUInt32("Spell Damage");
            attackData.SpellId = (uint)packet.ReadInt32<SpellId>("Melee Spell ID ");
            attackData.BlockedDamage = packet.ReadInt32("Block Amount");

            /*
            if (hitInfo.HasAnyFlag(SpellHitInfoVanilla.HITINFO_DEBUG))
            {
                packet.ReadUInt32("UnkDebug1");
                packet.ReadSingle("UnkDebug2");
                packet.ReadSingle("UnkDebug3");
                packet.ReadSingle("UnkDebug4");
                packet.ReadSingle("UnkDebug5");
                packet.ReadSingle("UnkDebug6");
                packet.ReadSingle("UnkDebug7");
                packet.ReadSingle("UnkDebug8");
                packet.ReadSingle("UnkDebug9");
                for (int i = 0; i < 5; ++i)
                {
                    packet.ReadSingle("UnkDebugPair1", i);
                    packet.ReadSingle("UnkDebugPair2", i);
                }
                packet.ReadUInt32("UnkDebug10");
            }
            */

            attackData.Time = packet.Time;
            Storage.StoreUnitAttackLog(attackData);
        }

        [Parser(Opcode.SMSG_ATTACKER_STATE_UPDATE, ClientVersionBuild.V1_10_0_5140, ClientVersionBuild.V4_0_6_13596)]
        public static void HandleAttackerStateUpdate(Packet packet)
        {
            UnitMeleeAttackLog attackData = new UnitMeleeAttackLog();
            var hitInfo = packet.ReadInt32E<SpellHitInfo>("HitInfo");
            attackData.HitInfo = (uint)hitInfo;
            attackData.Attacker = ClientVersion.AddedInVersion(1, 9, 0) ? packet.ReadPackedGuid("AttackerGUID") : packet.ReadGuid("AttackerGUID");
            attackData.Victim = ClientVersion.AddedInVersion(1, 9, 0) ? packet.ReadPackedGuid("TargetGUID") : packet.ReadGuid("TargetGUID");
            attackData.Damage = packet.ReadInt32("Damage");

            if (ClientVersion.AddedInVersion(ClientVersionBuild.V3_0_3_9183))
                attackData.OverkillDamage = packet.ReadInt32("OverDamage");

            attackData.SubDamageCount = packet.ReadByte("Sub Damage Count");
            for (int i = 0; i < attackData.SubDamageCount; i++)
            {
                attackData.TotalSchoolMask |= (uint)packet.ReadInt32("SchoolMask", i);
                packet.ReadSingle("Float Damage", i);
                packet.ReadInt32("Int Damage", i);

                if (ClientVersion.RemovedInVersion(ClientVersionBuild.V3_0_3_9183) ||
                    hitInfo.HasAnyFlag(SpellHitInfo.HITINFO_PARTIAL_ABSORB | SpellHitInfo.HITINFO_FULL_ABSORB))
                    attackData.TotalAbsorbedDamage += packet.ReadInt32("Damage Absorbed", i);

                if (ClientVersion.RemovedInVersion(ClientVersionBuild.V3_0_3_9183) ||
                    hitInfo.HasAnyFlag(SpellHitInfo.HITINFO_PARTIAL_RESIST | SpellHitInfo.HITINFO_FULL_RESIST))
                    attackData.TotalResistedDamage += packet.ReadInt32("Damage Resisted", i);
            }

            if (ClientVersion.AddedInVersion(ClientVersionBuild.V3_0_3_9183))
                attackData.VictimState = (uint)packet.ReadByteE<VictimStates>("VictimState");
            else
                attackData.VictimState = (uint)packet.ReadInt32E<VictimStates>("VictimState");

            attackData.AttackerState = packet.ReadInt32("AttackerState");

            attackData.SpellId = (uint)packet.ReadInt32<SpellId>("Melee Spell ID ");

            if (ClientVersion.RemovedInVersion(ClientVersionBuild.V3_0_3_9183) ||
                hitInfo.HasAnyFlag(SpellHitInfo.HITINFO_BLOCK))
                attackData.BlockedDamage = packet.ReadInt32("Block Amount");

            if (ClientVersion.AddedInVersion(ClientVersionBuild.V3_0_2_9056) &&
                hitInfo.HasAnyFlag(SpellHitInfo.HITINFO_RAGE_GAIN))
                packet.ReadInt32("Rage Gained");

            if (hitInfo.HasAnyFlag(SpellHitInfo.HITINFO_DEBUG))
            {
                packet.ReadInt32("Unk Attacker State 3 1");
                packet.ReadSingle("Unk Attacker State 3 2");
                packet.ReadSingle("Unk Attacker State 3 3");
                packet.ReadSingle("Unk Attacker State 3 4");
                packet.ReadSingle("Unk Attacker State 3 5");
                packet.ReadSingle("Unk Attacker State 3 6");
                packet.ReadSingle("Unk Attacker State 3 7");
                packet.ReadSingle("Unk Attacker State 3 8");
                packet.ReadSingle("Unk Attacker State 3 9");
                packet.ReadSingle("Unk Attacker State 3 10");
                packet.ReadSingle("Unk Attacker State 3 11");
                packet.ReadInt32("Unk Attacker State 3 12");
                packet.ReadInt32("Unk Attacker State 3 13");
                packet.ReadInt32("Unk Attacker State 3 14");
            }

            attackData.Time = packet.Time;
            Storage.StoreUnitAttackLog(attackData);
        }

        [Parser(Opcode.SMSG_DUEL_OUT_OF_BOUNDS)]
        [Parser(Opcode.SMSG_CANCEL_COMBAT)]
        [Parser(Opcode.CMSG_ATTACK_STOP)]
        [Parser(Opcode.SMSG_ATTACKSWING_NOTINRANGE)]
        [Parser(Opcode.SMSG_ATTACKSWING_BADFACING)]
        [Parser(Opcode.SMSG_ATTACKSWING_DEADTARGET)]
        [Parser(Opcode.SMSG_ATTACKSWING_CANT_ATTACK)]
        public static void HandleCombatNull(Packet packet)
        {
        }
    }
}

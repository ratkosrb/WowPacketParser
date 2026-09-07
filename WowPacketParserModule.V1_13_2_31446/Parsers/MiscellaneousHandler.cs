using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;
using WowPacketParser.Store;

namespace WowPacketParserModule.V1_13_2_31446.Parsers
{
    public static class MiscellaneousHandler
    {
        [Parser(Opcode.SMSG_FEATURE_SYSTEM_STATUS)]
        public static void HandleFeatureSystemStatus(Packet packet)
        {
            packet.ReadByte("ComplaintStatus");

            packet.ReadUInt32("ScrollOfResurrectionRequestsRemaining");
            packet.ReadUInt32("ScrollOfResurrectionMaxRequestsPerDay");
            packet.ReadUInt32("CfgRealmID");
            packet.ReadInt32("CfgRealmRecID");
            packet.ReadUInt32("TwitterPostThrottleLimit");
            packet.ReadUInt32("TwitterPostThrottleCooldown");
            packet.ReadUInt32("TokenPollTimeSeconds");

            if (ClientVersion.RemovedInVersion(ClientVersionBuild.V1_13_4_33598))
                packet.ReadUInt32E<ConsumableTokenRedeem>("TokenRedeemIndex");

            if (ClientVersion.AddedInVersion(ClientVersionBuild.V1_13_3_32790))
                packet.ReadUInt32("KioskSessionMinutes");

            packet.ReadInt64("TokenBalanceAmount");
            packet.ReadUInt32("BpayStoreProductDeliveryDelay");
            packet.ReadUInt32("ClubsPresenceUpdateTimer");
            packet.ReadUInt32("HiddenUIClubsPresenceUpdateTimer");

            packet.ResetBitReader();
            packet.ReadBit("VoiceEnabled");
            var hasEuropaTicketSystemStatus = packet.ReadBit("HasEuropaTicketSystemStatus");
            packet.ReadBit("ScrollOfResurrectionEnabled");
            packet.ReadBit("BpayStoreEnabled");
            packet.ReadBit("BpayStoreAvailable");
            packet.ReadBit("BpayStoreDisabledByParentalControls");
            packet.ReadBit("ItemRestorationButtonEnabled");
            packet.ReadBit("BrowserEnabled");
            var hasSessionAlert = packet.ReadBit("HasSessionAlert");
            packet.ReadBit("RecruitAFriendSendingEnabled");
            packet.ReadBit("CharUndeleteEnabled");
            packet.ReadBit("RestrictedAccount");
            packet.ReadBit("CommerceSystemEnabled");
            packet.ReadBit("TutorialsEnabled");
            packet.ReadBit("NPETutorialsEnabled");
            packet.ReadBit("TwitterEnabled");
            packet.ReadBit("Unk67");
            packet.ReadBit("WillKickFromWorld");
            packet.ReadBit("KioskModeEnabled");
            packet.ReadBit("CompetitiveModeEnabled");
            var hasRaceClassExpansionLevels = packet.ReadBit("RaceClassExpansionLevels");
            packet.ReadBit("TokenBalanceEnabled");
            packet.ReadBit("WarModeFeatureEnabled");
            packet.ReadBit("ClubsEnabled");
            packet.ReadBit("ClubsBattleNetClubTypeAllowed");
            packet.ReadBit("ClubsCharacterClubTypeAllowed");
            packet.ReadBit("ClubsPresenceUpdateEnabled");
            packet.ReadBit("VoiceChatDisabledByParentalControl");
            packet.ReadBit("VoiceChatMutedByParentalControl");

            {
                packet.ResetBitReader();
                packet.ReadBit("ToastsDisabled");
                packet.ReadSingle("ToastDuration");
                packet.ReadSingle("DelayDuration");
                packet.ReadSingle("QueueMultiplier");
                packet.ReadSingle("PlayerMultiplier");
                packet.ReadSingle("PlayerFriendValue");
                packet.ReadSingle("PlayerGuildValue");
                packet.ReadSingle("ThrottleInitialThreshold");
                packet.ReadSingle("ThrottleDecayTime");
                packet.ReadSingle("ThrottlePrioritySpike");
                packet.ReadSingle("ThrottleMinThreshold");
                packet.ReadSingle("ThrottlePvPPriorityNormal");
                packet.ReadSingle("ThrottlePvPPriorityLow");
                packet.ReadSingle("ThrottlePvPHonorThreshold");
                packet.ReadSingle("ThrottleLfgListPriorityDefault");
                packet.ReadSingle("ThrottleLfgListPriorityAbove");
                packet.ReadSingle("ThrottleLfgListPriorityBelow");
                packet.ReadSingle("ThrottleLfgListIlvlScalingAbove");
                packet.ReadSingle("ThrottleLfgListIlvlScalingBelow");
                packet.ReadSingle("ThrottleRfPriorityAbove");
                packet.ReadSingle("ThrottleRfIlvlScalingAbove");
                packet.ReadSingle("ThrottleDfMaxItemLevel");
                packet.ReadSingle("ThrottleDfBestPriority");
            }

            if (hasSessionAlert)
                V6_0_2_19033.Parsers.MiscellaneousHandler.ReadClientSessionAlertConfig(packet, "SessionAlert");

            if (hasRaceClassExpansionLevels)
            {
                var int88 = packet.ReadUInt32("RaceClassExpansionLevelsCount");
                for (int i = 0; i < int88; i++)
                    packet.ReadByte("RaceClassExpansionLevels", i);
            }

            packet.ResetBitReader();
            V8_0_1_27101.Parsers.MiscellaneousHandler.ReadVoiceChatManagerSettings(packet, "VoiceChatManagerSettings");

            if (hasEuropaTicketSystemStatus)
            {
                packet.ResetBitReader();
                V6_0_2_19033.Parsers.MiscellaneousHandler.ReadCliEuropaTicketConfig(packet, "EuropaTicketSystemStatus");
            }
        }

        [Parser(Opcode.SMSG_FEATURE_SYSTEM_STATUS_GLUE_SCREEN)]
        public static void HandleFeatureSystemStatusGlueScreen(Packet packet)
        {
            packet.ReadBit("BpayStoreEnabled");
            packet.ReadBit("BpayStoreAvailable");
            packet.ReadBit("BpayStoreDisabledByParentalControls");
            packet.ReadBit("CharUndeleteEnabled");
            packet.ReadBit("CommerceSystemEnabled");
            packet.ReadBit("Unk14");
            packet.ReadBit("WillKickFromWorld");
            packet.ReadBit("IsExpansionPreorderInStore");
            packet.ReadBit("KioskModeEnabled");
            packet.ReadBit("IsCompetitiveModeEnabled");

            if (ClientVersion.RemovedInVersion(ClientVersionBuild.V1_13_4_33598))
                packet.ReadBit("NoHandler"); // not accessed in handler

            packet.ReadBit("TrialBoostEnabled");
            packet.ReadBit("TokenBalanceEnabled");
            packet.ReadBit("LiveRegionCharacterListEnabled");
            packet.ReadBit("LiveRegionCharacterCopyEnabled");
            packet.ReadBit("LiveRegionAccountCopyEnabled");
            packet.ReadBit("NameReservationEnabled");
            var hasLaunchETA = packet.ReadBit("HasLaunchETA");

            packet.ReadUInt32("TokenPollTimeSeconds");

            if (ClientVersion.RemovedInVersion(ClientVersionBuild.V1_13_4_33598))
                packet.ReadUInt32E<ConsumableTokenRedeem>("TokenRedeemIndex");

            if (ClientVersion.AddedInVersion(ClientVersionBuild.V1_13_3_32790))
                packet.ReadUInt32("KioskSessionMinutes");

            packet.ReadInt64("TokenBalanceAmount");
            packet.ReadInt32("MaxCharactersPerRealm");

            var liveRegionCopyCount = 0;
            if (ClientVersion.AddedInVersion(ClientVersionBuild.V1_13_4_33598))
                liveRegionCopyCount = packet.ReadInt32("LiveRegionCopySourceRegionCount");

            packet.ReadUInt32("BpayStoreProductDeliveryDelay");
            packet.ReadInt32("ActiveCharacterUpgradeBoostType");
            packet.ReadInt32("ActiveClassTrialBoostType");
            packet.ReadInt32("MinimumExpansionLevel");
            packet.ReadInt32("MaximumExpansionLevel");

            if (hasLaunchETA)
                packet.ReadInt32("LaunchETA");

            for (var i = 0; i < liveRegionCopyCount; ++i)
                packet.ReadInt32("RegionID");
        }

        [Parser(Opcode.CMSG_TUTORIAL_FLAG)]
        public static void HandleTutorialFlag620(Packet packet)
        {
            var action = packet.ReadBitsE<TutorialAction>("TutorialAction", 2);

            if (action == TutorialAction.Update)
                packet.ReadInt32E<Tutorial>("TutorialBit");
        }

        [Parser(Opcode.CMSG_REPORT_KEYBINDING_EXECUTION_COUNTS)]
        public static void HandleReportKeybindingExecutionCounts(Packet packet)
        {
            var count = packet.ReadBits("KeyBindingsCount", 10);
            packet.ResetBitReader();

            for (var i = 0; i < count; i++)
            {
                var len1 = packet.ReadBits(6);
                var len2 = packet.ReadBits(6);
                packet.ResetBitReader();
                packet.ReadUInt32("ExecutionCount", i);
                packet.ReadWoWString("Key", len1, i);
                packet.ReadWoWString("Action", len2, i);
            }
        }

        [Parser(Opcode.CMSG_WHO)]
        public static void HandleWhoRequest(Packet packet)
        {
            var areaCount = packet.ReadBits(4);

            packet.ReadInt32("MinLevel");
            packet.ReadInt32("MaxLevel");
            packet.ReadInt64("RaceFilter");
            packet.ReadInt32("ClassFilter");

            packet.ResetBitReader();

            var nameLen = packet.ReadBits(6);
            var virtualRealmNameLen = packet.ReadBits(9);
            var guildLen = packet.ReadBits(7);
            var guildVirtualRealmNameLen = packet.ReadBits(9);
            var wordCount = packet.ReadBits(3);

            packet.ReadBit("ShowEnemies");
            packet.ReadBit("ShowArenaPlayers");
            packet.ReadBit("ExactName");
            var hasServerInfo = packet.ReadBit("HasServerInfo");
            packet.ResetBitReader();

            for (var i = 0; i < wordCount; ++i)
            {
                var bits0 = packet.ReadBits(7);
                packet.ReadWoWString("Word", bits0, i);
                packet.ResetBitReader();
            }

            packet.ReadWoWString("Name", nameLen);
            packet.ReadWoWString("VirtualRealmName", virtualRealmNameLen);
            packet.ReadWoWString("Guild", guildLen);
            packet.ReadWoWString("GuildVirtualRealmName", guildVirtualRealmNameLen);

            // WhoRequestServerInfo
            if (hasServerInfo)
            {
                packet.ReadInt32("FactionGroup");
                packet.ReadInt32("Locale");
                packet.ReadInt32("RequesterVirtualRealmAddress");
            }

            if (ClientVersion.AddedInClassicVersion(1, 14, 0, 2, 5, 1))
                packet.ReadUInt32("RequestID");

            for (var i = 0; i < areaCount; ++i)
                packet.ReadUInt32<AreaId>("Area", i);
        }

        [Parser(Opcode.SMSG_SUMMON_REQUEST)]
        public static void HandleSummonRequest(Packet packet)
        {
            packet.ReadPackedGuid128("SummonerGUID");
            packet.ReadUInt32("SummonerVirtualRealmAddress");
            packet.ReadInt32<AreaId>("AreaID");
            packet.ReadByte("Reason");
            packet.ResetBitReader();
            packet.ReadBit("SkipStartingArea");
        }
    }
}

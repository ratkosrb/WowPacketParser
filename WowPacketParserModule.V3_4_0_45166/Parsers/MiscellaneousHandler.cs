using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;
using CoreParsers = WowPacketParser.Parsing.Parsers;

namespace WowPacketParserModule.V3_4_0_45166.Parsers
{
    public static class MiscellaneousHandler
    {
        public static void ReadGameRuleValuePair(Packet packet, params object[] indexes)
        {
            packet.ReadInt32("Rule", indexes);
            packet.ReadInt32("Value", indexes);
        }

        [Parser(Opcode.SMSG_FEATURE_SYSTEM_STATUS_GLUE_SCREEN, ClientVersionBuild.V3_4_0_44832, ClientVersionBuild.V3_4_3_51505)]
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
            packet.ReadBit("TrialBoostEnabled");
            packet.ReadBit("TokenBalanceEnabled");
            packet.ReadBit("LiveRegionCharacterListEnabled");
            packet.ReadBit("LiveRegionCharacterCopyEnabled");
            packet.ReadBit("LiveRegionAccountCopyEnabled");
            packet.ReadBit("LiveRegionKeyBindingsCopyEnabled");

            packet.ReadBit("UnusedBit1_340");
            packet.ReadBit("UnusedBit2_340");
            var europaTicket = packet.ReadBit("IsEuropaTicketSystemStatusEnabled");
            packet.ReadBit("Unknown901CheckoutRelated");
            bool launchETA = packet.ReadBit("IsLaunchETA");
            packet.ReadBit("TBCInfoPaneEnabled");
            packet.ReadBit("TBCInfoPanePriceEnabled");
            packet.ReadBit("TBCTransitionUIEnabled");

            packet.ReadBit("SoMNotificationEnabled");
            packet.ResetBitReader();

            if (europaTicket)
                V6_0_2_19033.Parsers.MiscellaneousHandler.ReadCliEuropaTicketConfig(packet, "EuropaTicketSystemStatus");

            packet.ReadUInt32("TokenPollTimeSeconds");
            packet.ReadUInt32("KioskSessionMinutes");
            packet.ReadInt64("TokenBalanceAmount");
            packet.ReadInt32("MaxCharactersPerRealm");
            var liveRegionCharacterCopySourceRegionsCount = packet.ReadUInt32("LiveRegionCharacterCopySourceRegionsCount");
            packet.ReadUInt32("BpayStoreProductDeliveryDelay");
            packet.ReadInt32("ActiveCharacterUpgradeBoostType");
            packet.ReadInt32("ActiveClassTrialBoostType");
            packet.ReadInt32("MinimumExpansionLevel");
            packet.ReadInt32("MaximumExpansionLevel");
            packet.ReadInt32("ActiveSeason");
            var gameRuleValuesCount = packet.ReadUInt32("GameRuleValuesCount");
            packet.ReadInt16("MaxPlayerNameQueriesPerPacket");
            packet.ReadInt16("PlayerNameQueryTelemetryInterval");

            if (ClientVersion.AddedInVersion(ClientVersionBuild.V3_4_1_47720))
                packet.ReadUInt32("PlayerNameQueryInterval");

            if (launchETA)
                packet.ReadPackedTime("LaunchETA");

            for (int i = 0; i < liveRegionCharacterCopySourceRegionsCount; i++)
                packet.ReadUInt32("LiveRegionCharacterCopySourceRegion", i);

            for (var i = 0; i < gameRuleValuesCount; ++i)
                ReadGameRuleValuePair(packet, "GameRuleValues");
        }

        [Parser(Opcode.SMSG_FEATURE_SYSTEM_STATUS_GLUE_SCREEN, ClientVersionBuild.V3_4_3_51505)]
        public static void HandleFeatureSystemStatusGlueScreen343(Packet packet)
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
            packet.ReadBit("TrialBoostEnabled");
            packet.ReadBit("Unk");
            packet.ReadBit("TokenBalanceEnabled");
            packet.ReadBit("LiveRegionCharacterListEnabled");
            packet.ReadBit("LiveRegionCharacterCopyEnabled");
            packet.ReadBit("LiveRegionAccountCopyEnabled");

            packet.ReadBit("LiveRegionKeyBindingsCopyEnabled");
            packet.ReadBit("Unknown901CheckoutRelated");
            packet.ReadBit("Unused");
            var europaTicket = packet.ReadBit("IsEuropaTicketSystemStatusEnabled");
            packet.ReadBit("NameReservationOnly");
            bool launchETA = packet.ReadBit("IsLaunchETA");
            packet.ReadBit("AddonsDisabled");
            packet.ReadBit("Unk");

            packet.ReadBit("Unk");
            packet.ReadBit("SoMNotificationEnabled");
            packet.ReadBit("AccountSaveDataExportEnabled");
            packet.ReadBit("AccountLockedByExport");
            packet.ReadBit("Unk");
            bool realmHiddenAlert = packet.ReadBit("IsRealmHiddenAlert");

            if (realmHiddenAlert)
                packet.ReadBits("RealmHiddenAlert", 11);

            packet.ResetBitReader();

            if (europaTicket)
                V6_0_2_19033.Parsers.MiscellaneousHandler.ReadCliEuropaTicketConfig(packet, "EuropaTicketSystemStatus");

            packet.ReadUInt32("TokenPollTimeSeconds");
            packet.ReadUInt32("KioskSessionMinutes");
            packet.ReadInt64("TokenBalanceAmount");
            packet.ReadInt32("MaxCharactersPerRealm");
            var liveRegionCharacterCopySourceRegionsCount = packet.ReadUInt32("LiveRegionCharacterCopySourceRegionsCount");
            packet.ReadUInt32("BpayStoreProductDeliveryDelay");
            packet.ReadInt32("ActiveCharacterUpgradeBoostType");
            packet.ReadInt32("ActiveClassTrialBoostType");
            packet.ReadInt32("MinimumExpansionLevel");
            packet.ReadInt32("MaximumExpansionLevel");
            packet.ReadInt32("ActiveSeason");
            var gameRuleValuesCount = packet.ReadUInt32("GameRuleValuesCount");
            packet.ReadInt16("MaxPlayerNameQueriesPerPacket");
            packet.ReadInt16("PlayerNameQueryTelemetryInterval");

            packet.ReadInt32("PlayerNameQueryInterval");
            var debugTimeEventsCount = packet.ReadInt32("DebugTimeEventsSize");
            packet.ReadInt32("Unused1007");

            if (launchETA)
                packet.ReadPackedTime("LaunchETA");

            for (int i = 0; i < liveRegionCharacterCopySourceRegionsCount; i++)
                packet.ReadUInt32("LiveRegionCharacterCopySourceRegion", i);

            for (var i = 0; i < gameRuleValuesCount; ++i)
                ReadGameRuleValuePair(packet, "GameRuleValues");

            for (int i = 0; i < debugTimeEventsCount; i++)
            {
                packet.ReadUInt32("TimeEvent", i);
                var textlength = packet.ReadBits("TextLength", 7, i);
                packet.ResetBitReader();

                packet.ReadWoWString("Text", textlength, i);
            }
        }

        [Parser(Opcode.SMSG_TRIGGER_CINEMATIC)]
        [Parser(Opcode.SMSG_TRIGGER_MOVIE)]
        public static void HandleTriggerSequence(Packet packet)
        {
            packet.ReadInt32("CinematicID");
        }

        [Parser(Opcode.SMSG_FEATURE_SYSTEM_STATUS, ClientVersionBuild.V3_4_0_44832, ClientVersionBuild.V3_4_3_51505)]
        public static void HandleFeatureSystemStatus(Packet packet)
        {
            packet.ReadByte("ComplaintStatus");

            packet.ReadUInt32("ScrollOfResurrectionRequestsRemaining");
            packet.ReadUInt32("ScrollOfResurrectionMaxRequestsPerDay");
            packet.ReadUInt32("CfgRealmID");
            packet.ReadInt32("CfgRealmRecID");
            packet.ReadUInt32("MaxRecruits", "RAFSystem");
            packet.ReadUInt32("MaxRecruitMonths", "RAFSystem");
            packet.ReadUInt32("MaxRecruitmentUses", "RAFSystem");
            packet.ReadUInt32("DaysInCycle", "RAFSystem");
            packet.ReadUInt32("TwitterPostThrottleLimit");
            packet.ReadUInt32("TwitterPostThrottleCooldown");
            packet.ReadUInt32("TokenPollTimeSeconds");
            packet.ReadUInt32("KioskSessionMinutes");
            packet.ReadInt64("TokenBalanceAmount");
            packet.ReadUInt32("BpayStoreProductDeliveryDelay");
            packet.ReadUInt32("ClubsPresenceUpdateTimer");
            packet.ReadUInt32("HiddenUIClubsPresenceUpdateTimer");

            packet.ReadInt32("ActiveSeason");
            var gameRuleValuesCount = packet.ReadUInt32("GameRuleValuesCount");
            packet.ReadInt16("MaxPlayerNameQueriesPerPacket");
            packet.ReadInt16("PlayerNameQueryTelemetryInterval");

            if (ClientVersion.AddedInVersion(ClientVersionBuild.V3_4_1_47720))
                packet.ReadUInt32("PlayerNameQueryInterval");

            for (var i = 0; i < gameRuleValuesCount; ++i)
                ReadGameRuleValuePair(packet, "GameRuleValues");

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
            packet.ReadBit("Enabled", "RAFSystem");
            packet.ReadBit("RecruitingEnabled", "RAFSystem");
            packet.ReadBit("CharUndeleteEnabled");
            packet.ReadBit("RestrictedAccount");
            packet.ReadBit("CommerceSystemEnabled");
            packet.ReadBit("TutorialsEnabled");
            packet.ReadBit("TwitterEnabled");

            packet.ReadBit("Unk67");
            packet.ReadBit("WillKickFromWorld");
            packet.ReadBit("KioskModeEnabled");
            packet.ReadBit("CompetitiveModeEnabled");
            packet.ReadBit("TokenBalanceEnabled");
            packet.ReadBit("WarModeFeatureEnabled");
            packet.ReadBit("ClubsEnabled");
            packet.ReadBit("ClubsBattleNetClubTypeAllowed");

            packet.ReadBit("ClubsCharacterClubTypeAllowed");
            packet.ReadBit("ClubsPresenceUpdateEnabled");
            packet.ReadBit("VoiceChatDisabledByParentalControl");
            packet.ReadBit("VoiceChatMutedByParentalControl");
            packet.ReadBit("QuestSessionEnabled");
            packet.ReadBit("IsMuted");
            packet.ReadBit("ClubFinderEnabled");
            packet.ReadBit("Unknown901CheckoutRelated");

            packet.ReadBit("TextToSpeechFeatureEnabled");
            packet.ReadBit("ChatDisabledByDefault");
            packet.ReadBit("ChatDisabledByPlayer");
            packet.ReadBit("LFGListCustomRequiresAuthenticator");

            if (ClientVersion.AddedInVersion(ClientVersionBuild.V3_4_1_47720))
                packet.ReadBit("AddonsDisabled");
            packet.ReadBit("WarGamesEnabled");
            var unknown = packet.ReadBit("Unk340");

            {
                packet.ResetBitReader();
                packet.ReadBit("ToastsDisabled", "QuickJoinConfig");
                packet.ReadSingle("ToastDuration", "QuickJoinConfig");
                packet.ReadSingle("DelayDuration", "QuickJoinConfig");
                packet.ReadSingle("QueueMultiplier", "QuickJoinConfig");
                packet.ReadSingle("PlayerMultiplier", "QuickJoinConfig");
                packet.ReadSingle("PlayerFriendValue", "QuickJoinConfig");
                packet.ReadSingle("PlayerGuildValue", "QuickJoinConfig");
                packet.ReadSingle("ThrottleInitialThreshold", "QuickJoinConfig");
                packet.ReadSingle("ThrottleDecayTime", "QuickJoinConfig");
                packet.ReadSingle("ThrottlePrioritySpike", "QuickJoinConfig");
                packet.ReadSingle("ThrottleMinThreshold", "QuickJoinConfig");
                packet.ReadSingle("ThrottlePvPPriorityNormal", "QuickJoinConfig");
                packet.ReadSingle("ThrottlePvPPriorityLow", "QuickJoinConfig");
                packet.ReadSingle("ThrottlePvPHonorThreshold", "QuickJoinConfig");
                packet.ReadSingle("ThrottleLfgListPriorityDefault", "QuickJoinConfig");
                packet.ReadSingle("ThrottleLfgListPriorityAbove", "QuickJoinConfig");
                packet.ReadSingle("ThrottleLfgListPriorityBelow", "QuickJoinConfig");
                packet.ReadSingle("ThrottleLfgListIlvlScalingAbove", "QuickJoinConfig");
                packet.ReadSingle("ThrottleLfgListIlvlScalingBelow", "QuickJoinConfig");
                packet.ReadSingle("ThrottleRfPriorityAbove", "QuickJoinConfig");
                packet.ReadSingle("ThrottleRfIlvlScalingAbove", "QuickJoinConfig");
                packet.ReadSingle("ThrottleDfMaxItemLevel", "QuickJoinConfig");
                packet.ReadSingle("ThrottleDfBestPriority", "QuickJoinConfig");
            }

            if (hasSessionAlert)
                V6_0_2_19033.Parsers.MiscellaneousHandler.ReadClientSessionAlertConfig(packet, "SessionAlert");

            if (unknown)
            {
                var count = packet.ReadUInt32("Count", "Unk340");
                for (int i = 0; i < count; i++)
                    packet.ReadByte("UnknownByte", "Unk340", i);
            }

            packet.ResetBitReader();
            V8_0_1_27101.Parsers.MiscellaneousHandler.ReadVoiceChatManagerSettings(packet, "VoiceChatManagerSettings");

            if (hasEuropaTicketSystemStatus)
            {
                packet.ResetBitReader();
                V6_0_2_19033.Parsers.MiscellaneousHandler.ReadCliEuropaTicketConfig(packet, "EuropaTicketSystemStatus");
            }
        }

        [Parser(Opcode.SMSG_FEATURE_SYSTEM_STATUS, ClientVersionBuild.V3_4_3_51505)]
        public static void HandleFeatureSystemStatus343(Packet packet)
        {
            packet.ReadByte("ComplaintStatus");
            packet.ReadUInt32("CfgRealmID");
            packet.ReadInt32("CfgRealmRecID");

            packet.ReadUInt32("MaxRecruits", "RAFSystem");
            packet.ReadUInt32("MaxRecruitMonths", "RAFSystem");
            packet.ReadUInt32("MaxRecruitmentUses", "RAFSystem");
            packet.ReadUInt32("DaysInCycle", "RAFSystem");
            packet.ReadUInt32("Unknown1007", "RAFSystem");
            packet.ReadUInt32("TokenPollTimeSeconds");
            packet.ReadUInt32("KioskSessionMinutes");
            packet.ReadInt64("TokenBalanceAmount");
            packet.ReadUInt32("BpayStoreProductDeliveryDelay");
            packet.ReadUInt32("ClubsPresenceUpdateTimer");
            packet.ReadUInt32("HiddenUIClubsPresenceUpdateTimer");
            packet.ReadInt32("ActiveSeason");
            
            var gameRuleValuesCount = packet.ReadUInt32("GameRuleValuesCount");
            
            packet.ReadInt16("MaxPlayerNameQueriesPerPacket");
            packet.ReadInt16("PlayerNameQueryTelemetryInterval");
            packet.ReadUInt32("PlayerNameQueryInterval");
            
            for (var i = 0; i < gameRuleValuesCount; ++i)
                ReadGameRuleValuePair(packet, "GameRuleValues");

            packet.ResetBitReader();
            packet.ReadBit("VoiceEnabled");
            var hasEuropaTicketSystemStatus = packet.ReadBit("HasEuropaTicketSystemStatus");
            packet.ReadBit("BpayStoreEnabled");
            packet.ReadBit("BpayStoreAvailable");
            packet.ReadBit("BpayStoreDisabledByParentalControls");
            packet.ReadBit("ItemRestorationButtonEnabled");
            packet.ReadBit("BrowserEnabled");
            var hasSessionAlert = packet.ReadBit("HasSessionAlert");

            packet.ReadBit("Enabled", "RAFSystem");
            packet.ReadBit("RecruitingEnabled", "RAFSystem");
            packet.ReadBit("CharUndeleteEnabled");
            packet.ReadBit("RestrictedAccount");
            packet.ReadBit("CommerceSystemEnabled");
            packet.ReadBit("TutorialsEnabled");
            packet.ReadBit("Unk67");
            packet.ReadBit("WillKickFromWorld");

            packet.ReadBit("KioskModeEnabled");
            packet.ReadBit("CompetitiveModeEnabled");
            packet.ReadBit("TokenBalanceEnabled");
            packet.ReadBit("WarModeFeatureEnabled");
            packet.ReadBit("ClubsEnabled");
            packet.ReadBit("ClubsBattleNetClubTypeAllowed");
            packet.ReadBit("ClubsCharacterClubTypeAllowed");
            packet.ReadBit("ClubsPresenceUpdateEnabled");

            packet.ReadBit("VoiceChatDisabledByParentalControl");
            packet.ReadBit("VoiceChatMutedByParentalControl");
            packet.ReadBit("QuestSessionEnabled");
            packet.ReadBit("IsMuted");
            packet.ReadBit("ClubFinderEnabled");
            packet.ReadBit("Unknown901CheckoutRelated");
            packet.ReadBit("TextToSpeechFeatureEnabled");
            packet.ReadBit("ChatDisabledByDefault");

            packet.ReadBit("ChatDisabledByPlayer");
            packet.ReadBit("LFGListCustomRequiresAuthenticator");
            packet.ReadBit("AddonsDisabled");
            packet.ReadBit("WarGamesEnabled");
            var unk = packet.ReadBit("Unk343_1");
            packet.ReadBit("Unk343_2");
            packet.ReadBit("ContentTrackingEnabled");
            packet.ReadBit("IsSellAllJunkEnabled");

            packet.ReadBit("IsGroupFinderEnabled");
            packet.ReadBit("IsLFDEnabled");
            packet.ReadBit("IsPremadeGroupEnabled");
            packet.ReadBit("IsLFREnabled");

            {
                packet.ResetBitReader();
                packet.ReadBit("ToastsDisabled", "QuickJoinConfig");
                packet.ReadSingle("ToastDuration", "QuickJoinConfig");
                packet.ReadSingle("DelayDuration", "QuickJoinConfig");
                packet.ReadSingle("QueueMultiplier", "QuickJoinConfig");
                packet.ReadSingle("PlayerMultiplier", "QuickJoinConfig");
                packet.ReadSingle("PlayerFriendValue", "QuickJoinConfig");
                packet.ReadSingle("PlayerGuildValue", "QuickJoinConfig");
                packet.ReadSingle("ThrottleInitialThreshold", "QuickJoinConfig");
                packet.ReadSingle("ThrottleDecayTime", "QuickJoinConfig");
                packet.ReadSingle("ThrottlePrioritySpike", "QuickJoinConfig");
                packet.ReadSingle("ThrottleMinThreshold", "QuickJoinConfig");
                packet.ReadSingle("ThrottlePvPPriorityNormal", "QuickJoinConfig");
                packet.ReadSingle("ThrottlePvPPriorityLow", "QuickJoinConfig");
                packet.ReadSingle("ThrottlePvPHonorThreshold", "QuickJoinConfig");
                packet.ReadSingle("ThrottleLfgListPriorityDefault", "QuickJoinConfig");
                packet.ReadSingle("ThrottleLfgListPriorityAbove", "QuickJoinConfig");
                packet.ReadSingle("ThrottleLfgListPriorityBelow", "QuickJoinConfig");
                packet.ReadSingle("ThrottleLfgListIlvlScalingAbove", "QuickJoinConfig");
                packet.ReadSingle("ThrottleLfgListIlvlScalingBelow", "QuickJoinConfig");
                packet.ReadSingle("ThrottleRfPriorityAbove", "QuickJoinConfig");
                packet.ReadSingle("ThrottleRfIlvlScalingAbove", "QuickJoinConfig");
                packet.ReadSingle("ThrottleDfMaxItemLevel", "QuickJoinConfig");
                packet.ReadSingle("ThrottleDfBestPriority", "QuickJoinConfig");
            }

            if (hasSessionAlert)
                V6_0_2_19033.Parsers.MiscellaneousHandler.ReadClientSessionAlertConfig(packet, "SessionAlert");

            if (unk)
            {
                var count = packet.ReadUInt32("UnkCount");

                for (var i = 0; i < count; ++i)
                    packet.ReadByte("UnkByte", i);
            }

            V8_0_1_27101.Parsers.MiscellaneousHandler.ReadVoiceChatManagerSettings(packet, "VoiceChatManagerSettings");

            if (hasEuropaTicketSystemStatus)
            {
                packet.ResetBitReader();
                V6_0_2_19033.Parsers.MiscellaneousHandler.ReadCliEuropaTicketConfig(packet, "EuropaTicketSystemStatus");
            }
        }

        [Parser(Opcode.SMSG_WORLD_SERVER_INFO, ClientVersionBuild.V3_4_3_51505)]
        public static void HandleWorldServerInfo(Packet packet)
        {
            CoreParsers.MovementHandler.CurrentDifficultyID = packet.ReadUInt32<DifficultyId>("DifficultyID");

            packet.ReadBit("IsTournamentRealm");
            packet.ReadBit("XRealmPvpAlert");
            var hasRestrictedAccountMaxLevel = packet.ReadBit("HasRestrictedAccountMaxLevel");
            var hasRestrictedAccountMaxMoney = packet.ReadBit("HasRestrictedAccountMaxMoney");
            var hasInstanceGroupSize = packet.ReadBit("HasInstanceGroupSize");

            if (hasRestrictedAccountMaxLevel)
                packet.ReadUInt32("RestrictedAccountMaxLevel");

            if (hasRestrictedAccountMaxMoney)
                packet.ReadUInt64("RestrictedAccountMaxMoney");

            if (hasInstanceGroupSize)
                packet.ReadUInt32("InstanceGroupSize");
        }

        [Parser(Opcode.SMSG_GAME_TIME_SET)]
        public static void HandleGametimeSet(Packet packet)
        {
            // client just reads 16 bytes, wtf are they for?
            packet.ReadBytes("Unk", 16);
        }

        public static void ReadUIEventToast(Packet packet, params object[] args)
        {
            packet.ReadInt32("UiEventToastID", args);
            packet.ReadInt32("Asset", args);
        }

        [Parser(Opcode.SMSG_SET_CURRENCY, ClientVersionBuild.V3_4_1_47720)]
        public static void HandleSetCurrency(Packet packet)
        {
            packet.ReadInt32("Type");
            packet.ReadInt32("Quantity");
            packet.ReadUInt32("Flags");
            uint toastCount = packet.ReadUInt32("UiEventToastCount");
            for (var i = 0; i < toastCount; i++)
                ReadUIEventToast(packet, "UiEventToast", i);

            var hasWeeklyQuantity = packet.ReadBit("HasWeeklyQuantity");
            var hasTrackedQuantity = packet.ReadBit("HasTrackedQuantity");
            var hasMaxQuantity = packet.ReadBit("HasMaxQuantity");
            var hasTotalEarned = packet.ReadBit("HasTotalEarned");
            packet.ReadBit("SuppressChatLog");
            var hasQuantityChange = packet.ReadBit("HasQuantityChange");
            var hasQuantityLostSource = packet.ReadBit("HasQuantityLostSource");
            var hasQuantityGainSource = packet.ReadBit("HasQuantityGainSource");
            var hasFirstCraftOperationID = packet.ReadBit("HasFirstCraftOperationID");
            var hasHasNextRechargeTime = packet.ReadBit("HasNextRechargeTime");
            var hasRechargeCycleStartTime = false;
            if (ClientVersion.AddedInVersion(10, 1, 0, 1, 15, 0, 3, 4, 2))
                hasRechargeCycleStartTime = packet.ReadBit("HasRechargeCycleStartTime");

            if (hasWeeklyQuantity)
                packet.ReadInt32("WeeklyQuantity");

            if (hasTrackedQuantity)
                packet.ReadInt32("TrackedQuantity");

            if (hasMaxQuantity)
                packet.ReadInt32("MaxQuantity");

            if (hasTotalEarned)
                packet.ReadInt32("TotalEarned");

            if (hasQuantityChange)
                packet.ReadInt32("QuantityChange");

            if (hasQuantityLostSource)
                packet.ReadInt32("QuantityLostSource");

            if (hasQuantityGainSource)
                packet.ReadInt32("QuantityGainSource");

            if (hasFirstCraftOperationID)
                packet.ReadUInt32("FirstCraftOperationID");

            if (hasHasNextRechargeTime)
                packet.ReadTime64("NextRechargeTime");

            if (hasRechargeCycleStartTime)
                packet.ReadTime64("RechargeCycleStartTime");
        }

        [Parser(Opcode.SMSG_SUMMON_REQUEST)]
        public static void HandleSummonRequest(Packet packet)
        {
            packet.ReadPackedGuid128("SummonerGUID");
            packet.ReadUInt32("SummonerVirtualRealmAddress");
            packet.ReadInt32<AreaId>("AreaID");
            packet.ReadByte("Unknown");
            packet.ReadBit("ConfirmSummon_NC");
        }

        [Parser(Opcode.SMSG_ENCOUNTER_END)]
        public static void HandleEncounterStop(Packet packet)
        {
            packet.ReadInt32("EncounterID");
            packet.ReadInt32<DifficultyId>("DifficultyID");
            packet.ReadInt32("GroupSize");
            packet.ReadUInt32("Unknown");
            packet.ReadBit("Success");
        }

        [Parser(Opcode.SMSG_ACCOUNT_TOY_UPDATE, ClientVersionBuild.V3_4_0_44463, ClientVersionBuild.V3_4_0_45166)]
        public static void HandleAccountToyUpdate(Packet packet)
        {
            packet.ReadBit("IsFullUpdate");

            var itemIdCount = packet.ReadUInt32("ToyItemIDsCount");
            var isFavoriteCount = packet.ReadUInt32("ToyIsFavoriteCount");
            var fanfareCount = packet.ReadUInt32("FanfareCount");

            for (var i = 0; i < itemIdCount; i++)
                packet.ReadInt32("ToyItemID", i);

            packet.ResetBitReader();

            for (var i = 0; i < isFavoriteCount; i++)
                packet.ReadBit("ToyIsFavorite", i);

            for (var i = 0; i < fanfareCount; i++)
                packet.ReadBit("HasFanfare", i);
        }

        [Parser(Opcode.SMSG_ACCOUNT_TRANSMOG_UPDATE, ClientVersionBuild.V3_4_0_44463, ClientVersionBuild.V3_4_0_45166)]
        public static void HandleAccountTransmogUpdate(Packet packet)
        {
            packet.ReadBit("IsFullUpdate");
            packet.ReadBit("IsSetFavorite");
            var count = packet.ReadUInt32("FavoriteAppearancesCount");
            var newCount = packet.ReadUInt32("NewAppearancesCount");
            for (var i = 0; i < count; i++)
                packet.ReadUInt32("ItemModifiedAppearanceId", "FavoriteAppearances", i);
            for (var i = 0; i < newCount; i++)
                packet.ReadUInt32("ItemModifiedAppearanceId", "NewAppearances", i);
        }

        [Parser(Opcode.CMSG_LFG_LIST_SET_ROLES)]
        public static void HandleLfgListSetRoles(Packet packet)
        {
            packet.ReadByteE<LfgRoleFlag>("RolesDesired");
        }

        [Parser(Opcode.SMSG_SOCIAL_CONTRACT_REQUEST_RESPONSE)]
        public static void HandleSocialContractRequestResponse(Packet packet)
        {
            packet.ReadBit("ShowSocialContract");
        }

        [Parser(Opcode.SMSG_TALENT_GROUP_ROLE_CHANGED)]
        public static void HandleTalentGroupRoleChanged(Packet packet)
        {
            packet.ReadByte("TalentGroup");
            packet.ReadByte("Role");
        }

        [Parser(Opcode.SMSG_BARBER_SHOP_RESULT)]
        public static void HandleBarberShopResult(Packet packet)
        {
            packet.ReadUInt32("Result");
            packet.ReadBit("Unk");
        }

        [Parser(Opcode.SMSG_BATCH_PRESENCE_SUBSCRIPTION)]
        public static void HandleBatchPresenceSubscription(Packet packet)
        {
            packet.ReadBit("Unk");
            var count = packet.ReadUInt32("Count");
            for (var i = 0; i < count; ++i)
                packet.ReadUInt32("Id", i);
        }
    }
}

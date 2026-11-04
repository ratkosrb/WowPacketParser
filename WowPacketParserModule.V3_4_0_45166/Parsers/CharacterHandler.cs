using System;
using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;
using WowPacketParser.Store;
using WowPacketParser.Store.Objects;

namespace WowPacketParserModule.V3_4_0_45166.Parsers
{
    public static class CharacterHandler
    {
        public static void ReadChrCustomizationChoice(Packet packet, params object[] indexes)
        {
            packet.ReadUInt32("ChrCustomizationOptionID", indexes);
            packet.ReadUInt32("ChrCustomizationChoiceID", indexes);
        }

        public static void ReadVisualItemInfo(Packet packet, params object[] idx)
        {
            packet.ReadUInt32("DisplayID", idx);
            packet.ReadUInt32("DisplayEnchantID", idx);
            packet.ReadInt32("SecondaryItemModifiedAppearanceID", idx);
            packet.ReadByteE<InventoryType>("InvType", idx);
            packet.ReadByte("Subclass", idx);
        }

        public static void ReadRaceLimitDisableInfo(Packet packet, params object[] idx)
        {
            packet.ReadInt32E<Race>("RaceID", idx);
            packet.ReadInt32("BlockReason", idx);
        }

        public static void ReadCustomTabardInfo(Packet packet, params object[] idx)
        {
            packet.ReadInt32("EmblemStyle", idx);
            packet.ReadInt32("EmblemColor", idx);
            packet.ReadInt32("BorderStyle", idx);
            packet.ReadInt32("BorderColor", idx);
            packet.ReadInt32("BackgroundColor", idx);
        }

        public static void ReadCharactersListEntry(Packet packet, params object[] idx)
        {
            ReadCharactersListEntryEx(packet, 34, idx);
        }

        public static void ReadCharactersListEntryEx(Packet packet, int visualItemCount, params object[] idx)
        {
            var playerGuid = packet.ReadPackedGuid128("Guid", idx);
            packet.ReadUInt64("GuildClubMemberID", idx);
            packet.ReadByte("ListPosition", idx);
            var race = packet.ReadByteE<Race>("RaceID", idx);
            var @class = packet.ReadByteE<Class>("ClassID", idx);
            packet.ReadByteE<Gender>("SexID", idx);
            var customizationCount = packet.ReadUInt32();
            var level = packet.ReadByte("ExperienceLevel", idx);
            var zone = packet.ReadInt32<ZoneId>("ZoneID", idx);
            var mapId = packet.ReadInt32<MapId>("MapID", idx);
            var pos = packet.ReadVector3("PreloadPos", idx);
            packet.ReadPackedGuid128("GuildGUID", idx);
            packet.ReadUInt32("Flags", idx);
            packet.ReadUInt32("Flags2", idx);
            packet.ReadUInt32("Flags3", idx);
            packet.ReadUInt32("PetCreatureDisplayID", idx);
            packet.ReadUInt32("PetExperienceLevel", idx);
            packet.ReadUInt32("PetCreatureFamilyID", idx);

            for (uint j = 0; j < 2; ++j)
                packet.ReadInt32("ProfessionIDs", idx, j);

            for (uint j = 0; j < visualItemCount; ++j)
                ReadVisualItemInfo(packet, idx, j);

            packet.ReadTime64("LastPlayedTime", idx);
            packet.ReadInt16("SpecID", idx);
            packet.ReadInt32("Unknown703", idx);
            packet.ReadInt32("LastLoginVersion", idx);
            packet.ReadUInt32("Flags4", idx);
            var mailSenderLengths = new uint[packet.ReadUInt32()];
            var mailSenderTypes = new uint[packet.ReadUInt32()];
            packet.ReadUInt32("OverrideSelectScreenFileDataID", idx);

            for (var j = 0u; j < customizationCount; ++j)
                ReadChrCustomizationChoice(packet, idx, "Customizations", j);

            for (var j = 0; j < mailSenderTypes.Length; ++j)
                packet.ReadUInt32("MailSenderType", idx, j);

            packet.ResetBitReader();

            var nameLength = packet.ReadBits("Character Name Length", 6, idx);
            var firstLogin = packet.ReadBit("FirstLogin", idx);
            packet.ReadBit("BoostInProgress", idx);
            packet.ReadBits("UnkWod61x", 5, idx);
            packet.ReadBits("Unk", 2, idx);

            if (ClientVersion.AddedInVersion(ClientVersionBuild.V3_4_3_51126))
            {
                packet.ReadBit("RpeResetAvailable", idx);
                packet.ReadBit("RpeResetQuestClearAvailable", idx);
            }

            for (var j = 0; j < mailSenderLengths.Length; ++j)
                mailSenderLengths[j] = packet.ReadBits(6);

            for (var j = 0; j < mailSenderLengths.Length; ++j)
                if (mailSenderLengths[j] > 1)
                    packet.ReadDynamicString("MailSender", mailSenderLengths[j], idx);

            var name = packet.ReadWoWString("Character Name", nameLength, idx);

            if (firstLogin)
            {
                PlayerCreateInfo startPos = new PlayerCreateInfo { Race = race, Class = @class, Map = (uint)mapId, Zone = (uint)zone, Position = pos, Orientation = 0 };
                Storage.StartPositions.Add(startPos, packet.TimeSpan);
            }

            var playerInfo = new Player { Race = race, Class = @class, Name = name, FirstLogin = firstLogin, Level = level, Type = ObjectType.Player };
            if (Storage.Objects.ContainsKey(playerGuid))
                Storage.Objects[playerGuid] = new Tuple<WoWObject, TimeSpan?>(playerInfo, packet.TimeSpan);
            else
                Storage.Objects.Add(playerGuid, playerInfo, packet.TimeSpan);
        }

        public static void ReadRaceUnlockData(Packet packet, params object[] idx)
        {
            packet.ReadInt32E<Race>("RaceID", idx);
            packet.ResetBitReader();
            packet.ReadBit("HasExpansion", idx);
            packet.ReadBit("HasAchievement", idx);
            packet.ReadBit("HasHeritageArmor", idx);
            packet.ReadBit("IsLocked");
        }

        // 3.4.0.44930 / 3.4.0.45043: no RaceLimitDisables, 23 visual items per character
        [Parser(Opcode.SMSG_ENUM_CHARACTERS_RESULT, ClientVersionBuild.V3_4_0_44463)]
        public static void HandleEnumCharactersResultEarly(Packet packet)
        {
            if (ClientVersion.AddedInVersion(ClientVersionBuild.V3_4_0_45166))
            {
                V9_0_1_36216.Parsers.CharacterHandler.HandleEnumCharactersResult(packet);
                return;
            }

            packet.ReadBit("Success");
            packet.ReadBit("IsDeletedCharacters");
            packet.ReadBit("IsNewPlayerRestrictionSkipped");
            packet.ReadBit("IsNewPlayerRestricted");
            packet.ReadBit("IsNewPlayer");
            packet.ReadBit("IsTrialAccountRestricted");
            var hasDisabledClassesMask = packet.ReadBit("HasDisabledClassesMask");
            packet.ReadBit("IsAlliedRacesCreationAllowed");

            var charsCount = packet.ReadUInt32("CharactersCount");
            packet.ReadInt32("MaxCharacterLevel");
            var raceUnlockCount = packet.ReadUInt32("RaceUnlockCount");
            var unlockedConditionalAppearanceCount = packet.ReadUInt32("UnlockedConditionalAppearanceCount");

            if (hasDisabledClassesMask)
                packet.ReadUInt32("DisabledClassesMask");

            for (var i = 0u; i < unlockedConditionalAppearanceCount; ++i)
                V8_0_1_27101.Parsers.CharacterHandler.ReadUnlockedConditionalAppearance(packet, "UnlockedConditionalAppearances", i);

            for (var i = 0u; i < charsCount; ++i)
                ReadCharactersListEntryEx(packet, 23, i, "Characters");

            for (var i = 0u; i < raceUnlockCount; ++i)
                ReadRaceUnlockData(packet, i, "RaceUnlockData");
        }

        [Parser(Opcode.SMSG_ENUM_CHARACTERS_RESULT, ClientVersionBuild.V3_4_1_47720)]
        public static void HandleEnumCharactersResult(Packet packet)
        {
            packet.ReadBit("Success");
            packet.ReadBit("IsDeletedCharacters");
            packet.ReadBit("IsNewPlayerRestrictionSkipped");
            packet.ReadBit("IsNewPlayerRestricted");
            packet.ReadBit("IsNewPlayer");
            packet.ReadBit("IsTrialAccountRestricted");
            var hasDisabledClassesMask = packet.ReadBit("HasDisabledClassesMask");

            var charsCount = packet.ReadUInt32("CharactersCount");
            packet.ReadInt32("MaxCharacterLevel");
            var raceUnlockCount = packet.ReadUInt32("RaceUnlockCount");
            var unlockedConditionalAppearanceCount = packet.ReadUInt32("UnlockedConditionalAppearanceCount");
            var raceLimitDisablesCount = packet.ReadUInt32("RaceLimitDisablesCount");

            if (hasDisabledClassesMask)
                packet.ReadUInt32("DisabledClassesMask");

            for (var i = 0u; i < unlockedConditionalAppearanceCount; ++i)
                V8_0_1_27101.Parsers.CharacterHandler.ReadUnlockedConditionalAppearance(packet, "UnlockedConditionalAppearances", i);

            for (var i = 0u; i < raceLimitDisablesCount; i++)
                ReadRaceLimitDisableInfo(packet, "RaceLimitDisableInfo", i);

            for (var i = 0u; i < charsCount; ++i)
                ReadCharactersListEntry(packet, i, "Characters");

            for (var i = 0u; i < raceUnlockCount; ++i)
                ReadRaceUnlockData(packet, i, "RaceUnlockData");
        }

        [Parser(Opcode.SMSG_LEVEL_UP_INFO)]
        public static void HandleLevelUpInfo(Packet packet)
        {
            packet.ReadInt32("Level");
            packet.ReadInt32("HealthDelta");

            for (var i = 0; i < 7; i++)
                packet.ReadInt32("PowerDelta", (PowerType)i);

            for (var i = 0; i < 5; i++)
                packet.ReadInt32("StatDelta", (StatType)i);

            packet.ReadInt32("NumNewTalents");
            packet.ReadInt32("NumNewPvpTalentSlots");
        }

        public static void ReadTraitEntry(Packet packet, params object[] indexes)
        {
            packet.ReadInt32("TraitNodeID", indexes);
            packet.ReadInt32("TraitNodeEntryID", indexes);
            packet.ReadInt32("Rank", indexes);
            packet.ReadInt32("GrantedRanks", indexes);
        }

        public static void ReadTraitConfig(Packet packet, params object[] indexes)
        {
            packet.ReadInt32("ID", indexes);
            var type = packet.ReadInt32("Type", indexes);
            var entries = packet.ReadUInt32();

            switch (type)
            {
                case 1:
                    packet.ReadInt32("ChrSpecializationID", indexes);
                    packet.ReadInt32("CombatConfigFlags", indexes);
                    packet.ReadInt32("LocalIdentifier", indexes);
                    break;
                case 2:
                    packet.ReadInt32("SkillLineID", indexes);
                    break;
                case 3:
                    packet.ReadInt32("TraitSystemID", indexes);
                    break;
            }

            for (var i = 0u; i < entries; ++i)
                ReadTraitEntry(packet, indexes, "TraitEntry", i);

            var nameLength = packet.ReadBits(9);
            packet.ResetBitReader();

            packet.ReadWoWString("Name", nameLength, indexes);
        }

        [Parser(Opcode.SMSG_INSPECT_RESULT)]
        public static void HandleInspectResult(Packet packet)
        {
            WowPacketParserModule.V2_5_1_38835.Parsers.CharacterHandler.ReadPlayerModelDisplayInfo(packet, "DisplayInfo");
            packet.ReadUInt32("Unk");
            packet.ReadInt32("ItemLevel");
            packet.ReadByte("LifetimeMaxRank");
            packet.ReadUInt16("TodayHK");
            packet.ReadUInt16("YesterdayHK");
            packet.ReadUInt32("LifetimeHK");
            packet.ReadUInt32("HonorLevel");

            SpellHandler.ReadTalentInfoUpdate(packet);

            packet.ReadByte("Unk2");

            packet.ResetBitReader();
            var hasGuildData = packet.ReadBit("HasGuildData");
            var hasAzeriteLevel = packet.ReadBit("HasAzeriteLevel");

            int count = ClientVersion.AddedInVersion(ClientVersionBuild.V3_4_1_47720) ? 7 : 6;
            for (int i = 0; i < count; i++)
                WowPacketParserModule.V9_0_1_36216.Parsers.CharacterHandler.ReadPVPBracketData(packet, i, "PVPBracketData");

            if (hasGuildData)
            {
                packet.ReadPackedGuid128("GuildGUID");
                packet.ReadInt32("NumGuildMembers");
                packet.ReadInt32("GuildAchievementPoints");
            }
            if (hasAzeriteLevel)
                packet.ReadInt32("AzeriteLevel");

            if (ClientVersion.AddedInVersion(ClientVersionBuild.V3_4_1_47720))
            {
                packet.ReadInt32("Level", "TraitInspectData");
                packet.ReadInt32("ChrSpecializationID", "TraitInspectData");
                packet.ResetBitReader();
                ReadTraitConfig(packet, "TraitInspectData", "Traits");
            }
        }
    }
}

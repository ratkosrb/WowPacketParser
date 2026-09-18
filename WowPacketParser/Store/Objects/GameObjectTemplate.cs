using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.SQL;

namespace WowPacketParser.Store.Objects
{
    [DBTableName("gameobject_template")]
    public sealed class GameObjectTemplate : IDataModel
    {
        [DBFieldName("entry", true)]
        public uint? Entry;

        [DBFieldName("sniff_build", true, DbType = (TargetedDbType.WPP))]
        [DBFieldName("VerifiedBuild", false, DbType = (TargetedDbType.CMANGOS))]
        public int? VerifiedBuild = ClientVersion.BuildInt;

        [DBFieldName("type")]
        public GameObjectType? Type;

        [DBFieldName("display_id", DbType = (TargetedDbType.WPP))]
        [DBFieldName("displayId", DbType = (TargetedDbType.TRINITY | TargetedDbType.VMANGOS | TargetedDbType.CMANGOS))]
        public uint? DisplayID;

        [DBFieldName("scale", TargetedDbExpansion.WrathOfTheLichKing, Nullable = true, DbType = (TargetedDbType.WPP))]
        [DBFieldName("size", TargetedDbExpansion.WrathOfTheLichKing, Nullable = true, DbType = (TargetedDbType.TRINITY | TargetedDbType.VMANGOS | TargetedDbType.CMANGOS))]
        public float? Size;

        [DBFieldName("name", LocaleConstant.enUS)] // ToDo: Add locale support
        public string Name;

        [DBFieldName("icon_name", Nullable = true, DbType = (TargetedDbType.WPP))]
        [DBFieldName("IconName", Nullable = true, DbType = (TargetedDbType.TRINITY | TargetedDbType.CMANGOS))]
        public string IconName;

        // ToDo: Add locale support
        [DBFieldName("opening_text", TargetedDbExpansion.TheBurningCrusade, LocaleConstant.enUS, Nullable = true, DbType = (TargetedDbType.WPP))]
        [DBFieldName("castBarCaption", TargetedDbExpansion.TheBurningCrusade, LocaleConstant.enUS, Nullable = true, DbType = (TargetedDbType.TRINITY | TargetedDbType.CMANGOS))]
        public string CastCaption;

        [DBFieldName("closing_text", TargetedDbExpansion.TheBurningCrusade, LocaleConstant.enUS, Nullable = true, DbType = (TargetedDbType.WPP | TargetedDbType.TRINITY))]
        public string ClosingText;

        [DBFieldName("data", TargetedDbExpansion.Zero, TargetedDbExpansion.Cataclysm, 24, StartAtZero = true, Nullable = true, DbType = (TargetedDbType.WPP | TargetedDbType.VMANGOS | TargetedDbType.CMANGOS))]
        [DBFieldName("Data", TargetedDbExpansion.Zero, TargetedDbExpansion.Cataclysm, 24, StartAtZero = true, Nullable = true, DbType = (TargetedDbType.TRINITY))]
        [DBFieldName("data", TargetedDbExpansion.Cataclysm, TargetedDbExpansion.WarlordsOfDraenor, 32, StartAtZero = true, Nullable = true, DbType = (TargetedDbType.WPP | TargetedDbType.VMANGOS | TargetedDbType.CMANGOS))]
        [DBFieldName("Data", TargetedDbExpansion.Cataclysm, TargetedDbExpansion.WarlordsOfDraenor, 32, StartAtZero = true, Nullable = true, DbType = (TargetedDbType.TRINITY))]
        [DBFieldName("data", TargetedDbExpansion.WarlordsOfDraenor, TargetedDbExpansion.BattleForAzeroth, 33, StartAtZero = true, Nullable = true, DbType = (TargetedDbType.WPP | TargetedDbType.VMANGOS | TargetedDbType.CMANGOS))]
        [DBFieldName("Data", TargetedDbExpansion.WarlordsOfDraenor, TargetedDbExpansion.BattleForAzeroth, 33, StartAtZero = true, Nullable = true, DbType = (TargetedDbType.TRINITY))]
        [DBFieldName("data", TargetedDbExpansion.WrathOfTheLichKingClassic, TargetedDbExpansion.Zero, 34, StartAtZero = true, Nullable = true, DbType = (TargetedDbType.WPP | TargetedDbType.VMANGOS | TargetedDbType.CMANGOS))]
        [DBFieldName("Data", TargetedDbExpansion.WrathOfTheLichKingClassic, TargetedDbExpansion.Zero, 34, StartAtZero = true, Nullable = true, DbType = (TargetedDbType.TRINITY))]
        [DBFieldName("data", TargetedDbExpansion.BattleForAzeroth, TargetedDbExpansion.Shadowlands, 34, StartAtZero = true, Nullable = true, DbType = (TargetedDbType.WPP | TargetedDbType.VMANGOS | TargetedDbType.CMANGOS))]
        [DBFieldName("Data", TargetedDbExpansion.BattleForAzeroth, TargetedDbExpansion.Shadowlands, 34, StartAtZero = true, Nullable = true, DbType = (TargetedDbType.TRINITY))]
        [DBFieldName("data", TargetedDbExpansion.Shadowlands, 35, StartAtZero = true, Nullable = true, DbType = (TargetedDbType.WPP | TargetedDbType.VMANGOS | TargetedDbType.CMANGOS))]
        [DBFieldName("Data", TargetedDbExpansion.Shadowlands, 35, StartAtZero = true, Nullable = true, DbType = (TargetedDbType.TRINITY))]
        public int?[] Data;

        [DBFieldName("quest_items_count", TargetedDbExpansion.WrathOfTheLichKingClassic, TargetedDbExpansion.Zero, Nullable = true, DbType = (TargetedDbType.WPP))]
        [DBFieldName("quest_items_count", TargetedDbExpansion.WrathOfTheLichKing, Nullable = true, DbType = (TargetedDbType.WPP))]
        public uint QuestItems;

        [DBFieldName("required_level", TargetedDbExpansion.Cataclysm, TargetedDbExpansion.Shadowlands, Nullable = true, DbType = (TargetedDbType.WPP))]
        [DBFieldName("RequiredLevel", TargetedDbExpansion.Cataclysm, TargetedDbExpansion.Shadowlands, Nullable = true, DbType = (TargetedDbType.TRINITY))]
        public int? RequiredLevel;

        [DBFieldName("content_tuning_id", TargetedDbExpansion.Shadowlands, Nullable = true, DbType = (TargetedDbType.WPP))]
        [DBFieldName("ContentTuningId", TargetedDbExpansion.Shadowlands, Nullable = true, DbType = (TargetedDbType.TRINITY))]
        public int? ContentTuningId;
    }

    [DBTableName("gameobject_quest_item", TargetedDbType.WPP)]
    [DBTableName("gameobject_questitem", TargetedDbType.TRINITY)]
    public sealed class GameObjectTemplateQuestItem : IDataModel
    {
        [DBFieldName("entry", true, DbType = (TargetedDbType.WPP))]
        [DBFieldName("GameObjectEntry", true, DbType = (TargetedDbType.TRINITY))]
        public uint? GameObjectEntry;

        [DBFieldName("idx", true, DbType = (TargetedDbType.WPP))]
        [DBFieldName("Idx", true, DbType = (TargetedDbType.TRINITY))]
        public uint? Idx;

        [DBFieldName("item_id", DbType = (TargetedDbType.WPP))]
        [DBFieldName("ItemId", DbType = (TargetedDbType.TRINITY))]
        public uint? ItemId;

        [DBFieldName("sniff_build", DbType = (TargetedDbType.WPP))]
        [DBFieldName("VerifiedBuild", DbType = (TargetedDbType.TRINITY))]
        public int? VerifiedBuild = ClientVersion.BuildInt;
    }

    [DBTableName("gameobject_unique_anim")]
    public sealed class GameObjectUniqueAnim : ITableWithSniffIdList
    {
        [DBFieldName("entry", true)]
        public uint GameObjectEntry;

        [DBFieldName("anim_id", true)]
        public int AnimId;

        [DBFieldName("as_despawn", true, false, true)]
        public bool? AsDespawn;
    }
}

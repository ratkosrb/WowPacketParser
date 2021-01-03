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

        [DBFieldName("type")]
        public GameObjectType? Type;

        [DBFieldName("display_id")]
        public uint? DisplayID;

        [DBFieldName("scale")]
        public float? Size;

        [DBFieldName("name", LocaleConstant.enUS)] // ToDo: Add locale support
        public string Name;

        [DBFieldName("icon_name")]
        public string IconName;

        [DBFieldName("cast_bar_caption", LocaleConstant.enUS)] // ToDo: Add locale support
        public string CastCaption;

        [DBFieldName("unk_string")]
        public string UnkString;

        [DBFieldName("data", TargetedDatabase.Zero, TargetedDatabase.Cataclysm, 24, true)]
        [DBFieldName("data", TargetedDatabase.Cataclysm, TargetedDatabase.WarlordsOfDraenor, 32, true)]
        [DBFieldName("data", TargetedDatabase.WarlordsOfDraenor, TargetedDatabase.BattleForAzeroth, 33, true)]
        [DBFieldName("data", TargetedDatabase.BattleForAzeroth, 34, true)]
        [DBFieldName("data", TargetedDatabase.Classic, 34, true)]
        public int?[] Data;

        [DBFieldName("quest_items_count")]
        public uint QuestItems;

        [DBFieldName("RequiredLevel", TargetedDatabase.Cataclysm, TargetedDatabase.Shadowlands)]
        public int? RequiredLevel;

        [DBFieldName("ContentTuningId", TargetedDatabase.Shadowlands)]
        public int? ContentTuningId;

        [DBFieldName("sniff_build")]
        public int? VerifiedBuild = ClientVersion.BuildInt;
    }

    [DBTableName("gameobject_questitem")]
    public sealed class GameObjectTemplateQuestItem : IDataModel
    {
        [DBFieldName("GameObjectEntry", true)]
        public uint? GameObjectEntry;

        [DBFieldName("Idx", true)]
        public uint? Idx;

        [DBFieldName("ItemId")]
        public uint? ItemId;

        [DBFieldName("VerifiedBuild", TargetedDatabase.WarlordsOfDraenor)]
        public int? VerifiedBuild = ClientVersion.BuildInt;
    }
}

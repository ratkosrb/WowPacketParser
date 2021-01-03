using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.SQL;

namespace WowPacketParser.Store.Objects
{
    [DBTableName("client_quest_accept")]
    public sealed class QuestClientAccept : IDataModel
    {
        [DBFieldName("unixtimems", true)]
        public ulong UnixTimeMs;

        [DBFieldName("object_guid", false, true)]
        public string ObjectGuid;

        [DBFieldName("object_id")]
        public uint ObjectId;

        [DBFieldName("object_type")]
        public string ObjectType;

        [DBFieldName("quest_id")]
        public uint QuestId;
    }

    [DBTableName("client_quest_complete")]
    public sealed class QuestClientComplete : IDataModel
    {
        [DBFieldName("unixtimems", true)]
        public ulong UnixTimeMs;

        [DBFieldName("object_guid", false, true)]
        public string ObjectGuid;

        [DBFieldName("object_id")]
        public uint ObjectId;

        [DBFieldName("object_type")]
        public string ObjectType;

        [DBFieldName("quest_id")]
        public uint QuestId;
    }

    [DBTableName("client_creature_interact")]
    public sealed class CreatureClientInteract : IDataModel
    {
        [DBFieldName("unixtimems", true)]
        public ulong UnixTimeMs;

        [DBFieldName("guid", true, true)]
        public string GUID;
    }

    [DBTableName("client_gameobject_use")]
    public sealed class GameObjectClientUse : IDataModel
    {
        [DBFieldName("unixtimems", true)]
        public ulong UnixTimeMs;

        [DBFieldName("guid", true, true)]
        public string GUID;
    }

    [DBTableName("client_item_use")]
    public sealed class ItemClientUse : IDataModel
    {
        [DBFieldName("unixtimems", true)]
        public ulong UnixTimeMs;

        [DBFieldName("entry", true, true)]
        public uint Entry;
    }
    [DBTableName("client_release_spirit")]
    public sealed class ClientReleaseSpirit : IDataModel
    {
        [DBFieldName("unixtimems", true)]
        public ulong UnixTimeMs;
    }
    [DBTableName("client_reclaim_corpse")]
    public sealed class ClientReclaimCorpse : IDataModel
    {
        [DBFieldName("unixtimems", true)]
        public ulong UnixTimeMs;
    }
}

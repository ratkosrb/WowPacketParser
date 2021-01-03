using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.SQL;

namespace WowPacketParser.Store.Objects
{
    [DBTableName("gameobject")]
    public sealed class GameObjectModel : IDataModel
    {
        [DBFieldName("guid", true, true)]
        public string GUID;

        [DBFieldName("id")]
        public uint? ID;

        [DBFieldName("map", false, false, true)]
        public uint? Map;

        [DBFieldName("zone_id")]
        public uint? ZoneID;

        [DBFieldName("area_id")]
        public uint? AreaID;

        [DBFieldName("spawn_mask", TargetedDatabase.WrathOfTheLichKing, TargetedDatabase.Legion)]
        public uint? SpawnMask;

        [DBFieldName("spawn_difficulties", TargetedDatabase.Legion)]
        public string SpawnDifficulties;

        [DBFieldName("phase_mask", TargetedDatabase.WrathOfTheLichKing, TargetedDatabase.Cataclysm)]
        public uint? PhaseMask;

        [DBFieldName("phase_id", TargetedDatabase.Cataclysm)]
        public string PhaseID;

        [DBFieldName("phase_group", TargetedDatabase.Cataclysm)]
        public uint? PhaseGroup;

        [DBFieldName("position_x")]
        public float? PositionX;

        [DBFieldName("position_y")]
        public float? PositionY;

        [DBFieldName("position_z")]
        public float? PositionZ;

        [DBFieldName("orientation")]
        public float? Orientation;

        [DBFieldName("rotation", 4, true)]
        public float?[] Rotation;

        [DBFieldName("temp")]
        public byte? TemporarySpawn;

        [DBFieldName("creator_guid", false, true)]
        public string CreatedByGuid;

        [DBFieldName("creator_id")]
        public uint CreatedById;

        [DBFieldName("creator_type")]
        public string CreatedByType;

        [DBFieldName("display_id")]
        public uint? DisplayID;

        [DBFieldName("level")]
        public uint? Level;

        [DBFieldName("faction")]
        public uint? Faction;

        [DBFieldName("flags")]
        public uint? Flags;

        [DBFieldName("state")]
        public uint? State;

        [DBFieldName("animprogress")]
        public uint? AnimProgress;

        [DBFieldName("sniff_id", false, false, false, true)]
        public int? SniffId;

        [DBFieldName("sniff_build")]
        public int? SniffBuild = ClientVersion.BuildInt;
    }

    [DBTableName("gameobject_create1_time")]
    public sealed class GameObjectCreate1 : IDataModel
    {
        [DBFieldName("unixtimems", true)]
        public ulong UnixTimeMs;

        [DBFieldName("guid", true, true)]
        public string GUID;

        [DBFieldName("position_x")]
        public float? PositionX;

        [DBFieldName("position_y")]
        public float? PositionY;

        [DBFieldName("position_z")]
        public float? PositionZ;

        [DBFieldName("orientation")]
        public float? Orientation;
    }

    [DBTableName("gameobject_create2_time")]
    public sealed class GameObjectCreate2 : IDataModel
    {
        [DBFieldName("unixtimems", true)]
        public ulong UnixTimeMs;

        [DBFieldName("guid", true, true)]
        public string GUID;

        [DBFieldName("position_x")]
        public float? PositionX;

        [DBFieldName("position_y")]
        public float? PositionY;

        [DBFieldName("position_z")]
        public float? PositionZ;

        [DBFieldName("orientation")]
        public float? Orientation;
    }

    [DBTableName("gameobject_destroy_time")]
    public sealed class GameObjectDestroy : IDataModel
    {
        [DBFieldName("unixtimems", true)]
        public ulong UnixTimeMs;

        [DBFieldName("guid", true, true)]
        public string GUID;
    }

    [DBTableName("gameobject_values_update")]
    public sealed class GameObjectUpdate : IDataModel
    {
        [DBFieldName("unixtimems", true)]
        public ulong UnixTimeMs;

        [DBFieldName("guid", true, true)]
        public string GUID;

        [DBFieldName("display_id", false, false, true)]
        public uint? DisplayID;

        [DBFieldName("level", false, false, true)]
        public uint? Level;

        [DBFieldName("faction", false, false, true)]
        public uint? Faction;

        [DBFieldName("flags", false, false, true)]
        public uint? Flags;

        [DBFieldName("state", false, false, true)]
        public uint? State;

        [DBFieldName("animprogress", false, false, true)]
        public uint? AnimProgress;
    }

    [DBTableName("gameobject_custom_anim")]
    public sealed class GameObjectCustomAnim : IDataModel
    {
        [DBFieldName("unixtimems", true)]
        public ulong UnixTimeMs;

        [DBFieldName("guid", true, true)]
        public string GUID;

        [DBFieldName("anim_id")]
        public int AnimId;

        [DBFieldName("as_despawn", false, false, true)]
        public bool? AsDespawn;
    }

    [DBTableName("gameobject_despawn_anim")]
    public sealed class GameObjectDespawnAnim : IDataModel
    {
        [DBFieldName("unixtimems", true)]
        public ulong UnixTimeMs;

        [DBFieldName("guid", true, true)]
        public string GUID;
    }
}

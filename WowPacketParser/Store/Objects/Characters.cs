using System;
using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.SQL;

namespace WowPacketParser.Store.Objects
{
    [DBTableName("characters")]
    public sealed class CharacterTemplate : IDataModel
    {
        [DBFieldName("guid", true, true)]
        public string Guid;

        [DBFieldName("account", false, true)]
        public string Account;

        [DBFieldName("name")]
        public string Name;

        [DBFieldName("race")]
        public uint? Race;

        [DBFieldName("class")]
        public uint Class;

        [DBFieldName("gender")]
        public uint Gender;

        [DBFieldName("level")]
        public uint Level;

        [DBFieldName("xp")]
        public uint XP = 0;

        [DBFieldName("money")]
        public uint Money = 0;

        [DBFieldName("playerBytes")]
        public uint PlayerBytes;

        [DBFieldName("playerBytes2")]
        public uint PlayerBytes2;

        [DBFieldName("playerFlags")]
        public uint PlayerFlags;

        [DBFieldName("position_x")]
        public float PositionX;

        [DBFieldName("position_y")]
        public float PositionY;

        [DBFieldName("position_z")]
        public float PositionZ;

        [DBFieldName("map")]
        public uint Map;

        [DBFieldName("orientation")]
        public float Orientation;

        [DBFieldName("health")]
        public uint Health = 1;

        [DBFieldName("power1")]
        public uint Power1 = 0;

        [DBFieldName("equipmentCache")]
        public string EquipmentCache = "";
    }
    [DBTableName("player")]
    public sealed class PlayerTemplate : IDataModel
    {
        [DBFieldName("guid", true, true)]
        public string Guid;

        [DBFieldName("map")]
        public uint Map;

        [DBFieldName("position_x")]
        public float PositionX;

        [DBFieldName("position_y")]
        public float PositionY;

        [DBFieldName("position_z")]
        public float PositionZ;

        [DBFieldName("orientation")]
        public float Orientation;

        [DBFieldName("name")]
        public string Name;

        [DBFieldName("race")]
        public uint? Race;

        [DBFieldName("class")]
        public uint Class;

        [DBFieldName("gender")]
        public uint Gender;

        [DBFieldName("level")]
        public uint Level;

        [DBFieldName("xp")]
        public uint XP = 0;

        [DBFieldName("money")]
        public uint Money = 0;

        [DBFieldName("player_bytes1")]
        public uint PlayerBytes;

        [DBFieldName("player_bytes2")]
        public uint PlayerBytes2;

        [DBFieldName("player_flags")]
        public uint PlayerFlags;

        [DBFieldName("scale")]
        public float? Scale;

        [DBFieldName("display_id")]
        public uint? DisplayID;

        [DBFieldName("native_display_id")]
        public uint? NativeDisplayID;

        [DBFieldName("mount_display_id")]
        public uint? MountDisplayID;

        [DBFieldName("faction")]
        public uint? FactionTemplate;

        [DBFieldName("unit_flags")]
        public uint? UnitFlags;

        [DBFieldName("unit_flags2")]
        public uint? UnitFlags2;

        [DBFieldName("current_health")]
        public uint? CurHealth;

        [DBFieldName("max_health")]
        public uint? MaxHealth;

        [DBFieldName("current_mana")]
        public uint? CurMana;

        [DBFieldName("max_mana")]
        public uint? MaxMana;

        [DBFieldName("aura_state")]
        public uint? AuraState;

        [DBFieldName("emote_state")]
        public uint? EmoteState;

        [DBFieldName("stand_state")]
        public uint? StandState;

        [DBFieldName("pet_talent_points")]
        public uint? PetTalentPoints;

        [DBFieldName("vis_flags")]
        public uint? VisFlags;

        [DBFieldName("anim_tier")]
        public uint? AnimTier;

        [DBFieldName("sheath_state")]
        public uint? SheatheState;

        [DBFieldName("pvp_flags")]
        public uint? PvpFlags;

        [DBFieldName("pet_flags")]
        public uint? PetFlags;

        [DBFieldName("shapeshift_form")]
        public uint? ShapeshiftForm;

        [DBFieldName("speed_walk")]
        public float? SpeedWalk;

        [DBFieldName("speed_run")]
        public float? SpeedRun;

        [DBFieldName("bounding_radius")]
        public float? BoundingRadius;

        [DBFieldName("combat_reach")]
        public float? CombatReach;

        [DBFieldName("mod_melee_haste")]
        public float? ModMeleeHaste;

        [DBFieldName("mod_ranged_haste")]
        public float? ModRangedHaste;

        [DBFieldName("base_attack_time")]
        public uint? BaseAttackTime;

        [DBFieldName("ranged_attack_time")]
        public uint? RangedAttackTime;

        [DBFieldName("equipment_cache")]
        public string EquipmentCache = "";

        [DBFieldName("auras")]
        public string Auras;
    }

    [DBTableName("player_create1_time")]
    public sealed class PlayerCreate1 : IDataModel
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

    [DBTableName("player_create2_time")]
    public sealed class PlayerCreate2 : IDataModel
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

    [DBTableName("player_destroy_time")]
    public sealed class PlayerDestroy : IDataModel
    {
        [DBFieldName("unixtimems", true)]
        public ulong UnixTimeMs;

        [DBFieldName("guid", true, true)]
        public string GUID;
    }

    [DBTableName("character_inventory")]
    public sealed class CharacterInventory : IDataModel
    {
        [DBFieldName("guid", true, true)]
        public string Guid;

        [DBFieldName("bag")]
        public uint Bag;

        [DBFieldName("slot")]
        public uint Slot;

        [DBFieldName("item", true, true)]
        public string ItemGuid;

        [DBFieldName("item_template")]
        public uint ItemTemplate;
    }
    [DBTableName("item_instance")]
    public sealed class CharacterItemInstance : IDataModel
    {
        [DBFieldName("guid", true, true)]
        public string Guid;

        [DBFieldName("itemEntry")]
        public uint ItemEntry;

        [DBFieldName("owner_guid", false, true)]
        public string OwnerGuid;

        [DBFieldName("count")]
        public uint Count = 1;

        [DBFieldName("charges")]
        public string Charges = "0 0 0 0 0 ";

        [DBFieldName("enchantments")]
        public string Enchantments = "0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 ";

        [DBFieldName("durability")]
        public uint Durability = 1;
    }
    [DBTableName("player_movement_client")]
    public sealed class ClientSideMovement : IDataModel
    {
        [DBFieldName("unixtimems", true)]
        public ulong UnixTimeMs;

        [DBFieldName("guid", true, true)]
        public string Guid;

        [DBFieldName("opcode")]
        public string Opcode;

        [DBFieldName("move_time")]
        public uint MoveTime;

        [DBFieldName("move_flags")]
        public uint MoveFlags;

        [DBFieldName("map")]
        public uint Map;

        [DBFieldName("position_x")]
        public float PositionX;

        [DBFieldName("position_y")]
        public float PositionY;

        [DBFieldName("position_z")]
        public float PositionZ;

        [DBFieldName("orientation")]
        public float Orientation;
    }
    public sealed class PlayerMovement
    {
        public WowGuid guid;
        public Vector4 Position;
        public uint Map;
        public uint MoveTime;
        public uint MoveFlags;
        public int Opcode;
        public Direction OpcodeDirection;
        public DateTime Time;
    }
    [DBTableName("player_active_player")]
    public sealed class CharacterActivePlayer : IDataModel
    {
        [DBFieldName("unixtime", true)]
        public uint UnixTime;

        [DBFieldName("guid", true, true)]
        public string Guid;
    }
    public sealed class ActivePlayerCreateTime
    {
        public WowGuid Guid;
        public DateTime Time;
    }

    [DBTableName("guild_member")]
    public sealed class GuildMember : IDataModel
    {
        [DBFieldName("guildid")]
        public string GuildGUID = "";

        [DBFieldName("guid", true, true)]
        public string Guid;

        [DBFieldName("rank")]
        public uint GuildRank = 0;

        [DBFieldName("pnote")]
        public string pnote = "";

        [DBFieldName("offnote")]
        public string offnote = "";
    }
}

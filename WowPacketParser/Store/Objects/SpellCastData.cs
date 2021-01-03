using System;
using System.Collections.Generic;
using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.SQL;

namespace WowPacketParser.Store.Objects
{
    [DBTableName("play_spell_visual_kit")]
    public sealed class PlaySpellVisualKit : IDataModel
    {
        [DBFieldName("unixtimems", true)]
        public ulong UnixTimeMs;

        [DBFieldName("caster_guid", true, true)]
        public string CasterGuid;

        [DBFieldName("caster_id")]
        public uint CasterId;

        [DBFieldName("caster_type", true)]
        public string CasterType;

        [DBFieldName("kit_id", true)]
        public uint KitId;

        [DBFieldName("kit_type", false, false, true)]
        public uint? KitType;

        [DBFieldName("duration", false, false, true)]
        public uint? Duration;

        public WowGuid Guid;
        public DateTime Time;
    }

    [DBTableName("spell_cast_failed")]
    public sealed class SpellCastFailed : IDataModel
    {
        [DBFieldName("unixtimems", true)]
        public ulong UnixTimeMs;

        [DBFieldName("caster_guid", true, true)]
        public string CasterGuid;

        [DBFieldName("caster_id")]
        public uint CasterId;

        [DBFieldName("caster_type", true)]
        public string CasterType;

        [DBFieldName("spell_id", true)]
        public uint SpellId;

        [DBFieldName("visual_id")]
        public uint VisualId;

        [DBFieldName("reason")]
        public uint? Reason;

        public WowGuid Guid;
        public DateTime Time;
    }

    [DBTableName("spell_channel_start")]
    public sealed class SpellChannelStart : IDataModel
    {
        [DBFieldName("unixtimems", true)]
        public ulong UnixTimeMs;

        [DBFieldName("caster_guid", true, true)]
        public string CasterGuid;

        [DBFieldName("caster_id")]
        public uint CasterId;

        [DBFieldName("caster_type", true)]
        public string CasterType;

        [DBFieldName("spell_id", true)]
        public uint SpellId;

        [DBFieldName("visual_id")]
        public uint VisualId;

        [DBFieldName("duration")]
        public int Duration;

        public WowGuid Guid;
        public DateTime Time;
    }

    [DBTableName("spell_channel_update")]
    public sealed class SpellChannelUpdate : IDataModel
    {
        [DBFieldName("unixtimems", true)]
        public ulong UnixTimeMs;

        [DBFieldName("caster_guid", true, true)]
        public string CasterGuid;

        [DBFieldName("caster_id")]
        public uint CasterId;

        [DBFieldName("caster_type", true)]
        public string CasterType;

        [DBFieldName("duration", true)]
        public int Duration;

        public WowGuid Guid;
        public DateTime Time;
    }

    [DBTableName("spell_cast_start")]
    public sealed class SpellCastStart : IDataModel
    {
        [DBFieldName("unixtimems", true)]
        public ulong UnixTimeMs;

        [DBFieldName("caster_guid", true, true)]
        public string CasterGuid;

        [DBFieldName("caster_id")]
        public uint CasterId;

        [DBFieldName("caster_type", true)]
        public string CasterType;

        [DBFieldName("caster_unit_guid", false, true)]
        public string CasterUnitGuid;

        [DBFieldName("caster_unit_id")]
        public uint CasterUnitId;

        [DBFieldName("caster_unit_type")]
        public string CasterUnitType;

        [DBFieldName("spell_id", true)]
        public uint SpellId;

        [DBFieldName("visual_id")]
        public uint VisualId;

        [DBFieldName("cast_time")]
        public uint CastTime;

        [DBFieldName("cast_flags")]
        public uint CastFlags;

        [DBFieldName("cast_flags_ex")]
        public uint CastFlagsEx;

        [DBFieldName("ammo_display_id")]
        public int AmmoDisplayId;

        [DBFieldName("ammo_inventory_type")]
        public int AmmoInventoryType;

        [DBFieldName("target_guid", false, true)]
        public string TargetGuid;

        [DBFieldName("target_id")]
        public uint TargetId;

        [DBFieldName("target_type")]
        public string TargetType;
    }

    [DBTableName("spell_cast_go_target")]
    public sealed class SpellCastGoTarget : IDataModel
    {
        [DBFieldName("list_id", true)]
        public uint ListId;

        [DBFieldName("target_guid", true, true)]
        public string TargetGuid;

        [DBFieldName("target_id")]
        public uint TargetId;

        [DBFieldName("target_type", true)]
        public string TargetType;
    }

    [DBTableName("spell_cast_go_position")]
    public sealed class SpellCastGoPosition : IDataModel
    {
        [DBFieldName("id", true)]
        public uint Id;

        [DBFieldName("position_x")]
        public float PositionX;

        [DBFieldName("position_y")]
        public float PositionY;

        [DBFieldName("position_z")]
        public float PositionZ;
    }

    [DBTableName("spell_cast_go")]
    public sealed class SpellCastGo : IDataModel
    {
        [DBFieldName("unixtimems", true)]
        public ulong UnixTimeMs;

        [DBFieldName("caster_guid", true, true)]
        public string CasterGuid;

        [DBFieldName("caster_id")]
        public uint CasterId;

        [DBFieldName("caster_type", true)]
        public string CasterType;

        [DBFieldName("caster_unit_guid", false, true)]
        public string CasterUnitGuid;

        [DBFieldName("caster_unit_id")]
        public uint CasterUnitId;

        [DBFieldName("caster_unit_type")]
        public string CasterUnitType;

        [DBFieldName("spell_id", true)]
        public uint SpellId;

        [DBFieldName("visual_id")]
        public uint VisualId;

        [DBFieldName("cast_flags")]
        public uint CastFlags;

        [DBFieldName("cast_flags_ex")]
        public uint CastFlagsEx;

        [DBFieldName("ammo_display_id")]
        public int AmmoDisplayId;

        [DBFieldName("ammo_inventory_type")]
        public int AmmoInventoryType;

        [DBFieldName("main_target_guid", false, true)]
        public string MainTargetGuid;

        [DBFieldName("main_target_id")]
        public uint MainTargetId;

        [DBFieldName("main_target_type")]
        public string MainTargetType;

        [DBFieldName("hit_targets_count")]
        public uint HitTargetsCount;

        [DBFieldName("hit_targets_list_id")]
        public uint HitTargetsListId;

        [DBFieldName("miss_targets_count")]
        public uint MissTargetsCount;

        [DBFieldName("miss_targets_list_id")]
        public uint MissTargetsListId;

        [DBFieldName("src_position_id")]
        public uint SrcPositionId;

        [DBFieldName("dst_position_id")]
        public uint DstPositionId;
    }

    public sealed class SpellCastData : IDataModel
    {
        public DateTime Time;

        public WowGuid CasterGuid;

        public WowGuid CasterUnitGuid;

        public uint SpellID;

        public uint VisualID;

        public uint CastTime;

        public uint CastFlags;

        public uint CastFlagsEx;

        public int AmmoDisplayId;

        public int AmmoInventoryType;

        public WowGuid MainTargetGuid;

        public uint HitTargetsCount = 0;
        public List<WowGuid> HitTargetsList;
        public void AddHitTarget(WowGuid guid)
        {
            if (HitTargetsList == null)
                HitTargetsList = new List<WowGuid>();

            HitTargetsList.Add(guid);
        }
        public uint MissTargetsCount = 0;
        public List<WowGuid> MissTargetsList;
        public void AddMissTarget(WowGuid guid)
        {
            if (MissTargetsList == null)
                MissTargetsList = new List<WowGuid>();

            MissTargetsList.Add(guid);
        }
        public Vector3 SrcPosition;
        public Vector3 DstPosition;
    }

    public sealed class SpellPetCooldown : IDataModel
    {
        [DBFieldName("creature_id", true)]
        public uint? CasterID;

        [DBFieldName("flags")]
        public byte? Flags;

        [DBFieldName("index", true)]
        public byte? Index;

        [DBFieldName("spell_id", true)]
        public uint? SpellID;

        [DBFieldName("cooldown", true)]
        public uint? Cooldown;

        [DBFieldName("mod_rate")]
        public float? ModRate;
    }

    public sealed class SpellPetActions : IDataModel
    {
        [DBFieldName("creature_id", true)]
        public uint? CasterID;

        [DBFieldName("slot", 10)]
        public uint[] SpellID = new uint[10];
    }
}

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
        public int KitId;

        [DBFieldName("kit_type", false, false, true)]
        public int? KitType;

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
        public uint Reason;

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

    [DBTableName("spell_script_target")]
    public sealed class SpellScriptTarget : IDataModel
    {
        [DBFieldName("spell_id", true, DbType = TargetedDbType.WPP)]
        [DBFieldName("entry", true, DbType = (TargetedDbType.VMANGOS | TargetedDbType.CMANGOS))]
        public uint SpellId;

        [DBFieldName("type", true, DbType = (TargetedDbType.VMANGOS | TargetedDbType.CMANGOS))]
        public uint Type;

        [DBFieldName("target_type", true, DbType = TargetedDbType.WPP)]
        public string TargetType;

        [DBFieldName("target_id", true, DbType = TargetedDbType.WPP)]
        [DBFieldName("targetEntry", true, DbType = (TargetedDbType.VMANGOS | TargetedDbType.CMANGOS))]
        public uint TargetId;

        [DBFieldName("sniff_build", DbType = TargetedDbType.WPP)]
        public int SniffBuild = ClientVersion.BuildInt;
    }

    [DBTableName("spell_unique_caster")]
    public sealed class SpellUniqueCaster : ITableWithSniffIdList
    {
        [DBFieldName("caster_id", true)]
        public uint CasterId;

        [DBFieldName("caster_type", true)]
        public string CasterType;

        [DBFieldName("spell_id", true)]
        public uint SpellId;
    }

    [DBTableName("spell_aura_flags")]
    public sealed class SpellAuraFlags : IDataModel
    {
        [DBFieldName("spell_id", true)]
        public uint SpellId;

        [DBFieldName("flags", true)]
        public uint Flags;

        [DBFieldName("sniff_build")]
        public int SniffBuild = ClientVersion.BuildInt;
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

        [DBFieldName("miss_reason")]
        public uint MissReason;
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

        [DBFieldName("orientation")]
        public float Orientation;
    }

    public enum CastDataType
    {
        Start = 0,
        Go = 1
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
        public uint MissReasonsCount = 0;
        public List<uint> MissReasonsList;
        public void AddMissReason(uint reason)
        {
            if (MissReasonsList == null)
                MissReasonsList = new List<uint>();

            MissReasonsList.Add(reason);
        }
        public Vector3? SrcPosition;
        public Vector3? DstPosition;
        public float Orientation;
    }

    [DBTableName("creature_pet_cooldown")]
    public sealed class CreaturePetCooldown : IDataModel
    {
        [DBFieldName("entry", true)]
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

        [DBFieldName("sniff_id", false, true, false, true)]
        public string SniffId;

        [DBFieldName("sniff_build")]
        public int SniffBuild = ClientVersion.BuildInt;
    }

    [DBTableName("creature_pet_remaining_cooldown")]
    public sealed class CreaturePetRemainingCooldown : IDataModel
    {
        [DBFieldName("entry", true)]
        public uint? CasterID;

        [DBFieldName("spell_id", true)]
        public uint? SpellID;

        [DBFieldName("cooldown", true)]
        public uint? Cooldown;

        [DBFieldName("category", true)]
        public uint? Category;

        [DBFieldName("category_cooldown", true)]
        public int? CategoryCooldown;

        [DBFieldName("mod_rate")]
        public float? ModRate;

        [DBFieldName("time_since_cast")]
        public uint? TimeSinceCast;

        [DBFieldName("sniff_id", false, true, false, true)]
        public string SniffId;

        [DBFieldName("sniff_build")]
        public int SniffBuild = ClientVersion.BuildInt;
    }

    [DBTableName("creature_pet_actions")]
    public sealed class CreaturePetActions : IDataModel
    {
        [DBFieldName("entry", true)]
        public uint? CasterID;

        [DBFieldName("slot", 10)]
        public uint[] SpellID = new uint[10];

        [DBFieldName("sniff_id", false, true, false, true)]
        public string SniffId;

        [DBFieldName("sniff_build")]
        public int SniffBuild = ClientVersion.BuildInt;

        public WowGuid CasterGUID;
    }

    [DBTableName("creature_spell_timers")]
    public sealed class CreatureSpellTimers : IDataModel
    {
        [DBFieldName("entry", true)]
        public uint? CasterID;

        [DBFieldName("spell_id", true)]
        public uint? SpellID;

        [DBFieldName("initial_casts_count")]
        public uint InitialCastsCount;

        [DBFieldName("initial_delay_min")]
        public uint InitialDelayMin;

        [DBFieldName("initial_delay_average")]
        public uint InitialDelayAverage;

        [DBFieldName("initial_delay_max")]
        public uint InitialDelayMax;

        [DBFieldName("repeat_casts_count")]
        public uint RepeatCastsCount;

        [DBFieldName("repeat_delay_min")]
        public uint RepeatDelayMin;

        [DBFieldName("repeat_delay_average")]
        public uint RepeatDelayAverage;

        [DBFieldName("repeat_delay_max")]
        public uint RepeatDelayMax;

        [DBFieldName("sniff_build", true)]
        public int SniffBuild = ClientVersion.BuildInt;
    }

    [DBTableName("spell_unique_chain_updates")]
    public sealed class SpellUniqueChainUpdate : ITableWithSniffIdList
    {
        [DBFieldName("spell_id", true)]
        public uint SpellId;
    }

    [DBTableName("spell_chain_update")]
    public sealed class SpellChainUpdate : IDataModel
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
        
        [DBFieldName("targets_count")]
        public uint TargetsCount;

        [DBFieldName("targets_list_id")]
        public uint TargetsListId;

        public WowGuid Guid;
        public DateTime Time;
        public List<WowGuid> TargetsList = new List<WowGuid>();
    }

    [DBTableName("spell_chain_update_target")]
    public sealed class SpellChainUpdateTarget : IDataModel
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
}

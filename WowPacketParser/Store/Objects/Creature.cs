using System;
using System.Collections.Generic;
using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.SQL;

namespace WowPacketParser.Store.Objects
{
    [DBTableName("creature")]
    public sealed class Creature : IDataModel
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
        public int? PhaseGroup;

        [DBFieldName("position_x")]
        public float? PositionX;

        [DBFieldName("position_y")]
        public float? PositionY;

        [DBFieldName("position_z")]
        public float? PositionZ;

        [DBFieldName("orientation")]
        public float? Orientation;

        [DBFieldName("wander_distance")]
        public float? WanderDistance;

        [DBFieldName("movement_type")]
        public uint? MovementType;

        [DBFieldName("hover")]
        public byte? Hover;

        [DBFieldName("temp")]
        public byte? TemporarySpawn;

        [DBFieldName("pet")]
        public byte? IsPet;

        [DBFieldName("summon_spell")]
        public uint? SummonSpell;

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

        [DBFieldName("level")]
        public uint? Level;

        [DBFieldName("npc_flags")]
        public uint? NpcFlag;

        [DBFieldName("unit_flags")]
        public uint? UnitFlag;

        [DBFieldName("unit_flags2")]
        public uint? UnitFlag2;

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

        [DBFieldName("main_hand_slot_item")]
        public uint? MainHandSlotItem;

        [DBFieldName("off_hand_slot_item")]
        public uint? OffHandSlotItem;

        [DBFieldName("ranged_slot_item")]
        public uint? RangedSlotItem;

        [DBFieldName("auras")]
        public string Auras;

        [DBFieldName("sniff_id", false, false, false, true)]
        public int? SniffId;

        [DBFieldName("sniff_build")]
        public int? SniffBuild = ClientVersion.BuildInt;
    }

    [DBTableName("creature_guid_values")]
    public sealed class CreatureGuidValues : IDataModel
    {
        [DBFieldName("guid", true, true)]
        public string GUID;

        [DBFieldName("charm_guid", false, true)]
        public string CharmGuid;

        [DBFieldName("charm_id")]
        public uint CharmId;

        [DBFieldName("charm_type")]
        public string CharmType;

        [DBFieldName("summon_guid", false, true)]
        public string SummonGuid;

        [DBFieldName("summon_id")]
        public uint SummonId;

        [DBFieldName("summon_type")]
        public string SummonType;

        [DBFieldName("charmer_guid", false, true)]
        public string CharmedByGuid;

        [DBFieldName("charmer_id")]
        public uint CharmedById;

        [DBFieldName("charmer_type")]
        public string CharmedByType;

        [DBFieldName("creator_guid", false, true)]
        public string CreatedByGuid;

        [DBFieldName("creator_id")]
        public uint CreatedById;

        [DBFieldName("creator_type")]
        public string CreatedByType;

        [DBFieldName("summoner_guid", false, true)]
        public string SummonedByGuid;

        [DBFieldName("summoner_id")]
        public uint SummonedById;

        [DBFieldName("summoner_type")]
        public string SummonedByType;

        [DBFieldName("target_guid", false, true)]
        public string TargetGuid;

        [DBFieldName("target_id")]
        public uint TargetId;

        [DBFieldName("target_type")]
        public string TargetType;
    }

    [DBTableName("creature_movement_server_spline")]
    public sealed class ServerSideMovementSpline : IDataModel
    {
        [DBFieldName("guid", true, true)]
        public string GUID;

        [DBFieldName("parent_point", true)]
        public uint ParentPoint;

        [DBFieldName("spline_point", true)]
        public uint SplinePoint;

        [DBFieldName("position_x")]
        public float PositionX;

        [DBFieldName("position_y")]
        public float PositionY;

        [DBFieldName("position_z")]
        public float PositionZ;
    }

    [DBTableName("creature_movement_server")]
    public sealed class ServerSideMovement : IDataModel
    {
        [DBFieldName("unixtimems")]
        public ulong? UnixTimeMs;

        [DBFieldName("guid", true, true)]
        public string GUID;

        [DBFieldName("point", true)]
        public uint Point;

        [DBFieldName("move_time")]
        public uint MoveTime;

        [DBFieldName("spline_flags")]
        public uint SplineFlags;

        [DBFieldName("spline_count")]
        public uint SplineCount;

        [DBFieldName("start_position_x")]
        public float StartPositionX;

        [DBFieldName("start_position_y")]
        public float StartPositionY;

        [DBFieldName("start_position_z")]
        public float StartPositionZ;  

        [DBFieldName("end_position_x")]
        public float EndPositionX;

        [DBFieldName("end_position_y")]
        public float EndPositionY;

        [DBFieldName("end_position_z")]
        public float EndPositionZ;

        [DBFieldName("orientation")]
        public float Orientation;

        public List<Vector3> SplinePoints = null;
    }

    [DBTableName("creature_create1_time")]
    public sealed class CreatureCreate1 : IDataModel
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

    [DBTableName("creature_create2_time")]
    public sealed class CreatureCreate2 : IDataModel
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

    [DBTableName("creature_destroy_time")]
    public sealed class CreatureDestroy : IDataModel
    {
        [DBFieldName("unixtimems", true)]
        public ulong UnixTimeMs;

        [DBFieldName("guid", true, true)]
        public string GUID;
    }

    [DBTableName("creature_auras_update")]
    public sealed class CreatureAurasUpdate : IDataModel
    {
        [DBFieldName("unixtimems")]
        public ulong UnixTimeMs;

        [DBFieldName("guid", true, true)]
        public string GUID;

        [DBFieldName("update_id", true)]
        public uint UpdateId;

        [DBFieldName("slot", true)]
        public uint? Slot;

        [DBFieldName("spell_id")]
        public uint SpellId;

        [DBFieldName("visual_id", false, false, true)]
        public uint VisualId;

        [DBFieldName("aura_flags")]
        public uint AuraFlags;

        [DBFieldName("active_flags", false, false, true)]
        public uint ActiveFlags;

        [DBFieldName("level")]
        public uint Level;

        [DBFieldName("charges")]
        public uint Charges;

        [DBFieldName("content_tuning_id", false, false, true)]
        public int ContentTuningId;

        [DBFieldName("duration")]
        public int Duration;

        [DBFieldName("max_duration")]
        public int MaxDuration;

        [DBFieldName("caster_guid", false, true)]
        public string CasterGuid;

        [DBFieldName("caster_id")]
        public uint CasterId;

        [DBFieldName("caster_type")]
        public string CasterType;
    }

    [DBTableName("creature_speed_update")]
    public sealed class CreatureSpeedUpdate : IDataModel
    {
        [DBFieldName("unixtimems", true)]
        public ulong UnixTimeMs;

        [DBFieldName("guid", true, true)]
        public string GUID;

        [DBFieldName("speed_type", true)]
        public SpeedType SpeedType;

        [DBFieldName("speed_rate", true)]
        public float SpeedRate;
    }

    [DBTableName("creature_values_update")]
    public sealed class CreatureValuesUpdate : IDataModel
    {
        [DBFieldName("unixtimems", true)]
        public ulong UnixTimeMs;

        [DBFieldName("guid", true, true)]
        public string GUID;

        [DBFieldName("entry", true, false, true)]
        public uint? Entry;

        [DBFieldName("scale", true, false, true)]
        public float? Scale;

        [DBFieldName("display_id", true, false, true)]
        public uint? DisplayID;

        [DBFieldName("mount_display_id", true, false, true)]
        public uint? MountDisplayID;

        [DBFieldName("faction", true, false, true)]
        public uint? FactionTemplate;

        [DBFieldName("level", true, false, true)]
        public uint? Level;

        [DBFieldName("npc_flags", true, false, true)]
        public uint? NpcFlag;

        [DBFieldName("unit_flags", true, false, true)]
        public uint? UnitFlag;

        [DBFieldName("unit_flags2", true, false, true)]
        public uint? UnitFlag2;

        [DBFieldName("current_health", true, false, true)]
        public uint? CurrentHealth;

        [DBFieldName("max_health", true, false, true)]
        public uint? MaxHealth;

        [DBFieldName("current_mana", true, false, true)]
        public uint? CurrentMana;

        [DBFieldName("max_mana", true, false, true)]
        public uint? MaxMana;

        [DBFieldName("aura_state", true, false, true)]
        public uint? AuraState;

        [DBFieldName("emote_state", true, false, true)]
        public uint? EmoteState;

        [DBFieldName("stand_state", true, false, true)]
        public uint? StandState;

        [DBFieldName("pet_talent_points", true, false, true)]
        public uint? PetTalentPoints;

        [DBFieldName("vis_flags", true, false, true)]
        public uint? VisFlags;

        [DBFieldName("anim_tier", true, false, true)]
        public uint? AnimTier;

        [DBFieldName("sheath_state", true, false, true)]
        public uint? SheathState;

        [DBFieldName("pvp_flags", true, false, true)]
        public uint? PvpFlags;

        [DBFieldName("pet_flags", true, false, true)]
        public uint? PetFlags;

        [DBFieldName("shapeshift_form", true, false, true)]
        public uint? ShapeshiftForm;

        [DBFieldName("bounding_radius", true, false, true)]
        public float? BoundingRadius;

        [DBFieldName("combat_reach", true, false, true)]
        public float? CombatReach;

        [DBFieldName("mod_melee_haste", true, false, true)]
        public float? ModMeleeHaste;

        [DBFieldName("mod_ranged_haste", true, false, true)]
        public float? ModRangedHaste;

        [DBFieldName("base_attack_time", true, false, true)]
        public uint? BaseAttackTime;

        [DBFieldName("ranged_attack_time", true, false, true)]
        public uint? RangedAttackTime;
    }

    [DBTableName("creature_guid_values_update")]
    public sealed class CreatureGuidValuesUpdate : IDataModel
    {
        [DBFieldName("unixtimems", true)]
        public ulong UnixTimeMs;

        [DBFieldName("guid", true, true)]
        public string GUID;

        [DBFieldName("field_name", true)]
        public string FieldName;

        [DBFieldName("object_guid", false, true)]
        public string ObjectGuid;

        [DBFieldName("object_id")]
        public uint ObjectId;

        [DBFieldName("object_type")]
        public string ObjectType;

        public WowGuid guid;
        public DateTime time;
    }

    [DBTableName("creature_equipment_values_update")]
    public sealed class CreatureEquipmentValuesUpdate : IDataModel
    {
        [DBFieldName("unixtimems", true)]
        public ulong UnixTimeMs;

        [DBFieldName("guid", true, true)]
        public string GUID;

        [DBFieldName("slot", true)]
        public uint? Slot;

        [DBFieldName("item_id")]
        public uint? ItemId;

        public DateTime time;
    }

    [DBTableName("creature_stats")]
    public sealed class CreatureStats : IDataModel
    {
        [DBFieldName("entry", true)]
        public uint Entry;

        [DBFieldName("level", true)]
        public uint Level;

        [DBFieldName("dmg_min", false, false, true)]
        public float? DmgMin;

        [DBFieldName("dmg_max", false, false, true)]
        public float? DmgMax;

        [DBFieldName("offhand_dmg_min", false, false, true)]
        public float? OffhandDmgMin;

        [DBFieldName("offhand_dmg_max", false, false, true)]
        public float? OffhandDmgMax;

        [DBFieldName("ranged_dmg_min", false, false, true)]
        public float? RangedDmgMin;

        [DBFieldName("ranged_dmg_max", false, false, true)]
        public float? RangedDmgMax;

        [DBFieldName("attack_power", false, false, true)]
        public uint? AttackPower;

        [DBFieldName("ranged_attack_power", false, false, true)]
        public uint? RangedAttackPower;

        [DBFieldName("base_health", false, false, true)]
        public uint? BaseHealth;

        [DBFieldName("base_mana", false, false, true)]
        public uint? BaseMana;

        [DBFieldName("strength", false, false, true)]
        public uint? Strength;

        [DBFieldName("agility", false, false, true)]
        public uint? Agility;

        [DBFieldName("stamina", false, false, true)]
        public uint? Stamina;

        [DBFieldName("intellect", false, false, true)]
        public uint? Intellect;

        [DBFieldName("spirit", false, false, true)]
        public uint? Spirit;

        [DBFieldName("positive_strength", false, false, true)]
        public uint? PositiveStrength;

        [DBFieldName("positive_agility", false, false, true)]
        public uint? PositiveAgility;

        [DBFieldName("positive_stamina", false, false, true)]
        public uint? PositiveStamina;

        [DBFieldName("positive_intellect", false, false, true)]
        public uint? PositiveIntellect;

        [DBFieldName("positive_spirit", false, false, true)]
        public uint? PositiveSpirit;

        [DBFieldName("negative_strength", false, false, true)]
        public uint? NegativeStrength;

        [DBFieldName("negative_agility", false, false, true)]
        public uint? NegativeAgility;

        [DBFieldName("negative_stamina", false, false, true)]
        public uint? NegativeStamina;

        [DBFieldName("negative_intellect", false, false, true)]
        public uint? NegativeIntellect;

        [DBFieldName("negative_spirit", false, false, true)]
        public uint? NegativeSpirit;

        [DBFieldName("armor", false, false, true)]
        public int? Armor;

        [DBFieldName("holy_res", false, false, true)]
        public int? HolyResistance;

        [DBFieldName("fire_res", false, false, true)]
        public int? FireResistance;

        [DBFieldName("nature_res", false, false, true)]
        public int? NatureResistance;

        [DBFieldName("frost_res", false, false, true)]
        public int? FrostResistance;

        [DBFieldName("shadow_res", false, false, true)]
        public int? ShadowResistance;

        [DBFieldName("arcane_res", false, false, true)]
        public int? ArcaneResistance;

        [DBFieldName("positive_armor", false, false, true)]
        public int? PositiveArmor;

        [DBFieldName("positive_holy_res", false, false, true)]
        public int? PositiveHolyResistance;

        [DBFieldName("positive_fire_res", false, false, true)]
        public int? PositiveFireResistance;

        [DBFieldName("positive_nature_res", false, false, true)]
        public int? PositiveNatureResistance;

        [DBFieldName("positive_frost_res", false, false, true)]
        public int? PositiveFrostResistance;

        [DBFieldName("positive_shadow_res", false, false, true)]
        public int? PositiveShadowResistance;

        [DBFieldName("positive_arcane_res", false, false, true)]
        public int? PositiveArcaneResistance;

        [DBFieldName("negative_armor", false, false, true)]
        public int? NegativeArmor;

        [DBFieldName("negative_holy_res", false, false, true)]
        public int? NegativeHolyResistance;

        [DBFieldName("negative_fire_res", false, false, true)]
        public int? NegativeFireResistance;

        [DBFieldName("negative_nature_res", false, false, true)]
        public int? NegativeNatureResistance;

        [DBFieldName("negative_frost_res", false, false, true)]
        public int? NegativeFrostResistance;

        [DBFieldName("negative_shadow_res", false, false, true)]
        public int? NegativeShadowResistance;

        [DBFieldName("negative_arcane_res", false, false, true)]
        public int? NegativeArcaneResistance;
    }

    [DBTableName("creature_attack_log")]
    public sealed class UnitMeleeAttackLog : IDataModel
    {
        [DBFieldName("unixtimems", true)]
        public ulong UnixTimeMs;

        [DBFieldName("guid", true, true)]
        public string GUID;

        [DBFieldName("victim_guid", false, true)]
        public string VictimGuid;

        [DBFieldName("victim_id")]
        public uint VictimId;

        [DBFieldName("victim_type")]
        public string VictimType;

        [DBFieldName("hit_info")]
        public uint HitInfo;

        [DBFieldName("damage")]
        public uint Damage;

        [DBFieldName("original_damage")]
        public uint OriginalDamage;

        [DBFieldName("overkill_damage")]
        public int OverkillDamage;

        [DBFieldName("blocked_damage")]
        public int BlockedDamage;

        [DBFieldName("victim_state")]
        public uint VictimState;

        [DBFieldName("attacker_state")]
        public int AttackerState;

        [DBFieldName("spell_id")]
        public uint SpellId;

        public WowGuid Attacker;
        public WowGuid Victim;
        public DateTime Time;
    }

    [DBTableName("creature_emote")]
    public sealed class CreatureEmote : IDataModel
    {
        [DBFieldName("unixtimems", true)]
        public ulong UnixTimeMs;

        [DBFieldName("guid", true, true)]
        public string GUID;

        [DBFieldName("emote_id")]
        public uint EmoteId;

        [DBFieldName("emote_name")]
        public string EmoteName;

        public CreatureEmote()
        {
        }
        public CreatureEmote(EmoteType emote_, DateTime time_)
        {
            emote = emote_;
            EmoteId = (uint)emote_;
            EmoteName = emote_.ToString();
            UnixTimeMs = (ulong)Utilities.GetUnixTimeMsFromDateTime(time_);
            time = time_;
        }
        public EmoteType emote;
        public DateTime time;
    }

    [DBTableName("creature_attack_start")]
    public sealed class CreatureAttackToggle : IDataModel
    {
        [DBFieldName("unixtimems", true)]
        public ulong UnixTimeMs;

        [DBFieldName("guid", true, true)]
        public string GUID;

        [DBFieldName("victim_guid", false, true)]
        public string VictimGuid;

        [DBFieldName("victim_id")]
        public uint VictimId;

        [DBFieldName("victim_type")]
        public string VictimType;
    }

    public sealed class CreatureAttackData
    {
        public CreatureAttackData(WowGuid victim_, DateTime time_)
        {
            victim = victim_;
            time = time_;
        }
        public WowGuid victim;
        public DateTime time;
    }
}

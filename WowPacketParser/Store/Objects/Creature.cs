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

        [DBFieldName("original_id", DbType = (TargetedDbType.WPP))]
        public uint? OriginalID;

        [DBFieldName("id")]
        public uint? ID;

        [DBFieldName("total_observation_time", DbType = (TargetedDbType.WPP))]
        public uint? TotalObservationTime;

        [DBFieldName("longest_observation_time", DbType = (TargetedDbType.WPP))]
        public uint? LongestObservationTime;

        [DBFieldName("map", false, false, true)]
        public uint? Map;

        [DBFieldName("zone_id", DbType = (TargetedDbType.WPP))]
        [DBFieldName("zoneId", DbType = (TargetedDbType.TRINITY))]
        public uint? ZoneID;

        [DBFieldName("area_id", DbType = (TargetedDbType.WPP))]
        [DBFieldName("areaId", DbType = (TargetedDbType.TRINITY))]
        public uint? AreaID;

        [DBFieldName("spawn_mask", TargetedDbExpansion.WrathOfTheLichKing, TargetedDbExpansion.Legion, DbType = (TargetedDbType.WPP))]
        [DBFieldName("spawnMask", TargetedDbExpansion.WrathOfTheLichKing, TargetedDbExpansion.Legion, DbType = (TargetedDbType.TRINITY | TargetedDbType.CMANGOS))]
        public uint? SpawnMask;

        [DBFieldName("spawn_difficulties", TargetedDbExpansion.Legion, DbType = (TargetedDbType.WPP))]
        [DBFieldName("spawnDifficulties", TargetedDbExpansion.Legion, DbType = (TargetedDbType.TRINITY))]
        public string SpawnDifficulties;

        [DBFieldName("phase_mask", TargetedDbExpansion.WrathOfTheLichKing, TargetedDbExpansion.Cataclysm, DbType = (TargetedDbType.WPP))]
        [DBFieldName("phaseMask", TargetedDbExpansion.WrathOfTheLichKing, TargetedDbExpansion.Cataclysm, DbType = (TargetedDbType.TRINITY | TargetedDbType.CMANGOS))]
        public uint? PhaseMask;

        [DBFieldName("phase_id", TargetedDbExpansion.Cataclysm, DbType = (TargetedDbType.WPP))]
        [DBFieldName("PhaseId", TargetedDbExpansion.Cataclysm, DbType = (TargetedDbType.TRINITY))]
        public string PhaseID;

        [DBFieldName("phase_group", TargetedDbExpansion.Cataclysm, DbType = (TargetedDbType.WPP))]
        [DBFieldName("PhaseGroup", TargetedDbExpansion.Cataclysm, DbType = (TargetedDbType.TRINITY))]
        public int? PhaseGroup;

        [DBFieldName("position_x")]
        public float? PositionX;

        [DBFieldName("position_y")]
        public float? PositionY;

        [DBFieldName("position_z")]
        public float? PositionZ;

        [DBFieldName("orientation")]
        public float? Orientation;

        [DBFieldName("spawndist", DbType = (TargetedDbType.CMANGOS))]
        [DBFieldName("wander_distance", DbType = (TargetedDbType.WPP | TargetedDbType.TRINITY | TargetedDbType.VMANGOS))]
        public float? WanderDistance;

        [DBFieldName("waypoint_count", DbType = TargetedDbType.WPP)]
        public uint? WaypointCount;

        [DBFieldName("unique_waypoint_count", DbType = TargetedDbType.WPP)]
        public uint? UniqueWaypointCount;

        [DBFieldName("movement_type", DbType = (TargetedDbType.WPP | TargetedDbType.VMANGOS))]
        [DBFieldName("MovementType", DbType = (TargetedDbType.TRINITY | TargetedDbType.CMANGOS))]
        public uint? MovementType;

        [DBFieldName("movement_flags", DbType = TargetedDbType.WPP)]
        public uint? MovementFlags;

        [DBFieldName("is_spawn", DbType = (TargetedDbType.WPP))]
        public ObjectCreateType? IsSpawn;

        [DBFieldName("is_hovering", DbType = (TargetedDbType.WPP))]
        public byte? Hover;

        [DBFieldName("is_pet", DbType = (TargetedDbType.WPP))]
        public byte? IsPet;

        [DBFieldName("is_vehicle", DbType = (TargetedDbType.WPP))]
        public byte? IsVehicle;

        [DBFieldName("is_on_transport", DbType = (TargetedDbType.WPP))]
        public byte IsOnTransport;

        [DBFieldName("is_temporary", DbType = (TargetedDbType.WPP))]
        public byte? TemporarySpawn;

        [DBFieldName("summon_spell", DbType = (TargetedDbType.WPP))]
        public uint? SummonSpell;

        [DBFieldName("scale", DbType = (TargetedDbType.WPP))]
        public float? Scale;

        [DBFieldName("display_id", DbType = (TargetedDbType.WPP))]
        [DBFieldName("modelid", DbType = (TargetedDbType.TRINITY | TargetedDbType.CMANGOS))]
        public uint? DisplayID;

        [DBFieldName("native_display_id", DbType = (TargetedDbType.WPP))]
        public uint? NativeDisplayID;

        [DBFieldName("mount_display_id", DbType = (TargetedDbType.WPP))]
        public uint? MountDisplayID;

        [DBFieldName("class", DbType = (TargetedDbType.WPP))]
        public uint? ClassId;

        [DBFieldName("gender", DbType = (TargetedDbType.WPP))]
        public uint? Gender;

        [DBFieldName("faction", DbType = (TargetedDbType.WPP))]
        public uint? FactionTemplate;

        [DBFieldName("level", DbType = (TargetedDbType.WPP))]
        public uint? Level;

        [DBFieldName("npc_flags", DbType = (TargetedDbType.WPP))]
        [DBFieldName("npcflag", DbType = (TargetedDbType.TRINITY))]
        public uint? NpcFlag;

        [DBFieldName("unit_flags", DbType = (TargetedDbType.WPP | TargetedDbType.TRINITY))]
        public uint? UnitFlag;

        [DBFieldName("unit_flags2", TargetedDbExpansion.WrathOfTheLichKingClassic, TargetedDbExpansion.Zero, DbType = (TargetedDbType.WPP))]
        [DBFieldName("unit_flags2", TargetedDbExpansion.TheBurningCrusade, DbType = (TargetedDbType.WPP))]
        public uint? UnitFlag2;

        [DBFieldName("dynamic_flags", DbType = (TargetedDbType.WPP))]
        [DBFieldName("dynamicflags", DbType = (TargetedDbType.TRINITY))]
        public uint? DynamicFlags;

        [DBFieldName("current_health", DbType = (TargetedDbType.WPP))]
        [DBFieldName("curhealth", DbType = (TargetedDbType.TRINITY | TargetedDbType.CMANGOS))]
        public uint? CurHealth;

        [DBFieldName("max_health", DbType = (TargetedDbType.WPP))]
        public uint? MaxHealth;

        [DBFieldName("power_type", DbType = (TargetedDbType.WPP))]
        public uint? PowerType;

        [DBFieldName("curmana", DbType = (TargetedDbType.TRINITY | TargetedDbType.CMANGOS))]
        public uint? CurrentMana;

        [DBFieldName("current_power", DbType = (TargetedDbType.WPP))]
        public uint? CurrentPower;

        [DBFieldName("max_power", DbType = (TargetedDbType.WPP))]
        public uint? MaxPower;

        [DBFieldName("aura_state", DbType = (TargetedDbType.WPP))]
        public uint? AuraState;

        [DBFieldName("emote_state", DbType = (TargetedDbType.WPP))]
        public uint? EmoteState;

        [DBFieldName("stand_state", DbType = (TargetedDbType.WPP))]
        public uint? StandState;

        //[DBFieldName("pet_talent_points", DbType = (TargetedDbType.WPP))]
        //public uint? PetTalentPoints;

        [DBFieldName("vis_flags", DbType = (TargetedDbType.WPP))]
        public uint? VisFlags;

        [DBFieldName("anim_tier", TargetedDbExpansion.WrathOfTheLichKingClassic, TargetedDbExpansion.Zero, DbType = (TargetedDbType.WPP))]
        [DBFieldName("anim_tier", TargetedDbExpansion.WrathOfTheLichKing, DbType = (TargetedDbType.WPP))]
        public uint? AnimTier;

        [DBFieldName("sheath_state", DbType = (TargetedDbType.WPP))]
        public uint? SheatheState;

        [DBFieldName("misc_flags", TargetedDbExpansion.Zero, TargetedDbExpansion.TheBurningCrusade, DbType = (TargetedDbType.WPP))]
        public uint? MiscFlags;

        [DBFieldName("debuff_limit", TargetedDbExpansion.TheBurningCrusade, TargetedDbExpansion.WrathOfTheLichKing, DbType = (TargetedDbType.WPP))]
        public uint? DebuffLimit;

        [DBFieldName("pvp_flags", TargetedDbExpansion.WrathOfTheLichKingClassic, TargetedDbExpansion.Zero, DbType = (TargetedDbType.WPP))]
        [DBFieldName("pvp_flags", TargetedDbExpansion.WrathOfTheLichKing, DbType = (TargetedDbType.WPP))]
        public uint? PvpFlags;

        //[DBFieldName("pet_flags", DbType = (TargetedDbType.WPP))]
        //public uint? PetFlags;

        [DBFieldName("shapeshift_form", DbType = (TargetedDbType.WPP))]
        public uint? ShapeshiftForm;

        [DBFieldName("speed_walk", DbType = (TargetedDbType.WPP))]
        public float? SpeedWalk;

        [DBFieldName("speed_run", DbType = (TargetedDbType.WPP))]
        public float? SpeedRun;

        [DBFieldName("speed_run_back", DbType = (TargetedDbType.WPP))]
        public float? SpeedRunBack;

        [DBFieldName("speed_swim", DbType = (TargetedDbType.WPP))]
        public float? SpeedSwim;

        [DBFieldName("speed_swim_back", DbType = (TargetedDbType.WPP))]
        public float? SpeedSwimBack;

        [DBFieldName("speed_fly", DbType = (TargetedDbType.WPP))]
        public float? SpeedFly;

        [DBFieldName("speed_fly_back", DbType = (TargetedDbType.WPP))]
        public float? SpeedFlyBack;

        [DBFieldName("bounding_radius", DbType = (TargetedDbType.WPP))]
        public float? BoundingRadius;

        [DBFieldName("combat_reach", DbType = (TargetedDbType.WPP))]
        public float? CombatReach;

        [DBFieldName("mod_melee_haste", DbType = (TargetedDbType.WPP))]
        public float? ModMeleeHaste;

        [DBFieldName("main_hand_attack_time", DbType = (TargetedDbType.WPP))]
        public uint? MainHandAttackTime;

        [DBFieldName("off_hand_attack_time", DbType = (TargetedDbType.WPP))]
        public uint? OffHandAttackTime;

        [DBFieldName("main_hand_slot_item", DbType = (TargetedDbType.WPP))]
        public uint? MainHandSlotItem;

        [DBFieldName("off_hand_slot_item", DbType = (TargetedDbType.WPP))]
        public uint? OffHandSlotItem;

        [DBFieldName("ranged_slot_item", DbType = (TargetedDbType.WPP))]
        public uint? RangedSlotItem;

        [DBFieldName("channel_spell_id", DbType = (TargetedDbType.WPP))]
        public uint? ChannelSpellId;

        [DBFieldName("channel_visual_id", TargetedDbExpansion.WrathOfTheLichKingClassic, TargetedDbExpansion.Zero, DbType = (TargetedDbType.WPP))]
        [DBFieldName("channel_visual_id", TargetedDbExpansion.Legion, DbType = (TargetedDbType.WPP))]
        public uint? ChannelVisualId;

        [DBFieldName("auras", DbType = (TargetedDbType.WPP))]
        public string Auras;

        [DBFieldName("sniff_id", false, true, false, true, DbType = (TargetedDbType.WPP))]
        public string SniffId;

        [DBFieldName("sniff_build", DbType = (TargetedDbType.WPP))]
        [DBFieldName("VerifiedBuild", DbType = (TargetedDbType.TRINITY))]
        public int SniffBuild = ClientVersion.BuildInt;
    }

    [DBTableName("creature_power_values")]
    public sealed class CreaturePowerValues : IDataModel
    {
        [DBFieldName("guid", true, true)]
        public string GUID;

        [DBFieldName("power_type", true)]
        public uint PowerType;

        [DBFieldName("current_power")]
        public uint CurrentPower;

        [DBFieldName("max_power")]
        public uint MaxPower;
    }

    [DBTableName("creature_power_values_update")]
    public sealed class CreaturePowerValuesUpdate : IDataModel
    {
        [DBFieldName("unixtimems", true)]
        public ulong UnixTimeMs;

        [DBFieldName("guid", true, true)]
        public string GUID;

        [DBFieldName("power_type", true)]
        public uint PowerType;

        [DBFieldName("current_power", true, false, true)]
        public uint? CurrentPower;

        [DBFieldName("max_power", true, false, true)]
        public uint? MaxPower;
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

        [DBFieldName("summoner_guid", false, true)]
        public string SummonedByGuid;

        [DBFieldName("summoner_id")]
        public uint SummonedById;

        [DBFieldName("summoner_type")]
        public string SummonedByType;

        [DBFieldName("creator_guid", false, true)]
        public string CreatedByGuid;

        [DBFieldName("creator_id")]
        public uint CreatedById;

        [DBFieldName("creator_type")]
        public string CreatedByType;

        [DBFieldName("demon_creator_guid", false, true)]
        public string DemonCreatorGuid;

        [DBFieldName("demon_creator_id")]
        public uint DemonCreatorId;

        [DBFieldName("demon_creator_type")]
        public string DemonCreatorType;

        [DBFieldName("target_guid", false, true)]
        public string TargetGuid;

        [DBFieldName("target_id")]
        public uint TargetId;

        [DBFieldName("target_type")]
        public string TargetType;
    }

    [DBTableName("creature_movement_server_spline", TargetedDbType.WPP)]
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

    [DBTableName("creature_movement_server", TargetedDbType.WPP)]
    [DBTableName("waypoint_data", TargetedDbType.TRINITY)]
    [DBTableName("creature_movement", (TargetedDbType.VMANGOS | TargetedDbType.CMANGOS))]
    public sealed class ServerSideMovement : IDataModel
    {
        [DBFieldName("unixtimems", DbType = (TargetedDbType.WPP))]
        public ulong? UnixTimeMs;

        [DBFieldName("guid", true, true, DbType = (TargetedDbType.WPP))]
        [DBFieldName("id", true, true, DbType = (TargetedDbType.TRINITY | TargetedDbType.VMANGOS | TargetedDbType.CMANGOS))]
        public string GUID;

        [DBFieldName("point", true)]
        public uint Point;

        [DBFieldName("move_time", DbType = (TargetedDbType.WPP))]
        public uint MoveTime;

        [DBFieldName("spline_flags", DbType = (TargetedDbType.WPP))]
        public uint SplineFlags;

        [DBFieldName("spline_count", DbType = (TargetedDbType.WPP))]
        public uint SplineCount;

        [DBFieldName("start_position_x", DbType = (TargetedDbType.WPP))]
        [DBFieldName("position_x", DbType = (TargetedDbType.TRINITY | TargetedDbType.VMANGOS | TargetedDbType.CMANGOS))]
        public float StartPositionX;

        [DBFieldName("start_position_y", DbType = (TargetedDbType.WPP))]
        [DBFieldName("position_y", DbType = (TargetedDbType.TRINITY | TargetedDbType.VMANGOS | TargetedDbType.CMANGOS))]
        public float StartPositionY;

        [DBFieldName("start_position_z", DbType = (TargetedDbType.WPP))]
        [DBFieldName("position_z", DbType = (TargetedDbType.TRINITY | TargetedDbType.VMANGOS | TargetedDbType.CMANGOS))]
        public float StartPositionZ;  

        [DBFieldName("end_position_x", DbType = (TargetedDbType.WPP))]
        public float EndPositionX;

        [DBFieldName("end_position_y", DbType = (TargetedDbType.WPP))]
        public float EndPositionY;

        [DBFieldName("end_position_z", DbType = (TargetedDbType.WPP))]
        public float EndPositionZ;

        [DBFieldName("orientation")]
        public float Orientation;

        [DBFieldName("transport_guid", false, true, OnlyWhenSavingTransports = true, DbType = (TargetedDbType.WPP))]
        public string TransportGUID = "0";

        [DBFieldName("transport_id", OnlyWhenSavingTransports = true, DbType = (TargetedDbType.WPP))]
        public uint TransportId;

        [DBFieldName("transport_type", OnlyWhenSavingTransports = true, DbType = (TargetedDbType.WPP))]
        public string TransportType = "";

        [DBFieldName("transport_seat", OnlyWhenSavingTransports = true, DbType = (TargetedDbType.WPP))]
        public sbyte TransportSeat;

        [DBFieldName("anim_tier", DbType = (TargetedDbType.WPP))]
        public byte AnimTier;

        [DBFieldName("vertical_speed")]
        public float VerticalSpeed;

        [DBFieldName("effect_start_time", DbType = (TargetedDbType.WPP))]
        public uint EffectStartTime;

        public WowGuid TransportGuid = WowGuid64.Empty;
        public List<Vector3> SplinePoints = null;
    }

    [DBTableName("creature_flight_spline_sync", TargetedDbType.WPP)]
    public sealed class CreatureFlightSplineSync : IDataModel
    {
        [DBFieldName("unixtimems", true)]
        public ulong UnixTimeMs;

        [DBFieldName("guid", true, true)]
        public string GUID;

        [DBFieldName("duration_percent")]
        public float DurationPercent;
    }

    [DBTableName("creature_create1_time")]
    public sealed class CreatureCreate1 : IDataModel
    {
        [DBFieldName("unixtimems", true)]
        public ulong UnixTimeMs;

        [DBFieldName("guid", true, true)]
        public string GUID;

        [DBFieldName("map")]
        public uint? Map;

        [DBFieldName("position_x")]
        public float? PositionX;

        [DBFieldName("position_y")]
        public float? PositionY;

        [DBFieldName("position_z")]
        public float? PositionZ;

        [DBFieldName("orientation")]
        public float? Orientation;

        [DBFieldName("move_time")]
        public uint MoveTime;

        [DBFieldName("move_flags")]
        public uint MoveFlags;

        [DBFieldName("move_flags2")]
        public uint MoveFlags2;

        [DBFieldName("swim_pitch")]
        public float SwimPitch;

        [DBFieldName("fall_time")]
        public uint FallTime;

        [DBFieldName("jump_horizontal_speed")]
        public float JumpHorizontalSpeed;

        [DBFieldName("jump_vertical_speed")]
        public float JumpVerticalSpeed;

        [DBFieldName("jump_cos_angle")]
        public float JumpCosAngle;

        [DBFieldName("jump_sin_angle")]
        public float JumpSinAngle;

        [DBFieldName("spline_elevation")]
        public float SplineElevation;

        [DBFieldName("vehicle_id", OnlyWhenSavingTransports = true)]
        public uint VehicleId;

        [DBFieldName("vehicle_orientation", OnlyWhenSavingTransports = true)]
        public float VehicleOrientation;

        [DBFieldName("transport_guid", false, true, OnlyWhenSavingTransports = true)]
        public string TransportGuid = "0";

        [DBFieldName("transport_id", OnlyWhenSavingTransports = true)]
        public uint TransportId;

        [DBFieldName("transport_type", OnlyWhenSavingTransports = true)]
        public string TransportType = "";

        [DBFieldName("transport_x", OnlyWhenSavingTransports = true)]
        public float TransportPositionX;

        [DBFieldName("transport_y", OnlyWhenSavingTransports = true)]
        public float TransportPositionY;

        [DBFieldName("transport_z", OnlyWhenSavingTransports = true)]
        public float TransportPositionZ;

        [DBFieldName("transport_o", OnlyWhenSavingTransports = true)]
        public float TransportOrientation;

        [DBFieldName("transport_time", OnlyWhenSavingTransports = true)]
        public uint TransportTime;

        [DBFieldName("transport_seat", OnlyWhenSavingTransports = true)]
        public sbyte TransportSeat;
    }

    [DBTableName("creature_create2_time")]
    public sealed class CreatureCreate2 : IDataModel
    {
        [DBFieldName("unixtimems", true)]
        public ulong UnixTimeMs;

        [DBFieldName("guid", true, true)]
        public string GUID;

        [DBFieldName("map")]
        public uint? Map;

        [DBFieldName("position_x")]
        public float? PositionX;

        [DBFieldName("position_y")]
        public float? PositionY;

        [DBFieldName("position_z")]
        public float? PositionZ;

        [DBFieldName("orientation")]
        public float? Orientation;

        [DBFieldName("move_time")]
        public uint MoveTime;

        [DBFieldName("move_flags")]
        public uint MoveFlags;

        [DBFieldName("move_flags2")]
        public uint MoveFlags2;

        [DBFieldName("swim_pitch")]
        public float SwimPitch;

        [DBFieldName("fall_time")]
        public uint FallTime;

        [DBFieldName("jump_horizontal_speed")]
        public float JumpHorizontalSpeed;

        [DBFieldName("jump_vertical_speed")]
        public float JumpVerticalSpeed;

        [DBFieldName("jump_cos_angle")]
        public float JumpCosAngle;

        [DBFieldName("jump_sin_angle")]
        public float JumpSinAngle;

        [DBFieldName("spline_elevation")]
        public float SplineElevation;

        [DBFieldName("vehicle_id", OnlyWhenSavingTransports = true)]
        public uint VehicleId;

        [DBFieldName("vehicle_orientation", OnlyWhenSavingTransports = true)]
        public float VehicleOrientation;

        [DBFieldName("transport_guid", false, true, OnlyWhenSavingTransports = true)]
        public string TransportGuid = "0";

        [DBFieldName("transport_id", OnlyWhenSavingTransports = true)]
        public uint TransportId;

        [DBFieldName("transport_type", OnlyWhenSavingTransports = true)]
        public string TransportType = "";

        [DBFieldName("transport_x", OnlyWhenSavingTransports = true)]
        public float TransportPositionX;

        [DBFieldName("transport_y", OnlyWhenSavingTransports = true)]
        public float TransportPositionY;

        [DBFieldName("transport_z", OnlyWhenSavingTransports = true)]
        public float TransportPositionZ;

        [DBFieldName("transport_o", OnlyWhenSavingTransports = true)]
        public float TransportOrientation;

        [DBFieldName("transport_time", OnlyWhenSavingTransports = true)]
        public uint TransportTime;

        [DBFieldName("transport_seat", OnlyWhenSavingTransports = true)]
        public sbyte TransportSeat;
    }

    [DBTableName("creature_destroy_time")]
    public sealed class CreatureDestroy : IDataModel
    {
        [DBFieldName("unixtimems", true)]
        public ulong UnixTimeMs;

        [DBFieldName("guid", true, true)]
        public string GUID;
    }

    public class AuraUpdateData
    {
        public bool IsFullUpdate;
        public List<Aura> Auras;
        public DateTime Time;
        public AuraUpdateData(bool isFullUpdate, List<Aura> auras, DateTime time)
        {
            IsFullUpdate = isFullUpdate;
            Auras = auras;
            Time = time;
        }
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

        [DBFieldName("is_full_update")]
        public bool IsFullUpdate;

        [DBFieldName("slot", true)]
        public int Slot;

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

        [DBFieldName("content_tuning_id", TargetedDbExpansion.BattleForAzeroth, false, false, true)]
        public int ContentTuningId;

        [DBFieldName("duration")]
        public int Duration;

        [DBFieldName("max_duration")]
        public int MaxDuration;

        [DBFieldName("caster_guid", false, true)]
        public string CasterGuid = "0";

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

        [DBFieldName("dynamic_flags", true, false, true)]
        public uint? DynamicFlags;

        [DBFieldName("current_health", true, false, true)]
        public uint? CurrentHealth;

        [DBFieldName("max_health", true, false, true)]
        public uint? MaxHealth;

        [DBFieldName("power_type", true, false, true)]
        public uint? PowerType;

        [DBFieldName("aura_state", true, false, true)]
        public uint? AuraState;

        [DBFieldName("emote_state", true, false, true)]
        public uint? EmoteState;

        [DBFieldName("stand_state", true, false, true)]
        public uint? StandState;

        //[DBFieldName("pet_talent_points", true, false, true)]
        //public uint? PetTalentPoints;

        [DBFieldName("vis_flags", true, false, true)]
        public uint? VisFlags;

        [DBFieldName("anim_tier", TargetedDbExpansion.WrathOfTheLichKingClassic, TargetedDbExpansion.Zero, true, false, true)]
        [DBFieldName("anim_tier", TargetedDbExpansion.WrathOfTheLichKing, true, false, true)]
        public uint? AnimTier;

        [DBFieldName("sheath_state", true, false, true)]
        public uint? SheathState;

        [DBFieldName("pvp_flags", true, false, true)]
        public uint? PvpFlags;

        //[DBFieldName("pet_flags", true, false, true)]
        //public uint? PetFlags;

        [DBFieldName("shapeshift_form", true, false, true)]
        public uint? ShapeshiftForm;

        [DBFieldName("bounding_radius", true, false, true)]
        public float? BoundingRadius;

        [DBFieldName("combat_reach", true, false, true)]
        public float? CombatReach;

        [DBFieldName("mod_melee_haste", true, false, true)]
        public float? ModMeleeHaste;

        [DBFieldName("main_hand_attack_time", true, false, true)]
        public uint? MainHandAttackTime;

        [DBFieldName("off_hand_attack_time", true, false, true)]
        public uint? OffHandAttackTime;

        [DBFieldName("channel_spell_id", true, false, true)]
        public uint? ChannelSpellId;

        [DBFieldName("channel_visual_id", true, false, true)]
        public uint? ChannelVisualId;
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

        [DBFieldName("object_guid", true, true)]
        public string ObjectGuid;

        [DBFieldName("object_id", true)]
        public uint ObjectId;

        [DBFieldName("object_type", true)]
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

        [DBFieldName("is_pet", true)]
        public bool IsPet;

        [DBFieldName("is_dirty", true)]
        public bool IsDirty;

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
        public int? AttackPower;

        [DBFieldName("positive_attack_power", false, false, true)]
        public int? PositiveAttackPower;

        [DBFieldName("negative_attack_power", false, false, true)]
        public int? NegativeAttackPower;

        [DBFieldName("attack_power_multiplier", false, false, true)]
        public float? AttackPowerMultiplier;

        [DBFieldName("ranged_attack_power", false, false, true)]
        public int? RangedAttackPower;

        [DBFieldName("positive_ranged_attack_power", false, false, true)]
        public int? PositiveRangedAttackPower;

        [DBFieldName("negative_ranged_attack_power", false, false, true)]
        public int? NegativeRangedAttackPower;

        [DBFieldName("ranged_attack_power_multiplier", false, false, true)]
        public float? RangedAttackPowerMultiplier;

        [DBFieldName("base_health", false, false, true)]
        public uint? BaseHealth;

        [DBFieldName("base_mana", false, false, true)]
        public uint? BaseMana;

        [DBFieldName("strength", false, false, true)]
        public int? Strength;

        [DBFieldName("agility", false, false, true)]
        public int? Agility;

        [DBFieldName("stamina", false, false, true)]
        public int? Stamina;

        [DBFieldName("intellect", false, false, true)]
        public int? Intellect;

        [DBFieldName("spirit", false, false, true)]
        public int? Spirit;

        [DBFieldName("positive_strength", false, false, true)]
        public int? PositiveStrength;

        [DBFieldName("positive_agility", false, false, true)]
        public int? PositiveAgility;

        [DBFieldName("positive_stamina", false, false, true)]
        public int? PositiveStamina;

        [DBFieldName("positive_intellect", false, false, true)]
        public int? PositiveIntellect;

        [DBFieldName("positive_spirit", false, false, true)]
        public int? PositiveSpirit;

        [DBFieldName("negative_strength", false, false, true)]
        public int? NegativeStrength;

        [DBFieldName("negative_agility", false, false, true)]
        public int? NegativeAgility;

        [DBFieldName("negative_stamina", false, false, true)]
        public int? NegativeStamina;

        [DBFieldName("negative_intellect", false, false, true)]
        public int? NegativeIntellect;

        [DBFieldName("negative_spirit", false, false, true)]
        public int? NegativeSpirit;

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

        [DBFieldName("sniff_id", false, true, false, true)]
        public string SniffId;

        [DBFieldName("sniff_build")]
        public int SniffBuild = ClientVersion.BuildInt;

        public string Auras;
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
        public int Damage;

        [DBFieldName("original_damage")]
        public int OriginalDamage;

        [DBFieldName("overkill_damage")]
        public int OverkillDamage;

        [DBFieldName("sub_damage_count")]
        public uint SubDamageCount;

        [DBFieldName("total_school_mask")]
        public uint TotalSchoolMask;

        [DBFieldName("total_absorbed_damage")]
        public int TotalAbsorbedDamage;

        [DBFieldName("total_resisted_damage")]
        public int TotalResistedDamage;

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

    [DBTableName("creature_threat_update")]
    public sealed class CreatureThreatUpdate : IDataModel
    {
        [DBFieldName("unixtimems", true)]
        public ulong UnixTimeMs;

        [DBFieldName("guid", true, true)]
        public string GUID;

        [DBFieldName("target_count")]
        public uint TargetsCount;

        [DBFieldName("target_list_id", true)]
        public uint TargetListId;

        public DateTime Time;
        public List<Tuple<WowGuid, long>> TargetsList;
    }

    [DBTableName("creature_threat_update_target")]
    public sealed class CreatureThreatUpdateTarget : IDataModel
    {
        [DBFieldName("list_id", true)]
        public uint TargetListId;

        [DBFieldName("target_guid", true, true)]
        public string TargetGuid;

        [DBFieldName("target_id")]
        public uint TargetId;

        [DBFieldName("target_type", true)]
        public string TargetType;

        [DBFieldName("threat", true)]
        public long Threat;
    }

    [DBTableName("creature_threat_clear")]
    public sealed class CreatureThreatClear : IDataModel
    {
        [DBFieldName("unixtimems", true)]
        public ulong UnixTimeMs;

        [DBFieldName("guid", true, true)]
        public string GUID;

        public DateTime Time;
    }

    [DBTableName("creature_threat_remove")]
    public sealed class CreatureThreatRemove : IDataModel
    {
        [DBFieldName("unixtimems", true)]
        public ulong UnixTimeMs;

        [DBFieldName("guid", true, true)]
        public string GUID;

        [DBFieldName("target_guid", true, true)]
        public string TargetGuid;

        [DBFieldName("target_id")]
        public uint TargetId;

        [DBFieldName("target_type", true)]
        public string TargetType;

        public DateTime Time;
        public WowGuid TargetGUID;
    }

    [DBTableName("creature_pet_name")]
    public sealed class CreaturePetName : IDataModel
    {
        [DBFieldName("guid", true, true)]
        public string GUID;

        [DBFieldName("name")]
        public string Name;
    }

    [DBTableName("creature_spell_immunity")]
    public sealed class CreatureSpellImmunity : ITableWithSniffIdList
    {
        [DBFieldName("entry", true)]
        public uint Entry;

        [DBFieldName("spell_id", true)]
        public uint SpellID;
    }

    [DBTableName("creature_unique_spell_hit")]
    public sealed class CreatureUniqueSpellHit : ITableWithSniffIdList
    {
        [DBFieldName("entry", true)]
        public uint Entry;

        [DBFieldName("spell_id", true)]
        public uint SpellID;
    }
    
    [DBTableName("creature_respawn_time")]
    public sealed class CreatureRespawnTime : IDataModel
    {
        [DBFieldName("old_guid", true, true)]
        public string OldGUID;

        [DBFieldName("new_guid", true, true)]
        public string NewGUID;

        [DBFieldName("respawn_time")]
        public uint RespawnTime;
    }

    [DBTableName("creature_kill_reputation")]
    public sealed class CreatureKillReputation : IDataModel
    {
        [DBFieldName("entry", true)]
        public uint Entry;

        [DBFieldName("reputation_list_id", true, true)]
        public uint ReputationListId;

        [DBFieldName("old_standing", true)]
        public int OldStanding;

        [DBFieldName("new_standing", true)]
        public int NewStanding;

        [DBFieldName("player_level")]
        public uint PlayerLevel;

        [DBFieldName("player_race")]
        public uint PlayerRace;

        [DBFieldName("sniff_id", true, true, false, true)]
        public string SniffId;

        [DBFieldName("sniff_build", true)]
        public int SniffBuild = ClientVersion.BuildInt;
    }

    [DBTableName("creature_aggro_distance")]
    public sealed class CreatureAggroDistance : IDataModel
    {
        [DBFieldName("entry", true)]
        public uint Entry;

        [DBFieldName("creature_level")]
        public uint CreatureLevel;

        [DBFieldName("player_level")]
        public uint PlayerLevel;

        [DBFieldName("aggro_distance", true)]
        public float AggroDistance;

        [DBFieldName("expected_distance")]
        public float ExpectedDistance;

        [DBFieldName("sniff_id", true, true, false, true)]
        public string SniffId;

        [DBFieldName("sniff_build", true)]
        public int SniffBuild = ClientVersion.BuildInt;
    }

    [DBTableName("creature_unique_ammo")]
    public sealed class CreatureUniqueAmmo : ITableWithSniffIdList
    {
        [DBFieldName("entry", true)]
        public uint Entry;

        [DBFieldName("ammo_display_id", true)]
        public uint AmmoDisplayId;

        [DBFieldName("ammo_inventory_type", true)]
        public uint AmmoInventoryType;

        [DBFieldName("spell_id")]
        public uint SpellId;
    }
}

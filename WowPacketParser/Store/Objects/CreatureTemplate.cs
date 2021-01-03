using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.SQL;

namespace WowPacketParser.Store.Objects
{
    [DBTableName("creature_template_wdb")]
    public sealed class CreatureTemplate : IDataModel
    {
        [DBFieldName("entry", true)]
        public uint? Entry;

        [DBFieldName("KillCredit", 2)]
        public uint?[] KillCredits;

        [DBFieldName("display_id", TargetedDatabase.Zero, TargetedDatabase.BattleForAzeroth, 4)]
        public uint?[] ModelIDs;

        [DBFieldName("name")]
        public string Name;

        [DBFieldName("femaleName", TargetedDatabase.Cataclysm)]
        public string FemaleName;

        [DBFieldName("subname", nullable: true)]
        public string SubName;

        [DBFieldName("TitleAlt", TargetedDatabase.WarlordsOfDraenor /*Mists of Pandaria*/, nullable: true)]
        public string TitleAlt;

        [DBFieldName("IconName", nullable: true)]
        public string IconName;

        [DBFieldName("HealthScalingExpansion", TargetedDatabase.WarlordsOfDraenor)]
        public ClientType? HealthScalingExpansion;

        [DBFieldName("RequiredExpansion", TargetedDatabase.Cataclysm)]
        public ClientType? RequiredExpansion;

        [DBFieldName("VignetteID", TargetedDatabase.Legion)]
        public uint? VignetteID;

        [DBFieldName("unit_class", TargetedDatabase.BattleForAzeroth)]
        public uint? UnitClass;

        [DBFieldName("FadeRegionRadius", TargetedDatabase.BattleForAzeroth, TargetedDatabase.Shadowlands)]
        public float? FadeRegionRadius;

        [DBFieldName("WidgetSetID", TargetedDatabase.BattleForAzeroth)]
        public int? WidgetSetID;

        [DBFieldName("WidgetSetUnitConditionID", TargetedDatabase.BattleForAzeroth)]
        public int? WidgetSetUnitConditionID;

        [DBFieldName("rank")]
        public CreatureRank? Rank;

        [DBFieldName("family")]
        public CreatureFamily? Family;

        [DBFieldName("type")]
        public CreatureType? Type;

        [DBFieldName("type_flags")]
        public CreatureTypeFlag? TypeFlags;

        [DBFieldName("type_flags2", TargetedDatabase.Cataclysm)]
        public uint? TypeFlags2;

        [DBFieldName("PetSpellDataId", TargetedDatabase.Zero, TargetedDatabase.Cataclysm)]
        public uint? PetSpellDataID;

        [DBFieldName("HealthMultiplier")]
        public float? HealthMultiplier;

        [DBFieldName("ManaMultiplier")]
        public float? ManaMultiplier;

        [DBFieldName("RacialLeader")]
        public bool? RacialLeader;

        [DBFieldName("movementId")]
        public uint? MovementID;

        [DBFieldName("VerifiedBuild")]
        public int? VerifiedBuild = ClientVersion.BuildInt;
    }

    [DBTableName("creature_template")]
    public sealed class CreatureTemplateNonWDB : IDataModel
    {
        [DBFieldName("entry", true)]
        public uint? Entry;

        [DBFieldName("gossip_menu_id")]
        public uint? GossipMenuId;

        [DBFieldName("level_min")]
        public int? MinLevel;

        [DBFieldName("level_max")]
        public int? MaxLevel;

        [DBFieldName("faction")]
        public uint? Faction;

        [DBFieldName("npc_flags")]
        public NPCFlags? NpcFlag;

        [DBFieldName("speed_walk")]
        public float? SpeedWalk;

        [DBFieldName("speed_run")]
        public float? SpeedRun;

        [DBFieldName("scale")]
        public float? Scale;

        [DBFieldName("base_attack_time")]
        public uint? BaseAttackTime;

        [DBFieldName("ranged_attack_time")]
        public uint? RangedAttackTime;

        [DBFieldName("unit_class", TargetedDatabase.Zero, TargetedDatabase.BattleForAzeroth)]
        public uint? UnitClass;

        [DBFieldName("unit_flags")]
        public UnitFlags? UnitFlags;

        [DBFieldName("unit_flags2")]
        public UnitFlags2? UnitFlags2;

        [DBFieldName("unit_flags3", TargetedDatabase.Legion)]
        public UnitFlags3? UnitFlags3;

        [DBFieldName("dynamic_flags", TargetedDatabase.Zero, TargetedDatabase.WarlordsOfDraenor)]
        public UnitDynamicFlags? DynamicFlags;

        [DBFieldName("dynamic_flags", TargetedDatabase.WarlordsOfDraenor)]
        public UnitDynamicFlagsWOD? DynamicFlagsWod;

        [DBFieldName("vehicle_id")]
        public uint? VehicleID;

        [DBFieldName("hover_height")]
        public float? HoverHeight;

        [DBFieldName("auras")]
        public string Auras;
    }

    [DBTableName("creature_questitem")]
    public sealed class CreatureTemplateQuestItem : IDataModel
    {
        [DBFieldName("CreatureEntry", true)]
        public uint? CreatureEntry;

        [DBFieldName("Idx", true)]
        public uint? Idx;

        [DBFieldName("ItemId")]
        public uint? ItemId;

        [DBFieldName("VerifiedBuild")]
        public int? VerifiedBuild = ClientVersion.BuildInt;
    }

    [DBTableName("creature_template_wdb")]
    public sealed class CreatureTemplateClassic : IDataModel
    {
        [DBFieldName("entry", true)]
        public uint? Entry;

        [DBFieldName("kill_credit", 2)]
        public uint?[] KillCredits;

        [DBFieldName("display_total_count")]
        public uint DisplayTotalCount;

        [DBFieldName("display_total_probability")]
        public float DisplayTotalProbability;

        [DBFieldName("display_id1")]
        public uint DisplayId1;

        [DBFieldName("display_id2")]
        public uint DisplayId2;

        [DBFieldName("display_id3")]
        public uint DisplayId3;

        [DBFieldName("display_id4")]
        public uint DisplayId4;

        [DBFieldName("display_scale1")]
        public float DisplayScale1;

        [DBFieldName("display_scale2")]
        public float DisplayScale2;

        [DBFieldName("display_scale3")]
        public float DisplayScale3;

        [DBFieldName("display_scale4")]
        public float DisplayScale4;

        [DBFieldName("display_probability1")]
        public float DisplayProbability1;

        [DBFieldName("display_probability2")]
        public float DisplayProbability2;

        [DBFieldName("display_probability3")]
        public float DisplayProbability3;

        [DBFieldName("display_probability4")]
        public float DisplayProbability4;

        [DBFieldName("name")]
        public string Name;

        [DBFieldName("female_name")]
        public string FemaleName;

        [DBFieldName("subname", nullable: true)]
        public string SubName;

        [DBFieldName("title_alt", nullable: true)]
        public string TitleAlt;

        [DBFieldName("icon_name", nullable: true)]
        public string IconName;

        [DBFieldName("health_scaling_expansion")]
        public ClientType? HealthScalingExpansion;

        [DBFieldName("required_expansion")]
        public ClientType? RequiredExpansion;

        [DBFieldName("vignette_id")]
        public uint? VignetteID;

        [DBFieldName("unit_class")]
        public uint? UnitClass;

        [DBFieldName("rank")]
        public CreatureRank? Rank;

        [DBFieldName("beast_family")]
        public CreatureFamily? Family;

        [DBFieldName("type")]
        public CreatureType? Type;

        [DBFieldName("type_flags")]
        public CreatureTypeFlag? TypeFlags;

        [DBFieldName("type_flags2")]
        public uint? TypeFlags2;

        [DBFieldName("pet_spell_list_id")]
        public uint? PetSpellDataID;

        [DBFieldName("health_multiplier")]
        public float? HealthMultiplier;

        [DBFieldName("mana_multiplier")]
        public float? ManaMultiplier;

        [DBFieldName("civilian")]
        public bool? Civilian;

        [DBFieldName("racial_leader")]
        public bool? RacialLeader;

        [DBFieldName("movement_id")]
        public uint? MovementID;

        [DBFieldName("sniff_build")]
        public int? SniffBuild = ClientVersion.BuildInt;
    }
}

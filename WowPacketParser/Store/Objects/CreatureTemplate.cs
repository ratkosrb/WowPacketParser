using System;
using System.Collections.Generic;
using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.SQL;

namespace WowPacketParser.Store.Objects
{
    [DBTableName("creature_template")]
    public sealed class CreatureTemplateNonWDB : IDataModel
    {
        [DBFieldName("entry", true, DbType = (TargetedDbType.WPP | TargetedDbType.TRINITY | TargetedDbType.VMANGOS))]
        [DBFieldName("Entry", true, DbType = (TargetedDbType.CMANGOS))]
        public uint? Entry;

        [DBFieldName("gossip_menu_id", TargetedDbExpansion.WrathOfTheLichKingClassic, TargetedDbExpansion.Zero, DbType = (TargetedDbType.WPP | TargetedDbType.TRINITY | TargetedDbType.VMANGOS))]
        [DBFieldName("GossipMenuId", TargetedDbExpansion.WrathOfTheLichKingClassic, TargetedDbExpansion.Zero, DbType = (TargetedDbType.CMANGOS))]
        [DBFieldName("gossip_menu_id", TargetedDbExpansion.TheBurningCrusade, DbType = (TargetedDbType.WPP | TargetedDbType.TRINITY | TargetedDbType.VMANGOS))]
        [DBFieldName("GossipMenuId", TargetedDbExpansion.TheBurningCrusade, DbType = (TargetedDbType.CMANGOS))]
        public uint? GossipMenuId;

        [DBFieldName("level_min", DbType = (TargetedDbType.WPP | TargetedDbType.VMANGOS))]
        [DBFieldName("minlevel", DbType = (TargetedDbType.TRINITY))]
        [DBFieldName("MinLevel", DbType = (TargetedDbType.CMANGOS))]
        public int? MinLevel;

        [DBFieldName("level_max", DbType = (TargetedDbType.WPP | TargetedDbType.VMANGOS))]
        [DBFieldName("maxlevel", DbType = (TargetedDbType.TRINITY))]
        [DBFieldName("MaxLevel", DbType = (TargetedDbType.CMANGOS))]
        public int? MaxLevel;

        [DBFieldName("faction", DbType = (TargetedDbType.WPP | TargetedDbType.TRINITY | TargetedDbType.VMANGOS))]
        [DBFieldName("Faction", DbType = (TargetedDbType.CMANGOS))]
        public uint? Faction;

        [DBFieldName("npc_flags", DbType = (TargetedDbType.WPP | TargetedDbType.VMANGOS))]
        [DBFieldName("npcflag", DbType = (TargetedDbType.TRINITY))]
        [DBFieldName("NpcFlags", DbType = (TargetedDbType.CMANGOS))]
        public NPCFlags? NpcFlag;

        [DBFieldName("speed_walk", DbType = (TargetedDbType.WPP | TargetedDbType.TRINITY | TargetedDbType.VMANGOS))]
        [DBFieldName("SpeedWalk", DbType = (TargetedDbType.CMANGOS))]
        public float? SpeedWalk;

        [DBFieldName("speed_run", DbType = (TargetedDbType.WPP | TargetedDbType.TRINITY | TargetedDbType.VMANGOS))]
        [DBFieldName("SpeedRun", DbType = (TargetedDbType.CMANGOS))]
        public float? SpeedRun;

        [DBFieldName("scale", DbType = (TargetedDbType.WPP | TargetedDbType.TRINITY | TargetedDbType.VMANGOS))]
        [DBFieldName("Scale", DbType = (TargetedDbType.CMANGOS))]
        public float? Scale;

        [DBFieldName("base_attack_time", DbType = (TargetedDbType.WPP | TargetedDbType.VMANGOS))]
        [DBFieldName("BaseAttackTime", DbType = (TargetedDbType.TRINITY))]
        [DBFieldName("MeleeBaseAttackTime", DbType = (TargetedDbType.CMANGOS))]
        public uint? BaseAttackTime;

        [DBFieldName("unit_class", DbType = (TargetedDbType.WPP | TargetedDbType.VMANGOS))]
        [DBFieldName("unit_class", TargetedDbExpansion.Zero, TargetedDbExpansion.BattleForAzeroth, DbType = (TargetedDbType.TRINITY))]
        [DBFieldName("UnitClass", DbType = (TargetedDbType.CMANGOS))]
        public uint? UnitClass;

        [DBFieldName("unit_flags", DbType = (TargetedDbType.WPP | TargetedDbType.VMANGOS | TargetedDbType.TRINITY))]
        [DBFieldName("UnitFlags", DbType = (TargetedDbType.CMANGOS))]
        public UnitFlags? UnitFlags;

        [DBFieldName("unit_flags2", TargetedDbExpansion.WrathOfTheLichKingClassic, TargetedDbExpansion.Zero, DbType = (TargetedDbType.WPP | TargetedDbType.TRINITY))]
        [DBFieldName("unit_flags2", TargetedDbExpansion.TheBurningCrusade, DbType = (TargetedDbType.WPP | TargetedDbType.TRINITY))]
        public UnitFlags2? UnitFlags2;

        [DBFieldName("unit_flags3", TargetedDbExpansion.Legion, DbType = (TargetedDbType.WPP | TargetedDbType.TRINITY))]
        public UnitFlags3? UnitFlags3;

        [DBFieldName("dynamicflags", TargetedDbExpansion.Zero, TargetedDbExpansion.WarlordsOfDraenor, DbType = (TargetedDbType.TRINITY))]
        [DBFieldName("DynamicFlags", TargetedDbExpansion.Zero, TargetedDbExpansion.WarlordsOfDraenor, DbType = (TargetedDbType.CMANGOS))]
        public UnitDynamicFlags? DynamicFlags;

        [DBFieldName("dynamicflags", TargetedDbExpansion.WrathOfTheLichKingClassic, TargetedDbExpansion.Zero, DbType = (TargetedDbType.TRINITY))]
        [DBFieldName("DynamicFlags", TargetedDbExpansion.WrathOfTheLichKingClassic, TargetedDbExpansion.Zero, DbType = (TargetedDbType.CMANGOS))]
        [DBFieldName("dynamicflags", TargetedDbExpansion.WarlordsOfDraenor, DbType = (TargetedDbType.TRINITY))]
        [DBFieldName("DynamicFlags", TargetedDbExpansion.WarlordsOfDraenor, DbType = (TargetedDbType.CMANGOS))]
        public UnitDynamicFlagsWOD? DynamicFlagsWod;

        [DBFieldName("vehicle_id", TargetedDbExpansion.WrathOfTheLichKingClassic, TargetedDbExpansion.Zero, DbType = (TargetedDbType.WPP))]
        [DBFieldName("VehicleId", TargetedDbExpansion.WrathOfTheLichKingClassic, TargetedDbExpansion.Zero, DbType = (TargetedDbType.TRINITY))]
        [DBFieldName("VehicleTemplateId", TargetedDbExpansion.WrathOfTheLichKingClassic, TargetedDbExpansion.Zero, DbType = (TargetedDbType.CMANGOS))]
        [DBFieldName("vehicle_id", TargetedDbExpansion.WrathOfTheLichKing, DbType = (TargetedDbType.WPP))]
        [DBFieldName("VehicleId", TargetedDbExpansion.WrathOfTheLichKing, DbType = (TargetedDbType.TRINITY))]
        [DBFieldName("VehicleTemplateId", TargetedDbExpansion.WrathOfTheLichKing, DbType = (TargetedDbType.CMANGOS))]
        public uint? VehicleID;

        [DBFieldName("hover_height", TargetedDbExpansion.WrathOfTheLichKingClassic, TargetedDbExpansion.Zero, DbType = (TargetedDbType.WPP))]
        [DBFieldName("HoverHeight", TargetedDbExpansion.WrathOfTheLichKingClassic, TargetedDbExpansion.Zero, DbType = (TargetedDbType.TRINITY))]
        [DBFieldName("hover_height", TargetedDbExpansion.WrathOfTheLichKing, DbType = (TargetedDbType.WPP))]
        [DBFieldName("HoverHeight", TargetedDbExpansion.WrathOfTheLichKing, DbType = (TargetedDbType.TRINITY))]
        public float? HoverHeight;

        [DBFieldName("auras", DbType = (TargetedDbType.WPP | TargetedDbType.VMANGOS))]
        public string Auras;

        [DBFieldName("sniff_build")]
        public int SniffBuild = ClientVersion.BuildInt;
    }

    [DBTableName("creature_unique_emote")]
    public sealed class CreatureUniqueEmote : ITableWithSniffIdList
    {
        [DBFieldName("entry", true)]
        public uint Entry;

        [DBFieldName("emote_id", true)]
        public uint EmoteId;

        [DBFieldName("emote_name")]
        public string EmoteName;
    }

    [DBTableName("creature_unique_faction")]
    public sealed class CreatureUniqueFaction : ITableWithSniffIdList
    {
        [DBFieldName("entry", true)]
        public uint Entry;

        [DBFieldName("faction", true)]
        public uint Faction;
    }

    [DBTableName("creature_melee_damage")]
    public sealed class CreatureMeleeDamage : IDataModel
    {
        [DBFieldName("entry", true)]
        public uint Entry;

        [DBFieldName("level", true)]
        public uint Level;

        [DBFieldName("is_dirty", true)]
        public bool IsDirty;

        [DBFieldName("hits_count")]
        public uint HitsCount;

        [DBFieldName("damage_min")]
        public int DamageMin;

        [DBFieldName("damage_average")]
        public int DamageAverage;

        [DBFieldName("damage_max")]
        public int DamageMax;

        [DBFieldName("total_school_mask")]
        public uint TotalSchoolMask;

        [DBFieldName("sniff_build", true)]
        public int SniffBuild = ClientVersion.BuildInt;
    }

    public sealed class CreatureDamageTaken
    {
        public uint attackerLevel;
        public double damage;
        public double originalDamage;
        public CreatureDamageTaken(uint attackerLevel, double damage, double originalDamage)
        {
            this.attackerLevel = attackerLevel;
            this.damage = damage;
            this.originalDamage = originalDamage;
        }
    }

    [DBTableName("creature_armor")]
    public sealed class CreatureArmor : IDataModel
    {
        [DBFieldName("entry", true)]
        public uint Entry;

        [DBFieldName("level", true)]
        public uint Level;

        [DBFieldName("hits_count")]
        public uint HitsCount;

        [DBFieldName("armor")]
        public uint Armor;

        [DBFieldName("damage_reduction")]
        public float DamageReduction;

        [DBFieldName("sniff_build", true)]
        public int SniffBuild = ClientVersion.BuildInt;
    }

    [DBTableName("creature_quest_item", TargetedDbType.WPP)]
    [DBTableName("creature_questitem", TargetedDbType.TRINITY)]
    public sealed class CreatureTemplateQuestItem : IDataModel
    {
        [DBFieldName("entry", true, DbType = (TargetedDbType.WPP))]
        [DBFieldName("CreatureEntry", true, DbType = (TargetedDbType.TRINITY))]
        public uint? CreatureEntry;

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

    [DBTableName("creature_template_wdb", TargetedDbType.WPP)]
    [DBTableName("creature_template", TargetedDbType.TRINITY | TargetedDbType.VMANGOS | TargetedDbType.CMANGOS)]
    public sealed class CreatureTemplate : IDataModel
    {
        [DBFieldName("entry", true, DbType = (TargetedDbType.WPP | TargetedDbType.TRINITY | TargetedDbType.VMANGOS))]
        [DBFieldName("Entry", true, DbType = (TargetedDbType.CMANGOS))]
        public uint? Entry;

        [DBFieldName("sniff_build", true, DbType = (TargetedDbType.WPP))]
        [DBFieldName("VerifiedBuild", false, DbType = (TargetedDbType.TRINITY))]
        public int SniffBuild = ClientVersion.BuildInt;

        [DBFieldName("kill_credit", TargetedDbExpansion.WrathOfTheLichKingClassic, TargetedDbExpansion.Zero, 2, DbType = (TargetedDbType.WPP))]
        [DBFieldName("kill_credit", TargetedDbExpansion.WrathOfTheLichKing, 2, DbType = (TargetedDbType.WPP))]
        [DBFieldName("KillCredit", TargetedDbExpansion.WrathOfTheLichKingClassic, TargetedDbExpansion.Zero, 2, DbType = (TargetedDbType.TRINITY | TargetedDbType.CMANGOS))]
        [DBFieldName("KillCredit", TargetedDbExpansion.WrathOfTheLichKing, 2, DbType = (TargetedDbType.TRINITY | TargetedDbType.CMANGOS))]
        public uint?[] KillCredits;

        [DBFieldName("display_total_count", TargetedDbExpansion.WrathOfTheLichKingClassic, TargetedDbExpansion.Zero, DbType = (TargetedDbType.WPP | TargetedDbType.VMANGOS))]
        [DBFieldName("display_total_count", TargetedDbExpansion.BattleForAzeroth, DbType = (TargetedDbType.WPP | TargetedDbType.VMANGOS))]
        public uint DisplayTotalCount;

        [DBFieldName("display_total_probability", TargetedDbExpansion.WrathOfTheLichKingClassic, TargetedDbExpansion.Zero, DbType = (TargetedDbType.WPP | TargetedDbType.VMANGOS))]
        [DBFieldName("display_total_probability", TargetedDbExpansion.BattleForAzeroth, DbType = (TargetedDbType.WPP | TargetedDbType.VMANGOS))]
        public float DisplayTotalProbability;

        // 8 display ids sent in wotlk classic - tbc classic
        [DBFieldName("display_id", TargetedDbExpansion.WrathOfTheLichKingClassic, TargetedDbExpansion.BurningCrusadeClassic, 8, DbType = (TargetedDbType.WPP | TargetedDbType.VMANGOS))]
        [DBFieldName("modelid", TargetedDbExpansion.WrathOfTheLichKingClassic, TargetedDbExpansion.BurningCrusadeClassic, 8, DbType = (TargetedDbType.TRINITY))]
        [DBFieldName("ModelId", TargetedDbExpansion.WrathOfTheLichKingClassic, TargetedDbExpansion.BurningCrusadeClassic, 8, DbType = (TargetedDbType.CMANGOS))]
        // 4 display ids sent in tbc classic - vanilla classic
        [DBFieldName("display_id", TargetedDbExpansion.BurningCrusadeClassic, TargetedDbExpansion.Zero, 4, DbType = (TargetedDbType.WPP | TargetedDbType.VMANGOS))]
        [DBFieldName("modelid", TargetedDbExpansion.BurningCrusadeClassic, TargetedDbExpansion.Zero, 4, DbType = (TargetedDbType.TRINITY))]
        [DBFieldName("ModelId", TargetedDbExpansion.BurningCrusadeClassic, TargetedDbExpansion.Zero, 4, DbType = (TargetedDbType.CMANGOS))]
        // only 1 display id sent in vanilla
        [DBFieldName("display_id", TargetedDbExpansion.Zero, TargetedDbExpansion.TheBurningCrusade, 1, DbType = (TargetedDbType.WPP | TargetedDbType.VMANGOS))]
        [DBFieldName("modelid", TargetedDbExpansion.Zero, TargetedDbExpansion.TheBurningCrusade, 1, DbType = (TargetedDbType.TRINITY))]
        [DBFieldName("ModelId", TargetedDbExpansion.Zero, TargetedDbExpansion.TheBurningCrusade, 1, DbType = (TargetedDbType.CMANGOS))]
        // 4 display ids sent in tbc - legion
        [DBFieldName("display_id", TargetedDbExpansion.TheBurningCrusade, TargetedDbExpansion.BattleForAzeroth, 4, DbType = (TargetedDbType.WPP | TargetedDbType.VMANGOS))]
        [DBFieldName("modelid", TargetedDbExpansion.TheBurningCrusade, TargetedDbExpansion.BattleForAzeroth, 4, DbType = (TargetedDbType.TRINITY))]
        [DBFieldName("ModelId", TargetedDbExpansion.TheBurningCrusade, TargetedDbExpansion.BattleForAzeroth, 4, DbType = (TargetedDbType.CMANGOS))]
        public uint[] DisplayIDs;

        [DBFieldName("display_scale", TargetedDbExpansion.WrathOfTheLichKingClassic, TargetedDbExpansion.BurningCrusadeClassic, 8, DbType = (TargetedDbType.WPP | TargetedDbType.VMANGOS))]
        [DBFieldName("display_scale", TargetedDbExpansion.BurningCrusadeClassic, TargetedDbExpansion.Zero, 4, DbType = (TargetedDbType.WPP | TargetedDbType.VMANGOS))]
        public float[] DisplayScale;

        [DBFieldName("display_probability", TargetedDbExpansion.WrathOfTheLichKingClassic, TargetedDbExpansion.BurningCrusadeClassic, 8, DbType = (TargetedDbType.WPP | TargetedDbType.VMANGOS))]
        [DBFieldName("display_probability", TargetedDbExpansion.BurningCrusadeClassic, TargetedDbExpansion.Zero, 4, DbType = (TargetedDbType.WPP | TargetedDbType.VMANGOS))]
        public float[] DisplayProbability;

        [DBFieldName("name", DbType = (TargetedDbType.WPP | TargetedDbType.TRINITY | TargetedDbType.VMANGOS))]
        [DBFieldName("Name", DbType = (TargetedDbType.CMANGOS))]
        public string Name;

        [DBFieldName("female_name", TargetedDbExpansion.WrathOfTheLichKingClassic, TargetedDbExpansion.Zero, Nullable = true, DbType = (TargetedDbType.WPP))]
        [DBFieldName("female_name", TargetedDbExpansion.Cataclysm, Nullable = true, DbType = (TargetedDbType.WPP))]
        [DBFieldName("femaleName", TargetedDbExpansion.WrathOfTheLichKingClassic, TargetedDbExpansion.Zero, Nullable = true, DbType = (TargetedDbType.TRINITY))]
        [DBFieldName("femaleName", TargetedDbExpansion.Cataclysm, Nullable = true, DbType = (TargetedDbType.TRINITY))]
        public string FemaleName;

        [DBFieldName("subname", nullable: true, DbType = (TargetedDbType.WPP | TargetedDbType.TRINITY | TargetedDbType.VMANGOS))]
        [DBFieldName("SubName", nullable: true, DbType = (TargetedDbType.CMANGOS))]
        public string SubName;

        [DBFieldName("title_alt", TargetedDbExpansion.WrathOfTheLichKingClassic, TargetedDbExpansion.Zero, nullable: true, DbType = (TargetedDbType.WPP))]
        [DBFieldName("title_alt", TargetedDbExpansion.WarlordsOfDraenor /*Mists of Pandaria*/, nullable: true, DbType = (TargetedDbType.WPP))]
        [DBFieldName("TitleAlt", TargetedDbExpansion.WrathOfTheLichKingClassic, TargetedDbExpansion.Zero, nullable: true, DbType = (TargetedDbType.TRINITY))]
        [DBFieldName("TitleAlt", TargetedDbExpansion.WarlordsOfDraenor /*Mists of Pandaria*/, nullable: true, DbType = (TargetedDbType.TRINITY))]
        public string TitleAlt;

        [DBFieldName("icon_name", TargetedDbExpansion.WrathOfTheLichKingClassic, TargetedDbExpansion.Zero, nullable: true, DbType = (TargetedDbType.WPP))]
        [DBFieldName("IconName", TargetedDbExpansion.WrathOfTheLichKingClassic, TargetedDbExpansion.Zero, nullable: true, DbType = (TargetedDbType.TRINITY | TargetedDbType.CMANGOS))]
        [DBFieldName("icon_name", TargetedDbExpansion.TheBurningCrusade, nullable: true, DbType = (TargetedDbType.WPP))]
        [DBFieldName("IconName", TargetedDbExpansion.TheBurningCrusade, nullable: true, DbType = (TargetedDbType.TRINITY | TargetedDbType.CMANGOS))]
        public string IconName;

        [DBFieldName("health_scaling_expansion", TargetedDbExpansion.WrathOfTheLichKingClassic, TargetedDbExpansion.Zero, Nullable = true, DbType = (TargetedDbType.WPP))]
        [DBFieldName("health_scaling_expansion", TargetedDbExpansion.WarlordsOfDraenor, Nullable = true, DbType = (TargetedDbType.TRINITY))]
        [DBFieldName("HealthScalingExpansion", TargetedDbExpansion.WrathOfTheLichKingClassic, TargetedDbExpansion.Zero, Nullable = true, DbType = (TargetedDbType.TRINITY))]
        [DBFieldName("HealthScalingExpansion", TargetedDbExpansion.WarlordsOfDraenor, Nullable = true, DbType = (TargetedDbType.TRINITY))]
        public ClientType? HealthScalingExpansion;

        [DBFieldName("required_expansion", TargetedDbExpansion.WrathOfTheLichKingClassic, TargetedDbExpansion.Zero, Nullable = true, DbType = (TargetedDbType.WPP))]
        [DBFieldName("required_expansion", TargetedDbExpansion.Cataclysm, Nullable = true, DbType = (TargetedDbType.WPP))]
        [DBFieldName("RequiredExpansion", TargetedDbExpansion.WrathOfTheLichKingClassic, TargetedDbExpansion.Zero, Nullable = true, DbType = (TargetedDbType.TRINITY))]
        [DBFieldName("RequiredExpansion", TargetedDbExpansion.Cataclysm, Nullable = true, DbType = (TargetedDbType.TRINITY))]
        public ClientType? RequiredExpansion;

        [DBFieldName("vignette_id", TargetedDbExpansion.WrathOfTheLichKingClassic, TargetedDbExpansion.Zero, Nullable = true, DbType = (TargetedDbType.WPP))]
        [DBFieldName("vignette_id", TargetedDbExpansion.Legion, Nullable = true, DbType = (TargetedDbType.WPP))]
        [DBFieldName("VignetteID", TargetedDbExpansion.WrathOfTheLichKingClassic, TargetedDbExpansion.Zero, Nullable = true, DbType = (TargetedDbType.TRINITY))]
        [DBFieldName("VignetteID", TargetedDbExpansion.Legion, Nullable = true, DbType = (TargetedDbType.TRINITY))]
        public uint? VignetteID;

        [DBFieldName("unit_class", TargetedDbExpansion.WrathOfTheLichKingClassic, TargetedDbExpansion.Zero, Nullable = true, DbType = (TargetedDbType.WPP | TargetedDbType.TRINITY | TargetedDbType.VMANGOS))]
        [DBFieldName("unit_class", TargetedDbExpansion.BattleForAzeroth, Nullable = true, DbType = (TargetedDbType.WPP | TargetedDbType.TRINITY | TargetedDbType.VMANGOS))]
        [DBFieldName("UnitClass", TargetedDbExpansion.WrathOfTheLichKingClassic, TargetedDbExpansion.Zero, Nullable = true, DbType = (TargetedDbType.CMANGOS))]
        [DBFieldName("UnitClass", TargetedDbExpansion.BattleForAzeroth, Nullable = true, DbType = (TargetedDbType.CMANGOS))]
        public uint? UnitClass;

        [DBFieldName("FadeRegionRadius", TargetedDbExpansion.BattleForAzeroth, TargetedDbExpansion.Shadowlands, Nullable = true, DbType = (TargetedDbType.TRINITY))]
        public float? FadeRegionRadius;

        [DBFieldName("difficulty_id", TargetedDbExpansion.Shadowlands, Nullable = true, DbType = (TargetedDbType.WPP))]
        [DBFieldName("CreatureDifficultyID", TargetedDbExpansion.Shadowlands, Nullable = true, DbType = (TargetedDbType.TRINITY))]
        public int? DifficultyID;

        [DBFieldName("widget_set_id", TargetedDbExpansion.BattleForAzeroth, Nullable = true, DbType = (TargetedDbType.WPP))]
        [DBFieldName("WidgetSetID", TargetedDbExpansion.BattleForAzeroth, Nullable = true, DbType = (TargetedDbType.TRINITY))]
        public int? WidgetSetID;

        [DBFieldName("widget_set_unit_condition_id", TargetedDbExpansion.BattleForAzeroth, Nullable = true, DbType = (TargetedDbType.WPP))]
        [DBFieldName("WidgetSetUnitConditionID", TargetedDbExpansion.BattleForAzeroth, Nullable = true, DbType = (TargetedDbType.TRINITY))]
        public int? WidgetSetUnitConditionID;

        [DBFieldName("rank", DbType = (TargetedDbType.WPP | TargetedDbType.TRINITY | TargetedDbType.VMANGOS))]
        [DBFieldName("Rank", DbType = (TargetedDbType.CMANGOS))]
        public CreatureRank? Rank;

        [DBFieldName("pet_family", DbType = (TargetedDbType.WPP | TargetedDbType.VMANGOS))]
        [DBFieldName("family", DbType = (TargetedDbType.TRINITY))]
        [DBFieldName("Family", DbType = (TargetedDbType.CMANGOS))]
        public CreatureFamily? Family;

        [DBFieldName("type", DbType = (TargetedDbType.WPP | TargetedDbType.TRINITY | TargetedDbType.VMANGOS))]
        [DBFieldName("CreatureType", DbType = (TargetedDbType.CMANGOS))]
        public CreatureType? Type;

        [DBFieldName("static_flags1", Nullable = true, DbType = (TargetedDbType.WPP | TargetedDbType.VMANGOS))]
        public CreatureStaticFlag1? StaticFlags1;

        [DBFieldName("type_flags", Nullable = true, DbType = (TargetedDbType.WPP | TargetedDbType.TRINITY | TargetedDbType.VMANGOS))]
        [DBFieldName("CreatureTypeFlags", Nullable = true, DbType = (TargetedDbType.CMANGOS))]
        public CreatureTypeFlag? TypeFlags;

        [DBFieldName("type_flags2", TargetedDbExpansion.WrathOfTheLichKingClassic, TargetedDbExpansion.Zero, Nullable = true, DbType = (TargetedDbType.WPP | TargetedDbType.TRINITY))]
        [DBFieldName("type_flags2", TargetedDbExpansion.Cataclysm, Nullable = true, DbType = (TargetedDbType.WPP | TargetedDbType.TRINITY))]
        public uint? TypeFlags2;

        [DBFieldName("pet_spell_list_id", TargetedDbExpansion.WrathOfTheLichKingClassic, TargetedDbExpansion.Cataclysm, Nullable = true, DbType = (TargetedDbType.WPP | TargetedDbType.VMANGOS))]
        [DBFieldName("PetSpellDataId", TargetedDbExpansion.WrathOfTheLichKingClassic, TargetedDbExpansion.Cataclysm, Nullable = true, DbType = (TargetedDbType.TRINITY | TargetedDbType.CMANGOS))]
        public uint? PetSpellDataID;

        [DBFieldName("health_multiplier", TargetedDbExpansion.WrathOfTheLichKingClassic, TargetedDbExpansion.Zero, DbType = (TargetedDbType.WPP))]
        [DBFieldName("HealthModifier", TargetedDbExpansion.WrathOfTheLichKingClassic, TargetedDbExpansion.Zero, DbType = (TargetedDbType.TRINITY))]
        [DBFieldName("HealthMultiplier", TargetedDbExpansion.WrathOfTheLichKingClassic, TargetedDbExpansion.Zero, DbType = (TargetedDbType.CMANGOS))]
        [DBFieldName("health_multiplier", TargetedDbExpansion.TheBurningCrusade, DbType = (TargetedDbType.WPP))]
        [DBFieldName("HealthModifier", TargetedDbExpansion.TheBurningCrusade, DbType = (TargetedDbType.TRINITY))]
        [DBFieldName("HealthMultiplier", TargetedDbExpansion.TheBurningCrusade, DbType = (TargetedDbType.CMANGOS))]
        public float? HealthMultiplier;

        [DBFieldName("mana_multiplier", TargetedDbExpansion.WrathOfTheLichKingClassic, TargetedDbExpansion.Zero, DbType = (TargetedDbType.WPP))]
        [DBFieldName("ManaModifier", TargetedDbExpansion.WrathOfTheLichKingClassic, TargetedDbExpansion.Zero, DbType = (TargetedDbType.TRINITY))]
        [DBFieldName("PowerMultiplier", TargetedDbExpansion.WrathOfTheLichKingClassic, TargetedDbExpansion.Zero, DbType = (TargetedDbType.CMANGOS))]
        [DBFieldName("mana_multiplier", TargetedDbExpansion.TheBurningCrusade, DbType = (TargetedDbType.WPP))]
        [DBFieldName("ManaModifier", TargetedDbExpansion.TheBurningCrusade, DbType = (TargetedDbType.TRINITY))]
        [DBFieldName("PowerMultiplier", TargetedDbExpansion.TheBurningCrusade,  DbType = (TargetedDbType.CMANGOS))]
        public float? ManaMultiplier;

        [DBFieldName("civilian", TargetedDbExpansion.WrathOfTheLichKingClassic, TargetedDbExpansion.TheBurningCrusade, Nullable = true, DbType = (TargetedDbType.WPP | TargetedDbType.VMANGOS))]
        public bool? Civilian;

        [DBFieldName("racial_leader", Nullable = true, DbType = (TargetedDbType.WPP | TargetedDbType.VMANGOS))]
        [DBFieldName("RacialLeader", Nullable = true, DbType = (TargetedDbType.TRINITY | TargetedDbType.CMANGOS))]
        public bool? RacialLeader;

        [DBFieldName("movement_id", TargetedDbExpansion.WrathOfTheLichKingClassic, TargetedDbExpansion.Zero, Nullable = true, DbType = (TargetedDbType.WPP))]
        [DBFieldName("movementId", TargetedDbExpansion.WrathOfTheLichKingClassic, TargetedDbExpansion.Zero, Nullable = true, DbType = (TargetedDbType.TRINITY))]
        [DBFieldName("movement_id", TargetedDbExpansion.WrathOfTheLichKing, Nullable = true, DbType = (TargetedDbType.WPP))]
        [DBFieldName("movementId", TargetedDbExpansion.WrathOfTheLichKing, Nullable = true, DbType = (TargetedDbType.TRINITY))]
        public uint? MovementID;
    }
}

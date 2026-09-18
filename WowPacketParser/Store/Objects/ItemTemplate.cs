using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.SQL;

namespace WowPacketParser.Store.Objects
{
    [DBTableName("item_template")]
    public sealed class ItemTemplate : IDataModel
    {
        [DBFieldName("entry", true)]
        public uint? Entry;

        [DBFieldName("class")]
        public ItemClass? Class;

        [DBFieldName("subclass")]
        public uint? SubClass;

        [DBFieldName("sound_override_subclass", TargetedDbExpansion.TheBurningCrusade, Nullable = true)]
        public int? SoundOverrideSubclass;

        [DBFieldName("name")]
        public string Name;

        [DBFieldName("display_id")]
        public uint? DisplayID;

        [DBFieldName("quality")]
        public ItemQuality? Quality;

        [DBFieldName("flags")]
        public ItemProtoFlags? Flags;

        [DBFieldName("flags2", TargetedDbExpansion.WrathOfTheLichKing, Nullable = true)]
        public ItemFlagExtra? FlagsExtra;

        [DBFieldName("unk_430_1", TargetedDbExpansion.Cataclysm)]
        public float? Unk430_1;

        [DBFieldName("unk_430_2", TargetedDbExpansion.Cataclysm)]
        public float? Unk430_2;

        [DBFieldName("buy_count", TargetedDbExpansion.Cataclysm)]
        public uint? BuyCount;

        [DBFieldName("buy_price")]
        public long? BuyPrice;

        [DBFieldName("sell_price")]
        public uint? SellPrice;

        [DBFieldName("inventory_type")]
        public InventoryType? InventoryType;

        [DBFieldName("allowable_class")]
        public ClassMask? AllowedClasses;

        [DBFieldName("allowable_race")]
        public RaceMask? AllowedRaces;

        [DBFieldName("item_level")]
        public uint? ItemLevel;

        [DBFieldName("required_level")]
        public uint? RequiredLevel;

        [DBFieldName("required_skill")]
        public uint? RequiredSkillId;

        [DBFieldName("required_skill_rank")]
        public uint? RequiredSkillLevel;

        [DBFieldName("required_spell")]
        public uint? RequiredSpell;

        [DBFieldName("required_honor_rank")]
        public uint? RequiredHonorRank;

        [DBFieldName("required_city_rank")]
        public uint? RequiredCityRank;

        [DBFieldName("required_reputation_faction", false, false, true)]
        public uint? RequiredRepFaction;

        [DBFieldName("required_reputation_rank", false, false, true)]
        public uint? RequiredRepValue;

        [DBFieldName("max_count")]
        public int? MaxCount;

        [DBFieldName("stackable")]
        public int? MaxStackSize;

        [DBFieldName("container_slots")]
        public uint? ContainerSlots;

        [DBFieldName("stats_count", TargetedDbExpansion.WrathOfTheLichKing, TargetedDbExpansion.Cataclysm, Nullable = true)]
        public uint? StatsCount;

        [DBFieldName("stat_type", 10, Nullable = true)]
        public ItemModType?[] StatTypes;

        [DBFieldName("stat_value", 10, Nullable = true)]
        public int?[] StatValues;

        [DBFieldName("scaling_value", TargetedDbExpansion.Cataclysm, 10)]
        public int?[] ScalingValue;

        [DBFieldName("socket_cost_rate", TargetedDbExpansion.Cataclysm, 10)]
        public int?[] SocketCostRate;

        [DBFieldName("scaling_stat_distribution", TargetedDbExpansion.WrathOfTheLichKing)]
        public int? ScalingStatDistribution;

        [DBFieldName("scaling_stat_value", TargetedDbExpansion.WrathOfTheLichKing, TargetedDbExpansion.Cataclysm)]
        public uint? ScalingStatValue;

        [DBFieldName("dmg_min", TargetedDbExpansion.Zero, TargetedDbExpansion.WrathOfTheLichKing, 5, Nullable = true)]
        [DBFieldName("dmg_min", TargetedDbExpansion.WrathOfTheLichKing, TargetedDbExpansion.Cataclysm, 2, Nullable = true)]
        public float?[] DamageMins;

        [DBFieldName("dmg_max", TargetedDbExpansion.Zero, TargetedDbExpansion.WrathOfTheLichKing, 5, Nullable = true)]
        [DBFieldName("dmg_max", TargetedDbExpansion.WrathOfTheLichKing, TargetedDbExpansion.Cataclysm, 2, Nullable = true)]
        public float?[] DamageMaxs;

        [DBFieldName("dmg_type", TargetedDbExpansion.Zero, TargetedDbExpansion.WrathOfTheLichKing, 5, Nullable = true)]
        [DBFieldName("dmg_type", TargetedDbExpansion.WrathOfTheLichKing, TargetedDbExpansion.Cataclysm, 2, Nullable = true)]
        public DamageType?[] DamageTypes;

        [DBFieldName("armor", TargetedDbExpansion.Zero, TargetedDbExpansion.Cataclysm)]
        public uint? Armor;

        [DBFieldName("holy_res", TargetedDbExpansion.Zero, TargetedDbExpansion.Cataclysm)]
        public uint? HolyResistance;

        [DBFieldName("fire_res", TargetedDbExpansion.Zero, TargetedDbExpansion.Cataclysm)]
        public uint? FireResistance;

        [DBFieldName("nature_res", TargetedDbExpansion.Zero, TargetedDbExpansion.Cataclysm)]
        public uint? NatureResistance;

        [DBFieldName("frost_res", TargetedDbExpansion.Zero, TargetedDbExpansion.Cataclysm)]
        public uint? FrostResistance;

        [DBFieldName("shadow_res", TargetedDbExpansion.Zero, TargetedDbExpansion.Cataclysm)]
        public uint? ShadowResistance;

        [DBFieldName("arcane_res", TargetedDbExpansion.Zero, TargetedDbExpansion.Cataclysm)]
        public uint? ArcaneResistance;

        [DBFieldName("damage_type", TargetedDbExpansion.Cataclysm)]
        public DamageType? DamageType;

        [DBFieldName("delay")]
        public uint? Delay;

        [DBFieldName("ammo_type", TargetedDbExpansion.Zero, TargetedDbExpansion.Cataclysm)]
        public AmmoType? AmmoType;

        [DBFieldName("range_mod", false, false, true)]
        public float? RangedMod;

        [DBFieldName("spellid_", 5)]
        public int?[] TriggeredSpellIds;

        [DBFieldName("spelltrigger_", 5)]
        public ItemSpellTriggerType?[] TriggeredSpellTypes;

        [DBFieldName("spellcharges_", 5)]
        public int?[] TriggeredSpellCharges;

        [DBFieldName("spellcooldown_", 5)]
        public int?[] TriggeredSpellCooldowns;

        [DBFieldName("spellcategory_", 5)]
        public uint?[] TriggeredSpellCategories;

        [DBFieldName("spellcategorycooldown_", 5)]
        public int?[] TriggeredSpellCategoryCooldowns;

        [DBFieldName("bonding")]
        public ItemBonding? Bonding;

        [DBFieldName("description")]
        public string Description;

        [DBFieldName("page_text")]
        public uint? PageText;

        [DBFieldName("page_language")]
        public Language? Language;

        [DBFieldName("page_material")]
        public PageMaterial? PageMaterial;

        [DBFieldName("start_quest")]
        public uint? StartQuestId;

        [DBFieldName("lock_id")]
        public uint? LockId;

        [DBFieldName("material")]
        public Material? Material;

        [DBFieldName("sheath")]
        public SheathType? SheathType;

        [DBFieldName("random_property")]
        public int? RandomProperty;

        [DBFieldName("random_suffix", TargetedDbExpansion.TheBurningCrusade, Nullable = true)]
        public uint? RandomSuffix;

        [DBFieldName("block", TargetedDbExpansion.Zero, TargetedDbExpansion.Cataclysm)]
        public uint? Block;

        [DBFieldName("set_id")]
        public uint? ItemSet;

        [DBFieldName("max_durability")]
        public uint? MaxDurability;

        [DBFieldName("area_bound")]
        public uint? AreaID;

        [DBFieldName("map_bound", false, false, true)]
        public int? MapID;

        [DBFieldName("bag_family", false, false, true)]
        public BagFamilyMask? BagFamily;

        [DBFieldName("totem_category", TargetedDbExpansion.TheBurningCrusade, Nullable = true)]
        public TotemCategory? TotemCategory;

        [DBFieldName("socket_color_", TargetedDbExpansion.TheBurningCrusade, 3, Nullable = true)]
        public ItemSocketColor?[] ItemSocketColors;

        [DBFieldName("socket_content_", TargetedDbExpansion.TheBurningCrusade, 3, Nullable = true)]
        public uint?[] SocketContent;

        [DBFieldName("socket_bonus", TargetedDbExpansion.TheBurningCrusade, Nullable = true)]
        public int? SocketBonus;

        [DBFieldName("gem_properties", TargetedDbExpansion.TheBurningCrusade, Nullable = true)]
        public int? GemProperties;

        [DBFieldName("required_disenchant_skill", TargetedDbExpansion.TheBurningCrusade, TargetedDbExpansion.Cataclysm, Nullable = true)]
        public int? RequiredDisenchantSkill;

        [DBFieldName("armor_damage_modifier", TargetedDbExpansion.TheBurningCrusade, Nullable = true)]
        public float? ArmorDamageModifier;

        [DBFieldName("duration", TargetedDbExpansion.TheBurningCrusade, Nullable = true)]
        public uint? Duration;

        [DBFieldName("item_limit_category", TargetedDbExpansion.WrathOfTheLichKing, Nullable = true)]
        public int? ItemLimitCategory;

        [DBFieldName("holiday_id", TargetedDbExpansion.WrathOfTheLichKing, Nullable = true)]
        public Holiday? HolidayID;

        [DBFieldName("stat_scaling_factor", TargetedDbExpansion.Cataclysm, Nullable = true)]
        public float? StatScalingFactor;

        [DBFieldName("currency_substitution_id", TargetedDbExpansion.Cataclysm, Nullable = true)]
        public uint? CurrencySubstitutionID;

        [DBFieldName("currency_substitution_count", TargetedDbExpansion.Cataclysm, Nullable = true)]
        public uint? CurrencySubstitutionCount;

        [DBFieldName("sniff_build")]
        public int? VerifiedBuild = ClientVersion.BuildInt;
    }
}

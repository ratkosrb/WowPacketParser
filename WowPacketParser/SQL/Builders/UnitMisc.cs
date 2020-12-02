using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Text;
using WowPacketParser.Enums;
using WowPacketParser.Enums.Version;
using WowPacketParser.Hotfix;
using WowPacketParser.Misc;
using WowPacketParser.Store;
using WowPacketParser.Store.Objects;

namespace WowPacketParser.SQL.Builders
{
    [BuilderClass]
    public static class UnitMisc
    {
        [BuilderMethod(Units = true)]
        public static string CreatureTemplateAddon(Dictionary<WowGuid, Unit> units)
        {
            if (units.Count == 0)
                return string.Empty;

            if (!Settings.SqlTables.creature_template_addon)
                return string.Empty;

            var addons = new DataBag<CreatureTemplateAddon>();
            foreach (var unit in units)
            {
                var npc = unit.Value;

                if (Settings.AreaFilters.Length > 0)
                    if (!(npc.Area.ToString(CultureInfo.InvariantCulture).MatchesFilters(Settings.AreaFilters)))
                        continue;

                if (Settings.MapFilters.Length > 0)
                    if (!(npc.Map.ToString(CultureInfo.InvariantCulture).MatchesFilters(Settings.MapFilters)))
                        continue;

                var auras = string.Empty;
                var commentAuras = string.Empty;
                if (npc.Auras != null && npc.Auras.Count != 0)
                {
                    foreach (var aura in npc.Auras.Where(aura =>
                        aura != null &&
                        (ClientVersion.AddedInVersion(ClientType.MistsOfPandaria) ?
                            aura.AuraFlags.HasAnyFlag(AuraFlagMoP.NoCaster) :
                            aura.AuraFlags.HasAnyFlag(AuraFlag.NotCaster))))
                    {
                        auras += aura.SpellId + " ";
                        commentAuras += StoreGetters.GetName(StoreNameType.Spell, (int) aura.SpellId, false) + ", ";
                    }
                    auras = auras.TrimEnd(' ');
                    commentAuras = commentAuras.TrimEnd(',', ' ');
                }

                var addon = new CreatureTemplateAddon
                {
                    Entry = unit.Key.GetEntry(),
                    MountID = (uint)npc.UnitData.MountDisplayID,
                    Bytes1 = npc.Bytes1,
                    StandState = npc.UnitData.StandState,
                    PetTalentPoints = npc.UnitData.PetTalentPoints,
                    VisFlags = npc.UnitData.VisFlags,
                    AnimTier = npc.UnitData.AnimTier,
                    Bytes2 = npc.Bytes2,
                    SheatheState = npc.UnitData.SheatheState,
                    PvpFlags = npc.UnitData.PvpFlags,
                    PetFlags = npc.UnitData.PetFlags,
                    ShapeshiftForm = npc.UnitData.ShapeshiftForm,
                    Emote = 0,
                    AIAnimKit = npc.AIAnimKit.GetValueOrDefault(0),
                    MovementAnimKit = npc.MovementAnimKit.GetValueOrDefault(0),
                    MeleeAnimKit = npc.MeleeAnimKit.GetValueOrDefault(0),
                    Auras = auras,
                    CommentAuras = commentAuras
                };

                if (addons.ContainsKey(addon))
                    continue;

                addons.Add(addon);
            }
            
            var addonsDb = SQLDatabase.Get(addons);
            return SQLUtil.Compare(addons, addonsDb,
                addon =>
                {
                    var comment = StoreGetters.GetName(StoreNameType.Unit, (int)addon.Entry.GetValueOrDefault());
                    if (!string.IsNullOrEmpty(addon.CommentAuras))
                        comment += " - " + addon.CommentAuras;
                    return comment;
                });
        }

        public static Dictionary<uint, Tuple<int, int>> GetScalingDeltaLevels(Dictionary<WowGuid, Unit> units)
        {
            if (units.Count == 0)
                return null;

            var entries = units.GroupBy(unit => unit.Key.GetEntry());
            var list = new Dictionary<uint, List<int>>();

            foreach (var pair in entries.SelectMany(entry => entry))
            {
                if (list.ContainsKey(pair.Key.GetEntry()))
                    list[pair.Key.GetEntry()].Add(pair.Value.UnitData.ScalingLevelDelta);
                else
                    list.Add(pair.Key.GetEntry(), new List<int> { pair.Value.UnitData.ScalingLevelDelta });
            }

            var result = list.ToDictionary(pair => pair.Key, pair => Tuple.Create(pair.Value.Min(), pair.Value.Max()));

            return result.Count == 0 ? null : result;
        }

        [BuilderMethod(true, Units = true)]
        public static string CreatureTemplateScalingData(Dictionary<WowGuid, Unit> units)
        {
            if (units.Count == 0)
                return string.Empty;

            if (!Settings.SqlTables.creature_template_scaling)
                return string.Empty;

            var scalingdeltalevels = GetScalingDeltaLevels(units);

            foreach (var unit in units)
            {
                if (Storage.CreatureTemplateScalings.Any(creature => creature.Item1.Entry == unit.Key.GetEntry()))
                    continue;

                var npc = unit.Value;

                var minLevel = (uint)npc.UnitData.ScalingLevelMin;
                var maxLevel = (uint)npc.UnitData.ScalingLevelMax;
                var contentTuningID = npc.UnitData.ContentTuningID;

                if (minLevel != 0 || maxLevel != 0 || contentTuningID != 0)
                {
                    Storage.CreatureTemplateScalings.Add(new CreatureTemplateScaling
                    {
                        Entry = unit.Key.GetEntry(),
                        DifficultyID = npc.DifficultyID,
                        LevelScalingMin = minLevel,
                        LevelScalingMax = maxLevel,
                        LevelScalingDeltaMin = scalingdeltalevels[unit.Key.GetEntry()].Item1,
                        LevelScalingDeltaMax = scalingdeltalevels[unit.Key.GetEntry()].Item2,
                        ContentTuningID = contentTuningID
                    });
                }
            }

            var templatesDb = SQLDatabase.Get(Storage.CreatureTemplateScalings);

            return SQLUtil.Compare(Settings.SQLOrderByKey ? Storage.CreatureTemplateScalings.OrderBy(x => x.Item1.Entry).ToArray() : Storage.CreatureTemplateScalings.ToArray(), templatesDb, x => string.Empty);
        }

        [BuilderMethod(Units = true)]
        public static string ModelData(Dictionary<WowGuid, Unit> units)
        {
            if (units.Count == 0)
                return string.Empty;

            if (!Settings.SqlTables.creature_display_info_addon)
                return string.Empty;

            var models = new DataBag<ModelData>();
            foreach (var npc in units.Select(unit => unit.Value))
            {
                if (Settings.AreaFilters.Length > 0)
                    if (!(npc.Area.ToString(CultureInfo.InvariantCulture).MatchesFilters(Settings.AreaFilters)))
                        continue;

                if (Settings.MapFilters.Length > 0)
                    if (!(npc.Map.ToString(CultureInfo.InvariantCulture).MatchesFilters(Settings.MapFilters)))
                        continue;

                uint modelId = (uint)npc.UnitData.DisplayID;
                if (modelId == 0)
                    continue;

                var model = new ModelData
                {
                    DisplayID = modelId
                };

                //if (models.ContainsKey(model))
                if (models.Any(modelInfo => modelInfo.Item1.DisplayID == modelId))
                    continue;

                var scale = npc.ObjectData.Scale;
                model.BoundingRadius = npc.UnitData.BoundingRadius / scale;
                model.CombatReach = npc.UnitData.CombatReach / scale;
                model.Gender = (Gender)npc.UnitData.Sex;

                models.Add(model);
            }

            var modelsDb = SQLDatabase.Get(models);
            return SQLUtil.Compare(models, modelsDb, StoreNameType.None);
        }

        [BuilderMethod]
        public static string NpcTrainer()
        {
            if (Storage.NpcTrainers.IsEmpty())
                return string.Empty;

            if (!Settings.SqlTables.npc_trainer)
                return string.Empty;

            var templatesDb = SQLDatabase.Get(Storage.NpcTrainers);

            return SQLUtil.Compare(Storage.NpcTrainers, templatesDb, StoreNameType.Unit);
        }

        [BuilderMethod]
        public static string Trainer()
        {
            if (Storage.Trainers.IsEmpty())
                return string.Empty;

            if (!Settings.SqlTables.trainer)
                return string.Empty;

            var templatesDb = SQLDatabase.Get(Storage.Trainers);

            return SQLUtil.Compare(Storage.Trainers, templatesDb, StoreNameType.None);
        }

        [BuilderMethod]
        public static string TrainerSpell()
        {
            if (Storage.TrainerSpells.IsEmpty())
                return string.Empty;

            if (!Settings.SqlTables.trainer)
                return string.Empty;

            foreach (var trainerSpell in Storage.TrainerSpells)
                trainerSpell.Item1.ConvertToDBStruct();

            var templatesDb = SQLDatabase.Get(Storage.TrainerSpells);

            return SQLUtil.Compare(Storage.TrainerSpells, templatesDb, t => t.FactionHelper);
        }

        [BuilderMethod]
        public static string CreatureTrainer()
        {
            if (Storage.CreatureTrainers.IsEmpty())
                return string.Empty;

            if (!Settings.SqlTables.trainer)
                return string.Empty;

            return SQLUtil.Compare(Storage.CreatureTrainers, SQLDatabase.Get(Storage.CreatureTrainers), StoreNameType.None);
        }

        [BuilderMethod]
        public static string NpcVendor()
        {
            if (Storage.NpcVendors.IsEmpty())
                return string.Empty;

            if (!Settings.SqlTables.npc_vendor)
                return string.Empty;

            var templatesDb = SQLDatabase.Get(Storage.NpcVendors);

            return SQLUtil.Compare(Storage.NpcVendors, templatesDb,
                vendor => StoreGetters.GetName(vendor.Type <= 1 ? StoreNameType.Item : StoreNameType.Currency, vendor.Item.GetValueOrDefault(), false));
        }

        [BuilderMethod(Units = true)]
        public static string CreatureEquip(Dictionary<WowGuid, Unit> units)
        {
            if (units.Count == 0)
                return string.Empty;

            if (!Settings.SqlTables.creature_equip_template)
                return string.Empty;

            var equips = new DataBag<CreatureEquipment>();
            foreach (var npc in units)
            {
                if (Settings.AreaFilters.Length > 0)
                    if (!(npc.Value.Area.ToString(CultureInfo.InvariantCulture).MatchesFilters(Settings.AreaFilters)))
                        continue;

                if (Settings.MapFilters.Length > 0)
                    if (!(npc.Value.Map.ToString(CultureInfo.InvariantCulture).MatchesFilters(Settings.MapFilters)))
                        continue;

                var equipment = npc.Value.UnitData.VirtualItems;
                if (equipment.Length != 3)
                    continue;

                if (equipment[0].ItemID == 0 && equipment[1].ItemID == 0 && equipment[2].ItemID == 0)
                    continue;

                var equip = new CreatureEquipment
                {
                    CreatureID = npc.Key.GetEntry(),
                    ItemID1 = (uint)equipment[0].ItemID,
                    ItemID2 = (uint)equipment[1].ItemID,
                    ItemID3 = (uint)equipment[2].ItemID,

                    AppearanceModID1 = equipment[0].ItemAppearanceModID,
                    AppearanceModID2 = equipment[1].ItemAppearanceModID,
                    AppearanceModID3 = equipment[2].ItemAppearanceModID,

                    ItemVisual1 = equipment[0].ItemVisual,
                    ItemVisual2 = equipment[1].ItemVisual,
                    ItemVisual3 = equipment[2].ItemVisual
                };


                if (equips.Contains(equip))
                    continue;

                for (uint i = 1;; i++)
                {
                    equip.ID = i;
                    if (!equips.ContainsKey(equip))
                        break;
                }

                equips.Add(equip);
            }

            var equipsDb = SQLDatabase.Get(equips);
            return SQLUtil.Compare(equips, equipsDb, StoreNameType.Unit);
        }

        [BuilderMethod]
        public static string PointsOfInterest()
        {
            if (Storage.GossipPOIs.IsEmpty())
                return string.Empty;

            if (!Settings.SqlTables.points_of_interest)
                return string.Empty;

            if (Settings.DBEnabled)
                return SQLUtil.Compare(Storage.GossipPOIs, SQLDatabase.Get(Storage.GossipPOIs), StoreNameType.None);
            else
            {
                uint count = 0;
                var rows = new RowList<PointsOfInterest>();

                foreach (var pointOfInterest in Storage.GossipPOIs)
                {
                    Row<PointsOfInterest> row = new Row<PointsOfInterest>();

                    Type t = pointOfInterest.Item1.ID.GetType();
                    if (t.Equals(typeof(int)))
                        row.Data.ID = pointOfInterest.Item1.ID;
                    else
                        row.Data.ID = "@PID+" + count;

                    row.Data.PositionX = pointOfInterest.Item1.PositionX;
                    row.Data.PositionY = pointOfInterest.Item1.PositionY;
                    row.Data.Icon = pointOfInterest.Item1.Icon;
                    row.Data.Flags = pointOfInterest.Item1.Flags;
                    row.Data.Importance = pointOfInterest.Item1.Importance;
                    row.Data.Name = pointOfInterest.Item1.Name;
                    row.Data.VerifiedBuild = pointOfInterest.Item1.VerifiedBuild;

                    ++count;

                    rows.Add(row);
                }

                StringBuilder result = new StringBuilder();
                // delete query for GUIDs
                var delete = new SQLDelete<PointsOfInterest>(Tuple.Create("@PID+0", "@PID+" + --count));
                result.Append(delete.Build());

                var sql = new SQLInsert<PointsOfInterest>(rows, false);
                result.Append(sql.Build());

                return result.ToString();
            }
        }

        [BuilderMethod]
        public static string Gossip()
        {
            if (Storage.Gossips.IsEmpty() && Storage.GossipMenuOptions.IsEmpty())
                return string.Empty;

            var result = "";

            // `creature_gossip`
            if (Settings.SqlTables.creature_gossip)
                result += SQLUtil.Compare(Storage.CreatureGossips, SQLDatabase.Get(Storage.CreatureGossips),
                    t => StoreGetters.GetName(StoreNameType.Unit, (int)t.CreatureId)); // BUG: GOs can send gossips too

            // `gossip_menu`
            if (Settings.SqlTables.gossip_menu)
                result += SQLUtil.Compare(Storage.Gossips, SQLDatabase.Get(Storage.Gossips),
                    t => StoreGetters.GetName(StoreNameType.Unit, (int)t.ObjectEntry)); // BUG: GOs can send gossips too

            // `gossip_menu_option`
            if (Settings.SqlTables.gossip_menu_option)
            {
                result += SQLUtil.Compare(Storage.GossipMenuOptions, SQLDatabase.Get(Storage.GossipMenuOptions), t => t.BroadcastTextIDHelper);

                if (!Storage.GossipMenuOptionActions.IsEmpty())
                {
                    foreach (var gossip_pair in Storage.GossipMenuOptionActions)
                    {
                        string poiId = "0";
                        if (gossip_pair.Item1.ActionPoiId != null)
                            poiId = gossip_pair.Item1.ActionPoiId.ToString();
                        else
                            gossip_pair.Item1.ActionPoiId = 0;

                        result += "UPDATE `gossip_menu_option` SET `action_menu_id`=" + gossip_pair.Item1.ActionMenuId.ToString() + ", `action_poi_id`=" + poiId + ", `option_id`=1, `npc_option_npcflag`=1 WHERE `menu_id`=" + gossip_pair.Item1.MenuId.ToString() + " && `id`=" + gossip_pair.Item1.OptionIndex.ToString() + ";\r\n";

                    }

                    result += "\r\n";
                    result += SQLUtil.Compare(Storage.GossipMenuOptionActions, SQLDatabase.Get(Storage.GossipMenuOptionActions), StoreNameType.None);
                }

                if (!Storage.GossipMenuOptionBoxes.IsEmpty())
                {
                    foreach (var gossip_pair in Storage.GossipMenuOptionBoxes)
                        result += "UPDATE `gossip_menu_option` SET `box_coded`=" + gossip_pair.Item1.BoxCoded.ToString() + ", `box_money`=" + gossip_pair.Item1.BoxMoney.ToString() + ", `box_text`='" + MySql.Data.MySqlClient.MySqlHelper.EscapeString(gossip_pair.Item1.BoxText) + "' WHERE `menu_id`=" + gossip_pair.Item1.MenuId.ToString() + " && `id`=" + gossip_pair.Item1.OptionIndex.ToString() + ";\r\n";
                    result += "\r\n";
                    result += SQLUtil.Compare(Storage.GossipMenuOptionBoxes, SQLDatabase.Get(Storage.GossipMenuOptionBoxes), t => t.BroadcastTextIdHelper);
                }
            }

            return result;
        }

        //                      entry, <level_min, level_max>
        public static Dictionary<uint, Tuple<uint, uint>> GetLevels(Dictionary<WowGuid, Unit> units)
        {
            if (units.Count == 0)
                return null;

            var entries = units.GroupBy(unit => unit.Key.GetEntry());
            var list = new Dictionary<uint, List<uint>>();

            foreach (var pair in entries.SelectMany(entry => entry))
            {
                if (list.ContainsKey(pair.Key.GetEntry()))
                    list[pair.Key.GetEntry()].Add((uint)pair.Value.UnitData.Level);
                else
                    list.Add(pair.Key.GetEntry(), new List<uint> { (uint)pair.Value.UnitData.Level });
            }

            var result = list.ToDictionary(pair => pair.Key, pair => Tuple.Create(pair.Value.Min(), pair.Value.Max()));

            return result.Count == 0 ? null : result;
        }

        public static readonly HashSet<string> ProfessionTrainers = new HashSet<string>
        {
            "Alchemy Trainer", "Armorsmith Trainer", "Armorsmithing Trainer", "Blacksmith Trainer",
            "Blacksmithing Trainer", "Blacksmithing Trainer & Supplies", "Cold Weather Flying Trainer",
            "Cooking Trainer", "Cooking Trainer & Supplies", "Dragonscale Leatherworking Trainer",
            "Elemental Leatherworking Trainer", "Enchanting Trainer", "Engineering Trainer",
            "First Aid Trainer", "Fishing Trainer", "Fishing Trainer & Supplies",
            "Gnome Engineering Trainer", "Gnomish Engineering Trainer", "Goblin Engineering Trainer",
            "Grand Master Alchemy Trainer", "Grand Master Blacksmithing Trainer",
            "Grand Master Cooking Trainer", "Grand Master Enchanting Trainer",
            "Grand Master Engineering Trainer", "Grand Master First Aid Trainer",
            "Grand Master Fishing Trainer", "Grand Master Fishing Trainer & Supplies",
            "Grand Master Herbalism Trainer", "Grand Master Inscription Trainer",
            "Grand Master Jewelcrafting Trainer", "Grand Master Leatherworking Trainer",
            "Grand Master Mining Trainer", "Grand Master Skinning Trainer",
            "Grand Master Tailoring Trainer", "Herbalism Trainer",
            "Herbalism Trainer & Supplies", "Inscription Trainer",
            "Jewelcrafting Trainer", "Leatherworking Trainer",
            "Master Alchemy Trainer", "Master Blacksmithing Trainer",
            "Master Enchanting Trainer", "Master Engineering Trainer",
            "Master Fishing Trainer", "Master Herbalism Trainer",
            "Master Inscription Trainer", "Master Jewelcrafting Trainer",
            "Master Leatherworking Trainer", "Master Mining Trainer",
            "Master Skinning Trainer", "Master Tailoring Trainer",
            "Mining Trainer", "Skinning Trainer", "Tailor Trainer", "Tailoring Trainer",
            "Tribal Leatherworking Trainer", "Weaponsmith Trainer", "Weaponsmithing Trainer",
            "Horse Riding Trainer", "Ram Riding Trainer", "Raptor Riding Trainer",
            "Tiger Riding Trainer", "Wolf Riding Trainer", "Mechastrider Riding Trainer",
            "Riding Trainer", "Undead Horse Riding Trainer"
        };

        public static readonly HashSet<string> ClassTrainers = new HashSet<string>
        {
            "Druid Trainer", "Portal Trainer", "Portal: Darnassus Trainer",
            "Portal: Ironforge Trainer", "Portal: Orgrimmar Trainer",
            "Portal: Stormwind Trainer", "Portal: Thunder Bluff Trainer",
            "Portal: Undercity Trainer", "Deathknight Trainer",
            "Hunter Trainer", "Mage Trainer", "Paladin Trainer",
            "Priest Trainer", "Shaman Trainer", "Warlock Trainer",
            "Warrior Trainer"
        };

        private static string GetSubName(int entry, bool withEntry)
        {
            string name = StoreGetters.GetName(StoreNameType.Unit, entry, withEntry);
            int firstIndex = name.LastIndexOf('<');
            int lastIndex = name.LastIndexOf('>');
            if (firstIndex != -1 && lastIndex != -1)
                return name.Substring(firstIndex + 1, lastIndex - firstIndex - 1);

            return "";
        }

        private static NPCFlags ProcessNpcFlags(string subName)
        {
            if (ProfessionTrainers.Contains(subName))
                return NPCFlags.ProfessionTrainer;
            if (ClassTrainers.Contains(subName))
                return NPCFlags.ClassTrainer;

            return 0;
        }

        class CreatureTemplateNonWdbExport
        {
            public uint Entry = 0;
            // Count how many times each value has been seen
            public Dictionary<uint, uint> Factions = new Dictionary<uint, uint>();
            public Dictionary<uint, uint> NpcFlags1 = new Dictionary<uint, uint>();
            public Dictionary<uint, uint> NpcFlags2 = new Dictionary<uint, uint>();
            public Dictionary<float, uint> RunSpeeds = new Dictionary<float, uint>();
            public Dictionary<float, uint> WalkSpeeds = new Dictionary<float, uint>();
            public Dictionary<float, uint> Sizes = new Dictionary<float, uint>();
            public Dictionary<uint, uint> BaseAttackTimes = new Dictionary<uint, uint>();
            public Dictionary<uint, uint> RangedAttackTimes = new Dictionary<uint, uint>();
            public Dictionary<uint, uint> UnitClasses = new Dictionary<uint, uint>();
            public Dictionary<UnitFlags, uint> UnitFlags1 = new Dictionary<UnitFlags, uint>();
            public Dictionary<UnitFlags2, uint> UnitFlags2 = new Dictionary<UnitFlags2, uint>();
            public Dictionary<UnitFlags3, uint> UnitFlags3 = new Dictionary<UnitFlags3, uint>();
            public Dictionary<UnitDynamicFlags, uint> DynamicFlags = new Dictionary<UnitDynamicFlags, uint>();
            public Dictionary<UnitDynamicFlagsWOD, uint> DynamicFlagsWod = new Dictionary<UnitDynamicFlagsWOD, uint>();
            public Dictionary<uint, uint> VehicleIds = new Dictionary<uint, uint>();
            public Dictionary<float, uint> HoverHeights = new Dictionary<float, uint>();
        }

        [BuilderMethod(true, Units = true)]
        public static string CreatureTemplateNonWDB(Dictionary<WowGuid, Unit> units)
        {
            if (units.Count == 0)
                return string.Empty;

            if (!Settings.SqlTables.creature_template)
                return string.Empty;

            var levels = GetLevels(units);

            // Get most common value for fields
            Dictionary<uint, CreatureTemplateNonWdbExport> creatureExportData = new Dictionary<uint, CreatureTemplateNonWdbExport>();

            foreach (var unit in units)
            {
                var npc = unit.Value;

                if (Settings.SqlTables.creature_stats)
                {
                    bool hasData = false;
                    CreatureStats creatureStats = new CreatureStats();
                    UpdateField value;
                    if (npc.UpdateFields.TryGetValue(UpdateFields.GetUpdateField(UnitField.UNIT_FIELD_MINDAMAGE), out value))
                    {
                        hasData = true;
                        creatureStats.DmgMin = value.FloatValue;
                    }
                    if (npc.UpdateFields.TryGetValue(UpdateFields.GetUpdateField(UnitField.UNIT_FIELD_MAXDAMAGE), out value))
                    {
                        hasData = true;
                        creatureStats.DmgMax = value.FloatValue;
                    }
                    if (npc.UpdateFields.TryGetValue(UpdateFields.GetUpdateField(UnitField.UNIT_FIELD_MINOFFHANDDAMAGE), out value))
                    {
                        hasData = true;
                        creatureStats.OffhandDmgMin = value.FloatValue;
                    }
                    if (npc.UpdateFields.TryGetValue(UpdateFields.GetUpdateField(UnitField.UNIT_FIELD_MAXOFFHANDDAMAGE), out value))
                    {
                        hasData = true;
                        creatureStats.OffhandDmgMax = value.FloatValue;
                    }
                    if (npc.UpdateFields.TryGetValue(UpdateFields.GetUpdateField(UnitField.UNIT_FIELD_MINRANGEDDAMAGE), out value))
                    {
                        hasData = true;
                        creatureStats.RangedDmgMin = value.FloatValue;
                    }
                    if (npc.UpdateFields.TryGetValue(UpdateFields.GetUpdateField(UnitField.UNIT_FIELD_MAXRANGEDDAMAGE), out value))
                    {
                        hasData = true;
                        creatureStats.RangedDmgMax = value.FloatValue;
                    }
                    if (npc.UpdateFields.TryGetValue(UpdateFields.GetUpdateField(UnitField.UNIT_FIELD_ATTACK_POWER), out value))
                    {
                        hasData = true;
                        creatureStats.AttackPower = value.UInt32Value;
                    }
                    if (npc.UpdateFields.TryGetValue(UpdateFields.GetUpdateField(UnitField.UNIT_FIELD_RANGED_ATTACK_POWER), out value))
                    {
                        hasData = true;
                        creatureStats.RangedAttackPower = value.UInt32Value;
                    }
                    if (npc.UpdateFields.TryGetValue(UpdateFields.GetUpdateField(UnitField.UNIT_FIELD_STAT), out value))
                    {
                        hasData = true;
                        creatureStats.Strength = value.UInt32Value;
                    }
                    if (npc.UpdateFields.TryGetValue(UpdateFields.GetUpdateField(UnitField.UNIT_FIELD_STAT)+1, out value))
                    {
                        hasData = true;
                        creatureStats.Agility = value.UInt32Value;
                    }
                    if (npc.UpdateFields.TryGetValue(UpdateFields.GetUpdateField(UnitField.UNIT_FIELD_STAT)+2, out value))
                    {
                        hasData = true;
                        creatureStats.Stamina = value.UInt32Value;
                    }
                    if (npc.UpdateFields.TryGetValue(UpdateFields.GetUpdateField(UnitField.UNIT_FIELD_STAT)+3, out value))
                    {
                        hasData = true;
                        creatureStats.Intellect = value.UInt32Value;
                    }
                    if (npc.UpdateFields.TryGetValue(UpdateFields.GetUpdateField(UnitField.UNIT_FIELD_STAT)+4, out value))
                    {
                        hasData = true;
                        creatureStats.Spirit = value.UInt32Value;
                    }
                    if (npc.UpdateFields.TryGetValue(UpdateFields.GetUpdateField(UnitField.UNIT_FIELD_RESISTANCES), out value))
                    {
                        hasData = true;
                        creatureStats.Armor = value.Int32Value;
                    }
                    if (npc.UpdateFields.TryGetValue(UpdateFields.GetUpdateField(UnitField.UNIT_FIELD_RESISTANCES)+1, out value))
                    {
                        hasData = true;
                        creatureStats.HolyResistance = value.Int32Value;
                    }
                    if (npc.UpdateFields.TryGetValue(UpdateFields.GetUpdateField(UnitField.UNIT_FIELD_RESISTANCES)+2, out value))
                    {
                        hasData = true;
                        creatureStats.FireResistance = value.Int32Value;
                    }
                    if (npc.UpdateFields.TryGetValue(UpdateFields.GetUpdateField(UnitField.UNIT_FIELD_RESISTANCES)+3, out value))
                    {
                        hasData = true;
                        creatureStats.NatureResistance = value.Int32Value;
                    }
                    if (npc.UpdateFields.TryGetValue(UpdateFields.GetUpdateField(UnitField.UNIT_FIELD_RESISTANCES)+4, out value))
                    {
                        hasData = true;
                        creatureStats.FrostResistance = value.Int32Value;
                    }
                    if (npc.UpdateFields.TryGetValue(UpdateFields.GetUpdateField(UnitField.UNIT_FIELD_RESISTANCES)+5, out value))
                    {
                        hasData = true;
                        creatureStats.ShadowResistance = value.Int32Value;
                    }
                    if (npc.UpdateFields.TryGetValue(UpdateFields.GetUpdateField(UnitField.UNIT_FIELD_RESISTANCES)+6, out value))
                    {
                        hasData = true;
                        creatureStats.ArcaneResistance = value.Int32Value;
                    }
                    if (hasData)
                    {
                        creatureStats.Entry = unit.Key.GetEntry();
                        Storage.CreatureStats.Add(creatureStats);
                    }
                }

                if (!creatureExportData.ContainsKey(unit.Key.GetEntry()))
                {
                    CreatureTemplateNonWdbExport data = new CreatureTemplateNonWdbExport();
                    data.Entry = unit.Key.GetEntry();
                    data.Factions.Add((uint)npc.UnitData.FactionTemplate, 1);
                    data.NpcFlags1.Add(npc.UnitData.NpcFlags[0], 1);
                    data.NpcFlags2.Add(npc.UnitData.NpcFlags[1], 1);
                    data.RunSpeeds.Add(npc.Movement.RunSpeed, 1);
                    data.WalkSpeeds.Add(npc.Movement.WalkSpeed, 1);
                    data.Sizes.Add(npc.ObjectData.Scale, 1);
                    data.BaseAttackTimes.Add(npc.UnitData.AttackRoundBaseTime[0], 1);
                    data.RangedAttackTimes.Add(npc.UnitData.RangedAttackRoundBaseTime, 1);
                    data.UnitClasses.Add(npc.UnitData.ClassId, 1);
                    data.UnitFlags1.Add((UnitFlags)npc.UnitData.Flags, 1);
                    data.UnitFlags2.Add((UnitFlags2)npc.UnitData.Flags2, 1);
                    data.UnitFlags3.Add((UnitFlags3)npc.UnitData.Flags3, 1);
                    data.DynamicFlags.Add(npc.DynamicFlags.GetValueOrDefault(UnitDynamicFlags.None), 1);
                    data.DynamicFlagsWod.Add(npc.DynamicFlagsWod.GetValueOrDefault(UnitDynamicFlagsWOD.None), 1);
                    data.VehicleIds.Add(npc.Movement.VehicleId, 1);
                    data.HoverHeights.Add(npc.UnitData.HoverHeight, 1);
                    creatureExportData.Add(unit.Key.GetEntry(), data);
                }
                else
                {
                    CreatureTemplateNonWdbExport data = creatureExportData[unit.Key.GetEntry()];

                    if (data.Factions.ContainsKey((uint)npc.UnitData.FactionTemplate))
                        data.Factions[(uint)npc.UnitData.FactionTemplate]++;
                    else
                        data.Factions.Add((uint)npc.UnitData.FactionTemplate, 1);

                    if (data.NpcFlags1.ContainsKey((uint)npc.UnitData.NpcFlags[0]))
                        data.NpcFlags1[(uint)npc.UnitData.NpcFlags[0]]++;
                    else
                        data.NpcFlags1.Add((uint)npc.UnitData.NpcFlags[0], 1);

                    if (data.NpcFlags2.ContainsKey((uint)npc.UnitData.NpcFlags[1]))
                        data.NpcFlags2[(uint)npc.UnitData.NpcFlags[1]]++;
                    else
                        data.NpcFlags2.Add((uint)npc.UnitData.NpcFlags[1], 1);

                    if (data.RunSpeeds.ContainsKey(npc.Movement.RunSpeed))
                        data.RunSpeeds[npc.Movement.RunSpeed]++;
                    else
                        data.RunSpeeds.Add(npc.Movement.RunSpeed, 1);

                    if (data.WalkSpeeds.ContainsKey(npc.Movement.WalkSpeed))
                        data.WalkSpeeds[npc.Movement.WalkSpeed]++;
                    else
                        data.WalkSpeeds.Add(npc.Movement.WalkSpeed, 1);

                    if (data.Sizes.ContainsKey(npc.ObjectData.Scale))
                        data.Sizes[npc.ObjectData.Scale]++;
                    else
                        data.Sizes.Add(npc.ObjectData.Scale, 1);

                    if (data.BaseAttackTimes.ContainsKey(npc.UnitData.AttackRoundBaseTime[0]))
                        data.BaseAttackTimes[npc.UnitData.AttackRoundBaseTime[0]]++;
                    else
                        data.BaseAttackTimes.Add(npc.UnitData.AttackRoundBaseTime[0], 1);

                    if (data.RangedAttackTimes.ContainsKey(npc.UnitData.RangedAttackRoundBaseTime))
                        data.RangedAttackTimes[npc.UnitData.RangedAttackRoundBaseTime]++;
                    else
                        data.RangedAttackTimes.Add(npc.UnitData.RangedAttackRoundBaseTime, 1);

                    if (data.UnitClasses.ContainsKey(npc.UnitData.ClassId))
                        data.UnitClasses[npc.UnitData.ClassId]++;
                    else
                        data.UnitClasses.Add(npc.UnitData.ClassId, 1);

                    if (data.UnitFlags1.ContainsKey((UnitFlags)npc.UnitData.Flags))
                        data.UnitFlags1[(UnitFlags)npc.UnitData.Flags]++;
                    else
                        data.UnitFlags1.Add((UnitFlags)npc.UnitData.Flags, 1);

                    if (data.UnitFlags2.ContainsKey((UnitFlags2)npc.UnitData.Flags2))
                        data.UnitFlags2[(UnitFlags2)npc.UnitData.Flags2]++;
                    else
                        data.UnitFlags2.Add((UnitFlags2)npc.UnitData.Flags2, 1);

                    if (data.UnitFlags3.ContainsKey((UnitFlags3)npc.UnitData.Flags3))
                        data.UnitFlags3[(UnitFlags3)npc.UnitData.Flags3]++;
                    else
                        data.UnitFlags3.Add((UnitFlags3)npc.UnitData.Flags3, 1);

                    if (data.DynamicFlags.ContainsKey(npc.DynamicFlags.GetValueOrDefault(UnitDynamicFlags.None)))
                        data.DynamicFlags[npc.DynamicFlags.GetValueOrDefault(UnitDynamicFlags.None)]++;
                    else
                        data.DynamicFlags.Add(npc.DynamicFlags.GetValueOrDefault(UnitDynamicFlags.None), 1);

                    if (data.DynamicFlagsWod.ContainsKey(npc.DynamicFlagsWod.GetValueOrDefault(UnitDynamicFlagsWOD.None)))
                        data.DynamicFlagsWod[npc.DynamicFlagsWod.GetValueOrDefault(UnitDynamicFlagsWOD.None)]++;
                    else
                        data.DynamicFlagsWod.Add(npc.DynamicFlagsWod.GetValueOrDefault(UnitDynamicFlagsWOD.None), 1);

                    if (data.VehicleIds.ContainsKey(npc.Movement.VehicleId))
                        data.VehicleIds[npc.Movement.VehicleId]++;
                    else
                        data.VehicleIds.Add(npc.Movement.VehicleId, 1);

                    if (data.HoverHeights.ContainsKey(npc.UnitData.HoverHeight))
                        data.HoverHeights[npc.UnitData.HoverHeight]++;
                    else
                        data.HoverHeights.Add(npc.UnitData.HoverHeight, 1);
                }
            }

            foreach (var npc in creatureExportData)
            {
                uint mostCommonFaction = 0;
                uint mostCommonFactionCount = 0;
                foreach (var factionPair in npc.Value.Factions)
                {
                    if (factionPair.Value > mostCommonFactionCount)
                    {
                        mostCommonFaction = factionPair.Key;
                        mostCommonFactionCount = factionPair.Value;
                    }
                }

                uint mostCommonNpcFlag1 = 0;
                uint mostCommonNpcFlag1Count = 0;
                foreach (var npcFlag1Pair in npc.Value.NpcFlags1)
                {
                    if (npcFlag1Pair.Value > mostCommonNpcFlag1Count)
                    {
                        mostCommonNpcFlag1 = npcFlag1Pair.Key;
                        mostCommonNpcFlag1Count = npcFlag1Pair.Value;
                    }
                }

                uint mostCommonNpcFlag2 = 0;
                uint mostCommonNpcFlag2Count = 0;
                foreach (var npcFlag2Pair in npc.Value.NpcFlags2)
                {
                    if (npcFlag2Pair.Value > mostCommonNpcFlag2Count)
                    {
                        mostCommonNpcFlag2 = npcFlag2Pair.Key;
                        mostCommonNpcFlag2Count = npcFlag2Pair.Value;
                    }
                }

                float mostCommonRunSpeed = 0;
                uint mostCommonRunSpeedCount = 0;
                foreach (var runSpeedPair in npc.Value.RunSpeeds)
                {
                    if (runSpeedPair.Value > mostCommonRunSpeedCount)
                    {
                        mostCommonRunSpeed = runSpeedPair.Key;
                        mostCommonRunSpeedCount = runSpeedPair.Value;
                    }
                }

                float mostCommonWalkSpeed = 0;
                uint mostCommonWalkSpeedCount = 0;
                foreach (var walkSpeedPair in npc.Value.WalkSpeeds)
                {
                    if (walkSpeedPair.Value > mostCommonWalkSpeedCount)
                    {
                        mostCommonWalkSpeed = walkSpeedPair.Key;
                        mostCommonWalkSpeedCount = walkSpeedPair.Value;
                    }
                }

                float mostCommonScaleSize = 0;
                uint mostCommonScaleSizeCount = 0;
                foreach (var scaleSizePair in npc.Value.Sizes)
                {
                    if (scaleSizePair.Value > mostCommonScaleSizeCount)
                    {
                        mostCommonScaleSize = scaleSizePair.Key;
                        mostCommonScaleSizeCount = scaleSizePair.Value;
                    }
                }

                uint mostCommonBaseAttackTime = 0;
                uint mostCommonBaseAttackTimeCount = 0;
                foreach (var baseAttackTimePair in npc.Value.BaseAttackTimes)
                {
                    if (baseAttackTimePair.Value > mostCommonBaseAttackTimeCount)
                    {
                        mostCommonBaseAttackTime = baseAttackTimePair.Key;
                        mostCommonBaseAttackTimeCount = baseAttackTimePair.Value;
                    }
                }

                uint mostCommonRangedAttackTime = 0;
                uint mostCommonRangedAttackTimeCount = 0;
                foreach (var rangedAttackTimePair in npc.Value.RangedAttackTimes)
                {
                    if (rangedAttackTimePair.Value > mostCommonRangedAttackTimeCount)
                    {
                        mostCommonRangedAttackTime = rangedAttackTimePair.Key;
                        mostCommonRangedAttackTimeCount = rangedAttackTimePair.Value;
                    }
                }

                uint mostCommonClassId = 0;
                uint mostCommonClassIdCount = 0;
                foreach (var classIdPair in npc.Value.UnitClasses)
                {
                    if (classIdPair.Value > mostCommonClassIdCount)
                    {
                        mostCommonClassId = classIdPair.Key;
                        mostCommonClassIdCount = classIdPair.Value;
                    }
                }

                UnitFlags mostCommonUnitFlag1 = 0;
                uint mostCommonUnitFlag1Count = 0;
                foreach (var unitFlagPair in npc.Value.UnitFlags1)
                {
                    if (unitFlagPair.Value > mostCommonUnitFlag1Count)
                    {
                        mostCommonUnitFlag1 = unitFlagPair.Key;
                        mostCommonUnitFlag1Count = unitFlagPair.Value;
                    }
                }

                UnitFlags2 mostCommonUnitFlag2 = 0;
                uint mostCommonUnitFlag2Count = 0;
                foreach (var unitFlagPair in npc.Value.UnitFlags2)
                {
                    if (unitFlagPair.Value > mostCommonUnitFlag2Count)
                    {
                        mostCommonUnitFlag2 = unitFlagPair.Key;
                        mostCommonUnitFlag2Count = unitFlagPair.Value;
                    }
                }

                UnitFlags3 mostCommonUnitFlag3 = 0;
                uint mostCommonUnitFlag3Count = 0;
                foreach (var unitFlagPair in npc.Value.UnitFlags3)
                {
                    if (unitFlagPair.Value > mostCommonUnitFlag3Count)
                    {
                        mostCommonUnitFlag3 = unitFlagPair.Key;
                        mostCommonUnitFlag3Count = unitFlagPair.Value;
                    }
                }

                UnitDynamicFlags mostCommonDynamicFlag = 0;
                uint mostCommonDynamicFlagCount = 0;
                foreach (var unitFlagPair in npc.Value.DynamicFlags)
                {
                    if (unitFlagPair.Value > mostCommonDynamicFlagCount)
                    {
                        mostCommonDynamicFlag = unitFlagPair.Key;
                        mostCommonDynamicFlagCount = unitFlagPair.Value;
                    }
                }

                UnitDynamicFlagsWOD mostCommonDynamicFlagWod = 0;
                uint mostCommonDynamicFlagWodCount = 0;
                foreach (var dynamicFlagPair in npc.Value.DynamicFlagsWod)
                {
                    if (dynamicFlagPair.Value > mostCommonDynamicFlagWodCount)
                    {
                        mostCommonDynamicFlagWod = dynamicFlagPair.Key;
                        mostCommonDynamicFlagWodCount = dynamicFlagPair.Value;
                    }
                }

                uint mostCommonVehicleId = 0;
                uint mostCommonVehicleIdCount = 0;
                foreach (var vehicleIdPair in npc.Value.VehicleIds)
                {
                    if (vehicleIdPair.Value > mostCommonVehicleIdCount)
                    {
                        mostCommonVehicleId = vehicleIdPair.Key;
                        mostCommonVehicleIdCount = vehicleIdPair.Value;
                    }
                }

                float mostCommonHoverHeight = 0;
                uint mostCommonHoverHeightCount = 0;
                foreach (var hoverHeightPair in npc.Value.HoverHeights)
                {
                    if (hoverHeightPair.Value > mostCommonHoverHeightCount)
                    {
                        mostCommonHoverHeight = hoverHeightPair.Key;
                        mostCommonHoverHeightCount = hoverHeightPair.Value;
                    }
                }

                var template = new CreatureTemplateNonWDB
                {
                    Entry = npc.Value.Entry,
                    GossipMenuId = Storage.CreatureDefaultGossips.ContainsKey(npc.Value.Entry) ? Storage.CreatureDefaultGossips[npc.Value.Entry] : 0,
                    MinLevel = (int)levels[npc.Value.Entry].Item1,
                    MaxLevel = (int)levels[npc.Value.Entry].Item2,
                    Faction = mostCommonFaction,
                    NpcFlag = (NPCFlags)Utilities.MAKE_PAIR64(mostCommonNpcFlag1, mostCommonNpcFlag2),
                    SpeedRun = mostCommonRunSpeed / MovementInfo.DEFAULT_RUN_SPEED,
                    SpeedWalk = mostCommonWalkSpeed / MovementInfo.DEFAULT_WALK_SPEED,
                    Scale = mostCommonScaleSize,
                    BaseAttackTime = mostCommonBaseAttackTime,
                    RangedAttackTime = mostCommonRangedAttackTime,
                    UnitClass = mostCommonClassId,
                    UnitFlags = mostCommonUnitFlag1,
                    UnitFlags2 = mostCommonUnitFlag2,
                    UnitFlags3 = mostCommonUnitFlag3,
                    DynamicFlags = mostCommonDynamicFlag,
                    DynamicFlagsWod = mostCommonDynamicFlagWod,
                    VehicleID = mostCommonVehicleId,
                    HoverHeight = mostCommonHoverHeight
                };

                if (Settings.UseDBC)
                {
                    var creatureDiff = DBC.DBC.CreatureDifficulty.Where(diff => diff.Value.CreatureID == npc.Value.Entry);
                    if (creatureDiff.Any())
                    {
                        template.MinLevel = creatureDiff.Select(lv => lv.Value.MinLevel).First();
                        template.MaxLevel = creatureDiff.Select(lv => lv.Value.MaxLevel).First();
                        template.Faction = creatureDiff.Select(lv => lv.Value.FactionTemplateID).First();
                    }
                }

                if (template.Faction == 1 || template.Faction == 2 || template.Faction == 3 ||
                    template.Faction == 4 || template.Faction == 5 || template.Faction == 6 ||
                    template.Faction == 115 || template.Faction == 116 || template.Faction == 1610 ||
                    template.Faction == 1629 || template.Faction == 2203 || template.Faction == 2204) // player factions
                    template.Faction = 35;

                template.UnitFlags &= ~UnitFlags.IsInCombat;
                template.UnitFlags &= ~UnitFlags.PetIsAttackingTarget;
                template.UnitFlags &= ~UnitFlags.PlayerControlled;
                template.UnitFlags &= ~UnitFlags.Silenced;
                template.UnitFlags &= ~UnitFlags.PossessedByPlayer;

                if (!ClientVersion.AddedInVersion(ClientType.WarlordsOfDraenor))
                {
                    template.DynamicFlags &= ~UnitDynamicFlags.Lootable;
                    template.DynamicFlags &= ~UnitDynamicFlags.Tapped;
                    template.DynamicFlags &= ~UnitDynamicFlags.TappedByPlayer;
                    template.DynamicFlags &= ~UnitDynamicFlags.TappedByAllThreatList;
                }
                else
                {
                    template.DynamicFlagsWod &= ~UnitDynamicFlagsWOD.Lootable;
                    template.DynamicFlagsWod &= ~UnitDynamicFlagsWOD.Tapped;
                    template.DynamicFlagsWod &= ~UnitDynamicFlagsWOD.TappedByPlayer;
                    template.DynamicFlagsWod &= ~UnitDynamicFlagsWOD.TappedByAllThreatList;
                }

                // has trainer flag but doesn't have prof nor class trainer flag
                if ((template.NpcFlag & NPCFlags.Trainer) != 0 &&
                    ((template.NpcFlag & NPCFlags.ProfessionTrainer) == 0 ||
                     (template.NpcFlag & NPCFlags.ClassTrainer) == 0))
                {
                    var subname = GetSubName((int)npc.Value.Entry, false); // Fall back
                    var entry = Storage.CreatureTemplates.Where(creature => creature.Item1.Entry == npc.Value.Entry);
                    if (entry.Any())
                    {
                        var sub = entry.Select(creature => creature.Item1.SubName).First();
                        if (sub.Length > 0)
                        {
                            template.NpcFlag |= ProcessNpcFlags(sub);
                            Trace.WriteLine($"Entry: { npc.Value.Entry } NpcFlag: { template.NpcFlag }");
                        }
                        else // If the SubName doesn't exist or is cached, fall back to DB method
                            template.NpcFlag |= ProcessNpcFlags(subname);
                    }
                    else // In case we have NonWDB data which doesn't have an entry in CreatureTemplates
                        template.NpcFlag |= ProcessNpcFlags(subname);
                }

                Storage.CreatureTemplatesNonWDB.Add(template);
            }

            string result = "";

            // `creature_stats`
            if (Settings.SqlTables.creature_stats)
                result += SQLUtil.Compare(Storage.CreatureStats, SQLDatabase.Get(Storage.CreatureStats),
                    t => StoreGetters.GetName(StoreNameType.Unit, (int)t.Entry));

            var templatesDb = SQLDatabase.Get(Storage.CreatureTemplatesNonWDB);
            result += SQLUtil.Compare(Storage.CreatureTemplatesNonWDB, templatesDb, StoreNameType.Unit);

            return result;
        }

        static UnitMisc()
        {
            HotfixStoreMgr.OnRecordReceived += (hash, recordKey, added) =>
            {
                if (!added || hash != DB2Hash.BroadcastText)
                    return;

                var record = HotfixStoreMgr.GetStore(hash).GetRecord(recordKey) as IBroadcastTextEntry;
                if (record == null)
                    return;

                if (!SQLDatabase.BroadcastTexts.ContainsKey(record.Text))
                    SQLDatabase.BroadcastTexts[record.Text] = new List<int>();
                SQLDatabase.BroadcastTexts[record.Text].Add(recordKey);

                if (!SQLDatabase.BroadcastText1s.ContainsKey(record.Text1))
                    SQLDatabase.BroadcastText1s[record.Text1] = new List<int>();
                SQLDatabase.BroadcastText1s[record.Text1].Add(recordKey);
            };
        }

        [BuilderMethod]
        public static string CreatureTextTemplate()
        {
            if (Storage.CreatureTextTemplates.IsEmpty() || !Settings.SqlTables.creature_text_template)
                return string.Empty;

            // For each sound and emote, if the time they were send is in the +1/-1 seconds range of
            // our texts, add that sound and emote to our Storage.CreatureTextTemplates

            foreach (var text in Storage.CreatureTextTemplates)
            {
                // For each text
                foreach (var textValue in text.Value)
                {
                    // For each emote
                    if (Storage.Emotes.ContainsKey(textValue.Item1.SenderGUID))
                    {
                        foreach (var emote in Storage.Emotes[textValue.Item1.SenderGUID])
                        {
                            DateTime textTime = textValue.Item1.Time;
                            if (System.Math.Abs((textTime - emote.time).TotalSeconds) <= 1)
                                textValue.Item1.Emote = emote.emote;
                        }
                    }

                    // For each sound
                    foreach (var sound in Storage.Sounds)
                    {
                        DateTime textTime = textValue.Item1.Time;
                        if (sound.guid == textValue.Item1.SenderGUID)
                        {
                            if (System.Math.Abs((textTime - sound.time).TotalSeconds) <= 1)
                                textValue.Item1.Sound = sound.sound;
                        }
                    }

                    List<int> textList;
                    if (SQLDatabase.BroadcastTexts.TryGetValue(textValue.Item1.Text, out textList) ||
                        SQLDatabase.BroadcastText1s.TryGetValue(textValue.Item1.Text, out textList))
                    {
                        if (textList.Count == 1)
                            textValue.Item1.BroadcastTextID = (uint)textList.First();
                        else
                        {
                            textValue.Item1.BroadcastTextID = "PLEASE_SET_A_BROADCASTTEXT_ID";
                            textValue.Item1.BroadcastTextIDHelper = "BroadcastTextID: ";
                            textValue.Item1.BroadcastTextIDHelper += string.Join(" - ", textList);
                        }

                    }

                    // Set comment
                    string from = null, to = null;
                    if (!textValue.Item1.SenderGUID.IsEmpty())
                    {
                        if (textValue.Item1.SenderGUID.GetObjectType() == ObjectType.Player)
                            from = "Player";
                        else
                            from = !string.IsNullOrEmpty(textValue.Item1.SenderName) ? textValue.Item1.SenderName : StoreGetters.GetName(StoreNameType.Unit, (int)textValue.Item1.SenderGUID.GetEntry(), false);
                    }

                    if (!textValue.Item1.ReceiverGUID.IsEmpty())
                    {
                        if (textValue.Item1.ReceiverGUID.GetObjectType() == ObjectType.Player)
                            to = "Player";
                        else
                            to = !string.IsNullOrEmpty(textValue.Item1.ReceiverName) ? textValue.Item1.ReceiverName : StoreGetters.GetName(StoreNameType.Unit, (int)textValue.Item1.ReceiverGUID.GetEntry(), false);
                    }

                    Trace.Assert(text.Key == textValue.Item1.SenderGUID.GetEntry() ||
                        text.Key == textValue.Item1.ReceiverGUID.GetEntry());

                    if (from != null && to != null)
                        textValue.Item1.Comment = from + " to " + to;
                    else if (from != null)
                        textValue.Item1.Comment = from;
                    else
                        Trace.Assert(false);
                }
            }

            /* can't use compare DB without knowing values of groupid or id
            var entries = Storage.CreatureTextTemplates.Keys.ToList();
            var creatureTextDb = SQLDatabase.GetDict<uint, CreatureTextTemplate>(entries);
            */

            var rows = new RowList<CreatureTextTemplate>();
            Dictionary<uint, uint> entryCount = new Dictionary<uint, uint>();

            foreach (var text in Storage.CreatureTextTemplates.OrderBy(t => t.Key))
            {
                foreach (var textValue in text.Value)
                {
                    textValue.Item1.Entry = text.Key;
                    var count = entryCount.ContainsKey(text.Key) ? entryCount[text.Key] : 0;

                    var sameTextList = rows.Where(text2 => text2.Data.Entry == text.Key && text2.Data.Text == textValue.Item1.Text);
                    if (sameTextList.Count() != 0)
                        continue;

                    var row = new Row<CreatureTextTemplate>
                    {
                        Data = new CreatureTextTemplate
                        {
                            Entry = textValue.Item1.Entry,
                            GroupId = count,
                            Text = textValue.Item1.Text,
                            Type = textValue.Item1.Type,
                            Language = textValue.Item1.Language,
                            Emote = (textValue.Item1.Emote != null ? textValue.Item1.Emote : 0),
                            Sound = (textValue.Item1.Sound != null ? textValue.Item1.Sound : 0),
                            BroadcastTextID = textValue.Item1.BroadcastTextID,
                            Comment = textValue.Item1.Comment
                        },

                        Comment = textValue.Item1.BroadcastTextIDHelper
                    };

                    if (!entryCount.ContainsKey(text.Key))
                        entryCount.Add(text.Key, count + 1);
                    else
                        entryCount[text.Key] = count + 1;

                    rows.Add(row);
                }
            }

            string result = new SQLInsert<CreatureTextTemplate>(rows).Build();

            if (Settings.SqlTables.creature_text)
            {
                foreach (var text in Storage.CreatureTexts)
                {
                    if (text.Item1.Guid == null)
                        text.Item1.Guid = Storage.GetObjectDbGuid(text.Item1.SenderGUID);
                    var sameTextList = rows.Where(text2 => text2.Data.Entry == text.Item1.Entry && text2.Data.Text == text.Item1.Text);
                    if (sameTextList.Count() != 0)
                    {
                        foreach (var textRow in sameTextList)
                        {
                            text.Item1.GroupId = textRow.Data.GroupId;
                            break;
                        }
                        continue;
                    }
                }

                result += new SQLDelete<CreatureText>(Tuple.Create("0", "999999")).Build();
                result += SQLUtil.Compare(Storage.CreatureTexts, SQLDatabase.Get(Storage.CreatureTexts),
                    t => t.Entry.ToString(), false);
            }

            return result;
        }

        // copy paste of creature text method
        [BuilderMethod]
        public static string GameObjectTextTemplate()
        {
            if (Storage.GameObjectTextTemplates.IsEmpty() || !Settings.SqlTables.gameobject_text_template)
                return string.Empty;

            // For each sound and emote, if the time they were send is in the +1/-1 seconds range of
            // our texts, add that sound and emote to our Storage.GameObjectTextTemplates

            foreach (var text in Storage.GameObjectTextTemplates)
            {
                // For each text
                foreach (var textValue in text.Value)
                {
                    // For each sound
                    foreach (var sound in Storage.Sounds)
                    {
                        DateTime textTime = textValue.Item1.Time;
                        if (sound.guid == textValue.Item1.SenderGUID)
                        {
                            if (System.Math.Abs((textTime - sound.time).TotalSeconds) <= 1)
                                textValue.Item1.Sound = sound.sound;
                        }
                    }

                    List<int> textList;
                    if (SQLDatabase.BroadcastTexts.TryGetValue(textValue.Item1.Text, out textList) ||
                        SQLDatabase.BroadcastText1s.TryGetValue(textValue.Item1.Text, out textList))
                    {
                        if (textList.Count == 1)
                            textValue.Item1.BroadcastTextID = (uint)textList.First();
                        else
                        {
                            textValue.Item1.BroadcastTextID = "PLEASE_SET_A_BROADCASTTEXT_ID";
                            textValue.Item1.BroadcastTextIDHelper = "BroadcastTextID: ";
                            textValue.Item1.BroadcastTextIDHelper += string.Join(" - ", textList);
                        }

                    }

                    // Set comment
                    string from = null, to = null;
                    if (!textValue.Item1.SenderGUID.IsEmpty())
                    {
                        if (textValue.Item1.SenderGUID.GetObjectType() == ObjectType.Player)
                            from = "Player";
                        else
                            from = !string.IsNullOrEmpty(textValue.Item1.SenderName) ? textValue.Item1.SenderName : StoreGetters.GetName(StoreNameType.Unit, (int)textValue.Item1.SenderGUID.GetEntry(), false);
                    }

                    if (!textValue.Item1.ReceiverGUID.IsEmpty())
                    {
                        if (textValue.Item1.ReceiverGUID.GetObjectType() == ObjectType.Player)
                            to = "Player";
                        else
                            to = !string.IsNullOrEmpty(textValue.Item1.ReceiverName) ? textValue.Item1.ReceiverName : StoreGetters.GetName(StoreNameType.Unit, (int)textValue.Item1.ReceiverGUID.GetEntry(), false);
                    }

                    Trace.Assert(text.Key == textValue.Item1.SenderGUID.GetEntry() ||
                        text.Key == textValue.Item1.ReceiverGUID.GetEntry());

                    if (from != null && to != null)
                        textValue.Item1.Comment = from + " to " + to;
                    else if (from != null)
                        textValue.Item1.Comment = from;
                    else
                        Trace.Assert(false);
                }
            }

            /* can't use compare DB without knowing values of groupid or id
            var entries = Storage.GameObjectTextTemplates.Keys.ToList();
            var gameObjectTextDb = SQLDatabase.GetDict<uint, GameObjectTextTemplates>(entries);
            */

            var rows = new RowList<GameObjectTextTemplate>();
            Dictionary<uint, uint> entryCount = new Dictionary<uint, uint>();

            foreach (var text in Storage.GameObjectTextTemplates.OrderBy(t => t.Key))
            {
                foreach (var textValue in text.Value)
                {
                    textValue.Item1.Entry = text.Key;
                    var count = entryCount.ContainsKey(text.Key) ? entryCount[text.Key] : 0;

                    var sameTextList = rows.Where(text2 => text2.Data.Entry == text.Key && text2.Data.Text == textValue.Item1.Text);
                    if (sameTextList.Count() != 0)
                        continue;

                    var row = new Row<GameObjectTextTemplate>
                    {
                        Data = new GameObjectTextTemplate
                        {
                            Entry = textValue.Item1.Entry,
                            GroupId = count,
                            Text = textValue.Item1.Text,
                            Type = textValue.Item1.Type,
                            Language = textValue.Item1.Language,
                            Sound = (textValue.Item1.Sound != null ? textValue.Item1.Sound : 0),
                            BroadcastTextID = textValue.Item1.BroadcastTextID,
                            Comment = textValue.Item1.Comment
                        },

                        Comment = textValue.Item1.BroadcastTextIDHelper
                    };

                    if (!entryCount.ContainsKey(text.Key))
                        entryCount.Add(text.Key, count + 1);
                    else
                        entryCount[text.Key] = count + 1;

                    rows.Add(row);
                }
            }

            string result = new SQLInsert<GameObjectTextTemplate>(rows, false).Build();

            if (Settings.SqlTables.gameobject_text)
            {
                foreach (var text in Storage.GameObjectTexts)
                {
                    if (text.Item1.Guid == null)
                        text.Item1.Guid = Storage.GetObjectDbGuid(text.Item1.SenderGUID);

                    var sameTextList = rows.Where(text2 => text2.Data.Entry == text.Item1.Entry && text2.Data.Text == text.Item1.Text);
                    if (sameTextList.Count() != 0)
                    {
                        foreach (var textRow in sameTextList)
                        {
                            text.Item1.GroupId = textRow.Data.GroupId;
                            break;
                        }
                        continue;
                    }
                }

                result += SQLUtil.Compare(Storage.GameObjectTexts, SQLDatabase.Get(Storage.GameObjectTexts),
                t => t.Entry.ToString());
            }

            return result;
        }

        [BuilderMethod]
        public static string PlaySound()
        {
            if (Storage.Sounds.Count == 0 || !Settings.SqlTables.play_sound)
                return string.Empty;

            var soundRows = new RowList<PlaySound>();
            foreach (var sound in Storage.Sounds)
            {
                Row<PlaySound> row = new Row<PlaySound>();
                row.Data.Sound = sound.sound;
                Storage.GetObjectDbGuidEntryType(sound.guid, out row.Data.SourceGuid, out row.Data.SourceEntry, out row.Data.SourceType);
                row.Data.UnixTimeMs = (ulong)Utilities.GetUnixTimeMsFromDateTime(sound.time);
                soundRows.Add(row);
            }

            var soundsSql = new SQLInsert<PlaySound>(soundRows, false);
            return soundsSql.Build();
        }

        [BuilderMethod]
        public static string PlayMusic()
        {
            if (Storage.Music.IsEmpty() || !Settings.SqlTables.play_music)
                return string.Empty;

            var result = "";

            result += SQLUtil.Compare(Storage.Music, SQLDatabase.Get(Storage.Music),
                t => t.Music.ToString());

            return result;
        }

        [BuilderMethod]
        public static string VehicleAccessory()
        {
            if (Storage.VehicleTemplateAccessories.IsEmpty() || !Settings.SqlTables.vehicle_template_accessory)
                return string.Empty;

            var rows = new RowList<VehicleTemplateAccessory>();
            foreach (var accessory in Storage.VehicleTemplateAccessories)
            {
                if (accessory.Item1.SeatId < 0 || accessory.Item1.SeatId > 7)
                    continue;

                // ReSharper disable once UseObjectOrCollectionInitializer
                var row = new Row<VehicleTemplateAccessory>();
                row.Comment = StoreGetters.GetName(StoreNameType.Unit, (int)accessory.Item1.Entry.GetValueOrDefault(), false) + " - ";
                row.Comment += StoreGetters.GetName(StoreNameType.Unit, (int)accessory.Item1.AccessoryEntry.GetValueOrDefault(), false);
                accessory.Item1.Description = row.Comment;
                row.Data = accessory.Item1;

                rows.Add(row);
            }

            return new SQLInsert<VehicleTemplateAccessory>(rows, false).Build();
        }

        [BuilderMethod]
        public static string NpcSpellClick()
        {
            if (Storage.NpcSpellClicks.IsEmpty() || !Settings.SqlTables.npc_spellclick_spells)
                return string.Empty;

            var rows = new RowList<NpcSpellClick>();

            foreach (var npcSpellClick in Storage.NpcSpellClicks)
            {
                foreach (var spellClick in Storage.SpellClicks)
                {
                    var row = new Row<NpcSpellClick>();

                    if (spellClick.Item1.CasterGUID.GetObjectType() == ObjectType.Unit && spellClick.Item1.TargetGUID.GetObjectType() == ObjectType.Unit)
                        spellClick.Item1.CastFlags = 0x0;
                    if (spellClick.Item1.CasterGUID.GetObjectType() == ObjectType.Player && spellClick.Item1.TargetGUID.GetObjectType() == ObjectType.Unit)
                        spellClick.Item1.CastFlags = 0x1;
                    if (spellClick.Item1.CasterGUID.GetObjectType() == ObjectType.Unit && spellClick.Item1.TargetGUID.GetObjectType() == ObjectType.Player)
                        spellClick.Item1.CastFlags = 0x2;
                    if (spellClick.Item1.CasterGUID.GetObjectType() == ObjectType.Player && spellClick.Item1.TargetGUID.GetObjectType() == ObjectType.Player)
                        spellClick.Item1.CastFlags = 0x3;

                    spellClick.Item1.Entry = npcSpellClick.Item1.GetEntry();
                    row.Data = spellClick.Item1;

                    var timeSpan = spellClick.Item2 - npcSpellClick.Item2;
                    if (timeSpan != null && timeSpan.Value.Duration() <= TimeSpan.FromSeconds(1))
                        rows.Add(row);
                }
            }

            return new SQLInsert<NpcSpellClick>(rows, false).Build();
        }

        [BuilderMethod(Units = true)]
        public static string NpcSpellClickMop(Dictionary<WowGuid, Unit> units)
        {
            if (units.Count == 0 || !Settings.SqlTables.npc_spellclick_spells)
                return string.Empty;

            var rows = new RowList<NpcSpellClick>();

            foreach (var unit in units)
            {
                var row = new Row<NpcSpellClick>();

                var npc = unit.Value;
                if (npc.UnitData.InteractSpellID == 0)
                    continue;

                if (Settings.AreaFilters.Length > 0)
                    if (!npc.Area.ToString(CultureInfo.InvariantCulture).MatchesFilters(Settings.AreaFilters))
                        continue;

                if (Settings.MapFilters.Length > 0)
                    if (!npc.Map.ToString(CultureInfo.InvariantCulture).MatchesFilters(Settings.MapFilters))
                        continue;

                row.Data.Entry = unit.Key.GetEntry();
                row.Data.SpellID = (uint)npc.UnitData.InteractSpellID;

                rows.Add(row);
            }

            return new SQLInsert<NpcSpellClick>(rows, false).Build();
        }
    }
}

using System;
using WowPacketParser.Enums;

namespace WowPacketParser.Misc
{
    public static class Settings
    {
        private static readonly Configuration Conf = new Configuration();

        public static readonly string[] Filters = Conf.GetStringList("Filters", new string[0]);
        public static readonly string[] IgnoreFilters = Conf.GetStringList("IgnoreFilters", new string[0]);
        public static readonly string[] IgnoreByEntryFilters = Conf.GetStringList("IgnoreByEntryFilters", new string[0]);
        public static readonly string[] MapFilters = Conf.GetStringList("MapFilters", new string[0]);
        public static readonly string[] AreaFilters = Conf.GetStringList("AreaFilters", new string[0]);
        public static readonly int FilterPacketsNum = Conf.GetInt("FilterPacketsNum", 0);
        public static readonly ClientVersionBuild ClientBuild = Conf.GetEnum("ClientBuild", ClientVersionBuild.Zero);
        public static readonly TargetedDatabase TargetedDatabase = Conf.GetEnum("TargetedDatabase", TargetedDatabase.WrathOfTheLichKing);
        public static readonly DumpFormatType DumpFormat = Conf.GetEnum("DumpFormat", DumpFormatType.Text);
        public static readonly ulong SQLOutputFlag = GetSQLOutputFlag();
        public static readonly bool SQLOrderByKey = Conf.GetBoolean("SqlOrderByKey", false);
        public static readonly bool RandomizePlayerNames = Conf.GetBoolean("RandomizePlayerNames", false);
        public static readonly bool SkipOtherPlayers = Conf.GetBoolean("SkipOtherPlayers", false);
        public static readonly bool SavePlayerCasts = Conf.GetBoolean("SavePlayerCasts", false);
        public static readonly bool SaveHealthUpdates = Conf.GetBoolean("SaveHealthUpdates", false);
        public static readonly bool SaveManaUpdates = Conf.GetBoolean("SaveManaUpdates", false);
        public static readonly bool SaveTempSpawns = Conf.GetBoolean("SaveTempSpawns", false);
        public static readonly bool SkipOnlyVerifiedBuildUpdateRows = Conf.GetBoolean("SkipOnlyVerifiedBuildUpdateRows", false);
        public static readonly bool IgnoreZeroValues = Conf.GetBoolean("IgnoreZeroValues", false);
        public static readonly bool ForceInsertQueries = Conf.GetBoolean("ForceInsertQueries", false);
        public static readonly bool RecalcDiscount = Conf.GetBoolean("RecalcDiscount", false);
        public static readonly string SQLFileName = Conf.GetString("SQLFileName", string.Empty);
        public static readonly bool ShowEndPrompt = Conf.GetBoolean("ShowEndPrompt", false);
        public static readonly bool LogErrors = Conf.GetBoolean("LogErrors", false);
        public static readonly bool LogPacketErrors = Conf.GetBoolean("LogPacketErrors", false);
        public static readonly ParsedStatus OutputFlag = Conf.GetEnum("OutputFlag", ParsedStatus.All);
        public static readonly bool DebugReads = Conf.GetBoolean("DebugReads", false);
        public static readonly bool ParsingLog = Conf.GetBoolean("ParsingLog", false);
        public static readonly bool DevMode = Conf.GetBoolean("DevMode", false);
        public static readonly int Threads = Conf.GetInt("Threads", 8);

        public static readonly bool SSHEnabled = Conf.GetBoolean("SSHEnabled", false);
        public static readonly string SSHHost = Conf.GetString("SSHHost", "localhost");
        public static readonly string SSHUsername = Conf.GetString("SSHUsername", string.Empty);
        public static readonly string SSHPassword = Conf.GetString("SSHPassword", string.Empty);
        public static readonly int SSHPort = Conf.GetInt("SSHPort", 22);
        public static readonly int SSHLocalPort = Conf.GetInt("SSHLocalPort", 3307);

        public static readonly bool DBEnabled = Conf.GetBoolean("DBEnabled", false);
        public static readonly string Server = Conf.GetString("Server", "localhost");
        public static readonly string Port = Conf.GetString("Port", "3306");
        public static readonly string Username = Conf.GetString("Username", "root");
        public static readonly string Password = Conf.GetString("Password", string.Empty);
        public static readonly string WPPDatabase = Conf.GetString("WPPDatabase", "WPP");
        public static readonly string TDBDatabase = Conf.GetString("TDBDatabase", "world");
        public static readonly string HotfixesDatabase = Conf.GetString("HotfixesDatabase", "hotfixes");
        public static readonly string CharacterSet = Conf.GetString("CharacterSet", "utf8");

        // DB2
        public static readonly string DBCPath = Conf.GetString("DBCPath", $@"\dbc");
        public static readonly string DBCLocale = Conf.GetString("DBCLocale", "enUS");
        public static readonly bool UseDBC = Conf.GetBoolean("UseDBC", false);
        public static readonly bool ParseSpellInfos = Conf.GetBoolean("ParseSpellInfos", false);

        private static ulong GetSQLOutputFlag()
        {
            var names = Enum.GetNames(typeof(SQLOutput));
            var values = Enum.GetValues(typeof(SQLOutput));

            var result = 0ul;

            for (var i = 0; i < names.Length; ++i)
            {
                if (Conf.GetBoolean(names[i], false))
                    result += (1ul << (int)values.GetValue(i));
            }

            return result;
        }

        public static bool DumpFormatWithText()
        {
            return DumpFormat != DumpFormatType.SqlOnly && DumpFormat != DumpFormatType.SniffDataOnly;
        }

        public static bool DumpFormatWithSQL()
        {
            return DumpFormat == DumpFormatType.SniffDataOnly ||
                   DumpFormat == DumpFormatType.SqlOnly ||
                   DumpFormat == DumpFormatType.Text;
        }
        public sealed class SqlTables
        {
            public static readonly bool ObjectNames = Conf.GetBoolean("ObjectNames", false);
            public static readonly bool SniffData = Conf.GetBoolean("SniffData", false);
            public static readonly bool SniffDataOpcodes = Conf.GetBoolean("SniffDataOpcodes", false);

            public static readonly bool client_creature_interact = Conf.GetBoolean("client_creature_interact", false);
            public static readonly bool client_gameobject_use = Conf.GetBoolean("client_gameobject_use", false);
            public static readonly bool client_item_use = Conf.GetBoolean("client_item_use", false);
            public static readonly bool client_quest_accept = Conf.GetBoolean("client_quest_accept", false);
            public static readonly bool client_quest_complete = Conf.GetBoolean("client_quest_complete", false);
            public static readonly bool client_reclaim_corpse = Conf.GetBoolean("client_reclaim_corpse", false);
            public static readonly bool client_release_spirit = Conf.GetBoolean("client_release_spirit", false);
            public static readonly bool creature = Conf.GetBoolean("creature", false);
            public static readonly bool creature_attack_log = Conf.GetBoolean("creature_attack_log", false);
            public static readonly bool creature_attack_start = Conf.GetBoolean("creature_attack_start", false);
            public static readonly bool creature_attack_stop = Conf.GetBoolean("creature_attack_stop", false);
            public static readonly bool creature_auras_update = Conf.GetBoolean("creature_auras_update", false);
            public static readonly bool creature_create1_time = Conf.GetBoolean("creature_create1_time", false);
            public static readonly bool creature_create2_time = Conf.GetBoolean("creature_create2_time", false);
            public static readonly bool creature_destroy_time = Conf.GetBoolean("creature_destroy_time", false);
            public static readonly bool creature_emote = Conf.GetBoolean("creature_emote", false);
            public static readonly bool creature_equipment_values_update = Conf.GetBoolean("creature_equipment_values_update", false);
            public static readonly bool creature_guid_values = Conf.GetBoolean("creature_guid_values", false);
            public static readonly bool creature_guid_values_update = Conf.GetBoolean("creature_guid_values_update", false);
            public static readonly bool creature_movement_client = Conf.GetBoolean("creature_movement_client", false);
            public static readonly bool creature_movement_server = Conf.GetBoolean("creature_movement_server", false);
            public static readonly bool creature_movement_server_combat = Conf.GetBoolean("creature_movement_server_combat", false);
            public static readonly bool creature_speed_update = Conf.GetBoolean("creature_speed_update", false);
            public static readonly bool creature_text = Conf.GetBoolean("creature_text", false);
            public static readonly bool creature_text_template = Conf.GetBoolean("creature_text_template", false);
            public static readonly bool creature_values_update = Conf.GetBoolean("creature_values_update", false);
            public static readonly bool dynamicobject = Conf.GetBoolean("dynamicobject", false);
            public static readonly bool dynamicobject_create1_time = Conf.GetBoolean("dynamicobject_create1_time", false);
            public static readonly bool dynamicobject_create2_time = Conf.GetBoolean("dynamicobject_create2_time", false);
            public static readonly bool dynamicobject_destroy_time = Conf.GetBoolean("dynamicobject_destroy_time", false);
            public static readonly bool gameobject = Conf.GetBoolean("gameobject", false);
            public static readonly bool gameobject_create1_time = Conf.GetBoolean("gameobject_create1_time", false);
            public static readonly bool gameobject_create2_time = Conf.GetBoolean("gameobject_create2_time", false);
            public static readonly bool gameobject_custom_anim = Conf.GetBoolean("gameobject_custom_anim", false);
            public static readonly bool gameobject_despawn_anim = Conf.GetBoolean("gameobject_despawn_anim", false);
            public static readonly bool gameobject_destroy_time = Conf.GetBoolean("gameobject_destroy_time", false);
            public static readonly bool gameobject_text = Conf.GetBoolean("gameobject_text", false);
            public static readonly bool gameobject_text_template = Conf.GetBoolean("gameobject_text_template", false);
            public static readonly bool gameobject_values_update = Conf.GetBoolean("gameobject_values_update", false);
            public static readonly bool play_music = Conf.GetBoolean("play_music", false);
            public static readonly bool play_sound = Conf.GetBoolean("play_sound", false);
            public static readonly bool play_spell_visual_kit = Conf.GetBoolean("play_spell_visual_kit", false);
            public static readonly bool player = Conf.GetBoolean("player", false);
            public static readonly bool player_active_player = Conf.GetBoolean("player_active_player", false);
            public static readonly bool player_attack_log = Conf.GetBoolean("player_attack_log", false);
            public static readonly bool player_attack_start = Conf.GetBoolean("player_attack_start", false);
            public static readonly bool player_attack_stop = Conf.GetBoolean("player_attack_stop", false);
            public static readonly bool player_auras_update = Conf.GetBoolean("player_auras_update", false);
            public static readonly bool player_chat = Conf.GetBoolean("player_chat", false);
            public static readonly bool player_create1_time = Conf.GetBoolean("player_create1_time", false);
            public static readonly bool player_create2_time = Conf.GetBoolean("player_create2_time", false);
            public static readonly bool player_destroy_time = Conf.GetBoolean("player_destroy_time", false);
            public static readonly bool player_emote = Conf.GetBoolean("player_emote", false);
            public static readonly bool player_equipment_values_update = Conf.GetBoolean("player_equipment_values_update", false);
            public static readonly bool player_guid_values = Conf.GetBoolean("player_guid_values", false);
            public static readonly bool player_guid_values_update = Conf.GetBoolean("player_guid_values_update", false);
            public static readonly bool player_movement_client = Conf.GetBoolean("player_movement_client", false);
            public static readonly bool player_movement_server = Conf.GetBoolean("player_movement_server", false);
            public static readonly bool player_speed_update = Conf.GetBoolean("player_speed_update", false);
            public static readonly bool player_values_update = Conf.GetBoolean("player_values_update", false);
            public static readonly bool quest_complete_time = Conf.GetBoolean("quest_complete_time", false);
            public static readonly bool quest_fail_time = Conf.GetBoolean("quest_fail_time", false);
            public static readonly bool spell_cast_failed = Conf.GetBoolean("spell_cast_failed", false);
            public static readonly bool spell_cast_go = Conf.GetBoolean("spell_cast_go", false);
            public static readonly bool spell_cast_start = Conf.GetBoolean("spell_cast_start", false);
            public static readonly bool spell_channel_start = Conf.GetBoolean("spell_channel_start", false);
            public static readonly bool spell_channel_update = Conf.GetBoolean("spell_channel_update", false);
            public static readonly bool weather_updates = Conf.GetBoolean("weather_updates", false);
            public static readonly bool world_state_init = Conf.GetBoolean("world_state_init", false);
            public static readonly bool world_state_update = Conf.GetBoolean("world_state_update", false);
            public static readonly bool world_text = Conf.GetBoolean("world_text", false);

            public static readonly bool characters = Conf.GetBoolean("characters", false);
            public static readonly bool character_inventory = Conf.GetBoolean("character_inventory", false);
            public static readonly bool guild = Conf.GetBoolean("guild", false);
            public static readonly bool guild_rank = Conf.GetBoolean("guild_rank", false);

            public static readonly bool areatrigger_template = Conf.GetBoolean("areatrigger_template", false);
            public static readonly bool areatrigger_template_polygon_vertices = Conf.GetBoolean("areatrigger_template_polygon_vertices", false);
            public static readonly bool broadcast_text = Conf.GetBoolean("broadcast_text", false);
            public static readonly bool broadcast_text_locale = Conf.GetBoolean("broadcast_text_locale", false);
            public static readonly bool conversation_actor_template = Conf.GetBoolean("conversation_actor_template", false);
            public static readonly bool conversation_actors = Conf.GetBoolean("conversation_actors", false);
            public static readonly bool conversation_line_template = Conf.GetBoolean("conversation_line_template", false);
            public static readonly bool conversation_template = Conf.GetBoolean("conversation_template", false);
            public static readonly bool creature_addon = Conf.GetBoolean("creature_addon", false);
            public static readonly bool creature_display_info_addon = Conf.GetBoolean("creature_display_info_addon", false);
            public static readonly bool creature_equip_template = Conf.GetBoolean("creature_equip_template", false);
            public static readonly bool creature_gossip = Conf.GetBoolean("creature_gossip", false);
            public static readonly bool creature_loot = Conf.GetBoolean("creature_loot", false);
            public static readonly bool creature_stats = Conf.GetBoolean("creature_stats", false);
            public static readonly bool creature_template = Conf.GetBoolean("creature_template", false);
            public static readonly bool creature_template_addon = Conf.GetBoolean("creature_template_addon", false);
            public static readonly bool creature_template_locale = Conf.GetBoolean("creature_template_locale", false);
            public static readonly bool creature_template_scaling = Conf.GetBoolean("creature_template_scaling", false);
            public static readonly bool creature_template_wdb = Conf.GetBoolean("creature_template_wdb", false);
            public static readonly bool gameobject_addon = Conf.GetBoolean("gameobject_addon", false);
            public static readonly bool gameobject_loot = Conf.GetBoolean("gameobject_loot", false);
            public static readonly bool gameobject_template = Conf.GetBoolean("gameobject_template", false);
            public static readonly bool gameobject_template_addon = Conf.GetBoolean("gameobject_template_addon", false);
            public static readonly bool gossip_menu = Conf.GetBoolean("gossip_menu", false);
            public static readonly bool gossip_menu_option = Conf.GetBoolean("gossip_menu_option", false);
            public static readonly bool item_template = Conf.GetBoolean("item_template", false);
            public static readonly bool locales_quest = Conf.GetBoolean("locales_quest", false);
            public static readonly bool locales_quest_objectives = Conf.GetBoolean("locales_quest_objectives", false);
            public static readonly bool npc_spellclick_spells = Conf.GetBoolean("npc_spellclick_spells", false);
            public static readonly bool npc_text = Conf.GetBoolean("npc_text", false);
            public static readonly bool npc_trainer = Conf.GetBoolean("npc_trainer", false);
            public static readonly bool npc_vendor = Conf.GetBoolean("npc_vendor", false);
            public static readonly bool page_text = Conf.GetBoolean("page_text", false);
            public static readonly bool page_text_locale = Conf.GetBoolean("page_text_locale", false);
            public static readonly bool playerchoice = Conf.GetBoolean("playerchoice", false);
            public static readonly bool playerchoice_locale = Conf.GetBoolean("playerchoice_locale", false);
            public static readonly bool playercreateinfo = Conf.GetBoolean("playercreateinfo", false);
            public static readonly bool playercreateinfo_action = Conf.GetBoolean("playercreateinfo_action", false);
            public static readonly bool points_of_interest = Conf.GetBoolean("points_of_interest", false);
            public static readonly bool quest_ender = Conf.GetBoolean("quest_ender", false); 
            public static readonly bool quest_poi = Conf.GetBoolean("quest_poi", false);
            public static readonly bool quest_poi_points = Conf.GetBoolean("quest_poi_points", false);
            public static readonly bool quest_starter = Conf.GetBoolean("quest_starter", false);
            public static readonly bool quest_template = Conf.GetBoolean("quest_template", false);
            public static readonly bool scenario_poi = Conf.GetBoolean("scenario_poi", false);
            public static readonly bool scene_template = Conf.GetBoolean("scene_template", false);
            public static readonly bool spell_areatrigger = Conf.GetBoolean("spell_areatrigger", false);
            public static readonly bool spell_pet_action = Conf.GetBoolean("spell_pet_action", false);
            public static readonly bool spell_pet_cooldown = Conf.GetBoolean("spell_pet_cooldown", false);
            public static readonly bool spell_target_position = Conf.GetBoolean("spell_target_position", false);
            public static readonly bool trainer = Conf.GetBoolean("trainer", false);
            public static readonly bool vehicle_template_accessory = Conf.GetBoolean("vehicle_template_accessory", false);
            
            public static readonly bool hotfix_data = Conf.GetBoolean("hotfix_data", false);
            public static readonly bool hotfix_blob = Conf.GetBoolean("hotfix_blob", false);
        }
    }
}

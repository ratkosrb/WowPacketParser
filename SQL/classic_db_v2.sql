-- --------------------------------------------------------
-- Host:                         127.0.0.1
-- Server version:               5.5.53 - MySQL Community Server (GPL)
-- Server OS:                    Win32
-- HeidiSQL Version:             9.3.0.4998
-- --------------------------------------------------------

/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET NAMES utf8mb4 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;

-- Dumping structure for table sniffs_new_test.broadcast_text
DROP TABLE IF EXISTS `broadcast_text`;
CREATE TABLE IF NOT EXISTS `broadcast_text` (
  `entry` int(10) unsigned NOT NULL,
  `male_text` varchar(1024) COLLATE latin1_general_ci NOT NULL DEFAULT '',
  `female_text` varchar(1024) COLLATE latin1_general_ci NOT NULL DEFAULT '',
  `language_id` int(11) NOT NULL DEFAULT '0',
  `condition_id` int(11) NOT NULL DEFAULT '0',
  `emotes_id` int(11) NOT NULL DEFAULT '0',
  `flags` int(11) NOT NULL DEFAULT '0',
  `chat_bubble_duration` int(11) NOT NULL DEFAULT '0',
  `sound_id1` int(11) NOT NULL DEFAULT '0',
  `sound_id2` int(11) NOT NULL DEFAULT '0',
  `emote_id1` int(11) NOT NULL DEFAULT '0',
  `emote_id2` int(11) NOT NULL DEFAULT '0',
  `emote_id3` int(11) NOT NULL DEFAULT '0',
  `emote_delay1` int(11) NOT NULL DEFAULT '0',
  `emote_delay2` int(11) NOT NULL DEFAULT '0',
  `emote_delay3` int(11) NOT NULL DEFAULT '0',
  `sniff_build` mediumint(8) unsigned NOT NULL DEFAULT '0',
  PRIMARY KEY (`entry`)
) ENGINE=MyISAM DEFAULT CHARSET=latin1 COLLATE=latin1_general_ci;

-- Data exporting was unselected.


-- Dumping structure for table sniffs_new_test.broadcast_text_locale
DROP TABLE IF EXISTS `broadcast_text_locale`;
CREATE TABLE IF NOT EXISTS `broadcast_text_locale` (
  `entry` mediumint(8) unsigned NOT NULL DEFAULT '0',
  `locale` varchar(4) NOT NULL,
  `male_text` text,
  `female_text` text,
  `sniff_build` mediumint(8) unsigned NOT NULL DEFAULT '0',
  PRIMARY KEY (`entry`,`locale`)
) ENGINE=MyISAM DEFAULT CHARSET=utf8;

-- Data exporting was unselected.


-- Dumping structure for table sniffs_new_test.cinematic_begin
DROP TABLE IF EXISTS `cinematic_begin`;
CREATE TABLE IF NOT EXISTS `cinematic_begin` (
  `unixtimems` bigint(20) unsigned NOT NULL COMMENT 'when the packet was received',
  `cinematic_id` int(10) unsigned NOT NULL,
  PRIMARY KEY (`unixtimems`)
) ENGINE=MyISAM DEFAULT CHARSET=latin1 COLLATE=latin1_general_ci COMMENT='from SMSG_TRIGGER_CINEMATIC';

-- Data exporting was unselected.


-- Dumping structure for table sniffs_new_test.cinematic_end
DROP TABLE IF EXISTS `cinematic_end`;
CREATE TABLE IF NOT EXISTS `cinematic_end` (
  `unixtimems` bigint(20) unsigned NOT NULL COMMENT 'when the packet was sent',
  PRIMARY KEY (`unixtimems`)
) ENGINE=MyISAM DEFAULT CHARSET=latin1 COLLATE=latin1_general_ci COMMENT='from CMSG_COMPLETE_CINEMATIC';

-- Data exporting was unselected.


-- Dumping structure for table sniffs_new_test.client_areatrigger_enter
DROP TABLE IF EXISTS `client_areatrigger_enter`;
CREATE TABLE IF NOT EXISTS `client_areatrigger_enter` (
  `unixtimems` bigint(20) unsigned NOT NULL COMMENT 'when the packet was sent',
  `areatrigger_id` int(10) unsigned NOT NULL,
  PRIMARY KEY (`unixtimems`,`areatrigger_id`)
) ENGINE=MyISAM DEFAULT CHARSET=latin1 COLLATE=latin1_general_ci COMMENT='times when the client notified the server it has entered an areatrigger''s confines\r\nfrom CMSG_AREA_TRIGGER';

-- Data exporting was unselected.


-- Dumping structure for table sniffs_new_test.client_areatrigger_leave
DROP TABLE IF EXISTS `client_areatrigger_leave`;
CREATE TABLE IF NOT EXISTS `client_areatrigger_leave` (
  `unixtimems` bigint(20) unsigned NOT NULL COMMENT 'when the packet was sent',
  `areatrigger_id` int(10) unsigned NOT NULL,
  PRIMARY KEY (`unixtimems`,`areatrigger_id`)
) ENGINE=MyISAM DEFAULT CHARSET=latin1 COLLATE=latin1_general_ci COMMENT='times when the client notified the server it has left an areatrigger''s confines\r\nfrom CMSG_AREA_TRIGGER';

-- Data exporting was unselected.


-- Dumping structure for table sniffs_new_test.client_creature_interact
DROP TABLE IF EXISTS `client_creature_interact`;
CREATE TABLE IF NOT EXISTS `client_creature_interact` (
  `unixtimems` bigint(20) unsigned NOT NULL COMMENT 'when the packet was sent',
  `guid` int(10) unsigned NOT NULL COMMENT 'creature spawn guid',
  PRIMARY KEY (`guid`,`unixtimems`)
) ENGINE=MyISAM DEFAULT CHARSET=latin1 COLLATE=latin1_general_ci ROW_FORMAT=COMPACT COMMENT='times when the client talked to a creature';

-- Data exporting was unselected.


-- Dumping structure for table sniffs_new_test.client_gameobject_use
DROP TABLE IF EXISTS `client_gameobject_use`;
CREATE TABLE IF NOT EXISTS `client_gameobject_use` (
  `unixtimems` bigint(20) unsigned NOT NULL COMMENT 'when the packet was sent',
  `guid` int(10) unsigned NOT NULL COMMENT 'gameobject spawn guid',
  PRIMARY KEY (`guid`,`unixtimems`)
) ENGINE=MyISAM DEFAULT CHARSET=latin1 COLLATE=latin1_general_ci ROW_FORMAT=COMPACT COMMENT='times when client used a gameobject\r\nfrom CMSG_GAME_OBJ_USE and CMSG_GAME_OBJ_REPORT_USE';

-- Data exporting was unselected.


-- Dumping structure for table sniffs_new_test.client_item_use
DROP TABLE IF EXISTS `client_item_use`;
CREATE TABLE IF NOT EXISTS `client_item_use` (
  `unixtimems` bigint(20) unsigned NOT NULL COMMENT 'when the packet was sent',
  `entry` int(10) unsigned NOT NULL COMMENT 'item template entry',
  PRIMARY KEY (`entry`,`unixtimems`)
) ENGINE=MyISAM DEFAULT CHARSET=latin1 COLLATE=latin1_general_ci ROW_FORMAT=COMPACT COMMENT='times when client used an item\r\nfrom CMSG_USE_ITEM';

-- Data exporting was unselected.


-- Dumping structure for table sniffs_new_test.client_quest_accept
DROP TABLE IF EXISTS `client_quest_accept`;
CREATE TABLE IF NOT EXISTS `client_quest_accept` (
  `unixtimems` bigint(20) unsigned NOT NULL COMMENT 'when the packet was sent',
  `object_guid` int(10) unsigned NOT NULL COMMENT 'guid of the quest ender object',
  `object_id` int(10) unsigned NOT NULL COMMENT 'entry of the quest ender object',
  `object_type` varchar(16) COLLATE latin1_general_ci NOT NULL COMMENT 'type of the quest ender object',
  `quest_id` int(10) unsigned NOT NULL COMMENT 'references quest_template',
  PRIMARY KEY (`unixtimems`)
) ENGINE=MyISAM DEFAULT CHARSET=latin1 COLLATE=latin1_general_ci ROW_FORMAT=COMPACT COMMENT='times when a quest was accepted by client';

-- Data exporting was unselected.


-- Dumping structure for table sniffs_new_test.client_quest_complete
DROP TABLE IF EXISTS `client_quest_complete`;
CREATE TABLE IF NOT EXISTS `client_quest_complete` (
  `unixtimems` bigint(20) unsigned NOT NULL COMMENT 'when the packet was sent',
  `object_guid` int(10) unsigned NOT NULL COMMENT 'guid of the quest ender object',
  `object_id` int(10) unsigned NOT NULL COMMENT 'entry of the quest ender object',
  `object_type` varchar(16) COLLATE latin1_general_ci NOT NULL COMMENT 'type of the quest ender object',
  `quest_id` int(10) unsigned NOT NULL COMMENT 'references quest_template',
  PRIMARY KEY (`unixtimems`)
) ENGINE=MyISAM DEFAULT CHARSET=latin1 COLLATE=latin1_general_ci ROW_FORMAT=COMPACT COMMENT='times when a quest was turned in by client';

-- Data exporting was unselected.


-- Dumping structure for table sniffs_new_test.client_reclaim_corpse
DROP TABLE IF EXISTS `client_reclaim_corpse`;
CREATE TABLE IF NOT EXISTS `client_reclaim_corpse` (
  `unixtimems` bigint(20) unsigned NOT NULL COMMENT 'when the packet was sent',
  PRIMARY KEY (`unixtimems`)
) ENGINE=MyISAM DEFAULT CHARSET=latin1 COLLATE=latin1_general_ci ROW_FORMAT=COMPACT COMMENT='from CMSG_RECLAIM_CORPSE';

-- Data exporting was unselected.


-- Dumping structure for table sniffs_new_test.client_release_spirit
DROP TABLE IF EXISTS `client_release_spirit`;
CREATE TABLE IF NOT EXISTS `client_release_spirit` (
  `unixtimems` bigint(20) unsigned NOT NULL COMMENT 'when the packet was sent',
  PRIMARY KEY (`unixtimems`)
) ENGINE=MyISAM DEFAULT CHARSET=latin1 COLLATE=latin1_general_ci ROW_FORMAT=COMPACT COMMENT='from CMSG_REPOP_REQUEST';

-- Data exporting was unselected.


-- Dumping structure for table sniffs_new_test.creature
DROP TABLE IF EXISTS `creature`;
CREATE TABLE IF NOT EXISTS `creature` (
  `guid` bigint(20) unsigned NOT NULL DEFAULT '0',
  `original_id` mediumint(8) unsigned NOT NULL DEFAULT '0' COMMENT 'entry from guid',
  `id` mediumint(8) unsigned NOT NULL DEFAULT '0' COMMENT 'entry from update fields',
  `total_observation_time` mediumint(8) unsigned NOT NULL DEFAULT '0' COMMENT 'accumulated time in ms between create and destroy packets',
  `longest_observation_time` mediumint(8) unsigned NOT NULL DEFAULT '0' COMMENT 'longest uninterrupted period of time in ms the object was visible for',
  `map` smallint(5) unsigned DEFAULT '0' COMMENT 'Map Identifier',
  `zone_id` smallint(5) unsigned NOT NULL DEFAULT '0' COMMENT 'Zone Identifier',
  `area_id` smallint(5) unsigned NOT NULL DEFAULT '0' COMMENT 'Area Identifier',
  `position_x` float NOT NULL DEFAULT '0',
  `position_y` float NOT NULL DEFAULT '0',
  `position_z` float NOT NULL DEFAULT '0',
  `orientation` float NOT NULL DEFAULT '0',
  `wander_distance` float NOT NULL DEFAULT '0' COMMENT 'maximum distance traveled from initial position',
  `waypoint_count` smallint(5) unsigned NOT NULL DEFAULT '0' COMMENT 'number of out of combat movement packets seen',
  `movement_type` tinyint(3) unsigned NOT NULL DEFAULT '0' COMMENT 'guessed movement generator',
  `is_spawn` tinyint(3) unsigned NOT NULL DEFAULT '0' COMMENT 'create object type 2',
  `is_hovering` tinyint(3) unsigned NOT NULL DEFAULT '0' COMMENT 'hover bit from create object',
  `is_pet` tinyint(3) unsigned NOT NULL DEFAULT '0' COMMENT 'highguid is pet',
  `is_vehicle` tinyint(3) unsigned NOT NULL DEFAULT '0' COMMENT 'highguid is vehicle',
  `is_on_transport` tinyint(3) unsigned NOT NULL DEFAULT '0',
  `is_temporary` tinyint(3) unsigned NOT NULL DEFAULT '0',
  `summon_spell` int(10) unsigned NOT NULL DEFAULT '0',
  `scale` float NOT NULL DEFAULT '0',
  `display_id` int(10) unsigned NOT NULL DEFAULT '0',
  `native_display_id` int(10) unsigned NOT NULL DEFAULT '0',
  `mount_display_id` int(10) unsigned NOT NULL DEFAULT '0',
  `class` tinyint(3) unsigned NOT NULL DEFAULT '0',
  `gender` tinyint(3) unsigned NOT NULL DEFAULT '0',
  `faction` int(10) unsigned NOT NULL DEFAULT '0',
  `level` int(10) unsigned NOT NULL DEFAULT '0',
  `npc_flags` int(10) unsigned NOT NULL DEFAULT '0',
  `unit_flags` int(10) unsigned NOT NULL DEFAULT '0',
  `unit_flags2` int(10) unsigned NOT NULL DEFAULT '0',
  `dynamic_flags` int(10) unsigned NOT NULL DEFAULT '0',
  `current_health` int(10) unsigned NOT NULL DEFAULT '0',
  `max_health` int(10) unsigned NOT NULL DEFAULT '0',
  `power_type` tinyint(3) unsigned NOT NULL DEFAULT '0',
  `current_power` int(10) unsigned NOT NULL DEFAULT '0',
  `max_power` int(10) unsigned NOT NULL DEFAULT '0',
  `aura_state` int(10) unsigned NOT NULL DEFAULT '0',
  `emote_state` int(10) unsigned NOT NULL DEFAULT '0',
  `stand_state` tinyint(3) unsigned NOT NULL DEFAULT '0' COMMENT 'from UNIT_FIELD_BYTES_1',
  `vis_flags` tinyint(3) unsigned NOT NULL DEFAULT '0' COMMENT 'from UNIT_FIELD_BYTES_1',
  `anim_tier` tinyint(3) unsigned NOT NULL DEFAULT '0' COMMENT 'from UNIT_FIELD_BYTES_1',
  `sheath_state` tinyint(3) unsigned NOT NULL DEFAULT '0' COMMENT 'from UNIT_FIELD_BYTES_2',
  `misc_flags` tinyint(3) unsigned NOT NULL DEFAULT '0' COMMENT 'from UNIT_FIELD_BYTES_2, only in vanilla',
  `debuff_limit` tinyint(3) unsigned NOT NULL DEFAULT '0' COMMENT 'from UNIT_FIELD_BYTES_2, only in tbc',
  `pvp_flags` tinyint(3) unsigned NOT NULL DEFAULT '0' COMMENT 'from UNIT_FIELD_BYTES_2, wotlk+',
  `shapeshift_form` tinyint(3) unsigned NOT NULL DEFAULT '0' COMMENT 'from UNIT_FIELD_BYTES_2',
  `speed_walk` float NOT NULL DEFAULT '0',
  `speed_run` float NOT NULL DEFAULT '0',
  `speed_run_back` float NOT NULL DEFAULT '0',
  `speed_swim` float NOT NULL DEFAULT '0',
  `speed_swim_back` float NOT NULL DEFAULT '0',
  `speed_fly` float NOT NULL DEFAULT '0',
  `speed_fly_back` float NOT NULL DEFAULT '0',
  `bounding_radius` float NOT NULL DEFAULT '0',
  `combat_reach` float NOT NULL DEFAULT '0',
  `mod_melee_haste` float NOT NULL DEFAULT '0',
  `main_hand_attack_time` int(10) unsigned NOT NULL DEFAULT '0',
  `off_hand_attack_time` int(10) unsigned NOT NULL DEFAULT '0',
  `main_hand_slot_item` int(10) unsigned NOT NULL DEFAULT '0',
  `off_hand_slot_item` int(10) unsigned NOT NULL DEFAULT '0',
  `ranged_slot_item` int(10) unsigned NOT NULL DEFAULT '0',
  `channel_spell_id` int(10) unsigned NOT NULL DEFAULT '0',
  `channel_visual_id` int(10) unsigned NOT NULL DEFAULT '0',
  `auras` text COLLATE latin1_general_ci,
  `sniff_id` smallint(5) unsigned NOT NULL DEFAULT '0' COMMENT 'points to sniff_file table',
  `sniff_build` mediumint(8) unsigned NOT NULL DEFAULT '0',
  PRIMARY KEY (`guid`)
) ENGINE=MyISAM DEFAULT CHARSET=latin1 COLLATE=latin1_general_ci ROW_FORMAT=DYNAMIC COMMENT='the initial state of all individual creature spawns seen in the sniff';

-- Data exporting was unselected.


-- Dumping structure for table sniffs_new_test.creature_armor
DROP TABLE IF EXISTS `creature_armor`;
CREATE TABLE IF NOT EXISTS `creature_armor` (
  `entry` int(10) unsigned NOT NULL DEFAULT '0',
  `level` int(10) unsigned NOT NULL DEFAULT '0',
  `hits_count` smallint(5) unsigned NOT NULL DEFAULT '0',
  `armor` int(10) unsigned NOT NULL DEFAULT '0',
  `damage_reduction` float NOT NULL DEFAULT '0',
  `sniff_build` mediumint(8) unsigned NOT NULL DEFAULT '0',
  KEY `entry` (`entry`)
) ENGINE=MyISAM DEFAULT CHARSET=latin1 COLLATE=latin1_general_ci COMMENT='estimated armor of creatures from damage taken';

-- Data exporting was unselected.


-- Dumping structure for table sniffs_new_test.creature_attack_log
DROP TABLE IF EXISTS `creature_attack_log`;
CREATE TABLE IF NOT EXISTS `creature_attack_log` (
  `unixtimems` bigint(20) unsigned NOT NULL DEFAULT '0',
  `guid` int(10) unsigned NOT NULL,
  `victim_guid` int(10) unsigned NOT NULL DEFAULT '0',
  `victim_id` int(10) unsigned NOT NULL DEFAULT '0',
  `victim_type` varchar(16) COLLATE latin1_general_ci NOT NULL DEFAULT '',
  `hit_info` int(10) unsigned NOT NULL DEFAULT '0',
  `damage` int(10) NOT NULL DEFAULT '0',
  `original_damage` int(10) NOT NULL DEFAULT '0',
  `overkill_damage` int(10) NOT NULL DEFAULT '0',
  `sub_damage_count` tinyint(3) unsigned NOT NULL DEFAULT '0',
  `total_school_mask` int(10) unsigned NOT NULL DEFAULT '0',
  `total_absorbed_damage` int(11) NOT NULL DEFAULT '0',
  `total_resisted_damage` int(11) NOT NULL DEFAULT '0',
  `blocked_damage` int(10) NOT NULL DEFAULT '0',
  `victim_state` int(10) unsigned NOT NULL DEFAULT '0',
  `attacker_state` int(10) NOT NULL DEFAULT '0',
  `spell_id` int(10) unsigned NOT NULL DEFAULT '0',
  PRIMARY KEY (`guid`,`unixtimems`)
) ENGINE=MyISAM DEFAULT CHARSET=latin1 COLLATE=latin1_general_ci ROW_FORMAT=COMPACT COMMENT='from SMSG_ATTACKER_STATE_UPDATE';

-- Data exporting was unselected.


-- Dumping structure for table sniffs_new_test.creature_attack_start
DROP TABLE IF EXISTS `creature_attack_start`;
CREATE TABLE IF NOT EXISTS `creature_attack_start` (
  `unixtimems` bigint(20) unsigned NOT NULL DEFAULT '0',
  `guid` int(10) unsigned NOT NULL,
  `victim_guid` int(10) unsigned NOT NULL DEFAULT '0',
  `victim_id` int(10) unsigned NOT NULL DEFAULT '0',
  `victim_type` varchar(16) COLLATE latin1_general_ci NOT NULL DEFAULT '',
  PRIMARY KEY (`guid`,`unixtimems`)
) ENGINE=MyISAM DEFAULT CHARSET=latin1 COLLATE=latin1_general_ci COMMENT='from SMSG_ATTACK_START';

-- Data exporting was unselected.


-- Dumping structure for table sniffs_new_test.creature_attack_stop
DROP TABLE IF EXISTS `creature_attack_stop`;
CREATE TABLE IF NOT EXISTS `creature_attack_stop` (
  `unixtimems` bigint(20) unsigned NOT NULL DEFAULT '0',
  `guid` int(10) unsigned NOT NULL,
  `victim_guid` int(10) unsigned NOT NULL DEFAULT '0',
  `victim_id` int(10) unsigned NOT NULL DEFAULT '0',
  `victim_type` varchar(16) COLLATE latin1_general_ci NOT NULL DEFAULT '',
  PRIMARY KEY (`guid`,`unixtimems`)
) ENGINE=MyISAM DEFAULT CHARSET=latin1 COLLATE=latin1_general_ci COMMENT='from SMSG_ATTACK_STOP';

-- Data exporting was unselected.


-- Dumping structure for table sniffs_new_test.creature_auras_update
DROP TABLE IF EXISTS `creature_auras_update`;
CREATE TABLE IF NOT EXISTS `creature_auras_update` (
  `unixtimems` bigint(20) unsigned NOT NULL COMMENT 'when the packet was received',
  `guid` int(10) unsigned NOT NULL,
  `update_id` int(10) unsigned NOT NULL COMMENT 'counting aura update packets for this guid',
  `is_full_update` tinyint(3) unsigned NOT NULL,
  `slot` smallint(6) NOT NULL,
  `spell_id` int(10) unsigned NOT NULL,
  `visual_id` int(10) unsigned NOT NULL,
  `aura_flags` int(10) unsigned NOT NULL,
  `active_flags` int(10) unsigned NOT NULL,
  `level` int(10) unsigned NOT NULL,
  `charges` int(10) unsigned NOT NULL,
  `duration` int(10) NOT NULL,
  `max_duration` int(10) NOT NULL,
  `caster_guid` int(10) unsigned NOT NULL,
  `caster_id` int(10) unsigned NOT NULL,
  `caster_type` varchar(16) COLLATE latin1_general_ci NOT NULL,
  PRIMARY KEY (`guid`,`update_id`,`slot`)
) ENGINE=MyISAM DEFAULT CHARSET=latin1 COLLATE=latin1_general_ci ROW_FORMAT=COMPACT COMMENT='aura updates from SMSG_AURA_UPDATE';

-- Data exporting was unselected.


-- Dumping structure for table sniffs_new_test.creature_create1_time
DROP TABLE IF EXISTS `creature_create1_time`;
CREATE TABLE IF NOT EXISTS `creature_create1_time` (
  `unixtimems` bigint(20) unsigned NOT NULL COMMENT 'when the packet was received',
  `guid` int(10) unsigned NOT NULL COMMENT 'creature spawn guid',
  `map` smallint(5) unsigned NOT NULL,
  `position_x` float NOT NULL,
  `position_y` float NOT NULL,
  `position_z` float NOT NULL,
  `orientation` float NOT NULL,
  `move_time` int(10) unsigned NOT NULL DEFAULT '0',
  `move_flags` int(10) unsigned NOT NULL DEFAULT '0',
  `move_flags2` int(10) unsigned NOT NULL DEFAULT '0',
  `swim_pitch` float NOT NULL DEFAULT '0',
  `fall_time` int(10) unsigned NOT NULL DEFAULT '0',
  `jump_horizontal_speed` float NOT NULL DEFAULT '0',
  `jump_vertical_speed` float NOT NULL DEFAULT '0',
  `jump_cos_angle` float NOT NULL DEFAULT '0',
  `jump_sin_angle` float NOT NULL DEFAULT '0',
  `spline_elevation` float NOT NULL DEFAULT '0',
  `vehicle_id` int(10) unsigned NOT NULL DEFAULT '0',
  `vehicle_orientation` float NOT NULL DEFAULT '0',
  `transport_guid` int(10) unsigned NOT NULL DEFAULT '0',
  `transport_id` int(10) unsigned NOT NULL DEFAULT '0',
  `transport_type` varchar(16) COLLATE latin1_general_ci NOT NULL DEFAULT '',
  `transport_x` float NOT NULL DEFAULT '0',
  `transport_y` float NOT NULL DEFAULT '0',
  `transport_z` float NOT NULL DEFAULT '0',
  `transport_o` float NOT NULL DEFAULT '0',
  `transport_time` int(10) unsigned NOT NULL DEFAULT '0',
  `transport_seat` tinyint(4) NOT NULL DEFAULT '0',
  PRIMARY KEY (`guid`,`unixtimems`)
) ENGINE=MyISAM DEFAULT CHARSET=latin1 COLLATE=latin1_general_ci ROW_FORMAT=COMPACT COMMENT='times when the object became visible to the client';

-- Data exporting was unselected.


-- Dumping structure for table sniffs_new_test.creature_create2_time
DROP TABLE IF EXISTS `creature_create2_time`;
CREATE TABLE IF NOT EXISTS `creature_create2_time` (
  `unixtimems` bigint(20) unsigned NOT NULL COMMENT 'when the packet was received',
  `guid` int(10) unsigned NOT NULL COMMENT 'creature spawn guid',
  `map` smallint(5) unsigned NOT NULL,
  `position_x` float NOT NULL,
  `position_y` float NOT NULL,
  `position_z` float NOT NULL,
  `orientation` float NOT NULL,
  `move_time` int(10) unsigned NOT NULL DEFAULT '0',
  `move_flags` int(10) unsigned NOT NULL DEFAULT '0',
  `move_flags2` int(10) unsigned NOT NULL DEFAULT '0',
  `swim_pitch` float NOT NULL DEFAULT '0',
  `fall_time` int(10) unsigned NOT NULL DEFAULT '0',
  `jump_horizontal_speed` float NOT NULL DEFAULT '0',
  `jump_vertical_speed` float NOT NULL DEFAULT '0',
  `jump_cos_angle` float NOT NULL DEFAULT '0',
  `jump_sin_angle` float NOT NULL DEFAULT '0',
  `spline_elevation` float NOT NULL DEFAULT '0',
  `vehicle_id` int(10) unsigned NOT NULL DEFAULT '0',
  `vehicle_orientation` float NOT NULL DEFAULT '0',
  `transport_guid` int(10) unsigned NOT NULL DEFAULT '0',
  `transport_id` int(10) unsigned NOT NULL DEFAULT '0',
  `transport_type` varchar(16) COLLATE latin1_general_ci NOT NULL DEFAULT '',
  `transport_x` float NOT NULL DEFAULT '0',
  `transport_y` float NOT NULL DEFAULT '0',
  `transport_z` float NOT NULL DEFAULT '0',
  `transport_o` float NOT NULL DEFAULT '0',
  `transport_time` int(10) unsigned NOT NULL DEFAULT '0',
  `transport_seat` tinyint(4) NOT NULL DEFAULT '0',
  PRIMARY KEY (`guid`,`unixtimems`)
) ENGINE=MyISAM DEFAULT CHARSET=latin1 COLLATE=latin1_general_ci ROW_FORMAT=COMPACT COMMENT='the time at which the object spawned';

-- Data exporting was unselected.


-- Dumping structure for table sniffs_new_test.creature_destroy_time
DROP TABLE IF EXISTS `creature_destroy_time`;
CREATE TABLE IF NOT EXISTS `creature_destroy_time` (
  `unixtimems` bigint(20) unsigned NOT NULL COMMENT 'when the packet was received',
  `guid` int(10) unsigned NOT NULL COMMENT 'creature spawn guid',
  PRIMARY KEY (`guid`,`unixtimems`)
) ENGINE=MyISAM DEFAULT CHARSET=latin1 COLLATE=latin1_general_ci COMMENT='times when the object was destroyed from the client''s prespective due to despawning, becoming invisible, or going out of range';

-- Data exporting was unselected.


-- Dumping structure for table sniffs_new_test.creature_display_info_addon
DROP TABLE IF EXISTS `creature_display_info_addon`;
CREATE TABLE IF NOT EXISTS `creature_display_info_addon` (
  `display_id` mediumint(8) unsigned NOT NULL DEFAULT '0',
  `bounding_radius` float NOT NULL DEFAULT '0',
  `combat_reach` float NOT NULL DEFAULT '0',
  `speed_walk` float NOT NULL DEFAULT '0',
  `speed_run` float NOT NULL DEFAULT '0',
  `gender` tinyint(3) unsigned NOT NULL DEFAULT '2',
  `sniff_build` mediumint(8) unsigned NOT NULL DEFAULT '0',
  PRIMARY KEY (`display_id`)
) ENGINE=MyISAM DEFAULT CHARSET=latin1 COLLATE=latin1_general_ci COMMENT='data for display ids used by creatures';

-- Data exporting was unselected.


-- Dumping structure for table sniffs_new_test.creature_emote
DROP TABLE IF EXISTS `creature_emote`;
CREATE TABLE IF NOT EXISTS `creature_emote` (
  `unixtimems` bigint(20) unsigned NOT NULL COMMENT 'when the packet was received',
  `guid` int(10) unsigned NOT NULL COMMENT 'creature spawn guid',
  `emote_id` int(10) unsigned NOT NULL COMMENT 'references Emotes.dbc',
  `emote_name` varchar(50) COLLATE latin1_general_ci DEFAULT NULL,
  PRIMARY KEY (`guid`,`unixtimems`,`emote_id`)
) ENGINE=MyISAM DEFAULT CHARSET=latin1 COLLATE=latin1_general_ci COMMENT='from SMSG_EMOTE';

-- Data exporting was unselected.


-- Dumping structure for table sniffs_new_test.creature_equipment_values_update
DROP TABLE IF EXISTS `creature_equipment_values_update`;
CREATE TABLE IF NOT EXISTS `creature_equipment_values_update` (
  `unixtimems` bigint(20) unsigned NOT NULL COMMENT 'when the packet was received',
  `guid` int(10) unsigned NOT NULL COMMENT 'creature spawn guid',
  `slot` tinyint(3) unsigned NOT NULL,
  `item_id` int(10) unsigned NOT NULL,
  PRIMARY KEY (`guid`,`unixtimems`,`slot`)
) ENGINE=MyISAM DEFAULT CHARSET=latin1 COLLATE=latin1_general_ci ROW_FORMAT=COMPACT COMMENT='changes to visual item slots';

-- Data exporting was unselected.


-- Dumping structure for table sniffs_new_test.creature_flight_spline_sync
DROP TABLE IF EXISTS `creature_flight_spline_sync`;
CREATE TABLE IF NOT EXISTS `creature_flight_spline_sync` (
  `unixtimems` bigint(20) unsigned NOT NULL,
  `guid` int(10) unsigned NOT NULL,
  `duration_percent` float NOT NULL,
  PRIMARY KEY (`unixtimems`,`guid`)
) ENGINE=MyISAM DEFAULT CHARSET=latin1 COLLATE=latin1_general_ci;

-- Data exporting was unselected.


-- Dumping structure for table sniffs_new_test.creature_guid_values
DROP TABLE IF EXISTS `creature_guid_values`;
CREATE TABLE IF NOT EXISTS `creature_guid_values` (
  `guid` int(10) unsigned NOT NULL,
  `charm_guid` int(10) unsigned NOT NULL DEFAULT '0',
  `charm_id` int(10) unsigned NOT NULL DEFAULT '0',
  `charm_type` varchar(16) COLLATE latin1_general_ci NOT NULL DEFAULT '',
  `summon_guid` int(10) unsigned NOT NULL DEFAULT '0',
  `summon_id` int(10) unsigned NOT NULL DEFAULT '0',
  `summon_type` varchar(16) COLLATE latin1_general_ci NOT NULL DEFAULT '',
  `charmer_guid` int(10) unsigned NOT NULL DEFAULT '0',
  `charmer_id` int(10) unsigned NOT NULL DEFAULT '0',
  `charmer_type` varchar(16) COLLATE latin1_general_ci NOT NULL DEFAULT '',
  `summoner_guid` int(10) unsigned NOT NULL DEFAULT '0',
  `summoner_id` int(10) unsigned NOT NULL DEFAULT '0',
  `summoner_type` varchar(16) COLLATE latin1_general_ci NOT NULL DEFAULT '',
  `creator_guid` int(10) unsigned NOT NULL DEFAULT '0',
  `creator_id` int(10) unsigned NOT NULL DEFAULT '0',
  `creator_type` varchar(16) COLLATE latin1_general_ci NOT NULL DEFAULT '',
  `demon_creator_guid` int(10) unsigned NOT NULL DEFAULT '0',
  `demon_creator_id` int(10) unsigned NOT NULL DEFAULT '0',
  `demon_creator_type` varchar(16) COLLATE latin1_general_ci NOT NULL DEFAULT '',
  `target_guid` int(10) unsigned NOT NULL DEFAULT '0',
  `target_id` int(10) unsigned NOT NULL DEFAULT '0',
  `target_type` varchar(16) COLLATE latin1_general_ci NOT NULL DEFAULT '',
  PRIMARY KEY (`guid`)
) ENGINE=MyISAM DEFAULT CHARSET=latin1 COLLATE=latin1_general_ci ROW_FORMAT=COMPACT COMMENT='initial value in guid type update fields';

-- Data exporting was unselected.


-- Dumping structure for table sniffs_new_test.creature_guid_values_update
DROP TABLE IF EXISTS `creature_guid_values_update`;
CREATE TABLE IF NOT EXISTS `creature_guid_values_update` (
  `unixtimems` bigint(20) unsigned NOT NULL DEFAULT '0',
  `guid` int(10) unsigned NOT NULL,
  `field_name` varchar(16) COLLATE latin1_general_ci NOT NULL DEFAULT '',
  `object_guid` int(10) unsigned NOT NULL DEFAULT '0',
  `object_id` int(10) unsigned NOT NULL DEFAULT '0',
  `object_type` varchar(16) COLLATE latin1_general_ci NOT NULL DEFAULT '',
  PRIMARY KEY (`guid`,`unixtimems`,`field_name`,`object_guid`,`object_id`,`object_type`)
) ENGINE=MyISAM DEFAULT CHARSET=latin1 COLLATE=latin1_general_ci ROW_FORMAT=COMPACT COMMENT='changes to guid type update fields';

-- Data exporting was unselected.


-- Dumping structure for table sniffs_new_test.creature_kill_reputation
DROP TABLE IF EXISTS `creature_kill_reputation`;
CREATE TABLE IF NOT EXISTS `creature_kill_reputation` (
  `entry` int(10) unsigned NOT NULL,
  `reputation_list_id` int(10) unsigned NOT NULL,
  `old_standing` int(11) NOT NULL,
  `new_standing` int(11) NOT NULL,
  `player_level` tinyint(3) unsigned NOT NULL,
  `player_race` tinyint(3) unsigned NOT NULL,
  `sniff_id` smallint(5) unsigned NOT NULL DEFAULT '0' COMMENT 'points to sniff_file table',
  `sniff_build` mediumint(8) unsigned NOT NULL DEFAULT '0',
  KEY `entry` (`entry`)
) ENGINE=MyISAM DEFAULT CHARSET=latin1 COLLATE=latin1_general_ci COMMENT='faction standing changes after a creature died\r\ncan be wrong if multiple creatures died at the same time';

-- Data exporting was unselected.


-- Dumping structure for table sniffs_new_test.creature_loot
DROP TABLE IF EXISTS `creature_loot`;
CREATE TABLE IF NOT EXISTS `creature_loot` (
  `entry` int(10) unsigned NOT NULL COMMENT 'creature template id',
  `loot_id` int(10) unsigned NOT NULL COMMENT 'counter',
  `money` int(10) unsigned NOT NULL DEFAULT '0' COMMENT 'copper',
  `items_count` int(10) unsigned NOT NULL DEFAULT '0' COMMENT 'number of items dropped',
  `sniff_id` smallint(5) unsigned NOT NULL DEFAULT '0' COMMENT 'points to sniff_file table',
  `sniff_build` mediumint(8) unsigned NOT NULL DEFAULT '0',
  PRIMARY KEY (`entry`,`loot_id`)
) ENGINE=MyISAM DEFAULT CHARSET=latin1 COLLATE=latin1_general_ci COMMENT='each row represents a separate loot instance\r\nmight not contain all the items or gold that dropped if somebody else looted them first';

-- Data exporting was unselected.


-- Dumping structure for table sniffs_new_test.creature_loot_item
DROP TABLE IF EXISTS `creature_loot_item`;
CREATE TABLE IF NOT EXISTS `creature_loot_item` (
  `loot_id` int(10) unsigned NOT NULL COMMENT 'references creature_loot',
  `item_id` int(10) unsigned NOT NULL COMMENT 'item template id',
  `count` int(10) unsigned NOT NULL DEFAULT '1' COMMENT 'stack size'
) ENGINE=MyISAM DEFAULT CHARSET=latin1 COLLATE=latin1_general_ci COMMENT='individual item that is part of a loot instance';

-- Data exporting was unselected.


-- Dumping structure for table sniffs_new_test.creature_melee_damage
DROP TABLE IF EXISTS `creature_melee_damage`;
CREATE TABLE IF NOT EXISTS `creature_melee_damage` (
  `entry` int(10) unsigned NOT NULL DEFAULT '0',
  `level` int(10) unsigned NOT NULL DEFAULT '0',
  `is_dirty` tinyint(3) unsigned NOT NULL DEFAULT '0' COMMENT 'mob had auras that affect damage or there were no normal hits',
  `hits_count` smallint(5) unsigned NOT NULL DEFAULT '0',
  `damage_min` int(10) NOT NULL DEFAULT '0',
  `damage_average` int(10) NOT NULL DEFAULT '0',
  `damage_max` int(10) NOT NULL DEFAULT '0',
  `total_school_mask` int(10) unsigned NOT NULL DEFAULT '0',
  `sniff_build` mediumint(8) unsigned NOT NULL DEFAULT '0',
  KEY `entry` (`entry`)
) ENGINE=MyISAM DEFAULT CHARSET=latin1 COLLATE=latin1_general_ci COMMENT='statistics for melee damage per creature id';

-- Data exporting was unselected.


-- Dumping structure for table sniffs_new_test.creature_movement_client
DROP TABLE IF EXISTS `creature_movement_client`;
CREATE TABLE IF NOT EXISTS `creature_movement_client` (
  `unixtimems` bigint(20) unsigned NOT NULL,
  `guid` int(10) unsigned NOT NULL,
  `packet_id` int(10) unsigned NOT NULL,
  `opcode` varchar(64) COLLATE latin1_general_ci NOT NULL DEFAULT '',
  `move_time` int(10) unsigned NOT NULL DEFAULT '0',
  `move_flags` int(10) unsigned NOT NULL DEFAULT '0',
  `move_flags2` int(10) unsigned NOT NULL DEFAULT '0',
  `map` smallint(5) unsigned NOT NULL DEFAULT '0',
  `position_x` float NOT NULL DEFAULT '0',
  `position_y` float NOT NULL DEFAULT '0',
  `position_z` float NOT NULL DEFAULT '0',
  `orientation` float NOT NULL DEFAULT '0',
  `swim_pitch` float NOT NULL DEFAULT '0',
  `fall_time` int(11) unsigned NOT NULL DEFAULT '0',
  `jump_horizontal_speed` float NOT NULL DEFAULT '0',
  `jump_vertical_speed` float NOT NULL DEFAULT '0',
  `jump_cos_angle` float NOT NULL DEFAULT '0',
  `jump_sin_angle` float NOT NULL DEFAULT '0',
  `spline_elevation` float NOT NULL DEFAULT '0',
  `transport_guid` int(10) unsigned NOT NULL DEFAULT '0',
  `transport_id` int(10) unsigned NOT NULL DEFAULT '0',
  `transport_type` varchar(16) COLLATE latin1_general_ci NOT NULL DEFAULT '',
  `transport_x` float NOT NULL DEFAULT '0',
  `transport_y` float NOT NULL DEFAULT '0',
  `transport_z` float NOT NULL DEFAULT '0',
  `transport_o` float NOT NULL DEFAULT '0',
  `transport_time` int(10) unsigned NOT NULL DEFAULT '0',
  `transport_seat` tinyint(4) NOT NULL DEFAULT '0',
  PRIMARY KEY (`packet_id`)
) ENGINE=MyISAM DEFAULT CHARSET=latin1 COLLATE=latin1_general_ci ROW_FORMAT=COMPACT COMMENT='client side movement';

-- Data exporting was unselected.


-- Dumping structure for table sniffs_new_test.creature_movement_server
DROP TABLE IF EXISTS `creature_movement_server`;
CREATE TABLE IF NOT EXISTS `creature_movement_server` (
  `unixtimems` bigint(20) unsigned NOT NULL COMMENT 'when the packet was received',
  `guid` int(10) unsigned NOT NULL COMMENT 'creature spawn guid',
  `point` smallint(5) unsigned NOT NULL COMMENT 'counter of movements per guid',
  `move_time` int(10) unsigned NOT NULL COMMENT 'how long it will take to move between these points',
  `spline_flags` int(10) unsigned NOT NULL,
  `spline_count` smallint(5) unsigned NOT NULL COMMENT 'number of splines belonging to this point',
  `start_position_x` float NOT NULL COMMENT 'starting position',
  `start_position_y` float NOT NULL,
  `start_position_z` float NOT NULL,
  `end_position_x` float NOT NULL COMMENT 'final position',
  `end_position_y` float NOT NULL,
  `end_position_z` float NOT NULL,
  `orientation` float NOT NULL COMMENT 'final orientation',
  `transport_guid` int(10) unsigned NOT NULL DEFAULT '0',
  `transport_id` int(10) unsigned NOT NULL DEFAULT '0',
  `transport_type` varchar(16) COLLATE latin1_general_ci NOT NULL DEFAULT '',
  `transport_seat` tinyint(4) NOT NULL DEFAULT '0',
  `anim_tier` tinyint(4) unsigned NOT NULL DEFAULT '0',
  `vertical_speed` float NOT NULL DEFAULT '0',
  `effect_start_time` int(10) unsigned NOT NULL DEFAULT '0',
  PRIMARY KEY (`guid`,`point`)
) ENGINE=MyISAM DEFAULT CHARSET=latin1 COLLATE=latin1_general_ci COMMENT='out of combat movement points from SMSG_ON_MONSTER_MOVE';

-- Data exporting was unselected.


-- Dumping structure for table sniffs_new_test.creature_movement_server_combat
DROP TABLE IF EXISTS `creature_movement_server_combat`;
CREATE TABLE IF NOT EXISTS `creature_movement_server_combat` (
  `unixtimems` bigint(20) unsigned NOT NULL COMMENT 'when the packet was received',
  `guid` int(10) unsigned NOT NULL COMMENT 'creature spawn guid',
  `point` smallint(5) unsigned NOT NULL COMMENT 'counter of movements per guid',
  `move_time` int(10) unsigned NOT NULL COMMENT 'how long it will take to move between these points',
  `spline_flags` int(10) unsigned NOT NULL,
  `spline_count` smallint(5) unsigned NOT NULL COMMENT 'number of splines belonging to this point',
  `start_position_x` float NOT NULL COMMENT 'starting position',
  `start_position_y` float NOT NULL,
  `start_position_z` float NOT NULL,
  `end_position_x` float NOT NULL COMMENT 'final position',
  `end_position_y` float NOT NULL,
  `end_position_z` float NOT NULL,
  `orientation` float NOT NULL COMMENT 'final orientation',
  `transport_guid` int(10) unsigned NOT NULL DEFAULT '0',
  `transport_id` int(10) unsigned NOT NULL DEFAULT '0',
  `transport_type` varchar(16) COLLATE latin1_general_ci NOT NULL DEFAULT '',
  `transport_seat` tinyint(4) NOT NULL DEFAULT '0',
  `anim_tier` tinyint(4) unsigned NOT NULL DEFAULT '0',
  `vertical_speed` float NOT NULL DEFAULT '0',
  `effect_start_time` int(10) unsigned NOT NULL DEFAULT '0',
  PRIMARY KEY (`guid`,`point`)
) ENGINE=MyISAM DEFAULT CHARSET=latin1 COLLATE=latin1_general_ci COMMENT='in combat movement points from SMSG_ON_MONSTER_MOVE';

-- Data exporting was unselected.


-- Dumping structure for table sniffs_new_test.creature_movement_server_combat_spline
DROP TABLE IF EXISTS `creature_movement_server_combat_spline`;
CREATE TABLE IF NOT EXISTS `creature_movement_server_combat_spline` (
  `guid` int(10) unsigned NOT NULL COMMENT 'creature spawn guid',
  `parent_point` smallint(5) unsigned NOT NULL COMMENT 'point from creature_movement_server_combat to which the spline data belongs',
  `spline_point` smallint(5) unsigned NOT NULL COMMENT 'order of points within the spline',
  `position_x` float NOT NULL,
  `position_y` float NOT NULL,
  `position_z` float NOT NULL,
  PRIMARY KEY (`guid`,`parent_point`,`spline_point`)
) ENGINE=MyISAM DEFAULT CHARSET=latin1 COLLATE=latin1_general_ci COMMENT='individual spline points for combat  movement';

-- Data exporting was unselected.


-- Dumping structure for table sniffs_new_test.creature_movement_server_spline
DROP TABLE IF EXISTS `creature_movement_server_spline`;
CREATE TABLE IF NOT EXISTS `creature_movement_server_spline` (
  `guid` int(10) unsigned NOT NULL COMMENT 'creature spawn guid',
  `parent_point` smallint(5) unsigned NOT NULL COMMENT 'point from creature_movement_server to which the spline data belongs',
  `spline_point` smallint(5) unsigned NOT NULL COMMENT 'order of points within the spline',
  `position_x` float NOT NULL,
  `position_y` float NOT NULL,
  `position_z` float NOT NULL,
  PRIMARY KEY (`guid`,`parent_point`,`spline_point`)
) ENGINE=MyISAM DEFAULT CHARSET=latin1 COLLATE=latin1_general_ci COMMENT='individual spline points for out of combat movement';

-- Data exporting was unselected.


-- Dumping structure for table sniffs_new_test.creature_pet_actions
DROP TABLE IF EXISTS `creature_pet_actions`;
CREATE TABLE IF NOT EXISTS `creature_pet_actions` (
  `entry` int(10) unsigned NOT NULL DEFAULT '0',
  `slot1` int(10) unsigned NOT NULL DEFAULT '0',
  `slot2` int(10) unsigned NOT NULL DEFAULT '0',
  `slot3` int(10) unsigned NOT NULL DEFAULT '0',
  `slot4` int(10) unsigned NOT NULL DEFAULT '0',
  `slot5` int(10) unsigned NOT NULL DEFAULT '0',
  `slot6` int(10) unsigned NOT NULL DEFAULT '0',
  `slot7` int(10) unsigned NOT NULL DEFAULT '0',
  `slot8` int(10) unsigned NOT NULL DEFAULT '0',
  `slot9` int(10) unsigned NOT NULL DEFAULT '0',
  `slot10` int(10) unsigned NOT NULL DEFAULT '0',
  `sniff_id` smallint(5) unsigned NOT NULL DEFAULT '0' COMMENT 'points to sniff_file table',
  `sniff_build` mediumint(8) unsigned NOT NULL DEFAULT '0',
  KEY `entry` (`entry`)
) ENGINE=MyISAM DEFAULT CHARSET=latin1 COLLATE=latin1_general_ci COMMENT='from SMSG_PET_SPELLS_MESSAGE';

-- Data exporting was unselected.


-- Dumping structure for table sniffs_new_test.creature_pet_cooldown
DROP TABLE IF EXISTS `creature_pet_cooldown`;
CREATE TABLE IF NOT EXISTS `creature_pet_cooldown` (
  `entry` int(10) unsigned NOT NULL DEFAULT '0',
  `flags` tinyint(3) unsigned NOT NULL DEFAULT '0',
  `index` tinyint(3) unsigned NOT NULL DEFAULT '0',
  `spell_id` int(10) unsigned NOT NULL DEFAULT '0',
  `cooldown` int(10) unsigned NOT NULL DEFAULT '0',
  `mod_rate` float unsigned NOT NULL DEFAULT '1',
  `sniff_id` smallint(5) unsigned NOT NULL DEFAULT '0' COMMENT 'points to sniff_file table',
  `sniff_build` mediumint(8) unsigned NOT NULL DEFAULT '0',
  KEY `entry` (`entry`)
) ENGINE=MyISAM DEFAULT CHARSET=latin1 COLLATE=latin1_general_ci COMMENT='from SMSG_SPELL_COOLDOWN';

-- Data exporting was unselected.


-- Dumping structure for table sniffs_new_test.creature_pet_name
DROP TABLE IF EXISTS `creature_pet_name`;
CREATE TABLE IF NOT EXISTS `creature_pet_name` (
  `guid` int(10) unsigned NOT NULL,
  `name` varchar(16) COLLATE utf8_unicode_ci NOT NULL DEFAULT '',
  PRIMARY KEY (`guid`)
) ENGINE=MyISAM DEFAULT CHARSET=utf8 COLLATE=utf8_unicode_ci COMMENT='from SMSG_QUERY_PET_NAME_RESPONSE';

-- Data exporting was unselected.


-- Dumping structure for table sniffs_new_test.creature_pet_remaining_cooldown
DROP TABLE IF EXISTS `creature_pet_remaining_cooldown`;
CREATE TABLE IF NOT EXISTS `creature_pet_remaining_cooldown` (
  `entry` int(10) unsigned NOT NULL DEFAULT '0',
  `spell_id` int(10) unsigned NOT NULL DEFAULT '0',
  `cooldown` int(10) unsigned NOT NULL DEFAULT '0',
  `category` smallint(5) unsigned NOT NULL DEFAULT '0',
  `category_cooldown` int(10) NOT NULL DEFAULT '0',
  `mod_rate` float NOT NULL DEFAULT '1',
  `time_since_cast` int(10) unsigned NOT NULL DEFAULT '0' COMMENT 'milliseconds since last SMSG_SPELL_GO for this spell',
  `sniff_id` smallint(5) unsigned NOT NULL DEFAULT '0' COMMENT 'points to sniff_file table',
  `sniff_build` mediumint(8) unsigned NOT NULL DEFAULT '0',
  KEY `entry` (`entry`)
) ENGINE=MyISAM DEFAULT CHARSET=latin1 COLLATE=latin1_general_ci COMMENT='cooldowns that were already present when the creature became charmed\r\nfrom SMSG_PET_SPELLS_MESSAGE.';

-- Data exporting was unselected.


-- Dumping structure for table sniffs_new_test.creature_power_values
DROP TABLE IF EXISTS `creature_power_values`;
CREATE TABLE IF NOT EXISTS `creature_power_values` (
  `guid` int(10) unsigned NOT NULL,
  `power_type` tinyint(3) unsigned NOT NULL,
  `current_power` int(10) unsigned NOT NULL DEFAULT '0',
  `max_power` int(10) unsigned NOT NULL DEFAULT '0',
  PRIMARY KEY (`guid`,`power_type`)
) ENGINE=MyISAM DEFAULT CHARSET=latin1 COLLATE=latin1_general_ci COMMENT='initial value of power update fields';

-- Data exporting was unselected.


-- Dumping structure for table sniffs_new_test.creature_power_values_update
DROP TABLE IF EXISTS `creature_power_values_update`;
CREATE TABLE IF NOT EXISTS `creature_power_values_update` (
  `unixtimems` bigint(20) unsigned NOT NULL,
  `guid` int(10) unsigned NOT NULL,
  `power_type` tinyint(3) unsigned NOT NULL,
  `current_power` int(10) unsigned DEFAULT NULL,
  `max_power` int(10) unsigned DEFAULT NULL
) ENGINE=MyISAM DEFAULT CHARSET=latin1 COLLATE=latin1_general_ci COMMENT='changes to power update fields';

-- Data exporting was unselected.


-- Dumping structure for table sniffs_new_test.creature_quest_item
DROP TABLE IF EXISTS `creature_quest_item`;
CREATE TABLE IF NOT EXISTS `creature_quest_item` (
  `entry` int(10) unsigned NOT NULL,
  `idx` tinyint(3) unsigned NOT NULL,
  `item_id` int(10) unsigned NOT NULL DEFAULT '0',
  `sniff_build` mediumint(8) unsigned NOT NULL DEFAULT '0',
  PRIMARY KEY (`entry`,`idx`)
) ENGINE=MyISAM DEFAULT CHARSET=latin1 COLLATE=latin1_general_ci COMMENT='quest items that drop from a given creature id\r\nfrom SMSG_QUERY_CREATURE_RESPONSE';

-- Data exporting was unselected.


-- Dumping structure for table sniffs_new_test.creature_respawn_time
DROP TABLE IF EXISTS `creature_respawn_time`;
CREATE TABLE IF NOT EXISTS `creature_respawn_time` (
  `old_guid` int(10) unsigned NOT NULL,
  `new_guid` int(10) unsigned NOT NULL,
  `respawn_time` int(10) unsigned NOT NULL COMMENT 'time in seconds',
  PRIMARY KEY (`old_guid`,`new_guid`)
) ENGINE=MyISAM DEFAULT CHARSET=latin1 COLLATE=latin1_general_ci COMMENT='stores the time in seconds between the death of one creature, and the spawn of another on the same position\r\nrespawn time is reduced dynamically if there are too many players in the same area, so beware of abnormally low values';

-- Data exporting was unselected.


-- Dumping structure for table sniffs_new_test.creature_speed_update
DROP TABLE IF EXISTS `creature_speed_update`;
CREATE TABLE IF NOT EXISTS `creature_speed_update` (
  `unixtimems` bigint(20) unsigned NOT NULL COMMENT 'when the packet was received',
  `guid` int(10) unsigned NOT NULL,
  `speed_type` tinyint(3) unsigned NOT NULL,
  `speed_rate` float unsigned NOT NULL
) ENGINE=MyISAM DEFAULT CHARSET=latin1 COLLATE=latin1_general_ci ROW_FORMAT=COMPACT COMMENT='changes to movement speed';

-- Data exporting was unselected.


-- Dumping structure for table sniffs_new_test.creature_spell_immunity
DROP TABLE IF EXISTS `creature_spell_immunity`;
CREATE TABLE IF NOT EXISTS `creature_spell_immunity` (
  `entry` int(10) unsigned NOT NULL,
  `spell_id` int(10) unsigned NOT NULL,
  `sniff_id_list` text COLLATE latin1_general_ci NOT NULL,
  PRIMARY KEY (`entry`,`spell_id`)
) ENGINE=MyISAM DEFAULT CHARSET=latin1 COLLATE=latin1_general_ci COMMENT='spells that creatures were immune to\r\nfrom SMSG_SPELL_GO';

-- Data exporting was unselected.


-- Dumping structure for table sniffs_new_test.creature_spell_timers
DROP TABLE IF EXISTS `creature_spell_timers`;
CREATE TABLE IF NOT EXISTS `creature_spell_timers` (
  `entry` int(10) unsigned NOT NULL DEFAULT '0',
  `spell_id` int(10) unsigned NOT NULL DEFAULT '0',
  `initial_casts_count` smallint(5) unsigned NOT NULL DEFAULT '0',
  `initial_delay_min` int(10) unsigned NOT NULL DEFAULT '0',
  `initial_delay_average` int(10) unsigned NOT NULL DEFAULT '0',
  `initial_delay_max` int(10) unsigned NOT NULL DEFAULT '0',
  `repeat_casts_count` smallint(5) unsigned NOT NULL DEFAULT '0',
  `repeat_delay_min` int(10) unsigned NOT NULL DEFAULT '0',
  `repeat_delay_average` int(10) unsigned NOT NULL DEFAULT '0',
  `repeat_delay_max` int(10) unsigned NOT NULL DEFAULT '0',
  `sniff_build` mediumint(8) unsigned NOT NULL DEFAULT '0',
  KEY `entry` (`entry`)
) ENGINE=MyISAM DEFAULT CHARSET=latin1 COLLATE=latin1_general_ci COMMENT='calculated time between casts for creatures';

-- Data exporting was unselected.


-- Dumping structure for table sniffs_new_test.creature_stats
DROP TABLE IF EXISTS `creature_stats`;
CREATE TABLE IF NOT EXISTS `creature_stats` (
  `entry` int(10) unsigned NOT NULL COMMENT 'creature template id',
  `level` int(10) unsigned NOT NULL,
  `is_pet` tinyint(3) unsigned NOT NULL,
  `is_dirty` tinyint(3) unsigned NOT NULL COMMENT 'mob had auras that affect stats',
  `dmg_min` float DEFAULT NULL,
  `dmg_max` float DEFAULT NULL,
  `offhand_dmg_min` float DEFAULT NULL,
  `offhand_dmg_max` float DEFAULT NULL,
  `ranged_dmg_min` float DEFAULT NULL,
  `ranged_dmg_max` float DEFAULT NULL,
  `attack_power` int(10) DEFAULT NULL,
  `positive_attack_power` int(10) DEFAULT NULL,
  `negative_attack_power` int(10) DEFAULT NULL,
  `attack_power_multiplier` float DEFAULT NULL,
  `ranged_attack_power` int(10) DEFAULT NULL,
  `positive_ranged_attack_power` int(10) DEFAULT NULL,
  `negative_ranged_attack_power` int(10) DEFAULT NULL,
  `ranged_attack_power_multiplier` float DEFAULT NULL,
  `base_health` int(10) unsigned DEFAULT NULL,
  `base_mana` int(10) unsigned DEFAULT NULL,
  `strength` int(10) DEFAULT NULL,
  `agility` int(10) DEFAULT NULL,
  `stamina` int(10) DEFAULT NULL,
  `intellect` int(10) DEFAULT NULL,
  `spirit` int(10) DEFAULT NULL,
  `positive_strength` int(10) DEFAULT NULL,
  `positive_agility` int(10) DEFAULT NULL,
  `positive_stamina` int(10) DEFAULT NULL,
  `positive_intellect` int(10) DEFAULT NULL,
  `positive_spirit` int(10) DEFAULT NULL,
  `negative_strength` int(10) DEFAULT NULL,
  `negative_agility` int(10) DEFAULT NULL,
  `negative_stamina` int(10) DEFAULT NULL,
  `negative_intellect` int(10) DEFAULT NULL,
  `negative_spirit` int(10) DEFAULT NULL,
  `armor` int(11) DEFAULT NULL,
  `holy_res` int(11) DEFAULT NULL,
  `fire_res` int(11) DEFAULT NULL,
  `nature_res` int(11) DEFAULT NULL,
  `frost_res` int(11) DEFAULT NULL,
  `shadow_res` int(11) DEFAULT NULL,
  `arcane_res` int(11) DEFAULT NULL,
  `positive_armor` int(11) DEFAULT NULL,
  `positive_holy_res` int(11) DEFAULT NULL,
  `positive_fire_res` int(11) DEFAULT NULL,
  `positive_nature_res` int(11) DEFAULT NULL,
  `positive_frost_res` int(11) DEFAULT NULL,
  `positive_shadow_res` int(11) DEFAULT NULL,
  `positive_arcane_res` int(11) DEFAULT NULL,
  `negative_armor` int(11) DEFAULT NULL,
  `negative_holy_res` int(11) DEFAULT NULL,
  `negative_fire_res` int(11) DEFAULT NULL,
  `negative_nature_res` int(11) DEFAULT NULL,
  `negative_frost_res` int(11) DEFAULT NULL,
  `negative_shadow_res` int(11) DEFAULT NULL,
  `negative_arcane_res` int(11) DEFAULT NULL,
  `sniff_id` smallint(5) unsigned NOT NULL DEFAULT '0' COMMENT 'points to sniff_file table',
  `sniff_build` mediumint(8) unsigned NOT NULL DEFAULT '0',
  KEY `entry` (`entry`)
) ENGINE=MyISAM DEFAULT CHARSET=latin1 COLLATE=latin1_general_ci COMMENT='stats data from SMSG_UPDATE_OBJECT\r\nserver only sends it to the creature''s charmer, or player who casts beast lore on it';

-- Data exporting was unselected.


-- Dumping structure for table sniffs_new_test.creature_template
DROP TABLE IF EXISTS `creature_template`;
CREATE TABLE IF NOT EXISTS `creature_template` (
  `entry` mediumint(8) unsigned NOT NULL DEFAULT '0',
  `gossip_menu_id` mediumint(8) unsigned NOT NULL DEFAULT '0' COMMENT 'will only be set if the client talked to this npc',
  `level_min` tinyint(3) unsigned NOT NULL DEFAULT '1',
  `level_max` tinyint(3) unsigned NOT NULL DEFAULT '1',
  `faction` smallint(5) unsigned NOT NULL DEFAULT '0',
  `npc_flags` int(10) unsigned NOT NULL DEFAULT '0',
  `speed_walk` float NOT NULL DEFAULT '1' COMMENT 'Result of 2.5/2.5, most common value',
  `speed_run` float NOT NULL DEFAULT '1.14286' COMMENT 'Result of 8.0/7.0, most common value',
  `scale` float NOT NULL DEFAULT '1',
  `base_attack_time` int(10) unsigned NOT NULL DEFAULT '0',
  `unit_class` int(10) unsigned NOT NULL DEFAULT '0',
  `unit_flags` int(10) unsigned NOT NULL DEFAULT '0',
  `unit_flags2` int(10) unsigned NOT NULL DEFAULT '0',
  `vehicle_id` int(11) NOT NULL DEFAULT '0',
  `hover_height` float NOT NULL DEFAULT '0',
  `auras` text COLLATE latin1_general_ci COMMENT 'only includes auras with the NO_CASTER flag',
  `sniff_build` mediumint(8) unsigned NOT NULL DEFAULT '0',
  PRIMARY KEY (`entry`)
) ENGINE=MyISAM DEFAULT CHARSET=latin1 COLLATE=latin1_general_ci ROW_FORMAT=FIXED COMMENT='most commonly seen values per given creature id\r\ndata here is not guaranteed to be the true default';

-- Data exporting was unselected.


-- Dumping structure for table sniffs_new_test.creature_template_locale
DROP TABLE IF EXISTS `creature_template_locale`;
CREATE TABLE IF NOT EXISTS `creature_template_locale` (
  `entry` mediumint(8) unsigned NOT NULL DEFAULT '0',
  `locale` varchar(4) NOT NULL,
  `Name` text,
  `NameAlt` text,
  `Title` text,
  `TitleAlt` text,
  `VerifiedBuild` mediumint(8) unsigned NOT NULL DEFAULT '0',
  PRIMARY KEY (`entry`,`locale`)
) ENGINE=MyISAM DEFAULT CHARSET=utf8;

-- Data exporting was unselected.


-- Dumping structure for table sniffs_new_test.creature_template_wdb
DROP TABLE IF EXISTS `creature_template_wdb`;
CREATE TABLE IF NOT EXISTS `creature_template_wdb` (
  `entry` int(10) unsigned NOT NULL,
  `sniff_build` mediumint(8) unsigned NOT NULL DEFAULT '0',
  `kill_credit1` int(11) DEFAULT NULL COMMENT 'wotlk+',
  `kill_credit2` int(11) DEFAULT NULL COMMENT 'wotlk+',
  `display_total_count` int(11) DEFAULT NULL COMMENT '1.13+',
  `display_total_probability` float DEFAULT NULL COMMENT '1.13+',
  `display_id1` int(11) NOT NULL DEFAULT '0',
  `display_id2` int(11) DEFAULT NULL COMMENT 'tbc+',
  `display_id3` int(11) DEFAULT NULL COMMENT 'tbc+',
  `display_id4` int(11) DEFAULT NULL COMMENT 'tbc+',
  `display_scale1` float DEFAULT NULL COMMENT '1.13+',
  `display_scale2` float DEFAULT NULL COMMENT '1.13+',
  `display_scale3` float DEFAULT NULL COMMENT '1.13+',
  `display_scale4` float DEFAULT NULL COMMENT '1.13+',
  `display_probability1` float DEFAULT NULL COMMENT '1.13+',
  `display_probability2` float DEFAULT NULL COMMENT '1.13+',
  `display_probability3` float DEFAULT NULL COMMENT '1.13+',
  `display_probability4` float DEFAULT NULL COMMENT '1.13+',
  `name` varchar(256) COLLATE latin1_general_ci NOT NULL DEFAULT '',
  `female_name` varchar(256) COLLATE latin1_general_ci DEFAULT NULL COMMENT 'cata+',
  `subname` varchar(256) COLLATE latin1_general_ci DEFAULT '',
  `title_alt` varchar(256) COLLATE latin1_general_ci DEFAULT NULL COMMENT 'mop+',
  `icon_name` varchar(256) COLLATE latin1_general_ci DEFAULT NULL COMMENT 'tbc+',
  `health_scaling_expansion` int(11) DEFAULT NULL COMMENT 'wod+',
  `required_expansion` int(11) DEFAULT NULL COMMENT 'cata+',
  `vignette_id` int(11) DEFAULT NULL COMMENT 'legion+',
  `unit_class` int(11) DEFAULT NULL COMMENT 'bfa+',
  `rank` int(11) NOT NULL DEFAULT '0',
  `pet_family` int(11) NOT NULL DEFAULT '0',
  `type` int(11) NOT NULL DEFAULT '0',
  `type_flags` int(10) unsigned NOT NULL DEFAULT '0' COMMENT 'actually static flags before 1.10',
  `type_flags2` int(10) unsigned DEFAULT NULL COMMENT 'cata+',
  `pet_spell_list_id` int(11) DEFAULT NULL COMMENT '1.8+',
  `health_multiplier` float DEFAULT NULL COMMENT 'tbc+',
  `mana_multiplier` float DEFAULT NULL COMMENT 'tbc+',
  `civilian` int(11) DEFAULT NULL COMMENT 'removed in tbc',
  `racial_leader` int(11) NOT NULL DEFAULT '0',
  `movement_id` int(11) DEFAULT NULL COMMENT 'wotlk+',
  PRIMARY KEY (`entry`,`sniff_build`)
) ENGINE=MyISAM DEFAULT CHARSET=latin1 COLLATE=latin1_general_ci COMMENT='static creature data from SMSG_QUERY_CREATURE_RESPONSE which gets saved to wdb cache';

-- Data exporting was unselected.


-- Dumping structure for table sniffs_new_test.creature_text
DROP TABLE IF EXISTS `creature_text`;
CREATE TABLE IF NOT EXISTS `creature_text` (
  `unixtimems` bigint(20) unsigned NOT NULL COMMENT 'when the packet was received',
  `guid` int(10) unsigned NOT NULL COMMENT 'creature spawn guid',
  `entry` int(10) unsigned NOT NULL COMMENT 'creature template id',
  `text` text NOT NULL,
  `chat_type` tinyint(3) unsigned NOT NULL DEFAULT '0',
  `language` tinyint(3) unsigned NOT NULL DEFAULT '0',
  `target_guid` int(10) unsigned NOT NULL DEFAULT '0',
  `target_id` int(10) unsigned NOT NULL DEFAULT '0',
  `target_type` varchar(16) CHARACTER SET latin1 COLLATE latin1_general_ci NOT NULL DEFAULT ''
) ENGINE=MyISAM DEFAULT CHARSET=utf8 COMMENT='individual instances of creatures sending a text message';

-- Data exporting was unselected.


-- Dumping structure for table sniffs_new_test.creature_threat_clear
DROP TABLE IF EXISTS `creature_threat_clear`;
CREATE TABLE IF NOT EXISTS `creature_threat_clear` (
  `unixtimems` bigint(20) unsigned NOT NULL DEFAULT '0',
  `guid` int(10) unsigned NOT NULL,
  PRIMARY KEY (`unixtimems`,`guid`)
) ENGINE=MyISAM DEFAULT CHARSET=utf8 COLLATE=utf8_unicode_ci COMMENT='from SMSG_THREAT_CLEAR';

-- Data exporting was unselected.


-- Dumping structure for table sniffs_new_test.creature_threat_remove
DROP TABLE IF EXISTS `creature_threat_remove`;
CREATE TABLE IF NOT EXISTS `creature_threat_remove` (
  `unixtimems` bigint(20) unsigned NOT NULL DEFAULT '0',
  `guid` int(10) unsigned NOT NULL,
  `target_guid` int(10) unsigned NOT NULL DEFAULT '0',
  `target_id` int(10) unsigned NOT NULL DEFAULT '0',
  `target_type` varchar(16) COLLATE latin1_general_ci NOT NULL DEFAULT '',
  PRIMARY KEY (`unixtimems`,`guid`,`target_guid`,`target_id`,`target_type`)
) ENGINE=MyISAM DEFAULT CHARSET=latin1 COLLATE=latin1_general_ci COMMENT='from SMSG_THREAT_REMOVE';

-- Data exporting was unselected.


-- Dumping structure for table sniffs_new_test.creature_threat_update
DROP TABLE IF EXISTS `creature_threat_update`;
CREATE TABLE IF NOT EXISTS `creature_threat_update` (
  `unixtimems` bigint(20) unsigned NOT NULL DEFAULT '0',
  `guid` int(10) unsigned NOT NULL,
  `target_count` int(10) unsigned NOT NULL DEFAULT '0',
  `target_list_id` int(10) unsigned NOT NULL DEFAULT '0',
  PRIMARY KEY (`guid`,`unixtimems`,`target_list_id`)
) ENGINE=MyISAM DEFAULT CHARSET=latin1 COLLATE=latin1_general_ci ROW_FORMAT=COMPACT COMMENT='from SMSG_THREAT_UPDATE and SMSG_HIGHEST_THREAT_UPDATE';

-- Data exporting was unselected.


-- Dumping structure for table sniffs_new_test.creature_threat_update_target
DROP TABLE IF EXISTS `creature_threat_update_target`;
CREATE TABLE IF NOT EXISTS `creature_threat_update_target` (
  `list_id` int(10) unsigned NOT NULL,
  `target_guid` int(10) unsigned NOT NULL DEFAULT '0',
  `target_id` int(10) unsigned NOT NULL DEFAULT '0',
  `target_type` varchar(16) COLLATE latin1_general_ci NOT NULL DEFAULT '',
  `threat` bigint(20) NOT NULL DEFAULT '0'
) ENGINE=MyISAM DEFAULT CHARSET=latin1 COLLATE=latin1_general_ci ROW_FORMAT=COMPACT COMMENT='individual targets and their threat from SMSG_THREAT_UPDATE and SMSG_HIGHEST_THREAT_UPDATE';

-- Data exporting was unselected.


-- Dumping structure for table sniffs_new_test.creature_trainer
DROP TABLE IF EXISTS `creature_trainer`;
CREATE TABLE IF NOT EXISTS `creature_trainer` (
  `entry` int(11) unsigned NOT NULL,
  `trainer_id` int(11) unsigned NOT NULL DEFAULT '0',
  `menu_id` int(11) unsigned NOT NULL DEFAULT '0',
  `option_index` int(11) unsigned NOT NULL DEFAULT '0',
  `sniff_build` mediumint(8) unsigned NOT NULL DEFAULT '0',
  PRIMARY KEY (`entry`,`menu_id`,`trainer_id`,`option_index`)
) ENGINE=MyISAM DEFAULT CHARSET=latin1 COLLATE=latin1_general_ci;

-- Data exporting was unselected.


-- Dumping structure for table sniffs_new_test.creature_unique_emote
DROP TABLE IF EXISTS `creature_unique_emote`;
CREATE TABLE IF NOT EXISTS `creature_unique_emote` (
  `entry` int(10) unsigned NOT NULL,
  `emote_id` int(10) unsigned NOT NULL,
  `emote_name` varchar(50) COLLATE latin1_general_ci DEFAULT NULL,
  `sniff_id_list` text COLLATE latin1_general_ci NOT NULL,
  PRIMARY KEY (`entry`,`emote_id`)
) ENGINE=MyISAM DEFAULT CHARSET=latin1 COLLATE=latin1_general_ci ROW_FORMAT=COMPACT COMMENT='all unique emote ids played by a given creature id\r\nfrom SMSG_EMOTE';

-- Data exporting was unselected.


-- Dumping structure for table sniffs_new_test.creature_unique_equipment
DROP TABLE IF EXISTS `creature_unique_equipment`;
CREATE TABLE IF NOT EXISTS `creature_unique_equipment` (
  `entry` mediumint(8) unsigned NOT NULL DEFAULT '0',
  `idx` tinyint(3) unsigned NOT NULL AUTO_INCREMENT,
  `main_hand_slot_item` mediumint(8) unsigned NOT NULL DEFAULT '0',
  `off_hand_slot_item` mediumint(8) unsigned NOT NULL DEFAULT '0',
  `ranged_slot_item` mediumint(8) unsigned NOT NULL DEFAULT '0',
  `sniff_id_list` text COLLATE latin1_general_ci NOT NULL,
  PRIMARY KEY (`entry`,`main_hand_slot_item`,`off_hand_slot_item`,`ranged_slot_item`),
  UNIQUE KEY `entry` (`entry`,`idx`)
) ENGINE=MyISAM DEFAULT CHARSET=latin1 COLLATE=latin1_general_ci COMMENT='all weapons used by a given creature id, assigned in the virtual item slots';

-- Data exporting was unselected.


-- Dumping structure for table sniffs_new_test.creature_unique_faction
DROP TABLE IF EXISTS `creature_unique_faction`;
CREATE TABLE IF NOT EXISTS `creature_unique_faction` (
  `entry` mediumint(8) unsigned NOT NULL,
  `faction` int(10) unsigned NOT NULL,
  `sniff_id_list` text COLLATE latin1_general_ci NOT NULL,
  PRIMARY KEY (`entry`,`faction`)
) ENGINE=MyISAM DEFAULT CHARSET=latin1 COLLATE=latin1_general_ci COMMENT='all unique faction template ids used by a given creature id';

-- Data exporting was unselected.


-- Dumping structure for table sniffs_new_test.creature_unique_gossip
DROP TABLE IF EXISTS `creature_unique_gossip`;
CREATE TABLE IF NOT EXISTS `creature_unique_gossip` (
  `entry` mediumint(8) unsigned NOT NULL,
  `gossip_menu_id` mediumint(8) unsigned NOT NULL,
  `is_default` tinyint(3) unsigned NOT NULL,
  `sniff_id_list` text COLLATE latin1_general_ci NOT NULL,
  PRIMARY KEY (`entry`,`gossip_menu_id`,`is_default`)
) ENGINE=MyISAM DEFAULT CHARSET=latin1 COLLATE=latin1_general_ci COMMENT='all unique gossip menu ids used for given creature id';

-- Data exporting was unselected.


-- Dumping structure for table sniffs_new_test.creature_unique_spell_hit
DROP TABLE IF EXISTS `creature_unique_spell_hit`;
CREATE TABLE IF NOT EXISTS `creature_unique_spell_hit` (
  `entry` int(10) unsigned NOT NULL,
  `spell_id` int(10) unsigned NOT NULL,
  `sniff_id_list` text COLLATE latin1_general_ci NOT NULL,
  PRIMARY KEY (`entry`,`spell_id`)
) ENGINE=MyISAM DEFAULT CHARSET=latin1 COLLATE=latin1_general_ci ROW_FORMAT=DYNAMIC COMMENT='spells that creatures were hit by';

-- Data exporting was unselected.


-- Dumping structure for table sniffs_new_test.creature_unique_text
DROP TABLE IF EXISTS `creature_unique_text`;
CREATE TABLE IF NOT EXISTS `creature_unique_text` (
  `entry` mediumint(8) unsigned NOT NULL DEFAULT '0' COMMENT 'creature template id',
  `idx` tinyint(3) unsigned NOT NULL AUTO_INCREMENT COMMENT 'counter of unique texts per creature id',
  `text` varchar(512) COLLATE latin1_general_ci NOT NULL COMMENT 'the actual text that was sent',
  `chat_type` tinyint(3) unsigned NOT NULL DEFAULT '0' COMMENT 'version specific chat type enum, not the same as values used in mangos',
  `language` tinyint(3) unsigned NOT NULL DEFAULT '0' COMMENT 'part of the packet, references Languages.dbc',
  `emote` mediumint(8) unsigned NOT NULL DEFAULT '0' COMMENT 'not part of the packet, emote seen close to when the chat packet was received, references Emotes.dbc',
  `sound` mediumint(8) unsigned NOT NULL DEFAULT '0' COMMENT 'not part of the packet, sound heard close to when the chat packet was received, references SoundEntries.dbc',
  `broadcast_text_id` mediumint(6) unsigned NOT NULL DEFAULT '0' COMMENT 'not part of the packet, must be manually set',
  `health_percent` float DEFAULT NULL COMMENT 'not part of the packet, the current health of the creature at the time the text was said',
  `comment` varchar(255) CHARACTER SET utf8 DEFAULT '',
  `sniff_id_list` text COLLATE latin1_general_ci NOT NULL,
  PRIMARY KEY (`entry`,`text`),
  UNIQUE KEY `entry` (`entry`,`idx`)
) ENGINE=MyISAM DEFAULT CHARSET=latin1 COLLATE=latin1_general_ci COMMENT='unique texts per creature id';

-- Data exporting was unselected.


-- Dumping structure for table sniffs_new_test.creature_values_update
DROP TABLE IF EXISTS `creature_values_update`;
CREATE TABLE IF NOT EXISTS `creature_values_update` (
  `unixtimems` bigint(20) unsigned NOT NULL COMMENT 'when the packet was received',
  `guid` int(10) unsigned NOT NULL COMMENT 'creature spawn guid',
  `entry` int(10) unsigned DEFAULT NULL,
  `scale` float DEFAULT NULL,
  `display_id` int(10) unsigned DEFAULT NULL,
  `mount_display_id` int(10) unsigned DEFAULT NULL,
  `faction` int(10) unsigned DEFAULT NULL,
  `level` int(10) unsigned DEFAULT NULL,
  `npc_flags` int(10) unsigned DEFAULT NULL,
  `unit_flags` int(10) unsigned DEFAULT NULL,
  `unit_flags2` int(10) unsigned DEFAULT NULL,
  `dynamic_flags` int(10) unsigned DEFAULT NULL,
  `current_health` int(10) unsigned DEFAULT NULL,
  `max_health` int(10) unsigned DEFAULT NULL,
  `power_type` int(10) unsigned DEFAULT NULL,
  `aura_state` int(10) unsigned DEFAULT NULL,
  `emote_state` int(10) unsigned DEFAULT NULL,
  `stand_state` int(10) unsigned DEFAULT NULL,
  `vis_flags` int(10) unsigned DEFAULT NULL,
  `anim_tier` int(10) unsigned DEFAULT NULL,
  `sheath_state` int(10) unsigned DEFAULT NULL,
  `pvp_flags` int(10) unsigned DEFAULT NULL,
  `shapeshift_form` int(10) unsigned DEFAULT NULL,
  `bounding_radius` float DEFAULT NULL,
  `combat_reach` float DEFAULT NULL,
  `mod_melee_haste` float DEFAULT NULL,
  `main_hand_attack_time` int(10) unsigned DEFAULT NULL,
  `off_hand_attack_time` int(10) unsigned DEFAULT NULL,
  `channel_spell_id` int(10) unsigned DEFAULT NULL,
  `channel_visual_id` int(10) unsigned DEFAULT NULL
) ENGINE=MyISAM DEFAULT CHARSET=latin1 COLLATE=latin1_general_ci COMMENT='values updates from SMSG_UPDATE_OBJECT';

-- Data exporting was unselected.


-- Dumping structure for table sniffs_new_test.creature_visibility_distance
DROP TABLE IF EXISTS `creature_visibility_distance`;
CREATE TABLE IF NOT EXISTS `creature_visibility_distance` (
  `entry` int(10) unsigned NOT NULL,
  `map` int(10) unsigned NOT NULL,
  `distance` int(10) unsigned NOT NULL COMMENT 'distance from player at time of create object',
  `sniff_id_list` text COLLATE latin1_general_ci NOT NULL,
  PRIMARY KEY (`entry`,`distance`,`map`)
) ENGINE=MyISAM DEFAULT CHARSET=latin1 COLLATE=latin1_general_ci ROW_FORMAT=COMPACT COMMENT='max visibility distances';

-- Data exporting was unselected.


-- Dumping structure for table sniffs_new_test.dynamicobject
DROP TABLE IF EXISTS `dynamicobject`;
CREATE TABLE IF NOT EXISTS `dynamicobject` (
  `guid` int(10) unsigned NOT NULL AUTO_INCREMENT COMMENT 'Global Unique Identifier',
  `map` smallint(5) unsigned DEFAULT '0' COMMENT 'Map Identifier',
  `position_x` float NOT NULL DEFAULT '0',
  `position_y` float NOT NULL DEFAULT '0',
  `position_z` float NOT NULL DEFAULT '0',
  `orientation` float NOT NULL DEFAULT '0',
  `caster_guid` int(11) unsigned NOT NULL DEFAULT '0',
  `caster_id` int(11) unsigned NOT NULL DEFAULT '0',
  `caster_type` varchar(16) COLLATE latin1_general_ci NOT NULL DEFAULT '',
  `spell_id` int(10) unsigned NOT NULL DEFAULT '0',
  `visual_id` int(10) unsigned NOT NULL DEFAULT '0',
  `radius` float NOT NULL DEFAULT '0',
  `type` tinyint(3) unsigned NOT NULL DEFAULT '0',
  `cast_time` int(10) unsigned NOT NULL DEFAULT '0',
  PRIMARY KEY (`guid`)
) ENGINE=MyISAM DEFAULT CHARSET=latin1 COLLATE=latin1_general_ci ROW_FORMAT=FIXED COMMENT='the initial state of all individual dynamicobject spawns seen in the sniff\r\nthese are the objects used for ground targeted spell animations';

-- Data exporting was unselected.


-- Dumping structure for table sniffs_new_test.dynamicobject_create1_time
DROP TABLE IF EXISTS `dynamicobject_create1_time`;
CREATE TABLE IF NOT EXISTS `dynamicobject_create1_time` (
  `unixtimems` bigint(20) unsigned NOT NULL COMMENT 'when the packet was received',
  `guid` int(10) unsigned NOT NULL COMMENT 'dynamicobject spawn guid',
  `map` smallint(5) unsigned NOT NULL,
  `position_x` float NOT NULL,
  `position_y` float NOT NULL,
  `position_z` float NOT NULL,
  `orientation` float NOT NULL,
  `transport_guid` int(10) unsigned NOT NULL DEFAULT '0',
  `transport_id` int(10) unsigned NOT NULL DEFAULT '0',
  `transport_type` varchar(16) COLLATE latin1_general_ci NOT NULL DEFAULT '',
  `transport_x` float NOT NULL DEFAULT '0',
  `transport_y` float NOT NULL DEFAULT '0',
  `transport_z` float NOT NULL DEFAULT '0',
  `transport_o` float NOT NULL DEFAULT '0',
  PRIMARY KEY (`guid`,`unixtimems`)
) ENGINE=MyISAM DEFAULT CHARSET=latin1 COLLATE=latin1_general_ci ROW_FORMAT=COMPACT COMMENT='times when the object became visible to the client';

-- Data exporting was unselected.


-- Dumping structure for table sniffs_new_test.dynamicobject_create2_time
DROP TABLE IF EXISTS `dynamicobject_create2_time`;
CREATE TABLE IF NOT EXISTS `dynamicobject_create2_time` (
  `unixtimems` bigint(20) unsigned NOT NULL COMMENT 'when the packet was received',
  `guid` int(10) unsigned NOT NULL COMMENT 'dynamicobject spawn guid',
  `map` smallint(5) unsigned NOT NULL,
  `position_x` float NOT NULL,
  `position_y` float NOT NULL,
  `position_z` float NOT NULL,
  `orientation` float NOT NULL,
  `transport_guid` int(10) unsigned NOT NULL DEFAULT '0',
  `transport_id` int(10) unsigned NOT NULL DEFAULT '0',
  `transport_type` varchar(16) COLLATE latin1_general_ci NOT NULL DEFAULT '',
  `transport_x` float NOT NULL DEFAULT '0',
  `transport_y` float NOT NULL DEFAULT '0',
  `transport_z` float NOT NULL DEFAULT '0',
  `transport_o` float NOT NULL DEFAULT '0',
  PRIMARY KEY (`guid`,`unixtimems`)
) ENGINE=MyISAM DEFAULT CHARSET=latin1 COLLATE=latin1_general_ci ROW_FORMAT=COMPACT COMMENT='the time at which the object spawned';

-- Data exporting was unselected.


-- Dumping structure for table sniffs_new_test.dynamicobject_destroy_time
DROP TABLE IF EXISTS `dynamicobject_destroy_time`;
CREATE TABLE IF NOT EXISTS `dynamicobject_destroy_time` (
  `unixtimems` bigint(20) unsigned NOT NULL COMMENT 'when the packet was received',
  `guid` int(10) unsigned NOT NULL COMMENT 'dynamicobject spawn guid',
  PRIMARY KEY (`guid`,`unixtimems`)
) ENGINE=MyISAM DEFAULT CHARSET=latin1 COLLATE=latin1_general_ci ROW_FORMAT=COMPACT COMMENT='times when the object was destroyed from the client''s prespective due to despawning, becoming invisible, or going out of range';

-- Data exporting was unselected.


-- Dumping structure for table sniffs_new_test.faction_standing_update
DROP TABLE IF EXISTS `faction_standing_update`;
CREATE TABLE IF NOT EXISTS `faction_standing_update` (
  `unixtimems` bigint(20) unsigned NOT NULL DEFAULT '0',
  `reputation_list_id` int(10) NOT NULL DEFAULT '0',
  `standing` int(11) NOT NULL DEFAULT '0',
  `raf_bonus` float NOT NULL DEFAULT '0',
  `achievement_bonus` float NOT NULL DEFAULT '0',
  `show_visual` tinyint(3) unsigned NOT NULL DEFAULT '0',
  PRIMARY KEY (`unixtimems`,`reputation_list_id`,`standing`)
) ENGINE=MyISAM DEFAULT CHARSET=latin1 COLLATE=latin1_general_ci ROW_FORMAT=COMPACT COMMENT='from SMSG_SET_FACTION_STANDING';

-- Data exporting was unselected.


-- Dumping structure for table sniffs_new_test.gameobject
DROP TABLE IF EXISTS `gameobject`;
CREATE TABLE IF NOT EXISTS `gameobject` (
  `guid` int(10) unsigned NOT NULL AUTO_INCREMENT COMMENT 'Global Unique Identifier',
  `original_id` mediumint(8) unsigned NOT NULL DEFAULT '0' COMMENT 'entry from guid',
  `id` mediumint(8) unsigned NOT NULL DEFAULT '0' COMMENT 'entry from update fields',
  `total_observation_time` mediumint(8) unsigned NOT NULL DEFAULT '0' COMMENT 'accumulated time in ms between create and destroy packets',
  `longest_observation_time` mediumint(8) unsigned NOT NULL DEFAULT '0' COMMENT 'longest uninterrupted period of time in ms the object was visible for',
  `map` smallint(5) unsigned DEFAULT '0' COMMENT 'Map Identifier',
  `zone_id` smallint(5) unsigned NOT NULL DEFAULT '0' COMMENT 'Zone Identifier',
  `area_id` smallint(5) unsigned NOT NULL DEFAULT '0' COMMENT 'Area Identifier',
  `position_x` float NOT NULL DEFAULT '0',
  `position_y` float NOT NULL DEFAULT '0',
  `position_z` float NOT NULL DEFAULT '0',
  `orientation` float NOT NULL DEFAULT '0',
  `rotation0` float NOT NULL DEFAULT '0',
  `rotation1` float NOT NULL DEFAULT '0',
  `rotation2` float NOT NULL DEFAULT '0',
  `rotation3` float NOT NULL DEFAULT '0',
  `is_spawn` tinyint(1) unsigned NOT NULL DEFAULT '0' COMMENT 'create object type 2',
  `is_transport` tinyint(1) unsigned NOT NULL DEFAULT '0',
  `is_on_transport` tinyint(1) unsigned NOT NULL DEFAULT '0',
  `is_temporary` tinyint(1) unsigned NOT NULL DEFAULT '0',
  `creator_guid` int(11) unsigned NOT NULL DEFAULT '0',
  `creator_id` int(11) unsigned NOT NULL DEFAULT '0',
  `creator_type` varchar(16) COLLATE latin1_general_ci NOT NULL DEFAULT '',
  `display_id` int(10) unsigned NOT NULL DEFAULT '0',
  `level` int(10) unsigned NOT NULL DEFAULT '0',
  `faction` int(10) unsigned NOT NULL DEFAULT '0',
  `flags` int(10) unsigned NOT NULL DEFAULT '0',
  `dynamic_flags` int(10) unsigned NOT NULL DEFAULT '0',
  `path_progress` int(10) unsigned NOT NULL DEFAULT '0',
  `state` tinyint(3) unsigned NOT NULL DEFAULT '0',
  `type` tinyint(3) unsigned NOT NULL DEFAULT '0',
  `art_kit` tinyint(3) unsigned NOT NULL DEFAULT '0',
  `anim_progress` tinyint(3) unsigned NOT NULL DEFAULT '0',
  `custom_param` int(10) unsigned NOT NULL DEFAULT '0',
  `sniff_id` smallint(5) unsigned NOT NULL DEFAULT '0' COMMENT 'points to sniff_file table',
  `sniff_build` mediumint(8) unsigned NOT NULL DEFAULT '0',
  PRIMARY KEY (`guid`)
) ENGINE=MyISAM DEFAULT CHARSET=latin1 COLLATE=latin1_general_ci ROW_FORMAT=FIXED COMMENT='the initial state of all individual gameobject spawns seen in the sniff';

-- Data exporting was unselected.


-- Dumping structure for table sniffs_new_test.gameobject_addon
DROP TABLE IF EXISTS `gameobject_addon`;
CREATE TABLE IF NOT EXISTS `gameobject_addon` (
  `guid` int(10) unsigned NOT NULL DEFAULT '0',
  `parent_rotation0` float NOT NULL DEFAULT '0',
  `parent_rotation1` float NOT NULL DEFAULT '0',
  `parent_rotation2` float NOT NULL DEFAULT '0',
  `parent_rotation3` float NOT NULL DEFAULT '1',
  `invisibilityType` tinyint(3) unsigned NOT NULL DEFAULT '0',
  `invisibilityValue` int(10) unsigned NOT NULL DEFAULT '0',
  PRIMARY KEY (`guid`)
) ENGINE=MyISAM DEFAULT CHARSET=latin1 COLLATE=latin1_general_ci;

-- Data exporting was unselected.


-- Dumping structure for table sniffs_new_test.gameobject_create1_time
DROP TABLE IF EXISTS `gameobject_create1_time`;
CREATE TABLE IF NOT EXISTS `gameobject_create1_time` (
  `unixtimems` bigint(20) unsigned NOT NULL COMMENT 'when the packet was received',
  `guid` int(10) unsigned NOT NULL COMMENT 'gameobject spawn guid',
  `map` smallint(5) unsigned NOT NULL,
  `position_x` float NOT NULL,
  `position_y` float NOT NULL,
  `position_z` float NOT NULL,
  `orientation` float NOT NULL,
  `transport_guid` int(10) unsigned NOT NULL DEFAULT '0',
  `transport_id` int(10) unsigned NOT NULL DEFAULT '0',
  `transport_type` varchar(16) COLLATE latin1_general_ci NOT NULL DEFAULT '',
  `transport_x` float NOT NULL DEFAULT '0',
  `transport_y` float NOT NULL DEFAULT '0',
  `transport_z` float NOT NULL DEFAULT '0',
  `transport_o` float NOT NULL DEFAULT '0',
  `transport_path_timer` int(10) unsigned NOT NULL DEFAULT '0',
  PRIMARY KEY (`guid`,`unixtimems`)
) ENGINE=MyISAM DEFAULT CHARSET=latin1 COLLATE=latin1_general_ci ROW_FORMAT=COMPACT COMMENT='times when the object became visible to the client';

-- Data exporting was unselected.


-- Dumping structure for table sniffs_new_test.gameobject_create2_time
DROP TABLE IF EXISTS `gameobject_create2_time`;
CREATE TABLE IF NOT EXISTS `gameobject_create2_time` (
  `unixtimems` bigint(20) unsigned NOT NULL COMMENT 'when the packet was received',
  `guid` int(10) unsigned NOT NULL COMMENT 'gameobject spawn guid',
  `map` smallint(5) unsigned NOT NULL,
  `position_x` float NOT NULL,
  `position_y` float NOT NULL,
  `position_z` float NOT NULL,
  `orientation` float NOT NULL,
  `transport_guid` int(10) unsigned NOT NULL DEFAULT '0',
  `transport_id` int(10) unsigned NOT NULL DEFAULT '0',
  `transport_type` varchar(16) COLLATE latin1_general_ci NOT NULL DEFAULT '',
  `transport_x` float NOT NULL DEFAULT '0',
  `transport_y` float NOT NULL DEFAULT '0',
  `transport_z` float NOT NULL DEFAULT '0',
  `transport_o` float NOT NULL DEFAULT '0',
  `transport_path_timer` int(10) unsigned NOT NULL DEFAULT '0',
  PRIMARY KEY (`guid`,`unixtimems`)
) ENGINE=MyISAM DEFAULT CHARSET=latin1 COLLATE=latin1_general_ci ROW_FORMAT=COMPACT COMMENT='the time at which the object spawned';

-- Data exporting was unselected.


-- Dumping structure for table sniffs_new_test.gameobject_custom_anim
DROP TABLE IF EXISTS `gameobject_custom_anim`;
CREATE TABLE IF NOT EXISTS `gameobject_custom_anim` (
  `unixtimems` bigint(20) unsigned NOT NULL COMMENT 'when the packet was received',
  `guid` int(10) unsigned NOT NULL COMMENT 'gameobject spawn guid',
  `anim_id` int(10) unsigned NOT NULL DEFAULT '0',
  `as_despawn` tinyint(3) unsigned DEFAULT NULL,
  PRIMARY KEY (`guid`,`unixtimems`)
) ENGINE=MyISAM DEFAULT CHARSET=latin1 COLLATE=latin1_general_ci ROW_FORMAT=COMPACT COMMENT='from SMSG_GAME_OBJECT_CUSTOM_ANIM';

-- Data exporting was unselected.


-- Dumping structure for table sniffs_new_test.gameobject_despawn_anim
DROP TABLE IF EXISTS `gameobject_despawn_anim`;
CREATE TABLE IF NOT EXISTS `gameobject_despawn_anim` (
  `unixtimems` bigint(20) unsigned NOT NULL COMMENT 'when the packet was received',
  `guid` int(10) unsigned NOT NULL COMMENT 'gameobject spawn guid',
  PRIMARY KEY (`guid`,`unixtimems`)
) ENGINE=MyISAM DEFAULT CHARSET=latin1 COLLATE=latin1_general_ci ROW_FORMAT=COMPACT COMMENT='from SMSG_GAME_OBJECT_DESPAWN';

-- Data exporting was unselected.


-- Dumping structure for table sniffs_new_test.gameobject_destroy_time
DROP TABLE IF EXISTS `gameobject_destroy_time`;
CREATE TABLE IF NOT EXISTS `gameobject_destroy_time` (
  `unixtimems` bigint(20) unsigned NOT NULL COMMENT 'when the packet was received',
  `guid` int(10) unsigned NOT NULL COMMENT 'gameobject spawn guid',
  PRIMARY KEY (`guid`,`unixtimems`)
) ENGINE=MyISAM DEFAULT CHARSET=latin1 COLLATE=latin1_general_ci COMMENT='times when the object was destroyed from the client''s prespective due to despawning, becoming invisible, or going out of range';

-- Data exporting was unselected.


-- Dumping structure for table sniffs_new_test.gameobject_loot
DROP TABLE IF EXISTS `gameobject_loot`;
CREATE TABLE IF NOT EXISTS `gameobject_loot` (
  `entry` int(10) unsigned NOT NULL COMMENT 'gameobject template id',
  `loot_id` int(10) unsigned NOT NULL COMMENT 'counter',
  `money` int(10) unsigned NOT NULL DEFAULT '0' COMMENT 'copper',
  `items_count` int(10) unsigned NOT NULL DEFAULT '0' COMMENT 'number of items dropped',
  `sniff_id` smallint(5) unsigned NOT NULL DEFAULT '0' COMMENT 'points to sniff_file table',
  `sniff_build` mediumint(8) unsigned NOT NULL DEFAULT '0',
  PRIMARY KEY (`entry`,`loot_id`)
) ENGINE=MyISAM DEFAULT CHARSET=latin1 COLLATE=latin1_general_ci COMMENT='each row represents a separate loot instance\r\nmight not contain all the items or gold that dropped if somebody else looted them first';

-- Data exporting was unselected.


-- Dumping structure for table sniffs_new_test.gameobject_loot_item
DROP TABLE IF EXISTS `gameobject_loot_item`;
CREATE TABLE IF NOT EXISTS `gameobject_loot_item` (
  `loot_id` int(10) unsigned NOT NULL COMMENT 'references gameobject_loot',
  `item_id` int(10) unsigned NOT NULL COMMENT 'item template id',
  `count` int(10) unsigned NOT NULL DEFAULT '1' COMMENT 'stack size'
) ENGINE=MyISAM DEFAULT CHARSET=latin1 COLLATE=latin1_general_ci COMMENT='individual item that is part of a loot instance';

-- Data exporting was unselected.


-- Dumping structure for table sniffs_new_test.gameobject_quest_item
DROP TABLE IF EXISTS `gameobject_quest_item`;
CREATE TABLE IF NOT EXISTS `gameobject_quest_item` (
  `entry` int(10) unsigned NOT NULL,
  `idx` tinyint(3) unsigned NOT NULL,
  `item_id` int(10) unsigned NOT NULL DEFAULT '0',
  `sniff_build` mediumint(8) unsigned NOT NULL DEFAULT '0',
  PRIMARY KEY (`entry`,`idx`)
) ENGINE=MyISAM DEFAULT CHARSET=utf8 COMMENT='quest items that drop from a given gameobject id\r\nfrom SMSG_QUERY_GAME_OBJECT_RESPONSE';

-- Data exporting was unselected.


-- Dumping structure for table sniffs_new_test.gameobject_respawn_time
DROP TABLE IF EXISTS `gameobject_respawn_time`;
CREATE TABLE IF NOT EXISTS `gameobject_respawn_time` (
  `old_guid` int(10) unsigned NOT NULL,
  `new_guid` int(10) unsigned NOT NULL,
  `respawn_time` int(10) unsigned NOT NULL COMMENT 'time in seconds',
  PRIMARY KEY (`old_guid`,`new_guid`)
) ENGINE=MyISAM DEFAULT CHARSET=latin1 COLLATE=latin1_general_ci ROW_FORMAT=COMPACT COMMENT='stores the time in seconds between the despawn of a gameobject, and the spawn of another on the same position\r\nrespawn time is reduced dynamically if there are too many players in the same area, so beware of abnormally low values';

-- Data exporting was unselected.


-- Dumping structure for table sniffs_new_test.gameobject_template
DROP TABLE IF EXISTS `gameobject_template`;
CREATE TABLE IF NOT EXISTS `gameobject_template` (
  `entry` mediumint(8) unsigned NOT NULL DEFAULT '0',
  `sniff_build` mediumint(8) unsigned NOT NULL DEFAULT '0',
  `type` tinyint(3) unsigned NOT NULL DEFAULT '0',
  `display_id` mediumint(8) unsigned NOT NULL DEFAULT '0',
  `scale` float DEFAULT NULL COMMENT 'wotlk+',
  `name` varchar(100) NOT NULL DEFAULT '',
  `icon_name` varchar(100) DEFAULT NULL COMMENT 'tbc+',
  `cast_bar_caption` varchar(100) DEFAULT NULL COMMENT 'tbc+',
  `unk1` varchar(100) DEFAULT NULL COMMENT '1.12+',
  `data0` int(10) unsigned NOT NULL DEFAULT '0',
  `data1` int(11) NOT NULL DEFAULT '0',
  `data2` int(10) unsigned NOT NULL DEFAULT '0',
  `data3` int(10) unsigned NOT NULL DEFAULT '0',
  `data4` int(10) unsigned NOT NULL DEFAULT '0',
  `data5` int(10) unsigned NOT NULL DEFAULT '0',
  `data6` int(11) NOT NULL DEFAULT '0',
  `data7` int(10) unsigned NOT NULL DEFAULT '0',
  `data8` int(10) unsigned NOT NULL DEFAULT '0',
  `data9` int(10) unsigned NOT NULL DEFAULT '0',
  `data10` int(10) unsigned NOT NULL DEFAULT '0',
  `data11` int(10) unsigned NOT NULL DEFAULT '0',
  `data12` int(10) unsigned NOT NULL DEFAULT '0',
  `data13` int(10) unsigned NOT NULL DEFAULT '0',
  `data14` int(10) unsigned NOT NULL DEFAULT '0',
  `data15` int(10) unsigned NOT NULL DEFAULT '0',
  `data16` int(10) unsigned DEFAULT NULL COMMENT '1.12+',
  `data17` int(10) unsigned DEFAULT NULL COMMENT '1.12+',
  `data18` int(10) unsigned DEFAULT NULL COMMENT '1.12+',
  `data19` int(10) unsigned DEFAULT NULL COMMENT '1.12+',
  `data20` int(10) unsigned DEFAULT NULL COMMENT '1.12+',
  `data21` int(10) unsigned DEFAULT NULL COMMENT '1.12+',
  `data22` int(10) unsigned DEFAULT NULL COMMENT '1.12+',
  `data23` int(10) unsigned DEFAULT NULL COMMENT '1.12+',
  `data24` int(10) unsigned DEFAULT NULL COMMENT 'cata+',
  `data25` int(10) unsigned DEFAULT NULL COMMENT 'cata+',
  `data26` int(10) unsigned DEFAULT NULL COMMENT 'cata+',
  `data27` int(10) unsigned DEFAULT NULL COMMENT 'cata+',
  `data28` int(10) unsigned DEFAULT NULL COMMENT 'cata+',
  `data29` int(10) unsigned DEFAULT NULL COMMENT 'cata+',
  `data30` int(10) unsigned DEFAULT NULL COMMENT 'cata+',
  `data31` int(10) unsigned DEFAULT NULL COMMENT 'cata+',
  `data32` int(10) unsigned DEFAULT NULL COMMENT 'cata+',
  `data33` int(10) unsigned DEFAULT NULL COMMENT 'cata+',
  `quest_items_count` tinyint(3) unsigned DEFAULT NULL COMMENT 'wotlk+',
  `required_level` tinyint(3) unsigned DEFAULT NULL COMMENT 'cata+',
  PRIMARY KEY (`entry`,`sniff_build`),
  KEY `idx_name` (`name`)
) ENGINE=MyISAM DEFAULT CHARSET=utf8 ROW_FORMAT=FIXED COMMENT='static gameobject data from SMSG_QUERY_GAME_OBJECT_RESPONSE which gets saved to wdb cache';

-- Data exporting was unselected.


-- Dumping structure for table sniffs_new_test.gameobject_template_addon
DROP TABLE IF EXISTS `gameobject_template_addon`;
CREATE TABLE IF NOT EXISTS `gameobject_template_addon` (
  `entry` int(10) unsigned NOT NULL,
  `faction` int(10) unsigned NOT NULL DEFAULT '0',
  `flags` int(10) unsigned NOT NULL DEFAULT '0',
  PRIMARY KEY (`entry`)
) ENGINE=MyISAM DEFAULT CHARSET=latin1 COLLATE=latin1_general_ci;

-- Data exporting was unselected.


-- Dumping structure for table sniffs_new_test.gameobject_unique_anim
DROP TABLE IF EXISTS `gameobject_unique_anim`;
CREATE TABLE IF NOT EXISTS `gameobject_unique_anim` (
  `entry` int(10) unsigned NOT NULL,
  `anim_id` int(10) unsigned NOT NULL,
  `as_despawn` tinyint(3) unsigned DEFAULT NULL,
  `sniff_id_list` text COLLATE latin1_general_ci NOT NULL,
  PRIMARY KEY (`entry`,`anim_id`)
) ENGINE=MyISAM DEFAULT CHARSET=latin1 COLLATE=latin1_general_ci ROW_FORMAT=COMPACT COMMENT='all unique custom anim ids used by a given gameobject id\r\nfrom SMSG_GAME_OBJECT_CUSTOM_ANIM';

-- Data exporting was unselected.


-- Dumping structure for table sniffs_new_test.gameobject_values_update
DROP TABLE IF EXISTS `gameobject_values_update`;
CREATE TABLE IF NOT EXISTS `gameobject_values_update` (
  `unixtimems` bigint(20) unsigned NOT NULL COMMENT 'when the packet was received',
  `guid` int(10) unsigned NOT NULL COMMENT 'gameobject spawn guid',
  `flags` int(10) unsigned DEFAULT NULL,
  `dynamic_flags` int(10) unsigned DEFAULT NULL,
  `path_progress` int(10) unsigned DEFAULT NULL,
  `state` int(10) unsigned DEFAULT NULL,
  `art_kit` int(10) unsigned DEFAULT NULL,
  `anim_progress` int(10) unsigned DEFAULT NULL,
  `custom_param` int(10) unsigned DEFAULT NULL
) ENGINE=MyISAM DEFAULT CHARSET=latin1 COLLATE=latin1_general_ci COMMENT='values updates from SMSG_UPDATE_OBJECT';

-- Data exporting was unselected.


-- Dumping structure for table sniffs_new_test.gameobject_visibility_distance
DROP TABLE IF EXISTS `gameobject_visibility_distance`;
CREATE TABLE IF NOT EXISTS `gameobject_visibility_distance` (
  `entry` int(10) unsigned NOT NULL,
  `map` int(10) unsigned NOT NULL,
  `distance` int(10) unsigned NOT NULL COMMENT 'distance from player at time of create object',
  `sniff_id_list` text COLLATE latin1_general_ci NOT NULL,
  PRIMARY KEY (`entry`,`distance`,`map`)
) ENGINE=MyISAM DEFAULT CHARSET=latin1 COLLATE=latin1_general_ci ROW_FORMAT=COMPACT COMMENT='max visibility distances';

-- Data exporting was unselected.


-- Dumping structure for table sniffs_new_test.gossip_menu
DROP TABLE IF EXISTS `gossip_menu`;
CREATE TABLE IF NOT EXISTS `gossip_menu` (
  `entry` smallint(5) unsigned NOT NULL DEFAULT '0',
  `text_id` mediumint(8) unsigned NOT NULL DEFAULT '0',
  `sniff_build` mediumint(8) unsigned NOT NULL DEFAULT '0',
  PRIMARY KEY (`entry`,`text_id`)
) ENGINE=MyISAM DEFAULT CHARSET=latin1 COLLATE=latin1_general_ci;

-- Data exporting was unselected.


-- Dumping structure for table sniffs_new_test.gossip_menu_option
DROP TABLE IF EXISTS `gossip_menu_option`;
CREATE TABLE IF NOT EXISTS `gossip_menu_option` (
  `menu_id` smallint(5) unsigned NOT NULL DEFAULT '0',
  `id` smallint(5) unsigned NOT NULL DEFAULT '0',
  `option_icon` mediumint(8) unsigned NOT NULL DEFAULT '0',
  `option_text` text,
  `option_broadcast_text` mediumint(6) NOT NULL DEFAULT '0' COMMENT 'not part of the packet, must be manually set',
  `option_id` tinyint(3) unsigned NOT NULL DEFAULT '0' COMMENT 'not part of the packet, guessed based on icon',
  `npc_option_npcflag` int(10) unsigned NOT NULL DEFAULT '0' COMMENT 'not part of the packet, guessed based on icon',
  `action_menu_id` int(10) unsigned NOT NULL DEFAULT '0',
  `action_poi_id` mediumint(8) unsigned NOT NULL DEFAULT '0',
  `box_coded` tinyint(3) unsigned NOT NULL DEFAULT '0',
  `box_money` int(10) unsigned NOT NULL DEFAULT '0',
  `box_text` text,
  `box_broadcast_text` mediumint(6) NOT NULL DEFAULT '0' COMMENT 'not part of the packet, must be manually set',
  `sniff_build` mediumint(8) unsigned NOT NULL DEFAULT '0',
  PRIMARY KEY (`menu_id`,`id`)
) ENGINE=MyISAM DEFAULT CHARSET=utf8;

-- Data exporting was unselected.


-- Dumping structure for table sniffs_new_test.gossip_menu_option_action
DROP TABLE IF EXISTS `gossip_menu_option_action`;
CREATE TABLE IF NOT EXISTS `gossip_menu_option_action` (
  `menu_id` int(10) unsigned NOT NULL DEFAULT '0',
  `id` int(10) unsigned NOT NULL DEFAULT '0',
  `action_menu_id` int(11) unsigned NOT NULL DEFAULT '0',
  `action_poi_id` int(11) unsigned NOT NULL DEFAULT '0',
  PRIMARY KEY (`menu_id`,`id`)
) ENGINE=MyISAM DEFAULT CHARSET=latin1 COLLATE=latin1_general_ci;

-- Data exporting was unselected.


-- Dumping structure for table sniffs_new_test.gossip_menu_option_box
DROP TABLE IF EXISTS `gossip_menu_option_box`;
CREATE TABLE IF NOT EXISTS `gossip_menu_option_box` (
  `menu_id` int(10) unsigned NOT NULL DEFAULT '0',
  `id` int(10) unsigned NOT NULL DEFAULT '0',
  `box_coded` tinyint(3) unsigned NOT NULL DEFAULT '0',
  `box_money` int(11) unsigned NOT NULL DEFAULT '0',
  `box_text` text,
  `box_broadcast_text` int(11) unsigned NOT NULL DEFAULT '0',
  PRIMARY KEY (`menu_id`,`id`)
) ENGINE=MyISAM DEFAULT CHARSET=utf8;

-- Data exporting was unselected.


-- Dumping structure for table sniffs_new_test.hotfix_blob
DROP TABLE IF EXISTS `hotfix_blob`;
CREATE TABLE IF NOT EXISTS `hotfix_blob` (
  `TableHash` int(11) unsigned NOT NULL,
  `RecordId` int(11) NOT NULL,
  `locale` varchar(50) COLLATE utf8_unicode_ci NOT NULL,
  `Blob` blob,
  `VerifiedBuild` int(11) NOT NULL DEFAULT '0',
  PRIMARY KEY (`TableHash`,`RecordId`,`locale`)
) ENGINE=MyISAM DEFAULT CHARSET=utf8 COLLATE=utf8_unicode_ci;

-- Data exporting was unselected.


-- Dumping structure for table sniffs_new_test.hotfix_data
DROP TABLE IF EXISTS `hotfix_data`;
CREATE TABLE IF NOT EXISTS `hotfix_data` (
  `Id` int(10) unsigned NOT NULL,
  `TableHash` int(10) unsigned NOT NULL,
  `RecordId` int(10) NOT NULL,
  `Deleted` int(10) unsigned NOT NULL DEFAULT '0',
  `Status` int(10) unsigned NOT NULL DEFAULT '0',
  `VerifiedBuild` int(10) NOT NULL DEFAULT '0',
  PRIMARY KEY (`Id`,`TableHash`,`RecordId`)
) ENGINE=MyISAM DEFAULT CHARSET=utf8 COLLATE=utf8_unicode_ci;

-- Data exporting was unselected.


-- Dumping structure for table sniffs_new_test.item_loot
DROP TABLE IF EXISTS `item_loot`;
CREATE TABLE IF NOT EXISTS `item_loot` (
  `entry` int(10) unsigned NOT NULL COMMENT 'item template id',
  `loot_id` int(10) unsigned NOT NULL COMMENT 'counter',
  `money` int(10) unsigned NOT NULL DEFAULT '0' COMMENT 'copper',
  `items_count` int(10) unsigned NOT NULL DEFAULT '0' COMMENT 'number of items dropped',
  `sniff_id` smallint(5) unsigned NOT NULL DEFAULT '0' COMMENT 'points to sniff_file table',
  `sniff_build` mediumint(8) unsigned NOT NULL DEFAULT '0',
  PRIMARY KEY (`entry`,`loot_id`)
) ENGINE=MyISAM DEFAULT CHARSET=latin1 COLLATE=latin1_general_ci ROW_FORMAT=COMPACT COMMENT='each row represents a separate loot instance';

-- Data exporting was unselected.


-- Dumping structure for table sniffs_new_test.item_loot_item
DROP TABLE IF EXISTS `item_loot_item`;
CREATE TABLE IF NOT EXISTS `item_loot_item` (
  `loot_id` int(10) unsigned NOT NULL COMMENT 'references item_loot',
  `item_id` int(10) unsigned NOT NULL COMMENT 'item template id',
  `count` int(10) unsigned NOT NULL DEFAULT '1' COMMENT 'stack size'
) ENGINE=MyISAM DEFAULT CHARSET=latin1 COLLATE=latin1_general_ci ROW_FORMAT=COMPACT COMMENT='individual item that is part of a loot instance';

-- Data exporting was unselected.


-- Dumping structure for table sniffs_new_test.item_template
DROP TABLE IF EXISTS `item_template`;
CREATE TABLE IF NOT EXISTS `item_template` (
  `entry` mediumint(8) unsigned NOT NULL DEFAULT '0',
  `class` tinyint(3) unsigned NOT NULL DEFAULT '0',
  `subclass` tinyint(3) unsigned NOT NULL DEFAULT '0',
  `sound_override_subclass` tinyint(3) DEFAULT NULL COMMENT 'tbc+',
  `name` varchar(255) NOT NULL DEFAULT '',
  `description` varchar(255) NOT NULL DEFAULT '',
  `display_id` mediumint(8) unsigned NOT NULL DEFAULT '0',
  `quality` tinyint(3) unsigned NOT NULL DEFAULT '0',
  `flags` int(10) unsigned NOT NULL DEFAULT '0',
  `flags2` int(10) unsigned DEFAULT NULL COMMENT 'wotlk+',
  `buy_price` int(10) unsigned NOT NULL DEFAULT '0',
  `sell_price` int(10) unsigned NOT NULL DEFAULT '0',
  `inventory_type` tinyint(3) unsigned NOT NULL DEFAULT '0',
  `allowable_class` mediumint(9) NOT NULL DEFAULT '-1',
  `allowable_race` mediumint(9) NOT NULL DEFAULT '-1',
  `item_level` tinyint(3) unsigned NOT NULL DEFAULT '0',
  `required_level` tinyint(3) unsigned NOT NULL DEFAULT '0',
  `required_skill` smallint(5) unsigned NOT NULL DEFAULT '0',
  `required_skill_rank` smallint(5) unsigned NOT NULL DEFAULT '0',
  `required_spell` smallint(5) unsigned NOT NULL DEFAULT '0',
  `required_honor_rank` mediumint(8) unsigned NOT NULL DEFAULT '0',
  `required_city_rank` mediumint(8) unsigned NOT NULL DEFAULT '0',
  `required_reputation_faction` smallint(5) unsigned DEFAULT NULL COMMENT '1.7+',
  `required_reputation_rank` smallint(5) unsigned DEFAULT NULL COMMENT '1.7+',
  `max_count` smallint(5) unsigned NOT NULL DEFAULT '0',
  `stackable` smallint(5) unsigned NOT NULL DEFAULT '1',
  `container_slots` tinyint(3) unsigned NOT NULL DEFAULT '0',
  `stats_count` tinyint(3) unsigned DEFAULT NULL COMMENT 'wotlk+',
  `stat_type1` tinyint(3) unsigned NOT NULL DEFAULT '0',
  `stat_value1` smallint(6) NOT NULL DEFAULT '0',
  `stat_type2` tinyint(3) unsigned NOT NULL DEFAULT '0',
  `stat_value2` smallint(6) NOT NULL DEFAULT '0',
  `stat_type3` tinyint(3) unsigned NOT NULL DEFAULT '0',
  `stat_value3` smallint(6) NOT NULL DEFAULT '0',
  `stat_type4` tinyint(3) unsigned NOT NULL DEFAULT '0',
  `stat_value4` smallint(6) NOT NULL DEFAULT '0',
  `stat_type5` tinyint(3) unsigned NOT NULL DEFAULT '0',
  `stat_value5` smallint(6) NOT NULL DEFAULT '0',
  `stat_type6` tinyint(3) unsigned NOT NULL DEFAULT '0',
  `stat_value6` smallint(6) NOT NULL DEFAULT '0',
  `stat_type7` tinyint(3) unsigned NOT NULL DEFAULT '0',
  `stat_value7` smallint(6) NOT NULL DEFAULT '0',
  `stat_type8` tinyint(3) unsigned NOT NULL DEFAULT '0',
  `stat_value8` smallint(6) NOT NULL DEFAULT '0',
  `stat_type9` tinyint(3) unsigned NOT NULL DEFAULT '0',
  `stat_value9` smallint(6) NOT NULL DEFAULT '0',
  `stat_type10` tinyint(3) unsigned NOT NULL DEFAULT '0',
  `stat_value10` smallint(6) NOT NULL DEFAULT '0',
  `scaling_stat_distribution` int(11) DEFAULT NULL COMMENT 'wotlk+',
  `scaling_stat_value` int(10) unsigned DEFAULT NULL COMMENT 'wotlk+',
  `delay` smallint(5) unsigned NOT NULL DEFAULT '1000',
  `range_mod` float DEFAULT NULL COMMENT '1.10+',
  `ammo_type` tinyint(3) unsigned NOT NULL DEFAULT '0',
  `dmg_min1` float NOT NULL DEFAULT '0',
  `dmg_max1` float NOT NULL DEFAULT '0',
  `dmg_type1` tinyint(3) unsigned NOT NULL DEFAULT '0',
  `dmg_min2` float NOT NULL DEFAULT '0',
  `dmg_max2` float NOT NULL DEFAULT '0',
  `dmg_type2` tinyint(3) unsigned NOT NULL DEFAULT '0',
  `dmg_min3` float NOT NULL DEFAULT '0',
  `dmg_max3` float NOT NULL DEFAULT '0',
  `dmg_type3` tinyint(3) unsigned NOT NULL DEFAULT '0',
  `dmg_min4` float NOT NULL DEFAULT '0',
  `dmg_max4` float NOT NULL DEFAULT '0',
  `dmg_type4` tinyint(3) unsigned NOT NULL DEFAULT '0',
  `dmg_min5` float NOT NULL DEFAULT '0',
  `dmg_max5` float NOT NULL DEFAULT '0',
  `dmg_type5` tinyint(3) unsigned NOT NULL DEFAULT '0',
  `block` mediumint(8) unsigned NOT NULL DEFAULT '0',
  `armor` smallint(5) NOT NULL DEFAULT '0',
  `holy_res` smallint(5) NOT NULL DEFAULT '0',
  `fire_res` smallint(5) NOT NULL DEFAULT '0',
  `nature_res` smallint(5) NOT NULL DEFAULT '0',
  `frost_res` smallint(5) NOT NULL DEFAULT '0',
  `shadow_res` smallint(5) NOT NULL DEFAULT '0',
  `arcane_res` smallint(5) NOT NULL DEFAULT '0',
  `spellid_1` smallint(5) unsigned NOT NULL DEFAULT '0',
  `spelltrigger_1` tinyint(3) unsigned NOT NULL DEFAULT '0',
  `spellcharges_1` tinyint(4) NOT NULL DEFAULT '0',
  `spellcooldown_1` int(11) NOT NULL DEFAULT '-1',
  `spellcategory_1` smallint(5) unsigned NOT NULL DEFAULT '0',
  `spellcategorycooldown_1` int(11) NOT NULL DEFAULT '-1',
  `spellid_2` smallint(5) unsigned NOT NULL DEFAULT '0',
  `spelltrigger_2` tinyint(3) unsigned NOT NULL DEFAULT '0',
  `spellcharges_2` tinyint(4) NOT NULL DEFAULT '0',
  `spellcooldown_2` int(11) NOT NULL DEFAULT '-1',
  `spellcategory_2` smallint(5) unsigned NOT NULL DEFAULT '0',
  `spellcategorycooldown_2` int(11) NOT NULL DEFAULT '-1',
  `spellid_3` smallint(5) unsigned NOT NULL DEFAULT '0',
  `spelltrigger_3` tinyint(3) unsigned NOT NULL DEFAULT '0',
  `spellcharges_3` tinyint(4) NOT NULL DEFAULT '0',
  `spellcooldown_3` int(11) NOT NULL DEFAULT '-1',
  `spellcategory_3` smallint(5) unsigned NOT NULL DEFAULT '0',
  `spellcategorycooldown_3` int(11) NOT NULL DEFAULT '-1',
  `spellid_4` smallint(5) unsigned NOT NULL DEFAULT '0',
  `spelltrigger_4` tinyint(3) unsigned NOT NULL DEFAULT '0',
  `spellcharges_4` tinyint(4) NOT NULL DEFAULT '0',
  `spellcooldown_4` int(11) NOT NULL DEFAULT '-1',
  `spellcategory_4` smallint(5) unsigned NOT NULL DEFAULT '0',
  `spellcategorycooldown_4` int(11) NOT NULL DEFAULT '-1',
  `spellid_5` smallint(5) unsigned NOT NULL DEFAULT '0',
  `spelltrigger_5` tinyint(3) unsigned NOT NULL DEFAULT '0',
  `spellcharges_5` tinyint(4) NOT NULL DEFAULT '0',
  `spellcooldown_5` int(11) NOT NULL DEFAULT '-1',
  `spellcategory_5` smallint(5) unsigned NOT NULL DEFAULT '0',
  `spellcategorycooldown_5` int(11) NOT NULL DEFAULT '-1',
  `bonding` tinyint(3) unsigned NOT NULL DEFAULT '0',
  `page_text` mediumint(8) unsigned NOT NULL DEFAULT '0',
  `page_language` tinyint(3) unsigned NOT NULL DEFAULT '0',
  `page_material` tinyint(3) unsigned NOT NULL DEFAULT '0',
  `start_quest` mediumint(8) unsigned NOT NULL DEFAULT '0',
  `lock_id` mediumint(8) unsigned NOT NULL DEFAULT '0',
  `material` tinyint(4) NOT NULL DEFAULT '0',
  `sheath` tinyint(3) unsigned NOT NULL DEFAULT '0',
  `random_property` mediumint(8) unsigned NOT NULL DEFAULT '0',
  `random_suffix` mediumint(8) unsigned DEFAULT NULL COMMENT 'tbc+',
  `set_id` mediumint(8) unsigned NOT NULL DEFAULT '0',
  `max_durability` smallint(5) unsigned NOT NULL DEFAULT '0',
  `area_bound` mediumint(8) unsigned NOT NULL DEFAULT '0',
  `map_bound` smallint(6) DEFAULT NULL COMMENT '1.12+',
  `duration` int(11) unsigned DEFAULT NULL COMMENT 'tbc+',
  `bag_family` mediumint(9) DEFAULT NULL COMMENT '1.9+',
  `totem_category` tinyint(3) unsigned DEFAULT NULL COMMENT 'tbc+',
  `socket_color_1` tinyint(3) unsigned DEFAULT NULL COMMENT 'tbc+',
  `socket_color_2` tinyint(3) unsigned DEFAULT NULL COMMENT 'tbc+',
  `socket_color_3` tinyint(3) unsigned DEFAULT NULL COMMENT 'tbc+',
  `socket_content_1` int(10) unsigned DEFAULT NULL COMMENT 'tbc+',
  `socket_content_2` int(10) unsigned DEFAULT NULL COMMENT 'tbc+',
  `socket_content_3` int(10) unsigned DEFAULT NULL COMMENT 'tbc+',
  `socket_bonus` int(11) DEFAULT NULL COMMENT 'tbc+',
  `gem_properties` int(11) DEFAULT NULL COMMENT 'tbc+',
  `required_disenchant_skill` int(11) DEFAULT NULL COMMENT 'tbc+',
  `armor_damage_modifier` float DEFAULT NULL COMMENT 'tbc+',
  `item_limit_category` int(11) DEFAULT NULL COMMENT 'wotlk+',
  `holiday_id` smallint(5) unsigned DEFAULT NULL COMMENT 'wotlk+',
  `sniff_build` int(10) unsigned NOT NULL DEFAULT '0',
  PRIMARY KEY (`entry`),
  KEY `items_index` (`class`)
) ENGINE=MyISAM DEFAULT CHARSET=utf8 ROW_FORMAT=FIXED COMMENT='Item System';

-- Data exporting was unselected.


-- Dumping structure for table sniffs_new_test.logout_time
DROP TABLE IF EXISTS `logout_time`;
CREATE TABLE IF NOT EXISTS `logout_time` (
  `unixtimems` bigint(20) unsigned NOT NULL COMMENT 'when the packet was received',
  PRIMARY KEY (`unixtimems`)
) ENGINE=MyISAM DEFAULT CHARSET=latin1 COLLATE=latin1_general_ci COMMENT='from SMSG_LOGOUT_COMPLETE';

-- Data exporting was unselected.


-- Dumping structure for table sniffs_new_test.mail_template
DROP TABLE IF EXISTS `mail_template`;
CREATE TABLE IF NOT EXISTS `mail_template` (
  `entry` int(10) unsigned NOT NULL DEFAULT '0',
  `sniff_build` mediumint(8) unsigned NOT NULL DEFAULT '0',
  `stationery_id` int(10) unsigned NOT NULL DEFAULT '0',
  `sender_id` int(10) unsigned NOT NULL DEFAULT '0',
  `sender_type` tinyint(3) unsigned NOT NULL DEFAULT '0',
  `money` bigint(20) unsigned NOT NULL DEFAULT '0',
  `items_count` tinyint(3) unsigned NOT NULL DEFAULT '0',
  `subject` text COLLATE utf8_unicode_ci NOT NULL,
  `body` text COLLATE utf8_unicode_ci NOT NULL,
  PRIMARY KEY (`entry`,`sniff_build`)
) ENGINE=MyISAM DEFAULT CHARSET=utf8 COLLATE=utf8_unicode_ci COMMENT='data for mail sent by creatures\r\nfrom SMSG_MAIL_LIST_RESULT';

-- Data exporting was unselected.


-- Dumping structure for table sniffs_new_test.mail_template_item
DROP TABLE IF EXISTS `mail_template_item`;
CREATE TABLE IF NOT EXISTS `mail_template_item` (
  `entry` int(10) unsigned NOT NULL DEFAULT '0',
  `slot` tinyint(3) unsigned NOT NULL DEFAULT '0',
  `item_id` int(10) unsigned NOT NULL DEFAULT '0',
  `count` tinyint(3) unsigned NOT NULL DEFAULT '0',
  `sniff_build` mediumint(8) unsigned NOT NULL DEFAULT '0',
  PRIMARY KEY (`entry`,`slot`,`item_id`,`count`)
) ENGINE=MyISAM DEFAULT CHARSET=utf8 COLLATE=utf8_unicode_ci COMMENT='items attached to mail templates\r\nfrom SMSG_MAIL_LIST_RESULT';

-- Data exporting was unselected.


-- Dumping structure for table sniffs_new_test.npc_text
DROP TABLE IF EXISTS `npc_text`;
CREATE TABLE IF NOT EXISTS `npc_text` (
  `entry` mediumint(8) unsigned NOT NULL DEFAULT '0',
  `sniff_build` mediumint(8) unsigned NOT NULL DEFAULT '0',
  `probability1` float NOT NULL DEFAULT '0',
  `probability2` float NOT NULL DEFAULT '0',
  `probability3` float NOT NULL DEFAULT '0',
  `probability4` float NOT NULL DEFAULT '0',
  `probability5` float NOT NULL DEFAULT '0',
  `probability6` float NOT NULL DEFAULT '0',
  `probability7` float NOT NULL DEFAULT '0',
  `probability8` float NOT NULL DEFAULT '0',
  `broadcast_text_id1` mediumint(6) unsigned NOT NULL DEFAULT '0',
  `broadcast_text_id2` mediumint(6) unsigned NOT NULL DEFAULT '0',
  `broadcast_text_id3` mediumint(6) unsigned NOT NULL DEFAULT '0',
  `broadcast_text_id4` mediumint(6) unsigned NOT NULL DEFAULT '0',
  `broadcast_text_id5` mediumint(6) unsigned NOT NULL DEFAULT '0',
  `broadcast_text_id6` mediumint(6) unsigned NOT NULL DEFAULT '0',
  `broadcast_text_id7` mediumint(6) unsigned NOT NULL DEFAULT '0',
  `broadcast_text_id8` mediumint(6) unsigned NOT NULL DEFAULT '0',
  PRIMARY KEY (`entry`,`sniff_build`)
) ENGINE=MyISAM DEFAULT CHARSET=latin1 COLLATE=latin1_general_ci;

-- Data exporting was unselected.


-- Dumping structure for table sniffs_new_test.npc_text_old
DROP TABLE IF EXISTS `npc_text_old`;
CREATE TABLE IF NOT EXISTS `npc_text_old` (
  `entry` mediumint(8) unsigned NOT NULL DEFAULT '0',
  `sniff_build` mediumint(8) unsigned NOT NULL DEFAULT '0',
  `male_text1` varchar(512) COLLATE latin1_general_ci NOT NULL DEFAULT '',
  `male_text2` varchar(512) COLLATE latin1_general_ci NOT NULL DEFAULT '',
  `male_text3` varchar(512) COLLATE latin1_general_ci NOT NULL DEFAULT '',
  `male_text4` varchar(512) COLLATE latin1_general_ci NOT NULL DEFAULT '',
  `male_text5` varchar(512) COLLATE latin1_general_ci NOT NULL DEFAULT '',
  `male_text6` varchar(512) COLLATE latin1_general_ci NOT NULL DEFAULT '',
  `male_text7` varchar(512) COLLATE latin1_general_ci NOT NULL DEFAULT '',
  `male_text8` varchar(512) COLLATE latin1_general_ci NOT NULL DEFAULT '',
  `female_text1` varchar(512) COLLATE latin1_general_ci NOT NULL DEFAULT '',
  `female_text2` varchar(512) COLLATE latin1_general_ci NOT NULL DEFAULT '',
  `female_text3` varchar(512) COLLATE latin1_general_ci NOT NULL DEFAULT '',
  `female_text4` varchar(512) COLLATE latin1_general_ci NOT NULL DEFAULT '',
  `female_text5` varchar(512) COLLATE latin1_general_ci NOT NULL DEFAULT '',
  `female_text6` varchar(512) COLLATE latin1_general_ci NOT NULL DEFAULT '',
  `female_text7` varchar(512) COLLATE latin1_general_ci NOT NULL DEFAULT '',
  `female_text8` varchar(512) COLLATE latin1_general_ci NOT NULL DEFAULT '',
  `language_id1` mediumint(6) unsigned NOT NULL DEFAULT '0',
  `language_id2` mediumint(6) unsigned NOT NULL DEFAULT '0',
  `language_id3` mediumint(6) unsigned NOT NULL DEFAULT '0',
  `language_id4` mediumint(6) unsigned NOT NULL DEFAULT '0',
  `language_id5` mediumint(6) unsigned NOT NULL DEFAULT '0',
  `language_id6` mediumint(6) unsigned NOT NULL DEFAULT '0',
  `language_id7` mediumint(6) unsigned NOT NULL DEFAULT '0',
  `language_id8` mediumint(6) unsigned NOT NULL DEFAULT '0',
  `emote_delay1_0` mediumint(9) NOT NULL DEFAULT '0',
  `emote_delay2_0` mediumint(9) NOT NULL DEFAULT '0',
  `emote_delay3_0` mediumint(9) NOT NULL DEFAULT '0',
  `emote_delay4_0` mediumint(9) NOT NULL DEFAULT '0',
  `emote_delay5_0` mediumint(9) NOT NULL DEFAULT '0',
  `emote_delay6_0` mediumint(9) NOT NULL DEFAULT '0',
  `emote_delay7_0` mediumint(9) NOT NULL DEFAULT '0',
  `emote_delay8_0` mediumint(9) NOT NULL DEFAULT '0',
  `emote_delay1_1` mediumint(9) NOT NULL DEFAULT '0',
  `emote_delay2_1` mediumint(9) NOT NULL DEFAULT '0',
  `emote_delay3_1` mediumint(9) NOT NULL DEFAULT '0',
  `emote_delay4_1` mediumint(9) NOT NULL DEFAULT '0',
  `emote_delay5_1` mediumint(9) NOT NULL DEFAULT '0',
  `emote_delay6_1` mediumint(9) NOT NULL DEFAULT '0',
  `emote_delay7_1` mediumint(9) NOT NULL DEFAULT '0',
  `emote_delay8_1` mediumint(9) NOT NULL DEFAULT '0',
  `emote_delay1_2` mediumint(9) NOT NULL DEFAULT '0',
  `emote_delay2_2` mediumint(9) NOT NULL DEFAULT '0',
  `emote_delay3_2` mediumint(9) NOT NULL DEFAULT '0',
  `emote_delay4_2` mediumint(9) NOT NULL DEFAULT '0',
  `emote_delay5_2` mediumint(9) NOT NULL DEFAULT '0',
  `emote_delay6_2` mediumint(9) NOT NULL DEFAULT '0',
  `emote_delay7_2` mediumint(9) NOT NULL DEFAULT '0',
  `emote_delay8_2` mediumint(9) NOT NULL DEFAULT '0',
  `emote1_0` mediumint(9) NOT NULL DEFAULT '0',
  `emote2_0` mediumint(9) NOT NULL DEFAULT '0',
  `emote3_0` mediumint(9) NOT NULL DEFAULT '0',
  `emote4_0` mediumint(9) NOT NULL DEFAULT '0',
  `emote5_0` mediumint(9) NOT NULL DEFAULT '0',
  `emote6_0` mediumint(9) NOT NULL DEFAULT '0',
  `emote7_0` mediumint(9) NOT NULL DEFAULT '0',
  `emote8_0` mediumint(9) NOT NULL DEFAULT '0',
  `emote1_1` mediumint(9) NOT NULL DEFAULT '0',
  `emote2_1` mediumint(9) NOT NULL DEFAULT '0',
  `emote3_1` mediumint(9) NOT NULL DEFAULT '0',
  `emote4_1` mediumint(9) NOT NULL DEFAULT '0',
  `emote5_1` mediumint(9) NOT NULL DEFAULT '0',
  `emote6_1` mediumint(9) NOT NULL DEFAULT '0',
  `emote7_1` mediumint(9) NOT NULL DEFAULT '0',
  `emote8_1` mediumint(9) NOT NULL DEFAULT '0',
  `emote1_2` mediumint(9) NOT NULL DEFAULT '0',
  `emote2_2` mediumint(9) NOT NULL DEFAULT '0',
  `emote3_2` mediumint(9) NOT NULL DEFAULT '0',
  `emote4_2` mediumint(9) NOT NULL DEFAULT '0',
  `emote5_2` mediumint(9) NOT NULL DEFAULT '0',
  `emote6_2` mediumint(9) NOT NULL DEFAULT '0',
  `emote7_2` mediumint(9) NOT NULL DEFAULT '0',
  `emote8_2` mediumint(9) NOT NULL DEFAULT '0',
  `probability1` float NOT NULL DEFAULT '0',
  `probability2` float NOT NULL DEFAULT '0',
  `probability3` float NOT NULL DEFAULT '0',
  `probability4` float NOT NULL DEFAULT '0',
  `probability5` float NOT NULL DEFAULT '0',
  `probability6` float NOT NULL DEFAULT '0',
  `probability7` float NOT NULL DEFAULT '0',
  `probability8` float NOT NULL DEFAULT '0',
  PRIMARY KEY (`entry`,`sniff_build`)
) ENGINE=MyISAM DEFAULT CHARSET=latin1 COLLATE=latin1_general_ci ROW_FORMAT=FIXED;

-- Data exporting was unselected.


-- Dumping structure for table sniffs_new_test.npc_trainer
DROP TABLE IF EXISTS `npc_trainer`;
CREATE TABLE IF NOT EXISTS `npc_trainer` (
  `entry` mediumint(8) unsigned NOT NULL DEFAULT '0',
  `spell_id` mediumint(8) unsigned NOT NULL DEFAULT '0',
  `money_cost` int(10) unsigned NOT NULL DEFAULT '0' COMMENT 'could have reputation discount factored in',
  `required_skill_id` smallint(5) unsigned NOT NULL DEFAULT '0',
  `required_skill_value` smallint(5) unsigned NOT NULL DEFAULT '0',
  `required_level` tinyint(3) unsigned NOT NULL DEFAULT '0',
  `sniff_build` mediumint(8) unsigned NOT NULL DEFAULT '0',
  PRIMARY KEY (`entry`,`spell_id`,`money_cost`)
) ENGINE=MyISAM DEFAULT CHARSET=latin1 COLLATE=latin1_general_ci;

-- Data exporting was unselected.


-- Dumping structure for table sniffs_new_test.npc_vendor
DROP TABLE IF EXISTS `npc_vendor`;
CREATE TABLE IF NOT EXISTS `npc_vendor` (
  `entry` mediumint(8) unsigned NOT NULL DEFAULT '0',
  `slot` smallint(6) NOT NULL DEFAULT '0',
  `item` mediumint(8) NOT NULL DEFAULT '0',
  `maxcount` mediumint(8) unsigned NOT NULL DEFAULT '0',
  `extended_cost` mediumint(8) unsigned NOT NULL DEFAULT '0',
  `type` tinyint(3) unsigned NOT NULL DEFAULT '1',
  `player_condition_id` int(10) unsigned NOT NULL DEFAULT '0',
  `ignore_filtering` tinyint(3) unsigned NOT NULL DEFAULT '0',
  `sniff_id_list` text COLLATE latin1_general_ci NOT NULL,
  PRIMARY KEY (`entry`,`item`,`extended_cost`,`type`,`slot`,`maxcount`)
) ENGINE=MyISAM DEFAULT CHARSET=latin1 COLLATE=latin1_general_ci ROW_FORMAT=FIXED COMMENT='Npc System';

-- Data exporting was unselected.


-- Dumping structure for table sniffs_new_test.object_names
DROP TABLE IF EXISTS `object_names`;
CREATE TABLE IF NOT EXISTS `object_names` (
  `object_type` enum('None','Spell','Map','LFGDungeon','Battleground','Unit','GameObject','CreatureDifficulty','Item','Quest','Opcode','PageText','NpcText','BroadcastText','Gossip','Zone','Area','AreaTrigger','Phase','Player','Achievement') NOT NULL DEFAULT 'None',
  `id` int(10) NOT NULL,
  `name` text NOT NULL,
  PRIMARY KEY (`object_type`,`id`)
) ENGINE=MyISAM DEFAULT CHARSET=utf8 COMMENT='WPP''s ObjectTypes Names DataBase';

-- Data exporting was unselected.


-- Dumping structure for table sniffs_new_test.page_text
DROP TABLE IF EXISTS `page_text`;
CREATE TABLE IF NOT EXISTS `page_text` (
  `entry` mediumint(8) unsigned NOT NULL DEFAULT '0',
  `sniff_build` mediumint(8) unsigned NOT NULL DEFAULT '0',
  `text` longtext,
  `next_page` mediumint(8) unsigned NOT NULL DEFAULT '0',
  `player_condition_id` int(11) NOT NULL DEFAULT '0',
  `flags` tinyint(3) unsigned NOT NULL DEFAULT '0',
  PRIMARY KEY (`entry`,`sniff_build`)
) ENGINE=MyISAM DEFAULT CHARSET=utf8 COMMENT='Item System';

-- Data exporting was unselected.


-- Dumping structure for table sniffs_new_test.page_text_locale
DROP TABLE IF EXISTS `page_text_locale`;
CREATE TABLE IF NOT EXISTS `page_text_locale` (
  `entry` mediumint(8) unsigned NOT NULL DEFAULT '0',
  `locale` varchar(4) NOT NULL,
  `text` text,
  `sniff_build` mediumint(8) unsigned NOT NULL DEFAULT '0',
  PRIMARY KEY (`entry`,`locale`)
) ENGINE=MyISAM DEFAULT CHARSET=utf8;

-- Data exporting was unselected.


-- Dumping structure for table sniffs_new_test.player
DROP TABLE IF EXISTS `player`;
CREATE TABLE IF NOT EXISTS `player` (
  `guid` int(11) unsigned NOT NULL DEFAULT '0' COMMENT 'Global Unique Identifier',
  `map` smallint(5) unsigned DEFAULT '0' COMMENT 'Map Identifier',
  `zone_id` smallint(5) unsigned NOT NULL DEFAULT '0' COMMENT 'Zone Identifier',
  `area_id` smallint(5) unsigned NOT NULL DEFAULT '0' COMMENT 'Area Identifier',
  `position_x` float NOT NULL DEFAULT '0',
  `position_y` float NOT NULL DEFAULT '0',
  `position_z` float NOT NULL DEFAULT '0',
  `orientation` float NOT NULL DEFAULT '0',
  `name` varchar(12) NOT NULL DEFAULT '',
  `race` tinyint(3) unsigned NOT NULL DEFAULT '0',
  `class` tinyint(3) unsigned NOT NULL DEFAULT '0',
  `gender` tinyint(3) unsigned NOT NULL DEFAULT '0',
  `level` tinyint(3) unsigned NOT NULL DEFAULT '0',
  `skin` tinyint(3) unsigned NOT NULL DEFAULT '0',
  `face` tinyint(3) unsigned NOT NULL DEFAULT '0',
  `hair_style` tinyint(3) unsigned NOT NULL DEFAULT '0',
  `hair_color` tinyint(3) unsigned NOT NULL DEFAULT '0',
  `facial_hair` tinyint(3) unsigned NOT NULL DEFAULT '0',
  `player_flags` int(10) unsigned NOT NULL DEFAULT '0',
  `pvp_rank` tinyint(3) unsigned NOT NULL DEFAULT '0',
  `scale` float NOT NULL DEFAULT '0',
  `display_id` int(10) unsigned NOT NULL DEFAULT '0',
  `native_display_id` int(10) unsigned NOT NULL DEFAULT '0',
  `mount_display_id` int(10) unsigned NOT NULL DEFAULT '0',
  `faction` int(10) unsigned NOT NULL DEFAULT '0',
  `unit_flags` int(10) unsigned NOT NULL DEFAULT '0',
  `unit_flags2` int(10) unsigned NOT NULL DEFAULT '0',
  `current_health` int(10) unsigned NOT NULL DEFAULT '0',
  `max_health` int(10) unsigned NOT NULL DEFAULT '0',
  `power_type` tinyint(3) unsigned NOT NULL DEFAULT '0',
  `current_power` int(10) unsigned NOT NULL DEFAULT '0',
  `max_power` int(10) unsigned NOT NULL DEFAULT '0',
  `aura_state` int(10) unsigned NOT NULL DEFAULT '0',
  `emote_state` int(10) unsigned NOT NULL DEFAULT '0',
  `stand_state` tinyint(3) unsigned NOT NULL DEFAULT '0' COMMENT 'from UNIT_FIELD_BYTES_1',
  `vis_flags` tinyint(3) unsigned NOT NULL DEFAULT '0' COMMENT 'from UNIT_FIELD_BYTES_1',
  `anim_tier` tinyint(3) unsigned NOT NULL DEFAULT '0' COMMENT 'from UNIT_FIELD_BYTES_1',
  `sheath_state` tinyint(3) unsigned NOT NULL DEFAULT '0' COMMENT 'from UNIT_FIELD_BYTES_2',
  `misc_flags` tinyint(3) unsigned NOT NULL DEFAULT '0' COMMENT 'from UNIT_FIELD_BYTES_2, only in vanilla',
  `debuff_limit` tinyint(3) unsigned NOT NULL DEFAULT '0' COMMENT 'from UNIT_FIELD_BYTES_2, only in tbc',
  `pvp_flags` tinyint(3) unsigned NOT NULL DEFAULT '0' COMMENT 'from UNIT_FIELD_BYTES_2, wotlk+',
  `shapeshift_form` tinyint(3) unsigned NOT NULL DEFAULT '0' COMMENT 'from UNIT_FIELD_BYTES_2',
  `speed_walk` float NOT NULL DEFAULT '0',
  `speed_run` float NOT NULL DEFAULT '0',
  `speed_run_back` float NOT NULL DEFAULT '0',
  `speed_swim` float NOT NULL DEFAULT '0',
  `speed_swim_back` float NOT NULL DEFAULT '0',
  `speed_fly` float NOT NULL DEFAULT '0',
  `speed_fly_back` float NOT NULL DEFAULT '0',
  `bounding_radius` float NOT NULL DEFAULT '0',
  `combat_reach` float NOT NULL DEFAULT '0',
  `mod_melee_haste` float NOT NULL DEFAULT '0',
  `main_hand_attack_time` int(10) unsigned NOT NULL DEFAULT '0',
  `off_hand_attack_time` int(10) unsigned NOT NULL DEFAULT '0',
  `ranged_attack_time` int(10) unsigned NOT NULL DEFAULT '0',
  `channel_spell_id` int(10) unsigned NOT NULL DEFAULT '0',
  `channel_visual_id` int(10) unsigned NOT NULL DEFAULT '0',
  `equipment_cache` text,
  `auras` text,
  PRIMARY KEY (`guid`)
) ENGINE=MyISAM DEFAULT CHARSET=utf8 ROW_FORMAT=DYNAMIC COMMENT='the initial state of all players seen in the sniff';

-- Data exporting was unselected.


-- Dumping structure for table sniffs_new_test.playercreateinfo
DROP TABLE IF EXISTS `playercreateinfo`;
CREATE TABLE IF NOT EXISTS `playercreateinfo` (
  `race` tinyint(3) unsigned NOT NULL DEFAULT '0',
  `class` tinyint(3) unsigned NOT NULL DEFAULT '0',
  `map` smallint(5) unsigned NOT NULL DEFAULT '0',
  `zone` mediumint(8) unsigned NOT NULL DEFAULT '0',
  `position_x` float NOT NULL DEFAULT '0',
  `position_y` float NOT NULL DEFAULT '0',
  `position_z` float NOT NULL DEFAULT '0',
  `orientation` float NOT NULL DEFAULT '0',
  PRIMARY KEY (`race`,`class`)
) ENGINE=MyISAM DEFAULT CHARSET=latin1 COLLATE=latin1_general_ci;

-- Data exporting was unselected.


-- Dumping structure for table sniffs_new_test.playercreateinfo_action
DROP TABLE IF EXISTS `playercreateinfo_action`;
CREATE TABLE IF NOT EXISTS `playercreateinfo_action` (
  `race` tinyint(3) unsigned NOT NULL DEFAULT '0',
  `class` tinyint(3) unsigned NOT NULL DEFAULT '0',
  `button` smallint(5) unsigned NOT NULL DEFAULT '0',
  `action` int(10) unsigned NOT NULL DEFAULT '0',
  `type` smallint(5) unsigned NOT NULL DEFAULT '0',
  PRIMARY KEY (`race`,`class`,`button`),
  KEY `playercreateinfo_race_class_index` (`race`,`class`)
) ENGINE=MyISAM DEFAULT CHARSET=latin1 COLLATE=latin1_general_ci;

-- Data exporting was unselected.


-- Dumping structure for table sniffs_new_test.player_active_player
DROP TABLE IF EXISTS `player_active_player`;
CREATE TABLE IF NOT EXISTS `player_active_player` (
  `unixtime` int(10) unsigned NOT NULL,
  `guid` int(10) unsigned NOT NULL,
  PRIMARY KEY (`guid`,`unixtime`)
) ENGINE=MyISAM DEFAULT CHARSET=latin1 COLLATE=latin1_general_ci COMMENT='shows which character was controlled by the client at a given time';

-- Data exporting was unselected.


-- Dumping structure for table sniffs_new_test.player_attack_log
DROP TABLE IF EXISTS `player_attack_log`;
CREATE TABLE IF NOT EXISTS `player_attack_log` (
  `unixtimems` bigint(20) unsigned NOT NULL DEFAULT '0',
  `guid` int(10) unsigned NOT NULL,
  `victim_guid` int(10) unsigned NOT NULL DEFAULT '0',
  `victim_id` int(10) unsigned NOT NULL DEFAULT '0',
  `victim_type` varchar(16) COLLATE latin1_general_ci NOT NULL DEFAULT '',
  `hit_info` int(10) unsigned NOT NULL DEFAULT '0',
  `damage` int(10) NOT NULL DEFAULT '0',
  `original_damage` int(10) NOT NULL DEFAULT '0',
  `overkill_damage` int(10) NOT NULL DEFAULT '0',
  `sub_damage_count` tinyint(3) unsigned NOT NULL DEFAULT '0',
  `total_school_mask` int(10) unsigned NOT NULL DEFAULT '0',
  `total_absorbed_damage` int(11) NOT NULL DEFAULT '0',
  `total_resisted_damage` int(11) NOT NULL DEFAULT '0',
  `blocked_damage` int(10) NOT NULL DEFAULT '0',
  `victim_state` int(10) unsigned NOT NULL DEFAULT '0',
  `attacker_state` int(10) NOT NULL DEFAULT '0',
  `spell_id` int(10) unsigned NOT NULL DEFAULT '0',
  PRIMARY KEY (`guid`,`unixtimems`)
) ENGINE=MyISAM DEFAULT CHARSET=latin1 COLLATE=latin1_general_ci ROW_FORMAT=COMPACT COMMENT='from SMSG_ATTACKER_STATE_UPDATE';

-- Data exporting was unselected.


-- Dumping structure for table sniffs_new_test.player_attack_start
DROP TABLE IF EXISTS `player_attack_start`;
CREATE TABLE IF NOT EXISTS `player_attack_start` (
  `unixtimems` bigint(20) unsigned NOT NULL DEFAULT '0',
  `guid` int(10) unsigned NOT NULL,
  `victim_guid` int(10) unsigned NOT NULL DEFAULT '0',
  `victim_id` int(10) unsigned NOT NULL DEFAULT '0',
  `victim_type` varchar(16) COLLATE latin1_general_ci NOT NULL DEFAULT '',
  PRIMARY KEY (`guid`,`unixtimems`)
) ENGINE=MyISAM DEFAULT CHARSET=latin1 COLLATE=latin1_general_ci ROW_FORMAT=COMPACT COMMENT='from SMSG_ATTACK_START';

-- Data exporting was unselected.


-- Dumping structure for table sniffs_new_test.player_attack_stop
DROP TABLE IF EXISTS `player_attack_stop`;
CREATE TABLE IF NOT EXISTS `player_attack_stop` (
  `unixtimems` bigint(20) unsigned NOT NULL DEFAULT '0',
  `guid` int(10) unsigned NOT NULL,
  `victim_guid` int(10) unsigned NOT NULL DEFAULT '0',
  `victim_id` int(10) unsigned NOT NULL DEFAULT '0',
  `victim_type` varchar(16) COLLATE latin1_general_ci NOT NULL DEFAULT '',
  PRIMARY KEY (`guid`,`unixtimems`)
) ENGINE=MyISAM DEFAULT CHARSET=latin1 COLLATE=latin1_general_ci ROW_FORMAT=COMPACT COMMENT='from SMSG_ATTACK_STOP';

-- Data exporting was unselected.


-- Dumping structure for table sniffs_new_test.player_auras_update
DROP TABLE IF EXISTS `player_auras_update`;
CREATE TABLE IF NOT EXISTS `player_auras_update` (
  `unixtimems` bigint(20) unsigned NOT NULL COMMENT 'when the packet was received',
  `guid` int(10) unsigned NOT NULL,
  `update_id` int(10) unsigned NOT NULL COMMENT 'counting aura update packets for this guid',
  `is_full_update` tinyint(3) unsigned NOT NULL,
  `slot` smallint(6) NOT NULL,
  `spell_id` int(10) unsigned NOT NULL,
  `visual_id` int(10) unsigned NOT NULL,
  `aura_flags` int(10) unsigned NOT NULL,
  `active_flags` int(10) unsigned NOT NULL,
  `level` int(10) unsigned NOT NULL,
  `charges` int(10) unsigned NOT NULL,
  `duration` int(10) NOT NULL,
  `max_duration` int(10) NOT NULL,
  `caster_guid` int(10) unsigned NOT NULL,
  `caster_id` int(10) unsigned NOT NULL,
  `caster_type` varchar(16) COLLATE latin1_general_ci NOT NULL,
  PRIMARY KEY (`guid`,`update_id`,`slot`)
) ENGINE=MyISAM DEFAULT CHARSET=latin1 COLLATE=latin1_general_ci ROW_FORMAT=COMPACT COMMENT='aura updates from SMSG_AURA_UPDATE';

-- Data exporting was unselected.


-- Dumping structure for table sniffs_new_test.player_chat
DROP TABLE IF EXISTS `player_chat`;
CREATE TABLE IF NOT EXISTS `player_chat` (
  `unixtimems` bigint(20) unsigned NOT NULL DEFAULT '0',
  `guid` int(10) unsigned NOT NULL DEFAULT '0',
  `sender_name` varchar(12) NOT NULL DEFAULT '',
  `text` longtext,
  `chat_type` tinyint(3) unsigned NOT NULL DEFAULT '0',
  `channel_name` varchar(255) DEFAULT '',
  PRIMARY KEY (`guid`,`sender_name`,`unixtimems`)
) ENGINE=MyISAM DEFAULT CHARSET=utf8 ROW_FORMAT=DYNAMIC COMMENT='player chat packets';

-- Data exporting was unselected.


-- Dumping structure for table sniffs_new_test.player_classlevelstats
DROP TABLE IF EXISTS `player_classlevelstats`;
CREATE TABLE IF NOT EXISTS `player_classlevelstats` (
  `class` tinyint(3) unsigned NOT NULL,
  `level` tinyint(3) unsigned NOT NULL,
  `base_health` smallint(5) unsigned NOT NULL,
  `base_mana` smallint(5) unsigned NOT NULL,
  `sniff_id_list` text COLLATE latin1_general_ci NOT NULL,
  PRIMARY KEY (`class`,`level`,`base_health`,`base_mana`)
) ENGINE=MyISAM DEFAULT CHARSET=latin1 COLLATE=latin1_general_ci PACK_KEYS=0 COMMENT='Stores levels stats.';

-- Data exporting was unselected.


-- Dumping structure for table sniffs_new_test.player_create1_time
DROP TABLE IF EXISTS `player_create1_time`;
CREATE TABLE IF NOT EXISTS `player_create1_time` (
  `unixtimems` bigint(20) unsigned NOT NULL COMMENT 'when the packet was received',
  `guid` int(10) unsigned NOT NULL,
  `map` smallint(5) unsigned NOT NULL,
  `position_x` float NOT NULL,
  `position_y` float NOT NULL,
  `position_z` float NOT NULL,
  `orientation` float NOT NULL,
  `move_time` int(10) unsigned NOT NULL DEFAULT '0',
  `move_flags` int(10) unsigned NOT NULL DEFAULT '0',
  `move_flags2` int(10) unsigned NOT NULL DEFAULT '0',
  `swim_pitch` float NOT NULL DEFAULT '0',
  `fall_time` int(10) unsigned NOT NULL DEFAULT '0',
  `jump_horizontal_speed` float NOT NULL DEFAULT '0',
  `jump_vertical_speed` float NOT NULL DEFAULT '0',
  `jump_cos_angle` float NOT NULL DEFAULT '0',
  `jump_sin_angle` float NOT NULL DEFAULT '0',
  `spline_elevation` float NOT NULL DEFAULT '0',
  `vehicle_id` int(10) unsigned NOT NULL DEFAULT '0',
  `vehicle_orientation` float NOT NULL DEFAULT '0',
  `transport_guid` int(10) unsigned NOT NULL DEFAULT '0',
  `transport_id` int(10) unsigned NOT NULL DEFAULT '0',
  `transport_type` varchar(16) COLLATE latin1_general_ci NOT NULL DEFAULT '',
  `transport_x` float NOT NULL DEFAULT '0',
  `transport_y` float NOT NULL DEFAULT '0',
  `transport_z` float NOT NULL DEFAULT '0',
  `transport_o` float NOT NULL DEFAULT '0',
  `transport_time` int(10) unsigned NOT NULL DEFAULT '0',
  `transport_seat` tinyint(4) NOT NULL DEFAULT '0',
  PRIMARY KEY (`guid`,`unixtimems`)
) ENGINE=MyISAM DEFAULT CHARSET=latin1 COLLATE=latin1_general_ci ROW_FORMAT=COMPACT COMMENT='times when the object became visible to the client';

-- Data exporting was unselected.


-- Dumping structure for table sniffs_new_test.player_create2_time
DROP TABLE IF EXISTS `player_create2_time`;
CREATE TABLE IF NOT EXISTS `player_create2_time` (
  `unixtimems` bigint(20) unsigned NOT NULL COMMENT 'when the packet was received',
  `guid` int(10) unsigned NOT NULL,
  `map` smallint(5) unsigned NOT NULL,
  `position_x` float NOT NULL,
  `position_y` float NOT NULL,
  `position_z` float NOT NULL,
  `orientation` float NOT NULL,
  `move_time` int(10) unsigned NOT NULL DEFAULT '0',
  `move_flags` int(10) unsigned NOT NULL DEFAULT '0',
  `move_flags2` int(10) unsigned NOT NULL DEFAULT '0',
  `swim_pitch` float NOT NULL DEFAULT '0',
  `fall_time` int(10) unsigned NOT NULL DEFAULT '0',
  `jump_horizontal_speed` float NOT NULL DEFAULT '0',
  `jump_vertical_speed` float NOT NULL DEFAULT '0',
  `jump_cos_angle` float NOT NULL DEFAULT '0',
  `jump_sin_angle` float NOT NULL DEFAULT '0',
  `spline_elevation` float NOT NULL DEFAULT '0',
  `vehicle_id` int(10) unsigned NOT NULL DEFAULT '0',
  `vehicle_orientation` float NOT NULL DEFAULT '0',
  `transport_guid` int(10) unsigned NOT NULL DEFAULT '0',
  `transport_id` int(10) unsigned NOT NULL DEFAULT '0',
  `transport_type` varchar(16) COLLATE latin1_general_ci NOT NULL DEFAULT '',
  `transport_x` float NOT NULL DEFAULT '0',
  `transport_y` float NOT NULL DEFAULT '0',
  `transport_z` float NOT NULL DEFAULT '0',
  `transport_o` float NOT NULL DEFAULT '0',
  `transport_time` int(10) unsigned NOT NULL DEFAULT '0',
  `transport_seat` tinyint(4) NOT NULL DEFAULT '0',
  PRIMARY KEY (`guid`,`unixtimems`)
) ENGINE=MyISAM DEFAULT CHARSET=latin1 COLLATE=latin1_general_ci ROW_FORMAT=COMPACT COMMENT='the time at which the object spawned';

-- Data exporting was unselected.


-- Dumping structure for table sniffs_new_test.player_destroy_time
DROP TABLE IF EXISTS `player_destroy_time`;
CREATE TABLE IF NOT EXISTS `player_destroy_time` (
  `unixtimems` bigint(20) unsigned NOT NULL COMMENT 'when the packet was received',
  `guid` int(10) unsigned NOT NULL,
  PRIMARY KEY (`guid`,`unixtimems`)
) ENGINE=MyISAM DEFAULT CHARSET=latin1 COLLATE=latin1_general_ci ROW_FORMAT=COMPACT COMMENT='times when the object was destroyed from the client''s prespective due to despawning, becoming invisible, or going out of range';

-- Data exporting was unselected.


-- Dumping structure for table sniffs_new_test.player_dodge_chance
DROP TABLE IF EXISTS `player_dodge_chance`;
CREATE TABLE IF NOT EXISTS `player_dodge_chance` (
  `race` tinyint(3) unsigned NOT NULL DEFAULT '0',
  `class` tinyint(3) unsigned NOT NULL DEFAULT '0',
  `level` tinyint(3) unsigned NOT NULL DEFAULT '0',
  `agility` int(10) unsigned NOT NULL DEFAULT '0',
  `dodge_chance` float NOT NULL DEFAULT '0',
  `defense_current_value` smallint(5) unsigned NOT NULL DEFAULT '0',
  `defense_max_value` smallint(5) unsigned NOT NULL DEFAULT '0',
  `relevant_auras` varchar(128) COLLATE latin1_general_ci NOT NULL DEFAULT '' COMMENT 'auras of type 49',
  `sniff_id` smallint(5) unsigned NOT NULL DEFAULT '0',
  `sniff_build` mediumint(8) unsigned NOT NULL DEFAULT '0'
) ENGINE=MyISAM DEFAULT CHARSET=latin1 COLLATE=latin1_general_ci COMMENT='data about player dodge chance, so that we can calculate the correct bonus to dodge chance per agility in vanilla\r\nother expansions have this data in a dbc, because its shown on the user interface when you mouse over the agility stat';

-- Data exporting was unselected.


-- Dumping structure for table sniffs_new_test.player_emote
DROP TABLE IF EXISTS `player_emote`;
CREATE TABLE IF NOT EXISTS `player_emote` (
  `unixtimems` bigint(20) unsigned NOT NULL COMMENT 'when the packet was received',
  `guid` int(10) unsigned NOT NULL,
  `emote_id` int(10) unsigned NOT NULL COMMENT 'references Emotes.dbc',
  `emote_name` varchar(50) COLLATE latin1_general_ci DEFAULT NULL,
  PRIMARY KEY (`guid`,`unixtimems`,`emote_id`)
) ENGINE=MyISAM DEFAULT CHARSET=latin1 COLLATE=latin1_general_ci ROW_FORMAT=COMPACT COMMENT='from SMSG_EMOTE';

-- Data exporting was unselected.


-- Dumping structure for table sniffs_new_test.player_equipment_values_update
DROP TABLE IF EXISTS `player_equipment_values_update`;
CREATE TABLE IF NOT EXISTS `player_equipment_values_update` (
  `unixtimems` bigint(20) unsigned NOT NULL COMMENT 'when the packet was received',
  `guid` int(10) unsigned NOT NULL COMMENT 'creature spawn guid',
  `slot` tinyint(3) unsigned NOT NULL,
  `item_id` int(10) unsigned NOT NULL,
  PRIMARY KEY (`guid`,`unixtimems`,`slot`)
) ENGINE=MyISAM DEFAULT CHARSET=latin1 COLLATE=latin1_general_ci ROW_FORMAT=COMPACT COMMENT='changes to visual item slots';

-- Data exporting was unselected.


-- Dumping structure for table sniffs_new_test.player_guid_values
DROP TABLE IF EXISTS `player_guid_values`;
CREATE TABLE IF NOT EXISTS `player_guid_values` (
  `guid` int(10) unsigned NOT NULL,
  `charm_guid` int(10) unsigned NOT NULL DEFAULT '0',
  `charm_id` int(10) unsigned NOT NULL DEFAULT '0',
  `charm_type` varchar(16) COLLATE latin1_general_ci NOT NULL DEFAULT '',
  `summon_guid` int(10) unsigned NOT NULL DEFAULT '0',
  `summon_id` int(10) unsigned NOT NULL DEFAULT '0',
  `summon_type` varchar(16) COLLATE latin1_general_ci NOT NULL DEFAULT '',
  `charmer_guid` int(10) unsigned NOT NULL DEFAULT '0',
  `charmer_id` int(10) unsigned NOT NULL DEFAULT '0',
  `charmer_type` varchar(16) COLLATE latin1_general_ci NOT NULL DEFAULT '',
  `summoner_guid` int(10) unsigned NOT NULL DEFAULT '0',
  `summoner_id` int(10) unsigned NOT NULL DEFAULT '0',
  `summoner_type` varchar(16) COLLATE latin1_general_ci NOT NULL DEFAULT '',
  `creator_guid` int(10) unsigned NOT NULL DEFAULT '0',
  `creator_id` int(10) unsigned NOT NULL DEFAULT '0',
  `creator_type` varchar(16) COLLATE latin1_general_ci NOT NULL DEFAULT '',
  `demon_creator_guid` int(10) unsigned NOT NULL DEFAULT '0',
  `demon_creator_id` int(10) unsigned NOT NULL DEFAULT '0',
  `demon_creator_type` varchar(16) COLLATE latin1_general_ci NOT NULL DEFAULT '',
  `target_guid` int(10) unsigned NOT NULL DEFAULT '0',
  `target_id` int(10) unsigned NOT NULL DEFAULT '0',
  `target_type` varchar(16) COLLATE latin1_general_ci NOT NULL DEFAULT '',
  PRIMARY KEY (`guid`)
) ENGINE=MyISAM DEFAULT CHARSET=latin1 COLLATE=latin1_general_ci ROW_FORMAT=COMPACT COMMENT='initial value in guid type update fields';

-- Data exporting was unselected.


-- Dumping structure for table sniffs_new_test.player_guid_values_update
DROP TABLE IF EXISTS `player_guid_values_update`;
CREATE TABLE IF NOT EXISTS `player_guid_values_update` (
  `unixtimems` bigint(20) unsigned NOT NULL DEFAULT '0',
  `guid` int(10) unsigned NOT NULL,
  `field_name` varchar(16) COLLATE latin1_general_ci NOT NULL DEFAULT '',
  `object_guid` int(10) unsigned NOT NULL DEFAULT '0',
  `object_id` int(10) unsigned NOT NULL DEFAULT '0',
  `object_type` varchar(16) COLLATE latin1_general_ci NOT NULL DEFAULT '',
  PRIMARY KEY (`guid`,`unixtimems`,`field_name`,`object_guid`,`object_id`,`object_type`)
) ENGINE=MyISAM DEFAULT CHARSET=latin1 COLLATE=latin1_general_ci ROW_FORMAT=COMPACT COMMENT='changes to guid type update fields';

-- Data exporting was unselected.


-- Dumping structure for table sniffs_new_test.player_levelstats
DROP TABLE IF EXISTS `player_levelstats`;
CREATE TABLE IF NOT EXISTS `player_levelstats` (
  `race` tinyint(3) unsigned NOT NULL,
  `class` tinyint(3) unsigned NOT NULL,
  `level` tinyint(3) unsigned NOT NULL,
  `strength` tinyint(3) unsigned NOT NULL,
  `agility` tinyint(3) unsigned NOT NULL,
  `stamina` tinyint(3) unsigned NOT NULL,
  `intellect` tinyint(3) unsigned NOT NULL,
  `spirit` tinyint(3) unsigned NOT NULL,
  `sniff_id_list` text COLLATE latin1_general_ci NOT NULL,
  PRIMARY KEY (`race`,`class`,`level`,`strength`,`agility`,`stamina`,`intellect`,`spirit`)
) ENGINE=MyISAM DEFAULT CHARSET=latin1 COLLATE=latin1_general_ci PACK_KEYS=0 COMMENT='Stores levels stats.';

-- Data exporting was unselected.


-- Dumping structure for table sniffs_new_test.player_levelup_info
DROP TABLE IF EXISTS `player_levelup_info`;
CREATE TABLE IF NOT EXISTS `player_levelup_info` (
  `race` tinyint(3) unsigned NOT NULL,
  `class` tinyint(3) unsigned NOT NULL,
  `level` tinyint(3) unsigned NOT NULL,
  `health` int(11) NOT NULL DEFAULT '0',
  `power0` int(11) NOT NULL DEFAULT '0',
  `power1` int(11) NOT NULL DEFAULT '0',
  `power2` int(11) NOT NULL DEFAULT '0',
  `power3` int(11) NOT NULL DEFAULT '0',
  `power4` int(11) NOT NULL DEFAULT '0',
  `power5` int(11) NOT NULL DEFAULT '0',
  `stat0` int(11) NOT NULL DEFAULT '0',
  `stat1` int(11) NOT NULL DEFAULT '0',
  `stat2` int(11) NOT NULL DEFAULT '0',
  `stat3` int(11) NOT NULL DEFAULT '0',
  `stat4` int(11) NOT NULL DEFAULT '0',
  `sniff_id` smallint(5) unsigned NOT NULL DEFAULT '0',
  `sniff_build` mediumint(8) unsigned NOT NULL DEFAULT '0',
  PRIMARY KEY (`race`,`class`,`level`)
) ENGINE=MyISAM DEFAULT CHARSET=latin1 COLLATE=latin1_general_ci;

-- Data exporting was unselected.


-- Dumping structure for table sniffs_new_test.player_melee_crit_chance
DROP TABLE IF EXISTS `player_melee_crit_chance`;
CREATE TABLE IF NOT EXISTS `player_melee_crit_chance` (
  `race` tinyint(3) unsigned NOT NULL DEFAULT '0',
  `class` tinyint(3) unsigned NOT NULL DEFAULT '0',
  `level` tinyint(3) unsigned NOT NULL DEFAULT '0',
  `agility` int(10) unsigned NOT NULL DEFAULT '0',
  `crit_chance` float NOT NULL DEFAULT '0',
  `weapon_item_id` int(10) unsigned NOT NULL DEFAULT '0',
  `weapon_skill_id` smallint(5) unsigned NOT NULL DEFAULT '0',
  `skill_current_value` smallint(5) unsigned NOT NULL DEFAULT '0',
  `skill_max_value` smallint(5) unsigned NOT NULL DEFAULT '0',
  `relevant_auras` varchar(128) COLLATE latin1_general_ci NOT NULL DEFAULT '' COMMENT 'auras of type 52',
  `sniff_id` smallint(5) unsigned NOT NULL DEFAULT '0',
  `sniff_build` mediumint(8) unsigned NOT NULL DEFAULT '0'
) ENGINE=MyISAM DEFAULT CHARSET=latin1 COLLATE=latin1_general_ci COMMENT='data about player melee crit chance, so that we can calculate the correct bonus to crit chance per agility in vanilla\r\nother expansions have this data in a dbc, because its shown on the user interface when you mouse over the agility stat';

-- Data exporting was unselected.


-- Dumping structure for table sniffs_new_test.player_minimap_ping
DROP TABLE IF EXISTS `player_minimap_ping`;
CREATE TABLE IF NOT EXISTS `player_minimap_ping` (
  `unixtimems` bigint(20) unsigned NOT NULL DEFAULT '0',
  `guid` int(10) unsigned NOT NULL,
  `position_x` float NOT NULL DEFAULT '0',
  `position_y` float NOT NULL DEFAULT '0',
  PRIMARY KEY (`guid`,`unixtimems`)
) ENGINE=MyISAM DEFAULT CHARSET=latin1 COLLATE=latin1_general_ci ROW_FORMAT=COMPACT COMMENT='from SMSG_MINIMAP_PING';

-- Data exporting was unselected.


-- Dumping structure for table sniffs_new_test.player_movement_client
DROP TABLE IF EXISTS `player_movement_client`;
CREATE TABLE IF NOT EXISTS `player_movement_client` (
  `unixtimems` bigint(20) unsigned NOT NULL,
  `guid` int(10) unsigned NOT NULL,
  `packet_id` int(10) unsigned NOT NULL,
  `opcode` varchar(64) COLLATE latin1_general_ci NOT NULL DEFAULT '',
  `move_time` int(10) unsigned NOT NULL DEFAULT '0',
  `move_flags` int(10) unsigned NOT NULL DEFAULT '0',
  `move_flags2` int(10) unsigned NOT NULL DEFAULT '0',
  `map` smallint(5) unsigned NOT NULL DEFAULT '0',
  `position_x` float NOT NULL DEFAULT '0',
  `position_y` float NOT NULL DEFAULT '0',
  `position_z` float NOT NULL DEFAULT '0',
  `orientation` float NOT NULL DEFAULT '0',
  `swim_pitch` float NOT NULL DEFAULT '0',
  `fall_time` int(11) unsigned NOT NULL DEFAULT '0',
  `jump_horizontal_speed` float NOT NULL DEFAULT '0',
  `jump_vertical_speed` float NOT NULL DEFAULT '0',
  `jump_cos_angle` float NOT NULL DEFAULT '0',
  `jump_sin_angle` float NOT NULL DEFAULT '0',
  `spline_elevation` float NOT NULL DEFAULT '0',
  `transport_guid` int(10) unsigned NOT NULL DEFAULT '0',
  `transport_id` int(10) unsigned NOT NULL DEFAULT '0',
  `transport_type` varchar(16) COLLATE latin1_general_ci NOT NULL DEFAULT '',
  `transport_x` float NOT NULL DEFAULT '0',
  `transport_y` float NOT NULL DEFAULT '0',
  `transport_z` float NOT NULL DEFAULT '0',
  `transport_o` float NOT NULL DEFAULT '0',
  `transport_time` int(10) unsigned NOT NULL DEFAULT '0',
  `transport_seat` tinyint(4) NOT NULL DEFAULT '0',
  PRIMARY KEY (`packet_id`)
) ENGINE=MyISAM DEFAULT CHARSET=latin1 COLLATE=latin1_general_ci ROW_FORMAT=COMPACT COMMENT='client side movement';

-- Data exporting was unselected.


-- Dumping structure for table sniffs_new_test.player_movement_server
DROP TABLE IF EXISTS `player_movement_server`;
CREATE TABLE IF NOT EXISTS `player_movement_server` (
  `unixtimems` bigint(20) unsigned NOT NULL COMMENT 'when the packet was received',
  `guid` int(10) unsigned NOT NULL COMMENT 'player guid',
  `point` smallint(5) unsigned NOT NULL COMMENT 'counter of movements per guid',
  `move_time` int(10) unsigned NOT NULL COMMENT 'how long it will take to move between these points',
  `spline_flags` int(10) unsigned NOT NULL,
  `spline_count` smallint(5) unsigned NOT NULL COMMENT 'number of splines belonging to this point',
  `start_position_x` float NOT NULL COMMENT 'starting position',
  `start_position_y` float NOT NULL,
  `start_position_z` float NOT NULL,
  `end_position_x` float NOT NULL COMMENT 'final position',
  `end_position_y` float NOT NULL,
  `end_position_z` float NOT NULL,
  `orientation` float NOT NULL COMMENT 'final orientation',
  `transport_guid` int(10) unsigned NOT NULL DEFAULT '0',
  `transport_id` int(10) unsigned NOT NULL DEFAULT '0',
  `transport_type` varchar(16) COLLATE latin1_general_ci NOT NULL DEFAULT '',
  `transport_seat` tinyint(4) NOT NULL DEFAULT '0',
  `anim_tier` tinyint(4) unsigned NOT NULL DEFAULT '0',
  `vertical_speed` float NOT NULL DEFAULT '0',
  `effect_start_time` int(10) unsigned NOT NULL DEFAULT '0',
  PRIMARY KEY (`guid`,`point`)
) ENGINE=MyISAM DEFAULT CHARSET=latin1 COLLATE=latin1_general_ci ROW_FORMAT=COMPACT COMMENT='movement points from SMSG_ON_MONSTER_MOVE';

-- Data exporting was unselected.


-- Dumping structure for table sniffs_new_test.player_movement_server_spline
DROP TABLE IF EXISTS `player_movement_server_spline`;
CREATE TABLE IF NOT EXISTS `player_movement_server_spline` (
  `guid` int(10) unsigned NOT NULL COMMENT 'player guid',
  `parent_point` smallint(5) unsigned NOT NULL COMMENT 'point from character_movement_server to which the spline data belongs',
  `spline_point` smallint(5) unsigned NOT NULL COMMENT 'order of points within the spline',
  `position_x` float NOT NULL,
  `position_y` float NOT NULL,
  `position_z` float NOT NULL,
  PRIMARY KEY (`guid`,`parent_point`,`spline_point`)
) ENGINE=MyISAM DEFAULT CHARSET=latin1 COLLATE=latin1_general_ci ROW_FORMAT=COMPACT COMMENT='individual spline points for combat  movement';

-- Data exporting was unselected.


-- Dumping structure for table sniffs_new_test.player_power_values
DROP TABLE IF EXISTS `player_power_values`;
CREATE TABLE IF NOT EXISTS `player_power_values` (
  `guid` int(10) unsigned NOT NULL,
  `power_type` tinyint(3) unsigned NOT NULL,
  `current_power` int(10) unsigned NOT NULL DEFAULT '0',
  `max_power` int(10) unsigned NOT NULL DEFAULT '0',
  PRIMARY KEY (`guid`,`power_type`)
) ENGINE=MyISAM DEFAULT CHARSET=latin1 COLLATE=latin1_general_ci COMMENT='initial value of power update fields';

-- Data exporting was unselected.


-- Dumping structure for table sniffs_new_test.player_power_values_update
DROP TABLE IF EXISTS `player_power_values_update`;
CREATE TABLE IF NOT EXISTS `player_power_values_update` (
  `unixtimems` bigint(20) unsigned NOT NULL,
  `guid` int(10) unsigned NOT NULL,
  `power_type` tinyint(3) unsigned NOT NULL,
  `current_power` int(10) unsigned DEFAULT NULL,
  `max_power` int(10) unsigned DEFAULT NULL
) ENGINE=MyISAM DEFAULT CHARSET=latin1 COLLATE=latin1_general_ci COMMENT='changes to power update fields';

-- Data exporting was unselected.


-- Dumping structure for table sniffs_new_test.player_ranged_crit_chance
DROP TABLE IF EXISTS `player_ranged_crit_chance`;
CREATE TABLE IF NOT EXISTS `player_ranged_crit_chance` (
  `race` tinyint(3) unsigned NOT NULL DEFAULT '0',
  `class` tinyint(3) unsigned NOT NULL DEFAULT '0',
  `level` tinyint(3) unsigned NOT NULL DEFAULT '0',
  `agility` int(10) unsigned NOT NULL DEFAULT '0',
  `crit_chance` float NOT NULL DEFAULT '0',
  `weapon_item_id` int(10) unsigned NOT NULL DEFAULT '0',
  `weapon_skill_id` smallint(5) unsigned NOT NULL DEFAULT '0',
  `skill_current_value` smallint(5) unsigned NOT NULL DEFAULT '0',
  `skill_max_value` smallint(5) unsigned NOT NULL DEFAULT '0',
  `relevant_auras` varchar(128) COLLATE latin1_general_ci NOT NULL DEFAULT '' COMMENT 'auras of type 52',
  `sniff_id` smallint(5) unsigned NOT NULL DEFAULT '0',
  `sniff_build` mediumint(8) unsigned NOT NULL DEFAULT '0'
) ENGINE=MyISAM DEFAULT CHARSET=latin1 COLLATE=latin1_general_ci ROW_FORMAT=COMPACT COMMENT='data about player ranged crit chance, so that we can calculate the correct bonus to crit chance per agility in vanilla\r\nother expansions have this data in a dbc, because its shown on the user interface when you mouse over the agility stat';

-- Data exporting was unselected.


-- Dumping structure for table sniffs_new_test.player_speed_update
DROP TABLE IF EXISTS `player_speed_update`;
CREATE TABLE IF NOT EXISTS `player_speed_update` (
  `unixtimems` bigint(20) unsigned NOT NULL COMMENT 'when the packet was received',
  `guid` int(10) unsigned NOT NULL,
  `speed_type` tinyint(3) unsigned NOT NULL,
  `speed_rate` float unsigned NOT NULL
) ENGINE=MyISAM DEFAULT CHARSET=latin1 COLLATE=latin1_general_ci ROW_FORMAT=COMPACT COMMENT='changes to movement speed';

-- Data exporting was unselected.


-- Dumping structure for table sniffs_new_test.player_spell_crit_chance
DROP TABLE IF EXISTS `player_spell_crit_chance`;
CREATE TABLE IF NOT EXISTS `player_spell_crit_chance` (
  `race` tinyint(3) unsigned NOT NULL DEFAULT '0',
  `class` tinyint(3) unsigned NOT NULL DEFAULT '0',
  `level` tinyint(3) unsigned NOT NULL DEFAULT '0',
  `intellect` int(10) unsigned NOT NULL DEFAULT '0',
  `crit_chance` float NOT NULL DEFAULT '0',
  `relevant_auras` varchar(128) COLLATE latin1_general_ci NOT NULL DEFAULT '' COMMENT 'auras of type 57 and 71',
  `sniff_id` smallint(5) unsigned NOT NULL DEFAULT '0',
  `sniff_build` mediumint(8) unsigned NOT NULL DEFAULT '0'
) ENGINE=MyISAM DEFAULT CHARSET=latin1 COLLATE=latin1_general_ci ROW_FORMAT=COMPACT COMMENT='data about player spell crit chance, so that we can calculate the correct bonus to crit chance per intellect in vanilla\r\nother expansions have this data in a dbc, because its shown on the user interface when you mouse over the intellect stat';

-- Data exporting was unselected.


-- Dumping structure for table sniffs_new_test.player_values_update
DROP TABLE IF EXISTS `player_values_update`;
CREATE TABLE IF NOT EXISTS `player_values_update` (
  `unixtimems` bigint(20) unsigned NOT NULL COMMENT 'when the packet was received',
  `guid` int(10) unsigned NOT NULL COMMENT 'creature spawn guid',
  `entry` int(10) unsigned DEFAULT NULL,
  `scale` float DEFAULT NULL,
  `display_id` int(10) unsigned DEFAULT NULL,
  `mount_display_id` int(10) unsigned DEFAULT NULL,
  `faction` int(10) unsigned DEFAULT NULL,
  `level` int(10) unsigned DEFAULT NULL,
  `npc_flags` int(10) unsigned DEFAULT NULL,
  `unit_flags` int(10) unsigned DEFAULT NULL,
  `unit_flags2` int(10) unsigned DEFAULT NULL,
  `dynamic_flags` int(10) unsigned DEFAULT NULL,
  `current_health` int(10) unsigned DEFAULT NULL,
  `max_health` int(10) unsigned DEFAULT NULL,
  `power_type` int(10) unsigned DEFAULT NULL,
  `aura_state` int(10) unsigned DEFAULT NULL,
  `emote_state` int(10) unsigned DEFAULT NULL,
  `stand_state` int(10) unsigned DEFAULT NULL,
  `vis_flags` int(10) unsigned DEFAULT NULL,
  `anim_tier` int(10) unsigned DEFAULT NULL,
  `sheath_state` int(10) unsigned DEFAULT NULL,
  `pvp_flags` int(10) unsigned DEFAULT NULL,
  `shapeshift_form` int(10) unsigned DEFAULT NULL,
  `bounding_radius` float DEFAULT NULL,
  `combat_reach` float DEFAULT NULL,
  `mod_melee_haste` float DEFAULT NULL,
  `main_hand_attack_time` int(10) unsigned DEFAULT NULL,
  `off_hand_attack_time` int(10) unsigned DEFAULT NULL,
  `channel_spell_id` int(10) unsigned DEFAULT NULL,
  `channel_visual_id` int(10) unsigned DEFAULT NULL
) ENGINE=MyISAM DEFAULT CHARSET=latin1 COLLATE=latin1_general_ci ROW_FORMAT=COMPACT COMMENT='values updates from SMSG_UPDATE_OBJECT';

-- Data exporting was unselected.


-- Dumping structure for table sniffs_new_test.play_music
DROP TABLE IF EXISTS `play_music`;
CREATE TABLE IF NOT EXISTS `play_music` (
  `unixtimems` bigint(20) unsigned NOT NULL COMMENT 'when the packet was received',
  `music` int(10) unsigned NOT NULL COMMENT 'references SoundEntries.dbc',
  PRIMARY KEY (`music`,`unixtimems`)
) ENGINE=MyISAM DEFAULT CHARSET=latin1 COLLATE=latin1_general_ci COMMENT='from SMSG_PLAY_MUSIC';

-- Data exporting was unselected.


-- Dumping structure for table sniffs_new_test.play_sound
DROP TABLE IF EXISTS `play_sound`;
CREATE TABLE IF NOT EXISTS `play_sound` (
  `unixtimems` bigint(20) unsigned NOT NULL COMMENT 'when the packet was received',
  `source_guid` int(10) unsigned NOT NULL COMMENT 'guid of the object which was the source of the sound',
  `source_id` int(10) unsigned NOT NULL COMMENT 'entry of the object which was the source of the sound',
  `source_type` varchar(16) COLLATE latin1_general_ci NOT NULL COMMENT 'type of the object which was the source of the sound',
  `sound` int(10) unsigned NOT NULL COMMENT 'references SoundEntries.dbc',
  PRIMARY KEY (`unixtimems`,`source_guid`,`sound`,`source_id`,`source_type`)
) ENGINE=MyISAM DEFAULT CHARSET=latin1 COLLATE=latin1_general_ci COMMENT='from SMSG_PLAY_SOUND';

-- Data exporting was unselected.


-- Dumping structure for table sniffs_new_test.play_spell_visual_kit
DROP TABLE IF EXISTS `play_spell_visual_kit`;
CREATE TABLE IF NOT EXISTS `play_spell_visual_kit` (
  `unixtimems` bigint(20) unsigned NOT NULL COMMENT 'when the packet was received',
  `caster_guid` int(10) unsigned NOT NULL,
  `caster_id` int(10) unsigned NOT NULL,
  `caster_type` varchar(16) COLLATE latin1_general_ci NOT NULL,
  `kit_id` int(10) NOT NULL COMMENT 'references SpellVisualKit.dbc',
  `kit_type` int(10) DEFAULT NULL,
  `duration` int(10) unsigned DEFAULT NULL,
  PRIMARY KEY (`caster_id`,`caster_type`,`kit_id`,`unixtimems`,`caster_guid`)
) ENGINE=MyISAM DEFAULT CHARSET=latin1 COLLATE=latin1_general_ci ROW_FORMAT=COMPACT COMMENT='from SMSG_PLAY_SPELL_VISUAL_KIT';

-- Data exporting was unselected.


-- Dumping structure for table sniffs_new_test.points_of_interest
DROP TABLE IF EXISTS `points_of_interest`;
CREATE TABLE IF NOT EXISTS `points_of_interest` (
  `entry` mediumint(8) unsigned NOT NULL DEFAULT '0',
  `x` float NOT NULL DEFAULT '0',
  `y` float NOT NULL DEFAULT '0',
  `icon` mediumint(8) unsigned NOT NULL DEFAULT '0',
  `flags` mediumint(8) unsigned NOT NULL DEFAULT '0',
  `data` mediumint(8) unsigned NOT NULL DEFAULT '0',
  `icon_name` text NOT NULL,
  `sniff_build` mediumint(8) unsigned NOT NULL DEFAULT '0',
  PRIMARY KEY (`entry`)
) ENGINE=MyISAM DEFAULT CHARSET=utf8;

-- Data exporting was unselected.


-- Dumping structure for table sniffs_new_test.quest_details
DROP TABLE IF EXISTS `quest_details`;
CREATE TABLE IF NOT EXISTS `quest_details` (
  `entry` mediumint(8) unsigned NOT NULL DEFAULT '0',
  `sniff_build` mediumint(8) unsigned NOT NULL DEFAULT '0',
  `emote_id1` smallint(5) unsigned NOT NULL DEFAULT '0',
  `emote_id2` smallint(5) unsigned NOT NULL DEFAULT '0',
  `emote_id3` smallint(5) unsigned NOT NULL DEFAULT '0',
  `emote_id4` smallint(5) unsigned NOT NULL DEFAULT '0',
  `emote_delay1` int(10) unsigned NOT NULL DEFAULT '0',
  `emote_delay2` int(10) unsigned NOT NULL DEFAULT '0',
  `emote_delay3` int(10) unsigned NOT NULL DEFAULT '0',
  `emote_delay4` int(10) unsigned NOT NULL DEFAULT '0',
  PRIMARY KEY (`entry`,`sniff_build`)
) ENGINE=MyISAM DEFAULT CHARSET=latin1 COLLATE=latin1_general_ci;

-- Data exporting was unselected.


-- Dumping structure for table sniffs_new_test.quest_ender
DROP TABLE IF EXISTS `quest_ender`;
CREATE TABLE IF NOT EXISTS `quest_ender` (
  `object_id` int(10) unsigned NOT NULL COMMENT 'entry of the quest ender object',
  `object_type` varchar(16) COLLATE latin1_general_ci NOT NULL COMMENT 'type of the quest ender object',
  `quest_id` int(10) unsigned NOT NULL COMMENT 'references quest_template',
  `sniff_build` mediumint(8) unsigned NOT NULL DEFAULT '0',
  PRIMARY KEY (`object_id`,`object_type`,`quest_id`)
) ENGINE=MyISAM DEFAULT CHARSET=latin1 COLLATE=latin1_general_ci ROW_FORMAT=COMPACT COMMENT='list of quests that can be turned in to a given creature or gameobject';

-- Data exporting was unselected.


-- Dumping structure for table sniffs_new_test.quest_greeting
DROP TABLE IF EXISTS `quest_greeting`;
CREATE TABLE IF NOT EXISTS `quest_greeting` (
  `ID` mediumint(8) unsigned NOT NULL DEFAULT '0',
  `Type` tinyint(3) unsigned NOT NULL DEFAULT '0',
  `GreetEmoteType` smallint(5) unsigned NOT NULL DEFAULT '0',
  `GreetEmoteDelay` int(10) unsigned NOT NULL DEFAULT '0',
  `Greeting` text COLLATE latin1_general_ci,
  `VerifiedBuild` mediumint(8) unsigned NOT NULL DEFAULT '0',
  PRIMARY KEY (`ID`,`Type`)
) ENGINE=MyISAM DEFAULT CHARSET=latin1 COLLATE=latin1_general_ci;

-- Data exporting was unselected.


-- Dumping structure for table sniffs_new_test.quest_greeting_locale
DROP TABLE IF EXISTS `quest_greeting_locale`;
CREATE TABLE IF NOT EXISTS `quest_greeting_locale` (
  `ID` int(10) unsigned NOT NULL DEFAULT '0',
  `type` tinyint(3) unsigned NOT NULL DEFAULT '0',
  `locale` varchar(4) NOT NULL,
  `Greeting` text,
  `VerifiedBuild` mediumint(8) unsigned NOT NULL DEFAULT '0',
  PRIMARY KEY (`ID`,`type`,`locale`)
) ENGINE=MyISAM DEFAULT CHARSET=utf8;

-- Data exporting was unselected.


-- Dumping structure for table sniffs_new_test.quest_objectives
DROP TABLE IF EXISTS `quest_objectives`;
CREATE TABLE IF NOT EXISTS `quest_objectives` (
  `ID` mediumint(8) unsigned NOT NULL DEFAULT '0',
  `SniffBuild` mediumint(8) unsigned NOT NULL DEFAULT '0',
  `QuestID` mediumint(8) unsigned NOT NULL DEFAULT '0',
  `Type` tinyint(3) unsigned NOT NULL DEFAULT '0',
  `StorageIndex` tinyint(3) NOT NULL DEFAULT '0',
  `Order` int(11) NOT NULL DEFAULT '0',
  `ObjectID` int(10) NOT NULL DEFAULT '0',
  `Amount` int(10) NOT NULL DEFAULT '0',
  `Flags` int(10) unsigned NOT NULL DEFAULT '0',
  `Flags2` int(10) unsigned NOT NULL DEFAULT '0',
  `ProgressBarWeight` float NOT NULL DEFAULT '0',
  `Description` text COLLATE latin1_general_ci,
  PRIMARY KEY (`ID`,`SniffBuild`)
) ENGINE=MyISAM DEFAULT CHARSET=latin1 COLLATE=latin1_general_ci;

-- Data exporting was unselected.


-- Dumping structure for table sniffs_new_test.quest_objectives_locale
DROP TABLE IF EXISTS `quest_objectives_locale`;
CREATE TABLE IF NOT EXISTS `quest_objectives_locale` (
  `ID` mediumint(8) unsigned NOT NULL DEFAULT '0',
  `locale` varchar(4) NOT NULL,
  `QuestId` mediumint(8) unsigned NOT NULL DEFAULT '0',
  `StorageIndex` tinyint(3) NOT NULL DEFAULT '0',
  `Description` text,
  `VerifiedBuild` mediumint(8) unsigned NOT NULL DEFAULT '0',
  PRIMARY KEY (`ID`,`locale`)
) ENGINE=MyISAM DEFAULT CHARSET=utf8;

-- Data exporting was unselected.


-- Dumping structure for table sniffs_new_test.quest_offer_reward
DROP TABLE IF EXISTS `quest_offer_reward`;
CREATE TABLE IF NOT EXISTS `quest_offer_reward` (
  `entry` mediumint(8) unsigned NOT NULL DEFAULT '0',
  `sniff_build` mediumint(8) unsigned NOT NULL DEFAULT '0',
  `emote_id1` smallint(5) unsigned NOT NULL DEFAULT '0',
  `emote_id2` smallint(5) unsigned NOT NULL DEFAULT '0',
  `emote_id3` smallint(5) unsigned NOT NULL DEFAULT '0',
  `emote_id4` smallint(5) unsigned NOT NULL DEFAULT '0',
  `emote_delay1` int(10) unsigned NOT NULL DEFAULT '0',
  `emote_delay2` int(10) unsigned NOT NULL DEFAULT '0',
  `emote_delay3` int(10) unsigned NOT NULL DEFAULT '0',
  `emote_delay4` int(10) unsigned NOT NULL DEFAULT '0',
  `reward_text` text COLLATE latin1_general_ci,
  PRIMARY KEY (`entry`,`sniff_build`)
) ENGINE=MyISAM DEFAULT CHARSET=latin1 COLLATE=latin1_general_ci;

-- Data exporting was unselected.


-- Dumping structure for table sniffs_new_test.quest_offer_reward_locale
DROP TABLE IF EXISTS `quest_offer_reward_locale`;
CREATE TABLE IF NOT EXISTS `quest_offer_reward_locale` (
  `ID` int(10) unsigned NOT NULL DEFAULT '0',
  `locale` varchar(4) NOT NULL,
  `RewardText` text,
  `VerifiedBuild` mediumint(8) unsigned NOT NULL DEFAULT '0',
  PRIMARY KEY (`ID`,`locale`)
) ENGINE=MyISAM DEFAULT CHARSET=utf8;

-- Data exporting was unselected.


-- Dumping structure for table sniffs_new_test.quest_request_items
DROP TABLE IF EXISTS `quest_request_items`;
CREATE TABLE IF NOT EXISTS `quest_request_items` (
  `entry` mediumint(8) unsigned NOT NULL DEFAULT '0',
  `sniff_build` mediumint(8) unsigned NOT NULL DEFAULT '0',
  `emote_id` smallint(5) unsigned DEFAULT NULL,
  `completion_text` text COLLATE latin1_general_ci,
  PRIMARY KEY (`entry`,`sniff_build`)
) ENGINE=MyISAM DEFAULT CHARSET=latin1 COLLATE=latin1_general_ci;

-- Data exporting was unselected.


-- Dumping structure for table sniffs_new_test.quest_request_items_locale
DROP TABLE IF EXISTS `quest_request_items_locale`;
CREATE TABLE IF NOT EXISTS `quest_request_items_locale` (
  `ID` int(10) unsigned NOT NULL DEFAULT '0',
  `locale` varchar(4) NOT NULL,
  `CompletionText` text,
  `VerifiedBuild` mediumint(8) unsigned NOT NULL DEFAULT '0',
  PRIMARY KEY (`ID`,`locale`)
) ENGINE=MyISAM DEFAULT CHARSET=utf8;

-- Data exporting was unselected.


-- Dumping structure for table sniffs_new_test.quest_starter
DROP TABLE IF EXISTS `quest_starter`;
CREATE TABLE IF NOT EXISTS `quest_starter` (
  `object_id` int(10) unsigned NOT NULL COMMENT 'entry of the quest giver object',
  `object_type` varchar(16) COLLATE latin1_general_ci NOT NULL COMMENT 'type of the quest giver object',
  `quest_id` int(10) unsigned NOT NULL COMMENT 'references quest_template',
  `sniff_build` mediumint(8) unsigned NOT NULL DEFAULT '0',
  PRIMARY KEY (`object_id`,`object_type`,`quest_id`)
) ENGINE=MyISAM DEFAULT CHARSET=latin1 COLLATE=latin1_general_ci COMMENT='list of quests that can be picked up from a given creature or gameobject';

-- Data exporting was unselected.


-- Dumping structure for table sniffs_new_test.quest_template
DROP TABLE IF EXISTS `quest_template`;
CREATE TABLE IF NOT EXISTS `quest_template` (
  `ID` mediumint(8) unsigned NOT NULL DEFAULT '0',
  `SniffBuild` mediumint(8) unsigned NOT NULL DEFAULT '0',
  `QuestType` tinyint(3) unsigned NOT NULL DEFAULT '2',
  `QuestLevel` smallint(3) NOT NULL DEFAULT '1',
  `ScalingFactionGroup` int(11) NOT NULL DEFAULT '1',
  `MaxScalingLevel` int(11) NOT NULL DEFAULT '1',
  `QuestPackageID` int(10) unsigned NOT NULL DEFAULT '1',
  `MinLevel` tinyint(3) unsigned NOT NULL DEFAULT '0',
  `QuestSortID` smallint(6) NOT NULL DEFAULT '0',
  `QuestInfoID` smallint(5) unsigned NOT NULL DEFAULT '0',
  `SuggestedGroupNum` tinyint(3) unsigned NOT NULL DEFAULT '0',
  `RequiredFactionId1` smallint(5) unsigned NOT NULL DEFAULT '0',
  `RequiredFactionId2` smallint(5) unsigned NOT NULL DEFAULT '0',
  `RequiredFactionValue1` mediumint(8) NOT NULL DEFAULT '0',
  `RequiredFactionValue2` mediumint(8) NOT NULL DEFAULT '0',
  `RewardNextQuest` mediumint(8) unsigned NOT NULL DEFAULT '0',
  `RewardXPDifficulty` tinyint(3) unsigned NOT NULL DEFAULT '0',
  `RewardXPMultiplier` float NOT NULL DEFAULT '0',
  `RewardMoney` int(11) NOT NULL DEFAULT '0',
  `RewardMoneyDifficulty` int(11) unsigned NOT NULL DEFAULT '0',
  `RewardMoneyMultiplier` float NOT NULL DEFAULT '0',
  `RewardBonusMoney` int(10) unsigned NOT NULL DEFAULT '0',
  `RewardDisplaySpell1` mediumint(8) unsigned NOT NULL DEFAULT '0',
  `RewardDisplaySpell2` mediumint(8) unsigned NOT NULL DEFAULT '0',
  `RewardDisplaySpell3` mediumint(8) unsigned NOT NULL DEFAULT '0',
  `RewardSpell` int(11) unsigned NOT NULL DEFAULT '0',
  `RewardHonor` int(11) unsigned NOT NULL DEFAULT '0',
  `RewardKillHonor` float NOT NULL DEFAULT '0',
  `StartItem` mediumint(8) unsigned NOT NULL DEFAULT '0',
  `RewardArtifactXPDifficulty` mediumint(8) unsigned NOT NULL DEFAULT '0',
  `RewardArtifactXPMultiplier` float NOT NULL DEFAULT '0',
  `RewardArtifactCategoryID` mediumint(8) unsigned NOT NULL DEFAULT '0',
  `Flags` int(10) unsigned NOT NULL DEFAULT '0',
  `FlagsEx` int(10) unsigned NOT NULL DEFAULT '0',
  `FlagsEx2` int(10) unsigned NOT NULL DEFAULT '0',
  `RequiredPlayerKills` tinyint(3) unsigned NOT NULL DEFAULT '0',
  `RewardSkillLineID` int(10) unsigned NOT NULL DEFAULT '0',
  `RewardNumSkillUps` int(10) unsigned NOT NULL DEFAULT '0',
  `PortraitGiver` int(10) unsigned NOT NULL DEFAULT '0',
  `PortraitGiverMount` int(10) unsigned NOT NULL DEFAULT '0',
  `PortraitTurnIn` int(10) unsigned NOT NULL DEFAULT '0',
  `RewardItem1` mediumint(8) unsigned NOT NULL DEFAULT '0',
  `RewardAmount1` smallint(5) unsigned NOT NULL DEFAULT '0',
  `RewardItem2` mediumint(8) unsigned NOT NULL DEFAULT '0',
  `RewardAmount2` smallint(5) unsigned NOT NULL DEFAULT '0',
  `RewardItem3` mediumint(8) unsigned NOT NULL DEFAULT '0',
  `RewardAmount3` smallint(5) unsigned NOT NULL DEFAULT '0',
  `RewardItem4` mediumint(8) unsigned NOT NULL DEFAULT '0',
  `RewardAmount4` smallint(5) unsigned NOT NULL DEFAULT '0',
  `ItemDrop1` mediumint(8) unsigned NOT NULL DEFAULT '0',
  `ItemDropQuantity1` smallint(5) unsigned NOT NULL DEFAULT '0',
  `ItemDrop2` mediumint(8) unsigned NOT NULL DEFAULT '0',
  `ItemDropQuantity2` smallint(5) unsigned NOT NULL DEFAULT '0',
  `ItemDrop3` mediumint(8) unsigned NOT NULL DEFAULT '0',
  `ItemDropQuantity3` smallint(5) unsigned NOT NULL DEFAULT '0',
  `ItemDrop4` mediumint(8) unsigned NOT NULL DEFAULT '0',
  `ItemDropQuantity4` smallint(5) unsigned NOT NULL DEFAULT '0',
  `RewardChoiceItemID1` mediumint(8) unsigned NOT NULL DEFAULT '0',
  `RewardChoiceItemQuantity1` smallint(5) unsigned NOT NULL DEFAULT '0',
  `RewardChoiceItemDisplayID1` mediumint(8) unsigned NOT NULL DEFAULT '0',
  `RewardChoiceItemID2` mediumint(8) unsigned NOT NULL DEFAULT '0',
  `RewardChoiceItemQuantity2` smallint(5) unsigned NOT NULL DEFAULT '0',
  `RewardChoiceItemDisplayID2` mediumint(8) unsigned NOT NULL DEFAULT '0',
  `RewardChoiceItemID3` mediumint(8) unsigned NOT NULL DEFAULT '0',
  `RewardChoiceItemQuantity3` smallint(5) unsigned NOT NULL DEFAULT '0',
  `RewardChoiceItemDisplayID3` mediumint(8) unsigned NOT NULL DEFAULT '0',
  `RewardChoiceItemID4` mediumint(8) unsigned NOT NULL DEFAULT '0',
  `RewardChoiceItemQuantity4` smallint(5) unsigned NOT NULL DEFAULT '0',
  `RewardChoiceItemDisplayID4` mediumint(8) unsigned NOT NULL DEFAULT '0',
  `RewardChoiceItemID5` mediumint(8) unsigned NOT NULL DEFAULT '0',
  `RewardChoiceItemQuantity5` smallint(5) unsigned NOT NULL DEFAULT '0',
  `RewardChoiceItemDisplayID5` mediumint(8) unsigned NOT NULL DEFAULT '0',
  `RewardChoiceItemID6` mediumint(8) unsigned NOT NULL DEFAULT '0',
  `RewardChoiceItemQuantity6` smallint(5) unsigned NOT NULL DEFAULT '0',
  `RewardChoiceItemDisplayID6` mediumint(8) unsigned NOT NULL DEFAULT '0',
  `POIContinent` smallint(5) unsigned NOT NULL DEFAULT '0',
  `POIx` float NOT NULL DEFAULT '0',
  `POIy` float NOT NULL DEFAULT '0',
  `POIPriority` mediumint(8) NOT NULL DEFAULT '0',
  `RewardTitle` tinyint(3) unsigned NOT NULL DEFAULT '0',
  `RewardTalents` tinyint(3) unsigned NOT NULL DEFAULT '0',
  `RewardArenaPoints` smallint(5) unsigned NOT NULL DEFAULT '0',
  `RewardFactionID1` smallint(5) unsigned NOT NULL DEFAULT '0' COMMENT 'faction id from Faction.dbc in this case',
  `RewardFactionValue1` mediumint(8) NOT NULL DEFAULT '0',
  `RewardFactionCapIn1` mediumint(8) NOT NULL DEFAULT '0',
  `RewardFactionOverride1` mediumint(8) NOT NULL DEFAULT '0',
  `RewardFactionID2` smallint(5) unsigned NOT NULL DEFAULT '0' COMMENT 'faction id from Faction.dbc in this case',
  `RewardFactionValue2` mediumint(8) NOT NULL DEFAULT '0',
  `RewardFactionCapIn2` mediumint(8) NOT NULL DEFAULT '0',
  `RewardFactionOverride2` mediumint(8) NOT NULL DEFAULT '0',
  `RewardFactionID3` smallint(5) unsigned NOT NULL DEFAULT '0' COMMENT 'faction id from Faction.dbc in this case',
  `RewardFactionValue3` mediumint(8) NOT NULL DEFAULT '0',
  `RewardFactionCapIn3` mediumint(8) NOT NULL DEFAULT '0',
  `RewardFactionOverride3` mediumint(8) NOT NULL DEFAULT '0',
  `RewardFactionID4` smallint(5) unsigned NOT NULL DEFAULT '0' COMMENT 'faction id from Faction.dbc in this case',
  `RewardFactionValue4` mediumint(8) NOT NULL DEFAULT '0',
  `RewardFactionCapIn4` mediumint(8) NOT NULL DEFAULT '0',
  `RewardFactionOverride4` mediumint(8) NOT NULL DEFAULT '0',
  `RewardFactionID5` smallint(5) unsigned NOT NULL DEFAULT '0' COMMENT 'faction id from Faction.dbc in this case',
  `RewardFactionValue5` mediumint(8) NOT NULL DEFAULT '0',
  `RewardFactionCapIn5` mediumint(8) NOT NULL DEFAULT '0',
  `RewardFactionOverride5` mediumint(8) NOT NULL DEFAULT '0',
  `RewardFactionFlags` int(10) unsigned NOT NULL DEFAULT '0',
  `AreaGroupID` int(10) unsigned NOT NULL DEFAULT '0',
  `TimeAllowed` int(10) unsigned NOT NULL DEFAULT '0',
  `AllowableRaces` int(10) unsigned NOT NULL DEFAULT '0',
  `TreasurePickerID` int(10) NOT NULL DEFAULT '0',
  `Expansion` int(10) NOT NULL DEFAULT '0',
  `LogTitle` text COLLATE latin1_general_ci,
  `LogDescription` text COLLATE latin1_general_ci,
  `QuestDescription` text COLLATE latin1_general_ci,
  `AreaDescription` text COLLATE latin1_general_ci,
  `QuestCompletionLog` text COLLATE latin1_general_ci,
  `RequiredNpcOrGo1` mediumint(8) NOT NULL DEFAULT '0',
  `RequiredNpcOrGo2` mediumint(8) NOT NULL DEFAULT '0',
  `RequiredNpcOrGo3` mediumint(8) NOT NULL DEFAULT '0',
  `RequiredNpcOrGo4` mediumint(8) NOT NULL DEFAULT '0',
  `RequiredNpcOrGoCount1` smallint(5) unsigned NOT NULL DEFAULT '0',
  `RequiredNpcOrGoCount2` smallint(5) unsigned NOT NULL DEFAULT '0',
  `RequiredNpcOrGoCount3` smallint(5) unsigned NOT NULL DEFAULT '0',
  `RequiredNpcOrGoCount4` smallint(5) unsigned NOT NULL DEFAULT '0',
  `RequiredItemId1` mediumint(8) unsigned NOT NULL DEFAULT '0',
  `RequiredItemId2` mediumint(8) unsigned NOT NULL DEFAULT '0',
  `RequiredItemId3` mediumint(8) unsigned NOT NULL DEFAULT '0',
  `RequiredItemId4` mediumint(8) unsigned NOT NULL DEFAULT '0',
  `RequiredItemId5` mediumint(8) unsigned NOT NULL DEFAULT '0',
  `RequiredItemId6` mediumint(8) unsigned NOT NULL DEFAULT '0',
  `RequiredItemCount1` smallint(5) unsigned NOT NULL DEFAULT '0',
  `RequiredItemCount2` smallint(5) unsigned NOT NULL DEFAULT '0',
  `RequiredItemCount3` smallint(5) unsigned NOT NULL DEFAULT '0',
  `RequiredItemCount4` smallint(5) unsigned NOT NULL DEFAULT '0',
  `RequiredItemCount5` smallint(5) unsigned NOT NULL DEFAULT '0',
  `RequiredItemCount6` smallint(5) unsigned NOT NULL DEFAULT '0',
  `Unknown0` tinyint(3) unsigned NOT NULL DEFAULT '0',
  `ObjectiveText1` text COLLATE latin1_general_ci,
  `ObjectiveText2` text COLLATE latin1_general_ci,
  `ObjectiveText3` text COLLATE latin1_general_ci,
  `ObjectiveText4` text COLLATE latin1_general_ci,
  `RewardCurrencyID1` int(10) unsigned DEFAULT NULL,
  `RewardCurrencyID2` int(10) unsigned DEFAULT NULL,
  `RewardCurrencyID3` int(10) unsigned DEFAULT NULL,
  `RewardCurrencyID4` int(10) unsigned DEFAULT NULL,
  `RewardCurrencyQty1` int(10) unsigned DEFAULT NULL,
  `RewardCurrencyQty2` int(10) unsigned DEFAULT NULL,
  `RewardCurrencyQty3` int(10) unsigned DEFAULT NULL,
  `RewardCurrencyQty4` int(10) unsigned DEFAULT NULL,
  `PortraitGiverText` text COLLATE latin1_general_ci,
  `PortraitGiverName` text COLLATE latin1_general_ci,
  `PortraitTurnInText` text COLLATE latin1_general_ci,
  `PortraitTurnInName` text COLLATE latin1_general_ci,
  `AcceptedSoundKitID` int(10) unsigned DEFAULT NULL,
  `CompleteSoundKitID` int(10) unsigned DEFAULT NULL,
  PRIMARY KEY (`ID`,`SniffBuild`)
) ENGINE=MyISAM DEFAULT CHARSET=latin1 COLLATE=latin1_general_ci COMMENT='Quest System';

-- Data exporting was unselected.


-- Dumping structure for table sniffs_new_test.quest_template_locale
DROP TABLE IF EXISTS `quest_template_locale`;
CREATE TABLE IF NOT EXISTS `quest_template_locale` (
  `ID` mediumint(8) unsigned NOT NULL DEFAULT '0',
  `locale` varchar(4) NOT NULL,
  `LogTitle` text,
  `LogDescription` text,
  `QuestDescription` text,
  `AreaDescription` text,
  `PortraitGiverText` text,
  `PortraitGiverName` text,
  `PortraitTurnInText` text,
  `PortraitTurnInName` text,
  `QuestCompletionLog` text,
  `VerifiedBuild` mediumint(8) unsigned NOT NULL DEFAULT '0',
  PRIMARY KEY (`ID`,`locale`)
) ENGINE=MyISAM DEFAULT CHARSET=utf8;

-- Data exporting was unselected.


-- Dumping structure for table sniffs_new_test.quest_update_complete
DROP TABLE IF EXISTS `quest_update_complete`;
CREATE TABLE IF NOT EXISTS `quest_update_complete` (
  `unixtimems` bigint(20) unsigned NOT NULL COMMENT 'when the packet was received',
  `quest_id` int(10) unsigned NOT NULL COMMENT 'quest template entry',
  PRIMARY KEY (`quest_id`,`unixtimems`)
) ENGINE=MyISAM DEFAULT CHARSET=latin1 COLLATE=latin1_general_ci ROW_FORMAT=COMPACT COMMENT='from SMSG_QUEST_UPDATE_COMPLETE';

-- Data exporting was unselected.


-- Dumping structure for table sniffs_new_test.quest_update_failed
DROP TABLE IF EXISTS `quest_update_failed`;
CREATE TABLE IF NOT EXISTS `quest_update_failed` (
  `unixtimems` bigint(20) unsigned NOT NULL COMMENT 'when the packet was received',
  `quest_id` int(10) unsigned NOT NULL COMMENT 'quest template entry',
  PRIMARY KEY (`quest_id`,`unixtimems`)
) ENGINE=MyISAM DEFAULT CHARSET=latin1 COLLATE=latin1_general_ci ROW_FORMAT=COMPACT COMMENT='from SMSG_QUEST_UPDATE_FAILED and SMSG_QUEST_UPDATE_FAILED_TIMER';

-- Data exporting was unselected.


-- Dumping structure for table sniffs_new_test.raid_target_icon_update
DROP TABLE IF EXISTS `raid_target_icon_update`;
CREATE TABLE IF NOT EXISTS `raid_target_icon_update` (
  `unixtimems` bigint(20) unsigned NOT NULL,
  `is_full_update` tinyint(3) unsigned NOT NULL,
  `icon` tinyint(4) NOT NULL,
  `target_guid` int(10) unsigned NOT NULL,
  `target_id` int(10) unsigned NOT NULL,
  `target_type` varchar(16) COLLATE latin1_general_ci NOT NULL,
  PRIMARY KEY (`unixtimems`,`icon`,`target_guid`,`target_type`)
) ENGINE=MyISAM DEFAULT CHARSET=latin1 COLLATE=latin1_general_ci;

-- Data exporting was unselected.


-- Dumping structure for table sniffs_new_test.replay_marked_creature
DROP TABLE IF EXISTS `replay_marked_creature`;
CREATE TABLE IF NOT EXISTS `replay_marked_creature` (
  `guid` int(10) unsigned NOT NULL,
  `marker` int(11) NOT NULL DEFAULT '0',
  PRIMARY KEY (`guid`)
) ENGINE=MyISAM DEFAULT CHARSET=latin1 COLLATE=latin1_general_ci COMMENT='creatures that have been manually marked from replay core';

-- Data exporting was unselected.


-- Dumping structure for table sniffs_new_test.replay_marked_gameobject
DROP TABLE IF EXISTS `replay_marked_gameobject`;
CREATE TABLE IF NOT EXISTS `replay_marked_gameobject` (
  `guid` int(10) unsigned NOT NULL,
  `marker` int(11) NOT NULL DEFAULT '0',
  PRIMARY KEY (`guid`)
) ENGINE=MyISAM DEFAULT CHARSET=latin1 COLLATE=latin1_general_ci COMMENT='gameobjects that have been manually marked from replay core';

-- Data exporting was unselected.


-- Dumping structure for table sniffs_new_test.sniff_data
DROP TABLE IF EXISTS `sniff_data`;
CREATE TABLE IF NOT EXISTS `sniff_data` (
  `sniff_build` int(10) unsigned NOT NULL DEFAULT '0',
  `sniff_id` int(11) NOT NULL DEFAULT '0',
  `object_type` enum('None','Spell','Map','LFGDungeon','Battleground','Unit','GameObject','CreatureDifficulty','Item','Quest','Opcode','PageText','NpcText','BroadcastText','Gossip','Zone','Area','AreaTrigger','Phase','Player','Achievement','CreatureFamily','Criteria','Currency','Difficulty','Faction','MailTemplate','WorldState','Sound','Taxi') COLLATE latin1_general_ci NOT NULL DEFAULT 'None',
  `id` int(10) NOT NULL DEFAULT '0',
  `data` text COLLATE latin1_general_ci NOT NULL,
  UNIQUE KEY `SniffName` (`object_type`,`id`,`data`(255),`sniff_id`)
) ENGINE=MyISAM DEFAULT CHARSET=latin1 COLLATE=latin1_general_ci COMMENT='information about the contents of each sniff included in the database\r\ncan be used to figure out where specific data can be found';

-- Data exporting was unselected.


-- Dumping structure for table sniffs_new_test.sniff_file
DROP TABLE IF EXISTS `sniff_file`;
CREATE TABLE IF NOT EXISTS `sniff_file` (
  `id` smallint(5) unsigned NOT NULL,
  `build` mediumint(8) unsigned NOT NULL DEFAULT '0',
  `author` varchar(32) COLLATE utf8_unicode_ci NOT NULL DEFAULT '',
  `name` varchar(128) COLLATE utf8_unicode_ci NOT NULL,
  PRIMARY KEY (`id`),
  UNIQUE KEY `name` (`name`)
) ENGINE=MyISAM DEFAULT CHARSET=utf8 COLLATE=utf8_unicode_ci COMMENT='the names of all sniffs included in the database';

-- Data exporting was unselected.


-- Dumping structure for table sniffs_new_test.sound_unique_source
DROP TABLE IF EXISTS `sound_unique_source`;
CREATE TABLE IF NOT EXISTS `sound_unique_source` (
  `source_id` int(10) unsigned NOT NULL DEFAULT '0',
  `source_type` varchar(16) COLLATE latin1_general_ci NOT NULL DEFAULT '',
  `sound` int(10) unsigned NOT NULL DEFAULT '0',
  `sniff_id_list` text COLLATE latin1_general_ci NOT NULL,
  PRIMARY KEY (`source_id`,`source_type`,`sound`)
) ENGINE=MyISAM DEFAULT CHARSET=latin1 COLLATE=latin1_general_ci ROW_FORMAT=COMPACT COMMENT='unique source object and sound id combinations';

-- Data exporting was unselected.


-- Dumping structure for table sniffs_new_test.spell_aura_flags
DROP TABLE IF EXISTS `spell_aura_flags`;
CREATE TABLE IF NOT EXISTS `spell_aura_flags` (
  `spell_id` int(10) unsigned NOT NULL,
  `flags` int(10) unsigned NOT NULL,
  `sniff_build` mediumint(8) unsigned NOT NULL DEFAULT '0',
  PRIMARY KEY (`spell_id`,`flags`)
) ENGINE=MyISAM DEFAULT CHARSET=latin1 COLLATE=latin1_general_ci COMMENT='all aura flags seen for a given spell id';

-- Data exporting was unselected.


-- Dumping structure for table sniffs_new_test.spell_cast_failed
DROP TABLE IF EXISTS `spell_cast_failed`;
CREATE TABLE IF NOT EXISTS `spell_cast_failed` (
  `unixtimems` bigint(20) unsigned NOT NULL COMMENT 'when the packet was received',
  `caster_guid` int(10) unsigned NOT NULL,
  `caster_id` int(10) unsigned NOT NULL,
  `caster_type` varchar(16) COLLATE latin1_general_ci NOT NULL,
  `spell_id` int(10) unsigned NOT NULL,
  `visual_id` int(10) unsigned NOT NULL DEFAULT '0',
  `reason` int(10) unsigned NOT NULL DEFAULT '0',
  PRIMARY KEY (`caster_id`,`caster_type`,`spell_id`,`unixtimems`,`caster_guid`)
) ENGINE=MyISAM DEFAULT CHARSET=latin1 COLLATE=latin1_general_ci ROW_FORMAT=COMPACT COMMENT='from SMSG_SPELL_FAILURE and SMSG_SPELL_FAILED_OTHER';

-- Data exporting was unselected.


-- Dumping structure for table sniffs_new_test.spell_cast_go
DROP TABLE IF EXISTS `spell_cast_go`;
CREATE TABLE IF NOT EXISTS `spell_cast_go` (
  `unixtimems` bigint(20) unsigned NOT NULL DEFAULT '0' COMMENT 'when the packet was received',
  `caster_guid` int(10) unsigned NOT NULL DEFAULT '0',
  `caster_id` int(10) unsigned NOT NULL DEFAULT '0',
  `caster_type` varchar(16) COLLATE latin1_general_ci NOT NULL DEFAULT '',
  `caster_unit_guid` int(10) unsigned NOT NULL DEFAULT '0',
  `caster_unit_id` int(10) unsigned NOT NULL DEFAULT '0',
  `caster_unit_type` varchar(16) COLLATE latin1_general_ci NOT NULL DEFAULT '',
  `spell_id` int(10) unsigned NOT NULL DEFAULT '0',
  `visual_id` int(10) unsigned NOT NULL DEFAULT '0',
  `cast_flags` int(10) unsigned NOT NULL DEFAULT '0',
  `cast_flags_ex` int(10) unsigned NOT NULL DEFAULT '0',
  `ammo_display_id` int(10) NOT NULL DEFAULT '0',
  `ammo_inventory_type` int(10) NOT NULL DEFAULT '0',
  `main_target_guid` int(10) unsigned NOT NULL DEFAULT '0',
  `main_target_id` int(10) unsigned NOT NULL DEFAULT '0',
  `main_target_type` varchar(50) COLLATE latin1_general_ci NOT NULL DEFAULT '',
  `hit_targets_count` tinyint(3) unsigned NOT NULL DEFAULT '0',
  `hit_targets_list_id` int(10) unsigned NOT NULL DEFAULT '0',
  `miss_targets_count` tinyint(3) unsigned NOT NULL DEFAULT '0',
  `miss_targets_list_id` int(10) unsigned NOT NULL DEFAULT '0',
  `src_position_id` int(10) unsigned NOT NULL DEFAULT '0',
  `dst_position_id` int(10) unsigned NOT NULL DEFAULT '0',
  `orientation` float NOT NULL DEFAULT '0',
  PRIMARY KEY (`unixtimems`,`caster_guid`,`caster_id`,`caster_type`,`spell_id`)
) ENGINE=MyISAM DEFAULT CHARSET=latin1 COLLATE=latin1_general_ci COMMENT='from SMSG_SPELL_GO\r\nsent when a spell is successfully casted';

-- Data exporting was unselected.


-- Dumping structure for table sniffs_new_test.spell_cast_go_position
DROP TABLE IF EXISTS `spell_cast_go_position`;
CREATE TABLE IF NOT EXISTS `spell_cast_go_position` (
  `id` int(10) unsigned NOT NULL,
  `position_x` float NOT NULL DEFAULT '0',
  `position_y` float NOT NULL DEFAULT '0',
  `position_z` float NOT NULL DEFAULT '0',
  PRIMARY KEY (`id`)
) ENGINE=MyISAM DEFAULT CHARSET=latin1 COLLATE=latin1_general_ci ROW_FORMAT=COMPACT COMMENT='source and destination locations from SMSG_SPELL_GO';

-- Data exporting was unselected.


-- Dumping structure for table sniffs_new_test.spell_cast_go_target
DROP TABLE IF EXISTS `spell_cast_go_target`;
CREATE TABLE IF NOT EXISTS `spell_cast_go_target` (
  `list_id` int(10) unsigned NOT NULL DEFAULT '0',
  `target_guid` int(10) unsigned NOT NULL DEFAULT '0',
  `target_id` int(10) unsigned NOT NULL DEFAULT '0',
  `target_type` varchar(16) COLLATE latin1_general_ci NOT NULL DEFAULT '',
  `miss_reason` tinyint(3) unsigned NOT NULL DEFAULT '0'
) ENGINE=MyISAM DEFAULT CHARSET=latin1 COLLATE=latin1_general_ci ROW_FORMAT=COMPACT COMMENT='hit and miss targets from SMSG_SPELL_GO';

-- Data exporting was unselected.


-- Dumping structure for table sniffs_new_test.spell_cast_start
DROP TABLE IF EXISTS `spell_cast_start`;
CREATE TABLE IF NOT EXISTS `spell_cast_start` (
  `unixtimems` bigint(20) unsigned NOT NULL DEFAULT '0' COMMENT 'when the packet was received',
  `caster_guid` int(10) unsigned NOT NULL DEFAULT '0',
  `caster_id` int(10) unsigned NOT NULL DEFAULT '0',
  `caster_type` varchar(16) COLLATE latin1_general_ci NOT NULL DEFAULT '',
  `caster_unit_guid` int(10) unsigned NOT NULL DEFAULT '0',
  `caster_unit_id` int(10) unsigned NOT NULL DEFAULT '0',
  `caster_unit_type` varchar(16) COLLATE latin1_general_ci NOT NULL DEFAULT '',
  `spell_id` int(10) unsigned NOT NULL DEFAULT '0',
  `visual_id` int(10) unsigned NOT NULL DEFAULT '0',
  `cast_time` int(10) unsigned NOT NULL DEFAULT '0',
  `cast_flags` int(10) unsigned NOT NULL DEFAULT '0',
  `cast_flags_ex` int(10) unsigned NOT NULL DEFAULT '0',
  `ammo_display_id` int(10) NOT NULL DEFAULT '0',
  `ammo_inventory_type` int(10) NOT NULL DEFAULT '0',
  `target_guid` int(10) unsigned NOT NULL DEFAULT '0',
  `target_id` int(10) unsigned NOT NULL DEFAULT '0',
  `target_type` varchar(50) COLLATE latin1_general_ci NOT NULL DEFAULT '',
  PRIMARY KEY (`unixtimems`,`caster_guid`,`caster_id`,`caster_type`,`spell_id`)
) ENGINE=MyISAM DEFAULT CHARSET=latin1 COLLATE=latin1_general_ci COMMENT='from SMSG_SPELL_START\r\nsent when somebody starts casting a spell';

-- Data exporting was unselected.


-- Dumping structure for table sniffs_new_test.spell_chain_update
DROP TABLE IF EXISTS `spell_chain_update`;
CREATE TABLE IF NOT EXISTS `spell_chain_update` (
  `unixtimems` bigint(20) unsigned NOT NULL DEFAULT '0' COMMENT 'when the packet was received',
  `caster_guid` int(10) unsigned NOT NULL DEFAULT '0',
  `caster_id` int(10) unsigned NOT NULL DEFAULT '0',
  `caster_type` varchar(16) COLLATE latin1_general_ci NOT NULL DEFAULT '',
  `spell_id` int(10) unsigned NOT NULL DEFAULT '0',
  `targets_count` tinyint(3) unsigned NOT NULL DEFAULT '0',
  `targets_list_id` int(10) unsigned NOT NULL DEFAULT '0',
  PRIMARY KEY (`unixtimems`,`caster_guid`,`caster_id`,`caster_type`,`spell_id`)
) ENGINE=MyISAM DEFAULT CHARSET=latin1 COLLATE=latin1_general_ci ROW_FORMAT=DYNAMIC COMMENT='from SMSG_SPELL_UPDATE_CHAIN_TARGETS';

-- Data exporting was unselected.


-- Dumping structure for table sniffs_new_test.spell_chain_update_target
DROP TABLE IF EXISTS `spell_chain_update_target`;
CREATE TABLE IF NOT EXISTS `spell_chain_update_target` (
  `list_id` int(10) unsigned NOT NULL DEFAULT '0',
  `target_guid` int(10) unsigned NOT NULL DEFAULT '0',
  `target_id` int(10) unsigned NOT NULL DEFAULT '0',
  `target_type` varchar(16) COLLATE latin1_general_ci NOT NULL DEFAULT ''
) ENGINE=MyISAM DEFAULT CHARSET=latin1 COLLATE=latin1_general_ci ROW_FORMAT=DYNAMIC COMMENT='targets from SMSG_SPELL_UPDATE_CHAIN_TARGETS';

-- Data exporting was unselected.


-- Dumping structure for table sniffs_new_test.spell_channel_start
DROP TABLE IF EXISTS `spell_channel_start`;
CREATE TABLE IF NOT EXISTS `spell_channel_start` (
  `unixtimems` bigint(20) unsigned NOT NULL DEFAULT '0' COMMENT 'when the packet was received',
  `caster_guid` int(10) unsigned NOT NULL DEFAULT '0',
  `caster_id` int(10) unsigned NOT NULL DEFAULT '0',
  `caster_type` varchar(16) COLLATE latin1_general_ci NOT NULL DEFAULT '',
  `spell_id` int(10) unsigned NOT NULL DEFAULT '0',
  `visual_id` int(10) unsigned NOT NULL DEFAULT '0',
  `duration` int(10) NOT NULL DEFAULT '0',
  PRIMARY KEY (`unixtimems`,`caster_guid`,`caster_id`,`caster_type`,`spell_id`)
) ENGINE=MyISAM DEFAULT CHARSET=latin1 COLLATE=latin1_general_ci ROW_FORMAT=COMPACT COMMENT='from SMSG_SPELL_CHANNEL_START\r\nsent when somebody starts channeling a spell';

-- Data exporting was unselected.


-- Dumping structure for table sniffs_new_test.spell_channel_update
DROP TABLE IF EXISTS `spell_channel_update`;
CREATE TABLE IF NOT EXISTS `spell_channel_update` (
  `unixtimems` bigint(20) unsigned NOT NULL DEFAULT '0' COMMENT 'when the packet was received',
  `caster_guid` int(10) unsigned NOT NULL DEFAULT '0',
  `caster_id` int(10) unsigned NOT NULL DEFAULT '0',
  `caster_type` varchar(16) COLLATE latin1_general_ci NOT NULL DEFAULT '',
  `duration` int(10) NOT NULL DEFAULT '0',
  PRIMARY KEY (`unixtimems`,`caster_guid`,`caster_id`,`caster_type`,`duration`)
) ENGINE=MyISAM DEFAULT CHARSET=latin1 COLLATE=latin1_general_ci ROW_FORMAT=COMPACT COMMENT='from SMSG_SPELL_CHANNEL_UPDATE';

-- Data exporting was unselected.


-- Dumping structure for table sniffs_new_test.spell_script_target
DROP TABLE IF EXISTS `spell_script_target`;
CREATE TABLE IF NOT EXISTS `spell_script_target` (
  `spell_id` int(10) unsigned NOT NULL,
  `target_type` varchar(16) COLLATE latin1_general_ci NOT NULL,
  `target_id` int(10) unsigned NOT NULL,
  `sniff_build` mediumint(8) unsigned NOT NULL DEFAULT '0',
  PRIMARY KEY (`spell_id`,`target_type`,`target_id`)
) ENGINE=MyISAM DEFAULT CHARSET=latin1 COLLATE=latin1_general_ci;

-- Data exporting was unselected.


-- Dumping structure for table sniffs_new_test.spell_target_position
DROP TABLE IF EXISTS `spell_target_position`;
CREATE TABLE IF NOT EXISTS `spell_target_position` (
  `spell_id` mediumint(8) unsigned NOT NULL DEFAULT '0',
  `map` smallint(5) unsigned NOT NULL DEFAULT '0',
  `position_x` float NOT NULL DEFAULT '0',
  `position_y` float NOT NULL DEFAULT '0',
  `position_z` float NOT NULL DEFAULT '0',
  `orientation` float NOT NULL DEFAULT '0',
  `sniff_build` mediumint(8) unsigned NOT NULL DEFAULT '0',
  PRIMARY KEY (`spell_id`,`map`,`position_x`,`position_y`,`position_z`)
) ENGINE=MyISAM DEFAULT CHARSET=latin1 COLLATE=latin1_general_ci ROW_FORMAT=FIXED COMMENT='target coordinates for spells which need them defined in the database';

-- Data exporting was unselected.


-- Dumping structure for table sniffs_new_test.spell_unique_caster
DROP TABLE IF EXISTS `spell_unique_caster`;
CREATE TABLE IF NOT EXISTS `spell_unique_caster` (
  `caster_id` int(10) unsigned NOT NULL DEFAULT '0',
  `caster_type` varchar(16) COLLATE latin1_general_ci NOT NULL DEFAULT '',
  `spell_id` int(10) unsigned NOT NULL DEFAULT '0',
  `sniff_id_list` text COLLATE latin1_general_ci NOT NULL,
  PRIMARY KEY (`caster_id`,`caster_type`,`spell_id`)
) ENGINE=MyISAM DEFAULT CHARSET=latin1 COLLATE=latin1_general_ci ROW_FORMAT=COMPACT COMMENT='unique caster and spell combinations';

-- Data exporting was unselected.


-- Dumping structure for table sniffs_new_test.spell_unique_chain_updates
DROP TABLE IF EXISTS `spell_unique_chain_updates`;
CREATE TABLE IF NOT EXISTS `spell_unique_chain_updates` (
  `spell_id` int(10) unsigned NOT NULL DEFAULT '0',
  `sniff_id_list` text COLLATE latin1_general_ci NOT NULL,
  PRIMARY KEY (`spell_id`)
) ENGINE=MyISAM DEFAULT CHARSET=latin1 COLLATE=latin1_general_ci ROW_FORMAT=COMPACT COMMENT='spell ids that have been sent in SMSG_SPELL_UPDATE_CHAIN_TARGETS';

-- Data exporting was unselected.


-- Dumping structure for table sniffs_new_test.trainer
DROP TABLE IF EXISTS `trainer`;
CREATE TABLE IF NOT EXISTS `trainer` (
  `id` int(10) unsigned NOT NULL DEFAULT '0',
  `type` tinyint(2) unsigned NOT NULL DEFAULT '2',
  `greeting` varchar(128) NOT NULL DEFAULT '',
  `sniff_build` mediumint(8) unsigned NOT NULL DEFAULT '0',
  PRIMARY KEY (`id`,`type`,`greeting`)
) ENGINE=MyISAM DEFAULT CHARSET=utf8;

-- Data exporting was unselected.


-- Dumping structure for table sniffs_new_test.trainer_spell
DROP TABLE IF EXISTS `trainer_spell`;
CREATE TABLE IF NOT EXISTS `trainer_spell` (
  `trainer_id` int(10) unsigned NOT NULL DEFAULT '0',
  `spell_id` int(10) unsigned NOT NULL DEFAULT '0',
  `money_cost` int(10) unsigned NOT NULL DEFAULT '0' COMMENT 'could have reputation discount factored in',
  `required_skill_id` int(10) unsigned NOT NULL DEFAULT '0',
  `required_skill_value` int(10) unsigned NOT NULL DEFAULT '0',
  `required_ability1` int(10) unsigned NOT NULL DEFAULT '0',
  `required_ability2` int(10) unsigned NOT NULL DEFAULT '0',
  `required_ability3` int(10) unsigned NOT NULL DEFAULT '0',
  `required_level` tinyint(3) unsigned NOT NULL DEFAULT '0',
  `sniff_build` mediumint(8) unsigned NOT NULL DEFAULT '0',
  PRIMARY KEY (`trainer_id`,`spell_id`,`money_cost`)
) ENGINE=MyISAM DEFAULT CHARSET=latin1 COLLATE=latin1_general_ci;

-- Data exporting was unselected.


-- Dumping structure for table sniffs_new_test.weather_update
DROP TABLE IF EXISTS `weather_update`;
CREATE TABLE IF NOT EXISTS `weather_update` (
  `unixtimems` bigint(20) unsigned NOT NULL DEFAULT '0',
  `map_id` int(11) unsigned NOT NULL DEFAULT '0',
  `zone_id` int(11) NOT NULL DEFAULT '0',
  `weather_state` int(11) unsigned NOT NULL DEFAULT '0',
  `grade` float NOT NULL DEFAULT '0',
  `sound` int(11) NOT NULL DEFAULT '0',
  `instant` tinyint(3) unsigned NOT NULL DEFAULT '0',
  PRIMARY KEY (`unixtimems`)
) ENGINE=MyISAM DEFAULT CHARSET=latin1 COLLATE=latin1_general_ci COMMENT='from SMSG_WEATHER';

-- Data exporting was unselected.


-- Dumping structure for table sniffs_new_test.world_state_init
DROP TABLE IF EXISTS `world_state_init`;
CREATE TABLE IF NOT EXISTS `world_state_init` (
  `unixtimems` bigint(20) unsigned NOT NULL DEFAULT '0',
  `map` int(10) unsigned NOT NULL DEFAULT '0',
  `zone_id` int(11) NOT NULL DEFAULT '0',
  `area_id` int(11) NOT NULL DEFAULT '0',
  `variable` int(11) NOT NULL DEFAULT '0',
  `value` int(11) NOT NULL DEFAULT '0',
  PRIMARY KEY (`unixtimems`,`variable`,`value`,`area_id`,`zone_id`,`map`)
) ENGINE=MyISAM DEFAULT CHARSET=latin1 COLLATE=latin1_general_ci COMMENT='from SMSG_INIT_WORLD_STATES';

-- Data exporting was unselected.


-- Dumping structure for table sniffs_new_test.world_state_update
DROP TABLE IF EXISTS `world_state_update`;
CREATE TABLE IF NOT EXISTS `world_state_update` (
  `unixtimems` bigint(20) unsigned NOT NULL DEFAULT '0',
  `variable` int(11) NOT NULL DEFAULT '0',
  `value` int(11) NOT NULL DEFAULT '0',
  PRIMARY KEY (`unixtimems`,`variable`,`value`)
) ENGINE=MyISAM DEFAULT CHARSET=latin1 COLLATE=latin1_general_ci ROW_FORMAT=COMPACT COMMENT='from SMSG_UPDATE_WORLD_STATE';

-- Data exporting was unselected.


-- Dumping structure for table sniffs_new_test.world_text
DROP TABLE IF EXISTS `world_text`;
CREATE TABLE IF NOT EXISTS `world_text` (
  `unixtimems` bigint(20) unsigned NOT NULL DEFAULT '0' COMMENT 'when the packet was received',
  `text` longtext COMMENT 'the actual text that was sent',
  `chat_type` tinyint(3) unsigned NOT NULL DEFAULT '0' COMMENT 'chat type',
  `language` tinyint(3) NOT NULL DEFAULT '0' COMMENT 'references Languages.dbc',
  PRIMARY KEY (`unixtimems`)
) ENGINE=MyISAM DEFAULT CHARSET=utf8 ROW_FORMAT=DYNAMIC COMMENT='texts sent by the server which did not originate from a creature or player';

-- Data exporting was unselected.


-- Dumping structure for table sniffs_new_test.xp_gain_aborted
DROP TABLE IF EXISTS `xp_gain_aborted`;
CREATE TABLE IF NOT EXISTS `xp_gain_aborted` (
  `unixtimems` bigint(20) unsigned NOT NULL DEFAULT '0',
  `victim_guid` int(10) unsigned NOT NULL DEFAULT '0',
  `victim_id` int(10) unsigned NOT NULL DEFAULT '0',
  `victim_type` varchar(16) COLLATE latin1_general_ci NOT NULL DEFAULT '',
  `amount` int(10) unsigned NOT NULL DEFAULT '0',
  `gain_reason` tinyint(3) unsigned NOT NULL DEFAULT '0',
  `abort_reason` tinyint(3) unsigned NOT NULL DEFAULT '0',
  PRIMARY KEY (`unixtimems`,`victim_guid`,`victim_type`)
) ENGINE=MyISAM DEFAULT CHARSET=latin1 COLLATE=latin1_general_ci ROW_FORMAT=COMPACT COMMENT='from SMSG_XP_GAIN_ABORTED';

-- Data exporting was unselected.


-- Dumping structure for table sniffs_new_test.xp_gain_log
DROP TABLE IF EXISTS `xp_gain_log`;
CREATE TABLE IF NOT EXISTS `xp_gain_log` (
  `unixtimems` bigint(20) unsigned NOT NULL DEFAULT '0',
  `victim_guid` int(10) unsigned NOT NULL DEFAULT '0',
  `victim_id` int(10) unsigned NOT NULL DEFAULT '0',
  `victim_type` varchar(16) COLLATE latin1_general_ci NOT NULL DEFAULT '',
  `original_amount` int(10) unsigned NOT NULL DEFAULT '0',
  `reason` tinyint(3) unsigned NOT NULL DEFAULT '0',
  `amount` int(10) unsigned NOT NULL DEFAULT '0',
  `group_bonus` float unsigned NOT NULL DEFAULT '0',
  `raf_bonus` tinyint(3) unsigned NOT NULL DEFAULT '0',
  PRIMARY KEY (`unixtimems`,`victim_guid`,`victim_type`)
) ENGINE=MyISAM DEFAULT CHARSET=latin1 COLLATE=latin1_general_ci ROW_FORMAT=COMPACT COMMENT='from SMSG_LOG_XP_GAIN';

-- Data exporting was unselected.
/*!40101 SET SQL_MODE=IFNULL(@OLD_SQL_MODE, '') */;
/*!40014 SET FOREIGN_KEY_CHECKS=IF(@OLD_FOREIGN_KEY_CHECKS IS NULL, 1, @OLD_FOREIGN_KEY_CHECKS) */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;

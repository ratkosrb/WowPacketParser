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
  `ID` int(10) unsigned NOT NULL,
  `Text` varchar(1024) NOT NULL DEFAULT '',
  `Text1` varchar(1024) NOT NULL DEFAULT '',
  `LanguageID` int(11) NOT NULL DEFAULT '0',
  `ConditionID` int(11) NOT NULL DEFAULT '0',
  `EmotesID` int(11) NOT NULL DEFAULT '0',
  `Flags` int(11) NOT NULL DEFAULT '0',
  `ChatBubbleDurationMs` int(11) NOT NULL DEFAULT '0',
  `SoundEntriesID1` int(11) NOT NULL DEFAULT '0',
  `SoundEntriesID2` int(11) NOT NULL DEFAULT '0',
  `EmoteID1` int(11) NOT NULL DEFAULT '0',
  `EmoteID2` int(11) NOT NULL DEFAULT '0',
  `EmoteID3` int(11) NOT NULL DEFAULT '0',
  `EmoteDelay1` int(11) NOT NULL DEFAULT '0',
  `EmoteDelay2` int(11) NOT NULL DEFAULT '0',
  `EmoteDelay3` int(11) NOT NULL DEFAULT '0',
  `VerifiedBuild` int(11) NOT NULL DEFAULT '0',
  PRIMARY KEY (`ID`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

-- Data exporting was unselected.


-- Dumping structure for table sniffs_new_test.broadcast_text_locale
DROP TABLE IF EXISTS `broadcast_text_locale`;
CREATE TABLE IF NOT EXISTS `broadcast_text_locale` (
  `ID` mediumint(8) unsigned NOT NULL DEFAULT '0',
  `locale` varchar(4) NOT NULL,
  `Text_lang` text,
  `Text1_lang` text,
  `VerifiedBuild` smallint(5) DEFAULT '0',
  PRIMARY KEY (`ID`,`locale`)
) ENGINE=MyISAM DEFAULT CHARSET=utf8;

-- Data exporting was unselected.


-- Dumping structure for table sniffs_new_test.client_creature_interact
DROP TABLE IF EXISTS `client_creature_interact`;
CREATE TABLE IF NOT EXISTS `client_creature_interact` (
  `unixtimems` bigint(20) unsigned NOT NULL COMMENT 'when the packet was sent',
  `guid` int(10) unsigned NOT NULL COMMENT 'creature spawn guid',
  PRIMARY KEY (`guid`,`unixtimems`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_unicode_ci ROW_FORMAT=COMPACT COMMENT='times when the client talked to the creature';

-- Data exporting was unselected.


-- Dumping structure for table sniffs_new_test.client_gameobject_use
DROP TABLE IF EXISTS `client_gameobject_use`;
CREATE TABLE IF NOT EXISTS `client_gameobject_use` (
  `unixtimems` bigint(20) unsigned NOT NULL COMMENT 'when the packet was sent',
  `guid` int(10) unsigned NOT NULL COMMENT 'gameobject spawn guid',
  PRIMARY KEY (`guid`,`unixtimems`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_unicode_ci ROW_FORMAT=COMPACT COMMENT='times when client used a gameobject\r\nfrom CMSG_GAME_OBJ_USE and CMSG_GAME_OBJ_REPORT_USE';

-- Data exporting was unselected.


-- Dumping structure for table sniffs_new_test.client_item_use
DROP TABLE IF EXISTS `client_item_use`;
CREATE TABLE IF NOT EXISTS `client_item_use` (
  `unixtimems` bigint(20) unsigned NOT NULL COMMENT 'when the packet was sent',
  `entry` int(10) unsigned NOT NULL COMMENT 'item template entry',
  PRIMARY KEY (`entry`,`unixtimems`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_unicode_ci ROW_FORMAT=COMPACT COMMENT='times when client used an item\r\nfrom CMSG_USE_ITEM';

-- Data exporting was unselected.


-- Dumping structure for table sniffs_new_test.client_quest_accept
DROP TABLE IF EXISTS `client_quest_accept`;
CREATE TABLE IF NOT EXISTS `client_quest_accept` (
  `unixtimems` bigint(20) unsigned NOT NULL COMMENT 'when the packet was sent',
  `object_guid` int(10) unsigned NOT NULL COMMENT 'guid of the quest ender object',
  `object_id` int(10) unsigned NOT NULL COMMENT 'entry of the quest ender object',
  `object_type` varchar(16) COLLATE utf8_unicode_ci NOT NULL COMMENT 'type of the quest ender object',
  `quest_id` int(10) unsigned NOT NULL COMMENT 'references quest_template',
  PRIMARY KEY (`unixtimems`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_unicode_ci ROW_FORMAT=COMPACT COMMENT='times when a quest was accepted by client';

-- Data exporting was unselected.


-- Dumping structure for table sniffs_new_test.client_quest_complete
DROP TABLE IF EXISTS `client_quest_complete`;
CREATE TABLE IF NOT EXISTS `client_quest_complete` (
  `unixtimems` bigint(20) unsigned NOT NULL COMMENT 'when the packet was sent',
  `object_guid` int(10) unsigned NOT NULL COMMENT 'guid of the quest ender object',
  `object_id` int(10) unsigned NOT NULL COMMENT 'entry of the quest ender object',
  `object_type` varchar(16) COLLATE utf8_unicode_ci NOT NULL COMMENT 'type of the quest ender object',
  `quest_id` int(10) unsigned NOT NULL COMMENT 'references quest_template',
  PRIMARY KEY (`unixtimems`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_unicode_ci ROW_FORMAT=COMPACT COMMENT='times when a quest was turned in by client';

-- Data exporting was unselected.


-- Dumping structure for table sniffs_new_test.client_reclaim_corpse
DROP TABLE IF EXISTS `client_reclaim_corpse`;
CREATE TABLE IF NOT EXISTS `client_reclaim_corpse` (
  `unixtimems` bigint(20) unsigned NOT NULL COMMENT 'when the packet was sent',
  PRIMARY KEY (`unixtimems`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_unicode_ci ROW_FORMAT=COMPACT COMMENT='from CMSG_RECLAIM_CORPSE';

-- Data exporting was unselected.


-- Dumping structure for table sniffs_new_test.client_release_spirit
DROP TABLE IF EXISTS `client_release_spirit`;
CREATE TABLE IF NOT EXISTS `client_release_spirit` (
  `unixtimems` bigint(20) unsigned NOT NULL COMMENT 'when the packet was sent',
  PRIMARY KEY (`unixtimems`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_unicode_ci ROW_FORMAT=COMPACT COMMENT='from CMSG_CLIENT_PORT_GRAVEYARD';

-- Data exporting was unselected.


-- Dumping structure for table sniffs_new_test.creature
DROP TABLE IF EXISTS `creature`;
CREATE TABLE IF NOT EXISTS `creature` (
  `guid` bigint(20) unsigned NOT NULL DEFAULT '0',
  `id` mediumint(8) unsigned NOT NULL DEFAULT '0' COMMENT 'Creature Identifier',
  `map` smallint(5) unsigned DEFAULT '0' COMMENT 'Map Identifier',
  `zone_id` smallint(5) unsigned NOT NULL DEFAULT '0' COMMENT 'Zone Identifier',
  `area_id` smallint(5) unsigned NOT NULL DEFAULT '0' COMMENT 'Area Identifier',
  `position_x` float NOT NULL DEFAULT '0',
  `position_y` float NOT NULL DEFAULT '0',
  `position_z` float NOT NULL DEFAULT '0',
  `orientation` float NOT NULL DEFAULT '0',
  `wander_distance` float NOT NULL DEFAULT '0' COMMENT 'maximum distance traveled from initial position',
  `movement_type` tinyint(3) unsigned NOT NULL DEFAULT '0' COMMENT 'guessed movement generator',
  `hover` tinyint(3) unsigned NOT NULL DEFAULT '0',
  `temp` tinyint(3) unsigned NOT NULL DEFAULT '0' COMMENT 'is this a temporary summon',
  `pet` tinyint(3) unsigned NOT NULL DEFAULT '0' COMMENT 'is this a pet',
  `summon_spell` int(10) unsigned NOT NULL DEFAULT '0',
  `scale` float NOT NULL DEFAULT '0',
  `display_id` int(10) unsigned NOT NULL DEFAULT '0',
  `native_display_id` int(10) unsigned NOT NULL DEFAULT '0',
  `mount_display_id` int(10) unsigned NOT NULL DEFAULT '0',
  `faction` int(10) unsigned NOT NULL DEFAULT '0',
  `level` int(10) unsigned NOT NULL DEFAULT '0',
  `npc_flags` int(10) unsigned NOT NULL DEFAULT '0',
  `unit_flags` int(10) unsigned NOT NULL DEFAULT '0',
  `unit_flags2` int(10) unsigned NOT NULL DEFAULT '0',
  `current_health` int(10) unsigned NOT NULL DEFAULT '0',
  `max_health` int(10) unsigned NOT NULL DEFAULT '0',
  `current_mana` int(10) unsigned NOT NULL DEFAULT '0',
  `max_mana` int(10) unsigned NOT NULL DEFAULT '0',
  `aura_state` int(10) unsigned NOT NULL DEFAULT '0',
  `emote_state` int(10) unsigned NOT NULL DEFAULT '0',
  `stand_state` tinyint(3) unsigned NOT NULL DEFAULT '0' COMMENT 'from UNIT_FIELD_BYTES_1',
  `pet_talent_points` tinyint(3) unsigned NOT NULL DEFAULT '0' COMMENT 'from UNIT_FIELD_BYTES_1',
  `vis_flags` tinyint(3) unsigned NOT NULL DEFAULT '0' COMMENT 'from UNIT_FIELD_BYTES_1',
  `anim_tier` tinyint(3) unsigned NOT NULL DEFAULT '0' COMMENT 'from UNIT_FIELD_BYTES_1',
  `sheath_state` tinyint(3) unsigned NOT NULL DEFAULT '0' COMMENT 'from UNIT_FIELD_BYTES_2',
  `pvp_flags` tinyint(3) unsigned NOT NULL DEFAULT '0' COMMENT 'from UNIT_FIELD_BYTES_2',
  `pet_flags` tinyint(3) unsigned NOT NULL DEFAULT '0' COMMENT 'from UNIT_FIELD_BYTES_2',
  `shapeshift_form` tinyint(3) unsigned NOT NULL DEFAULT '0' COMMENT 'from UNIT_FIELD_BYTES_2',
  `speed_walk` float NOT NULL DEFAULT '0',
  `speed_run` float NOT NULL DEFAULT '0',
  `bounding_radius` float NOT NULL DEFAULT '0',
  `combat_reach` float NOT NULL DEFAULT '0',
  `mod_melee_haste` float NOT NULL DEFAULT '0',
  `mod_ranged_haste` float NOT NULL DEFAULT '0',
  `base_attack_time` int(10) unsigned NOT NULL DEFAULT '0',
  `ranged_attack_time` int(10) unsigned NOT NULL DEFAULT '0',
  `main_hand_slot_item` int(10) unsigned NOT NULL DEFAULT '0',
  `off_hand_slot_item` int(10) unsigned NOT NULL DEFAULT '0',
  `ranged_slot_item` int(10) unsigned NOT NULL DEFAULT '0',
  `auras` text,
  `sniff_id` smallint(5) unsigned NOT NULL DEFAULT '0' COMMENT 'points to sniff_file table',
  `sniff_build` smallint(5) unsigned DEFAULT '0',
  PRIMARY KEY (`guid`),
  KEY `idx_map` (`map`),
  KEY `idx_id` (`id`)
) ENGINE=MyISAM DEFAULT CHARSET=utf8 ROW_FORMAT=DYNAMIC COMMENT='Creature System';

-- Data exporting was unselected.


-- Dumping structure for table sniffs_new_test.creature_attack_log
DROP TABLE IF EXISTS `creature_attack_log`;
CREATE TABLE IF NOT EXISTS `creature_attack_log` (
  `unixtimems` bigint(20) unsigned NOT NULL DEFAULT '0',
  `guid` int(10) unsigned NOT NULL,
  `victim_guid` int(10) unsigned NOT NULL DEFAULT '0',
  `victim_id` int(10) unsigned NOT NULL DEFAULT '0',
  `victim_type` varchar(16) COLLATE utf8_unicode_ci NOT NULL DEFAULT '',
  `hit_info` int(10) unsigned NOT NULL DEFAULT '0',
  `damage` int(10) unsigned NOT NULL DEFAULT '0',
  `original_damage` int(10) unsigned NOT NULL DEFAULT '0',
  `overkill_damage` int(10) NOT NULL DEFAULT '0',
  `blocked_damage` int(10) NOT NULL DEFAULT '0',
  `victim_state` int(10) unsigned NOT NULL DEFAULT '0',
  `attacker_state` int(10) NOT NULL DEFAULT '0',
  `spell_id` int(10) unsigned NOT NULL DEFAULT '0',
  PRIMARY KEY (`guid`,`unixtimems`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_unicode_ci ROW_FORMAT=COMPACT COMMENT='from SMSG_ATTACKER_STATE_UPDATE';

-- Data exporting was unselected.


-- Dumping structure for table sniffs_new_test.creature_attack_start
DROP TABLE IF EXISTS `creature_attack_start`;
CREATE TABLE IF NOT EXISTS `creature_attack_start` (
  `unixtimems` bigint(20) unsigned NOT NULL DEFAULT '0',
  `guid` int(10) unsigned NOT NULL,
  `victim_guid` int(10) unsigned NOT NULL DEFAULT '0',
  `victim_id` int(10) unsigned NOT NULL DEFAULT '0',
  `victim_type` varchar(16) COLLATE utf8_unicode_ci NOT NULL DEFAULT '',
  PRIMARY KEY (`guid`,`unixtimems`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_unicode_ci COMMENT='from SMSG_ATTACK_START';

-- Data exporting was unselected.


-- Dumping structure for table sniffs_new_test.creature_attack_stop
DROP TABLE IF EXISTS `creature_attack_stop`;
CREATE TABLE IF NOT EXISTS `creature_attack_stop` (
  `unixtimems` bigint(20) unsigned NOT NULL DEFAULT '0',
  `guid` int(10) unsigned NOT NULL,
  `victim_guid` int(10) unsigned NOT NULL DEFAULT '0',
  `victim_id` int(10) unsigned NOT NULL DEFAULT '0',
  `victim_type` varchar(16) COLLATE utf8_unicode_ci NOT NULL DEFAULT '',
  PRIMARY KEY (`guid`,`unixtimems`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_unicode_ci ROW_FORMAT=COMPACT COMMENT='from SMSG_ATTACK_STOP';

-- Data exporting was unselected.


-- Dumping structure for table sniffs_new_test.creature_auras_update
DROP TABLE IF EXISTS `creature_auras_update`;
CREATE TABLE IF NOT EXISTS `creature_auras_update` (
  `unixtimems` bigint(20) unsigned NOT NULL COMMENT 'when the packet was received',
  `guid` int(10) unsigned NOT NULL,
  `update_id` int(10) unsigned NOT NULL COMMENT 'counting aura update packets for this guid',
  `slot` int(10) unsigned NOT NULL,
  `spell_id` int(10) unsigned NOT NULL,
  `visual_id` int(10) unsigned NOT NULL,
  `aura_flags` int(10) unsigned NOT NULL,
  `active_flags` int(10) unsigned NOT NULL,
  `level` int(10) unsigned NOT NULL,
  `charges` int(10) unsigned NOT NULL,
  `content_tuning_id` int(10) NOT NULL,
  `duration` int(10) NOT NULL,
  `max_duration` int(10) NOT NULL,
  `caster_guid` int(10) unsigned NOT NULL,
  `caster_id` int(10) unsigned NOT NULL,
  `caster_type` varchar(16) COLLATE utf8_unicode_ci NOT NULL,
  PRIMARY KEY (`guid`,`update_id`,`slot`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_unicode_ci ROW_FORMAT=COMPACT COMMENT='aura updates from SMSG_AURA_UPDATE';

-- Data exporting was unselected.


-- Dumping structure for table sniffs_new_test.creature_create1_time
DROP TABLE IF EXISTS `creature_create1_time`;
CREATE TABLE IF NOT EXISTS `creature_create1_time` (
  `unixtimems` bigint(20) unsigned NOT NULL COMMENT 'when the packet was received',
  `guid` int(10) unsigned NOT NULL COMMENT 'creature spawn guid',
  `position_x` float NOT NULL,
  `position_y` float NOT NULL,
  `position_z` float NOT NULL,
  `orientation` float NOT NULL,
  PRIMARY KEY (`guid`,`unixtimems`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_unicode_ci ROW_FORMAT=COMPACT COMMENT='times when the object became visible to the client';

-- Data exporting was unselected.


-- Dumping structure for table sniffs_new_test.creature_create2_time
DROP TABLE IF EXISTS `creature_create2_time`;
CREATE TABLE IF NOT EXISTS `creature_create2_time` (
  `unixtimems` bigint(20) unsigned NOT NULL COMMENT 'when the packet was received',
  `guid` int(10) unsigned NOT NULL COMMENT 'gameobject spawn guid',
  `position_x` float NOT NULL,
  `position_y` float NOT NULL,
  `position_z` float NOT NULL,
  `orientation` float NOT NULL,
  PRIMARY KEY (`guid`,`unixtimems`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_unicode_ci ROW_FORMAT=COMPACT COMMENT='the time at which the object spawned';

-- Data exporting was unselected.


-- Dumping structure for table sniffs_new_test.creature_destroy_time
DROP TABLE IF EXISTS `creature_destroy_time`;
CREATE TABLE IF NOT EXISTS `creature_destroy_time` (
  `unixtimems` bigint(20) unsigned NOT NULL COMMENT 'when the packet was received',
  `guid` int(10) unsigned NOT NULL COMMENT 'creature spawn guid',
  PRIMARY KEY (`guid`,`unixtimems`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_unicode_ci COMMENT='times when the object was destroyed from the client''s prespective due to despawning, becoming invisible, or going out of range';

-- Data exporting was unselected.


-- Dumping structure for table sniffs_new_test.creature_display_info_addon
DROP TABLE IF EXISTS `creature_display_info_addon`;
CREATE TABLE IF NOT EXISTS `creature_display_info_addon` (
  `display_id` mediumint(8) unsigned NOT NULL DEFAULT '0',
  `bounding_radius` float NOT NULL DEFAULT '0',
  `combat_reach` float NOT NULL DEFAULT '0',
  `gender` tinyint(3) unsigned NOT NULL DEFAULT '2',
  PRIMARY KEY (`display_id`)
) ENGINE=MyISAM DEFAULT CHARSET=utf8 COMMENT='Creature System (Model related info)';

-- Data exporting was unselected.


-- Dumping structure for table sniffs_new_test.creature_emote
DROP TABLE IF EXISTS `creature_emote`;
CREATE TABLE IF NOT EXISTS `creature_emote` (
  `unixtimems` bigint(20) unsigned NOT NULL COMMENT 'when the packet was received',
  `guid` int(10) unsigned NOT NULL COMMENT 'creature spawn guid',
  `emote_id` int(10) unsigned NOT NULL COMMENT 'references Emotes.dbc',
  `emote_name` varchar(50) COLLATE utf8_unicode_ci DEFAULT NULL,
  PRIMARY KEY (`guid`,`unixtimems`,`emote_id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_unicode_ci COMMENT='from SMSG_EMOTE';

-- Data exporting was unselected.


-- Dumping structure for table sniffs_new_test.creature_equipment_values_update
DROP TABLE IF EXISTS `creature_equipment_values_update`;
CREATE TABLE IF NOT EXISTS `creature_equipment_values_update` (
  `unixtimems` bigint(20) unsigned NOT NULL COMMENT 'when the packet was received',
  `guid` int(10) unsigned NOT NULL COMMENT 'creature spawn guid',
  `slot` tinyint(3) unsigned NOT NULL,
  `item_id` int(10) unsigned NOT NULL,
  PRIMARY KEY (`guid`,`unixtimems`,`slot`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_unicode_ci ROW_FORMAT=COMPACT COMMENT='changes to visual item slots';

-- Data exporting was unselected.


-- Dumping structure for table sniffs_new_test.creature_equip_template
DROP TABLE IF EXISTS `creature_equip_template`;
CREATE TABLE IF NOT EXISTS `creature_equip_template` (
  `CreatureID` mediumint(8) unsigned NOT NULL DEFAULT '0',
  `ID` tinyint(3) unsigned NOT NULL DEFAULT '1',
  `ItemID1` mediumint(8) unsigned NOT NULL DEFAULT '0',
  `ItemID2` mediumint(8) unsigned NOT NULL DEFAULT '0',
  `ItemID3` mediumint(8) unsigned NOT NULL DEFAULT '0',
  `VerifiedBuild` smallint(5) unsigned DEFAULT '0',
  PRIMARY KEY (`CreatureID`,`ID`)
) ENGINE=MyISAM DEFAULT CHARSET=utf8;

-- Data exporting was unselected.


-- Dumping structure for table sniffs_new_test.creature_gossip
DROP TABLE IF EXISTS `creature_gossip`;
CREATE TABLE IF NOT EXISTS `creature_gossip` (
  `entry` int(10) unsigned NOT NULL DEFAULT '0',
  `gossip_menu_id` int(10) unsigned NOT NULL DEFAULT '0',
  `sniff_build` int(10) unsigned NOT NULL DEFAULT '0',
  PRIMARY KEY (`entry`,`gossip_menu_id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_unicode_ci;

-- Data exporting was unselected.


-- Dumping structure for table sniffs_new_test.creature_guid_values
DROP TABLE IF EXISTS `creature_guid_values`;
CREATE TABLE IF NOT EXISTS `creature_guid_values` (
  `guid` int(10) unsigned NOT NULL,
  `charm_guid` int(10) unsigned NOT NULL DEFAULT '0',
  `charm_id` int(10) unsigned NOT NULL DEFAULT '0',
  `charm_type` varchar(16) COLLATE utf8_unicode_ci NOT NULL DEFAULT '',
  `summon_guid` int(10) unsigned NOT NULL DEFAULT '0',
  `summon_id` int(10) unsigned NOT NULL DEFAULT '0',
  `summon_type` varchar(16) COLLATE utf8_unicode_ci NOT NULL DEFAULT '',
  `charmer_guid` int(10) unsigned NOT NULL DEFAULT '0',
  `charmer_id` int(10) unsigned NOT NULL DEFAULT '0',
  `charmer_type` varchar(16) COLLATE utf8_unicode_ci NOT NULL DEFAULT '',
  `creator_guid` int(10) unsigned NOT NULL DEFAULT '0',
  `creator_id` int(10) unsigned NOT NULL DEFAULT '0',
  `creator_type` varchar(16) COLLATE utf8_unicode_ci NOT NULL DEFAULT '',
  `summoner_guid` int(10) unsigned NOT NULL DEFAULT '0',
  `summoner_id` int(10) unsigned NOT NULL DEFAULT '0',
  `summoner_type` varchar(16) COLLATE utf8_unicode_ci NOT NULL DEFAULT '',
  `target_guid` int(10) unsigned NOT NULL DEFAULT '0',
  `target_id` int(10) unsigned NOT NULL DEFAULT '0',
  `target_type` varchar(16) COLLATE utf8_unicode_ci NOT NULL DEFAULT '',
  PRIMARY KEY (`guid`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_unicode_ci ROW_FORMAT=COMPACT COMMENT='initial value in guid type update fields';

-- Data exporting was unselected.


-- Dumping structure for table sniffs_new_test.creature_guid_values_update
DROP TABLE IF EXISTS `creature_guid_values_update`;
CREATE TABLE IF NOT EXISTS `creature_guid_values_update` (
  `unixtimems` bigint(20) unsigned NOT NULL DEFAULT '0',
  `guid` int(10) unsigned NOT NULL,
  `field_name` varchar(16) COLLATE utf8_unicode_ci NOT NULL DEFAULT '',
  `object_guid` int(10) unsigned NOT NULL DEFAULT '0',
  `object_id` int(10) unsigned NOT NULL DEFAULT '0',
  `object_type` varchar(16) COLLATE utf8_unicode_ci NOT NULL DEFAULT '',
  PRIMARY KEY (`guid`,`unixtimems`,`field_name`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_unicode_ci ROW_FORMAT=COMPACT COMMENT='changes to guid type update fields';

-- Data exporting was unselected.


-- Dumping structure for table sniffs_new_test.creature_loot
DROP TABLE IF EXISTS `creature_loot`;
CREATE TABLE IF NOT EXISTS `creature_loot` (
  `entry` int(10) unsigned NOT NULL COMMENT 'creature template id',
  `loot_id` int(10) unsigned NOT NULL COMMENT 'counter',
  `money` int(10) unsigned NOT NULL DEFAULT '0' COMMENT 'copper',
  `items_count` int(10) unsigned NOT NULL DEFAULT '0' COMMENT 'number of items dropped',
  PRIMARY KEY (`entry`,`loot_id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_unicode_ci COMMENT='each row represents a separate loot instance';

-- Data exporting was unselected.


-- Dumping structure for table sniffs_new_test.creature_loot_item
DROP TABLE IF EXISTS `creature_loot_item`;
CREATE TABLE IF NOT EXISTS `creature_loot_item` (
  `loot_id` int(10) unsigned NOT NULL COMMENT 'references creature_loot',
  `item_id` int(10) unsigned NOT NULL COMMENT 'item template id',
  `count` int(10) unsigned NOT NULL DEFAULT '1' COMMENT 'stack size'
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_unicode_ci COMMENT='individual item that is part of a loot instance';

-- Data exporting was unselected.


-- Dumping structure for table sniffs_new_test.creature_movement_client
DROP TABLE IF EXISTS `creature_movement_client`;
CREATE TABLE IF NOT EXISTS `creature_movement_client` (
  `unixtimems` bigint(20) unsigned NOT NULL,
  `guid` int(10) unsigned NOT NULL,
  `opcode` varchar(64) COLLATE utf8_unicode_ci NOT NULL DEFAULT '',
  `move_time` int(10) unsigned NOT NULL DEFAULT '0',
  `move_flags` int(10) unsigned NOT NULL DEFAULT '0',
  `map` smallint(5) unsigned NOT NULL DEFAULT '0',
  `position_x` float NOT NULL DEFAULT '0',
  `position_y` float NOT NULL DEFAULT '0',
  `position_z` float NOT NULL DEFAULT '0',
  `orientation` float NOT NULL DEFAULT '0',
  PRIMARY KEY (`guid`,`opcode`,`unixtimems`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_unicode_ci ROW_FORMAT=COMPACT COMMENT='client side movement';

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
  PRIMARY KEY (`guid`,`point`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_unicode_ci COMMENT='out of combat movement points from SMSG_ON_MONSTER_MOVE';

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
  PRIMARY KEY (`guid`,`point`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_unicode_ci ROW_FORMAT=COMPACT COMMENT='in combat movement points from SMSG_ON_MONSTER_MOVE';

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
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_unicode_ci ROW_FORMAT=COMPACT COMMENT='individual spline points for combat  movement';

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
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_unicode_ci COMMENT='individual spline points for out of combat movement';

-- Data exporting was unselected.


-- Dumping structure for table sniffs_new_test.creature_questitem
DROP TABLE IF EXISTS `creature_questitem`;
CREATE TABLE IF NOT EXISTS `creature_questitem` (
  `CreatureEntry` int(10) unsigned NOT NULL,
  `Idx` int(10) unsigned NOT NULL,
  `ItemId` int(10) unsigned NOT NULL DEFAULT '0',
  `VerifiedBuild` int(10) unsigned NOT NULL DEFAULT '0',
  PRIMARY KEY (`CreatureEntry`,`Idx`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_unicode_ci;

-- Data exporting was unselected.


-- Dumping structure for table sniffs_new_test.creature_speed_update
DROP TABLE IF EXISTS `creature_speed_update`;
CREATE TABLE IF NOT EXISTS `creature_speed_update` (
  `unixtimems` bigint(20) unsigned NOT NULL COMMENT 'when the packet was received',
  `guid` int(10) unsigned NOT NULL,
  `speed_type` tinyint(3) unsigned NOT NULL,
  `speed_rate` float unsigned NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_unicode_ci ROW_FORMAT=COMPACT COMMENT='changes to movement speed';

-- Data exporting was unselected.


-- Dumping structure for table sniffs_new_test.creature_stats
DROP TABLE IF EXISTS `creature_stats`;
CREATE TABLE IF NOT EXISTS `creature_stats` (
  `entry` int(10) unsigned NOT NULL COMMENT 'creature template id',
  `level` int(10) unsigned NOT NULL,
  `dmg_min` float DEFAULT NULL,
  `dmg_max` float DEFAULT NULL,
  `offhand_dmg_min` float DEFAULT NULL,
  `offhand_dmg_max` float DEFAULT NULL,
  `ranged_dmg_min` float DEFAULT NULL,
  `ranged_dmg_max` float DEFAULT NULL,
  `attack_power` int(10) unsigned DEFAULT NULL,
  `ranged_attack_power` int(10) unsigned DEFAULT NULL,
  `base_health` int(10) unsigned DEFAULT NULL,
  `base_mana` int(10) unsigned DEFAULT NULL,
  `strength` int(10) unsigned DEFAULT NULL,
  `agility` int(10) unsigned DEFAULT NULL,
  `stamina` int(10) unsigned DEFAULT NULL,
  `intellect` int(10) unsigned DEFAULT NULL,
  `spirit` int(10) unsigned DEFAULT NULL,
  `positive_strength` int(10) unsigned DEFAULT NULL,
  `positive_agility` int(10) unsigned DEFAULT NULL,
  `positive_stamina` int(10) unsigned DEFAULT NULL,
  `positive_intellect` int(10) unsigned DEFAULT NULL,
  `positive_spirit` int(10) unsigned DEFAULT NULL,
  `negative_strength` int(10) unsigned DEFAULT NULL,
  `negative_agility` int(10) unsigned DEFAULT NULL,
  `negative_stamina` int(10) unsigned DEFAULT NULL,
  `negative_intellect` int(10) unsigned DEFAULT NULL,
  `negative_spirit` int(10) unsigned DEFAULT NULL,
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
  PRIMARY KEY (`entry`,`level`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_unicode_ci COMMENT='stats data from SMSG_UPDATE_OBJECT, server only sends it if the creature is mind controlled';

-- Data exporting was unselected.


-- Dumping structure for table sniffs_new_test.creature_template
DROP TABLE IF EXISTS `creature_template`;
CREATE TABLE IF NOT EXISTS `creature_template` (
  `entry` mediumint(8) unsigned NOT NULL DEFAULT '0',
  `gossip_menu_id` mediumint(8) unsigned NOT NULL DEFAULT '0',
  `level_min` tinyint(3) unsigned NOT NULL DEFAULT '1',
  `level_max` tinyint(3) unsigned NOT NULL DEFAULT '1',
  `faction` smallint(5) unsigned NOT NULL DEFAULT '0',
  `npc_flags` int(10) unsigned NOT NULL DEFAULT '0',
  `speed_walk` float NOT NULL DEFAULT '1' COMMENT 'Result of 2.5/2.5, most common value',
  `speed_run` float NOT NULL DEFAULT '1.14286' COMMENT 'Result of 8.0/7.0, most common value',
  `scale` float NOT NULL DEFAULT '1.14286',
  `base_attack_time` int(10) unsigned NOT NULL DEFAULT '0',
  `ranged_attack_time` int(10) unsigned NOT NULL DEFAULT '0',
  `unit_flags` int(10) unsigned NOT NULL DEFAULT '0',
  `unit_flags2` int(10) unsigned NOT NULL DEFAULT '0',
  `vehicle_id` int(11) NOT NULL DEFAULT '0',
  `hover_height` float NOT NULL DEFAULT '0',
  `auras` text,
  PRIMARY KEY (`entry`)
) ENGINE=MyISAM DEFAULT CHARSET=utf8 ROW_FORMAT=FIXED COMMENT='Creature System';

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
  `VerifiedBuild` int(11) NOT NULL DEFAULT '0',
  PRIMARY KEY (`entry`,`locale`)
) ENGINE=MyISAM DEFAULT CHARSET=utf8;

-- Data exporting was unselected.


-- Dumping structure for table sniffs_new_test.creature_template_wdb
DROP TABLE IF EXISTS `creature_template_wdb`;
CREATE TABLE IF NOT EXISTS `creature_template_wdb` (
  `entry` int(10) unsigned NOT NULL,
  `kill_credit1` int(11) NOT NULL DEFAULT '0',
  `kill_credit2` int(11) NOT NULL DEFAULT '0',
  `display_total_count` int(11) NOT NULL DEFAULT '0',
  `display_total_probability` float NOT NULL DEFAULT '0',
  `display_id1` int(11) NOT NULL DEFAULT '0',
  `display_id2` int(11) NOT NULL DEFAULT '0',
  `display_id3` int(11) NOT NULL DEFAULT '0',
  `display_id4` int(11) NOT NULL DEFAULT '0',
  `display_scale1` float NOT NULL DEFAULT '0',
  `display_scale2` float NOT NULL DEFAULT '0',
  `display_scale3` float NOT NULL DEFAULT '0',
  `display_scale4` float NOT NULL DEFAULT '0',
  `display_probability1` float NOT NULL DEFAULT '0',
  `display_probability2` float NOT NULL DEFAULT '0',
  `display_probability3` float NOT NULL DEFAULT '0',
  `display_probability4` float NOT NULL DEFAULT '0',
  `name` varchar(256) NOT NULL DEFAULT '',
  `female_name` varchar(256) NOT NULL DEFAULT '',
  `subname` varchar(256) DEFAULT '',
  `title_alt` varchar(256) DEFAULT '',
  `icon_name` varchar(256) DEFAULT '',
  `health_scaling_expansion` int(11) NOT NULL DEFAULT '0',
  `required_expansion` int(11) NOT NULL DEFAULT '0',
  `vignette_id` int(11) NOT NULL DEFAULT '0',
  `unit_class` int(11) NOT NULL DEFAULT '0',
  `rank` int(11) NOT NULL DEFAULT '0',
  `beast_family` int(11) NOT NULL DEFAULT '0',
  `type` int(11) NOT NULL DEFAULT '0',
  `type_flags` int(10) unsigned NOT NULL DEFAULT '0',
  `type_flags2` int(10) unsigned NOT NULL DEFAULT '0',
  `pet_spell_list_id` int(11) NOT NULL DEFAULT '0',
  `health_multiplier` float NOT NULL DEFAULT '0',
  `mana_multiplier` float NOT NULL DEFAULT '0',
  `civilian` int(11) NOT NULL DEFAULT '0',
  `racial_leader` int(11) NOT NULL DEFAULT '0',
  `movement_id` int(11) NOT NULL DEFAULT '0',
  `sniff_build` int(11) NOT NULL DEFAULT '0',
  PRIMARY KEY (`entry`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

-- Data exporting was unselected.


-- Dumping structure for table sniffs_new_test.creature_text
DROP TABLE IF EXISTS `creature_text`;
CREATE TABLE IF NOT EXISTS `creature_text` (
  `unixtimems` bigint(20) unsigned NOT NULL COMMENT 'when the packet was received',
  `guid` int(10) unsigned NOT NULL COMMENT 'creature spawn guid',
  `entry` int(10) unsigned NOT NULL COMMENT 'creature template id',
  `group_id` int(10) unsigned NOT NULL COMMENT 'counter of unique texts per creature id',
  `health_percent` float DEFAULT NULL COMMENT 'the creature''s current health percent at the time the text was sent',
  PRIMARY KEY (`entry`,`group_id`,`unixtimems`,`guid`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_unicode_ci COMMENT='individual instances of creatures sending a text message';

-- Data exporting was unselected.


-- Dumping structure for table sniffs_new_test.creature_text_template
DROP TABLE IF EXISTS `creature_text_template`;
CREATE TABLE IF NOT EXISTS `creature_text_template` (
  `entry` mediumint(8) unsigned NOT NULL DEFAULT '0' COMMENT 'creature template id',
  `group_id` tinyint(3) unsigned NOT NULL DEFAULT '0' COMMENT 'counter of unique texts per creature id',
  `text` longtext COMMENT 'the actual text that was sent',
  `chat_type` tinyint(3) unsigned NOT NULL DEFAULT '0' COMMENT 'chat type',
  `language` tinyint(3) NOT NULL DEFAULT '0' COMMENT 'references Languages.dbc',
  `emote` mediumint(8) unsigned NOT NULL DEFAULT '0' COMMENT 'references Emotes.dbc',
  `sound` mediumint(8) unsigned NOT NULL DEFAULT '0' COMMENT 'references SoundEntries.dbc',
  `broadcast_text_id` mediumint(6) NOT NULL DEFAULT '0' COMMENT 'must be manually set',
  `comment` varchar(255) DEFAULT '',
  PRIMARY KEY (`entry`,`group_id`)
) ENGINE=MyISAM DEFAULT CHARSET=utf8 COMMENT='unique texts per creature id';

-- Data exporting was unselected.


-- Dumping structure for table sniffs_new_test.creature_trainer
DROP TABLE IF EXISTS `creature_trainer`;
CREATE TABLE IF NOT EXISTS `creature_trainer` (
  `creature_id` int(11) unsigned NOT NULL,
  `trainer_id` int(11) unsigned NOT NULL DEFAULT '0',
  `menu_id` int(11) unsigned NOT NULL DEFAULT '0',
  `id` int(11) unsigned NOT NULL DEFAULT '0',
  PRIMARY KEY (`creature_id`,`menu_id`)
) ENGINE=MyISAM DEFAULT CHARSET=utf8;

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
  `current_health` int(10) unsigned DEFAULT NULL,
  `max_health` int(10) unsigned DEFAULT NULL,
  `current_mana` int(10) unsigned DEFAULT NULL,
  `max_mana` int(10) unsigned DEFAULT NULL,
  `aura_state` int(10) unsigned DEFAULT NULL,
  `emote_state` int(10) unsigned DEFAULT NULL,
  `stand_state` int(10) unsigned DEFAULT NULL,
  `pet_talent_points` int(10) unsigned DEFAULT NULL,
  `vis_flags` int(10) unsigned DEFAULT NULL,
  `anim_tier` int(10) unsigned DEFAULT NULL,
  `sheath_state` int(10) unsigned DEFAULT NULL,
  `pvp_flags` int(10) unsigned DEFAULT NULL,
  `pet_flags` int(10) unsigned DEFAULT NULL,
  `shapeshift_form` int(10) unsigned DEFAULT NULL,
  `bounding_radius` float DEFAULT NULL,
  `combat_reach` float DEFAULT NULL,
  `mod_melee_haste` float DEFAULT NULL,
  `mod_ranged_haste` float DEFAULT NULL,
  `base_attack_time` int(10) unsigned DEFAULT NULL,
  `ranged_attack_time` int(10) unsigned DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_unicode_ci COMMENT='values updates from SMSG_UPDATE_OBJECT';

-- Data exporting was unselected.


-- Dumping structure for table sniffs_new_test.dynamicobject
DROP TABLE IF EXISTS `dynamicobject`;
CREATE TABLE IF NOT EXISTS `dynamicobject` (
  `guid` int(10) unsigned NOT NULL AUTO_INCREMENT COMMENT 'Global Unique Identifier',
  `map` smallint(5) unsigned NOT NULL DEFAULT '0' COMMENT 'Map Identifier',
  `position_x` float NOT NULL DEFAULT '0',
  `position_y` float NOT NULL DEFAULT '0',
  `position_z` float NOT NULL DEFAULT '0',
  `orientation` float NOT NULL DEFAULT '0',
  `caster_guid` int(11) unsigned NOT NULL DEFAULT '0',
  `caster_id` int(11) unsigned NOT NULL DEFAULT '0',
  `caster_type` varchar(16) NOT NULL DEFAULT '',
  `spell_id` int(10) unsigned NOT NULL DEFAULT '0',
  `visual_id` int(10) unsigned NOT NULL DEFAULT '0',
  `radius` float NOT NULL DEFAULT '0',
  `type` tinyint(3) unsigned NOT NULL DEFAULT '0',
  `cast_time` int(10) unsigned NOT NULL DEFAULT '0',
  PRIMARY KEY (`guid`)
) ENGINE=MyISAM DEFAULT CHARSET=utf8 ROW_FORMAT=FIXED COMMENT='Gameobject System';

-- Data exporting was unselected.


-- Dumping structure for table sniffs_new_test.dynamicobject_create1_time
DROP TABLE IF EXISTS `dynamicobject_create1_time`;
CREATE TABLE IF NOT EXISTS `dynamicobject_create1_time` (
  `unixtimems` bigint(20) unsigned NOT NULL COMMENT 'when the packet was received',
  `guid` int(10) unsigned NOT NULL COMMENT 'dynamicobject spawn guid',
  `position_x` float NOT NULL,
  `position_y` float NOT NULL,
  `position_z` float NOT NULL,
  `orientation` float NOT NULL,
  PRIMARY KEY (`guid`,`unixtimems`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_unicode_ci ROW_FORMAT=COMPACT COMMENT='times when the object became visible to the client';

-- Data exporting was unselected.


-- Dumping structure for table sniffs_new_test.dynamicobject_create2_time
DROP TABLE IF EXISTS `dynamicobject_create2_time`;
CREATE TABLE IF NOT EXISTS `dynamicobject_create2_time` (
  `unixtimems` bigint(20) unsigned NOT NULL COMMENT 'when the packet was received',
  `guid` int(10) unsigned NOT NULL COMMENT 'dynamicobject spawn guid',
  `position_x` float NOT NULL,
  `position_y` float NOT NULL,
  `position_z` float NOT NULL,
  `orientation` float NOT NULL,
  PRIMARY KEY (`guid`,`unixtimems`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_unicode_ci ROW_FORMAT=COMPACT COMMENT='the time at which the object spawned';

-- Data exporting was unselected.


-- Dumping structure for table sniffs_new_test.dynamicobject_destroy_time
DROP TABLE IF EXISTS `dynamicobject_destroy_time`;
CREATE TABLE IF NOT EXISTS `dynamicobject_destroy_time` (
  `unixtimems` bigint(20) unsigned NOT NULL COMMENT 'when the packet was received',
  `guid` int(10) unsigned NOT NULL COMMENT 'dynamicobject spawn guid',
  PRIMARY KEY (`guid`,`unixtimems`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_unicode_ci ROW_FORMAT=COMPACT COMMENT='times when the object was destroyed from the client''s prespective due to despawning, becoming invisible, or going out of range';

-- Data exporting was unselected.


-- Dumping structure for table sniffs_new_test.gameobject
DROP TABLE IF EXISTS `gameobject`;
CREATE TABLE IF NOT EXISTS `gameobject` (
  `guid` int(10) unsigned NOT NULL AUTO_INCREMENT COMMENT 'Global Unique Identifier',
  `id` mediumint(8) unsigned NOT NULL DEFAULT '0' COMMENT 'Gameobject Identifier',
  `map` smallint(5) unsigned NOT NULL DEFAULT '0' COMMENT 'Map Identifier',
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
  `temp` tinyint(1) unsigned NOT NULL DEFAULT '0',
  `creator_guid` int(11) unsigned NOT NULL DEFAULT '0',
  `creator_id` int(11) unsigned NOT NULL DEFAULT '0',
  `creator_type` varchar(16) NOT NULL DEFAULT '',
  `display_id` int(10) unsigned NOT NULL DEFAULT '0',
  `level` int(10) unsigned NOT NULL DEFAULT '0',
  `faction` int(10) unsigned NOT NULL DEFAULT '0',
  `flags` int(10) unsigned NOT NULL DEFAULT '0',
  `state` tinyint(3) unsigned NOT NULL DEFAULT '0',
  `animprogress` tinyint(3) unsigned NOT NULL DEFAULT '0',
  `sniff_id` smallint(5) unsigned NOT NULL DEFAULT '0',
  `sniff_build` smallint(5) unsigned DEFAULT '0',
  PRIMARY KEY (`guid`)
) ENGINE=MyISAM DEFAULT CHARSET=utf8 ROW_FORMAT=FIXED COMMENT='Gameobject System';

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
) ENGINE=MyISAM DEFAULT CHARSET=utf8;

-- Data exporting was unselected.


-- Dumping structure for table sniffs_new_test.gameobject_create1_time
DROP TABLE IF EXISTS `gameobject_create1_time`;
CREATE TABLE IF NOT EXISTS `gameobject_create1_time` (
  `unixtimems` bigint(20) unsigned NOT NULL COMMENT 'when the packet was received',
  `guid` int(10) unsigned NOT NULL COMMENT 'gameobject spawn guid',
  `position_x` float NOT NULL,
  `position_y` float NOT NULL,
  `position_z` float NOT NULL,
  `orientation` float NOT NULL,
  PRIMARY KEY (`guid`,`unixtimems`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_unicode_ci ROW_FORMAT=COMPACT COMMENT='times when the object became visible to the client';

-- Data exporting was unselected.


-- Dumping structure for table sniffs_new_test.gameobject_create2_time
DROP TABLE IF EXISTS `gameobject_create2_time`;
CREATE TABLE IF NOT EXISTS `gameobject_create2_time` (
  `unixtimems` bigint(20) unsigned NOT NULL COMMENT 'when the packet was received',
  `guid` int(10) unsigned NOT NULL COMMENT 'gameobject spawn guid',
  `position_x` float NOT NULL,
  `position_y` float NOT NULL,
  `position_z` float NOT NULL,
  `orientation` float NOT NULL,
  PRIMARY KEY (`guid`,`unixtimems`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_unicode_ci ROW_FORMAT=COMPACT COMMENT='the time at which the object spawned';

-- Data exporting was unselected.


-- Dumping structure for table sniffs_new_test.gameobject_custom_anim
DROP TABLE IF EXISTS `gameobject_custom_anim`;
CREATE TABLE IF NOT EXISTS `gameobject_custom_anim` (
  `unixtimems` bigint(20) unsigned NOT NULL COMMENT 'when the packet was received',
  `guid` int(10) unsigned NOT NULL COMMENT 'gameobject spawn guid',
  `anim_id` int(10) unsigned NOT NULL DEFAULT '0',
  `as_despawn` tinyint(3) unsigned DEFAULT NULL,
  PRIMARY KEY (`guid`,`unixtimems`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_unicode_ci ROW_FORMAT=COMPACT COMMENT='from SMSG_GAME_OBJECT_CUSTOM_ANIM';

-- Data exporting was unselected.


-- Dumping structure for table sniffs_new_test.gameobject_despawn_anim
DROP TABLE IF EXISTS `gameobject_despawn_anim`;
CREATE TABLE IF NOT EXISTS `gameobject_despawn_anim` (
  `unixtimems` bigint(20) unsigned NOT NULL COMMENT 'when the packet was received',
  `guid` int(10) unsigned NOT NULL COMMENT 'gameobject spawn guid',
  PRIMARY KEY (`guid`,`unixtimems`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_unicode_ci ROW_FORMAT=COMPACT COMMENT='from SMSG_GAME_OBJECT_DESPAWN';

-- Data exporting was unselected.


-- Dumping structure for table sniffs_new_test.gameobject_destroy_time
DROP TABLE IF EXISTS `gameobject_destroy_time`;
CREATE TABLE IF NOT EXISTS `gameobject_destroy_time` (
  `unixtimems` bigint(20) unsigned NOT NULL COMMENT 'when the packet was received',
  `guid` int(10) unsigned NOT NULL COMMENT 'gameobject spawn guid',
  PRIMARY KEY (`guid`,`unixtimems`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_unicode_ci COMMENT='times when the object was destroyed from the client''s prespective due to despawning, becoming invisible, or going out of range';

-- Data exporting was unselected.


-- Dumping structure for table sniffs_new_test.gameobject_loot
DROP TABLE IF EXISTS `gameobject_loot`;
CREATE TABLE IF NOT EXISTS `gameobject_loot` (
  `entry` int(10) unsigned NOT NULL COMMENT 'gameobject template id',
  `loot_id` int(10) unsigned NOT NULL COMMENT 'counter',
  `money` int(10) unsigned NOT NULL DEFAULT '0' COMMENT 'copper',
  `items_count` int(10) unsigned NOT NULL DEFAULT '0' COMMENT 'number of items dropped',
  PRIMARY KEY (`entry`,`loot_id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_unicode_ci COMMENT='each row represents a separate loot instance';

-- Data exporting was unselected.


-- Dumping structure for table sniffs_new_test.gameobject_loot_item
DROP TABLE IF EXISTS `gameobject_loot_item`;
CREATE TABLE IF NOT EXISTS `gameobject_loot_item` (
  `loot_id` int(10) unsigned NOT NULL COMMENT 'references creature_loot',
  `item_id` int(10) unsigned NOT NULL COMMENT 'item template id',
  `count` int(10) unsigned NOT NULL DEFAULT '1' COMMENT 'stack size'
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_unicode_ci COMMENT='individual item that is part of a loot instance';

-- Data exporting was unselected.


-- Dumping structure for table sniffs_new_test.gameobject_questitem
DROP TABLE IF EXISTS `gameobject_questitem`;
CREATE TABLE IF NOT EXISTS `gameobject_questitem` (
  `GameObjectEntry` int(10) unsigned NOT NULL DEFAULT '0',
  `Idx` int(10) unsigned NOT NULL DEFAULT '0',
  `ItemId` int(10) unsigned NOT NULL DEFAULT '0',
  `VerifiedBuild` smallint(5) unsigned NOT NULL DEFAULT '0',
  PRIMARY KEY (`GameObjectEntry`,`Idx`)
) ENGINE=MyISAM DEFAULT CHARSET=utf8;

-- Data exporting was unselected.


-- Dumping structure for table sniffs_new_test.gameobject_template
DROP TABLE IF EXISTS `gameobject_template`;
CREATE TABLE IF NOT EXISTS `gameobject_template` (
  `entry` mediumint(8) unsigned NOT NULL DEFAULT '0',
  `type` tinyint(3) unsigned NOT NULL DEFAULT '0',
  `display_id` mediumint(8) unsigned NOT NULL DEFAULT '0',
  `scale` float NOT NULL DEFAULT '1',
  `name` varchar(100) NOT NULL DEFAULT '',
  `icon_name` varchar(100) NOT NULL DEFAULT '',
  `cast_bar_caption` varchar(100) NOT NULL DEFAULT '',
  `unk_string` varchar(100) NOT NULL DEFAULT '',
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
  `data16` int(10) unsigned NOT NULL DEFAULT '0',
  `data17` int(10) unsigned NOT NULL DEFAULT '0',
  `data18` int(10) unsigned NOT NULL DEFAULT '0',
  `data19` int(10) unsigned NOT NULL DEFAULT '0',
  `data20` int(10) unsigned NOT NULL DEFAULT '0',
  `data21` int(10) unsigned NOT NULL DEFAULT '0',
  `data22` int(10) unsigned NOT NULL DEFAULT '0',
  `data23` int(10) unsigned NOT NULL DEFAULT '0',
  `data24` int(10) unsigned NOT NULL DEFAULT '0',
  `data25` int(10) unsigned NOT NULL DEFAULT '0',
  `data26` int(10) unsigned NOT NULL DEFAULT '0',
  `data27` int(10) unsigned NOT NULL DEFAULT '0',
  `data28` int(10) unsigned NOT NULL DEFAULT '0',
  `data29` int(10) unsigned NOT NULL DEFAULT '0',
  `data30` int(10) unsigned NOT NULL DEFAULT '0',
  `data31` int(10) unsigned NOT NULL DEFAULT '0',
  `data32` int(10) unsigned NOT NULL DEFAULT '0',
  `data33` int(10) unsigned NOT NULL DEFAULT '0',
  `quest_items_count` tinyint(3) unsigned NOT NULL DEFAULT '0',
  `sniff_build` smallint(5) unsigned DEFAULT '0',
  PRIMARY KEY (`entry`),
  KEY `idx_name` (`name`)
) ENGINE=MyISAM DEFAULT CHARSET=utf8 ROW_FORMAT=FIXED COMMENT='Gameobject System';

-- Data exporting was unselected.


-- Dumping structure for table sniffs_new_test.gameobject_template_addon
DROP TABLE IF EXISTS `gameobject_template_addon`;
CREATE TABLE IF NOT EXISTS `gameobject_template_addon` (
  `entry` int(10) unsigned NOT NULL,
  `faction` int(10) unsigned NOT NULL DEFAULT '0',
  `flags` int(10) unsigned NOT NULL DEFAULT '0',
  PRIMARY KEY (`entry`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_unicode_ci;

-- Data exporting was unselected.


-- Dumping structure for table sniffs_new_test.gameobject_text
DROP TABLE IF EXISTS `gameobject_text`;
CREATE TABLE IF NOT EXISTS `gameobject_text` (
  `unixtimems` bigint(20) unsigned NOT NULL COMMENT 'when the packet was received',
  `guid` int(10) unsigned NOT NULL COMMENT 'gameobject spawn guid',
  `entry` int(10) unsigned NOT NULL COMMENT 'gameobject template id',
  `group_id` int(10) unsigned NOT NULL COMMENT 'counter of unique texts per gameobject id',
  PRIMARY KEY (`entry`,`group_id`,`unixtimems`,`guid`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_unicode_ci ROW_FORMAT=COMPACT COMMENT='individual instances of gameobjects sending a text message';

-- Data exporting was unselected.


-- Dumping structure for table sniffs_new_test.gameobject_text_template
DROP TABLE IF EXISTS `gameobject_text_template`;
CREATE TABLE IF NOT EXISTS `gameobject_text_template` (
  `entry` mediumint(8) unsigned NOT NULL DEFAULT '0' COMMENT 'gameobject template id',
  `group_id` tinyint(3) unsigned NOT NULL DEFAULT '0' COMMENT 'counter of unique texts per gameobject id',
  `text` longtext COMMENT 'the actual text that was sent',
  `chat_type` tinyint(3) unsigned NOT NULL DEFAULT '0' COMMENT 'chat type',
  `language` tinyint(3) NOT NULL DEFAULT '0' COMMENT 'references Languages.dbc',
  `sound` mediumint(8) unsigned NOT NULL DEFAULT '0' COMMENT 'references SoundEntries.dbc',
  `broadcast_text_id` mediumint(6) NOT NULL DEFAULT '0' COMMENT 'must be manually set',
  `comment` varchar(255) DEFAULT '',
  PRIMARY KEY (`entry`,`group_id`)
) ENGINE=MyISAM DEFAULT CHARSET=utf8 ROW_FORMAT=DYNAMIC COMMENT='unique texts per gameobject id';

-- Data exporting was unselected.


-- Dumping structure for table sniffs_new_test.gameobject_values_update
DROP TABLE IF EXISTS `gameobject_values_update`;
CREATE TABLE IF NOT EXISTS `gameobject_values_update` (
  `unixtimems` bigint(20) unsigned NOT NULL COMMENT 'when the packet was received',
  `guid` int(10) unsigned NOT NULL COMMENT 'gameobject spawn guid',
  `display_id` int(10) unsigned DEFAULT NULL,
  `level` int(10) unsigned DEFAULT NULL,
  `faction` int(10) unsigned DEFAULT NULL,
  `flags` int(10) unsigned DEFAULT NULL,
  `state` int(10) unsigned DEFAULT NULL,
  `animprogress` int(10) unsigned DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_unicode_ci COMMENT='values updates from SMSG_UPDATE_OBJECT';

-- Data exporting was unselected.


-- Dumping structure for table sniffs_new_test.gossip_menu
DROP TABLE IF EXISTS `gossip_menu`;
CREATE TABLE IF NOT EXISTS `gossip_menu` (
  `entry` smallint(5) unsigned NOT NULL DEFAULT '0',
  `text_id` mediumint(8) unsigned NOT NULL DEFAULT '0',
  `sniff_build` smallint(5) unsigned NOT NULL DEFAULT '0',
  PRIMARY KEY (`entry`,`text_id`)
) ENGINE=MyISAM DEFAULT CHARSET=utf8;

-- Data exporting was unselected.


-- Dumping structure for table sniffs_new_test.gossip_menu_option
DROP TABLE IF EXISTS `gossip_menu_option`;
CREATE TABLE IF NOT EXISTS `gossip_menu_option` (
  `menu_id` smallint(5) unsigned NOT NULL DEFAULT '0',
  `id` smallint(5) unsigned NOT NULL DEFAULT '0',
  `option_icon` mediumint(8) unsigned NOT NULL DEFAULT '0',
  `option_text` text,
  `option_broadcast_text` mediumint(6) NOT NULL DEFAULT '0',
  `option_id` tinyint(3) unsigned NOT NULL DEFAULT '0',
  `npc_option_npcflag` int(10) unsigned NOT NULL DEFAULT '0',
  `action_menu_id` int(10) unsigned NOT NULL DEFAULT '0',
  `action_poi_id` mediumint(8) unsigned NOT NULL DEFAULT '0',
  `box_coded` tinyint(3) unsigned NOT NULL DEFAULT '0',
  `box_money` int(10) unsigned NOT NULL DEFAULT '0',
  `box_text` text,
  `box_broadcast_text` mediumint(6) NOT NULL DEFAULT '0',
  `sniff_build` smallint(5) unsigned NOT NULL DEFAULT '0',
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
) ENGINE=MyISAM DEFAULT CHARSET=utf8;

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
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_unicode_ci;

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
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_unicode_ci;

-- Data exporting was unselected.


-- Dumping structure for table sniffs_new_test.item_template
DROP TABLE IF EXISTS `item_template`;
CREATE TABLE IF NOT EXISTS `item_template` (
  `entry` mediumint(8) unsigned NOT NULL DEFAULT '0',
  `class` tinyint(3) unsigned NOT NULL DEFAULT '0',
  `subclass` tinyint(3) unsigned NOT NULL DEFAULT '0',
  `SoundOverrideSubclass` tinyint(3) NOT NULL DEFAULT '-1',
  `name` varchar(255) NOT NULL DEFAULT '',
  `displayid` mediumint(8) unsigned NOT NULL DEFAULT '0',
  `Quality` tinyint(3) unsigned NOT NULL DEFAULT '0',
  `Flags` int(10) unsigned NOT NULL DEFAULT '0',
  `FlagsExtra` int(10) unsigned NOT NULL DEFAULT '0',
  `BuyCount` tinyint(3) unsigned NOT NULL DEFAULT '1',
  `BuyPrice` bigint(20) NOT NULL DEFAULT '0',
  `SellPrice` int(10) unsigned NOT NULL DEFAULT '0',
  `InventoryType` tinyint(3) unsigned NOT NULL DEFAULT '0',
  `AllowableClass` int(11) NOT NULL DEFAULT '-1',
  `AllowableRace` int(11) NOT NULL DEFAULT '-1',
  `ItemLevel` smallint(5) unsigned NOT NULL DEFAULT '0',
  `RequiredLevel` tinyint(3) unsigned NOT NULL DEFAULT '0',
  `RequiredSkill` smallint(5) unsigned NOT NULL DEFAULT '0',
  `RequiredSkillRank` smallint(5) unsigned NOT NULL DEFAULT '0',
  `requiredspell` mediumint(8) unsigned NOT NULL DEFAULT '0',
  `requiredhonorrank` mediumint(8) unsigned NOT NULL DEFAULT '0',
  `RequiredCityRank` mediumint(8) unsigned NOT NULL DEFAULT '0',
  `RequiredReputationFaction` smallint(5) unsigned NOT NULL DEFAULT '0',
  `RequiredReputationRank` smallint(5) unsigned NOT NULL DEFAULT '0',
  `maxcount` int(11) NOT NULL DEFAULT '0',
  `stackable` int(11) DEFAULT '1',
  `ContainerSlots` tinyint(3) unsigned NOT NULL DEFAULT '0',
  `StatsCount` tinyint(3) unsigned NOT NULL DEFAULT '0',
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
  `ScalingStatDistribution` smallint(6) NOT NULL DEFAULT '0',
  `ScalingStatValue` int(10) unsigned NOT NULL DEFAULT '0',
  `dmg_min1` float NOT NULL DEFAULT '0',
  `dmg_max1` float NOT NULL DEFAULT '0',
  `dmg_type1` tinyint(3) unsigned NOT NULL DEFAULT '0',
  `dmg_min2` float NOT NULL DEFAULT '0',
  `dmg_max2` float NOT NULL DEFAULT '0',
  `dmg_type2` tinyint(3) unsigned NOT NULL DEFAULT '0',
  `armor` smallint(5) unsigned NOT NULL DEFAULT '0',
  `holy_res` tinyint(3) unsigned NOT NULL DEFAULT '0',
  `fire_res` tinyint(3) unsigned NOT NULL DEFAULT '0',
  `nature_res` tinyint(3) unsigned NOT NULL DEFAULT '0',
  `frost_res` tinyint(3) unsigned NOT NULL DEFAULT '0',
  `shadow_res` tinyint(3) unsigned NOT NULL DEFAULT '0',
  `arcane_res` tinyint(3) unsigned NOT NULL DEFAULT '0',
  `delay` smallint(5) unsigned NOT NULL DEFAULT '1000',
  `ammo_type` tinyint(3) unsigned NOT NULL DEFAULT '0',
  `RangedModRange` float NOT NULL DEFAULT '0',
  `spellid_1` mediumint(8) NOT NULL DEFAULT '0',
  `spelltrigger_1` tinyint(3) unsigned NOT NULL DEFAULT '0',
  `spellcharges_1` smallint(6) NOT NULL DEFAULT '0',
  `spellppmRate_1` float NOT NULL DEFAULT '0',
  `spellcooldown_1` int(11) NOT NULL DEFAULT '-1',
  `spellcategory_1` smallint(5) unsigned NOT NULL DEFAULT '0',
  `spellcategorycooldown_1` int(11) NOT NULL DEFAULT '-1',
  `spellid_2` mediumint(8) NOT NULL DEFAULT '0',
  `spelltrigger_2` tinyint(3) unsigned NOT NULL DEFAULT '0',
  `spellcharges_2` smallint(6) NOT NULL DEFAULT '0',
  `spellppmRate_2` float NOT NULL DEFAULT '0',
  `spellcooldown_2` int(11) NOT NULL DEFAULT '-1',
  `spellcategory_2` smallint(5) unsigned NOT NULL DEFAULT '0',
  `spellcategorycooldown_2` int(11) NOT NULL DEFAULT '-1',
  `spellid_3` mediumint(8) NOT NULL DEFAULT '0',
  `spelltrigger_3` tinyint(3) unsigned NOT NULL DEFAULT '0',
  `spellcharges_3` smallint(6) NOT NULL DEFAULT '0',
  `spellppmRate_3` float NOT NULL DEFAULT '0',
  `spellcooldown_3` int(11) NOT NULL DEFAULT '-1',
  `spellcategory_3` smallint(5) unsigned NOT NULL DEFAULT '0',
  `spellcategorycooldown_3` int(11) NOT NULL DEFAULT '-1',
  `spellid_4` mediumint(8) NOT NULL DEFAULT '0',
  `spelltrigger_4` tinyint(3) unsigned NOT NULL DEFAULT '0',
  `spellcharges_4` smallint(6) NOT NULL DEFAULT '0',
  `spellppmRate_4` float NOT NULL DEFAULT '0',
  `spellcooldown_4` int(11) NOT NULL DEFAULT '-1',
  `spellcategory_4` smallint(5) unsigned NOT NULL DEFAULT '0',
  `spellcategorycooldown_4` int(11) NOT NULL DEFAULT '-1',
  `spellid_5` mediumint(8) NOT NULL DEFAULT '0',
  `spelltrigger_5` tinyint(3) unsigned NOT NULL DEFAULT '0',
  `spellcharges_5` smallint(6) NOT NULL DEFAULT '0',
  `spellppmRate_5` float NOT NULL DEFAULT '0',
  `spellcooldown_5` int(11) NOT NULL DEFAULT '-1',
  `spellcategory_5` smallint(5) unsigned NOT NULL DEFAULT '0',
  `spellcategorycooldown_5` int(11) NOT NULL DEFAULT '-1',
  `bonding` tinyint(3) unsigned NOT NULL DEFAULT '0',
  `description` varchar(255) NOT NULL DEFAULT '',
  `PageText` mediumint(8) unsigned NOT NULL DEFAULT '0',
  `LanguageID` tinyint(3) unsigned NOT NULL DEFAULT '0',
  `PageMaterial` tinyint(3) unsigned NOT NULL DEFAULT '0',
  `startquest` mediumint(8) unsigned NOT NULL DEFAULT '0',
  `lockid` mediumint(8) unsigned NOT NULL DEFAULT '0',
  `Material` tinyint(4) NOT NULL DEFAULT '0',
  `sheath` tinyint(3) unsigned NOT NULL DEFAULT '0',
  `RandomProperty` mediumint(8) NOT NULL DEFAULT '0',
  `RandomSuffix` mediumint(8) unsigned NOT NULL DEFAULT '0',
  `block` mediumint(8) unsigned NOT NULL DEFAULT '0',
  `itemset` mediumint(8) unsigned NOT NULL DEFAULT '0',
  `MaxDurability` smallint(5) unsigned NOT NULL DEFAULT '0',
  `area` mediumint(8) unsigned NOT NULL DEFAULT '0',
  `Map` smallint(6) NOT NULL DEFAULT '0',
  `BagFamily` mediumint(8) NOT NULL DEFAULT '0',
  `TotemCategory` mediumint(8) NOT NULL DEFAULT '0',
  `socketColor_1` tinyint(4) NOT NULL DEFAULT '0',
  `socketContent_1` mediumint(8) NOT NULL DEFAULT '0',
  `socketColor_2` tinyint(4) NOT NULL DEFAULT '0',
  `socketContent_2` mediumint(8) NOT NULL DEFAULT '0',
  `socketColor_3` tinyint(4) NOT NULL DEFAULT '0',
  `socketContent_3` mediumint(8) NOT NULL DEFAULT '0',
  `socketBonus` mediumint(8) NOT NULL DEFAULT '0',
  `GemProperties` mediumint(8) NOT NULL DEFAULT '0',
  `RequiredDisenchantSkill` smallint(6) NOT NULL DEFAULT '-1',
  `ArmorDamageModifier` float NOT NULL DEFAULT '0',
  `duration` int(10) unsigned NOT NULL DEFAULT '0',
  `ItemLimitCategory` smallint(6) NOT NULL DEFAULT '0',
  `HolidayId` int(11) unsigned NOT NULL DEFAULT '0',
  `ScriptName` varchar(64) NOT NULL DEFAULT '',
  `DisenchantID` mediumint(8) unsigned NOT NULL DEFAULT '0',
  `FoodType` tinyint(3) unsigned NOT NULL DEFAULT '0',
  `minMoneyLoot` int(10) unsigned NOT NULL DEFAULT '0',
  `maxMoneyLoot` int(10) unsigned NOT NULL DEFAULT '0',
  `VerifiedBuild` smallint(5) DEFAULT '0',
  PRIMARY KEY (`entry`),
  KEY `idx_name` (`name`),
  KEY `items_index` (`class`)
) ENGINE=MyISAM DEFAULT CHARSET=utf8 ROW_FORMAT=FIXED COMMENT='Item System';

-- Data exporting was unselected.


-- Dumping structure for table sniffs_new_test.npc_text
DROP TABLE IF EXISTS `npc_text`;
CREATE TABLE IF NOT EXISTS `npc_text` (
  `ID` mediumint(8) unsigned NOT NULL DEFAULT '0',
  `Probability0` float NOT NULL DEFAULT '0',
  `Probability1` float NOT NULL DEFAULT '0',
  `Probability2` float NOT NULL DEFAULT '0',
  `Probability3` float NOT NULL DEFAULT '0',
  `Probability4` float NOT NULL DEFAULT '0',
  `Probability5` float NOT NULL DEFAULT '0',
  `Probability6` float NOT NULL DEFAULT '0',
  `Probability7` float NOT NULL DEFAULT '0',
  `BroadcastTextID0` mediumint(6) unsigned NOT NULL DEFAULT '0',
  `BroadcastTextID1` mediumint(6) unsigned NOT NULL DEFAULT '0',
  `BroadcastTextID2` mediumint(6) unsigned NOT NULL DEFAULT '0',
  `BroadcastTextID3` mediumint(6) unsigned NOT NULL DEFAULT '0',
  `BroadcastTextID4` mediumint(6) unsigned NOT NULL DEFAULT '0',
  `BroadcastTextID5` mediumint(6) unsigned NOT NULL DEFAULT '0',
  `BroadcastTextID6` mediumint(6) unsigned NOT NULL DEFAULT '0',
  `BroadcastTextID7` mediumint(6) unsigned NOT NULL DEFAULT '0',
  `VerifiedBuild` smallint(5) unsigned DEFAULT '0',
  PRIMARY KEY (`ID`)
) ENGINE=MyISAM DEFAULT CHARSET=utf8;

-- Data exporting was unselected.


-- Dumping structure for table sniffs_new_test.npc_trainer
DROP TABLE IF EXISTS `npc_trainer`;
CREATE TABLE IF NOT EXISTS `npc_trainer` (
  `entry` mediumint(8) unsigned NOT NULL DEFAULT '0',
  `spell` mediumint(8) unsigned NOT NULL DEFAULT '0',
  `spellcost` int(10) unsigned NOT NULL DEFAULT '0',
  `reqskill` smallint(5) unsigned NOT NULL DEFAULT '0',
  `reqskillvalue` smallint(5) unsigned NOT NULL DEFAULT '0',
  `reqlevel` tinyint(3) unsigned NOT NULL DEFAULT '0',
  UNIQUE KEY `entry_spell` (`entry`,`spell`)
) ENGINE=MyISAM DEFAULT CHARSET=utf8;

-- Data exporting was unselected.


-- Dumping structure for table sniffs_new_test.npc_vendor
DROP TABLE IF EXISTS `npc_vendor`;
CREATE TABLE IF NOT EXISTS `npc_vendor` (
  `entry` mediumint(8) unsigned NOT NULL DEFAULT '0',
  `slot` smallint(6) NOT NULL DEFAULT '0',
  `item` mediumint(8) NOT NULL DEFAULT '0',
  `maxcount` mediumint(8) unsigned NOT NULL DEFAULT '0',
  `incrtime` int(10) unsigned NOT NULL DEFAULT '0',
  `ExtendedCost` mediumint(8) unsigned NOT NULL DEFAULT '0',
  `type` tinyint(3) unsigned NOT NULL DEFAULT '1',
  `PlayerConditionID` int(10) unsigned NOT NULL DEFAULT '0',
  `IgnoreFiltering` tinyint(3) unsigned NOT NULL DEFAULT '0',
  `VerifiedBuild` smallint(5) unsigned DEFAULT '0',
  PRIMARY KEY (`entry`,`item`,`ExtendedCost`,`type`),
  KEY `slot` (`slot`)
) ENGINE=MyISAM DEFAULT CHARSET=utf8 ROW_FORMAT=FIXED COMMENT='Npc System';

-- Data exporting was unselected.


-- Dumping structure for table sniffs_new_test.object_names
DROP TABLE IF EXISTS `object_names`;
CREATE TABLE IF NOT EXISTS `object_names` (
  `ObjectType` enum('None','Spell','Map','LFGDungeon','Battleground','Unit','GameObject','CreatureDifficulty','Item','Quest','Opcode','PageText','NpcText','BroadcastText','Gossip','Zone','Area','AreaTrigger','Phase','Player','Achievement') NOT NULL DEFAULT 'None',
  `Id` int(10) NOT NULL,
  `Name` text NOT NULL,
  PRIMARY KEY (`ObjectType`,`Id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COMMENT='WPP''s ObjectTypes Names DataBase';

-- Data exporting was unselected.


-- Dumping structure for table sniffs_new_test.page_text
DROP TABLE IF EXISTS `page_text`;
CREATE TABLE IF NOT EXISTS `page_text` (
  `ID` mediumint(8) unsigned NOT NULL DEFAULT '0',
  `Text` longtext,
  `NextPageID` mediumint(8) unsigned NOT NULL DEFAULT '0',
  `PlayerConditionID` int(11) NOT NULL DEFAULT '0',
  `Flags` tinyint(3) unsigned NOT NULL DEFAULT '0',
  `VerifiedBuild` int(11) NOT NULL DEFAULT '0',
  PRIMARY KEY (`ID`)
) ENGINE=MyISAM DEFAULT CHARSET=utf8 COMMENT='Item System';

-- Data exporting was unselected.


-- Dumping structure for table sniffs_new_test.page_text_locale
DROP TABLE IF EXISTS `page_text_locale`;
CREATE TABLE IF NOT EXISTS `page_text_locale` (
  `ID` mediumint(8) unsigned NOT NULL DEFAULT '0',
  `locale` varchar(4) NOT NULL,
  `Text` text,
  `VerifiedBuild` int(11) NOT NULL DEFAULT '0',
  PRIMARY KEY (`ID`,`locale`)
) ENGINE=MyISAM DEFAULT CHARSET=utf8;

-- Data exporting was unselected.


-- Dumping structure for table sniffs_new_test.player
DROP TABLE IF EXISTS `player`;
CREATE TABLE IF NOT EXISTS `player` (
  `guid` int(11) unsigned NOT NULL DEFAULT '0' COMMENT 'Global Unique Identifier',
  `map` int(11) unsigned NOT NULL DEFAULT '0' COMMENT 'Map Identifier',
  `position_x` float NOT NULL DEFAULT '0',
  `position_y` float NOT NULL DEFAULT '0',
  `position_z` float NOT NULL DEFAULT '0',
  `orientation` float NOT NULL DEFAULT '0',
  `name` varchar(12) NOT NULL DEFAULT '',
  `race` tinyint(3) unsigned NOT NULL DEFAULT '0',
  `class` tinyint(3) unsigned NOT NULL DEFAULT '0',
  `gender` tinyint(3) unsigned NOT NULL DEFAULT '0',
  `level` tinyint(3) unsigned NOT NULL DEFAULT '0',
  `xp` int(10) unsigned NOT NULL DEFAULT '0',
  `money` int(10) unsigned NOT NULL DEFAULT '0',
  `player_bytes1` int(10) unsigned NOT NULL DEFAULT '0',
  `player_bytes2` int(10) unsigned NOT NULL DEFAULT '0',
  `player_flags` int(10) unsigned NOT NULL DEFAULT '0',
  `scale` float NOT NULL DEFAULT '0',
  `display_id` int(10) unsigned NOT NULL DEFAULT '0',
  `native_display_id` int(10) unsigned NOT NULL DEFAULT '0',
  `mount_display_id` int(10) unsigned NOT NULL DEFAULT '0',
  `faction` int(10) unsigned NOT NULL DEFAULT '0',
  `unit_flags` int(10) unsigned NOT NULL DEFAULT '0',
  `unit_flags2` int(10) unsigned NOT NULL DEFAULT '0',
  `current_health` int(10) unsigned NOT NULL DEFAULT '0',
  `max_health` int(10) unsigned NOT NULL DEFAULT '0',
  `current_mana` int(10) unsigned NOT NULL DEFAULT '0',
  `max_mana` int(10) unsigned NOT NULL DEFAULT '0',
  `aura_state` int(10) unsigned NOT NULL DEFAULT '0',
  `emote_state` int(10) unsigned NOT NULL DEFAULT '0',
  `stand_state` tinyint(3) unsigned NOT NULL DEFAULT '0' COMMENT 'from UNIT_FIELD_BYTES_1',
  `pet_talent_points` tinyint(3) unsigned NOT NULL DEFAULT '0' COMMENT 'from UNIT_FIELD_BYTES_1',
  `vis_flags` tinyint(3) unsigned NOT NULL DEFAULT '0' COMMENT 'from UNIT_FIELD_BYTES_1',
  `anim_tier` tinyint(3) unsigned NOT NULL DEFAULT '0' COMMENT 'from UNIT_FIELD_BYTES_1',
  `sheath_state` tinyint(3) unsigned NOT NULL DEFAULT '0' COMMENT 'from UNIT_FIELD_BYTES_2',
  `pvp_flags` tinyint(3) unsigned NOT NULL DEFAULT '0' COMMENT 'from UNIT_FIELD_BYTES_2',
  `pet_flags` tinyint(3) unsigned NOT NULL DEFAULT '0' COMMENT 'from UNIT_FIELD_BYTES_2',
  `shapeshift_form` tinyint(3) unsigned NOT NULL DEFAULT '0' COMMENT 'from UNIT_FIELD_BYTES_2',
  `speed_walk` float NOT NULL DEFAULT '0',
  `speed_run` float NOT NULL DEFAULT '0',
  `bounding_radius` float NOT NULL DEFAULT '0',
  `combat_reach` float NOT NULL DEFAULT '0',
  `mod_melee_haste` float NOT NULL DEFAULT '0',
  `mod_ranged_haste` float NOT NULL DEFAULT '0',
  `base_attack_time` int(10) unsigned NOT NULL DEFAULT '0',
  `ranged_attack_time` int(10) unsigned NOT NULL DEFAULT '0',
  `equipment_cache` text,
  `auras` text,
  PRIMARY KEY (`guid`)
) ENGINE=MyISAM DEFAULT CHARSET=utf8 ROW_FORMAT=DYNAMIC COMMENT='player data in format used by vmangos db';

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
) ENGINE=MyISAM DEFAULT CHARSET=utf8;

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
) ENGINE=MyISAM DEFAULT CHARSET=utf8;

-- Data exporting was unselected.


-- Dumping structure for table sniffs_new_test.player_active_player
DROP TABLE IF EXISTS `player_active_player`;
CREATE TABLE IF NOT EXISTS `player_active_player` (
  `unixtime` int(10) unsigned NOT NULL,
  `guid` int(10) unsigned NOT NULL,
  PRIMARY KEY (`guid`,`unixtime`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_unicode_ci COMMENT='shows which character was controlled by the client at a given time';

-- Data exporting was unselected.


-- Dumping structure for table sniffs_new_test.player_attack_log
DROP TABLE IF EXISTS `player_attack_log`;
CREATE TABLE IF NOT EXISTS `player_attack_log` (
  `unixtimems` bigint(20) unsigned NOT NULL DEFAULT '0',
  `guid` int(10) unsigned NOT NULL,
  `victim_guid` int(10) unsigned NOT NULL DEFAULT '0',
  `victim_id` int(10) unsigned NOT NULL DEFAULT '0',
  `victim_type` varchar(16) COLLATE utf8_unicode_ci NOT NULL DEFAULT '',
  `hit_info` int(10) unsigned NOT NULL DEFAULT '0',
  `damage` int(10) unsigned NOT NULL DEFAULT '0',
  `original_damage` int(10) unsigned NOT NULL DEFAULT '0',
  `overkill_damage` int(10) NOT NULL DEFAULT '0',
  `blocked_damage` int(10) NOT NULL DEFAULT '0',
  `victim_state` int(10) unsigned NOT NULL DEFAULT '0',
  `attacker_state` int(10) NOT NULL DEFAULT '0',
  `spell_id` int(10) unsigned NOT NULL DEFAULT '0',
  PRIMARY KEY (`guid`,`unixtimems`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_unicode_ci ROW_FORMAT=COMPACT COMMENT='from SMSG_ATTACKER_STATE_UPDATE';

-- Data exporting was unselected.


-- Dumping structure for table sniffs_new_test.player_attack_start
DROP TABLE IF EXISTS `player_attack_start`;
CREATE TABLE IF NOT EXISTS `player_attack_start` (
  `unixtimems` bigint(20) unsigned NOT NULL DEFAULT '0',
  `guid` int(10) unsigned NOT NULL,
  `victim_guid` int(10) unsigned NOT NULL DEFAULT '0',
  `victim_id` int(10) unsigned NOT NULL DEFAULT '0',
  `victim_type` varchar(16) COLLATE utf8_unicode_ci NOT NULL DEFAULT '',
  PRIMARY KEY (`guid`,`unixtimems`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_unicode_ci ROW_FORMAT=COMPACT COMMENT='from SMSG_ATTACK_START';

-- Data exporting was unselected.


-- Dumping structure for table sniffs_new_test.player_attack_stop
DROP TABLE IF EXISTS `player_attack_stop`;
CREATE TABLE IF NOT EXISTS `player_attack_stop` (
  `unixtimems` bigint(20) unsigned NOT NULL DEFAULT '0',
  `guid` int(10) unsigned NOT NULL,
  `victim_guid` int(10) unsigned NOT NULL DEFAULT '0',
  `victim_id` int(10) unsigned NOT NULL DEFAULT '0',
  `victim_type` varchar(16) COLLATE utf8_unicode_ci NOT NULL DEFAULT '',
  PRIMARY KEY (`guid`,`unixtimems`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_unicode_ci ROW_FORMAT=COMPACT COMMENT='from SMSG_ATTACK_STOP';

-- Data exporting was unselected.


-- Dumping structure for table sniffs_new_test.player_auras_update
DROP TABLE IF EXISTS `player_auras_update`;
CREATE TABLE IF NOT EXISTS `player_auras_update` (
  `unixtimems` bigint(20) unsigned NOT NULL COMMENT 'when the packet was received',
  `guid` int(10) unsigned NOT NULL,
  `update_id` int(10) unsigned NOT NULL COMMENT 'counting aura update packets for this guid',
  `slot` int(10) unsigned NOT NULL,
  `spell_id` int(10) unsigned NOT NULL,
  `visual_id` int(10) unsigned NOT NULL,
  `aura_flags` int(10) unsigned NOT NULL,
  `active_flags` int(10) unsigned NOT NULL,
  `level` int(10) unsigned NOT NULL,
  `charges` int(10) unsigned NOT NULL,
  `content_tuning_id` int(10) NOT NULL,
  `duration` int(10) NOT NULL,
  `max_duration` int(10) NOT NULL,
  `caster_guid` int(10) unsigned NOT NULL,
  `caster_id` int(10) unsigned NOT NULL,
  `caster_type` varchar(16) COLLATE utf8_unicode_ci NOT NULL,
  PRIMARY KEY (`guid`,`update_id`,`slot`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_unicode_ci ROW_FORMAT=COMPACT COMMENT='aura updates from SMSG_AURA_UPDATE';

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


-- Dumping structure for table sniffs_new_test.player_create1_time
DROP TABLE IF EXISTS `player_create1_time`;
CREATE TABLE IF NOT EXISTS `player_create1_time` (
  `unixtimems` bigint(20) unsigned NOT NULL COMMENT 'when the packet was received',
  `guid` int(10) unsigned NOT NULL,
  `position_x` float NOT NULL,
  `position_y` float NOT NULL,
  `position_z` float NOT NULL,
  `orientation` float NOT NULL,
  PRIMARY KEY (`guid`,`unixtimems`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_unicode_ci ROW_FORMAT=COMPACT COMMENT='times when the object became visible to the client';

-- Data exporting was unselected.


-- Dumping structure for table sniffs_new_test.player_create2_time
DROP TABLE IF EXISTS `player_create2_time`;
CREATE TABLE IF NOT EXISTS `player_create2_time` (
  `unixtimems` bigint(20) unsigned NOT NULL COMMENT 'when the packet was received',
  `guid` int(10) unsigned NOT NULL,
  `position_x` float NOT NULL,
  `position_y` float NOT NULL,
  `position_z` float NOT NULL,
  `orientation` float NOT NULL,
  PRIMARY KEY (`guid`,`unixtimems`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_unicode_ci ROW_FORMAT=COMPACT COMMENT='the time at which the object spawned';

-- Data exporting was unselected.


-- Dumping structure for table sniffs_new_test.player_destroy_time
DROP TABLE IF EXISTS `player_destroy_time`;
CREATE TABLE IF NOT EXISTS `player_destroy_time` (
  `unixtimems` bigint(20) unsigned NOT NULL COMMENT 'when the packet was received',
  `guid` int(10) unsigned NOT NULL,
  PRIMARY KEY (`guid`,`unixtimems`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_unicode_ci ROW_FORMAT=COMPACT COMMENT='times when the object was destroyed from the client''s prespective due to despawning, becoming invisible, or going out of range';

-- Data exporting was unselected.


-- Dumping structure for table sniffs_new_test.player_emote
DROP TABLE IF EXISTS `player_emote`;
CREATE TABLE IF NOT EXISTS `player_emote` (
  `unixtimems` bigint(20) unsigned NOT NULL COMMENT 'when the packet was received',
  `guid` int(10) unsigned NOT NULL,
  `emote_id` int(10) unsigned NOT NULL COMMENT 'references Emotes.dbc',
  `emote_name` varchar(50) COLLATE utf8_unicode_ci DEFAULT NULL,
  PRIMARY KEY (`guid`,`unixtimems`,`emote_id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_unicode_ci ROW_FORMAT=COMPACT COMMENT='from SMSG_EMOTE';

-- Data exporting was unselected.


-- Dumping structure for table sniffs_new_test.player_equipment_values_update
DROP TABLE IF EXISTS `player_equipment_values_update`;
CREATE TABLE IF NOT EXISTS `player_equipment_values_update` (
  `unixtimems` bigint(20) unsigned NOT NULL COMMENT 'when the packet was received',
  `guid` int(10) unsigned NOT NULL COMMENT 'creature spawn guid',
  `slot` tinyint(3) unsigned NOT NULL,
  `item_id` int(10) unsigned NOT NULL,
  PRIMARY KEY (`guid`,`unixtimems`,`slot`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_unicode_ci ROW_FORMAT=COMPACT COMMENT='changes to visual item slots';

-- Data exporting was unselected.


-- Dumping structure for table sniffs_new_test.player_guid_values
DROP TABLE IF EXISTS `player_guid_values`;
CREATE TABLE IF NOT EXISTS `player_guid_values` (
  `guid` int(10) unsigned NOT NULL,
  `charm_guid` int(10) unsigned NOT NULL DEFAULT '0',
  `charm_id` int(10) unsigned NOT NULL DEFAULT '0',
  `charm_type` varchar(16) COLLATE utf8_unicode_ci NOT NULL DEFAULT '',
  `summon_guid` int(10) unsigned NOT NULL DEFAULT '0',
  `summon_id` int(10) unsigned NOT NULL DEFAULT '0',
  `summon_type` varchar(16) COLLATE utf8_unicode_ci NOT NULL DEFAULT '',
  `charmer_guid` int(10) unsigned NOT NULL DEFAULT '0',
  `charmer_id` int(10) unsigned NOT NULL DEFAULT '0',
  `charmer_type` varchar(16) COLLATE utf8_unicode_ci NOT NULL DEFAULT '',
  `creator_guid` int(10) unsigned NOT NULL DEFAULT '0',
  `creator_id` int(10) unsigned NOT NULL DEFAULT '0',
  `creator_type` varchar(16) COLLATE utf8_unicode_ci NOT NULL DEFAULT '',
  `summoner_guid` int(10) unsigned NOT NULL DEFAULT '0',
  `summoner_id` int(10) unsigned NOT NULL DEFAULT '0',
  `summoner_type` varchar(16) COLLATE utf8_unicode_ci NOT NULL DEFAULT '',
  `target_guid` int(10) unsigned NOT NULL DEFAULT '0',
  `target_id` int(10) unsigned NOT NULL DEFAULT '0',
  `target_type` varchar(16) COLLATE utf8_unicode_ci NOT NULL DEFAULT '',
  PRIMARY KEY (`guid`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_unicode_ci ROW_FORMAT=COMPACT COMMENT='initial value in guid type update fields';

-- Data exporting was unselected.


-- Dumping structure for table sniffs_new_test.player_guid_values_update
DROP TABLE IF EXISTS `player_guid_values_update`;
CREATE TABLE IF NOT EXISTS `player_guid_values_update` (
  `unixtimems` bigint(20) unsigned NOT NULL DEFAULT '0',
  `guid` int(10) unsigned NOT NULL,
  `field_name` varchar(16) COLLATE utf8_unicode_ci NOT NULL DEFAULT '',
  `object_guid` int(10) unsigned NOT NULL DEFAULT '0',
  `object_id` int(10) unsigned NOT NULL DEFAULT '0',
  `object_type` varchar(16) COLLATE utf8_unicode_ci NOT NULL DEFAULT '',
  PRIMARY KEY (`guid`,`unixtimems`,`field_name`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_unicode_ci ROW_FORMAT=COMPACT COMMENT='changes to guid type update fields';

-- Data exporting was unselected.


-- Dumping structure for table sniffs_new_test.player_movement_client
DROP TABLE IF EXISTS `player_movement_client`;
CREATE TABLE IF NOT EXISTS `player_movement_client` (
  `unixtimems` bigint(20) unsigned NOT NULL,
  `guid` int(10) unsigned NOT NULL,
  `opcode` varchar(64) COLLATE utf8_unicode_ci NOT NULL DEFAULT '',
  `move_time` int(10) unsigned NOT NULL DEFAULT '0',
  `move_flags` int(10) unsigned NOT NULL DEFAULT '0',
  `map` smallint(5) unsigned NOT NULL DEFAULT '0',
  `position_x` float NOT NULL DEFAULT '0',
  `position_y` float NOT NULL DEFAULT '0',
  `position_z` float NOT NULL DEFAULT '0',
  `orientation` float NOT NULL DEFAULT '0',
  PRIMARY KEY (`guid`,`opcode`,`unixtimems`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_unicode_ci ROW_FORMAT=COMPACT COMMENT='client side movement';

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
  PRIMARY KEY (`guid`,`point`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_unicode_ci ROW_FORMAT=COMPACT COMMENT='movement points from SMSG_ON_MONSTER_MOVE';

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
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_unicode_ci ROW_FORMAT=COMPACT COMMENT='individual spline points for combat  movement';

-- Data exporting was unselected.


-- Dumping structure for table sniffs_new_test.player_speed_update
DROP TABLE IF EXISTS `player_speed_update`;
CREATE TABLE IF NOT EXISTS `player_speed_update` (
  `unixtimems` bigint(20) unsigned NOT NULL COMMENT 'when the packet was received',
  `guid` int(10) unsigned NOT NULL,
  `speed_type` tinyint(3) unsigned NOT NULL,
  `speed_rate` float unsigned NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_unicode_ci ROW_FORMAT=COMPACT COMMENT='changes to movement speed';

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
  `current_health` int(10) unsigned DEFAULT NULL,
  `max_health` int(10) unsigned DEFAULT NULL,
  `current_mana` int(10) unsigned DEFAULT NULL,
  `max_mana` int(10) unsigned DEFAULT NULL,
  `aura_state` int(10) unsigned DEFAULT NULL,
  `emote_state` int(10) unsigned DEFAULT NULL,
  `stand_state` int(10) unsigned DEFAULT NULL,
  `pet_talent_points` int(10) unsigned DEFAULT NULL,
  `vis_flags` int(10) unsigned DEFAULT NULL,
  `anim_tier` int(10) unsigned DEFAULT NULL,
  `sheath_state` int(10) unsigned DEFAULT NULL,
  `pvp_flags` int(10) unsigned DEFAULT NULL,
  `pet_flags` int(10) unsigned DEFAULT NULL,
  `shapeshift_form` int(10) unsigned DEFAULT NULL,
  `bounding_radius` float DEFAULT NULL,
  `combat_reach` float DEFAULT NULL,
  `mod_melee_haste` float DEFAULT NULL,
  `mod_ranged_haste` float DEFAULT NULL,
  `base_attack_time` int(10) unsigned DEFAULT NULL,
  `ranged_attack_time` int(10) unsigned DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_unicode_ci ROW_FORMAT=COMPACT COMMENT='values updates from SMSG_UPDATE_OBJECT';

-- Data exporting was unselected.


-- Dumping structure for table sniffs_new_test.play_music
DROP TABLE IF EXISTS `play_music`;
CREATE TABLE IF NOT EXISTS `play_music` (
  `unixtimems` bigint(20) unsigned NOT NULL COMMENT 'when the packet was received',
  `music` int(10) unsigned NOT NULL COMMENT 'references SoundEntries.dbc',
  PRIMARY KEY (`music`,`unixtimems`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_unicode_ci COMMENT='from SMSG_PLAY_MUSIC';

-- Data exporting was unselected.


-- Dumping structure for table sniffs_new_test.play_sound
DROP TABLE IF EXISTS `play_sound`;
CREATE TABLE IF NOT EXISTS `play_sound` (
  `unixtimems` bigint(20) unsigned NOT NULL COMMENT 'when the packet was received',
  `source_guid` int(10) unsigned NOT NULL COMMENT 'guid of the object which was the source of the sound',
  `source_id` int(10) unsigned NOT NULL COMMENT 'entry of the object which was the source of the sound',
  `source_type` varchar(16) COLLATE utf8_unicode_ci NOT NULL COMMENT 'type of the object which was the source of the sound',
  `sound` int(10) unsigned NOT NULL COMMENT 'references SoundEntries.dbc',
  PRIMARY KEY (`unixtimems`,`source_guid`,`sound`,`source_id`,`source_type`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_unicode_ci COMMENT='from SMSG_PLAY_SOUND';

-- Data exporting was unselected.


-- Dumping structure for table sniffs_new_test.play_spell_visual_kit
DROP TABLE IF EXISTS `play_spell_visual_kit`;
CREATE TABLE IF NOT EXISTS `play_spell_visual_kit` (
  `unixtimems` bigint(20) unsigned NOT NULL COMMENT 'when the packet was received',
  `caster_guid` int(10) unsigned NOT NULL,
  `caster_id` int(10) unsigned NOT NULL,
  `caster_type` varchar(16) COLLATE utf8_unicode_ci NOT NULL,
  `kit_id` int(10) unsigned NOT NULL COMMENT 'references SpellVisualKit.dbc',
  `kit_type` int(10) unsigned DEFAULT NULL,
  `duration` int(10) unsigned DEFAULT NULL,
  PRIMARY KEY (`caster_id`,`caster_type`,`kit_id`,`unixtimems`,`caster_guid`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_unicode_ci ROW_FORMAT=COMPACT COMMENT='from SMSG_PLAY_SPELL_VISUAL_KIT';

-- Data exporting was unselected.


-- Dumping structure for table sniffs_new_test.points_of_interest
DROP TABLE IF EXISTS `points_of_interest`;
CREATE TABLE IF NOT EXISTS `points_of_interest` (
  `ID` mediumint(8) unsigned NOT NULL DEFAULT '0',
  `PositionX` float NOT NULL DEFAULT '0',
  `PositionY` float NOT NULL DEFAULT '0',
  `Icon` mediumint(8) unsigned NOT NULL DEFAULT '0',
  `Flags` mediumint(8) unsigned NOT NULL DEFAULT '0',
  `Importance` mediumint(8) unsigned NOT NULL DEFAULT '0',
  `Name` text NOT NULL,
  `VerifiedBuild` smallint(5) unsigned DEFAULT '0',
  PRIMARY KEY (`ID`)
) ENGINE=MyISAM DEFAULT CHARSET=utf8;

-- Data exporting was unselected.


-- Dumping structure for table sniffs_new_test.quest_details
DROP TABLE IF EXISTS `quest_details`;
CREATE TABLE IF NOT EXISTS `quest_details` (
  `ID` mediumint(8) unsigned NOT NULL DEFAULT '0',
  `Emote1` smallint(5) unsigned NOT NULL DEFAULT '0',
  `Emote2` smallint(5) unsigned NOT NULL DEFAULT '0',
  `Emote3` smallint(5) unsigned NOT NULL DEFAULT '0',
  `Emote4` smallint(5) unsigned NOT NULL DEFAULT '0',
  `EmoteDelay1` int(10) unsigned NOT NULL DEFAULT '0',
  `EmoteDelay2` int(10) unsigned NOT NULL DEFAULT '0',
  `EmoteDelay3` int(10) unsigned NOT NULL DEFAULT '0',
  `EmoteDelay4` int(10) unsigned NOT NULL DEFAULT '0',
  `VerifiedBuild` smallint(5) unsigned NOT NULL DEFAULT '0',
  PRIMARY KEY (`ID`)
) ENGINE=MyISAM DEFAULT CHARSET=utf8;

-- Data exporting was unselected.


-- Dumping structure for table sniffs_new_test.quest_ender
DROP TABLE IF EXISTS `quest_ender`;
CREATE TABLE IF NOT EXISTS `quest_ender` (
  `object_id` int(10) unsigned NOT NULL COMMENT 'entry of the quest ender object',
  `object_type` varchar(16) COLLATE utf8_unicode_ci NOT NULL COMMENT 'type of the quest ender object',
  `quest_id` int(10) unsigned NOT NULL COMMENT 'references quest_template',
  PRIMARY KEY (`object_id`,`object_type`,`quest_id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_unicode_ci ROW_FORMAT=COMPACT COMMENT='list of quests that can be turned in to a given creature or gameobject';

-- Data exporting was unselected.


-- Dumping structure for table sniffs_new_test.quest_greeting
DROP TABLE IF EXISTS `quest_greeting`;
CREATE TABLE IF NOT EXISTS `quest_greeting` (
  `ID` mediumint(8) unsigned NOT NULL DEFAULT '0',
  `Type` tinyint(3) unsigned NOT NULL DEFAULT '0',
  `GreetEmoteType` smallint(5) unsigned NOT NULL DEFAULT '0',
  `GreetEmoteDelay` int(10) unsigned NOT NULL DEFAULT '0',
  `Greeting` text,
  `VerifiedBuild` smallint(5) unsigned NOT NULL DEFAULT '0',
  PRIMARY KEY (`ID`,`Type`)
) ENGINE=MyISAM DEFAULT CHARSET=utf8;

-- Data exporting was unselected.


-- Dumping structure for table sniffs_new_test.quest_greeting_locale
DROP TABLE IF EXISTS `quest_greeting_locale`;
CREATE TABLE IF NOT EXISTS `quest_greeting_locale` (
  `ID` int(10) unsigned NOT NULL DEFAULT '0',
  `type` tinyint(3) unsigned NOT NULL DEFAULT '0',
  `locale` varchar(4) NOT NULL,
  `Greeting` text,
  `VerifiedBuild` int(10) NOT NULL DEFAULT '0',
  PRIMARY KEY (`ID`,`type`,`locale`)
) ENGINE=MyISAM DEFAULT CHARSET=utf8;

-- Data exporting was unselected.


-- Dumping structure for table sniffs_new_test.quest_objectives
DROP TABLE IF EXISTS `quest_objectives`;
CREATE TABLE IF NOT EXISTS `quest_objectives` (
  `ID` mediumint(8) unsigned NOT NULL DEFAULT '0',
  `QuestID` mediumint(8) unsigned NOT NULL DEFAULT '0',
  `Type` tinyint(3) unsigned NOT NULL DEFAULT '0',
  `StorageIndex` tinyint(3) NOT NULL DEFAULT '0',
  `ObjectID` int(10) NOT NULL DEFAULT '0',
  `Amount` int(10) NOT NULL DEFAULT '0',
  `Flags` int(10) unsigned NOT NULL DEFAULT '0',
  `Flags2` int(10) unsigned NOT NULL DEFAULT '0',
  `ProgressBarWeight` float NOT NULL DEFAULT '0',
  `Description` text,
  `VerifiedBuild` smallint(5) unsigned NOT NULL DEFAULT '0',
  PRIMARY KEY (`ID`)
) ENGINE=MyISAM DEFAULT CHARSET=utf8;

-- Data exporting was unselected.


-- Dumping structure for table sniffs_new_test.quest_objectives_locale
DROP TABLE IF EXISTS `quest_objectives_locale`;
CREATE TABLE IF NOT EXISTS `quest_objectives_locale` (
  `ID` mediumint(8) unsigned NOT NULL DEFAULT '0',
  `locale` varchar(4) NOT NULL,
  `QuestId` mediumint(8) unsigned NOT NULL DEFAULT '0',
  `StorageIndex` tinyint(3) NOT NULL DEFAULT '0',
  `Description` text,
  `VerifiedBuild` int(11) NOT NULL DEFAULT '0',
  PRIMARY KEY (`ID`,`locale`)
) ENGINE=MyISAM DEFAULT CHARSET=utf8;

-- Data exporting was unselected.


-- Dumping structure for table sniffs_new_test.quest_offer_reward
DROP TABLE IF EXISTS `quest_offer_reward`;
CREATE TABLE IF NOT EXISTS `quest_offer_reward` (
  `ID` mediumint(8) unsigned NOT NULL DEFAULT '0',
  `Emote1` smallint(5) unsigned NOT NULL DEFAULT '0',
  `Emote2` smallint(5) unsigned NOT NULL DEFAULT '0',
  `Emote3` smallint(5) unsigned NOT NULL DEFAULT '0',
  `Emote4` smallint(5) unsigned NOT NULL DEFAULT '0',
  `EmoteDelay1` int(10) unsigned NOT NULL DEFAULT '0',
  `EmoteDelay2` int(10) unsigned NOT NULL DEFAULT '0',
  `EmoteDelay3` int(10) unsigned NOT NULL DEFAULT '0',
  `EmoteDelay4` int(10) unsigned NOT NULL DEFAULT '0',
  `RewardText` text,
  `VerifiedBuild` smallint(5) unsigned NOT NULL DEFAULT '0',
  PRIMARY KEY (`ID`)
) ENGINE=MyISAM DEFAULT CHARSET=utf8;

-- Data exporting was unselected.


-- Dumping structure for table sniffs_new_test.quest_offer_reward_locale
DROP TABLE IF EXISTS `quest_offer_reward_locale`;
CREATE TABLE IF NOT EXISTS `quest_offer_reward_locale` (
  `ID` int(10) unsigned NOT NULL DEFAULT '0',
  `locale` varchar(4) NOT NULL,
  `RewardText` text,
  `VerifiedBuild` int(10) NOT NULL DEFAULT '0',
  PRIMARY KEY (`ID`,`locale`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- Data exporting was unselected.


-- Dumping structure for table sniffs_new_test.quest_poi
DROP TABLE IF EXISTS `quest_poi`;
CREATE TABLE IF NOT EXISTS `quest_poi` (
  `QuestID` int(10) unsigned NOT NULL DEFAULT '0',
  `id` int(10) unsigned NOT NULL DEFAULT '0',
  `ObjectiveIndex` int(11) NOT NULL DEFAULT '0',
  `MapID` int(10) unsigned NOT NULL DEFAULT '0',
  `WorldMapAreaId` int(10) unsigned NOT NULL DEFAULT '0',
  `Floor` int(10) unsigned NOT NULL DEFAULT '0',
  `Priority` int(10) unsigned NOT NULL DEFAULT '0',
  `Flags` int(10) unsigned NOT NULL DEFAULT '0',
  `VerifiedBuild` smallint(5) DEFAULT '0',
  PRIMARY KEY (`QuestID`,`id`),
  KEY `idx` (`QuestID`,`id`)
) ENGINE=MyISAM DEFAULT CHARSET=utf8;

-- Data exporting was unselected.


-- Dumping structure for table sniffs_new_test.quest_poi_points
DROP TABLE IF EXISTS `quest_poi_points`;
CREATE TABLE IF NOT EXISTS `quest_poi_points` (
  `QuestID` int(10) unsigned NOT NULL DEFAULT '0',
  `Idx1` int(10) unsigned NOT NULL DEFAULT '0',
  `Idx2` int(10) unsigned NOT NULL DEFAULT '0',
  `X` int(11) NOT NULL DEFAULT '0',
  `Y` int(11) NOT NULL DEFAULT '0',
  `VerifiedBuild` smallint(5) unsigned DEFAULT '0',
  PRIMARY KEY (`QuestID`,`Idx1`,`Idx2`),
  KEY `questId_id` (`QuestID`,`Idx1`)
) ENGINE=MyISAM DEFAULT CHARSET=utf8;

-- Data exporting was unselected.


-- Dumping structure for table sniffs_new_test.quest_request_items
DROP TABLE IF EXISTS `quest_request_items`;
CREATE TABLE IF NOT EXISTS `quest_request_items` (
  `ID` mediumint(8) unsigned NOT NULL DEFAULT '0',
  `Emote` smallint(5) unsigned DEFAULT NULL,
  `CompletionText` text,
  `VerifiedBuild` smallint(5) unsigned NOT NULL DEFAULT '0',
  PRIMARY KEY (`ID`)
) ENGINE=MyISAM DEFAULT CHARSET=utf8;

-- Data exporting was unselected.


-- Dumping structure for table sniffs_new_test.quest_request_items_locale
DROP TABLE IF EXISTS `quest_request_items_locale`;
CREATE TABLE IF NOT EXISTS `quest_request_items_locale` (
  `ID` int(10) unsigned NOT NULL DEFAULT '0',
  `locale` varchar(4) NOT NULL,
  `CompletionText` text,
  `VerifiedBuild` int(10) NOT NULL DEFAULT '0',
  PRIMARY KEY (`ID`,`locale`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- Data exporting was unselected.


-- Dumping structure for table sniffs_new_test.quest_starter
DROP TABLE IF EXISTS `quest_starter`;
CREATE TABLE IF NOT EXISTS `quest_starter` (
  `object_id` int(10) unsigned NOT NULL COMMENT 'entry of the quest giver object',
  `object_type` varchar(16) COLLATE utf8_unicode_ci NOT NULL COMMENT 'type of the quest giver object',
  `quest_id` int(10) unsigned NOT NULL COMMENT 'references quest_template',
  PRIMARY KEY (`object_id`,`object_type`,`quest_id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_unicode_ci COMMENT='list of quests that can be picked up from a given creature or gameobject';

-- Data exporting was unselected.


-- Dumping structure for table sniffs_new_test.quest_template
DROP TABLE IF EXISTS `quest_template`;
CREATE TABLE IF NOT EXISTS `quest_template` (
  `ID` mediumint(8) unsigned NOT NULL DEFAULT '0',
  `QuestType` tinyint(3) unsigned NOT NULL DEFAULT '2',
  `QuestLevel` smallint(3) NOT NULL DEFAULT '1',
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
  `RewardMoney` int(11) NOT NULL DEFAULT '0',
  `RewardBonusMoney` int(10) unsigned NOT NULL DEFAULT '0',
  `RewardDisplaySpell` mediumint(8) unsigned NOT NULL DEFAULT '0',
  `RewardSpell` int(11) NOT NULL DEFAULT '0',
  `RewardHonor` int(11) NOT NULL DEFAULT '0',
  `RewardKillHonor` float NOT NULL DEFAULT '0',
  `StartItem` mediumint(8) unsigned NOT NULL DEFAULT '0',
  `Flags` int(10) unsigned NOT NULL DEFAULT '0',
  `RequiredPlayerKills` tinyint(3) unsigned NOT NULL DEFAULT '0',
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
  `RewardChoiceItemID2` mediumint(8) unsigned NOT NULL DEFAULT '0',
  `RewardChoiceItemQuantity2` smallint(5) unsigned NOT NULL DEFAULT '0',
  `RewardChoiceItemID3` mediumint(8) unsigned NOT NULL DEFAULT '0',
  `RewardChoiceItemQuantity3` smallint(5) unsigned NOT NULL DEFAULT '0',
  `RewardChoiceItemID4` mediumint(8) unsigned NOT NULL DEFAULT '0',
  `RewardChoiceItemQuantity4` smallint(5) unsigned NOT NULL DEFAULT '0',
  `RewardChoiceItemID5` mediumint(8) unsigned NOT NULL DEFAULT '0',
  `RewardChoiceItemQuantity5` smallint(5) unsigned NOT NULL DEFAULT '0',
  `RewardChoiceItemID6` mediumint(8) unsigned NOT NULL DEFAULT '0',
  `RewardChoiceItemQuantity6` smallint(5) unsigned NOT NULL DEFAULT '0',
  `POIContinent` smallint(5) unsigned NOT NULL DEFAULT '0',
  `POIx` float NOT NULL DEFAULT '0',
  `POIy` float NOT NULL DEFAULT '0',
  `POIPriority` mediumint(8) unsigned NOT NULL DEFAULT '0',
  `RewardTitle` tinyint(3) unsigned NOT NULL DEFAULT '0',
  `RewardTalents` tinyint(3) unsigned NOT NULL DEFAULT '0',
  `RewardArenaPoints` smallint(5) unsigned NOT NULL DEFAULT '0',
  `RewardFactionID1` smallint(5) unsigned NOT NULL DEFAULT '0' COMMENT 'faction id from Faction.dbc in this case',
  `RewardFactionValue1` mediumint(8) NOT NULL DEFAULT '0',
  `RewardFactionOverride1` mediumint(8) NOT NULL DEFAULT '0',
  `RewardFactionID2` smallint(5) unsigned NOT NULL DEFAULT '0' COMMENT 'faction id from Faction.dbc in this case',
  `RewardFactionValue2` mediumint(8) NOT NULL DEFAULT '0',
  `RewardFactionOverride2` mediumint(8) NOT NULL DEFAULT '0',
  `RewardFactionID3` smallint(5) unsigned NOT NULL DEFAULT '0' COMMENT 'faction id from Faction.dbc in this case',
  `RewardFactionValue3` mediumint(8) NOT NULL DEFAULT '0',
  `RewardFactionOverride3` mediumint(8) NOT NULL DEFAULT '0',
  `RewardFactionID4` smallint(5) unsigned NOT NULL DEFAULT '0' COMMENT 'faction id from Faction.dbc in this case',
  `RewardFactionValue4` mediumint(8) NOT NULL DEFAULT '0',
  `RewardFactionOverride4` mediumint(8) NOT NULL DEFAULT '0',
  `RewardFactionID5` smallint(5) unsigned NOT NULL DEFAULT '0' COMMENT 'faction id from Faction.dbc in this case',
  `RewardFactionValue5` mediumint(8) NOT NULL DEFAULT '0',
  `RewardFactionOverride5` mediumint(8) NOT NULL DEFAULT '0',
  `TimeAllowed` int(10) unsigned NOT NULL DEFAULT '0',
  `AllowableRaces` smallint(5) unsigned NOT NULL DEFAULT '0',
  `LogTitle` text,
  `LogDescription` text,
  `QuestDescription` text,
  `AreaDescription` text,
  `QuestCompletionLog` text,
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
  `ObjectiveText1` text,
  `ObjectiveText2` text,
  `ObjectiveText3` text,
  `ObjectiveText4` text,
  `VerifiedBuild` smallint(5) unsigned DEFAULT '0',
  PRIMARY KEY (`ID`)
) ENGINE=MyISAM DEFAULT CHARSET=utf8 COMMENT='Quest System';

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
  `VerifiedBuild` int(11) NOT NULL DEFAULT '0',
  PRIMARY KEY (`ID`,`locale`)
) ENGINE=MyISAM DEFAULT CHARSET=utf8;

-- Data exporting was unselected.


-- Dumping structure for table sniffs_new_test.quest_update_complete
DROP TABLE IF EXISTS `quest_update_complete`;
CREATE TABLE IF NOT EXISTS `quest_update_complete` (
  `unixtimems` bigint(20) unsigned NOT NULL COMMENT 'when the packet was received',
  `quest_id` int(10) unsigned NOT NULL COMMENT 'quest template entry',
  PRIMARY KEY (`quest_id`,`unixtimems`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_unicode_ci ROW_FORMAT=COMPACT COMMENT='from SMSG_QUEST_UPDATE_COMPLETE';

-- Data exporting was unselected.


-- Dumping structure for table sniffs_new_test.quest_update_failed
DROP TABLE IF EXISTS `quest_update_failed`;
CREATE TABLE IF NOT EXISTS `quest_update_failed` (
  `unixtimems` bigint(20) unsigned NOT NULL COMMENT 'when the packet was received',
  `quest_id` int(10) unsigned NOT NULL COMMENT 'quest template entry',
  PRIMARY KEY (`quest_id`,`unixtimems`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_unicode_ci ROW_FORMAT=COMPACT COMMENT='from SMSG_QUEST_UPDATE_FAILED and SMSG_QUEST_UPDATE_FAILED_TIMER';

-- Data exporting was unselected.


-- Dumping structure for table sniffs_new_test.sniff_file
DROP TABLE IF EXISTS `sniff_file`;
CREATE TABLE IF NOT EXISTS `sniff_file` (
  `id` int(10) unsigned NOT NULL,
  `build` int(10) unsigned NOT NULL DEFAULT '0',
  `name` varchar(256) COLLATE utf8_unicode_ci DEFAULT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_unicode_ci;

-- Data exporting was unselected.


-- Dumping structure for table sniffs_new_test.spell_cast_failed
DROP TABLE IF EXISTS `spell_cast_failed`;
CREATE TABLE IF NOT EXISTS `spell_cast_failed` (
  `unixtimems` bigint(20) unsigned NOT NULL COMMENT 'when the packet was received',
  `caster_guid` int(10) unsigned NOT NULL,
  `caster_id` int(10) unsigned NOT NULL,
  `caster_type` varchar(16) COLLATE utf8_unicode_ci NOT NULL,
  `spell_id` int(10) unsigned NOT NULL,
  `visual_id` int(10) unsigned NOT NULL DEFAULT '0',
  `reason` int(10) unsigned NOT NULL DEFAULT '0',
  PRIMARY KEY (`caster_id`,`caster_type`,`spell_id`,`unixtimems`,`caster_guid`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_unicode_ci ROW_FORMAT=COMPACT COMMENT='from SMSG_SPELL_FAILURE and SMSG_SPELL_FAILED_OTHER';

-- Data exporting was unselected.


-- Dumping structure for table sniffs_new_test.spell_cast_go
DROP TABLE IF EXISTS `spell_cast_go`;
CREATE TABLE IF NOT EXISTS `spell_cast_go` (
  `unixtimems` bigint(20) unsigned NOT NULL DEFAULT '0' COMMENT 'when the packet was received',
  `caster_guid` int(10) unsigned NOT NULL DEFAULT '0',
  `caster_id` int(10) unsigned NOT NULL DEFAULT '0',
  `caster_type` varchar(16) COLLATE utf8_unicode_ci NOT NULL DEFAULT '',
  `caster_unit_guid` int(10) unsigned NOT NULL DEFAULT '0',
  `caster_unit_id` int(10) unsigned NOT NULL DEFAULT '0',
  `caster_unit_type` varchar(16) COLLATE utf8_unicode_ci NOT NULL DEFAULT '',
  `spell_id` int(10) unsigned NOT NULL DEFAULT '0',
  `visual_id` int(10) unsigned NOT NULL DEFAULT '0',
  `cast_flags` int(10) unsigned NOT NULL DEFAULT '0',
  `cast_flags_ex` int(10) unsigned NOT NULL DEFAULT '0',
  `ammo_display_id` int(10) NOT NULL DEFAULT '0',
  `ammo_inventory_type` int(10) NOT NULL DEFAULT '0',
  `main_target_guid` int(10) unsigned NOT NULL DEFAULT '0',
  `main_target_id` int(10) unsigned NOT NULL DEFAULT '0',
  `main_target_type` varchar(50) COLLATE utf8_unicode_ci NOT NULL DEFAULT '',
  `hit_targets_count` tinyint(3) unsigned NOT NULL DEFAULT '0',
  `hit_targets_list_id` int(10) unsigned NOT NULL DEFAULT '0',
  `miss_targets_count` tinyint(3) unsigned NOT NULL DEFAULT '0',
  `miss_targets_list_id` int(10) unsigned NOT NULL DEFAULT '0',
  `src_position_id` int(10) unsigned NOT NULL DEFAULT '0',
  `dst_position_id` int(10) unsigned NOT NULL DEFAULT '0',
  PRIMARY KEY (`unixtimems`,`caster_guid`,`caster_id`,`caster_type`,`spell_id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_unicode_ci COMMENT='from SMSG_SPELL_GO\r\nsent when a spell is successfully casted';

-- Data exporting was unselected.


-- Dumping structure for table sniffs_new_test.spell_cast_go_position
DROP TABLE IF EXISTS `spell_cast_go_position`;
CREATE TABLE IF NOT EXISTS `spell_cast_go_position` (
  `id` int(10) unsigned NOT NULL,
  `position_x` float NOT NULL DEFAULT '0',
  `position_y` float NOT NULL DEFAULT '0',
  `position_z` float NOT NULL DEFAULT '0',
  PRIMARY KEY (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_unicode_ci ROW_FORMAT=COMPACT COMMENT='source and destination locations from SMSG_SPELL_GO';

-- Data exporting was unselected.


-- Dumping structure for table sniffs_new_test.spell_cast_go_target
DROP TABLE IF EXISTS `spell_cast_go_target`;
CREATE TABLE IF NOT EXISTS `spell_cast_go_target` (
  `list_id` int(10) unsigned NOT NULL DEFAULT '0',
  `target_guid` int(10) unsigned NOT NULL DEFAULT '0',
  `target_id` int(10) unsigned NOT NULL DEFAULT '0',
  `target_type` varchar(16) COLLATE utf8_unicode_ci NOT NULL DEFAULT ''
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_unicode_ci ROW_FORMAT=COMPACT COMMENT='hit and miss targets from SMSG_SPELL_GO';

-- Data exporting was unselected.


-- Dumping structure for table sniffs_new_test.spell_cast_start
DROP TABLE IF EXISTS `spell_cast_start`;
CREATE TABLE IF NOT EXISTS `spell_cast_start` (
  `unixtimems` bigint(20) unsigned NOT NULL DEFAULT '0' COMMENT 'when the packet was received',
  `caster_guid` int(10) unsigned NOT NULL DEFAULT '0',
  `caster_id` int(10) unsigned NOT NULL DEFAULT '0',
  `caster_type` varchar(16) COLLATE utf8_unicode_ci NOT NULL DEFAULT '',
  `caster_unit_guid` int(10) unsigned NOT NULL DEFAULT '0',
  `caster_unit_id` int(10) unsigned NOT NULL DEFAULT '0',
  `caster_unit_type` varchar(16) COLLATE utf8_unicode_ci NOT NULL DEFAULT '',
  `spell_id` int(10) unsigned NOT NULL DEFAULT '0',
  `visual_id` int(10) unsigned NOT NULL DEFAULT '0',
  `cast_time` int(10) unsigned NOT NULL DEFAULT '0',
  `cast_flags` int(10) unsigned NOT NULL DEFAULT '0',
  `cast_flags_ex` int(10) unsigned NOT NULL DEFAULT '0',
  `ammo_display_id` int(10) NOT NULL DEFAULT '0',
  `ammo_inventory_type` int(10) NOT NULL DEFAULT '0',
  `target_guid` int(10) unsigned NOT NULL DEFAULT '0',
  `target_id` int(10) unsigned NOT NULL DEFAULT '0',
  `target_type` varchar(50) COLLATE utf8_unicode_ci NOT NULL DEFAULT '',
  PRIMARY KEY (`unixtimems`,`caster_guid`,`caster_id`,`caster_type`,`spell_id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_unicode_ci COMMENT='from SMSG_SPELL_START\r\nsent when somebody starts casting a spell';

-- Data exporting was unselected.


-- Dumping structure for table sniffs_new_test.spell_channel_start
DROP TABLE IF EXISTS `spell_channel_start`;
CREATE TABLE IF NOT EXISTS `spell_channel_start` (
  `unixtimems` bigint(20) unsigned NOT NULL DEFAULT '0' COMMENT 'when the packet was received',
  `caster_guid` int(10) unsigned NOT NULL DEFAULT '0',
  `caster_id` int(10) unsigned NOT NULL DEFAULT '0',
  `caster_type` varchar(16) COLLATE utf8_unicode_ci NOT NULL DEFAULT '',
  `spell_id` int(10) unsigned NOT NULL DEFAULT '0',
  `visual_id` int(10) unsigned NOT NULL DEFAULT '0',
  `duration` int(10) NOT NULL DEFAULT '0',
  PRIMARY KEY (`unixtimems`,`caster_guid`,`caster_id`,`caster_type`,`spell_id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_unicode_ci ROW_FORMAT=COMPACT COMMENT='from SMSG_SPELL_CHANNEL_START\r\nsent when somebody starts channeling a spell';

-- Data exporting was unselected.


-- Dumping structure for table sniffs_new_test.spell_channel_update
DROP TABLE IF EXISTS `spell_channel_update`;
CREATE TABLE IF NOT EXISTS `spell_channel_update` (
  `unixtimems` bigint(20) unsigned NOT NULL DEFAULT '0' COMMENT 'when the packet was received',
  `caster_guid` int(10) unsigned NOT NULL DEFAULT '0',
  `caster_id` int(10) unsigned NOT NULL DEFAULT '0',
  `caster_type` varchar(16) COLLATE utf8_unicode_ci NOT NULL DEFAULT '',
  `duration` int(10) NOT NULL DEFAULT '0',
  PRIMARY KEY (`unixtimems`,`caster_guid`,`caster_id`,`caster_type`,`duration`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_unicode_ci ROW_FORMAT=COMPACT COMMENT='from SMSG_SPELL_CHANNEL_UPDATE';

-- Data exporting was unselected.


-- Dumping structure for table sniffs_new_test.spell_pet_actions
DROP TABLE IF EXISTS `spell_pet_actions`;
CREATE TABLE IF NOT EXISTS `spell_pet_actions` (
  `creature_id` int(10) unsigned NOT NULL DEFAULT '0',
  `slot1` int(10) unsigned NOT NULL DEFAULT '0',
  `slot2` int(10) unsigned NOT NULL DEFAULT '0',
  `slot3` int(10) unsigned NOT NULL DEFAULT '0',
  `slot4` int(10) unsigned NOT NULL DEFAULT '0',
  `slot5` int(10) unsigned NOT NULL DEFAULT '0',
  `slot6` int(10) unsigned NOT NULL DEFAULT '0',
  `slot7` int(10) unsigned NOT NULL DEFAULT '0',
  `slot8` int(10) unsigned NOT NULL DEFAULT '0',
  `slot9` int(10) unsigned NOT NULL DEFAULT '0',
  `slot10` int(10) unsigned NOT NULL DEFAULT '0'
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_unicode_ci;

-- Data exporting was unselected.


-- Dumping structure for table sniffs_new_test.spell_pet_cooldown
DROP TABLE IF EXISTS `spell_pet_cooldown`;
CREATE TABLE IF NOT EXISTS `spell_pet_cooldown` (
  `creature_id` int(10) unsigned NOT NULL DEFAULT '0',
  `flags` tinyint(3) unsigned NOT NULL DEFAULT '0',
  `index` tinyint(3) unsigned NOT NULL DEFAULT '0',
  `spell_id` int(10) unsigned NOT NULL DEFAULT '0',
  `cooldown` int(10) unsigned NOT NULL DEFAULT '0',
  `mod_rate` float unsigned NOT NULL DEFAULT '1'
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_unicode_ci;

-- Data exporting was unselected.


-- Dumping structure for table sniffs_new_test.spell_target_position
DROP TABLE IF EXISTS `spell_target_position`;
CREATE TABLE IF NOT EXISTS `spell_target_position` (
  `ID` mediumint(8) unsigned NOT NULL DEFAULT '0' COMMENT 'Identifier',
  `EffectIndex` tinyint(3) unsigned NOT NULL DEFAULT '0',
  `MapID` smallint(5) unsigned NOT NULL DEFAULT '0',
  `PositionX` float NOT NULL DEFAULT '0',
  `PositionY` float NOT NULL DEFAULT '0',
  `PositionZ` float NOT NULL DEFAULT '0',
  `Orientation` float NOT NULL DEFAULT '0',
  `VerifiedBuild` smallint(5) unsigned DEFAULT '0',
  PRIMARY KEY (`ID`,`EffectIndex`)
) ENGINE=MyISAM DEFAULT CHARSET=utf8 ROW_FORMAT=FIXED COMMENT='Spell System';

-- Data exporting was unselected.


-- Dumping structure for table sniffs_new_test.trainer
DROP TABLE IF EXISTS `trainer`;
CREATE TABLE IF NOT EXISTS `trainer` (
  `Id` int(10) unsigned NOT NULL DEFAULT '0',
  `Type` tinyint(2) unsigned NOT NULL DEFAULT '2',
  `Requirement` mediumint(8) unsigned NOT NULL DEFAULT '0',
  `Greeting` text,
  `VerifiedBuild` smallint(5) unsigned DEFAULT '0',
  PRIMARY KEY (`Id`)
) ENGINE=MyISAM DEFAULT CHARSET=utf8;

-- Data exporting was unselected.


-- Dumping structure for table sniffs_new_test.trainer_spell
DROP TABLE IF EXISTS `trainer_spell`;
CREATE TABLE IF NOT EXISTS `trainer_spell` (
  `TrainerId` int(10) unsigned NOT NULL DEFAULT '0',
  `SpellId` int(10) unsigned NOT NULL DEFAULT '0',
  `MoneyCost` int(10) unsigned NOT NULL DEFAULT '0',
  `ReqSkillLine` int(10) unsigned NOT NULL DEFAULT '0',
  `ReqSkillRank` int(10) unsigned NOT NULL DEFAULT '0',
  `ReqAbility1` int(10) unsigned NOT NULL DEFAULT '0',
  `ReqAbility2` int(10) unsigned NOT NULL DEFAULT '0',
  `ReqAbility3` int(10) unsigned NOT NULL DEFAULT '0',
  `ReqLevel` tinyint(3) unsigned NOT NULL DEFAULT '0',
  `VerifiedBuild` smallint(5) unsigned DEFAULT '0',
  PRIMARY KEY (`TrainerId`,`SpellId`)
) ENGINE=MyISAM DEFAULT CHARSET=utf8;

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
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_unicode_ci;

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
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_unicode_ci COMMENT='from SMSG_INIT_WORLD_STATES';

-- Data exporting was unselected.


-- Dumping structure for table sniffs_new_test.world_state_update
DROP TABLE IF EXISTS `world_state_update`;
CREATE TABLE IF NOT EXISTS `world_state_update` (
  `unixtimems` bigint(20) unsigned NOT NULL DEFAULT '0',
  `variable` int(11) NOT NULL DEFAULT '0',
  `value` int(11) NOT NULL DEFAULT '0',
  PRIMARY KEY (`unixtimems`,`variable`,`value`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_unicode_ci ROW_FORMAT=COMPACT COMMENT='from SMSG_UPDATE_WORLD_STATE';

-- Data exporting was unselected.


-- Dumping structure for table sniffs_new_test.world_text
DROP TABLE IF EXISTS `world_text`;
CREATE TABLE IF NOT EXISTS `world_text` (
  `unixtimems` bigint(20) unsigned NOT NULL DEFAULT '0' COMMENT 'when the packet was received',
  `text` longtext COMMENT 'the actual text that was sent',
  `chat_type` tinyint(3) unsigned NOT NULL DEFAULT '0' COMMENT 'chat type',
  `language` tinyint(3) NOT NULL DEFAULT '0' COMMENT 'references Languages.dbc',
  PRIMARY KEY (`unixtimems`)
) ENGINE=MyISAM DEFAULT CHARSET=utf8 ROW_FORMAT=DYNAMIC COMMENT='unique texts per creature id';

-- Data exporting was unselected.
/*!40101 SET SQL_MODE=IFNULL(@OLD_SQL_MODE, '') */;
/*!40014 SET FOREIGN_KEY_CHECKS=IF(@OLD_FOREIGN_KEY_CHECKS IS NULL, 1, @OLD_FOREIGN_KEY_CHECKS) */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;

ALTER TABLE `creature_movement_client`
	ADD COLUMN `swim_pitch` FLOAT NOT NULL DEFAULT '0' AFTER `orientation`,
	ADD COLUMN `fall_time` INT NOT NULL DEFAULT '0' AFTER `swim_pitch`,
	ADD COLUMN `jump_horizontal_speed` FLOAT NOT NULL DEFAULT '0' AFTER `fall_time`,
	ADD COLUMN `jump_vertical_speed` FLOAT NOT NULL DEFAULT '0' AFTER `jump_horizontal_speed`,
	ADD COLUMN `jump_cos_angle` FLOAT NOT NULL DEFAULT '0' AFTER `jump_vertical_speed`,
	ADD COLUMN `jump_sin_angle` FLOAT NOT NULL DEFAULT '0' AFTER `jump_cos_angle`;

ALTER TABLE `player_movement_client`
	ADD COLUMN `swim_pitch` FLOAT NOT NULL DEFAULT '0' AFTER `orientation`,
	ADD COLUMN `fall_time` INT NOT NULL DEFAULT '0' AFTER `swim_pitch`,
	ADD COLUMN `jump_horizontal_speed` FLOAT NOT NULL DEFAULT '0' AFTER `fall_time`,
	ADD COLUMN `jump_vertical_speed` FLOAT NOT NULL DEFAULT '0' AFTER `jump_horizontal_speed`,
	ADD COLUMN `jump_cos_angle` FLOAT NOT NULL DEFAULT '0' AFTER `jump_vertical_speed`,
	ADD COLUMN `jump_sin_angle` FLOAT NOT NULL DEFAULT '0' AFTER `jump_cos_angle`;

ALTER TABLE `creature_movement_client`
	ADD COLUMN `move_flags2` INT(10) UNSIGNED NOT NULL DEFAULT '0' AFTER `move_flags`,
	ADD COLUMN `transport_guid` INT UNSIGNED NOT NULL DEFAULT '0' AFTER `orientation`,
	ADD COLUMN `transport_x` FLOAT NOT NULL DEFAULT '0' AFTER `transport_guid`,
	ADD COLUMN `transport_y` FLOAT NOT NULL DEFAULT '0' AFTER `transport_x`,
	ADD COLUMN `transport_z` FLOAT NOT NULL DEFAULT '0' AFTER `transport_y`,
	ADD COLUMN `transport_o` FLOAT NOT NULL DEFAULT '0' AFTER `transport_z`,
	ADD COLUMN `spline_elevation` FLOAT NOT NULL DEFAULT '0' AFTER `jump_sin_angle`;

ALTER TABLE `player_movement_client`
	ADD COLUMN `move_flags2` INT(10) UNSIGNED NOT NULL DEFAULT '0' AFTER `move_flags`,
	ADD COLUMN `transport_guid` INT UNSIGNED NOT NULL DEFAULT '0' AFTER `orientation`,
	ADD COLUMN `transport_x` FLOAT NOT NULL DEFAULT '0' AFTER `transport_guid`,
	ADD COLUMN `transport_y` FLOAT NOT NULL DEFAULT '0' AFTER `transport_x`,
	ADD COLUMN `transport_z` FLOAT NOT NULL DEFAULT '0' AFTER `transport_y`,
	ADD COLUMN `transport_o` FLOAT NOT NULL DEFAULT '0' AFTER `transport_z`,
	ADD COLUMN `spline_elevation` FLOAT NOT NULL DEFAULT '0' AFTER `jump_sin_angle`;

ALTER TABLE `creature_movement_client`
	CHANGE COLUMN `fall_time` `fall_time` INT(11) UNSIGNED NOT NULL DEFAULT '0' AFTER `swim_pitch`;

ALTER TABLE `player_movement_client`
	CHANGE COLUMN `fall_time` `fall_time` INT(11) UNSIGNED NOT NULL DEFAULT '0' AFTER `swim_pitch`;

ALTER TABLE `creature`
	DROP COLUMN `move_flags`;

ALTER TABLE `player`
	DROP COLUMN `move_flags`;

ALTER TABLE `creature_create1_time`
	ADD COLUMN `transport_guid` INT UNSIGNED NOT NULL DEFAULT '0' AFTER `orientation`,
	ADD COLUMN `transport_x` FLOAT NOT NULL DEFAULT '0' AFTER `transport_guid`,
	ADD COLUMN `transport_y` FLOAT NOT NULL DEFAULT '0' AFTER `transport_x`,
	ADD COLUMN `transport_z` FLOAT NOT NULL DEFAULT '0' AFTER `transport_y`,
	ADD COLUMN `transport_o` FLOAT NOT NULL DEFAULT '0' AFTER `transport_z`,
	ADD COLUMN `move_time` INT UNSIGNED NOT NULL DEFAULT '0' AFTER `transport_o`,
	ADD COLUMN `move_flags` INT UNSIGNED NOT NULL DEFAULT '0' AFTER `move_time`,
	ADD COLUMN `move_flags2` INT UNSIGNED NOT NULL DEFAULT '0' AFTER `move_flags`,
	ADD COLUMN `swim_pitch` FLOAT NOT NULL DEFAULT '0' AFTER `move_flags2`,
	ADD COLUMN `fall_time` INT UNSIGNED NOT NULL DEFAULT '0' AFTER `swim_pitch`,
	ADD COLUMN `jump_horizontal_speed` FLOAT NOT NULL DEFAULT '0' AFTER `fall_time`,
	ADD COLUMN `jump_vertical_speed` FLOAT NOT NULL DEFAULT '0' AFTER `jump_horizontal_speed`,
	ADD COLUMN `jump_cos_angle` FLOAT NOT NULL DEFAULT '0' AFTER `jump_vertical_speed`,
	ADD COLUMN `jump_sin_angle` FLOAT NOT NULL DEFAULT '0' AFTER `jump_cos_angle`,
	ADD COLUMN `spline_elevation` FLOAT NOT NULL DEFAULT '0' AFTER `jump_sin_angle`;

ALTER TABLE `creature_create2_time`
	ADD COLUMN `transport_guid` INT UNSIGNED NOT NULL DEFAULT '0' AFTER `orientation`,
	ADD COLUMN `transport_x` FLOAT NOT NULL DEFAULT '0' AFTER `transport_guid`,
	ADD COLUMN `transport_y` FLOAT NOT NULL DEFAULT '0' AFTER `transport_x`,
	ADD COLUMN `transport_z` FLOAT NOT NULL DEFAULT '0' AFTER `transport_y`,
	ADD COLUMN `transport_o` FLOAT NOT NULL DEFAULT '0' AFTER `transport_z`,
	ADD COLUMN `move_time` INT UNSIGNED NOT NULL DEFAULT '0' AFTER `transport_o`,
	ADD COLUMN `move_flags` INT UNSIGNED NOT NULL DEFAULT '0' AFTER `move_time`,
	ADD COLUMN `move_flags2` INT UNSIGNED NOT NULL DEFAULT '0' AFTER `move_flags`,
	ADD COLUMN `swim_pitch` FLOAT NOT NULL DEFAULT '0' AFTER `move_flags2`,
	ADD COLUMN `fall_time` INT UNSIGNED NOT NULL DEFAULT '0' AFTER `swim_pitch`,
	ADD COLUMN `jump_horizontal_speed` FLOAT NOT NULL DEFAULT '0' AFTER `fall_time`,
	ADD COLUMN `jump_vertical_speed` FLOAT NOT NULL DEFAULT '0' AFTER `jump_horizontal_speed`,
	ADD COLUMN `jump_cos_angle` FLOAT NOT NULL DEFAULT '0' AFTER `jump_vertical_speed`,
	ADD COLUMN `jump_sin_angle` FLOAT NOT NULL DEFAULT '0' AFTER `jump_cos_angle`,
	ADD COLUMN `spline_elevation` FLOAT NOT NULL DEFAULT '0' AFTER `jump_sin_angle`;

ALTER TABLE `player_create1_time`
	ADD COLUMN `transport_guid` INT UNSIGNED NOT NULL DEFAULT '0' AFTER `orientation`,
	ADD COLUMN `transport_x` FLOAT NOT NULL DEFAULT '0' AFTER `transport_guid`,
	ADD COLUMN `transport_y` FLOAT NOT NULL DEFAULT '0' AFTER `transport_x`,
	ADD COLUMN `transport_z` FLOAT NOT NULL DEFAULT '0' AFTER `transport_y`,
	ADD COLUMN `transport_o` FLOAT NOT NULL DEFAULT '0' AFTER `transport_z`,
	ADD COLUMN `move_time` INT UNSIGNED NOT NULL DEFAULT '0' AFTER `transport_o`,
	ADD COLUMN `move_flags` INT UNSIGNED NOT NULL DEFAULT '0' AFTER `move_time`,
	ADD COLUMN `move_flags2` INT UNSIGNED NOT NULL DEFAULT '0' AFTER `move_flags`,
	ADD COLUMN `swim_pitch` FLOAT NOT NULL DEFAULT '0' AFTER `move_flags2`,
	ADD COLUMN `fall_time` INT UNSIGNED NOT NULL DEFAULT '0' AFTER `swim_pitch`,
	ADD COLUMN `jump_horizontal_speed` FLOAT NOT NULL DEFAULT '0' AFTER `fall_time`,
	ADD COLUMN `jump_vertical_speed` FLOAT NOT NULL DEFAULT '0' AFTER `jump_horizontal_speed`,
	ADD COLUMN `jump_cos_angle` FLOAT NOT NULL DEFAULT '0' AFTER `jump_vertical_speed`,
	ADD COLUMN `jump_sin_angle` FLOAT NOT NULL DEFAULT '0' AFTER `jump_cos_angle`,
	ADD COLUMN `spline_elevation` FLOAT NOT NULL DEFAULT '0' AFTER `jump_sin_angle`;

ALTER TABLE `player_create2_time`
	ADD COLUMN `transport_guid` INT UNSIGNED NOT NULL DEFAULT '0' AFTER `orientation`,
	ADD COLUMN `transport_x` FLOAT NOT NULL DEFAULT '0' AFTER `transport_guid`,
	ADD COLUMN `transport_y` FLOAT NOT NULL DEFAULT '0' AFTER `transport_x`,
	ADD COLUMN `transport_z` FLOAT NOT NULL DEFAULT '0' AFTER `transport_y`,
	ADD COLUMN `transport_o` FLOAT NOT NULL DEFAULT '0' AFTER `transport_z`,
	ADD COLUMN `move_time` INT UNSIGNED NOT NULL DEFAULT '0' AFTER `transport_o`,
	ADD COLUMN `move_flags` INT UNSIGNED NOT NULL DEFAULT '0' AFTER `move_time`,
	ADD COLUMN `move_flags2` INT UNSIGNED NOT NULL DEFAULT '0' AFTER `move_flags`,
	ADD COLUMN `swim_pitch` FLOAT NOT NULL DEFAULT '0' AFTER `move_flags2`,
	ADD COLUMN `fall_time` INT UNSIGNED NOT NULL DEFAULT '0' AFTER `swim_pitch`,
	ADD COLUMN `jump_horizontal_speed` FLOAT NOT NULL DEFAULT '0' AFTER `fall_time`,
	ADD COLUMN `jump_vertical_speed` FLOAT NOT NULL DEFAULT '0' AFTER `jump_horizontal_speed`,
	ADD COLUMN `jump_cos_angle` FLOAT NOT NULL DEFAULT '0' AFTER `jump_vertical_speed`,
	ADD COLUMN `jump_sin_angle` FLOAT NOT NULL DEFAULT '0' AFTER `jump_cos_angle`,
	ADD COLUMN `spline_elevation` FLOAT NOT NULL DEFAULT '0' AFTER `jump_sin_angle`;
  
ALTER TABLE `dynamicobject_create1_time`
	ADD COLUMN `transport_guid` INT UNSIGNED NOT NULL DEFAULT '0' AFTER `orientation`,
	ADD COLUMN `transport_x` FLOAT NOT NULL DEFAULT '0' AFTER `transport_guid`,
	ADD COLUMN `transport_y` FLOAT NOT NULL DEFAULT '0' AFTER `transport_x`,
	ADD COLUMN `transport_z` FLOAT NOT NULL DEFAULT '0' AFTER `transport_y`,
	ADD COLUMN `transport_o` FLOAT NOT NULL DEFAULT '0' AFTER `transport_z`;

ALTER TABLE `dynamicobject_create2_time`
	ADD COLUMN `transport_guid` INT UNSIGNED NOT NULL DEFAULT '0' AFTER `orientation`,
	ADD COLUMN `transport_x` FLOAT NOT NULL DEFAULT '0' AFTER `transport_guid`,
	ADD COLUMN `transport_y` FLOAT NOT NULL DEFAULT '0' AFTER `transport_x`,
	ADD COLUMN `transport_z` FLOAT NOT NULL DEFAULT '0' AFTER `transport_y`,
	ADD COLUMN `transport_o` FLOAT NOT NULL DEFAULT '0' AFTER `transport_z`;

ALTER TABLE `gameobject_create1_time`
	ADD COLUMN `transport_guid` INT UNSIGNED NOT NULL DEFAULT '0' AFTER `orientation`,
	ADD COLUMN `transport_x` FLOAT NOT NULL DEFAULT '0' AFTER `transport_guid`,
	ADD COLUMN `transport_y` FLOAT NOT NULL DEFAULT '0' AFTER `transport_x`,
	ADD COLUMN `transport_z` FLOAT NOT NULL DEFAULT '0' AFTER `transport_y`,
	ADD COLUMN `transport_o` FLOAT NOT NULL DEFAULT '0' AFTER `transport_z`;

ALTER TABLE `gameobject_create2_time`
	ADD COLUMN `transport_guid` INT UNSIGNED NOT NULL DEFAULT '0' AFTER `orientation`,
	ADD COLUMN `transport_x` FLOAT NOT NULL DEFAULT '0' AFTER `transport_guid`,
	ADD COLUMN `transport_y` FLOAT NOT NULL DEFAULT '0' AFTER `transport_x`,
	ADD COLUMN `transport_z` FLOAT NOT NULL DEFAULT '0' AFTER `transport_y`,
	ADD COLUMN `transport_o` FLOAT NOT NULL DEFAULT '0' AFTER `transport_z`;

ALTER TABLE `gameobject_create1_time`
	ADD COLUMN `transport_path_timer` INT UNSIGNED NOT NULL DEFAULT '0' AFTER `transport_o`;

ALTER TABLE `gameobject_create2_time`
	ADD COLUMN `transport_path_timer` INT UNSIGNED NOT NULL DEFAULT '0' AFTER `transport_o`;

ALTER TABLE `creature_movement_server`
	ADD COLUMN `transport_guid` INT UNSIGNED NOT NULL DEFAULT '0' AFTER `orientation`;

ALTER TABLE `creature_movement_server_combat`
	ADD COLUMN `transport_guid` INT UNSIGNED NOT NULL DEFAULT '0' AFTER `orientation`;

ALTER TABLE `player_movement_server`
	ADD COLUMN `transport_guid` INT UNSIGNED NOT NULL DEFAULT '0' AFTER `orientation`;

ALTER TABLE `creature_text`
	ADD COLUMN `target_guid` INT UNSIGNED NOT NULL DEFAULT '0' AFTER `health_percent`,
	ADD COLUMN `target_id` INT UNSIGNED NOT NULL DEFAULT '0' AFTER `target_guid`,
	ADD COLUMN `target_type` VARCHAR(16) NOT NULL DEFAULT '' COLLATE 'utf8_unicode_ci' AFTER `target_id`;

DROP TABLE `gameobject_text`;
DROP TABLE `gameobject_text_template`;

ALTER TABLE `creature_movement_client`
	ADD COLUMN `packet_id` INT(10) UNSIGNED NOT NULL AFTER `guid`;

ALTER TABLE `creature_movement_client`
	DROP PRIMARY KEY,
	ADD PRIMARY KEY (`packet_id`);

ALTER TABLE `player_movement_client`
	ADD COLUMN `packet_id` INT(10) UNSIGNED NOT NULL AFTER `guid`;

ALTER TABLE `player_movement_client`
	DROP PRIMARY KEY,
	ADD PRIMARY KEY (`packet_id`);

CREATE TABLE `creature_faction` (
	`entry` MEDIUMINT UNSIGNED NOT NULL,
	`faction` INT UNSIGNED NOT NULL,
	PRIMARY KEY (`entry`, `faction`)
)
COLLATE='utf8_unicode_ci'
ENGINE=InnoDB
;

ALTER TABLE `creature_faction`
	COMMENT='all unique faction template ids used by a given creature id';


CREATE TABLE `creature_damage_school` (
	`entry` MEDIUMINT UNSIGNED NOT NULL,
	`total_school_mask` INT UNSIGNED NOT NULL,
	PRIMARY KEY (`entry`, `total_school_mask`)
)
COLLATE='utf8_unicode_ci'
ENGINE=InnoDB
;

ALTER TABLE `creature_damage_school`
	COMMENT='all schools of damage dealt with melee attacks by a given creature id';

ALTER TABLE `player`
	CHANGE COLUMN `map` `map` SMALLINT UNSIGNED NULL DEFAULT '0' COMMENT 'Map Identifier' AFTER `guid`,
	ADD COLUMN `zone_id` SMALLINT UNSIGNED NOT NULL DEFAULT '0' COMMENT 'Zone Identifier' AFTER `map`,
	ADD COLUMN `area_id` SMALLINT UNSIGNED NOT NULL DEFAULT '0' COMMENT 'Area Identifier' AFTER `zone_id`;

ALTER TABLE `creature_stats`
	ADD COLUMN `is_pet` TINYINT UNSIGNED NOT NULL AFTER `level`,
	DROP PRIMARY KEY,
	ADD PRIMARY KEY (`entry`, `level`, `is_pet`);

CREATE TABLE `logout_time` (
	`unixtimems` BIGINT(20) UNSIGNED NOT NULL COMMENT 'when the packet was received',
	PRIMARY KEY (`unixtimems`)
)
COMMENT='from SMSG_LOGOUT_COMPLETE'
COLLATE='utf8_unicode_ci'
ENGINE=InnoDB
;

CREATE TABLE `creature_pet_name` (
	`guid` INT UNSIGNED NOT NULL,
	`name` VARCHAR(16) NOT NULL DEFAULT '',
	PRIMARY KEY (`guid`)
)
COMMENT='from SMSG_QUERY_PET_NAME_RESPONSE'
COLLATE='utf8_unicode_ci'
ENGINE=InnoDB
;

ALTER TABLE `creature_threat_update`
	COMMENT='from SMSG_THREAT_UPDATE and SMSG_HIGHEST_THREAT_UPDATE';

ALTER TABLE `creature_threat_update_target`
	COMMENT='individual targets and their threat from SMSG_THREAT_UPDATE and SMSG_HIGHEST_THREAT_UPDATE';

ALTER TABLE `client_release_spirit`
	COMMENT='from CMSG_REPOP_REQUEST';

ALTER TABLE `client_creature_interact`
	COMMENT='times when the client talked to a creature';

ALTER TABLE `creature`
	COMMENT='the initial state of all individual creature spawns seen in the sniff';

ALTER TABLE `creature_display_info_addon`
	COMMENT='data for display ids used by creatures';

ALTER TABLE `creature_equip_template`
	COMMENT='all weapons used by a given creature id, assigned in the virtual item slots';

ALTER TABLE `creature_gossip`
	COMMENT='all unique gossip menu ids used for given creature id';

ALTER TABLE `creature_loot`
	COMMENT='each row represents a separate loot instance\r\nmight not contain all the items or gold that dropped if somebody else looted them first';

ALTER TABLE `creature_pet_actions`
	COMMENT='from SMSG_PET_SPELLS_MESSAGE';

ALTER TABLE `creature_pet_cooldown`
	COMMENT='from SMSG_SPELL_COOLDOWN';

ALTER TABLE `creature_questitem`
	COMMENT='quest items that drop from a given creature id\r\nfrom SMSG_QUERY_CREATURE_RESPONSE';

ALTER TABLE `creature_stats`
	COMMENT='stats data from SMSG_UPDATE_OBJECT\r\nserver only sends it to the creature\'s charmer, or player who casts beast lore on it';

ALTER TABLE `creature_template`
	COMMENT='most commonly seen values per given creature id\r\ndata here is not guaranteed to be the true default',
	CHANGE COLUMN `gossip_menu_id` `gossip_menu_id` MEDIUMINT(8) UNSIGNED NOT NULL DEFAULT '0' COMMENT 'will only be set if the client talked to this npc' AFTER `entry`,
	CHANGE COLUMN `scale` `scale` FLOAT NOT NULL DEFAULT '1' AFTER `speed_run`,
	CHANGE COLUMN `auras` `auras` TEXT NULL COMMENT 'only includes auras with the NO_CASTER flag' AFTER `hover_height`;

ALTER TABLE `creature_template_wdb`
	COMMENT='static creature data from SMSG_QUERY_CREATURE_RESPONSE which gets saved to wdb cache';

ALTER TABLE `dynamicobject`
	COMMENT='the initial state of all individual dynamicobject spawns seen in the sniff\r\nthese are the objects used for ground targeted spell animations';

ALTER TABLE `faction_standing_update`
	COMMENT='from SMSG_SET_FACTION_STANDING';

ALTER TABLE `gameobject`
	COMMENT='the initial state of all individual gameobject spawns seen in the sniff';

ALTER TABLE `gameobject_loot`
	COMMENT='each row represents a separate loot instance\r\nmight not contain all the items or gold that dropped if somebody else looted them first';

ALTER TABLE `gameobject_questitem`
	COMMENT='quest items that drop from a given gameobject id\r\nfrom SMSG_QUERY_GAME_OBJECT_RESPONSE';

ALTER TABLE `gameobject_template`
	COMMENT='static gameobject data from SMSG_QUERY_GAME_OBJECT_RESPONSE which gets saved to wdb cache';

ALTER TABLE `player`
	COMMENT='the initial state of all players seen in the sniff';

ALTER TABLE `sniff_data`
	COMMENT='information about the contents of each sniff included in the database\r\ncan be used to figure out where specific data can be found';

ALTER TABLE `sniff_file`
	COMMENT='the names of all sniffs included in the database';
  
ALTER TABLE `spell_target_position`
	COMMENT='target coordinates for spells which need them defined in the database';
  
ALTER TABLE `weather_update`
	COMMENT='from SMSG_WEATHER';

ALTER TABLE `world_text`
	COMMENT='texts sent by the server which did not originate from a creature or player';

ALTER TABLE `gameobject`
	ADD COLUMN `is_spawn` TINYINT(1) UNSIGNED NOT NULL DEFAULT '0' COMMENT 'create object type 2' AFTER `rotation3`;

CREATE TABLE `creature_threat_clear` (
	`unixtimems` BIGINT UNSIGNED NOT NULL DEFAULT '0',
	`guid` INT UNSIGNED NOT NULL,
	PRIMARY KEY (`unixtimems`, `guid`)
)
COMMENT='from SMSG_THREAT_CLEAR'
COLLATE='utf8_unicode_ci'
ENGINE=InnoDB
;

CREATE TABLE `creature_threat_remove` (
	`unixtimems` BIGINT UNSIGNED NOT NULL DEFAULT '0',
	`guid` INT UNSIGNED NOT NULL,
	`target_guid` INT UNSIGNED NOT NULL DEFAULT '0',
	`target_id` INT UNSIGNED NOT NULL DEFAULT '0',
	`target_type` VARCHAR(16) NOT NULL DEFAULT '',
	PRIMARY KEY (`unixtimems`, `guid`)
)
COMMENT='from SMSG_THREAT_REMOVE'
COLLATE='utf8_unicode_ci'
ENGINE=InnoDB
;

ALTER TABLE `creature_guid_values_update`
	DROP PRIMARY KEY,
	ADD PRIMARY KEY (`guid`, `unixtimems`, `field_name`, `object_guid`, `object_id`, `object_type`);

ALTER TABLE `player_guid_values_update`
	DROP PRIMARY KEY,
	ADD PRIMARY KEY (`guid`, `unixtimems`, `field_name`, `object_guid`, `object_id`, `object_type`);

CREATE TABLE `cinematic_begin` (
	`unixtimems` BIGINT UNSIGNED NOT NULL COMMENT 'when the packet was received',
	`cinematic_id` INT UNSIGNED NOT NULL,
	PRIMARY KEY (`unixtimems`)
)
COMMENT='from SMSG_TRIGGER_CINEMATIC'
COLLATE='utf8_unicode_ci'
ENGINE=InnoDB
;

CREATE TABLE `cinematic_end` (
	`unixtimems` BIGINT UNSIGNED NOT NULL COMMENT 'when the packet was sent',
	PRIMARY KEY (`unixtimems`)
)
COMMENT='from CMSG_COMPLETE_CINEMATIC'
COLLATE='utf8_unicode_ci'
ENGINE=InnoDB
;

ALTER TABLE `creature_threat_remove`
	DROP PRIMARY KEY,
	ADD PRIMARY KEY (`unixtimems`, `guid`, `target_guid`, `target_id`, `target_type`);

CREATE TABLE `creature_pet_remaining_cooldown` (
	`entry` INT UNSIGNED NOT NULL DEFAULT '0',
	`spell_id` INT UNSIGNED NOT NULL DEFAULT '0',
	`cooldown` INT UNSIGNED NOT NULL DEFAULT '0',
	`category` SMALLINT UNSIGNED NOT NULL DEFAULT '0',
	`category_cooldown` INT UNSIGNED NOT NULL DEFAULT '0',
	`mod_rate` FLOAT UNSIGNED NOT NULL DEFAULT '1'
)
COMMENT='cooldowns that were already present when the creature became charmed\r\nfrom SMSG_PET_SPELLS_MESSAGE'
COLLATE='utf8_unicode_ci'
ENGINE=InnoDB
;

ALTER TABLE `sniff_data`
	CHANGE COLUMN `object_type` `object_type` ENUM('None','Spell','Map','LFGDungeon','Battleground','Unit','GameObject','CreatureDifficulty','Item','Quest','Opcode','PageText','NpcText','BroadcastText','Gossip','Zone','Area','AreaTrigger','Phase','Player','Achievement','CreatureFamily','Criteria','Currency','Difficulty','Faction','QuestGreeting','QuestObjective','Sound','Taxi') NOT NULL DEFAULT 'None' AFTER `sniff_id`;

ALTER TABLE `creature_pet_remaining_cooldown`
	ADD COLUMN `time_since_cast` INT UNSIGNED NOT NULL DEFAULT '0' COMMENT 'milliseconds since last SMSG_SPELL_GO for this spell' AFTER `mod_rate`;

CREATE TABLE `creature_spell_timers` (
	`entry` INT UNSIGNED NOT NULL DEFAULT '0',
	`spell_id` INT UNSIGNED NOT NULL DEFAULT '0',
	`initial_casts_count` SMALLINT UNSIGNED NOT NULL DEFAULT '0',
	`initial_delay_min` INT UNSIGNED NOT NULL DEFAULT '0',
	`initial_delay_average` INT UNSIGNED NOT NULL DEFAULT '0',
	`initial_delay_max` INT UNSIGNED NOT NULL DEFAULT '0',
	`repeat_casts_count` SMALLINT UNSIGNED NOT NULL DEFAULT '0',
	`repeat_delay_min` INT UNSIGNED NOT NULL DEFAULT '0',
	`repeat_delay_average` INT UNSIGNED NOT NULL DEFAULT '0',
	`repeat_delay_max` INT UNSIGNED NOT NULL DEFAULT '0',
	`sniff_build` SMALLINT UNSIGNED NOT NULL DEFAULT '0'
)
COMMENT='calculated time between casts for creatures'
COLLATE='utf8_unicode_ci'
ENGINE=InnoDB
;

ALTER TABLE `creature_damage_school`
	COMMENT='statistics for melee damage per creature id';
  
RENAME TABLE `creature_damage_school` TO `creature_melee_damage`;

ALTER TABLE `creature_melee_damage`
	CHANGE COLUMN `entry` `entry` INT UNSIGNED NOT NULL DEFAULT '0' FIRST,
	ADD COLUMN `hits_count` SMALLINT UNSIGNED NOT NULL DEFAULT '0' AFTER `entry`,
	ADD COLUMN `damage_min` INT UNSIGNED NOT NULL DEFAULT '0' AFTER `hits_count`,
	ADD COLUMN `damage_average` INT UNSIGNED NOT NULL DEFAULT '0' AFTER `damage_min`,
	ADD COLUMN `damage_max` INT UNSIGNED NOT NULL DEFAULT '0' AFTER `damage_average`,
	CHANGE COLUMN `total_school_mask` `total_school_mask` INT(10) UNSIGNED NOT NULL DEFAULT '0' AFTER `damage_max`,
	ADD COLUMN `sniff_build` SMALLINT UNSIGNED NOT NULL DEFAULT '0' AFTER `total_school_mask`,
	DROP PRIMARY KEY;

ALTER TABLE `creature_melee_damage`
	ADD COLUMN `is_dirty` TINYINT UNSIGNED NOT NULL DEFAULT '0' COMMENT 'mob had auras that affect damage or there were no normal hits' AFTER `entry`;

ALTER TABLE `creature_stats`
	ADD COLUMN `is_dirty` TINYINT(3) UNSIGNED NOT NULL COMMENT 'mob had auras that affect stats' AFTER `is_pet`,
	DROP PRIMARY KEY,
	ADD PRIMARY KEY (`entry`, `level`, `is_pet`, `is_dirty`);

ALTER TABLE `creature_melee_damage`
	CHANGE COLUMN `sniff_build` `sniff_build` MEDIUMINT UNSIGNED NOT NULL DEFAULT '0' AFTER `total_school_mask`;

ALTER TABLE `creature_spell_timers`
	CHANGE COLUMN `sniff_build` `sniff_build` MEDIUMINT UNSIGNED NOT NULL DEFAULT '0' AFTER `repeat_delay_max`;
  
ALTER TABLE `creature_template_locale`
	CHANGE COLUMN `VerifiedBuild` `VerifiedBuild` MEDIUMINT UNSIGNED NOT NULL DEFAULT '0' AFTER `TitleAlt`;

ALTER TABLE `gameobject`
	CHANGE COLUMN `sniff_build` `sniff_build` MEDIUMINT UNSIGNED NOT NULL DEFAULT '0' AFTER `sniff_id`;

ALTER TABLE `gameobject`
	CHANGE COLUMN `sniff_id` `sniff_id` SMALLINT(5) UNSIGNED NOT NULL DEFAULT '0' COMMENT 'points to sniff_file table' AFTER `custom_param`;

ALTER TABLE `gameobject_questitem`
	CHANGE COLUMN `sniff_build` `sniff_build` MEDIUMINT UNSIGNED NOT NULL DEFAULT '0' AFTER `item_id`;

ALTER TABLE `quest_details`
	CHANGE COLUMN `VerifiedBuild` `VerifiedBuild` MEDIUMINT UNSIGNED NOT NULL DEFAULT '0' AFTER `EmoteDelay4`;

ALTER TABLE `quest_greeting`
	CHANGE COLUMN `VerifiedBuild` `VerifiedBuild` MEDIUMINT UNSIGNED NOT NULL DEFAULT '0' AFTER `Greeting`;

ALTER TABLE `quest_greeting_locale`
	CHANGE COLUMN `VerifiedBuild` `VerifiedBuild` MEDIUMINT UNSIGNED NOT NULL DEFAULT '0' AFTER `Greeting`;

ALTER TABLE `quest_objectives`
	CHANGE COLUMN `VerifiedBuild` `VerifiedBuild` MEDIUMINT UNSIGNED NOT NULL DEFAULT '0' AFTER `Description`;

ALTER TABLE `quest_objectives_locale`
	CHANGE COLUMN `VerifiedBuild` `VerifiedBuild` MEDIUMINT UNSIGNED NOT NULL DEFAULT '0' AFTER `Description`;

ALTER TABLE `quest_offer_reward`
	CHANGE COLUMN `VerifiedBuild` `VerifiedBuild` MEDIUMINT UNSIGNED NOT NULL DEFAULT '0' AFTER `RewardText`;

ALTER TABLE `quest_offer_reward_locale`
	CHANGE COLUMN `VerifiedBuild` `VerifiedBuild` MEDIUMINT UNSIGNED NOT NULL DEFAULT '0' AFTER `RewardText`;

ALTER TABLE `quest_request_items`
	CHANGE COLUMN `VerifiedBuild` `VerifiedBuild` MEDIUMINT UNSIGNED NOT NULL DEFAULT '0' AFTER `CompletionText`;

ALTER TABLE `quest_request_items_locale`
	CHANGE COLUMN `VerifiedBuild` `VerifiedBuild` MEDIUMINT UNSIGNED NOT NULL DEFAULT '0' AFTER `CompletionText`;

ALTER TABLE `quest_template`
	CHANGE COLUMN `VerifiedBuild` `VerifiedBuild` MEDIUMINT UNSIGNED NULL DEFAULT '0' AFTER `CompleteSoundKitID`;

ALTER TABLE `quest_template_locale`
	CHANGE COLUMN `VerifiedBuild` `VerifiedBuild` MEDIUMINT UNSIGNED NOT NULL DEFAULT '0' AFTER `QuestCompletionLog`;

ALTER TABLE `trainer`
	CHANGE COLUMN `sniff_build` `sniff_build` MEDIUMINT UNSIGNED NOT NULL DEFAULT '0' AFTER `greeting`;

ALTER TABLE `trainer_spell`
	CHANGE COLUMN `sniff_build` `sniff_build` MEDIUMINT UNSIGNED NOT NULL DEFAULT '0' AFTER `required_level`;

ALTER TABLE `creature_pet_actions`
	ADD COLUMN `sniff_build` MEDIUMINT UNSIGNED NOT NULL DEFAULT '0' AFTER `slot10`;

ALTER TABLE `creature_pet_cooldown`
	ADD COLUMN `sniff_build` MEDIUMINT UNSIGNED NOT NULL DEFAULT '0' AFTER `mod_rate`;

ALTER TABLE `creature_pet_remaining_cooldown`
	ADD COLUMN `sniff_build` MEDIUMINT UNSIGNED NOT NULL DEFAULT '0' AFTER `time_since_cast`;

ALTER TABLE `creature_stats`
	ADD COLUMN `sniff_build` MEDIUMINT UNSIGNED NOT NULL DEFAULT '0' AFTER `negative_arcane_res`;

ALTER TABLE `gameobject`
	CHANGE COLUMN `map` `map` SMALLINT(5) UNSIGNED NULL DEFAULT '0' COMMENT 'Map Identifier' AFTER `id`;

ALTER TABLE `dynamicobject`
	CHANGE COLUMN `map` `map` SMALLINT(5) UNSIGNED NULL DEFAULT '0' COMMENT 'Map Identifier' AFTER `guid`;

ALTER TABLE `creature`
	ADD COLUMN `waypoint_count` SMALLINT UNSIGNED NOT NULL DEFAULT '0' COMMENT 'number of out of combat movement packets seen' AFTER `wander_distance`;

ALTER TABLE `creature`
	ADD COLUMN `power_type` TINYINT UNSIGNED NOT NULL DEFAULT '0' AFTER `max_health`;

ALTER TABLE `creature`
	CHANGE COLUMN `current_mana` `current_power` INT(10) UNSIGNED NOT NULL DEFAULT '0' AFTER `power_type`,
	CHANGE COLUMN `max_mana` `max_power` INT(10) UNSIGNED NOT NULL DEFAULT '0' AFTER `current_power`;

ALTER TABLE `player`
	ADD COLUMN `power_type` TINYINT UNSIGNED NOT NULL DEFAULT '0' AFTER `max_health`;

ALTER TABLE `player`
	CHANGE COLUMN `current_mana` `current_power` INT(10) UNSIGNED NOT NULL DEFAULT '0' AFTER `power_type`,
	CHANGE COLUMN `max_mana` `max_power` INT(10) UNSIGNED NOT NULL DEFAULT '0' AFTER `current_power`;

CREATE TABLE `creature_power_values` (
	`guid` INT UNSIGNED NOT NULL,
	`power_type` TINYINT UNSIGNED NOT NULL,
	`current_power` INT UNSIGNED NOT NULL DEFAULT '0',
	`max_power` INT UNSIGNED NOT NULL DEFAULT '0',
	PRIMARY KEY (`guid`, `power_type`)
)
COMMENT='initial value of power update fields'
COLLATE='utf8_unicode_ci'
ENGINE=InnoDB
;

CREATE TABLE `player_power_values` (
	`guid` INT UNSIGNED NOT NULL,
	`power_type` TINYINT UNSIGNED NOT NULL,
	`current_power` INT UNSIGNED NOT NULL DEFAULT '0',
	`max_power` INT UNSIGNED NOT NULL DEFAULT '0',
	PRIMARY KEY (`guid`, `power_type`)
)
COMMENT='initial value of power update fields'
COLLATE='utf8_unicode_ci'
ENGINE=InnoDB
;

CREATE TABLE `creature_power_values_update` (
	`unixtimems` BIGINT UNSIGNED NOT NULL,
	`guid` INT UNSIGNED NOT NULL,
	`power_type` TINYINT UNSIGNED NOT NULL,
	`current_power` INT UNSIGNED NULL,
	`max_power` INT UNSIGNED NULL
)
COMMENT='changes to power update fields'
COLLATE='utf8_unicode_ci'
ENGINE=InnoDB
;

CREATE TABLE `player_power_values_update` (
	`unixtimems` BIGINT UNSIGNED NOT NULL,
	`guid` INT UNSIGNED NOT NULL,
	`power_type` TINYINT UNSIGNED NOT NULL,
	`current_power` INT UNSIGNED NULL,
	`max_power` INT UNSIGNED NULL
)
COMMENT='changes to power update fields'
COLLATE='utf8_unicode_ci'
ENGINE=InnoDB
;

ALTER TABLE `creature_values_update`
	CHANGE COLUMN `current_mana` `power_type` INT(10) UNSIGNED NULL DEFAULT NULL AFTER `max_health`,
	DROP COLUMN `max_mana`;

ALTER TABLE `player_values_update`
	CHANGE COLUMN `current_mana` `power_type` INT(10) UNSIGNED NULL DEFAULT NULL AFTER `max_health`,
	DROP COLUMN `max_mana`;

ALTER TABLE `creature`
	ADD COLUMN `is_vehicle` TINYINT(3) UNSIGNED NOT NULL DEFAULT '0' AFTER `is_pet`;

ALTER TABLE `creature_create1_time`
	ADD COLUMN `transport_id` INT(10) UNSIGNED NOT NULL DEFAULT '0' AFTER `transport_guid`,
	ADD COLUMN `transport_type` VARCHAR(16) NOT NULL DEFAULT '' AFTER `transport_id`;

ALTER TABLE `creature_create2_time`
	ADD COLUMN `transport_id` INT(10) UNSIGNED NOT NULL DEFAULT '0' AFTER `transport_guid`,
	ADD COLUMN `transport_type` VARCHAR(16) NOT NULL DEFAULT '' AFTER `transport_id`;

ALTER TABLE `player_create1_time`
	ADD COLUMN `transport_id` INT(10) UNSIGNED NOT NULL DEFAULT '0' AFTER `transport_guid`,
	ADD COLUMN `transport_type` VARCHAR(16) NOT NULL DEFAULT '' AFTER `transport_id`;

ALTER TABLE `player_create2_time`
	ADD COLUMN `transport_id` INT(10) UNSIGNED NOT NULL DEFAULT '0' AFTER `transport_guid`,
	ADD COLUMN `transport_type` VARCHAR(16) NOT NULL DEFAULT '' AFTER `transport_id`;

ALTER TABLE `gameobject_create1_time`
	ADD COLUMN `transport_id` INT(10) UNSIGNED NOT NULL DEFAULT '0' AFTER `transport_guid`,
	ADD COLUMN `transport_type` VARCHAR(16) NOT NULL DEFAULT '' AFTER `transport_id`;

ALTER TABLE `gameobject_create2_time`
	ADD COLUMN `transport_id` INT(10) UNSIGNED NOT NULL DEFAULT '0' AFTER `transport_guid`,
	ADD COLUMN `transport_type` VARCHAR(16) NOT NULL DEFAULT '' AFTER `transport_id`;

ALTER TABLE `dynamicobject_create1_time`
	ADD COLUMN `transport_id` INT(10) UNSIGNED NOT NULL DEFAULT '0' AFTER `transport_guid`,
	ADD COLUMN `transport_type` VARCHAR(16) NOT NULL DEFAULT '' AFTER `transport_id`;

ALTER TABLE `dynamicobject_create2_time`
	ADD COLUMN `transport_id` INT(10) UNSIGNED NOT NULL DEFAULT '0' AFTER `transport_guid`,
	ADD COLUMN `transport_type` VARCHAR(16) NOT NULL DEFAULT '' AFTER `transport_id`;

ALTER TABLE `creature_movement_client`
	ADD COLUMN `transport_id` INT(10) UNSIGNED NOT NULL DEFAULT '0' AFTER `transport_guid`,
	ADD COLUMN `transport_type` VARCHAR(16) NOT NULL DEFAULT '' AFTER `transport_id`;

ALTER TABLE `player_movement_client`
	ADD COLUMN `transport_id` INT(10) UNSIGNED NOT NULL DEFAULT '0' AFTER `transport_guid`,
	ADD COLUMN `transport_type` VARCHAR(16) NOT NULL DEFAULT '' AFTER `transport_id`;

ALTER TABLE `creature_movement_server`
	ADD COLUMN `transport_id` INT(10) UNSIGNED NOT NULL DEFAULT '0' AFTER `transport_guid`,
	ADD COLUMN `transport_type` VARCHAR(16) NOT NULL DEFAULT '' AFTER `transport_id`;

ALTER TABLE `creature_movement_server_combat`
	ADD COLUMN `transport_id` INT(10) UNSIGNED NOT NULL DEFAULT '0' AFTER `transport_guid`,
	ADD COLUMN `transport_type` VARCHAR(16) NOT NULL DEFAULT '' AFTER `transport_id`;

ALTER TABLE `player_movement_server`
	ADD COLUMN `transport_id` INT(10) UNSIGNED NOT NULL DEFAULT '0' AFTER `transport_guid`,
	ADD COLUMN `transport_type` VARCHAR(16) NOT NULL DEFAULT '' AFTER `transport_id`;

ALTER TABLE `player_create1_time`
	CHANGE COLUMN `transport_guid` `transport_guid` INT(10) UNSIGNED NOT NULL DEFAULT '0' AFTER `spline_elevation`,
	CHANGE COLUMN `transport_id` `transport_id` INT(10) UNSIGNED NOT NULL DEFAULT '0' AFTER `transport_guid`,
	CHANGE COLUMN `transport_type` `transport_type` VARCHAR(16) NOT NULL DEFAULT '' COLLATE 'utf8_unicode_ci' AFTER `transport_id`,
	CHANGE COLUMN `transport_x` `transport_x` FLOAT NOT NULL DEFAULT '0' AFTER `transport_type`,
	CHANGE COLUMN `transport_y` `transport_y` FLOAT NOT NULL DEFAULT '0' AFTER `transport_x`,
	CHANGE COLUMN `transport_z` `transport_z` FLOAT NOT NULL DEFAULT '0' AFTER `transport_y`,
	CHANGE COLUMN `transport_o` `transport_o` FLOAT NOT NULL DEFAULT '0' AFTER `transport_z`;

ALTER TABLE `player_create1_time`
	ADD COLUMN `vehicle_id` INT UNSIGNED NOT NULL DEFAULT '0' AFTER `spline_elevation`,
	ADD COLUMN `vehicle_orientation` FLOAT NOT NULL DEFAULT '0' AFTER `vehicle_id`;

ALTER TABLE `player_create2_time`
	CHANGE COLUMN `transport_guid` `transport_guid` INT(10) UNSIGNED NOT NULL DEFAULT '0' AFTER `spline_elevation`,
	CHANGE COLUMN `transport_id` `transport_id` INT(10) UNSIGNED NOT NULL DEFAULT '0' AFTER `transport_guid`,
	CHANGE COLUMN `transport_type` `transport_type` VARCHAR(16) NOT NULL DEFAULT '' COLLATE 'utf8_unicode_ci' AFTER `transport_id`,
	CHANGE COLUMN `transport_x` `transport_x` FLOAT NOT NULL DEFAULT '0' AFTER `transport_type`,
	CHANGE COLUMN `transport_y` `transport_y` FLOAT NOT NULL DEFAULT '0' AFTER `transport_x`,
	CHANGE COLUMN `transport_z` `transport_z` FLOAT NOT NULL DEFAULT '0' AFTER `transport_y`,
	CHANGE COLUMN `transport_o` `transport_o` FLOAT NOT NULL DEFAULT '0' AFTER `transport_z`;

ALTER TABLE `player_create2_time`
	ADD COLUMN `vehicle_id` INT UNSIGNED NOT NULL DEFAULT '0' AFTER `spline_elevation`,
	ADD COLUMN `vehicle_orientation` FLOAT NOT NULL DEFAULT '0' AFTER `vehicle_id`;

ALTER TABLE `creature_create1_time`
	CHANGE COLUMN `transport_guid` `transport_guid` INT(10) UNSIGNED NOT NULL DEFAULT '0' AFTER `spline_elevation`,
	CHANGE COLUMN `transport_id` `transport_id` INT(10) UNSIGNED NOT NULL DEFAULT '0' AFTER `transport_guid`,
	CHANGE COLUMN `transport_type` `transport_type` VARCHAR(16) NOT NULL DEFAULT '' COLLATE 'utf8_unicode_ci' AFTER `transport_id`,
	CHANGE COLUMN `transport_x` `transport_x` FLOAT NOT NULL DEFAULT '0' AFTER `transport_type`,
	CHANGE COLUMN `transport_y` `transport_y` FLOAT NOT NULL DEFAULT '0' AFTER `transport_x`,
	CHANGE COLUMN `transport_z` `transport_z` FLOAT NOT NULL DEFAULT '0' AFTER `transport_y`,
	CHANGE COLUMN `transport_o` `transport_o` FLOAT NOT NULL DEFAULT '0' AFTER `transport_z`;

ALTER TABLE `creature_create1_time`
	ADD COLUMN `vehicle_id` INT UNSIGNED NOT NULL DEFAULT '0' AFTER `spline_elevation`,
	ADD COLUMN `vehicle_orientation` FLOAT NOT NULL DEFAULT '0' AFTER `vehicle_id`;

ALTER TABLE `creature_create2_time`
	CHANGE COLUMN `transport_guid` `transport_guid` INT(10) UNSIGNED NOT NULL DEFAULT '0' AFTER `spline_elevation`,
	CHANGE COLUMN `transport_id` `transport_id` INT(10) UNSIGNED NOT NULL DEFAULT '0' AFTER `transport_guid`,
	CHANGE COLUMN `transport_type` `transport_type` VARCHAR(16) NOT NULL DEFAULT '' COLLATE 'utf8_unicode_ci' AFTER `transport_id`,
	CHANGE COLUMN `transport_x` `transport_x` FLOAT NOT NULL DEFAULT '0' AFTER `transport_type`,
	CHANGE COLUMN `transport_y` `transport_y` FLOAT NOT NULL DEFAULT '0' AFTER `transport_x`,
	CHANGE COLUMN `transport_z` `transport_z` FLOAT NOT NULL DEFAULT '0' AFTER `transport_y`,
	CHANGE COLUMN `transport_o` `transport_o` FLOAT NOT NULL DEFAULT '0' AFTER `transport_z`;

ALTER TABLE `creature_create2_time`
	ADD COLUMN `vehicle_id` INT UNSIGNED NOT NULL DEFAULT '0' AFTER `spline_elevation`,
	ADD COLUMN `vehicle_orientation` FLOAT NOT NULL DEFAULT '0' AFTER `vehicle_id`;

ALTER TABLE `gameobject`
	ADD COLUMN `is_transport` TINYINT(1) UNSIGNED NOT NULL DEFAULT '0' AFTER `is_spawn`;

ALTER TABLE `creature`
	CHANGE COLUMN `is_pet` `is_pet` TINYINT(3) UNSIGNED NOT NULL DEFAULT '0' AFTER `is_hovering`,
	CHANGE COLUMN `is_vehicle` `is_vehicle` TINYINT(3) UNSIGNED NOT NULL DEFAULT '0' AFTER `is_pet`;

ALTER TABLE `creature`
	ADD COLUMN `original_id` MEDIUMINT(8) UNSIGNED NOT NULL DEFAULT '0' COMMENT 'entry from guid' AFTER `guid`,
	CHANGE COLUMN `id` `id` MEDIUMINT(8) UNSIGNED NOT NULL DEFAULT '0' COMMENT 'entry from update fields' AFTER `original_id`,
	DROP INDEX `idx_map`,
	DROP INDEX `idx_id`;

ALTER TABLE `gameobject`
	ADD COLUMN `original_id` MEDIUMINT(8) UNSIGNED NOT NULL DEFAULT '0' COMMENT 'entry from guid' AFTER `guid`,
	CHANGE COLUMN `id` `id` MEDIUMINT(8) UNSIGNED NOT NULL DEFAULT '0' COMMENT 'entry from update fields' AFTER `original_id`;

ALTER TABLE `creature_create1_time`
	ADD COLUMN `transport_time` INT UNSIGNED NOT NULL DEFAULT '0' AFTER `transport_o`,
	ADD COLUMN `transport_seat` TINYINT NOT NULL DEFAULT '0' AFTER `transport_time`;

ALTER TABLE `creature_create2_time`
	ADD COLUMN `transport_time` INT UNSIGNED NOT NULL DEFAULT '0' AFTER `transport_o`,
	ADD COLUMN `transport_seat` TINYINT NOT NULL DEFAULT '0' AFTER `transport_time`;

ALTER TABLE `player_create1_time`
	ADD COLUMN `transport_time` INT UNSIGNED NOT NULL DEFAULT '0' AFTER `transport_o`,
	ADD COLUMN `transport_seat` TINYINT NOT NULL DEFAULT '0' AFTER `transport_time`;

ALTER TABLE `player_create2_time`
	ADD COLUMN `transport_time` INT UNSIGNED NOT NULL DEFAULT '0' AFTER `transport_o`,
	ADD COLUMN `transport_seat` TINYINT NOT NULL DEFAULT '0' AFTER `transport_time`;

ALTER TABLE `creature_movement_server`
	ADD COLUMN `transport_seat` TINYINT NOT NULL DEFAULT '0' AFTER `transport_type`;

ALTER TABLE `creature_movement_server_combat`
	ADD COLUMN `transport_seat` TINYINT NOT NULL DEFAULT '0' AFTER `transport_type`;

ALTER TABLE `player_movement_server`
	ADD COLUMN `transport_seat` TINYINT NOT NULL DEFAULT '0' AFTER `transport_type`;

ALTER TABLE `creature_movement_client`
	CHANGE COLUMN `transport_guid` `transport_guid` INT(10) UNSIGNED NOT NULL DEFAULT '0' AFTER `spline_elevation`,
	CHANGE COLUMN `transport_id` `transport_id` INT(10) UNSIGNED NOT NULL DEFAULT '0' AFTER `transport_guid`,
	CHANGE COLUMN `transport_type` `transport_type` VARCHAR(16) NOT NULL DEFAULT '' COLLATE 'utf8_unicode_ci' AFTER `transport_id`,
	CHANGE COLUMN `transport_x` `transport_x` FLOAT NOT NULL DEFAULT '0' AFTER `transport_type`,
	CHANGE COLUMN `transport_y` `transport_y` FLOAT NOT NULL DEFAULT '0' AFTER `transport_x`,
	CHANGE COLUMN `transport_z` `transport_z` FLOAT NOT NULL DEFAULT '0' AFTER `transport_y`,
	CHANGE COLUMN `transport_o` `transport_o` FLOAT NOT NULL DEFAULT '0' AFTER `transport_z`;

ALTER TABLE `creature_movement_client`
	ADD COLUMN `transport_time` INT UNSIGNED NOT NULL DEFAULT '0' AFTER `transport_o`,
	ADD COLUMN `transport_seat` TINYINT NOT NULL DEFAULT '0' AFTER `transport_time`;

ALTER TABLE `player_movement_client`
	CHANGE COLUMN `transport_guid` `transport_guid` INT(10) UNSIGNED NOT NULL DEFAULT '0' AFTER `spline_elevation`,
	CHANGE COLUMN `transport_id` `transport_id` INT(10) UNSIGNED NOT NULL DEFAULT '0' AFTER `transport_guid`,
	CHANGE COLUMN `transport_type` `transport_type` VARCHAR(16) NOT NULL DEFAULT '' COLLATE 'utf8_unicode_ci' AFTER `transport_id`,
	CHANGE COLUMN `transport_x` `transport_x` FLOAT NOT NULL DEFAULT '0' AFTER `transport_type`,
	CHANGE COLUMN `transport_y` `transport_y` FLOAT NOT NULL DEFAULT '0' AFTER `transport_x`,
	CHANGE COLUMN `transport_z` `transport_z` FLOAT NOT NULL DEFAULT '0' AFTER `transport_y`,
	CHANGE COLUMN `transport_o` `transport_o` FLOAT NOT NULL DEFAULT '0' AFTER `transport_z`;

ALTER TABLE `player_movement_client`
	ADD COLUMN `transport_time` INT UNSIGNED NOT NULL DEFAULT '0' AFTER `transport_o`,
	ADD COLUMN `transport_seat` TINYINT NOT NULL DEFAULT '0' AFTER `transport_time`;

ALTER TABLE `gameobject`
	ADD COLUMN `anim_progress` TINYINT(3) UNSIGNED NOT NULL DEFAULT '0' AFTER `artkit`;

ALTER TABLE `gameobject_values_update`
	ADD COLUMN `anim_progress` INT(10) UNSIGNED NULL DEFAULT NULL AFTER `artkit`;

ALTER TABLE `gameobject`
	CHANGE COLUMN `artkit` `art_kit` TINYINT(3) UNSIGNED NOT NULL DEFAULT '0' AFTER `type`;

ALTER TABLE `gameobject_values_update`
	CHANGE COLUMN `artkit` `art_kit` INT(10) UNSIGNED NULL DEFAULT NULL AFTER `state`;

ALTER TABLE `creature_melee_damage`
	ADD COLUMN `level` INT(10) UNSIGNED NOT NULL DEFAULT '0' AFTER `entry`;

ALTER TABLE `creature`
	ADD COLUMN `anim_tier` TINYINT(3) UNSIGNED NOT NULL DEFAULT '0' COMMENT 'from UNIT_FIELD_BYTES_1' AFTER `vis_flags`;

ALTER TABLE `creature_values_update`
	ADD COLUMN `anim_tier` INT(10) UNSIGNED NULL DEFAULT NULL AFTER `vis_flags`;

ALTER TABLE `player`
	ADD COLUMN `anim_tier` TINYINT(3) UNSIGNED NOT NULL DEFAULT '0' COMMENT 'from UNIT_FIELD_BYTES_1' AFTER `vis_flags`;

ALTER TABLE `player_values_update`
	ADD COLUMN `anim_tier` INT(10) UNSIGNED NULL DEFAULT NULL AFTER `vis_flags`;
  
ALTER TABLE `creature_stats`
	ADD COLUMN `sniff_id` SMALLINT UNSIGNED NOT NULL DEFAULT '0' COMMENT 'points to sniff_file table' AFTER `negative_arcane_res`;

ALTER TABLE `creature_pet_actions`
	ADD COLUMN `sniff_id` SMALLINT UNSIGNED NOT NULL DEFAULT '0' COMMENT 'points to sniff_file table' AFTER `slot10`;

ALTER TABLE `creature_pet_cooldown`
	ADD COLUMN `sniff_id` SMALLINT UNSIGNED NOT NULL DEFAULT '0' COMMENT 'points to sniff_file table' AFTER `mod_rate`;

ALTER TABLE `creature_pet_remaining_cooldown`
	ADD COLUMN `sniff_id` SMALLINT UNSIGNED NOT NULL DEFAULT '0' COMMENT 'points to sniff_file table' AFTER `time_since_cast`;

ALTER TABLE `creature_loot`
	ADD COLUMN `sniff_id` SMALLINT UNSIGNED NOT NULL DEFAULT '0' COMMENT 'points to sniff_file table' AFTER `items_count`,
	ADD COLUMN `sniff_build` MEDIUMINT UNSIGNED NOT NULL DEFAULT '0' AFTER `sniff_id`;

ALTER TABLE `gameobject_loot`
	ADD COLUMN `sniff_id` SMALLINT UNSIGNED NOT NULL DEFAULT '0' COMMENT 'points to sniff_file table' AFTER `items_count`,
	ADD COLUMN `sniff_build` MEDIUMINT UNSIGNED NOT NULL DEFAULT '0' AFTER `sniff_id`;

CREATE TABLE `creature_armor` (
	`entry` INT UNSIGNED NOT NULL DEFAULT '0',
	`level` INT UNSIGNED NOT NULL DEFAULT '0',
	`hits_count` SMALLINT UNSIGNED NOT NULL DEFAULT '0',
	`armor` INT UNSIGNED NOT NULL DEFAULT '0',
	`damage_reduction` FLOAT NOT NULL DEFAULT '0',
	`sniff_build` MEDIUMINT UNSIGNED NOT NULL DEFAULT '0'
)
COLLATE='utf8_unicode_ci'
ENGINE=InnoDB
;

ALTER TABLE `creature_armor`
	COMMENT='estimated armor of creatures from damage taken';

ALTER TABLE `spell_target_position`
	DROP COLUMN `effect_index`;

CREATE TABLE `spell_script_target` (
  `spell_id` int(10) unsigned NOT NULL,
  `target_type` varchar(16) COLLATE utf8_unicode_ci NOT NULL,
  `target_id` int(10) unsigned NOT NULL,
  `sniff_build` mediumint(8) unsigned NOT NULL DEFAULT '0',
  PRIMARY KEY (`spell_id`,`target_type`,`target_id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_unicode_ci;


ALTER TABLE `player`
	ADD COLUMN `skin` TINYINT UNSIGNED NOT NULL DEFAULT '0' AFTER `money`,
	ADD COLUMN `face` TINYINT UNSIGNED NOT NULL DEFAULT '0' AFTER `skin`,
	ADD COLUMN `hair_style` TINYINT UNSIGNED NOT NULL DEFAULT '0' AFTER `face`,
	ADD COLUMN `hair_color` TINYINT UNSIGNED NOT NULL DEFAULT '0' AFTER `hair_style`,
	ADD COLUMN `facial_hair` TINYINT UNSIGNED NOT NULL DEFAULT '0' AFTER `hair_color`,
	DROP COLUMN `player_bytes1`,
	DROP COLUMN `player_bytes2`;

CREATE TABLE `creature_spell_immunity` (
	`entry` INT UNSIGNED NOT NULL,
	`spell_id` INT UNSIGNED NOT NULL,
	`sniff_id` SMALLINT UNSIGNED NOT NULL DEFAULT '0',
	`sniff_build` MEDIUMINT UNSIGNED NOT NULL DEFAULT '0',
	PRIMARY KEY (`entry`, `spell_id`, `sniff_id`, `sniff_build`)
)
COMMENT='spells that creatures were immune to\r\nfrom SMSG_SPELL_GO'
COLLATE='utf8_unicode_ci'
ENGINE=InnoDB
;

ALTER TABLE `spell_cast_go_target`
	ADD COLUMN `miss_reason` TINYINT UNSIGNED NOT NULL DEFAULT '0' AFTER `target_type`;

CREATE TABLE `creature_respawn_time` (
	`old_guid` INT UNSIGNED NOT NULL,
	`new_guid` INT UNSIGNED NOT NULL,
	`respawn_time` INT UNSIGNED NOT NULL COMMENT 'time in seconds',
	PRIMARY KEY (`old_guid`, `new_guid`)
)
COMMENT='stores the time in seconds between the death of one creature, and the spawn of another on the same position\r\nrespawn time is reduced dynamically if there are too many players in the same area, so beware of abnormally low values'
COLLATE='utf8_unicode_ci'
ENGINE=InnoDB
;

CREATE TABLE `creature_kill_reputation` (
	`entry` INT UNSIGNED NOT NULL,
	`reputation_list_id` INT UNSIGNED NOT NULL,
	`old_standing` INT NOT NULL,
	`new_standing` INT NOT NULL,
	`player_level` TINYINT UNSIGNED NOT NULL,
	`player_race` TINYINT UNSIGNED NOT NULL,
	`sniff_id` SMALLINT UNSIGNED NOT NULL DEFAULT '0' COMMENT 'points to sniff_file table',
	`sniff_build` MEDIUMINT UNSIGNED NOT NULL DEFAULT '0'
)
COMMENT='faction standing changes after a creature died\r\ncan be wrong if multiple creatures died at the same time'
COLLATE='utf8_unicode_ci'
ENGINE=InnoDB
;

ALTER TABLE `spell_unique_caster`
	ADD COLUMN `sniff_build` MEDIUMINT UNSIGNED NOT NULL DEFAULT '0' AFTER `spell_id`;

CREATE TABLE `spell_aura_flags` (
	`spell_id` INT UNSIGNED NOT NULL,
	`flags` INT UNSIGNED NOT NULL,
	`sniff_build` MEDIUMINT UNSIGNED NOT NULL DEFAULT '0',
	PRIMARY KEY (`spell_id`, `flags`)
)
COMMENT='all aura flags seen for a given spell id'
COLLATE='utf8_unicode_ci'
ENGINE=InnoDB
;

ALTER TABLE `spell_target_position`
	CHANGE COLUMN `entry` `spell_id` MEDIUMINT(8) UNSIGNED NOT NULL DEFAULT '0' FIRST,
	DROP PRIMARY KEY,
	ADD PRIMARY KEY (`spell_id`, `map`);

CREATE TABLE `mail_template` (
  `entry` int(10) unsigned NOT NULL DEFAULT '0',
  `sniff_build` mediumint(8) unsigned NOT NULL DEFAULT '0',
  `stationery_id` int(10) unsigned NOT NULL DEFAULT '0',
  `sender_id` int(10) unsigned NOT NULL DEFAULT '0',
  `sender_type` tinyint(3) unsigned NOT NULL DEFAULT '0',
  `money` bigint(20) unsigned NOT NULL DEFAULT '0',
  `items_count` tinyint(3) unsigned NOT NULL DEFAULT '0',
  `subject` varchar(64) COLLATE utf8_unicode_ci NOT NULL DEFAULT '',
  `body` varchar(1024) COLLATE utf8_unicode_ci NOT NULL DEFAULT '',
  PRIMARY KEY (`entry`,`sniff_build`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_unicode_ci COMMENT='data for mail sent by creatures\r\nfrom SMSG_MAIL_LIST_RESULT';

CREATE TABLE `mail_template_item` (
  `entry` int(10) unsigned NOT NULL DEFAULT '0',
  `slot` tinyint(3) unsigned NOT NULL DEFAULT '0',
  `item_id` int(10) unsigned NOT NULL DEFAULT '0',
  `count` tinyint(3) unsigned NOT NULL DEFAULT '0',
  `sniff_build` mediumint(8) unsigned NOT NULL DEFAULT '0',
  PRIMARY KEY (`entry`,`slot`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_unicode_ci COMMENT='items attached to mail templates\r\nfrom SMSG_MAIL_LIST_RESULT';

ALTER TABLE `mail_template`
	CHANGE COLUMN `subject` `subject` TEXT NOT NULL DEFAULT '' COLLATE 'utf8_unicode_ci' AFTER `items_count`,
	CHANGE COLUMN `body` `body` TEXT NOT NULL DEFAULT '' COLLATE 'utf8_unicode_ci' AFTER `subject`;

ALTER TABLE `creature_template_wdb`
	CHANGE COLUMN `sniff_build` `sniff_build` MEDIUMINT(8) UNSIGNED NOT NULL DEFAULT '0' AFTER `entry`,
	DROP PRIMARY KEY,
	ADD PRIMARY KEY (`entry`, `sniff_build`);

ALTER TABLE `gameobject_template`
	CHANGE COLUMN `sniff_build` `sniff_build` MEDIUMINT(8) UNSIGNED NOT NULL DEFAULT '0' AFTER `entry`,
	DROP PRIMARY KEY,
	ADD PRIMARY KEY (`entry`, `sniff_build`);

ALTER TABLE `npc_text`
	CHANGE COLUMN `sniff_build` `sniff_build` MEDIUMINT(8) UNSIGNED NOT NULL DEFAULT '0' AFTER `entry`,
	DROP PRIMARY KEY,
	ADD PRIMARY KEY (`entry`, `sniff_build`);

ALTER TABLE `page_text`
	CHANGE COLUMN `sniff_build` `sniff_build` MEDIUMINT(8) UNSIGNED NOT NULL DEFAULT '0' AFTER `entry`,
	DROP PRIMARY KEY,
	ADD PRIMARY KEY (`entry`, `sniff_build`);

ALTER TABLE `sniff_data`
	CHANGE COLUMN `object_type` `object_type` ENUM('None','Spell','Map','LFGDungeon','Battleground','Unit','GameObject','CreatureDifficulty','Item','Quest','Opcode','PageText','NpcText','BroadcastText','Gossip','Zone','Area','AreaTrigger','Phase','Player','Achievement','CreatureFamily','Criteria','Currency','Difficulty','Faction','MailTemplate','WorldState','Sound','Taxi') NOT NULL DEFAULT 'None' AFTER `sniff_id`;

CREATE TABLE `client_areatrigger_enter` (
	`unixtimems` BIGINT UNSIGNED NOT NULL COMMENT 'when the packet was sent',
	`areatrigger_id` INT UNSIGNED NOT NULL,
	PRIMARY KEY (`unixtimems`, `areatrigger_id`)
)
COMMENT='times when the client notified the server it has entered an areatrigger\'s confines\r\nfrom CMSG_AREA_TRIGGER'
COLLATE='latin1_swedish_ci'
ENGINE=InnoDB
;

CREATE TABLE `client_areatrigger_leave` (
	`unixtimems` BIGINT UNSIGNED NOT NULL COMMENT 'when the packet was sent',
	`areatrigger_id` INT UNSIGNED NOT NULL,
	PRIMARY KEY (`unixtimems`, `areatrigger_id`)
)
COMMENT='times when the client notified the server it has left an areatrigger\'s confines\r\nfrom CMSG_AREA_TRIGGER'
COLLATE='latin1_swedish_ci'
ENGINE=InnoDB
;

ALTER TABLE `creature_template`
	DROP COLUMN `ranged_attack_time`;

ALTER TABLE `creature`
	CHANGE COLUMN `is_hovering` `is_hovering` TINYINT(3) UNSIGNED NOT NULL DEFAULT '0' COMMENT 'hover bit from create object' AFTER `is_spawn`,
	CHANGE COLUMN `is_pet` `is_pet` TINYINT(3) UNSIGNED NOT NULL DEFAULT '0' COMMENT 'highguid is pet' AFTER `is_hovering`,
	CHANGE COLUMN `is_vehicle` `is_vehicle` TINYINT(3) UNSIGNED NOT NULL DEFAULT '0' COMMENT 'highguid is vehicle' AFTER `is_pet`;

CREATE TABLE `player_crit_chance` (
	`race` TINYINT UNSIGNED NOT NULL DEFAULT '0',
	`class` TINYINT UNSIGNED NOT NULL DEFAULT '0',
	`level` TINYINT UNSIGNED NOT NULL DEFAULT '0',
	`agility` INT UNSIGNED NOT NULL DEFAULT '0',
	`crit_chance` FLOAT NOT NULL DEFAULT '0',
	`weapon_item_id` INT UNSIGNED NOT NULL DEFAULT '0',
	`weapon_skill_id` SMALLINT UNSIGNED NOT NULL DEFAULT '0',
	`skill_current_value` SMALLINT UNSIGNED NOT NULL DEFAULT '0',
	`skill_max_value` SMALLINT UNSIGNED NOT NULL DEFAULT '0',
	`relevant_auras` VARCHAR(128) NOT NULL DEFAULT '' COMMENT 'auras of type 52'
)
COLLATE='latin1_swedish_ci'
ENGINE=InnoDB
;

ALTER TABLE `player_crit_chance`
	COMMENT='data about player melee crit chance, so that we can calculate the correct bonus to crit chance per agility in vanilla\r\nother expansions have this data in a dbc, because its shown on the user interface when you mouse over the agility stat';

CREATE TABLE `player_dodge_chance` (
	`race` TINYINT UNSIGNED NOT NULL DEFAULT '0',
	`class` TINYINT UNSIGNED NOT NULL DEFAULT '0',
	`level` TINYINT UNSIGNED NOT NULL DEFAULT '0',
	`agility` INT UNSIGNED NOT NULL DEFAULT '0',
	`dodge_chance` FLOAT NOT NULL DEFAULT '0',
	`defense_current_value` SMALLINT UNSIGNED NOT NULL DEFAULT '0',
	`defense_max_value` SMALLINT UNSIGNED NOT NULL DEFAULT '0',
	`relevant_auras` VARCHAR(128) NOT NULL DEFAULT '' COMMENT 'auras of type 49'
)
COLLATE='latin1_swedish_ci'
ENGINE=InnoDB
;

ALTER TABLE `player_dodge_chance`
	COMMENT='data about player dodge chance, so that we can calculate the correct bonus to dodge chance per agility in vanilla\r\nother expansions have this data in a dbc, because its shown on the user interface when you mouse over the agility stat';

ALTER TABLE `creature_questitem`
	ALTER `id` DROP DEFAULT;
ALTER TABLE `creature_questitem`
	CHANGE COLUMN `id` `idx` INT(10) UNSIGNED NOT NULL AFTER `entry`;

ALTER TABLE `creature_questitem`
	ALTER `idx` DROP DEFAULT;
ALTER TABLE `creature_questitem`
	CHANGE COLUMN `idx` `idx` TINYINT UNSIGNED NOT NULL AFTER `entry`;

ALTER TABLE `gameobject_questitem`
	CHANGE COLUMN `id` `idx` TINYINT UNSIGNED NOT NULL DEFAULT '0' AFTER `entry`;
  
ALTER TABLE `gameobject_questitem`
	ALTER `entry` DROP DEFAULT,
	ALTER `idx` DROP DEFAULT;
ALTER TABLE `gameobject_questitem`
	CHANGE COLUMN `entry` `entry` INT(10) UNSIGNED NOT NULL FIRST,
	CHANGE COLUMN `idx` `idx` TINYINT(3) UNSIGNED NOT NULL AFTER `entry`;

RENAME TABLE `creature_questitem` TO `creature_quest_item`;

RENAME TABLE `gameobject_questitem` TO `gameobject_quest_item`;

ALTER TABLE `creature_text_template`
	CHANGE COLUMN `group_id` `idx` TINYINT(3) UNSIGNED NOT NULL DEFAULT '0' COMMENT 'counter of unique texts per creature id' AFTER `entry`,
	CHANGE COLUMN `chat_type` `chat_type` TINYINT(3) UNSIGNED NOT NULL DEFAULT '0' COMMENT 'version specific chat type enum, not the same as values used in mangos' AFTER `text`,
	CHANGE COLUMN `language` `language` TINYINT(3) UNSIGNED NOT NULL DEFAULT '0' COMMENT 'part of the packet, references Languages.dbc' AFTER `chat_type`,
	CHANGE COLUMN `emote` `emote` MEDIUMINT(8) UNSIGNED NOT NULL DEFAULT '0' COMMENT 'not part of the packet, emote seen close to when the chat packet was received, references Emotes.dbc' AFTER `language`,
	CHANGE COLUMN `sound` `sound` MEDIUMINT(8) UNSIGNED NOT NULL DEFAULT '0' COMMENT 'not part of the packet, sound heard close to when the chat packet was received, references SoundEntries.dbc' AFTER `emote`,
	CHANGE COLUMN `broadcast_text_id` `broadcast_text_id` MEDIUMINT(6) UNSIGNED NOT NULL DEFAULT '0' COMMENT 'not part of the packet, must be manually set' AFTER `sound`;

ALTER TABLE `creature_text`
	ALTER `group_id` DROP DEFAULT;
ALTER TABLE `creature_text`
	CHANGE COLUMN `group_id` `idx` INT(10) UNSIGNED NOT NULL COMMENT 'counter of unique texts per creature id' AFTER `entry`;

ALTER TABLE `creature_text`
	DROP COLUMN `health_percent`;

ALTER TABLE `creature_text_template`
	ADD COLUMN `health_percent` FLOAT NULL DEFAULT NULL COMMENT 'not part of the packet, the current health of the creature at the time the text was said' AFTER `broadcast_text_id`;

ALTER TABLE `gameobject`
	ADD COLUMN `is_on_transport` TINYINT(1) UNSIGNED NOT NULL DEFAULT '0' AFTER `is_transport`;

ALTER TABLE `creature`
	ADD COLUMN `is_on_transport` TINYINT(3) UNSIGNED NOT NULL DEFAULT '0' AFTER `is_vehicle`;

CREATE TABLE `gameobject_unique_anim` (
  `entry` int(10) unsigned NOT NULL,
  `anim_id` int(10) unsigned NOT NULL,
  `as_despawn` tinyint(3) unsigned DEFAULT NULL,
  `sniff_build` mediumint(8) unsigned NOT NULL DEFAULT '0',
  PRIMARY KEY (`entry`,`anim_id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_unicode_ci ROW_FORMAT=COMPACT COMMENT='all unique custom anim ids used by a given gameobject id\r\nfrom SMSG_GAME_OBJECT_CUSTOM_ANIM';

ALTER TABLE `creature_faction`
	ADD COLUMN `sniff_build` MEDIUMINT UNSIGNED NOT NULL DEFAULT '0' AFTER `faction`;

RENAME TABLE `creature_faction` TO `creature_unique_faction`;

ALTER TABLE `creature_gossip`
	ALTER `entry` DROP DEFAULT,
	ALTER `gossip_menu_id` DROP DEFAULT,
	ALTER `is_default` DROP DEFAULT;

ALTER TABLE `creature_gossip`
	CHANGE COLUMN `entry` `entry` MEDIUMINT(8) UNSIGNED NOT NULL FIRST,
	CHANGE COLUMN `gossip_menu_id` `gossip_menu_id` MEDIUMINT(8) UNSIGNED NOT NULL AFTER `entry`,
	CHANGE COLUMN `is_default` `is_default` TINYINT(3) UNSIGNED NOT NULL AFTER `gossip_menu_id`;

RENAME TABLE `creature_gossip` TO `creature_unique_gossip`;

CREATE TABLE `creature_unique_emote` (
  `entry` int(10) unsigned NOT NULL,
  `emote_id` int(10) unsigned NOT NULL,
  `emote_name` varchar(50) COLLATE utf8_unicode_ci DEFAULT NULL,
  `sniff_build` mediumint(8) unsigned NOT NULL DEFAULT '0',
  PRIMARY KEY (`entry`,`emote_id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_unicode_ci ROW_FORMAT=COMPACT COMMENT='all unique emote ids played by a given creature id\r\nfrom SMSG_EMOTE';

ALTER TABLE `creature_text_template`
	COLLATE='latin1_general_ci';
  
ALTER TABLE `creature_text_template`
	CHANGE COLUMN `text` `text` LONGTEXT NULL COMMENT 'the actual text that was sent' COLLATE 'latin1_general_ci' AFTER `idx`;

ALTER TABLE `creature_text_template`
	CHANGE COLUMN `text` `text` VARCHAR(512) NOT NULL COMMENT 'the actual text that was sent' COLLATE 'latin1_general_ci' AFTER `idx`,
	DROP PRIMARY KEY,
	ADD PRIMARY KEY (`entry`, `text`);

ALTER TABLE `creature_text_template`
	CHANGE COLUMN `idx` `idx` TINYINT(3) UNSIGNED NOT NULL AUTO_INCREMENT COMMENT 'counter of unique texts per creature id' AFTER `entry`,
	ADD UNIQUE INDEX `entry` (`entry`, `idx`);

ALTER TABLE `creature_text_template`
	ADD COLUMN `sniff_id_list` TEXT NOT NULL AFTER `comment`;

ALTER TABLE `creature_unique_emote`
	ADD COLUMN `sniff_id_list` TEXT NOT NULL COLLATE 'latin1_general_ci' AFTER `sniff_build`;

ALTER TABLE `creature_unique_emote`
	DROP COLUMN `sniff_build`;
  
ALTER TABLE `creature_unique_emote`
	COLLATE='latin1_general_ci',
	CHANGE COLUMN `emote_name` `emote_name` VARCHAR(50) NULL DEFAULT NULL COLLATE 'latin1_general_ci' AFTER `emote_id`;

ALTER TABLE `creature_unique_faction`
	ADD COLUMN `sniff_id_list` TEXT NOT NULL AFTER `sniff_build`;

ALTER TABLE `creature_unique_faction`
	DROP COLUMN `sniff_build`;

ALTER TABLE `creature_unique_faction`
	COLLATE='latin1_general_ci',
	CHANGE COLUMN `sniff_id_list` `sniff_id_list` TEXT NOT NULL COLLATE 'latin1_general_ci' AFTER `faction`;

ALTER TABLE `creature_unique_gossip`
	DROP COLUMN `sniff_build`;

ALTER TABLE `creature_unique_gossip`
	ADD COLUMN `sniff_id_list` TEXT NOT NULL AFTER `is_default`;

ALTER TABLE `creature_unique_gossip`
	COLLATE='latin1_general_ci',
	CHANGE COLUMN `sniff_id_list` `sniff_id_list` TEXT NOT NULL COLLATE 'latin1_general_ci' AFTER `is_default`;

ALTER TABLE `gameobject_unique_anim`
	COLLATE='utf8_unicode_ci',
	ADD COLUMN `sniff_id_list` TEXT NOT NULL COLLATE 'latin1_general_ci' AFTER `as_despawn`,
	DROP COLUMN `sniff_build`;

ALTER TABLE `gameobject_unique_anim`
	COLLATE='latin1_general_ci';

ALTER TABLE `broadcast_text`
	COLLATE='latin1_general_ci';

ALTER TABLE `broadcast_text`
	CHANGE COLUMN `male_text` `male_text` VARCHAR(1024) NOT NULL DEFAULT '' COLLATE 'latin1_general_ci' AFTER `entry`,
	CHANGE COLUMN `female_text` `female_text` VARCHAR(1024) NOT NULL DEFAULT '' COLLATE 'latin1_general_ci' AFTER `male_text`;

ALTER TABLE `creature_spell_immunity`
	DROP COLUMN `sniff_id`,
	DROP COLUMN `sniff_build`;

ALTER TABLE `creature_spell_immunity`
	ADD COLUMN `sniff_id_list` TEXT NOT NULL AFTER `spell_id`;

ALTER TABLE `spell_unique_caster`
	ADD COLUMN `sniff_id_list` TEXT NOT NULL AFTER `spell_id`,
	DROP COLUMN `sniff_build`;

ALTER TABLE `gameobject_loot_item`
	CHANGE COLUMN `loot_id` `loot_id` INT(10) UNSIGNED NOT NULL COMMENT 'references gameobject_loot' FIRST;

CREATE TABLE `item_loot` (
  `entry` int(10) unsigned NOT NULL COMMENT 'item template id',
  `loot_id` int(10) unsigned NOT NULL COMMENT 'counter',
  `money` int(10) unsigned NOT NULL DEFAULT '0' COMMENT 'copper',
  `items_count` int(10) unsigned NOT NULL DEFAULT '0' COMMENT 'number of items dropped',
  `sniff_id` smallint(5) unsigned NOT NULL DEFAULT '0' COMMENT 'points to sniff_file table',
  `sniff_build` mediumint(8) unsigned NOT NULL DEFAULT '0',
  PRIMARY KEY (`entry`,`loot_id`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1 COLLATE=latin1_general_ci ROW_FORMAT=COMPACT COMMENT='each row represents a separate loot instance';


CREATE TABLE `item_loot_item` (
  `loot_id` int(10) unsigned NOT NULL COMMENT 'references item_loot',
  `item_id` int(10) unsigned NOT NULL COMMENT 'item template id',
  `count` int(10) unsigned NOT NULL DEFAULT '1' COMMENT 'stack size'
) ENGINE=InnoDB DEFAULT CHARSET=latin1 COLLATE=latin1_general_ci ROW_FORMAT=COMPACT COMMENT='individual item that is part of a loot instance';

RENAME TABLE `player_crit_chance` TO `player_melee_crit_chance`;

CREATE TABLE `player_ranged_crit_chance` (
  `race` tinyint(3) unsigned NOT NULL DEFAULT '0',
  `class` tinyint(3) unsigned NOT NULL DEFAULT '0',
  `level` tinyint(3) unsigned NOT NULL DEFAULT '0',
  `agility` int(10) unsigned NOT NULL DEFAULT '0',
  `crit_chance` float NOT NULL DEFAULT '0',
  `weapon_item_id` int(10) unsigned NOT NULL DEFAULT '0',
  `weapon_skill_id` smallint(5) unsigned NOT NULL DEFAULT '0',
  `skill_current_value` smallint(5) unsigned NOT NULL DEFAULT '0',
  `skill_max_value` smallint(5) unsigned NOT NULL DEFAULT '0',
  `relevant_auras` varchar(128) COLLATE latin1_general_ci NOT NULL DEFAULT '' COMMENT 'auras of type 52'
) ENGINE=InnoDB DEFAULT CHARSET=latin1 COLLATE=latin1_general_ci ROW_FORMAT=COMPACT COMMENT='data about player ranged crit chance, so that we can calculate the correct bonus to crit chance per agility in vanilla\r\nother expansions have this data in a dbc, because its shown on the user interface when you mouse over the agility stat';

CREATE TABLE `player_spell_crit_chance` (
  `race` tinyint(3) unsigned NOT NULL DEFAULT '0',
  `class` tinyint(3) unsigned NOT NULL DEFAULT '0',
  `level` tinyint(3) unsigned NOT NULL DEFAULT '0',
  `intellect` int(10) unsigned NOT NULL DEFAULT '0',
  `crit_chance` float NOT NULL DEFAULT '0',
  `relevant_auras` varchar(128) COLLATE latin1_general_ci NOT NULL DEFAULT '' COMMENT 'auras of type 57 and 71'
) ENGINE=InnoDB DEFAULT CHARSET=latin1 COLLATE=latin1_general_ci ROW_FORMAT=COMPACT COMMENT='data about player spell crit chance, so that we can calculate the correct bonus to crit chance per intellect in vanilla\r\nother expansions have this data in a dbc, because its shown on the user interface when you mouse over the intellect stat';

CREATE TABLE `sound_unique_source` (
  `source_id` int(10) unsigned NOT NULL DEFAULT '0',
  `source_type` varchar(16) COLLATE latin1_general_ci NOT NULL DEFAULT '',
  `sound_id` int(10) unsigned NOT NULL DEFAULT '0',
  `sniff_id_list` text COLLATE latin1_general_ci NOT NULL,
  PRIMARY KEY (`source_id`,`source_type`,`sound_id`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1 COLLATE=latin1_general_ci ROW_FORMAT=COMPACT COMMENT='unique source object and sound id combinations';

ALTER TABLE `creature_trainer`
	DROP PRIMARY KEY,
	ADD PRIMARY KEY (`entry`, `menu_id`, `trainer_id`, `option_index`);

ALTER TABLE `creature_trainer`
	ADD COLUMN `sniff_build` MEDIUMINT UNSIGNED NOT NULL DEFAULT '0' AFTER `option_index`;

ALTER TABLE `trainer`
	CHANGE COLUMN `greeting` `greeting` VARCHAR(128) NOT NULL DEFAULT '' AFTER `type`,
	DROP PRIMARY KEY,
	ADD PRIMARY KEY (`id`, `type`, `greeting`);

ALTER TABLE `spell_target_position`
	DROP PRIMARY KEY,
	ADD PRIMARY KEY (`spell_id`, `map`, `position_x`, `position_y`, `position_z`);

ALTER TABLE `quest_ender`
	ADD COLUMN `sniff_build` MEDIUMINT UNSIGNED NOT NULL DEFAULT '0' AFTER `quest_id`;

ALTER TABLE `quest_starter`
	ADD COLUMN `sniff_build` MEDIUMINT UNSIGNED NOT NULL DEFAULT '0' AFTER `quest_id`;

ALTER TABLE `npc_vendor`
	DROP INDEX `slot`,
	DROP PRIMARY KEY,
	ADD PRIMARY KEY (`entry`, `item`, `extended_cost`, `type`, `slot`, `maxcount`);

ALTER TABLE `npc_vendor`
	DROP COLUMN `sniff_build`;

ALTER TABLE `npc_vendor`
	ADD COLUMN `sniff_id_list` TEXT NOT NULL AFTER `ignore_filtering`;

ALTER TABLE `mail_template_item`
	DROP PRIMARY KEY,
	ADD PRIMARY KEY (`entry`, `slot`, `item_id`, `count`);

ALTER TABLE `creature_melee_damage`
	ADD INDEX `entry` (`entry`);

ALTER TABLE `creature_armor`
	ADD INDEX `entry` (`entry`);

ALTER TABLE `creature_kill_reputation`
	ADD INDEX `entry` (`entry`);

ALTER TABLE `creature_pet_actions`
	ADD INDEX `entry` (`entry`);

ALTER TABLE `creature_pet_cooldown`
	ADD INDEX `entry` (`entry`);

ALTER TABLE `creature_pet_remaining_cooldown`
	ADD INDEX `entry` (`entry`);

ALTER TABLE `creature_spell_timers`
	ADD INDEX `entry` (`entry`);

ALTER TABLE `creature_equip_template`
	DROP PRIMARY KEY,
	ADD PRIMARY KEY (`entry`, `item_id1`, `item_id2`, `item_id3`);

ALTER TABLE `creature_equip_template`
	CHANGE COLUMN `id` `idx` TINYINT(3) UNSIGNED NOT NULL AUTO_INCREMENT AFTER `entry`,
	ADD UNIQUE INDEX `entry` (`entry`, `idx`);

ALTER TABLE `creature_equip_template`
	CHANGE COLUMN `item_id1` `main_hand_slot_item` MEDIUMINT(8) UNSIGNED NOT NULL DEFAULT '0' AFTER `idx`,
	CHANGE COLUMN `item_id2` `off_hand_slot_item` MEDIUMINT(8) UNSIGNED NOT NULL DEFAULT '0' AFTER `main_hand_slot_item`,
	CHANGE COLUMN `item_id3` `ranged_slot_item` MEDIUMINT(8) UNSIGNED NOT NULL DEFAULT '0' AFTER `off_hand_slot_item`;

ALTER TABLE `creature_equip_template`
	ADD COLUMN `sniff_id_list` TEXT NOT NULL AFTER `ranged_slot_item`,
	DROP COLUMN `sniff_build`;

ALTER TABLE `player_classlevelstats`
	DROP PRIMARY KEY,
	ADD PRIMARY KEY (`class`, `level`, `basehp`, `basemana`);

ALTER TABLE `player_classlevelstats`
	ADD COLUMN `sniff_id_list` TEXT NOT NULL AFTER `basemana`,
	DROP COLUMN `sniff_id`,
	DROP COLUMN `sniff_build`;

ALTER TABLE `player_levelstats`
	DROP PRIMARY KEY,
	ADD PRIMARY KEY (`race`, `class`, `level`, `str`, `agi`, `sta`, `inte`, `spi`);

ALTER TABLE `player_levelstats`
	ADD COLUMN `sniff_id_list` TEXT NOT NULL AFTER `spi`,
	DROP COLUMN `sniff_id`,
	DROP COLUMN `sniff_build`;

ALTER TABLE `player_classlevelstats`
	ALTER `basehp` DROP DEFAULT,
	ALTER `basemana` DROP DEFAULT;
ALTER TABLE `player_classlevelstats`
	CHANGE COLUMN `basehp` `base_health` SMALLINT(5) UNSIGNED NOT NULL AFTER `level`,
	CHANGE COLUMN `basemana` `base_mana` SMALLINT(5) UNSIGNED NOT NULL AFTER `base_health`;

ALTER TABLE `player_levelstats`
	ALTER `str` DROP DEFAULT,
	ALTER `agi` DROP DEFAULT,
	ALTER `sta` DROP DEFAULT,
	ALTER `inte` DROP DEFAULT,
	ALTER `spi` DROP DEFAULT;
ALTER TABLE `player_levelstats`
	CHANGE COLUMN `str` `strength` TINYINT(3) UNSIGNED NOT NULL AFTER `level`,
	CHANGE COLUMN `agi` `agility` TINYINT(3) UNSIGNED NOT NULL AFTER `strength`,
	CHANGE COLUMN `sta` `stamina` TINYINT(3) UNSIGNED NOT NULL AFTER `agility`,
	CHANGE COLUMN `inte` `intellect` TINYINT(3) UNSIGNED NOT NULL AFTER `stamina`,
	CHANGE COLUMN `spi` `spirit` TINYINT(3) UNSIGNED NOT NULL AFTER `intellect`;

ALTER TABLE `creature_template`
	ADD COLUMN `sniff_build` MEDIUMINT UNSIGNED NOT NULL DEFAULT '0' AFTER `auras`;

ALTER TABLE `creature_stats`
	DROP PRIMARY KEY,
	ADD INDEX `entry` (`entry`);

ALTER TABLE `sound_unique_source`
	CHANGE COLUMN `sound_id` `sound` INT(10) UNSIGNED NOT NULL DEFAULT '0' AFTER `source_type`;

ALTER TABLE `creature_template_wdb`
	CHANGE COLUMN `beast_family` `pet_family` INT(11) NOT NULL DEFAULT '0' AFTER `rank`;

RENAME TABLE `creature_equip_template` TO `creature_unique_equipment`;

RENAME TABLE `creature_text_template` TO `creature_unique_text`;

ALTER TABLE `creature_text`
	ALTER `idx` DROP DEFAULT;
ALTER TABLE `creature_text`
	CHANGE COLUMN `idx` `text` VARCHAR(50) NOT NULL AFTER `entry`,
	ADD COLUMN `chat_type` TINYINT UNSIGNED NOT NULL DEFAULT '0' AFTER `text`,
	ADD COLUMN `language` TINYINT UNSIGNED NOT NULL DEFAULT '0' AFTER `chat_type`;

ALTER TABLE `creature_text`
	ALTER `text` DROP DEFAULT;
ALTER TABLE `creature_text`
	COLLATE='utf8_general_ci',
	CHANGE COLUMN `text` `text` TEXT NOT NULL COLLATE 'utf8_general_ci' AFTER `entry`,
	DROP PRIMARY KEY;

CREATE TABLE `player_minimap_ping` (
  `unixtimems` bigint(20) unsigned NOT NULL DEFAULT '0',
  `guid` int(10) unsigned NOT NULL,
  `position_x` float NOT NULL DEFAULT '0',
  `position_y` float NOT NULL DEFAULT '0',
  PRIMARY KEY (`guid`,`unixtimems`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1 COLLATE=latin1_general_ci ROW_FORMAT=COMPACT COMMENT='from SMSG_MINIMAP_PING';

CREATE TABLE `raid_target_icon_update` (
	`unixtimems` BIGINT UNSIGNED NOT NULL,
	`is_full_update` TINYINT UNSIGNED NOT NULL,
	`icon` TINYINT NOT NULL,
	`target_guid` INT UNSIGNED NOT NULL,
	`target_id` INT UNSIGNED NOT NULL,
	`target_type` VARCHAR(16) NOT NULL,
	PRIMARY KEY (`unixtimems`, `icon`, `target_guid`, `target_type`)
)
COLLATE='latin1_general_ci'
ENGINE=InnoDB
;

ALTER TABLE `sniff_file`
	ADD COLUMN `author` VARCHAR(32) NOT NULL DEFAULT '' AFTER `build`;

ALTER TABLE `creature_auras_update`
	ALTER `slot` DROP DEFAULT;
ALTER TABLE `creature_auras_update`
	ADD COLUMN `is_full_update` TINYINT UNSIGNED NOT NULL AFTER `update_id`,
	CHANGE COLUMN `slot` `slot` SMALLINT NOT NULL AFTER `is_full_update`;

ALTER TABLE `player_auras_update`
	ALTER `slot` DROP DEFAULT;
ALTER TABLE `player_auras_update`
	ADD COLUMN `is_full_update` TINYINT UNSIGNED NOT NULL AFTER `update_id`,
	CHANGE COLUMN `slot` `slot` SMALLINT NOT NULL AFTER `is_full_update`;

ALTER TABLE `player`
	CHANGE COLUMN `pvp_rank` `pvp_rank` TINYINT UNSIGNED NOT NULL DEFAULT '0' AFTER `player_flags`,
	DROP COLUMN `xp`,
	DROP COLUMN `money`;

ALTER TABLE `creature_attack_log`
	CHANGE COLUMN `damage` `damage` INT(10) NOT NULL DEFAULT '0' AFTER `hit_info`,
	CHANGE COLUMN `original_damage` `original_damage` INT(10) NOT NULL DEFAULT '0' AFTER `damage`,
	CHANGE COLUMN `total_absorbed_damage` `total_absorbed_damage` INT(11) NOT NULL DEFAULT '0' AFTER `total_school_mask`,
	CHANGE COLUMN `total_resisted_damage` `total_resisted_damage` INT(11) NOT NULL DEFAULT '0' AFTER `total_absorbed_damage`;

ALTER TABLE `player_attack_log`
	CHANGE COLUMN `damage` `damage` INT(10) NOT NULL DEFAULT '0' AFTER `hit_info`,
	CHANGE COLUMN `original_damage` `original_damage` INT(10) NOT NULL DEFAULT '0' AFTER `damage`,
	CHANGE COLUMN `total_absorbed_damage` `total_absorbed_damage` INT(11) NOT NULL DEFAULT '0' AFTER `total_school_mask`,
	CHANGE COLUMN `total_resisted_damage` `total_resisted_damage` INT(11) NOT NULL DEFAULT '0' AFTER `total_absorbed_damage`;

ALTER TABLE `creature_melee_damage`
	CHANGE COLUMN `damage_min` `damage_min` INT(10) NOT NULL DEFAULT '0' AFTER `hits_count`,
	CHANGE COLUMN `damage_average` `damage_average` INT(10) NOT NULL DEFAULT '0' AFTER `damage_min`,
	CHANGE COLUMN `damage_max` `damage_max` INT(10) NOT NULL DEFAULT '0' AFTER `damage_average`;

CREATE TABLE `replay_marked_creature` (
	`guid` INT UNSIGNED NOT NULL,
	`marker` INT NOT NULL DEFAULT '0',
	PRIMARY KEY (`guid`)
)
COMMENT='creatures that have been manually marked from replay core'
COLLATE='latin1_general_ci'
ENGINE=InnoDB
;

CREATE TABLE `replay_marked_gameobject` (
	`guid` INT UNSIGNED NOT NULL,
	`marker` INT NOT NULL DEFAULT '0',
	PRIMARY KEY (`guid`)
)
COMMENT='gameobjects that have been manually marked from replay core'
COLLATE='latin1_general_ci'
ENGINE=InnoDB
;

ALTER TABLE `creature_pet_remaining_cooldown`
	CHANGE COLUMN `category_cooldown` `category_cooldown` INT(10) NOT NULL DEFAULT '0' AFTER `category`;
ALTER TABLE `creature_pet_remaining_cooldown`
	CHANGE COLUMN `mod_rate` `mod_rate` FLOAT NOT NULL DEFAULT '1' AFTER `category_cooldown`;

CREATE TABLE `gameobject_respawn_time` (
	`old_guid` INT(10) UNSIGNED NOT NULL,
	`new_guid` INT(10) UNSIGNED NOT NULL,
	`respawn_time` INT(10) UNSIGNED NOT NULL COMMENT 'time in seconds',
	PRIMARY KEY (`old_guid`, `new_guid`)
)
 COLLATE 'latin1_general_ci' ENGINE=InnoDB ROW_FORMAT=Compact COMMENT='stores the time in seconds between the despawn of a gameobject, and the spawn of another on the same position\r\nrespawn time is reduced dynamically if there are too many players in the same area, so beware of abnormally low values';

CREATE TABLE IF NOT EXISTS `creature_visibility_distance` (
  `entry` int(10) unsigned NOT NULL,
  `map` int(10) unsigned NOT NULL,
  `distance` int(10) unsigned NOT NULL COMMENT 'distance from player at time of create object',
  `sniff_id_list` text COLLATE latin1_general_ci NOT NULL,
  PRIMARY KEY (`entry`,`distance`,`map`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1 COLLATE=latin1_general_ci ROW_FORMAT=COMPACT COMMENT='max visibility distances';

CREATE TABLE IF NOT EXISTS `gameobject_visibility_distance` (
  `entry` int(10) unsigned NOT NULL,
  `map` int(10) unsigned NOT NULL,
  `distance` int(10) unsigned NOT NULL COMMENT 'distance from player at time of create object',
  `sniff_id_list` text COLLATE latin1_general_ci NOT NULL,
  PRIMARY KEY (`entry`,`distance`,`map`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1 COLLATE=latin1_general_ci ROW_FORMAT=COMPACT COMMENT='max visibility distances';

ALTER TABLE `sniff_file`
	CHANGE COLUMN `name` `name` VARCHAR(128) NOT NULL COLLATE 'utf8_unicode_ci' AFTER `author`,
	ADD UNIQUE INDEX `name` (`name`);

DROP TABLE IF EXISTS `creature_spell_not_immune_dispel`;
CREATE TABLE IF NOT EXISTS `creature_spell_not_immune_dispel` (
  `entry` int(10) unsigned NOT NULL,
  `dispel` int(10) unsigned NOT NULL,
  `spell_id` int(10) unsigned NOT NULL,
  `sniff_id_list` text COLLATE latin1_general_ci NOT NULL,
  PRIMARY KEY (`entry`,`dispel`)
) ENGINE=MyISAM DEFAULT CHARSET=latin1 COLLATE=latin1_general_ci ROW_FORMAT=DYNAMIC COMMENT='spells that creatures were not immune to\r\nfrom SMSG_SPELL_GO';

DROP TABLE IF EXISTS `creature_spell_not_immune_mechanic`;
CREATE TABLE IF NOT EXISTS `creature_spell_not_immune_mechanic` (
  `entry` int(10) unsigned NOT NULL,
  `mechanic` int(10) unsigned NOT NULL,
  `spell_id` int(10) unsigned NOT NULL,
  `sniff_id_list` text COLLATE latin1_general_ci NOT NULL,
  PRIMARY KEY (`entry`,`mechanic`)
) ENGINE=MyISAM DEFAULT CHARSET=latin1 COLLATE=latin1_general_ci ROW_FORMAT=DYNAMIC COMMENT='spells that creatures were not immune to\r\nfrom SMSG_SPELL_GO';

DROP TABLE IF EXISTS `creature_spell_not_immune_school`;
CREATE TABLE IF NOT EXISTS `creature_spell_not_immune_school` (
  `entry` int(10) unsigned NOT NULL,
  `school` int(10) unsigned NOT NULL,
  `spell_id` int(10) unsigned NOT NULL,
  `sniff_id_list` text COLLATE latin1_general_ci NOT NULL,
  PRIMARY KEY (`entry`,`school`)
) ENGINE=MyISAM DEFAULT CHARSET=latin1 COLLATE=latin1_general_ci ROW_FORMAT=DYNAMIC COMMENT='spells that creatures were not immune to\r\nfrom SMSG_SPELL_GO';

ALTER TABLE `creature`
	ADD COLUMN `observation_time` MEDIUMINT(8) UNSIGNED NOT NULL DEFAULT '0' COMMENT 'accumulated time in ms between create and destroy packets' AFTER `id`;

ALTER TABLE `gameobject`
	ADD COLUMN `observation_time` MEDIUMINT(8) UNSIGNED NOT NULL DEFAULT '0' COMMENT 'accumulated time in ms between create and destroy packets' AFTER `id`;

ALTER TABLE `creature`
	CHANGE COLUMN `observation_time` `total_observation_time` MEDIUMINT(8) UNSIGNED NOT NULL DEFAULT '0' COMMENT 'accumulated time in ms between create and destroy packets' AFTER `id`,
	ADD COLUMN `longest_observation_time` MEDIUMINT(8) UNSIGNED NOT NULL DEFAULT '0' COMMENT 'longest uninterrupted period of time in ms the object was visible for' AFTER `total_observation_time`;

ALTER TABLE `gameobject`
	CHANGE COLUMN `observation_time` `total_observation_time` MEDIUMINT(8) UNSIGNED NOT NULL DEFAULT '0' COMMENT 'accumulated time in ms between create and destroy packets' AFTER `id`,
	ADD COLUMN `longest_observation_time` MEDIUMINT(8) UNSIGNED NOT NULL DEFAULT '0' COMMENT 'longest uninterrupted period of time in ms the object was visible for' AFTER `total_observation_time`;

ALTER TABLE `quest_details`
	CHANGE COLUMN `ID` `entry` MEDIUMINT(8) UNSIGNED NOT NULL DEFAULT '0' FIRST,
	CHANGE COLUMN `VerifiedBuild` `sniff_build` MEDIUMINT(8) UNSIGNED NOT NULL DEFAULT '0' AFTER `entry`,
	CHANGE COLUMN `Emote1` `emote_id1` SMALLINT(5) UNSIGNED NOT NULL DEFAULT '0' AFTER `sniff_build`,
	CHANGE COLUMN `Emote2` `emote_id2` SMALLINT(5) UNSIGNED NOT NULL DEFAULT '0' AFTER `emote_id1`,
	CHANGE COLUMN `Emote3` `emote_id3` SMALLINT(5) UNSIGNED NOT NULL DEFAULT '0' AFTER `emote_id2`,
	CHANGE COLUMN `Emote4` `emote_id4` SMALLINT(5) UNSIGNED NOT NULL DEFAULT '0' AFTER `emote_id3`,
	CHANGE COLUMN `EmoteDelay1` `emote_delay1` INT(10) UNSIGNED NOT NULL DEFAULT '0' AFTER `emote_id4`,
	CHANGE COLUMN `EmoteDelay2` `emote_delay2` INT(10) UNSIGNED NOT NULL DEFAULT '0' AFTER `emote_delay1`,
	CHANGE COLUMN `EmoteDelay3` `emote_delay3` INT(10) UNSIGNED NOT NULL DEFAULT '0' AFTER `emote_delay2`,
	CHANGE COLUMN `EmoteDelay4` `emote_delay4` INT(10) UNSIGNED NOT NULL DEFAULT '0' AFTER `emote_delay3`,
	DROP PRIMARY KEY,
	ADD PRIMARY KEY (`entry`, `sniff_build`);

ALTER TABLE `quest_offer_reward`
	CHANGE COLUMN `ID` `entry` MEDIUMINT(8) UNSIGNED NOT NULL DEFAULT '0' FIRST,
	CHANGE COLUMN `VerifiedBuild` `sniff_build` MEDIUMINT(8) UNSIGNED NOT NULL DEFAULT '0' AFTER `entry`,
	CHANGE COLUMN `Emote1` `emote_id1` SMALLINT(5) UNSIGNED NOT NULL DEFAULT '0' AFTER `sniff_build`,
	CHANGE COLUMN `Emote2` `emote_id2` SMALLINT(5) UNSIGNED NOT NULL DEFAULT '0' AFTER `emote_id1`,
	CHANGE COLUMN `Emote3` `emote_id3` SMALLINT(5) UNSIGNED NOT NULL DEFAULT '0' AFTER `emote_id2`,
	CHANGE COLUMN `Emote4` `emote_id4` SMALLINT(5) UNSIGNED NOT NULL DEFAULT '0' AFTER `emote_id3`,
	CHANGE COLUMN `EmoteDelay1` `emote_delay1` INT(10) UNSIGNED NOT NULL DEFAULT '0' AFTER `emote_id4`,
	CHANGE COLUMN `EmoteDelay2` `emote_delay2` INT(10) UNSIGNED NOT NULL DEFAULT '0' AFTER `emote_delay1`,
	CHANGE COLUMN `EmoteDelay3` `emote_delay3` INT(10) UNSIGNED NOT NULL DEFAULT '0' AFTER `emote_delay2`,
	CHANGE COLUMN `EmoteDelay4` `emote_delay4` INT(10) UNSIGNED NOT NULL DEFAULT '0' AFTER `emote_delay3`,
	CHANGE COLUMN `RewardText` `reward_text` TEXT NULL COLLATE 'latin1_general_ci' AFTER `emote_delay4`,
	DROP PRIMARY KEY,
	ADD PRIMARY KEY (`entry`, `sniff_build`);

ALTER TABLE `quest_request_items`
	CHANGE COLUMN `ID` `entry` MEDIUMINT(8) UNSIGNED NOT NULL DEFAULT '0' FIRST,
	CHANGE COLUMN `VerifiedBuild` `sniff_build` MEDIUMINT(8) UNSIGNED NOT NULL DEFAULT '0' AFTER `entry`,
	CHANGE COLUMN `Emote` `emote_id` SMALLINT(5) UNSIGNED NULL DEFAULT NULL AFTER `sniff_build`,
	CHANGE COLUMN `CompletionText` `completion_text` TEXT NULL COLLATE 'latin1_general_ci' AFTER `emote_id`,
	DROP PRIMARY KEY,
	ADD PRIMARY KEY (`entry`, `sniff_build`);

DROP TABLE `creature_spell_not_immune_dispel`;
DROP TABLE `creature_spell_not_immune_mechanic`;
DROP TABLE `creature_spell_not_immune_school`;

DROP TABLE IF EXISTS `creature_unique_spell_hit`;
CREATE TABLE IF NOT EXISTS `creature_unique_spell_hit` (
  `entry` int(10) unsigned NOT NULL,
  `spell_id` int(10) unsigned NOT NULL,
  `sniff_id_list` text COLLATE latin1_general_ci NOT NULL,
  PRIMARY KEY (`entry`,`spell_id`)
) ENGINE=MyISAM DEFAULT CHARSET=latin1 COLLATE=latin1_general_ci ROW_FORMAT=DYNAMIC COMMENT='spells that creatures were hit by';

ALTER TABLE `quest_template`
	CHANGE COLUMN `VerifiedBuild` `SniffBuild` MEDIUMINT(8) UNSIGNED NOT NULL DEFAULT '0' AFTER `ID`;

ALTER TABLE `quest_template`
	DROP PRIMARY KEY,
	ADD PRIMARY KEY (`ID`, `SniffBuild`);

ALTER TABLE `quest_objectives`
	CHANGE COLUMN `VerifiedBuild` `SniffBuild` MEDIUMINT(8) UNSIGNED NOT NULL DEFAULT '0' AFTER `ID`,
	DROP PRIMARY KEY,
	ADD PRIMARY KEY (`ID`, `SniffBuild`);

ALTER TABLE `creature`
	ADD COLUMN `misc_flags` TINYINT(3) UNSIGNED NOT NULL DEFAULT '0' COMMENT 'from UNIT_FIELD_BYTES_2, only in vanilla' AFTER `sheath_state`,
	ADD COLUMN `debuff_limit` TINYINT(3) UNSIGNED NOT NULL DEFAULT '0' COMMENT 'from UNIT_FIELD_BYTES_2, only in tbc' AFTER `misc_flags`,
	CHANGE COLUMN `pvp_flags` `pvp_flags` TINYINT(3) UNSIGNED NOT NULL DEFAULT '0' COMMENT 'from UNIT_FIELD_BYTES_2, wotlk+' AFTER `debuff_limit`;

ALTER TABLE `player`
	ADD COLUMN `misc_flags` TINYINT(3) UNSIGNED NOT NULL DEFAULT '0' COMMENT 'from UNIT_FIELD_BYTES_2, only in vanilla' AFTER `sheath_state`,
	ADD COLUMN `debuff_limit` TINYINT(3) UNSIGNED NOT NULL DEFAULT '0' COMMENT 'from UNIT_FIELD_BYTES_2, only in tbc' AFTER `misc_flags`,
	CHANGE COLUMN `pvp_flags` `pvp_flags` TINYINT(3) UNSIGNED NOT NULL DEFAULT '0' COMMENT 'from UNIT_FIELD_BYTES_2, wotlk+' AFTER `debuff_limit`;

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

ALTER TABLE `gameobject_template`
	CHANGE COLUMN `scale` `scale` FLOAT NULL DEFAULT NULL COMMENT 'wotlk+' AFTER `display_id`,
	CHANGE COLUMN `icon_name` `icon_name` VARCHAR(100) NULL DEFAULT NULL COMMENT 'tbc+' AFTER `name`,
	CHANGE COLUMN `cast_bar_caption` `cast_bar_caption` VARCHAR(100) NULL DEFAULT NULL COMMENT 'tbc+' AFTER `icon_name`,
	CHANGE COLUMN `unk1` `unk1` VARCHAR(100) NULL DEFAULT NULL COMMENT '1.12+' AFTER `cast_bar_caption`,
	CHANGE COLUMN `data16` `data16` INT(10) UNSIGNED NULL DEFAULT NULL COMMENT '1.12+' AFTER `data15`,
	CHANGE COLUMN `data17` `data17` INT(10) UNSIGNED NULL DEFAULT NULL COMMENT '1.12+' AFTER `data16`,
	CHANGE COLUMN `data18` `data18` INT(10) UNSIGNED NULL DEFAULT NULL COMMENT '1.12+' AFTER `data17`,
	CHANGE COLUMN `data19` `data19` INT(10) UNSIGNED NULL DEFAULT NULL COMMENT '1.12+' AFTER `data18`,
	CHANGE COLUMN `data20` `data20` INT(10) UNSIGNED NULL DEFAULT NULL COMMENT '1.12+' AFTER `data19`,
	CHANGE COLUMN `data21` `data21` INT(10) UNSIGNED NULL DEFAULT NULL COMMENT '1.12+' AFTER `data20`,
	CHANGE COLUMN `data22` `data22` INT(10) UNSIGNED NULL DEFAULT NULL COMMENT '1.12+' AFTER `data21`,
	CHANGE COLUMN `data23` `data23` INT(10) UNSIGNED NULL DEFAULT NULL COMMENT '1.12+' AFTER `data22`,
	CHANGE COLUMN `data24` `data24` INT(10) UNSIGNED NULL DEFAULT NULL COMMENT 'cata+' AFTER `data23`,
	CHANGE COLUMN `data25` `data25` INT(10) UNSIGNED NULL DEFAULT NULL COMMENT 'cata+' AFTER `data24`,
	CHANGE COLUMN `data26` `data26` INT(10) UNSIGNED NULL DEFAULT NULL COMMENT 'cata+' AFTER `data25`,
	CHANGE COLUMN `data27` `data27` INT(10) UNSIGNED NULL DEFAULT NULL COMMENT 'cata+' AFTER `data26`,
	CHANGE COLUMN `data28` `data28` INT(10) UNSIGNED NULL DEFAULT NULL COMMENT 'cata+' AFTER `data27`,
	CHANGE COLUMN `data29` `data29` INT(10) UNSIGNED NULL DEFAULT NULL COMMENT 'cata+' AFTER `data28`,
	CHANGE COLUMN `data30` `data30` INT(10) UNSIGNED NULL DEFAULT NULL COMMENT 'cata+' AFTER `data29`,
	CHANGE COLUMN `data31` `data31` INT(10) UNSIGNED NULL DEFAULT NULL COMMENT 'cata+' AFTER `data30`,
	CHANGE COLUMN `data32` `data32` INT(10) UNSIGNED NULL DEFAULT NULL COMMENT 'cata+' AFTER `data31`,
	CHANGE COLUMN `data33` `data33` INT(10) UNSIGNED NULL DEFAULT NULL COMMENT 'cata+' AFTER `data32`,
	CHANGE COLUMN `quest_items_count` `quest_items_count` TINYINT(3) UNSIGNED NULL DEFAULT NULL COMMENT 'wotlk+' AFTER `data33`,
	CHANGE COLUMN `required_level` `required_level` TINYINT(3) UNSIGNED NULL DEFAULT NULL COMMENT 'cata+' AFTER `quest_items_count`;

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

ALTER TABLE `creature_template_wdb`
	CHANGE COLUMN `kill_credit1` `kill_credit1` INT(11) NULL DEFAULT NULL COMMENT 'wotlk+' AFTER `sniff_build`,
	CHANGE COLUMN `kill_credit2` `kill_credit2` INT(11) NULL DEFAULT NULL COMMENT 'wotlk+' AFTER `kill_credit1`,
	CHANGE COLUMN `display_total_count` `display_total_count` INT(11) NULL DEFAULT NULL COMMENT '1.13+' AFTER `kill_credit2`,
	CHANGE COLUMN `display_total_probability` `display_total_probability` FLOAT NULL DEFAULT NULL COMMENT '1.13+' AFTER `display_total_count`,
	CHANGE COLUMN `display_id2` `display_id2` INT(11) NULL DEFAULT NULL COMMENT 'tbc+' AFTER `display_id1`,
	CHANGE COLUMN `display_id3` `display_id3` INT(11) NULL DEFAULT NULL COMMENT 'tbc+' AFTER `display_id2`,
	CHANGE COLUMN `display_id4` `display_id4` INT(11) NULL DEFAULT NULL COMMENT 'tbc+' AFTER `display_id3`,
	CHANGE COLUMN `display_scale1` `display_scale1` FLOAT NULL DEFAULT NULL COMMENT '1.13+' AFTER `display_id4`,
	CHANGE COLUMN `display_scale2` `display_scale2` FLOAT NULL DEFAULT NULL COMMENT '1.13+' AFTER `display_scale1`,
	CHANGE COLUMN `display_scale3` `display_scale3` FLOAT NULL DEFAULT NULL COMMENT '1.13+' AFTER `display_scale2`,
	CHANGE COLUMN `display_scale4` `display_scale4` FLOAT NULL DEFAULT NULL COMMENT '1.13+' AFTER `display_scale3`,
	CHANGE COLUMN `display_probability1` `display_probability1` FLOAT NULL DEFAULT NULL COMMENT '1.13+' AFTER `display_scale4`,
	CHANGE COLUMN `display_probability2` `display_probability2` FLOAT NULL DEFAULT NULL COMMENT '1.13+' AFTER `display_probability1`,
	CHANGE COLUMN `display_probability3` `display_probability3` FLOAT NULL DEFAULT NULL COMMENT '1.13+' AFTER `display_probability2`,
	CHANGE COLUMN `display_probability4` `display_probability4` FLOAT NULL DEFAULT NULL COMMENT '1.13+' AFTER `display_probability3`,
	CHANGE COLUMN `female_name` `female_name` VARCHAR(256) NULL DEFAULT NULL COMMENT 'cata+' COLLATE 'latin1_general_ci' AFTER `name`,
	CHANGE COLUMN `title_alt` `title_alt` VARCHAR(256) NULL DEFAULT NULL COMMENT 'mop+' COLLATE 'latin1_general_ci' AFTER `subname`,
	CHANGE COLUMN `icon_name` `icon_name` VARCHAR(256) NULL DEFAULT NULL COMMENT 'tbc+' COLLATE 'latin1_general_ci' AFTER `title_alt`,
	CHANGE COLUMN `health_scaling_expansion` `health_scaling_expansion` INT(11) NULL DEFAULT NULL COMMENT 'wod+' AFTER `icon_name`,
	CHANGE COLUMN `required_expansion` `required_expansion` INT(11) NULL DEFAULT NULL COMMENT 'cata+' AFTER `health_scaling_expansion`,
	CHANGE COLUMN `vignette_id` `vignette_id` INT(11) NULL DEFAULT NULL COMMENT 'legion+' AFTER `required_expansion`,
	CHANGE COLUMN `unit_class` `unit_class` INT(11) NULL DEFAULT NULL COMMENT 'bfa+' AFTER `vignette_id`,
	CHANGE COLUMN `type_flags` `type_flags` INT(10) UNSIGNED NOT NULL DEFAULT '0' COMMENT 'actually static flags before 1.10' AFTER `type`,
	CHANGE COLUMN `type_flags2` `type_flags2` INT(10) UNSIGNED NULL DEFAULT NULL COMMENT 'cata+' AFTER `type_flags`,
	CHANGE COLUMN `pet_spell_list_id` `pet_spell_list_id` INT(11) NULL DEFAULT NULL COMMENT '1.8+' AFTER `type_flags2`,
	CHANGE COLUMN `health_multiplier` `health_multiplier` FLOAT NULL DEFAULT NULL COMMENT 'tbc+' AFTER `pet_spell_list_id`,
	CHANGE COLUMN `mana_multiplier` `mana_multiplier` FLOAT NULL DEFAULT NULL COMMENT 'tbc+' AFTER `health_multiplier`,
	CHANGE COLUMN `civilian` `civilian` INT(11) NULL DEFAULT NULL COMMENT 'removed in tbc' AFTER `mana_multiplier`,
	CHANGE COLUMN `movement_id` `movement_id` INT(11) NULL DEFAULT NULL COMMENT 'wotlk+' AFTER `racial_leader`;

ALTER TABLE `creature_template`
	ADD COLUMN `unit_class` INT(10) UNSIGNED NOT NULL DEFAULT '0' AFTER `base_attack_time`;

ALTER TABLE `creature_template`
	CHANGE COLUMN `unit_flags2` `unit_flags2` INT(10) UNSIGNED NULL DEFAULT NULL COMMENT 'tbc+' AFTER `unit_flags`,
	CHANGE COLUMN `vehicle_id` `vehicle_id` INT(11) NULL DEFAULT NULL COMMENT 'wotlk+' AFTER `unit_flags2`,
	CHANGE COLUMN `hover_height` `hover_height` FLOAT NULL DEFAULT NULL COMMENT 'wotlk+' AFTER `vehicle_id`;

ALTER TABLE `play_spell_visual_kit`
	ALTER `kit_id` DROP DEFAULT;
ALTER TABLE `play_spell_visual_kit`
	CHANGE COLUMN `kit_id` `kit_id` INT(10) NOT NULL COMMENT 'references SpellVisualKit.dbc' AFTER `caster_type`,
	CHANGE COLUMN `kit_type` `kit_type` INT(10) NULL DEFAULT NULL AFTER `kit_id`;

ALTER TABLE `creature_movement_server`
	ADD COLUMN `anim_tier` TINYINT(4) UNSIGNED NOT NULL DEFAULT '0' AFTER `transport_seat`,
	ADD COLUMN `vertical_speed` FLOAT NOT NULL DEFAULT '0' AFTER `anim_tier`,
	ADD COLUMN `effect_start_time` INT UNSIGNED NOT NULL DEFAULT '0' AFTER `vertical_speed`;

ALTER TABLE `player_movement_server`
	ADD COLUMN `anim_tier` TINYINT(4) UNSIGNED NOT NULL DEFAULT '0' AFTER `transport_seat`,
	ADD COLUMN `vertical_speed` FLOAT NOT NULL DEFAULT '0' AFTER `anim_tier`,
	ADD COLUMN `effect_start_time` INT UNSIGNED NOT NULL DEFAULT '0' AFTER `vertical_speed`;

ALTER TABLE `creature_movement_server_combat`
	ADD COLUMN `anim_tier` TINYINT(4) UNSIGNED NOT NULL DEFAULT '0' AFTER `transport_seat`,
	ADD COLUMN `vertical_speed` FLOAT NOT NULL DEFAULT '0' AFTER `anim_tier`,
	ADD COLUMN `effect_start_time` INT UNSIGNED NOT NULL DEFAULT '0' AFTER `vertical_speed`;

DROP TABLE IF EXISTS `creature_flight_spline_sync`;
CREATE TABLE IF NOT EXISTS `creature_flight_spline_sync` (
  `unixtimems` bigint(20) unsigned NOT NULL,
  `guid` int(10) unsigned NOT NULL,
  `duration_percent` float NOT NULL,
  PRIMARY KEY (`unixtimems`,`guid`)
) ENGINE=MyISAM DEFAULT CHARSET=latin1 COLLATE=latin1_general_ci;

CREATE TABLE IF NOT EXISTS `spell_unique_chain_updates` (
  `spell_id` int(10) unsigned NOT NULL DEFAULT '0',
  `sniff_id_list` text COLLATE latin1_general_ci NOT NULL,
  PRIMARY KEY (`spell_id`)
) ENGINE=MyISAM DEFAULT CHARSET=latin1 COLLATE=latin1_general_ci ROW_FORMAT=COMPACT COMMENT='spell ids that have been sent in SMSG_SPELL_UPDATE_CHAIN_TARGETS';

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

CREATE TABLE IF NOT EXISTS `spell_chain_update_target` (
  `list_id` int(10) unsigned NOT NULL DEFAULT '0',
  `target_guid` int(10) unsigned NOT NULL DEFAULT '0',
  `target_id` int(10) unsigned NOT NULL DEFAULT '0',
  `target_type` varchar(16) COLLATE latin1_general_ci NOT NULL DEFAULT ''
) ENGINE=MyISAM DEFAULT CHARSET=latin1 COLLATE=latin1_general_ci ROW_FORMAT=DYNAMIC COMMENT='targets from SMSG_SPELL_UPDATE_CHAIN_TARGETS';

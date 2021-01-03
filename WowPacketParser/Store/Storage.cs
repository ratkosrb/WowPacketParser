using System;
using System.Collections.Generic;
using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Store.Objects;
using System.Linq;

namespace WowPacketParser.Store
{
    public static class Storage
    {
        // Stores opcodes read, npc/GOs/spell/item/etc IDs found in sniffs
        // and other miscellaneous stuff
        public static readonly DataBag<SniffData> SniffData = new DataBag<SniffData>(Settings.SqlTables.SniffData || Settings.SqlTables.SniffDataOpcodes);

        /* Key: Guid */

        // Units, GameObjects, Players, Items
        public static readonly StoreDictionary<WowGuid, WoWObject> Objects = new StoreDictionary<WowGuid, WoWObject>(new List<SQLOutput>());
        public static void StoreNewObject(WowGuid guid, WoWObject obj, Packet packet)
        {
            obj.OriginalMovement = obj.Movement != null ? obj.Movement.CopyFromMe() : null;
            obj.OriginalUpdateFields = obj.UpdateFields != null ? new Dictionary<int, UpdateField>(obj.UpdateFields) : null;
            if (!string.IsNullOrWhiteSpace(Settings.SQLFileName) && Settings.DumpFormatWithSQL())
                obj.SourceSniffId = Program.sniffFileNames.IndexOf(packet.FileName);
            obj.SourceSniffBuild = ClientVersion.BuildInt;
            Storage.Objects.Add(guid, obj, packet.TimeSpan);
        }
        public static string GetObjectDbGuid(WowGuid guid)
        {
            if (Objects.ContainsKey(guid))
            {
                if (guid.GetObjectType() == ObjectType.Unit)
                {
                    Unit creature = Objects[guid].Item1 as Unit;
                    if (creature != null)
                        return "@CGUID+" + creature.DbGuid;
                }
                else if (guid.GetObjectType() == ObjectType.GameObject)
                {
                    GameObject gameobject = Objects[guid].Item1 as GameObject;
                    if (gameobject != null)
                        return "@OGUID+" + gameobject.DbGuid;
                }
                else if (guid.GetObjectType() == ObjectType.Player ||
                         guid.GetObjectType() == ObjectType.ActivePlayer)
                {
                    Player player = Objects[guid].Item1 as Player;
                    if (player != null)
                        return "@PGUID+" + player.DbGuid;
                }
            }
            return "0";
        }
        public static string GetObjectTypeNameForDB(WowGuid guid)
        {
            if (guid.IsEmpty())
                return "";

            if (guid.GetObjectType() == ObjectType.Unit)
            {
                if (guid.GetHighType() == HighGuidType.Pet)
                    return "Pet";
                else
                    return "Creature";
            }
            else if (guid.GetObjectType() == ObjectType.Player ||
                     guid.GetObjectType() == ObjectType.ActivePlayer)
            {
                return "Player";
            }

            return guid.GetObjectType().ToString();
        }
        public static void GetObjectDbGuidEntryType(WowGuid guid, out string objectGuid, out uint objectEntry, out string objectType)
        {
            if (guid == null || guid.IsEmpty())
            {
                objectGuid = "0";
                objectEntry = 0;
                objectType = "";
                return;
            }

            objectType = GetObjectTypeNameForDB(guid);

            if (Objects.ContainsKey(guid))
            {
                if (guid.GetObjectType() == ObjectType.Unit)
                {
                    Unit creature = Objects[guid].Item1 as Unit;
                    if (creature != null)
                        objectGuid = "@CGUID+" + creature.DbGuid;
                    else
                        objectGuid = "0";

                    objectEntry = guid.GetEntry();

                    return;
                }
                else if (guid.GetObjectType() == ObjectType.GameObject)
                {
                    GameObject gameobject = Objects[guid].Item1 as GameObject;
                    if (gameobject != null)
                        objectGuid = "@OGUID+" + gameobject.DbGuid;
                    else
                        objectGuid = "0";

                    objectEntry = guid.GetEntry();

                    return;
                }
                else if (guid.GetObjectType() == ObjectType.Player ||
                         guid.GetObjectType() == ObjectType.ActivePlayer)
                {
                    Player player = Objects[guid].Item1 as Player;
                    if (player != null)
                        objectGuid = "@PGUID+" + player.DbGuid;
                    else
                        objectGuid = "0";

                    objectEntry = 0;

                    return;
                }
                else if (guid.GetObjectType() == ObjectType.Item)
                {
                    objectGuid = "0";
                    objectEntry = (uint)Objects[guid].Item1.ObjectData.EntryID;

                    return;
                }
            }
            objectGuid = "0";
            objectEntry = guid.GetEntry();
        }
        public static uint GetObjectEntry(WowGuid guid)
        {
            if (guid.HasEntry())
                return guid.GetEntry();

            if (Objects.ContainsKey(guid))
                return (uint)Objects[guid].Item1.ObjectData.EntryID;

            return 0;
        }

        public static readonly Dictionary<WowGuid, List<DateTime>> ObjectDestroyTimes = new Dictionary<WowGuid, List<DateTime>>();
        public static void StoreObjectDestroyTime(WowGuid guid, DateTime time)
        {
            if (guid.GetObjectType() != ObjectType.Unit &&
                guid.GetObjectType() != ObjectType.GameObject &&
                guid.GetObjectType() != ObjectType.DynamicObject &&
                guid.GetObjectType() != ObjectType.Player &&
                guid.GetObjectType() != ObjectType.ActivePlayer)
                return;

            if (guid.GetObjectType() == ObjectType.Unit && !Settings.SqlTables.creature_destroy_time)
                return;

            if (guid.GetObjectType() == ObjectType.GameObject && !Settings.SqlTables.gameobject_destroy_time)
                return;

            if (guid.GetObjectType() == ObjectType.DynamicObject && !Settings.SqlTables.dynamicobject_destroy_time)
                return;

            if (guid.GetObjectType() == ObjectType.Player && !Settings.SqlTables.player_destroy_time)
                return;

            if (guid.GetObjectType() == ObjectType.ActivePlayer && !Settings.SqlTables.player_destroy_time)
                return;

            if (Storage.ObjectDestroyTimes.ContainsKey(guid))
            {
                Storage.ObjectDestroyTimes[guid].Add(time);
            }
            else
            {
                List<DateTime> timeList = new List<DateTime>();
                timeList.Add(time);
                Storage.ObjectDestroyTimes.Add(guid, timeList);
            }
        }
        public static readonly Dictionary<WowGuid, List<ObjectCreate>> ObjectCreate1Times = new Dictionary<WowGuid, List<ObjectCreate>>();
        public static void StoreObjectCreate1Time(WowGuid guid, MovementInfo movement, DateTime time)
        {
            if (guid.GetObjectType() != ObjectType.Unit &&
                guid.GetObjectType() != ObjectType.GameObject &&
                guid.GetObjectType() != ObjectType.DynamicObject &&
                guid.GetObjectType() != ObjectType.Player &&
                guid.GetObjectType() != ObjectType.ActivePlayer)
                return;

            if (guid.GetObjectType() == ObjectType.Unit && !Settings.SqlTables.creature_create1_time)
                return;

            if (guid.GetObjectType() == ObjectType.GameObject && !Settings.SqlTables.gameobject_create1_time)
                return;

            if (guid.GetObjectType() == ObjectType.DynamicObject && !Settings.SqlTables.dynamicobject_create1_time)
                return;

            if (guid.GetObjectType() == ObjectType.Player && !Settings.SqlTables.player_create1_time)
                return;

            if (guid.GetObjectType() == ObjectType.ActivePlayer && !Settings.SqlTables.player_create1_time)
                return;

            if (Storage.ObjectCreate1Times.ContainsKey(guid))
            {
                ObjectCreate createData = new ObjectCreate();
                createData.UnixTimeMs = (ulong)Utilities.GetUnixTimeMsFromDateTime(time);
                if (movement != null)
                {
                    createData.PositionX = movement.Position.X;
                    createData.PositionY = movement.Position.Y;
                    createData.PositionZ = movement.Position.Z;
                    createData.Orientation = movement.Orientation;
                }
                Storage.ObjectCreate1Times[guid].Add(createData);
            }
            else
            {
                List<ObjectCreate> createList = new List<ObjectCreate>();
                ObjectCreate createData = new ObjectCreate();
                createData.UnixTimeMs = (ulong)Utilities.GetUnixTimeMsFromDateTime(time);
                if (movement != null)
                {
                    createData.PositionX = movement.Position.X;
                    createData.PositionY = movement.Position.Y;
                    createData.PositionZ = movement.Position.Z;
                    createData.Orientation = movement.Orientation;
                }
                createList.Add(createData);
                Storage.ObjectCreate1Times.Add(guid, createList);
            }
        }
        public static readonly Dictionary<WowGuid, List<ObjectCreate>> ObjectCreate2Times = new Dictionary<WowGuid, List<ObjectCreate>>();
        public static void StoreObjectCreate2Time(WowGuid guid, MovementInfo movement, DateTime time)
        {
            if (guid.GetObjectType() != ObjectType.Unit &&
                guid.GetObjectType() != ObjectType.GameObject &&
                guid.GetObjectType() != ObjectType.DynamicObject &&
                guid.GetObjectType() != ObjectType.Player &&
                guid.GetObjectType() != ObjectType.ActivePlayer)
                return;

            if (guid.GetObjectType() == ObjectType.Unit && !Settings.SqlTables.creature_create2_time)
                return;

            if (guid.GetObjectType() == ObjectType.GameObject && !Settings.SqlTables.gameobject_create2_time)
                return;

            if (guid.GetObjectType() == ObjectType.DynamicObject && !Settings.SqlTables.dynamicobject_create2_time)
                return;

            if (guid.GetObjectType() == ObjectType.Player && !Settings.SqlTables.player_create2_time)
                return;

            if (guid.GetObjectType() == ObjectType.ActivePlayer && !Settings.SqlTables.player_create2_time)
                return;

            if (Storage.ObjectCreate2Times.ContainsKey(guid))
            {
                ObjectCreate createData = new ObjectCreate();
                createData.UnixTimeMs = (ulong)Utilities.GetUnixTimeMsFromDateTime(time);
                if (movement != null)
                {
                    createData.PositionX = movement.Position.X;
                    createData.PositionY = movement.Position.Y;
                    createData.PositionZ = movement.Position.Z;
                    createData.Orientation = movement.Orientation;
                }
                Storage.ObjectCreate2Times[guid].Add(createData);
            }
            else
            {
                List<ObjectCreate> createList = new List<ObjectCreate>();
                ObjectCreate createData = new ObjectCreate();
                createData.UnixTimeMs = (ulong)Utilities.GetUnixTimeMsFromDateTime(time);
                if (movement != null)
                {
                    createData.PositionX = movement.Position.X;
                    createData.PositionY = movement.Position.Y;
                    createData.PositionZ = movement.Position.Z;
                    createData.Orientation = movement.Orientation;
                }
                createList.Add(createData);
                Storage.ObjectCreate2Times.Add(guid, createList);
            }
        }
        public static void StoreObjectCreateTime(WowGuid guid, MovementInfo movement, DateTime time, ObjectCreateType type)
        {
            if (type == ObjectCreateType.Create1)
                StoreObjectCreate1Time(guid, movement, time);
            else if (type == ObjectCreateType.Create2)
                StoreObjectCreate2Time(guid, movement, time);

        }
        public static readonly Dictionary<WowGuid, List<Tuple<List<Aura>, DateTime>>> UnitAurasUpdates = new Dictionary<WowGuid, List<Tuple<List<Aura>, DateTime>>>();
        public static void StoreUnitAurasUpdate(WowGuid guid, List<Aura> auras, DateTime time)
        {
            if (Storage.Objects.ContainsKey(guid))
            {
                var unit = Storage.Objects[guid].Item1 as Unit;
                if (unit != null)
                {
                    // If this is the first packet that sends auras
                    // (hopefully at spawn time) add it to the "Auras" field
                    if (unit.Auras == null)
                        unit.Auras = auras;
                }
            }

            if (guid.GetObjectType() == ObjectType.Unit)
            {
                if (!Settings.SqlTables.creature_auras_update)
                    return;
            }
            else if (guid.GetObjectType() == ObjectType.Player ||
                     guid.GetObjectType() == ObjectType.ActivePlayer)
            {
                if (!Settings.SqlTables.player_auras_update)
                    return;
            }
            else
                return;

            if (Storage.UnitAurasUpdates.ContainsKey(guid))
            {
                Storage.UnitAurasUpdates[guid].Add(new Tuple<List<Aura>, DateTime>(auras, time));
            }
            else
            {
                List<Tuple<List<Aura>, DateTime>> updateList = new List<Tuple<List<Aura>, DateTime>>();
                updateList.Add(new Tuple<List<Aura>, DateTime>(auras, time));
                Storage.UnitAurasUpdates.Add(guid, updateList);
            }
        }
        public static readonly Dictionary<WowGuid, List<CreatureGuidValuesUpdate>> UnitGuidValuesUpdates = new Dictionary<WowGuid, List<CreatureGuidValuesUpdate>>();
        public static void StoreUnitGuidValuesUpdate(WowGuid guid, CreatureGuidValuesUpdate update)
        {
            if (guid.GetObjectType() == ObjectType.Unit)
            {
                if (!Settings.SqlTables.creature_guid_values_update)
                    return;
            }
            else if (guid.GetObjectType() == ObjectType.Player ||
                     guid.GetObjectType() == ObjectType.ActivePlayer)
            {
                if (!Settings.SqlTables.player_guid_values_update)
                    return;
            }
            else
                return;

            if (Storage.UnitGuidValuesUpdates.ContainsKey(guid))
            {
                Storage.UnitGuidValuesUpdates[guid].Add(update);
            }
            else
            {
                List<CreatureGuidValuesUpdate> updateList = new List<CreatureGuidValuesUpdate>();
                updateList.Add(update);
                Storage.UnitGuidValuesUpdates.Add(guid, updateList);
            }
        }
        public static readonly Dictionary<WowGuid, List<CreatureEquipmentValuesUpdate>> UnitEquipmentValuesUpdates = new Dictionary<WowGuid, List<CreatureEquipmentValuesUpdate>>();
        public static void StoreUnitEquipmentValuesUpdate(WowGuid guid, CreatureEquipmentValuesUpdate update)
        {
            if (guid.GetObjectType() == ObjectType.Unit)
            {
                if (!Settings.SqlTables.creature_equipment_values_update)
                    return;
            }
            else if (guid.GetObjectType() == ObjectType.Player ||
                     guid.GetObjectType() == ObjectType.ActivePlayer)
            {
                if (!Settings.SqlTables.player_equipment_values_update)
                    return;
            }
            else
                return;

            if (Storage.UnitEquipmentValuesUpdates.ContainsKey(guid))
            {
                Storage.UnitEquipmentValuesUpdates[guid].Add(update);
            }
            else
            {
                List<CreatureEquipmentValuesUpdate> updateList = new List<CreatureEquipmentValuesUpdate>();
                updateList.Add(update);
                Storage.UnitEquipmentValuesUpdates.Add(guid, updateList);
            }
        }
        public static readonly Dictionary<WowGuid, List<CreatureValuesUpdate>> UnitValuesUpdates = new Dictionary<WowGuid, List<CreatureValuesUpdate>>();
        public static void StoreUnitValuesUpdate(WowGuid guid, CreatureValuesUpdate update)
        {
            if (guid.GetObjectType() == ObjectType.Unit)
            {
                if (!Settings.SqlTables.creature_values_update)
                    return;
            }
            else if (guid.GetObjectType() == ObjectType.Player ||
                     guid.GetObjectType() == ObjectType.ActivePlayer)
            {
                if (!Settings.SqlTables.player_values_update)
                    return;
            }
            else
                return;

            if (Storage.UnitValuesUpdates.ContainsKey(guid))
            {
                Storage.UnitValuesUpdates[guid].Add(update);
            }
            else
            {
                List<CreatureValuesUpdate> updateList = new List<CreatureValuesUpdate>();
                updateList.Add(update);
                Storage.UnitValuesUpdates.Add(guid, updateList);
            }
        }
        public static readonly Dictionary<WowGuid, List<CreatureSpeedUpdate>> UnitSpeedUpdates = new Dictionary<WowGuid, List<CreatureSpeedUpdate>>();
        public static void StoreUnitSpeedUpdate(WowGuid guid, CreatureSpeedUpdate update)
        {
            if (guid.GetObjectType() == ObjectType.Unit)
            {
                if (!Settings.SqlTables.creature_speed_update)
                    return;
            }
            else if (guid.GetObjectType() == ObjectType.Player ||
                     guid.GetObjectType() == ObjectType.ActivePlayer)
            {
                if (!Settings.SqlTables.player_speed_update)
                    return;
            }
            else
                return;

            if (Storage.UnitSpeedUpdates.ContainsKey(guid))
            {
                Storage.UnitSpeedUpdates[guid].Add(update);
            }
            else
            {
                List<CreatureSpeedUpdate> updateList = new List<CreatureSpeedUpdate>();
                updateList.Add(update);
                Storage.UnitSpeedUpdates.Add(guid, updateList);
            }
        }
        public static readonly Dictionary<WowGuid, List<GameObjectUpdate>> GameObjectUpdates = new Dictionary<WowGuid, List<GameObjectUpdate>>();
        public static void StoreGameObjectUpdate(WowGuid guid, GameObjectUpdate update)
        {
            if (!Settings.SqlTables.gameobject_values_update)
                return;

            if (guid.GetObjectType() != ObjectType.GameObject)
                return;

            if (Storage.GameObjectUpdates.ContainsKey(guid))
            {
                Storage.GameObjectUpdates[guid].Add(update);
            }
            else
            {
                List<GameObjectUpdate> updateList = new List<GameObjectUpdate>();
                updateList.Add(update);
                Storage.GameObjectUpdates.Add(guid, updateList);
            }
        }
        public static readonly Dictionary<WowGuid, List<GameObjectCustomAnim>> GameObjectCustomAnims = new Dictionary<WowGuid, List<GameObjectCustomAnim>>();
        public static void StoreGameObjectCustomAnim(WowGuid guid, GameObjectCustomAnim animData)
        {
            if (!Settings.SqlTables.gameobject_custom_anim)
                return;

            if (guid.GetObjectType() != ObjectType.GameObject)
                return;

            if (Storage.GameObjectCustomAnims.ContainsKey(guid))
            {
                Storage.GameObjectCustomAnims[guid].Add(animData);
            }
            else
            {
                List<GameObjectCustomAnim> animList = new List<GameObjectCustomAnim>();
                animList.Add(animData);
                Storage.GameObjectCustomAnims.Add(guid, animList);
            }
        }
        public static readonly Dictionary<WowGuid, List<DateTime>> GameObjectDespawnAnims = new Dictionary<WowGuid, List<DateTime>>();
        public static void StoreGameObjectDespawnAnim(WowGuid guid, DateTime time)
        {
            if (!Settings.SqlTables.gameobject_despawn_anim)
                return;

            if (Storage.GameObjectDespawnAnims.ContainsKey(guid))
            {
                Storage.GameObjectDespawnAnims[guid].Add(time);
            }
            else
            {
                List<DateTime> timeList = new List<DateTime>();
                timeList.Add(time);
                Storage.GameObjectDespawnAnims.Add(guid, timeList);
            }
        }
        public static readonly Dictionary<WowGuid, List<DateTime>> GameObjectClientUseTimes = new Dictionary<WowGuid, List<DateTime>>();
        public static void StoreGameObjectUse(WowGuid guid, DateTime time)
        {
            if (!Settings.SqlTables.client_gameobject_use)
                return;

            if (Storage.GameObjectClientUseTimes.ContainsKey(guid))
            {
                Storage.GameObjectClientUseTimes[guid].Add(time);
            }
            else
            {
                List<DateTime> usesList = new List<DateTime>();
                usesList.Add(time);
                Storage.GameObjectClientUseTimes.Add(guid, usesList);
            }
        }
        public static readonly Dictionary<WowGuid, List<DateTime>> CreatureClientInteractTimes = new Dictionary<WowGuid, List<DateTime>>();
        public static void StoreCreatureInteract(WowGuid guid, DateTime time)
        {
            if (!Settings.SqlTables.client_creature_interact)
                return;

            if (Storage.CreatureClientInteractTimes.ContainsKey(guid))
            {
                Storage.CreatureClientInteractTimes[guid].Add(time);
            }
            else
            {
                List<DateTime> usesList = new List<DateTime>();
                usesList.Add(time);
                Storage.CreatureClientInteractTimes.Add(guid, usesList);
            }
        }
        public static readonly Dictionary<WowGuid, List<CreatureEmote>> Emotes = new Dictionary<WowGuid, List<CreatureEmote>>();
        public static void StoreUnitEmote(WowGuid guid, EmoteType emote, DateTime time)
        {
            if (guid.GetObjectType() == ObjectType.Unit)
            {
                if (!Settings.SqlTables.creature_emote)
                    return;
            }
            else if (guid.GetObjectType() == ObjectType.Player ||
                     guid.GetObjectType() == ObjectType.ActivePlayer)
            {
                if (!Settings.SqlTables.player_emote)
                    return;
            }
            else
                return;

            if (Storage.Emotes.ContainsKey(guid))
            {
                Storage.Emotes[guid].Add(new CreatureEmote(emote, time));
            }
            else
            {
                List<CreatureEmote> emotesList = new List<CreatureEmote>();
                emotesList.Add(new CreatureEmote(emote, time));
                Storage.Emotes.Add(guid, emotesList);
            }
        }
        public static readonly Dictionary<WowGuid, List<UnitMeleeAttackLog>> UnitAttackLogs = new Dictionary<WowGuid, List<UnitMeleeAttackLog>>();
        public static void StoreUnitAttackLog(UnitMeleeAttackLog attackData)
        {
            WowGuid attackerGuid = attackData.Attacker;

            if (attackerGuid.GetObjectType() == ObjectType.Unit)
            {
                if (!Settings.SqlTables.creature_attack_log)
                    return;
            }
            else if (attackerGuid.GetObjectType() == ObjectType.Player ||
                     attackerGuid.GetObjectType() == ObjectType.ActivePlayer)
            {
                if (!Settings.SqlTables.player_attack_log)
                    return;
            }
            else
                return;

            if (Storage.UnitAttackLogs.ContainsKey(attackerGuid))
            {
                Storage.UnitAttackLogs[attackerGuid].Add(attackData);
            }
            else
            {
                List<UnitMeleeAttackLog> attacksList = new List<UnitMeleeAttackLog>();
                attacksList.Add(attackData);
                Storage.UnitAttackLogs.Add(attackerGuid, attacksList);
            }
        }
        public static readonly Dictionary<WowGuid, List<CreatureAttackData>> UnitAttackStartTimes = new Dictionary<WowGuid, List<CreatureAttackData>>();
        public static readonly Dictionary<WowGuid, List<CreatureAttackData>> UnitAttackStopTimes = new Dictionary<WowGuid, List<CreatureAttackData>>();
        public static void StoreUnitAttackToggle(WowGuid attackerGuid, WowGuid victimGuid, DateTime time, bool start)
        {
            Dictionary<WowGuid, List<CreatureAttackData>> store = null;
            if (start)
            {
                if (attackerGuid.GetObjectType() == ObjectType.Unit &&
                    !Settings.SqlTables.creature_attack_start)
                    return;
                else if ((attackerGuid.GetObjectType() == ObjectType.Player || attackerGuid.GetObjectType() == ObjectType.ActivePlayer) &&
                         !Settings.SqlTables.player_attack_start)
                    return;

                store = UnitAttackStartTimes;
            }
            else
            {
                if (attackerGuid.GetObjectType() == ObjectType.Unit &&
                    !Settings.SqlTables.creature_attack_stop)
                    return;
                else if ((attackerGuid.GetObjectType() == ObjectType.Player || attackerGuid.GetObjectType() == ObjectType.ActivePlayer) &&
                         !Settings.SqlTables.player_attack_stop)
                    return;

                store = UnitAttackStopTimes;
            }

            if (store.ContainsKey(attackerGuid))
            {
                store[attackerGuid].Add(new CreatureAttackData(victimGuid, time));
            }
            else
            {
                List<CreatureAttackData> attackList = new List<CreatureAttackData>();
                attackList.Add(new CreatureAttackData(victimGuid, time));
                store.Add(attackerGuid, attackList);
            }
        }
        public static readonly List<PlayerMovement> PlayerMovements = new List<PlayerMovement>();
        public static readonly List<ActivePlayerCreateTime> PlayerActiveCreateTime = new List<ActivePlayerCreateTime>();

        /* Key: Entry */

        // Templates
        public static readonly DataBag<AreaTriggerTemplate> AreaTriggerTemplates = new DataBag<AreaTriggerTemplate>(Settings.SqlTables.areatrigger_template );
        public static readonly DataBag<AreaTriggerTemplateVertices> AreaTriggerTemplatesVertices = new DataBag<AreaTriggerTemplateVertices>(Settings.SqlTables.areatrigger_template_polygon_vertices);
        public static readonly DataBag<ConversationActor> ConversationActors = new DataBag<ConversationActor>(Settings.SqlTables.conversation_actors);
        public static readonly DataBag<ConversationActorTemplate> ConversationActorTemplates = new DataBag<ConversationActorTemplate>(Settings.SqlTables.conversation_actor_template);
        public static readonly DataBag<ConversationLineTemplate> ConversationLineTemplates = new DataBag<ConversationLineTemplate>(Settings.SqlTables.conversation_line_template);
        public static readonly DataBag<ConversationTemplate> ConversationTemplates = new DataBag<ConversationTemplate>(Settings.SqlTables.conversation_template);
        public static readonly DataBag<GameObjectTemplate> GameObjectTemplates = new DataBag<GameObjectTemplate>(Settings.SqlTables.gameobject_template);
        public static readonly DataBag<GameObjectTemplateQuestItem> GameObjectTemplateQuestItems = new DataBag<GameObjectTemplateQuestItem>(Settings.SqlTables.gameobject_template);
        public static readonly DataBag<ItemClientUse> ItemClientUseTimes = new DataBag<ItemClientUse>(Settings.SqlTables.client_item_use);
        public static readonly DataBag<ItemTemplate> ItemTemplates = new DataBag<ItemTemplate>(Settings.SqlTables.item_template);
        public static readonly DataBag<QuestTemplate> QuestTemplates = new DataBag<QuestTemplate>(Settings.SqlTables.quest_template);
        public static readonly DataBag<QuestObjective> QuestObjectives = new DataBag<QuestObjective>(Settings.SqlTables.quest_template);
        public static readonly DataBag<QuestVisualEffect> QuestVisualEffects = new DataBag<QuestVisualEffect>(Settings.SqlTables.quest_template);
        public static readonly DataBag<QuestRewardDisplaySpell> QuestRewardDisplaySpells = new DataBag<QuestRewardDisplaySpell>(Settings.SqlTables.quest_template);
        public static readonly DataBag<CreatureTemplate> CreatureTemplates = new DataBag<CreatureTemplate>(Settings.SqlTables.creature_template_wdb);
        public static readonly DataBag<CreatureTemplateClassic> CreatureTemplatesClassic = new DataBag<CreatureTemplateClassic>(Settings.SqlTables.creature_template_wdb);
        public static readonly DataBag<CreatureTemplateNonWDB> CreatureTemplatesNonWDB = new DataBag<CreatureTemplateNonWDB>(Settings.SqlTables.creature_template);
        public static readonly DataBag<CreatureTemplateQuestItem> CreatureTemplateQuestItems = new DataBag<CreatureTemplateQuestItem>(Settings.SqlTables.creature_template_wdb);
        public static readonly DataBag<CreatureTemplateScaling> CreatureTemplateScalings = new DataBag<CreatureTemplateScaling>(Settings.SqlTables.creature_template_scaling);
        public static readonly DataBag<CreatureTemplateModel> CreatureTemplateModels = new DataBag<CreatureTemplateModel>(Settings.SqlTables.creature_template);
        public static readonly DataBag<CreatureStats> CreatureStats = new DataBag<CreatureStats>(Settings.SqlTables.creature_stats);

        // Vendor & trainer
        public static readonly DataBag<NpcTrainer> NpcTrainers = new DataBag<NpcTrainer>(Settings.SqlTables.npc_trainer); // legacy 3.3.5 support
        public static readonly DataBag<NpcVendor> NpcVendors = new DataBag<NpcVendor>(Settings.SqlTables.npc_vendor);
        public static readonly DataBag<Trainer> Trainers = new DataBag<Trainer>(Settings.SqlTables.trainer);
        public static readonly DataBag<TrainerSpell> TrainerSpells = new DataBag<TrainerSpell>(Settings.SqlTables.trainer);
        public static readonly DataBag<CreatureTrainer> CreatureTrainers = new DataBag<CreatureTrainer>(Settings.SqlTables.trainer);

        // Loot
        public static readonly Dictionary<uint, Dictionary<WowGuid, LootEntry>> CreatureLoot = new Dictionary<uint, Dictionary<WowGuid, LootEntry>>();
        public static readonly Dictionary<uint, Dictionary<WowGuid, LootEntry>> GameObjectLoot = new Dictionary<uint, Dictionary<WowGuid, LootEntry>>();
        public static void StoreLoot(LootEntry loot, WowGuid objectGuid, WowGuid lootGuid)
        {
            Dictionary<uint, Dictionary<WowGuid, LootEntry>> lootStorage = null;
            if (objectGuid.GetObjectType() == ObjectType.Unit)
            {
                if (!Settings.SqlTables.creature_loot)
                    return;

                lootStorage = CreatureLoot;
            }
            else if (objectGuid.GetObjectType() == ObjectType.GameObject)
            {
                if (!Settings.SqlTables.gameobject_loot)
                    return;

                lootStorage = GameObjectLoot;
            }
            if (lootStorage == null)
                return;

            if (lootStorage.ContainsKey(loot.Entry))
            {
                if (lootStorage[loot.Entry].ContainsKey(lootGuid))
                    return;

                loot.LootId = LootEntry.LootIdCounter++;
                foreach (LootItem item in loot.ItemsList)
                    item.LootId = loot.LootId;

                lootStorage[loot.Entry].Add(lootGuid, loot);
            }
            else
            {
                loot.LootId = LootEntry.LootIdCounter++;
                foreach (LootItem item in loot.ItemsList)
                    item.LootId = loot.LootId;

                Dictionary<WowGuid, LootEntry> dict = new Dictionary<WowGuid, LootEntry>();
                dict.Add(lootGuid, loot);
                lootStorage.Add(loot.Entry, dict);
            }
        }

        // Page & npc text
        public static readonly DataBag<PageText> PageTexts = new DataBag<PageText>(Settings.SqlTables.page_text);
        public static readonly DataBag<NpcText> NpcTexts = new DataBag<NpcText>(Settings.SqlTables.npc_text);
        public static readonly DataBag<NpcTextMop> NpcTextsMop = new DataBag<NpcTextMop>(Settings.SqlTables.npc_text);

        // Chat packet data (says, yells, etc.)
        public static readonly DataBag<WorldText> WorldTexts = new DataBag<WorldText>(Settings.SqlTables.world_text);
        public static readonly DataBag<CreatureText> CreatureTexts = new DataBag<CreatureText>(Settings.SqlTables.creature_text);
        public static readonly StoreMulti<uint, CreatureTextTemplate> CreatureTextTemplates = new StoreMulti<uint, CreatureTextTemplate>(Settings.SqlTables.creature_text_template);
        public static readonly DataBag<GameObjectText> GameObjectTexts = new DataBag<GameObjectText>(Settings.SqlTables.gameobject_text);
        public static readonly StoreMulti<uint, GameObjectTextTemplate> GameObjectTextTemplates = new StoreMulti<uint, GameObjectTextTemplate>(Settings.SqlTables.gameobject_text_template);
        public static readonly DataBag<CharacterChat> CharacterTexts = new DataBag<CharacterChat>(Settings.SqlTables.player_chat);

        public static void StoreText(ChatPacketData text, Packet packet)
        {
            uint creatureId = 0;
            uint gameObjectId = 0;
            if (text.SenderGUID.GetObjectType() == ObjectType.Unit)
                creatureId = text.SenderGUID.GetEntry();
            else if (text.ReceiverGUID != null && text.ReceiverGUID.GetObjectType() == ObjectType.Unit)
                creatureId = text.ReceiverGUID.GetEntry();
            else if (text.SenderGUID.GetObjectType() == ObjectType.GameObject)
                gameObjectId = text.SenderGUID.GetEntry();
            else if (text.ReceiverGUID != null && text.ReceiverGUID.GetObjectType() == ObjectType.GameObject)
                gameObjectId = text.ReceiverGUID.GetEntry();

            text.Time = packet.Time;

            if (creatureId != 0)
            {
                if (Settings.SqlTables.creature_text_template)
                {
                    CreatureTextTemplate textTemplate = new CreatureTextTemplate(text);
                    textTemplate.Entry = creatureId;
                    Storage.CreatureTextTemplates.Add(creatureId, textTemplate, packet.TimeSpan);

                    if (Settings.SqlTables.creature_text)
                    {
                        CreatureText textEntry = new CreatureText();
                        textEntry.Entry = creatureId;
                        textEntry.Text = textTemplate.Text;
                        textEntry.UnixTimeMs = (ulong)Utilities.GetUnixTimeMsFromDateTime(packet.Time);
                        textEntry.SenderGUID = textTemplate.SenderGUID;
                        if (Storage.Objects.ContainsKey(textTemplate.SenderGUID))
                        {
                            var obj = Storage.Objects[textTemplate.SenderGUID].Item1 as Unit;
                            textEntry.HealthPercent = obj.UnitData.HealthPercent;
                        }
                        Storage.CreatureTexts.Add(textEntry);
                    }
                }
            }
            else if (gameObjectId != 0)
            {
                if (Settings.SqlTables.gameobject_text_template)
                {
                    GameObjectTextTemplate textTemplate = new GameObjectTextTemplate(text);
                    textTemplate.Entry = gameObjectId;
                    Storage.GameObjectTextTemplates.Add(gameObjectId, textTemplate, packet.TimeSpan);

                    if (Settings.SqlTables.gameobject_text)
                    {
                        GameObjectText textEntry = new GameObjectText();
                        textEntry.Entry = gameObjectId;
                        textEntry.Text = textTemplate.Text;
                        textEntry.UnixTimeMs = (ulong)Utilities.GetUnixTimeMsFromDateTime(packet.Time);
                        textEntry.SenderGUID = textTemplate.SenderGUID;
                        Storage.GameObjectTexts.Add(textEntry);
                    }
                }
            }
            else if (((text.SenderGUID.GetObjectType() == ObjectType.Player) || (text.SenderName != null && text.Type == ChatMessageType.Channel)) &&
                     (text.Language != Language.Addon && text.Language != Language.AddonBfA && text.Language != Language.AddonLogged))
            {
                if (Settings.SqlTables.player_chat)
                {
                    var textEntry = new CharacterChat
                    {
                        SenderGUID = text.SenderGUID,
                        SenderName = text.SenderName,
                        Text = text.Text,
                        Type = text.Type,
                        ChannelName = text.ChannelName,
                        UnixTimeMs = (ulong)Utilities.GetUnixTimeMsFromDateTime(packet.Time)
                    };
                    Storage.CharacterTexts.Add(textEntry);
                }
            }
            else if (text.SenderGUID.IsEmpty() && (text.Type == ChatMessageType.BattlegroundNeutral))
            {
                if (Settings.SqlTables.world_text)
                {
                    var worldText = new WorldText
                    {
                        UnixTimeMs = (ulong)Utilities.GetUnixTimeMsFromDateTime(packet.Time),
                        Type = text.Type,
                        Language = text.Language,
                        Text = text.Text
                    };
                    Storage.WorldTexts.Add(worldText);
                }
            }
        }
        // Points of Interest
        public static readonly DataBag<PointsOfInterest> GossipPOIs = new DataBag<PointsOfInterest>(Settings.SqlTables.points_of_interest);

        // "Helper" stores, do not match a specific table
        public static readonly List<ObjectSound> Sounds = new List<ObjectSound>();
        public static readonly DataBag<PlayMusic> Music = new DataBag<PlayMusic>(Settings.SqlTables.play_music);
        public static readonly StoreDictionary<uint, List<uint?>> SpellsX = new StoreDictionary<uint, List<uint?>>(Settings.SqlTables.creature_template); // `creature_template`.`spellsX`
        public static readonly DataBag<QuestOfferReward> QuestOfferRewards = new DataBag<QuestOfferReward>(Settings.SqlTables.quest_template);
        public static readonly StoreDictionary<Tuple<uint, uint>, object> GossipSelects = new StoreDictionary<Tuple<uint, uint>, object>(Settings.SqlTables.points_of_interest || Settings.SqlTables.gossip_menu || Settings.SqlTables.gossip_menu_option);

        /* Key: Misc */

        // Start info (Race, Class)
        public static readonly DataBag<PlayerCreateInfoAction> StartActions = new DataBag<PlayerCreateInfoAction>(Settings.SqlTables.playercreateinfo_action);
        public static readonly DataBag<PlayerCreateInfo> StartPositions = new DataBag<PlayerCreateInfo>(Settings.SqlTables.playercreateinfo);

        // Gossips (MenuId, TextId)
        public static readonly Dictionary<uint, uint> CreatureDefaultGossips = new Dictionary<uint, uint>();
        public static readonly DataBag<CreatureGossip> CreatureGossips = new DataBag<CreatureGossip>(Settings.SqlTables.creature_gossip);
        public static readonly DataBag<GossipMenu> Gossips = new DataBag<GossipMenu>(Settings.SqlTables.gossip_menu);
        public static readonly DataBag<GossipMenuOption> GossipMenuOptions = new DataBag<GossipMenuOption>(Settings.SqlTables.gossip_menu_option);
        public static readonly DataBag<GossipMenuOptionAction> GossipMenuOptionActions = new DataBag<GossipMenuOptionAction>(Settings.SqlTables.gossip_menu_option);
        public static readonly DataBag<GossipMenuOptionBox> GossipMenuOptionBoxes = new DataBag<GossipMenuOptionBox>(Settings.SqlTables.gossip_menu_option);

        // Quest POI (QuestId, Id)
        public static readonly DataBag<QuestPOI> QuestPOIs = new DataBag<QuestPOI>(Settings.SqlTables.quest_poi_points);
        public static readonly DataBag<QuestPOIPoint> QuestPOIPoints = new DataBag<QuestPOIPoint>(Settings.SqlTables.quest_poi_points); // WoD

        // Quest Misc
        public static readonly DataBag<QuestStarter> QuestStarters = new DataBag<QuestStarter>(Settings.SqlTables.quest_starter);
        public static readonly DataBag<QuestEnder> QuestEnders = new DataBag<QuestEnder>(Settings.SqlTables.quest_ender);
        public static readonly DataBag<QuestClientAccept> QuestClientAcceptTimes = new DataBag<QuestClientAccept>(Settings.SqlTables.client_quest_accept);
        public static readonly DataBag<QuestClientComplete> QuestClientCompleteTimes = new DataBag<QuestClientComplete>(Settings.SqlTables.client_quest_complete);
        public static readonly DataBag<QuestCompleteTime> QuestCompleteTimes = new DataBag<QuestCompleteTime>(Settings.SqlTables.quest_update_complete);
        public static readonly DataBag<QuestFailTime> QuestFailTimes = new DataBag<QuestFailTime>(Settings.SqlTables.quest_update_failed);
        public static readonly DataBag<QuestGreeting> QuestGreetings = new DataBag<QuestGreeting>(Settings.SqlTables.quest_template);
        public static readonly DataBag<QuestDetails> QuestDetails = new DataBag<QuestDetails>(Settings.SqlTables.quest_template);
        public static readonly DataBag<QuestRequestItems> QuestRequestItems = new DataBag<QuestRequestItems>(Settings.SqlTables.quest_template);

        // Names
        public static readonly DataBag<ObjectName> ObjectNames = new DataBag<ObjectName>();

        // Vehicle Template Accessory
        public static readonly DataBag<VehicleTemplateAccessory> VehicleTemplateAccessories = new DataBag<VehicleTemplateAccessory>(Settings.SqlTables.vehicle_template_accessory);

        // Weather updates
        public static readonly DataBag<WeatherUpdate> WeatherUpdates = new DataBag<WeatherUpdate>(Settings.SqlTables.weather_updates);

        // Npc Spell Click
        public static readonly StoreBag<WowGuid> NpcSpellClicks = new StoreBag<WowGuid>(Settings.SqlTables.npc_spellclick_spells);
        public static readonly DataBag<NpcSpellClick> SpellClicks = new DataBag<NpcSpellClick>(Settings.SqlTables.npc_spellclick_spells);

        // Locales
        public static readonly DataBag<CreatureTemplateLocale> LocalesCreatures = new DataBag<CreatureTemplateLocale>(Settings.SqlTables.creature_template_locale);
        public static readonly DataBag<LocalesQuest> LocalesQuests = new DataBag<LocalesQuest>(Settings.SqlTables.locales_quest);
        public static readonly DataBag<QuestObjectivesLocale> LocalesQuestObjectives = new DataBag<QuestObjectivesLocale>(Settings.SqlTables.locales_quest_objectives);
        public static readonly DataBag<QuestOfferRewardLocale> LocalesQuestOfferRewards = new DataBag<QuestOfferRewardLocale>(Settings.SqlTables.locales_quest);
        public static readonly DataBag<QuestGreetingLocale> LocalesQuestGreeting = new DataBag<QuestGreetingLocale>(Settings.SqlTables.locales_quest);
        public static readonly DataBag<QuestRequestItemsLocale> LocalesQuestRequestItems = new DataBag<QuestRequestItemsLocale>(Settings.SqlTables.locales_quest);
        public static readonly DataBag<PageTextLocale> LocalesPageText = new DataBag<PageTextLocale>(Settings.SqlTables.page_text_locale);

        // Spell Casts
        public static readonly DataBag<PlaySpellVisualKit> SpellPlayVisualKit = new DataBag<PlaySpellVisualKit>(Settings.SqlTables.play_spell_visual_kit);
        public static readonly DataBag<SpellChannelStart> SpellChannelStart = new DataBag<SpellChannelStart>(Settings.SqlTables.spell_channel_start);
        public static readonly DataBag<SpellChannelUpdate> SpellChannelUpdate = new DataBag<SpellChannelUpdate>(Settings.SqlTables.spell_channel_update);
        public static readonly DataBag<SpellCastFailed> SpellCastFailed = new DataBag<SpellCastFailed>(Settings.SqlTables.spell_cast_failed);
        public static readonly DataBag<SpellCastData> SpellCastStart = new DataBag<SpellCastData>(Settings.SqlTables.spell_cast_start);
        public static readonly DataBag<SpellCastData> SpellCastGo = new DataBag<SpellCastData>(Settings.SqlTables.spell_cast_go);
        public static void StoreSpellCastData(SpellCastData castData, DataBag<SpellCastData> storage, Packet packet)
        {
            if (!Settings.SqlTables.spell_cast_start &&
                !Settings.SqlTables.spell_cast_go)
                return;

            if (!Settings.SavePlayerCasts && castData.CasterGuid.GetObjectType() == ObjectType.Player)
                return;

            castData.Time = packet.Time;

            /*
            uncomment for unique casts only
            foreach (var cast_pair in storage)
            {
                if (cast_pair.Item1.CasterID == castData.CasterID &&
                    cast_pair.Item1.CasterType == castData.CasterType &&
                    cast_pair.Item1.CastFlags == castData.CastFlags &&
                    cast_pair.Item1.CastFlagsEx == castData.CastFlagsEx &&
                    cast_pair.Item1.SpellID == castData.SpellID &&
                    cast_pair.Item1.MainTargetID == castData.MainTargetID &&
                    cast_pair.Item1.MainTargetType == castData.MainTargetType &&
                    cast_pair.Item1.HitTargetID.SequenceEqual(castData.HitTargetID) &&
                    cast_pair.Item1.HitTargetType.SequenceEqual(castData.HitTargetType))
                    return;
            }
            */

            storage.Add(castData, packet.TimeSpan);
        }
        public static readonly DataBag<SpellPetCooldown> SpellPetCooldown = new DataBag<SpellPetCooldown>(Settings.SqlTables.spell_pet_cooldown);
        public static readonly DataBag<SpellPetActions> SpellPetActions = new DataBag<SpellPetActions>(Settings.SqlTables.spell_pet_action);
        public static readonly DataBag<SpellTargetPosition> SpellTargetPositions = new DataBag<SpellTargetPosition>(Settings.SqlTables.spell_target_position);

        // World state
        public static readonly DataBag<WorldStateInit> WorldStateInits = new DataBag<WorldStateInit>(Settings.SqlTables.world_state_init);
        public static readonly DataBag<WorldStateUpdate> WorldStateUpdates = new DataBag<WorldStateUpdate>(Settings.SqlTables.world_state_update);

        public static readonly DataBag<HotfixData> HotfixDatas = new DataBag<HotfixData>(Settings.SqlTables.hotfix_data);
        public static readonly DataBag<HotfixBlob> HotfixBlobs = new DataBag<HotfixBlob>(Settings.SqlTables.hotfix_blob);
        public static readonly DataBag<HotfixOptionalData> HotfixOptionalDatas = new DataBag<HotfixOptionalData>(new List<SQLOutput> { });

        // Scenes
        public static readonly DataBag<SceneTemplate> Scenes = new DataBag<SceneTemplate>(Settings.SqlTables.scene_template);

        // Scenario
        public static readonly DataBag<ScenarioPOI> ScenarioPOIs = new DataBag<ScenarioPOI>(Settings.SqlTables.scenario_poi);
        public static readonly DataBag<ScenarioPOIPoint> ScenarioPOIPoints = new DataBag<ScenarioPOIPoint>(Settings.SqlTables.scenario_poi);

        public static readonly DataBag<BroadcastText> BroadcastTexts = new DataBag<BroadcastText>(Settings.SqlTables.broadcast_text);
        public static readonly DataBag<BroadcastTextLocale> BroadcastTextLocales = new DataBag<BroadcastTextLocale>(Settings.SqlTables.broadcast_text_locale);

        //Player Choice
        public static readonly DataBag<PlayerChoiceTemplate> PlayerChoices = new DataBag<PlayerChoiceTemplate>(Settings.SqlTables.playerchoice);
        public static readonly DataBag<PlayerChoiceLocaleTemplate> PlayerChoiceLocales = new DataBag<PlayerChoiceLocaleTemplate>(Settings.SqlTables.playerchoice);
        public static readonly DataBag<PlayerChoiceResponseTemplate> PlayerChoiceResponses = new DataBag<PlayerChoiceResponseTemplate>(Settings.SqlTables.playerchoice);
        public static readonly DataBag<PlayerChoiceResponseLocaleTemplate> PlayerChoiceResponseLocales = new DataBag<PlayerChoiceResponseLocaleTemplate>(Settings.SqlTables.playerchoice);
        public static readonly DataBag<PlayerChoiceResponseRewardTemplate> PlayerChoiceResponseRewards = new DataBag<PlayerChoiceResponseRewardTemplate>(Settings.SqlTables.playerchoice);
        public static readonly DataBag<PlayerChoiceResponseRewardCurrencyTemplate> PlayerChoiceResponseRewardCurrencies = new DataBag<PlayerChoiceResponseRewardCurrencyTemplate>(Settings.SqlTables.playerchoice);
        public static readonly DataBag<PlayerChoiceResponseRewardFactionTemplate> PlayerChoiceResponseRewardFactions = new DataBag<PlayerChoiceResponseRewardFactionTemplate>(Settings.SqlTables.playerchoice);
        public static readonly DataBag<PlayerChoiceResponseRewardItemTemplate> PlayerChoiceResponseRewardItems = new DataBag<PlayerChoiceResponseRewardItemTemplate>(Settings.SqlTables.playerchoice);

        // Client Actions
        public static readonly DataBag<ClientReclaimCorpse> ClientReclaimCorpseTimes = new DataBag<ClientReclaimCorpse>(Settings.SqlTables.client_reclaim_corpse);
        public static readonly DataBag<ClientReleaseSpirit> ClientReleaseSpiritTimes = new DataBag<ClientReleaseSpirit>(Settings.SqlTables.client_release_spirit);

        // Guild
        public static readonly DataBag<GuildTemplate> Guild = new DataBag<GuildTemplate>(Settings.SqlTables.guild);
        public static readonly DataBag<GuildRankTemplate> GuildRank = new DataBag<GuildRankTemplate>(Settings.SqlTables.guild_rank);

        public static void ClearContainers()
        {
            SniffData.Clear();

            Objects.Clear();
            ObjectDestroyTimes.Clear();
            ObjectCreate1Times.Clear();
            ObjectCreate2Times.Clear();

            AreaTriggerTemplates.Clear();
            AreaTriggerTemplatesVertices.Clear();

            ConversationActors.Clear();
            ConversationActorTemplates.Clear();
            ConversationLineTemplates.Clear();
            ConversationTemplates.Clear();

            PlayerMovements.Clear();
            PlayerActiveCreateTime.Clear();

            GameObjectClientUseTimes.Clear();
            GameObjectCustomAnims.Clear();
            GameObjectDespawnAnims.Clear();
            GameObjectLoot.Clear();
            GameObjectTemplates.Clear();
            GameObjectTemplateQuestItems.Clear();
            GameObjectUpdates.Clear();

            ItemClientUseTimes.Clear();
            ItemTemplates.Clear();

            QuestTemplates.Clear();
            QuestObjectives.Clear();
            QuestVisualEffects.Clear();
            QuestRewardDisplaySpells.Clear();

            UnitAttackLogs.Clear();
            UnitAttackStartTimes.Clear();
            UnitAttackStopTimes.Clear();
            CreatureClientInteractTimes.Clear();
            CreatureLoot.Clear();
            CreatureStats.Clear();

            CreatureTemplates.Clear();
            CreatureTemplatesClassic.Clear();
            CreatureTemplatesNonWDB.Clear();
            CreatureTemplateQuestItems.Clear();
            CreatureTemplateScalings.Clear();
            CreatureTemplateModels.Clear();
            UnitAurasUpdates.Clear();
            UnitEquipmentValuesUpdates.Clear();
            UnitGuidValuesUpdates.Clear();
            UnitValuesUpdates.Clear();
            UnitSpeedUpdates.Clear();

            NpcTrainers.Clear();
            NpcVendors.Clear();
            Trainers.Clear();
            TrainerSpells.Clear();
            CreatureTrainers.Clear();

            PageTexts.Clear();
            NpcTexts.Clear();
            NpcTextsMop.Clear();

            WorldTexts.Clear();
            CreatureTexts.Clear();
            CreatureTextTemplates.Clear();
            GameObjectTexts.Clear();
            GameObjectTextTemplates.Clear();
            CharacterTexts.Clear();

            GossipPOIs.Clear();

            Emotes.Clear();
            Music.Clear();
            Sounds.Clear();
            SpellsX.Clear();
            QuestOfferRewards.Clear();
            GossipSelects.Clear();

            StartActions.Clear();
            StartPositions.Clear();

            CreatureDefaultGossips.Clear();
            CreatureGossips.Clear();
            Gossips.Clear();
            GossipMenuOptions.Clear();
            GossipMenuOptionActions.Clear();
            GossipMenuOptionBoxes.Clear();

            QuestPOIs.Clear();
            QuestPOIPoints.Clear();

            QuestStarters.Clear();
            QuestEnders.Clear();
            QuestClientAcceptTimes.Clear();
            QuestClientCompleteTimes.Clear();
            QuestCompleteTimes.Clear();
            QuestFailTimes.Clear();
            QuestGreetings.Clear();
            QuestDetails.Clear();
            QuestRequestItems.Clear();

            ObjectNames.Clear();

            VehicleTemplateAccessories.Clear();

            WeatherUpdates.Clear();

            NpcSpellClicks.Clear();
            SpellClicks.Clear();

            SpellPlayVisualKit.Clear();
            SpellCastFailed.Clear();
            SpellCastStart.Clear();
            SpellCastGo.Clear();
            SpellPetActions.Clear();
            SpellPetCooldown.Clear();
            SpellTargetPositions.Clear();

            LocalesCreatures.Clear();
            LocalesQuests.Clear();
            LocalesQuestObjectives.Clear();
            LocalesQuestOfferRewards.Clear();
            LocalesQuestGreeting.Clear();
            LocalesQuestRequestItems.Clear();
            LocalesPageText.Clear();

            WorldStateInits.Clear();
            WorldStateUpdates.Clear();

            HotfixDatas.Clear();
            HotfixBlobs.Clear();
            HotfixOptionalDatas.Clear();

            Scenes.Clear();

            ScenarioPOIs.Clear();
            ScenarioPOIPoints.Clear();

            BroadcastTexts.Clear();
            BroadcastTextLocales.Clear();
            
            PlayerChoices.Clear();
            PlayerChoiceLocales.Clear();
            PlayerChoiceResponses.Clear();
            PlayerChoiceResponseLocales.Clear();
            PlayerChoiceResponseRewards.Clear();
            PlayerChoiceResponseRewardCurrencies.Clear();
            PlayerChoiceResponseRewardFactions.Clear();
            PlayerChoiceResponseRewardItems.Clear();

            Guild.Clear();
            GuildRank.Clear();
        }
    }
}

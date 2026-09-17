using System;
using System.Collections.Generic;
using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;
using WowPacketParser.Store;
using WowPacketParser.Store.Objects;
using WowPacketParserModule.V7_0_3_22248.Parsers;
using CoreFields = WowPacketParser.Enums.Version;
using CoreParsers = WowPacketParser.Parsing.Parsers;
using MovementFlag = WowPacketParser.Enums.v4.MovementFlag;
using MovementFlag2 = WowPacketParser.Enums.v7.MovementFlag2;
using SplineFlag = WowPacketParserModule.V7_0_3_22248.Enums.SplineFlag;
using WowPacketParser.Store.Objects.UpdateFields;
using System.Collections;

namespace WowPacketParserModule.V8_0_1_27101.Parsers
{
    public static class UpdateHandler
    {
        [HasSniffData] // in ReadCreateObjectBlock
        [Parser(Opcode.SMSG_UPDATE_OBJECT)]
        public static void HandleUpdateObject(Packet packet)
        {
            var count = packet.ReadUInt32("NumObjUpdates");
            uint map = packet.ReadUInt16<MapId>("MapID");
            packet.ResetBitReader();
            var hasRemovedObjects = packet.ReadBit("HasRemovedObjects");
            if (hasRemovedObjects)
            {
                var destroyedObjCount = packet.ReadInt16("DestroyedObjCount");
                var removedObjCount = packet.ReadUInt32("RemovedObjCount"); // destroyed + out of range
                var outOfRangeObjCount = removedObjCount - destroyedObjCount;

                for (var i = 0; i < destroyedObjCount; i++)
                {
                    WowGuid guid = packet.ReadPackedGuid128("ObjectGUID", "Destroyed", i);
                    Storage.StoreObjectDestroyTime(guid, packet.Time);

                    if (guid.GetHighType() == HighGuidType.GameObject)
                        Storage.StoreGameObjectDespawnTime(guid, packet.Time);
                }
                for (var i = 0; i < outOfRangeObjCount; i++)
                {
                    WowGuid guid = packet.ReadPackedGuid128("ObjectGUID", "OutOfRange", i);
                    Storage.StoreObjectDestroyTime(guid, packet.Time);
                }
            }
            packet.ReadUInt32("Data size");

            for (var i = 0; i < count; i++)
            {
                var type = packet.ReadByte();
                var typeString = ((UpdateTypeCataclysm)type).ToString();

                packet.AddValue("UpdateType", typeString, i);
                switch (typeString)
                {
                    case "Values":
                    {
                        var guid = packet.ReadPackedGuid128("Object Guid", i);
                        if (ClientVersion.AddedInVersion(ClientVersionBuild.V8_1_0_28724))
                        {
                            var updatefieldSize = packet.ReadUInt32();
                            var handler = CoreFields.UpdateFields.GetHandler();
                            using (var fieldsData = new Packet(packet.ReadBytes((int)updatefieldSize), packet.Opcode, packet.Time, packet.Direction, packet.Number, packet.Writer, packet.FileName))
                            {
                                if (handler == null)
                                {
                                    packet.WriteLine("[{0}] Error: No update field handler for current build.", i);
                                    continue;
                                }

                                WoWObject obj;
                                Storage.Objects.TryGetValue(guid, out obj);

                                IObjectData oldObjectData = null;
                                IGameObjectData oldGameObjectData = null;
                                IUnitData oldUnitData = null;
                                IPlayerData oldPlayerData = null;
                                IActivePlayerData oldActivePlayerData = null;

                                var updateTypeFlag = fieldsData.ReadUInt32();
                                if ((updateTypeFlag & 0x0001) != 0)
                                {
                                    if (obj != null && obj.ObjectData != null)
                                        oldObjectData = obj.ObjectData.Clone();
                                    var data = handler.ReadUpdateObjectData(fieldsData, obj?.ObjectData, i);
                                    if (obj != null)
                                        obj.ObjectData = data;
                                }
                                if ((updateTypeFlag & 0x0002) != 0)
                                    handler.ReadUpdateItemData(fieldsData, null, i);
                                if ((updateTypeFlag & 0x0004) != 0)
                                    handler.ReadUpdateContainerData(fieldsData, null, i);
                                if ((updateTypeFlag & 0x0008) != 0)
                                    handler.ReadUpdateAzeriteEmpoweredItemData(fieldsData, null, i);
                                if ((updateTypeFlag & 0x0010) != 0)
                                    handler.ReadUpdateAzeriteItemData(fieldsData, null, i);
                                if ((updateTypeFlag & 0x0020) != 0)
                                {
                                    var unit = obj as Unit;
                                    if (unit != null && unit.UnitData != null)
                                        oldUnitData = unit.UnitData.Clone();
                                        var data = handler.ReadUpdateUnitData(fieldsData, unit?.UnitData, i);
                                    if (unit != null)
                                        unit.UnitData = data;
                                }
                                if ((updateTypeFlag & 0x0040) != 0)
                                {
                                    var player = obj as Player;
                                    if (player != null && player.PlayerData != null)
                                        oldPlayerData = player.PlayerData.Clone();
                                    var data = handler.ReadUpdatePlayerData(fieldsData, player?.PlayerData, i);
                                    if (player != null)
                                        player.PlayerData = data;
                                }
                                if ((updateTypeFlag & 0x0080) != 0)
                                {
                                    var player = obj as Player;
                                    if (player != null && player.ActivePlayerData != null)
                                        oldActivePlayerData = player.ActivePlayerData.Clone();
                                    var data = handler.ReadUpdateActivePlayerData(fieldsData, player?.ActivePlayerData, i);
                                    if (player != null)
                                        player.ActivePlayerData = data;
                                }
                                if ((updateTypeFlag & 0x0100) != 0)
                                {
                                    var go = obj as GameObject;
                                    if (go != null && go.GameObjectData != null)
                                        oldGameObjectData = go.GameObjectData.Clone();
                                    var data = handler.ReadUpdateGameObjectData(fieldsData, go?.GameObjectData, i);
                                    if (go != null)
                                        go.GameObjectData = data;
                                }
                                if ((updateTypeFlag & 0x0200) != 0)
                                {
                                    DynamicObject dynobj = obj as DynamicObject;
                                    var data = handler.ReadUpdateDynamicObjectData(fieldsData, dynobj?.DynamicObjectData, i);
                                    if (dynobj != null)
                                        dynobj.DynamicObjectData = data;
                                }
                                if ((updateTypeFlag & 0x0400) != 0)
                                    handler.ReadUpdateCorpseData(fieldsData, null, i);
                                if ((updateTypeFlag & 0x0800) != 0)
                                    handler.ReadUpdateAreaTriggerData(fieldsData, null, i);
                                if ((updateTypeFlag & 0x1000) != 0)
                                    handler.ReadUpdateSceneObjectData(fieldsData, null, i);
                                if ((updateTypeFlag & 0x2000) != 0)
                                {
                                    var conversation = obj as ConversationTemplate;
                                    var data = handler.ReadUpdateConversationData(fieldsData, conversation?.ConversationData, i);
                                    if (conversation != null)
                                        conversation.ConversationData = data;
                                }

                                if (obj != null)
                                    StoreObjectUpdate(packet, guid, obj, oldObjectData, oldGameObjectData, oldUnitData, oldPlayerData, oldActivePlayerData, false);
                            }
                        }
                        else
                            CoreParsers.UpdateHandler.ReadValuesUpdateBlock(packet, guid, i);
                        break;
                    }
                    case "CreateObject1":
                    {
                        var guid = packet.ReadPackedGuid128("Object Guid", i);
                        ReadCreateObjectBlock(packet, guid, map, i, ObjectCreateType.Create1);
                        break;
                    }
                    case "CreateObject2":
                    {
                        var guid = packet.ReadPackedGuid128("Object Guid", i);
                        ReadCreateObjectBlock(packet, guid, map, i, ObjectCreateType.Create2);
                        break;
                    }
                }
            }
        }

        public static void StoreObjectUpdate(Packet packet, WowGuid guid, WoWObject obj, IObjectData oldObjectData, IGameObjectData oldGameObjectData, IUnitData oldUnitData, IPlayerData oldPlayerData, IActivePlayerData oldActivePlayerData, bool isCreate)
        {
            ObjectType objectType = obj.Type;
            if ((objectType == ObjectType.Unit) ||
                (objectType == ObjectType.Player) ||
                (objectType == ObjectType.ActivePlayer))
            {
                Unit unit = obj as Unit;
                bool hasData = false;
                CreatureValuesUpdate creatureUpdate = new CreatureValuesUpdate();
                if (oldObjectData != null)
                {
                    if (oldObjectData.EntryID != unit.ObjectData.EntryID)
                    {
                        hasData = true;
                        creatureUpdate.Entry = (uint)unit.ObjectData.EntryID;
                    }
                    if (oldObjectData.Scale != unit.ObjectData.Scale)
                    {
                        hasData = true;
                        creatureUpdate.Scale = unit.ObjectData.Scale;
                    }
                    if (oldObjectData.DynamicFlags != unit.ObjectData.DynamicFlags)
                    {
                        hasData = true;
                        creatureUpdate.DynamicFlags = unit.ObjectData.DynamicFlags;
                    }
                }
                if (oldUnitData != null)
                {
                    if (!isCreate && Settings.SqlTables.creature_aggro_distance &&
                        guid.GetHighType() == HighGuidType.Creature &&
                       (oldUnitData.Flags & (uint)UnitFlags.IsInCombat) == 0 &&
                       (unit.UnitData.Flags & (uint)UnitFlags.IsInCombat) != 0 &&
                       (oldUnitData.Target == null || oldUnitData.Target.IsEmpty()) &&
                        unit.UnitData.Target.GetHighType() == HighGuidType.Player &&
                        unit.UnitData.Health == unit.UnitData.MaxHealth &&
                       !Storage.WasLastCastOnCreatureWithin(guid, packet.Time, 10))
                    {
                        Storage.StoreCreatureAggroDistance(unit, packet);
                    }

                    if (oldUnitData.DisplayID != unit.UnitData.DisplayID)
                    {
                        hasData = true;
                        creatureUpdate.DisplayID = (uint)unit.UnitData.DisplayID;
                    }
                    if (oldUnitData.MountDisplayID != unit.UnitData.MountDisplayID)
                    {
                        hasData = true;
                        creatureUpdate.MountDisplayID = (uint)unit.UnitData.MountDisplayID;
                    }
                    if (oldUnitData.FactionTemplate != unit.UnitData.FactionTemplate)
                    {
                        hasData = true;
                        creatureUpdate.FactionTemplate = (uint)unit.UnitData.FactionTemplate;
                    }
                    if (oldUnitData.Level != unit.UnitData.Level)
                    {
                        if (objectType == ObjectType.ActivePlayer)
                            Storage.SavePlayerStats(obj, false, packet.SniffId);

                        hasData = true;
                        creatureUpdate.Level = (uint)unit.UnitData.Level;
                    }
                    if (oldUnitData.AuraState != unit.UnitData.AuraState)
                    {
                        hasData = true;
                        creatureUpdate.AuraState = unit.UnitData.AuraState;
                    }
                    if (oldUnitData.EmoteState != unit.UnitData.EmoteState)
                    {
                        hasData = true;
                        creatureUpdate.EmoteState = (uint)unit.UnitData.EmoteState;
                    }
                    if (oldUnitData.StandState != unit.UnitData.StandState)
                    {
                        hasData = true;
                        creatureUpdate.StandState = unit.UnitData.StandState;
                    }
                    /*
                    if (oldUnitData.PetTalentPoints != unit.UnitData.PetTalentPoints)
                    {
                        hasData = true;
                        creatureUpdate.PetTalentPoints = unit.UnitData.PetTalentPoints;
                    }
                    */
                    if (oldUnitData.VisFlags != unit.UnitData.VisFlags)
                    {
                        hasData = true;
                        creatureUpdate.VisFlags = unit.UnitData.VisFlags;
                    }
                    if (oldUnitData.AnimTier != unit.UnitData.AnimTier)
                    {
                        hasData = true;
                        creatureUpdate.AnimTier = unit.UnitData.AnimTier;
                    }
                    if (oldUnitData.SheatheState != unit.UnitData.SheatheState)
                    {
                        hasData = true;
                        creatureUpdate.SheathState = unit.UnitData.SheatheState;
                    }
                    if (oldUnitData.PvpFlags != unit.UnitData.PvpFlags)
                    {
                        hasData = true;
                        creatureUpdate.PvpFlags = unit.UnitData.PvpFlags;
                    }
                    /*
                    if (oldUnitData.PetFlags != unit.UnitData.PetFlags)
                    {
                        hasData = true;
                        creatureUpdate.PetFlags = unit.UnitData.PetFlags;
                    }
                    */
                    if (oldUnitData.ShapeshiftForm != unit.UnitData.ShapeshiftForm)
                    {
                        hasData = true;
                        creatureUpdate.ShapeshiftForm = unit.UnitData.ShapeshiftForm;
                    }
                    if (oldUnitData.NpcFlags[0] != unit.UnitData.NpcFlags[0])
                    {
                        hasData = true;
                        creatureUpdate.NpcFlag = unit.UnitData.NpcFlags[0];
                    }
                    if (oldUnitData.Flags != unit.UnitData.Flags)
                    {
                        if (((oldUnitData.Flags & (uint)UnitFlags.IsInCombat) == 0) && // was not in combat
                            ((unit.UnitData.Flags & (uint)UnitFlags.IsInCombat) != 0)) // is in combat
                        {
                            // on enter combat
                            unit.EnterCombatTime = packet.Time;
                        }
                        else if (((oldUnitData.Flags & (uint)UnitFlags.IsInCombat) != 0) && // was in combat
                                 ((unit.UnitData.Flags & (uint)UnitFlags.IsInCombat) == 0)) // is not in combat
                        {
                            // on leave combat
                            unit.EnterCombatTime = null;
                            unit.DontSaveCombatSpellTimers = false;
                        }

                        hasData = true;
                        creatureUpdate.UnitFlag = unit.UnitData.Flags;
                    }
                    if (oldUnitData.Flags2 != unit.UnitData.Flags2)
                    {
                        hasData = true;
                        creatureUpdate.UnitFlag2 = unit.UnitData.Flags2;
                    }
                    if (oldUnitData.Health != unit.UnitData.Health)
                    {
                        if (!isCreate && unit.UnitData.Health == 0 &&
                            objectType == ObjectType.Unit &&
                            guid.GetHighType() != HighGuidType.Pet)
                        {
                            Storage.StoreCreatureDeathTime(guid, packet.Time);
                            packet.AddSniffData(StoreNameType.Unit, (int)guid.GetEntry(), "DEATH");
                        }

                        if (Settings.SaveHealthUpdates)
                        {
                            hasData = true;
                            creatureUpdate.CurrentHealth = (uint)unit.UnitData.Health;
                        }
                    }
                    if (oldUnitData.MaxHealth != unit.UnitData.MaxHealth && Settings.SaveHealthUpdates)
                    {
                        hasData = true;
                        creatureUpdate.MaxHealth = (uint)unit.UnitData.MaxHealth;
                    }
                    if (oldUnitData.DisplayPower != unit.UnitData.DisplayPower)
                    {
                        hasData = true;
                        creatureUpdate.PowerType = unit.UnitData.DisplayPower;
                    }
                    if (objectType == ObjectType.Unit && Settings.SqlTables.creature_power_values_update ||
                        objectType != ObjectType.Unit && Settings.SqlTables.player_power_values_update)
                    {
                        int powersCount = ClientVersion.GetPowerCountForClientVersion();
                        for (int powerType = 0; powerType < powersCount; powerType++)
                        {
                            if (oldUnitData.Power[powerType] != unit.UnitData.Power[powerType] ||
                                oldUnitData.MaxPower[powerType] != unit.UnitData.MaxPower[powerType])
                            {
                                CreaturePowerValuesUpdate powerUpdate = new CreaturePowerValuesUpdate();
                                powerUpdate.PowerType = (uint)powerType;
                                powerUpdate.UnixTimeMs = (ulong)packet.UnixTimeMs;

                                if (oldUnitData.Power[powerType] != unit.UnitData.Power[powerType])
                                {
                                    // don't calculate spell timers if mob is out of mana
                                    if (powerType == (int)PowerType.Mana &&
                                        oldUnitData.Mana > unit.UnitData.Mana && // mana decreasing
                                        unit.IsInCombat() && unit.UnitData.MaxMana > 0 &&
                                        ((float)unit.UnitData.Mana / unit.UnitData.MaxMana) < 0.1) // less than 10%
                                        unit.DontSaveCombatSpellTimers = true;

                                    powerUpdate.CurrentPower = (uint)unit.UnitData.Power[powerType];
                                }
                                if (oldUnitData.MaxPower[powerType] != unit.UnitData.MaxPower[powerType])
                                {
                                    powerUpdate.MaxPower = (uint)unit.UnitData.MaxPower[powerType];
                                }
                                Storage.StoreUnitPowerValuesUpdate(guid, powerUpdate);
                            }
                        }
                    }
                    if (oldUnitData.BoundingRadius != unit.UnitData.BoundingRadius)
                    {
                        hasData = true;
                        creatureUpdate.BoundingRadius = unit.UnitData.BoundingRadius;
                    }
                    if (oldUnitData.CombatReach != unit.UnitData.CombatReach)
                    {
                        hasData = true;
                        creatureUpdate.CombatReach = unit.UnitData.CombatReach;
                    }
                    if (oldUnitData.ModHaste != unit.UnitData.ModHaste)
                    {
                        hasData = true;
                        creatureUpdate.ModMeleeHaste = unit.UnitData.ModHaste;
                    }
                    if (oldUnitData.AttackRoundBaseTime[0] != unit.UnitData.AttackRoundBaseTime[0])
                    {
                        hasData = true;
                        creatureUpdate.MainHandAttackTime = unit.UnitData.AttackRoundBaseTime[0];
                    }
                    if (oldUnitData.AttackRoundBaseTime[1] != unit.UnitData.AttackRoundBaseTime[1])
                    {
                        hasData = true;
                        creatureUpdate.OffHandAttackTime = unit.UnitData.AttackRoundBaseTime[1];
                    }
                    if (oldUnitData.ChannelData.SpellID != unit.UnitData.ChannelData.SpellID)
                    {
                        hasData = true;
                        creatureUpdate.ChannelSpellId = (uint)unit.UnitData.ChannelData.SpellID;
                    }
                    if (oldUnitData.ChannelData.SpellXSpellVisualID != unit.UnitData.ChannelData.SpellXSpellVisualID)
                    {
                        hasData = true;
                        creatureUpdate.ChannelVisualId = (uint)unit.UnitData.ChannelData.SpellXSpellVisualID;
                    }
                    uint slot = 0;
                    foreach (var item in unit.UnitData.VirtualItems)
                    {
                        if (oldUnitData.VirtualItems[slot].ItemID != unit.UnitData.VirtualItems[slot].ItemID)
                        {
                            CreatureEquipmentValuesUpdate equipmentUpdate = new CreatureEquipmentValuesUpdate();
                            equipmentUpdate.ItemId = (uint)unit.UnitData.VirtualItems[slot].ItemID;
                            equipmentUpdate.Slot = slot;
                            equipmentUpdate.time = packet.Time;
                            Storage.StoreUnitEquipmentValuesUpdate(guid, equipmentUpdate);
                        }
                        slot++;
                    }
                    if (oldUnitData.Charm != unit.UnitData.Charm)
                    {
                        CreatureGuidValuesUpdate guidUpdate = new CreatureGuidValuesUpdate();
                        guidUpdate.guid = unit.UnitData.Charm;
                        guidUpdate.time = packet.Time;
                        guidUpdate.FieldName = "Charm";
                        Storage.StoreUnitGuidValuesUpdate(guid, guidUpdate);
                    }
                    if (oldUnitData.Summon != unit.UnitData.Summon)
                    {
                        CreatureGuidValuesUpdate guidUpdate = new CreatureGuidValuesUpdate();
                        guidUpdate.guid = unit.UnitData.Summon;
                        guidUpdate.time = packet.Time;
                        guidUpdate.FieldName = "Summon";
                        Storage.StoreUnitGuidValuesUpdate(guid, guidUpdate);
                    }
                    if (oldUnitData.CharmedBy != unit.UnitData.CharmedBy)
                    {
                        CreatureGuidValuesUpdate guidUpdate = new CreatureGuidValuesUpdate();
                        guidUpdate.guid = unit.UnitData.CharmedBy;
                        guidUpdate.time = packet.Time;
                        guidUpdate.FieldName = "CharmedBy";
                        Storage.StoreUnitGuidValuesUpdate(guid, guidUpdate);
                    }
                    if (oldUnitData.SummonedBy != unit.UnitData.SummonedBy)
                    {
                        CreatureGuidValuesUpdate guidUpdate = new CreatureGuidValuesUpdate();
                        guidUpdate.guid = unit.UnitData.SummonedBy;
                        guidUpdate.time = packet.Time;
                        guidUpdate.FieldName = "SummonedBy";
                        Storage.StoreUnitGuidValuesUpdate(guid, guidUpdate);
                    }
                    if (oldUnitData.CreatedBy != unit.UnitData.CreatedBy)
                    {
                        CreatureGuidValuesUpdate guidUpdate = new CreatureGuidValuesUpdate();
                        guidUpdate.guid = unit.UnitData.CreatedBy;
                        guidUpdate.time = packet.Time;
                        guidUpdate.FieldName = "CreatedBy";
                        Storage.StoreUnitGuidValuesUpdate(guid, guidUpdate);
                    }
                    if (oldUnitData.DemonCreator != unit.UnitData.DemonCreator)
                    {
                        CreatureGuidValuesUpdate guidUpdate = new CreatureGuidValuesUpdate();
                        guidUpdate.guid = unit.UnitData.DemonCreator;
                        guidUpdate.time = packet.Time;
                        guidUpdate.FieldName = "DemonCreator";
                        Storage.StoreUnitGuidValuesUpdate(guid, guidUpdate);
                    }
                    if (oldUnitData.Target != unit.UnitData.Target)
                    {
                        CreatureGuidValuesUpdate guidUpdate = new CreatureGuidValuesUpdate();
                        guidUpdate.guid = unit.UnitData.Target;
                        guidUpdate.time = packet.Time;
                        guidUpdate.FieldName = "Target";
                        Storage.StoreUnitGuidValuesUpdate(guid, guidUpdate);
                    }
                }
                if (oldPlayerData != null)
                {
                    Player player = obj as Player;
                    uint slot = 0;
                    foreach (var item in player.PlayerData.VisibleItems)
                    {
                        if (oldPlayerData.VisibleItems[slot].ItemID != player.PlayerData.VisibleItems[slot].ItemID)
                        {
                            CreatureEquipmentValuesUpdate equipmentUpdate = new CreatureEquipmentValuesUpdate();
                            equipmentUpdate.ItemId = (uint)player.PlayerData.VisibleItems[slot].ItemID;
                            equipmentUpdate.Slot = slot;
                            equipmentUpdate.time = packet.Time;
                            Storage.StoreUnitEquipmentValuesUpdate(guid, equipmentUpdate);
                        }
                        slot++;
                    }
                    if (oldActivePlayerData != null)
                    {
                        if (oldActivePlayerData.CritPercentage != player.ActivePlayerData.CritPercentage)
                            Storage.SavePlayerMeleeCrit(obj, packet.SniffId);
                        if (oldActivePlayerData.RangedCritPercentage != player.ActivePlayerData.RangedCritPercentage)
                            Storage.SavePlayerRangedCrit(obj, packet.SniffId);
                        if (oldActivePlayerData.SpellCritPercentage != player.ActivePlayerData.SpellCritPercentage)
                            Storage.SavePlayerSpellCrit(obj, packet.SniffId);
                        if (oldActivePlayerData.DodgePercentage != player.ActivePlayerData.DodgePercentage)
                            Storage.SavePlayerDodge(obj, packet.SniffId);
                    }
                }
                if (hasData)
                {
                    creatureUpdate.UnixTimeMs = (ulong)packet.UnixTimeMs;
                    Storage.StoreUnitValuesUpdate(guid, creatureUpdate);
                }
            }
            else if (objectType == ObjectType.GameObject && oldGameObjectData != null)
            {
                GameObject go = obj as GameObject;
                bool hasData = false;
                GameObjectUpdate goUpdate = new GameObjectUpdate();
                if (oldObjectData != null)
                {
                    if ((oldObjectData.DynamicFlags & 0x0000FFFF) != (go.ObjectData.DynamicFlags & 0x0000FFFF))
                    {
                        hasData = true;
                        goUpdate.DynamicFlags = (go.ObjectData.DynamicFlags & 0x0000FFFF);
                    }
                    if (((oldObjectData.DynamicFlags & 0xFFFF0000) >> 16) != ((go.ObjectData.DynamicFlags & 0xFFFF0000) >> 16))
                    {
                        hasData = true;
                        goUpdate.PathProgress = ((go.ObjectData.DynamicFlags & 0xFFFF0000) >> 16);
                    }
                }
                if (oldGameObjectData.CustomParam != go.GameObjectData.CustomParam)
                {
                    hasData = true;
                    goUpdate.CustomParam = go.GameObjectData.CustomParam;
                }
                if (oldGameObjectData.Flags != go.GameObjectData.Flags)
                {
                    hasData = true;
                    goUpdate.Flags = go.GameObjectData.Flags;
                }
                if (oldGameObjectData.AnimProgress != go.GameObjectData.AnimProgress)
                {
                    hasData = true;
                    goUpdate.AnimProgress = go.GameObjectData.AnimProgress;
                }
                if (oldGameObjectData.State != go.GameObjectData.State)
                {
                    hasData = true;
                    goUpdate.State = (uint)go.GameObjectData.State;
                }
                if (oldGameObjectData.ArtKit != go.GameObjectData.ArtKit)
                {
                    hasData = true;
                    goUpdate.ArtKit = (uint)go.GameObjectData.ArtKit;
                }
                if (hasData)
                {
                    goUpdate.UnixTimeMs = (ulong)packet.UnixTimeMs;
                    Storage.StoreGameObjectUpdate(guid, goUpdate);
                }
            }
        }

        private static void ReadCreateObjectBlock(Packet packet, WowGuid guid, uint map, object index, ObjectCreateType type)
        {
            ObjectType objType = ObjectTypeConverter.Convert(packet.ReadByteE<ObjectType801>("Object Type", index));
            if (ClientVersion.RemovedInVersion(ClientVersionBuild.V8_1_0_28724))
                packet.ReadInt32("HeirFlags", index);

            WoWObject obj;
            bool isExistingObject = Storage.Objects.ContainsKey(guid);
            if (isExistingObject)
                obj = Storage.Objects[guid].Item1;
            else
                obj = CoreParsers.UpdateHandler.CreateObject(objType, map);

            BitArray updateMaskArray = null;
            var moves = ReadMovementUpdateBlock(packet, guid, obj, index);
            Storage.StoreObjectCreateTime(guid, map, moves, packet, type);

            if (ClientVersion.AddedInVersion(ClientVersionBuild.V8_1_0_28724))
            {
                var updatefieldSize = packet.ReadUInt32();
                var handler = CoreFields.UpdateFields.GetHandler();
                using (var fieldsData = new Packet(packet.ReadBytes((int)updatefieldSize), packet.Opcode, packet.Time, packet.Direction, packet.Number, packet.Writer, packet.FileName))
                {
                    if (handler == null)
                    {
                        packet.WriteLine("[{0}] Error: No update field handler for current build.", index);
                        return;
                    }

                    var flags = fieldsData.ReadByteE<UpdateFieldFlag>("FieldFlags", index);

                    IObjectData oldObjectData = null;
                    IGameObjectData oldGameObjectData = null;
                    IUnitData oldUnitData = null;
                    IPlayerData oldPlayerData = null;
                    IActivePlayerData oldActivePlayerData = null;

                    if (isExistingObject)
                        oldObjectData = obj.ObjectData.Clone();

                    obj.ObjectData = handler.ReadCreateObjectData(fieldsData, flags, index);

                    if (!isExistingObject)
                        obj.ObjectDataOriginal = obj.ObjectData.Clone();

                    switch (objType)
                    {
                        case ObjectType.Item:
                            handler.ReadCreateItemData(fieldsData, flags, index);
                            break;
                        case ObjectType.Container:
                            handler.ReadCreateItemData(fieldsData, flags, index);
                            handler.ReadCreateContainerData(fieldsData, flags, index);
                            break;
                        case ObjectType.AzeriteEmpoweredItem:
                            handler.ReadCreateItemData(fieldsData, flags, index);
                            handler.ReadCreateAzeriteEmpoweredItemData(fieldsData, flags, index);
                            break;
                        case ObjectType.AzeriteItem:
                            handler.ReadCreateItemData(fieldsData, flags, index);
                            handler.ReadCreateAzeriteItemData(fieldsData, flags, index);
                            break;
                        case ObjectType.Unit:
                            if (isExistingObject && (obj as Unit).UnitData != null)
                                oldUnitData = (obj as Unit).UnitData.Clone();
                            (obj as Unit).UnitData = handler.ReadCreateUnitData(fieldsData, flags, index);
                            if (!isExistingObject)
                                (obj as Unit).UnitDataOriginal = (obj as Unit).UnitData.Clone();
                            break;
                        case ObjectType.Player:
                            if (isExistingObject && (obj as Unit).UnitData != null)
                                oldUnitData = (obj as Unit).UnitData.Clone();
                            (obj as Unit).UnitData = handler.ReadCreateUnitData(fieldsData, flags, index);
                            if (!isExistingObject)
                                (obj as Unit).UnitDataOriginal = (obj as Unit).UnitData.Clone();

                            if (isExistingObject && (obj as Player).PlayerData != null)
                                oldPlayerData = (obj as Player).PlayerData.Clone();
                            (obj as Player).PlayerData = handler.ReadCreatePlayerData(fieldsData, flags, index);
                            if (!isExistingObject)
                                (obj as Player).PlayerDataOriginal = (obj as Player).PlayerData.Clone();
                            break;
                        case ObjectType.ActivePlayer:
                            if (isExistingObject && (obj as Unit).UnitData != null)
                                oldUnitData = (obj as Unit).UnitData.Clone();
                            (obj as Unit).UnitData = handler.ReadCreateUnitData(fieldsData, flags, index);
                            if (!isExistingObject)
                                (obj as Unit).UnitDataOriginal = (obj as Unit).UnitData.Clone();

                            if (isExistingObject && (obj as Player).PlayerData != null)
                                oldPlayerData = (obj as Player).PlayerData.Clone();
                            (obj as Player).PlayerData = handler.ReadCreatePlayerData(fieldsData, flags, index);
                            if (!isExistingObject)
                                (obj as Player).PlayerDataOriginal = (obj as Player).PlayerData.Clone();

                            if (isExistingObject && (obj as Player).ActivePlayerData != null)
                                oldActivePlayerData = (obj as Player).ActivePlayerData.Clone();
                            (obj as Player).ActivePlayerData = handler.ReadCreateActivePlayerData(fieldsData, flags, index);
                            if (!isExistingObject)
                                (obj as Player).ActivePlayerDataOriginal = (obj as Player).ActivePlayerData;
                            break;
                        case ObjectType.GameObject:
                            if (isExistingObject && (obj as GameObject).GameObjectData != null)
                                oldGameObjectData = (obj as GameObject).GameObjectData.Clone();
                            (obj as GameObject).GameObjectData = handler.ReadCreateGameObjectData(fieldsData, flags, index);
                            if (!isExistingObject)
                                (obj as GameObject).GameObjectDataOriginal = (obj as GameObject).GameObjectData.Clone();
                            break;
                        case ObjectType.DynamicObject:
                            (obj as DynamicObject).DynamicObjectData = handler.ReadCreateDynamicObjectData(fieldsData, flags, index);
                            if (!isExistingObject)
                                (obj as DynamicObject).DynamicObjectDataOriginal = (obj as DynamicObject).DynamicObjectData;
                            break;
                        case ObjectType.Corpse:
                            handler.ReadCreateCorpseData(fieldsData, flags, index);
                            break;
                        case ObjectType.AreaTrigger:
                            (obj as AreaTriggerCreateProperties).AreaTriggerData = handler.ReadCreateAreaTriggerData(fieldsData, flags, index);
                            break;
                        case ObjectType.SceneObject:
                            (obj as SceneObject).SceneObjectData = handler.ReadCreateSceneObjectData(fieldsData, flags, index);
                            break;
                        case ObjectType.Conversation:
                            (obj as ConversationTemplate).ConversationData = handler.ReadCreateConversationData(fieldsData, flags, index);
                            break;
                    }

                    if (isExistingObject)
                        StoreObjectUpdate(packet, guid, obj, oldObjectData, oldGameObjectData, oldUnitData, oldPlayerData, oldActivePlayerData, true);
                }

                obj.Movement = moves;

                // If this is the second time we see the same object (same guid,
                // same position) update its phasemask
                if (isExistingObject)
                    CoreParsers.UpdateHandler.ProcessExistingObject(ref obj, guid, packet, updateMaskArray, obj.UpdateFields, obj.DynamicUpdateFields, moves); // can't do "ref Storage.Objects[guid].Item1 directly
                else
                {
                    Storage.StoreNewObject(guid, obj, type, packet);

                    // Only needed for pets.
                    if (guid.GetHighType() == HighGuidType.Pet)
                        Storage.StoreCreatureStats(obj as Unit, updateMaskArray, guid.GetHighType() == HighGuidType.Pet, packet);
                    else if (objType == ObjectType.ActivePlayer)
                    {
                        Storage.SavePlayerMeleeCrit(obj, packet.SniffId);
                        Storage.SavePlayerRangedCrit(obj, packet.SniffId);
                        Storage.SavePlayerSpellCrit(obj, packet.SniffId);
                        Storage.SavePlayerDodge(obj, packet.SniffId);
                    }
                }
            }
            else
            {
                var updates = CoreParsers.UpdateHandler.ReadValuesUpdateBlockOnCreate(packet, objType, index, out updateMaskArray);
                var dynamicUpdates = CoreParsers.UpdateHandler.ReadDynamicValuesUpdateBlockOnCreate(packet, objType, index);

                // If this is the second time we see the same object (same guid,
                // same position) update its phasemask
                if (isExistingObject)
                {
                    CoreParsers.UpdateHandler.ProcessExistingObject(ref obj, guid, packet, updateMaskArray, updates, dynamicUpdates, moves);
                }
                else
                {
                    obj.Movement = moves;
                    obj.UpdateFields = updates;
                    obj.DynamicUpdateFields = dynamicUpdates;
                    Storage.StoreNewObject(guid, obj, type, packet);

                    // Only needed for pets.
                    if (guid.GetHighType() == HighGuidType.Pet)
                        Storage.StoreCreatureStats(obj as Unit, updateMaskArray, guid.GetHighType() == HighGuidType.Pet, packet);
                }
            }

            if (guid.HasEntry() && (objType == ObjectType.Unit || objType == ObjectType.GameObject))
                packet.AddSniffData(Utilities.ObjectTypeToStore(objType), (int)guid.GetEntry(), "SPAWN");
        }

        public static void ReadTransportData(MovementInfo moveInfo, WowGuid guid, Packet packet, object index)
        {
            packet.ResetBitReader();
            moveInfo.TransportGuid = packet.ReadPackedGuid128("TransportGUID", index);
            moveInfo.TransportOffset = packet.ReadVector4("TransportPosition", index);
            moveInfo.TransportSeat = packet.ReadSByte("VehicleSeatIndex", index);
            moveInfo.TransportTime = packet.ReadUInt32("MoveTime", index);

            var hasPrevMoveTime = packet.ReadBit("HasPrevMoveTime", index);
            var hasVehicleRecID = packet.ReadBit("HasVehicleRecID", index);

            if (hasPrevMoveTime)
                packet.ReadUInt32("PrevMoveTime", index);

            if (hasVehicleRecID)
                packet.ReadInt32("VehicleRecID", index);

            if (moveInfo.TransportGuid.HasEntry() && moveInfo.TransportGuid.GetHighType() == HighGuidType.Vehicle &&
                guid.HasEntry() && guid.GetHighType() == HighGuidType.Creature)
            {
                VehicleTemplateAccessory vehicleAccessory = new VehicleTemplateAccessory
                {
                    Entry = moveInfo.TransportGuid.GetEntry(),
                    AccessoryEntry = guid.GetEntry(),
                    SeatId = moveInfo.TransportSeat
                };
                Storage.VehicleTemplateAccessories.Add(vehicleAccessory, packet.TimeSpan);
            }
        }

        private static MovementInfo ReadMovementUpdateBlock(Packet packet, WowGuid guid, WoWObject obj, object index)
        {
            var moveInfo = new MovementInfo();

            packet.ResetBitReader();

            packet.ReadBit("NoBirthAnim", index);
            packet.ReadBit("EnablePortals", index);
            moveInfo.Hover = packet.ReadBit("PlayHoverAnim", index);

            var hasMovementUpdate = packet.ReadBit("HasMovementUpdate", index);
            var hasMovementTransport = packet.ReadBit("HasMovementTransport", index);
            var hasStationaryPosition = packet.ReadBit("Stationary", index);
            var hasCombatVictim = packet.ReadBit("HasCombatVictim", index);
            var hasServerTime = packet.ReadBit("HasServerTime", index);
            var hasVehicleCreate = packet.ReadBit("HasVehicleCreate", index);
            var hasAnimKitCreate = packet.ReadBit("HasAnimKitCreate", index);
            var hasRotation = packet.ReadBit("HasRotation", index);
            var hasAreaTrigger = packet.ReadBit("HasAreaTrigger", index);
            var hasGameObject = packet.ReadBit("HasGameObject", index);
            var hasSmoothPhasing = packet.ReadBit("HasSmoothPhasing", index);

            var isSelf = packet.ReadBit("ThisIsYou", index);
            if (isSelf)
                Storage.SetCurrentActivePlayer(guid, packet.Time);

            var sceneObjCreate = packet.ReadBit("SceneObjCreate", index);
            var playerCreateData = packet.ReadBit("HasPlayerCreateData", index);
            var hasConversation = packet.ReadBit("HasConversation", index);

            if (hasMovementUpdate)
            {
                packet.ResetBitReader();
                packet.ReadPackedGuid128("MoverGUID", index);

                if (ClientVersion.AddedInVersion(ClientVersionBuild.V9_2_0_42423))
                {
                    moveInfo.Flags = (uint)packet.ReadUInt32E<MovementFlag>("MovementFlags", index);
                    moveInfo.Flags2 = (uint)packet.ReadUInt32E<MovementFlag2>("MovementFlags2", index);
                    moveInfo.Flags3 = (uint)packet.ReadUInt32E<MovementFlag3>("MovementFlags3", index);
                }

                moveInfo.MoveTime = packet.ReadUInt32("MoveTime", index);
                moveInfo.Position = packet.ReadVector3("Position", index);
                moveInfo.Orientation = packet.ReadSingle("Orientation", index);

                moveInfo.SwimPitch = packet.ReadSingle("Pitch", index);
                moveInfo.SplineElevation = packet.ReadSingle("StepUpStartElevation", index);

                var removeForcesIDsCount = packet.ReadInt32();
                packet.ReadInt32("MoveIndex", index);

                for (var i = 0; i < removeForcesIDsCount; i++)
                    packet.ReadPackedGuid128("RemoveForcesIDs", index, i);

                if (ClientVersion.RemovedInVersion(ClientVersionBuild.V9_2_0_42423))
                {
                    moveInfo.Flags = (uint)packet.ReadBitsE<MovementFlag>("Movement Flags", 30, index);
                    moveInfo.Flags2 = (uint)packet.ReadBitsE<MovementFlag2>("Extra Movement Flags", 18, index);
                }

                var hasTransport = packet.ReadBit("Has Transport Data", index);
                var hasFall = packet.ReadBit("Has Fall Data", index);
                packet.ReadBit("HasSpline", index);
                packet.ReadBit("HeightChangeFailed", index);
                packet.ReadBit("RemoteTimeValid", index);
                var hasInertia = ClientVersion.AddedInVersion(ClientVersionBuild.V9_2_0_42423) && packet.ReadBit("HasInertia", index);

                if (hasTransport)
                    ReadTransportData(moveInfo, guid, packet, index);

                if (hasInertia)
                {
                    packet.ReadPackedGuid128("GUID", index, "Inertia");
                    packet.ReadVector3("Force", index, "Inertia");
                    packet.ReadUInt32("Lifetime", index, "Inertia");
                }

                if (hasFall)
                {
                    packet.ResetBitReader();
                    moveInfo.FallTime = packet.ReadUInt32("Jump Fall Time", index);
                    moveInfo.JumpVerticalSpeed = packet.ReadSingle("Jump Vertical Speed", index);

                    var hasFallDirection = packet.ReadBit("Has Fall Direction", index);
                    if (hasFallDirection)
                    {
                        moveInfo.JumpSinAngle = packet.ReadSingle("Jump Sin Angle", index);
                        moveInfo.JumpCosAngle = packet.ReadSingle("Jump Cos Angle", index);
                        moveInfo.JumpHorizontalSpeed = packet.ReadSingle("Jump Horizontal Speed", index);
                    }
                }

                moveInfo.WalkSpeed = packet.ReadSingle("WalkSpeed", index);
                moveInfo.RunSpeed = packet.ReadSingle("RunSpeed", index);
                moveInfo.RunBackSpeed = packet.ReadSingle("RunBackSpeed", index);
                moveInfo.SwimSpeed = packet.ReadSingle("SwimSpeed", index);
                moveInfo.SwimBackSpeed = packet.ReadSingle("SwimBackSpeed", index);
                moveInfo.FlightSpeed = packet.ReadSingle("FlightSpeed", index);
                moveInfo.FlightBackSpeed = packet.ReadSingle("FlightBackSpeed", index);
                moveInfo.TurnRate = packet.ReadSingle("TurnRate", index);
                moveInfo.PitchRate = packet.ReadSingle("PitchRate", index);

                var movementForceCount = packet.ReadUInt32("MovementForceCount", index);

                if (ClientVersion.AddedInVersion(ClientVersionBuild.V8_1_0_28724))
                    packet.ReadSingle("MovementForcesModMagnitude", index);

                packet.ResetBitReader();

                moveInfo.HasSplineData = packet.ReadBit("HasMovementSpline", index);

                for (var i = 0; i < movementForceCount; ++i)
                {
                    packet.ResetBitReader();
                    packet.ReadPackedGuid128("Id", index);
                    packet.ReadVector3("Origin", index);
                    packet.ReadVector3("Direction", index);
                    packet.ReadUInt32("TransportID", index);
                    packet.ReadSingle("Magnitude", index);
                    packet.ReadBits("Type", 2, index);

                    if (ClientVersion.AddedInVersion(ClientVersionBuild.V9_1_0_39185))
                    {
                        var unused910 = packet.ReadBit();
                        if (unused910)
                            packet.ReadInt32("Unused910", index);
                    }
                }

                if (moveInfo.HasSplineData)
                {
                    packet.ResetBitReader();
                    packet.ReadInt32("ID", index);
                    packet.ReadVector3("Destination", index);

                    var hasMovementSplineMove = packet.ReadBit("MovementSplineMove", index);
                    if (hasMovementSplineMove)
                    {
                        packet.ResetBitReader();

                        ServerSideMovement monsterMove = new ServerSideMovement();
                        monsterMove.SplineFlags = (uint)packet.ReadUInt32E<SplineFlag>("SplineFlags", index);
                        packet.ReadInt32("Elapsed", index);
                        monsterMove.MoveTime = packet.ReadUInt32("Duration", index);
                        packet.ReadSingle("DurationModifier", index);
                        packet.ReadSingle("NextDurationModifier", index);

                        var face = packet.ReadBits("Face", 2, index);
                        var hasSpecialTime = packet.ReadBit("HasSpecialTime", index);

                        var pointsCount = packet.ReadBits("PointsCount", 16, index);
                        monsterMove.SplineCount = pointsCount;
                        if (pointsCount > 0)
                            monsterMove.SplinePoints = new List<Vector3>();

                        if (ClientVersion.RemovedInVersion(ClientType.Shadowlands))
                            packet.ReadBitsE<SplineMode>("Mode", 2, index);

                        var hasSplineFilterKey = packet.ReadBit("HasSplineFilterKey", index);
                        var hasSpellEffectExtraData = packet.ReadBit("HasSpellEffectExtraData", index);
                        var hasJumpExtraData = packet.ReadBit("HasJumpExtraData", index);

                        var hasAnimationTierTransition = false;
                        var hasUnknown901 = false;
                        if (ClientVersion.AddedInVersion(ClientType.Shadowlands))
                        {
                            hasAnimationTierTransition = packet.ReadBit("HasAnimationTierTransition", index);
                            hasUnknown901 = packet.ReadBit("Unknown901", index);
                        }

                        if (hasSplineFilterKey)
                        {
                            packet.ResetBitReader();
                            var filterKeysCount = packet.ReadUInt32("FilterKeysCount", index);
                            for (var i = 0; i < filterKeysCount; ++i)
                            {
                                packet.ReadSingle("In", index, i);
                                packet.ReadSingle("Out", index, i);
                            }

                            packet.ReadBits("FilterFlags", 2, index);
                        }

                        float orientation = 100;
                        switch (face)
                        {
                            case 1:
                                var faceSpot = packet.ReadVector3("FaceSpot", index);
                                orientation = Utilities.GetAngle(moveInfo.Position.X, moveInfo.Position.Y, faceSpot.X, faceSpot.Y);
                                break;
                            case 2:
                                packet.ReadPackedGuid128("FaceGUID", index);
                                break;
                            case 3:
                                orientation = packet.ReadSingle("FaceDirection", index);
                                break;
                            default:
                                break;
                        }
                        monsterMove.Orientation = orientation;

                        if (hasSpecialTime)
                            packet.ReadUInt32("SpecialTime", index);

                        for (var i = 0; i < pointsCount; ++i)
                        {
                            var spot = packet.ReadVector3("Points", index, i);
                            monsterMove.SplinePoints.Add(spot);
                        }

                        if (hasSpellEffectExtraData)
                            MovementHandler.ReadMonsterSplineSpellEffectExtraData(packet, index);

                        if (hasJumpExtraData)
                            MovementHandler.ReadMonsterSplineJumpExtraData(monsterMove, packet, index);

                        if (hasAnimationTierTransition)
                        {
                            packet.ReadInt32("TierTransitionID", index);
                            monsterMove.EffectStartTime = packet.ReadUInt32("StartTime", index);
                            packet.ReadInt32("EndTime", index);
                            monsterMove.AnimTier = packet.ReadByte("AnimTier", index);
                        }

                        if (hasUnknown901)
                        {
                            for (var i = 0; i < 16; ++i)
                            {
                                packet.ReadInt32("Unknown1", index, "Unknown901", i);
                                packet.ReadInt32("Unknown2", index, "Unknown901", i);
                                packet.ReadInt32("Unknown3", index, "Unknown901", i);
                                packet.ReadInt32("Unknown4", index, "Unknown901", i);
                            }
                        }

                        if (guid == Storage.CurrentActivePlayer)
                            Storage.CurrentMoveSplineExpireTime = packet.UnixTimeMs + (long)monsterMove.MoveTime;

                        if (pointsCount > 0 && (Settings.SaveTransports || (moveInfo.TransportGuid == null || moveInfo.TransportGuid.IsEmpty())))
                        {
                            if (moveInfo.TransportGuid != null)
                                monsterMove.TransportGuid = moveInfo.TransportGuid;
                            monsterMove.TransportSeat = moveInfo.TransportSeat;

                            Unit unit = obj as Unit;
                            if (unit != null)
                                unit.AddWaypoint(monsterMove, moveInfo.Position, packet.Time);
                        }
                    }
                }
            }

            var pauseTimesCount = packet.ReadUInt32("PauseTimesCount", index);

            if (hasStationaryPosition)
            {
                moveInfo.Position = packet.ReadVector3();
                moveInfo.Orientation = packet.ReadSingle();

                packet.AddValue("Stationary Position", moveInfo.Position, index);
                packet.AddValue("Stationary Orientation", moveInfo.Orientation, index);
            }

            if (hasCombatVictim)
            {
                WowGuid victimGuid = packet.ReadPackedGuid128("CombatVictim Guid", index);
                Storage.StoreUnitAttackToggle(guid, victimGuid, packet.Time, true);
            } 

            if (hasServerTime)
                moveInfo.TransportPathTimer = packet.ReadUInt32("ServerTime", index);

            if (hasVehicleCreate)
            {
                moveInfo.VehicleId = (uint)packet.ReadInt32("RecID", index);
                moveInfo.VehicleOrientation = packet.ReadSingle("InitialRawFacing", index);
            }

            if (hasAnimKitCreate)
            {
                var aiId = packet.ReadUInt16("AiID", index);
                var movementId = packet.ReadUInt16("MovementID", index);
                var meleeId = packet.ReadUInt16("MeleeID", index);
                if (obj is Unit)
                {
                    Unit unit = obj as Unit;
                    unit.AIAnimKit = aiId;
                    unit.MovementAnimKit = movementId;
                    unit.MeleeAnimKit = meleeId;
                }
                else if (obj is GameObject)
                {
                    GameObject gob = obj as GameObject;
                    gob.AIAnimKitID = aiId;
                }
            }

            if (hasRotation)
                moveInfo.Rotation = packet.ReadPackedQuaternion("GameObject Rotation", index);

            for (var i = 0; i < pauseTimesCount; ++i)
                packet.ReadUInt32("PauseTimes", index, i);

            if (hasMovementTransport)
                ReadTransportData(moveInfo, guid, packet, index);

            if (hasAreaTrigger && obj is AreaTriggerCreateProperties)
            {
                AreaTriggerTemplate areaTriggerTemplate = new AreaTriggerTemplate
                {
                    Id = guid.GetEntry()
                };

                AreaTriggerCreateProperties spellAreaTrigger = (AreaTriggerCreateProperties)obj;
                spellAreaTrigger.AreaTriggerId = guid.GetEntry();

                packet.ResetBitReader();

                // CliAreaTrigger
                packet.ReadUInt32("ElapsedMs", index);

                packet.ReadVector3("RollPitchYaw1", index);

                areaTriggerTemplate.Flags   = 0;

                if (packet.ReadBit("HasAbsoluteOrientation", index))
                    areaTriggerTemplate.Flags |= (uint)AreaTriggerFlags.HasAbsoluteOrientation;

                if (packet.ReadBit("HasDynamicShape", index))
                    areaTriggerTemplate.Flags |= (uint)AreaTriggerFlags.HasDynamicShape;

                if (packet.ReadBit("HasAttached", index))
                    areaTriggerTemplate.Flags |= (uint)AreaTriggerFlags.HasAttached;

                if (packet.ReadBit("HasFaceMovementDir", index))
                    areaTriggerTemplate.Flags |= (uint)AreaTriggerFlags.FaceMovementDirection;

                if (packet.ReadBit("HasFollowsTerrain", index))
                    areaTriggerTemplate.Flags |= (uint)AreaTriggerFlags.FollowsTerrain;

                if (packet.ReadBit("Unk bit WoD62x", index))
                    areaTriggerTemplate.Flags |= (uint)AreaTriggerFlags.Unk1;

                if (packet.ReadBit("HasTargetRollPitchYaw", index))
                    areaTriggerTemplate.Flags |= (uint)AreaTriggerFlags.HasTargetRollPitchYaw;

                bool hasScaleCurveID = packet.ReadBit("HasScaleCurveID", index);
                bool hasMorphCurveID = packet.ReadBit("HasMorphCurveID", index);
                bool hasFacingCurveID = packet.ReadBit("HasFacingCurveID", index);
                bool hasMoveCurveID = packet.ReadBit("HasMoveCurveID", index);
                bool hasAnimProgress = false;

                if (ClientVersion.RemovedInVersion(ClientVersionBuild.V9_1_5_40772))
                {
                    if (packet.ReadBit("HasAnimID", index))
                        areaTriggerTemplate.Flags |= (uint)AreaTriggerFlags.HasAnimId;

                    if (packet.ReadBit("HasAnimKitID", index))
                        areaTriggerTemplate.Flags |= (uint)AreaTriggerFlags.HasAnimKitId;

                    if (packet.ReadBit("unkbit50", index))
                        areaTriggerTemplate.Flags |= (uint)AreaTriggerFlags.Unk3;

                    hasAnimProgress = packet.ReadBit("HasAnimProgress", index);
                }

                if (packet.ReadBit("HasAreaTriggerSphere", index))
                    areaTriggerTemplate.Type = (byte)AreaTriggerType.Sphere;

                if (packet.ReadBit("HasAreaTriggerBox", index))
                    areaTriggerTemplate.Type = (byte)AreaTriggerType.Box;

                if (packet.ReadBit("HasAreaTriggerPolygon", index))
                    areaTriggerTemplate.Type = (byte)AreaTriggerType.Polygon;

                if (packet.ReadBit("HasAreaTriggerCylinder", index))
                    areaTriggerTemplate.Type = (byte)AreaTriggerType.Cylinder;

                if (ClientVersion.AddedInVersion(ClientVersionBuild.V9_2_0_42423))
                    if (packet.ReadBit("HasAreaTriggerDisk", index))
                        areaTriggerTemplate.Type = (byte)AreaTriggerType.Disk;

                bool hasAreaTriggerSpline = packet.ReadBit("HasAreaTriggerSpline", index);

                if (packet.ReadBit("HasAreaTriggerOrbit", index))
                    areaTriggerTemplate.Flags |= (uint)AreaTriggerFlags.HasOrbit;

                if (ClientVersion.AddedInVersion(ClientType.Shadowlands))
                    if (packet.ReadBit("HasAreaTriggerMovementScript", index)) // seen with spellid 343597
                        areaTriggerTemplate.Flags |= (uint)AreaTriggerFlags.HasMovementScript;

                if ((areaTriggerTemplate.Flags & (uint)AreaTriggerFlags.Unk3) != 0)
                    packet.ReadBit();

                if (hasAreaTriggerSpline)
                    foreach (var splinePoint in AreaTriggerHandler.ReadAreaTriggerSpline(guid, packet, index, "AreaTriggerSpline"))
                        Storage.AreaTriggerCreatePropertiesSplinePoints.Add(splinePoint);

                if ((areaTriggerTemplate.Flags & (uint)AreaTriggerFlags.HasTargetRollPitchYaw) != 0)
                    packet.ReadVector3("TargetRollPitchYaw", index);

                if (hasScaleCurveID)
                    spellAreaTrigger.ScaleCurveId = (int)packet.ReadUInt32("ScaleCurveID", index);

                if (hasMorphCurveID)
                    spellAreaTrigger.MorphCurveId = (int)packet.ReadUInt32("MorphCurveID", index);

                if (hasFacingCurveID)
                    spellAreaTrigger.FacingCurveId = (int)packet.ReadUInt32("FacingCurveID", index);

                if (hasMoveCurveID)
                    spellAreaTrigger.MoveCurveId = (int)packet.ReadUInt32("MoveCurveID", index);

                if ((areaTriggerTemplate.Flags & (int)AreaTriggerFlags.HasAnimId) != 0)
                    spellAreaTrigger.AnimId = packet.ReadInt32("AnimId", index);

                if ((areaTriggerTemplate.Flags & (int)AreaTriggerFlags.HasAnimKitId) != 0)
                    spellAreaTrigger.AnimKitId = packet.ReadInt32("AnimKitId", index);

                if (hasAnimProgress)
                    packet.ReadUInt32("AnimProgress", index);

                if (areaTriggerTemplate.Type == (byte)AreaTriggerType.Sphere)
                {
                    areaTriggerTemplate.Data[0] = packet.ReadSingle("Radius", index);
                    areaTriggerTemplate.Data[1] = packet.ReadSingle("RadiusTarget", index);
                }

                if (areaTriggerTemplate.Type == (byte)AreaTriggerType.Box)
                {
                    Vector3 Extents = packet.ReadVector3("Extents", index);
                    Vector3 ExtentsTarget = packet.ReadVector3("ExtentsTarget", index);

                    areaTriggerTemplate.Data[0] = Extents.X;
                    areaTriggerTemplate.Data[1] = Extents.Y;
                    areaTriggerTemplate.Data[2] = Extents.Z;

                    areaTriggerTemplate.Data[3] = ExtentsTarget.X;
                    areaTriggerTemplate.Data[4] = ExtentsTarget.Y;
                    areaTriggerTemplate.Data[5] = ExtentsTarget.Z;
                }

                if (areaTriggerTemplate.Type == (byte)AreaTriggerType.Polygon)
                {
                    var verticesCount = packet.ReadUInt32("VerticesCount", index);
                    var verticesTargetCount = packet.ReadUInt32("VerticesTargetCount", index);

                    List<AreaTriggerCreatePropertiesPolygonVertex> verticesList = new List<AreaTriggerCreatePropertiesPolygonVertex>();

                    areaTriggerTemplate.Data[0] = packet.ReadSingle("Height", index);
                    areaTriggerTemplate.Data[1] = packet.ReadSingle("HeightTarget", index);

                    for (uint i = 0; i < verticesCount; ++i)
                    {
                        AreaTriggerCreatePropertiesPolygonVertex spellAreatriggerVertices = new AreaTriggerCreatePropertiesPolygonVertex
                        {
                            areatriggerGuid = guid,
                            Idx = i
                        };

                        Vector2 vertices = packet.ReadVector2("Vertices", index, i);

                        spellAreatriggerVertices.VerticeX = vertices.X;
                        spellAreatriggerVertices.VerticeY = vertices.Y;

                        verticesList.Add(spellAreatriggerVertices);
                    }

                    for (var i = 0; i < verticesTargetCount; ++i)
                    {
                        Vector2 verticesTarget = packet.ReadVector2("VerticesTarget", index, i);

                        verticesList[i].VerticeTargetX = verticesTarget.X;
                        verticesList[i].VerticeTargetY = verticesTarget.Y;
                    }

                    foreach (AreaTriggerCreatePropertiesPolygonVertex vertice in verticesList)
                        Storage.AreaTriggerCreatePropertiesPolygonVertices.Add(vertice);
                }

                if (areaTriggerTemplate.Type == (byte)AreaTriggerType.Cylinder)
                {
                    areaTriggerTemplate.Data[0] = packet.ReadSingle("Radius", index);
                    areaTriggerTemplate.Data[1] = packet.ReadSingle("RadiusTarget", index);
                    areaTriggerTemplate.Data[2] = packet.ReadSingle("Height", index);
                    areaTriggerTemplate.Data[3] = packet.ReadSingle("HeightTarget", index);
                    areaTriggerTemplate.Data[4] = packet.ReadSingle("LocationZOffset", index);
                    areaTriggerTemplate.Data[5] = packet.ReadSingle("LocationZOffsetTarget", index);
                }

                if (areaTriggerTemplate.Type == (byte)AreaTriggerType.Disk)
                {
                    areaTriggerTemplate.Data[0] = packet.ReadSingle("InnerRadius", index);
                    areaTriggerTemplate.Data[1] = packet.ReadSingle("InnerRadiusTarget", index);
                    areaTriggerTemplate.Data[2] = packet.ReadSingle("OuterRadius", index);
                    areaTriggerTemplate.Data[3] = packet.ReadSingle("OuterRadiusTarget", index);
                    areaTriggerTemplate.Data[4] = packet.ReadSingle("Height", index);
                    areaTriggerTemplate.Data[5] = packet.ReadSingle("HeightTarget", index);
                    areaTriggerTemplate.Data[6] = packet.ReadSingle("LocationZOffset", index);
                    areaTriggerTemplate.Data[7] = packet.ReadSingle("LocationZOffsetTarget", index);
                }

                if ((areaTriggerTemplate.Flags & (uint)AreaTriggerFlags.HasMovementScript) != 0)
                {
                    packet.ReadInt32("SpellScriptID");
                    packet.ReadVector3("Center");
                }

                if ((areaTriggerTemplate.Flags & (uint)AreaTriggerFlags.HasOrbit) != 0)
                    Storage.AreaTriggerCreatePropertiesOrbits.Add(AreaTriggerHandler.ReadAreaTriggerOrbit(guid, packet, index, "AreaTriggerOrbit"));

                spellAreaTrigger.Shape = areaTriggerTemplate.Type;
                Array.Copy(areaTriggerTemplate.Data, spellAreaTrigger.ShapeData, Math.Min(areaTriggerTemplate.Data.Length, spellAreaTrigger.ShapeData.Length));

                Storage.AreaTriggerTemplates.Add(areaTriggerTemplate);
            }

            if (hasGameObject)
            {
                packet.ResetBitReader();
                var worldEffectId = packet.ReadUInt32("WorldEffectID", index);
                if (worldEffectId != 0 && obj is GameObject)
                    (obj as GameObject).WorldEffectID = worldEffectId;

                var bit8 = packet.ReadBit("bit8", index);
                if (bit8)
                    packet.ReadUInt32("Int1", index);
            }

            if (hasSmoothPhasing)
            {
                packet.ResetBitReader();
                packet.ReadBit("ReplaceActive", index);
                if (ClientVersion.AddedInVersion(ClientType.Shadowlands))
                    packet.ReadBit("StopAnimKits", index);

                var replaceObject = packet.ReadBit();
                if (replaceObject)
                    packet.ReadPackedGuid128("ReplaceObject", index);
            }

            if (sceneObjCreate)
            {
                packet.ResetBitReader();

                var hasSceneLocalScriptData = packet.ReadBit("HasSceneLocalScriptData", index);
                var petBattleFullUpdate = packet.ReadBit("HasPetBattleFullUpdate", index);

                if (hasSceneLocalScriptData)
                {
                    packet.ResetBitReader();
                    var dataLength = packet.ReadBits(7);
                    packet.ReadWoWString("Data", dataLength, index);
                }

                if (petBattleFullUpdate)
                    V6_0_2_19033.Parsers.BattlePetHandler.ReadPetBattleFullUpdate(packet, index);
            }

            if (playerCreateData)
            {
                packet.ResetBitReader();
                var hasSceneInstanceIDs = packet.ReadBit("ScenePendingInstances", index);
                var hasRuneState = packet.ReadBit("Runes", index);

                if (hasSceneInstanceIDs)
                {
                    var sceneInstanceIDs = packet.ReadUInt32("SceneInstanceIDsCount");
                    for (var i = 0; i < sceneInstanceIDs; ++i)
                        packet.ReadInt32("SceneInstanceIDs", index, i);
                }

                if (hasRuneState)
                {
                    packet.ReadByte("RechargingRuneMask", index);
                    packet.ReadByte("UsableRuneMask", index);
                    var runeCount = packet.ReadUInt32();
                    for (var i = 0; i < runeCount; ++i)
                        packet.ReadByte("RuneCooldown", index, i);
                }
            }

            if (hasConversation)
            {
                packet.ResetBitReader();
                if (packet.ReadBit("HasTextureKitID", index))
                    (obj as ConversationTemplate).TextureKitId = packet.ReadUInt32("TextureKitID", index);
            }

            return moveInfo;
        }
    }
}

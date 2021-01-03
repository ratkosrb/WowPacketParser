using System.Collections.Generic;
using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;
using WowPacketParser.Store;
using WowPacketParser.Store.Objects;
using WowPacketParserModule.V7_0_3_22248.Parsers;
using CoreParsers = WowPacketParser.Parsing.Parsers;
using SplineFlag = WowPacketParserModule.V7_0_3_22248.Enums.SplineFlag;
using CoreFields = WowPacketParser.Enums.Version;
using WowPacketParser.Store.Objects.UpdateFields;
using System;

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
            var hasDestroyObject = packet.ReadBit("HasDestroyObjects");
            if (hasDestroyObject)
            {
                packet.ReadInt16("Int0");
                var destroyObjCount = packet.ReadUInt32("DestroyObjectsCount");
                for (var i = 0; i < destroyObjCount; i++)
                {
                    WowGuid guid = packet.ReadPackedGuid128("Object GUID", i);
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
                                WoWObject obj;
                                Storage.Objects.TryGetValue(guid, out obj);

                                IObjectData oldObjectData = null;
                                IGameObjectData oldGameObjectData = null;
                                IUnitData oldUnitData = null;
                                IPlayerData oldPlayerData = null;

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
                                    handler.ReadUpdateActivePlayerData(fieldsData, null, i);
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
                                    StoreObjectUpdate(packet.Time, guid, obj, oldObjectData, oldGameObjectData, oldUnitData, oldPlayerData);
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

        public static void StoreObjectUpdate(DateTime time, WowGuid guid, WoWObject obj, IObjectData oldObjectData, IGameObjectData oldGameObjectData, IUnitData oldUnitData, IPlayerData oldPlayerData)
        {
            if ((guid.GetObjectType() == ObjectType.Unit) ||
                (guid.GetObjectType() == ObjectType.Player) ||
                (guid.GetObjectType() == ObjectType.ActivePlayer))
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
                }
                if (oldUnitData != null)
                {
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
                    if (oldUnitData.PetTalentPoints != unit.UnitData.PetTalentPoints)
                    {
                        hasData = true;
                        creatureUpdate.PetTalentPoints = unit.UnitData.PetTalentPoints;
                    }
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
                    if (oldUnitData.PetFlags != unit.UnitData.PetFlags)
                    {
                        hasData = true;
                        creatureUpdate.PetFlags = unit.UnitData.PetFlags;
                    }
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
                        hasData = true;
                        creatureUpdate.UnitFlag = unit.UnitData.Flags;
                    }
                    if (oldUnitData.Flags2 != unit.UnitData.Flags2)
                    {
                        hasData = true;
                        creatureUpdate.UnitFlag2 = unit.UnitData.Flags2;
                    }
                    if (oldUnitData.CurHealth != unit.UnitData.CurHealth)
                    {
                        hasData = true;
                        creatureUpdate.CurrentHealth = (uint)unit.UnitData.CurHealth;
                    }
                    if (oldUnitData.MaxHealth != unit.UnitData.MaxHealth)
                    {
                        hasData = true;
                        creatureUpdate.MaxHealth = (uint)unit.UnitData.MaxHealth;
                    }
                    if (oldUnitData.CurMana != unit.UnitData.CurMana)
                    {
                        hasData = true;
                        creatureUpdate.CurrentMana = (uint)unit.UnitData.CurMana;
                    }
                    if (oldUnitData.MaxMana != unit.UnitData.MaxMana)
                    {
                        hasData = true;
                        creatureUpdate.MaxMana = (uint)unit.UnitData.MaxMana;
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
                    if (oldUnitData.ModRangedHaste != unit.UnitData.ModRangedHaste)
                    {
                        hasData = true;
                        creatureUpdate.ModRangedHaste = unit.UnitData.ModRangedHaste;
                    }
                    if (oldUnitData.AttackRoundBaseTime[0] != unit.UnitData.AttackRoundBaseTime[0])
                    {
                        hasData = true;
                        creatureUpdate.BaseAttackTime = unit.UnitData.AttackRoundBaseTime[0];
                    }
                    if (oldUnitData.RangedAttackRoundBaseTime != unit.UnitData.RangedAttackRoundBaseTime)
                    {
                        hasData = true;
                        creatureUpdate.RangedAttackTime = unit.UnitData.RangedAttackRoundBaseTime;
                    }
                    uint slot = 0;
                    foreach (var item in unit.UnitData.VirtualItems)
                    {
                        if (oldUnitData.VirtualItems[slot].ItemID != unit.UnitData.VirtualItems[slot].ItemID)
                        {
                            CreatureEquipmentValuesUpdate equipmentUpdate = new CreatureEquipmentValuesUpdate();
                            equipmentUpdate.ItemId = (uint)unit.UnitData.VirtualItems[slot].ItemID;
                            equipmentUpdate.Slot = slot;
                            equipmentUpdate.time = time;
                            Storage.StoreUnitEquipmentValuesUpdate(guid, equipmentUpdate);
                        }
                        slot++;
                    }
                    if (oldUnitData.Charm != unit.UnitData.Charm)
                    {
                        CreatureGuidValuesUpdate guidUpdate = new CreatureGuidValuesUpdate();
                        guidUpdate.guid = unit.UnitData.Charm;
                        guidUpdate.time = time;
                        guidUpdate.FieldName = "Charm";
                        Storage.StoreUnitGuidValuesUpdate(guid, guidUpdate);
                    }
                    if (oldUnitData.Summon != unit.UnitData.Summon)
                    {
                        CreatureGuidValuesUpdate guidUpdate = new CreatureGuidValuesUpdate();
                        guidUpdate.guid = unit.UnitData.Summon;
                        guidUpdate.time = time;
                        guidUpdate.FieldName = "Summon";
                        Storage.StoreUnitGuidValuesUpdate(guid, guidUpdate);
                    }
                    if (oldUnitData.CharmedBy != unit.UnitData.CharmedBy)
                    {
                        CreatureGuidValuesUpdate guidUpdate = new CreatureGuidValuesUpdate();
                        guidUpdate.guid = unit.UnitData.CharmedBy;
                        guidUpdate.time = time;
                        guidUpdate.FieldName = "CharmedBy";
                        Storage.StoreUnitGuidValuesUpdate(guid, guidUpdate);
                    }
                    if (oldUnitData.SummonedBy != unit.UnitData.SummonedBy)
                    {
                        CreatureGuidValuesUpdate guidUpdate = new CreatureGuidValuesUpdate();
                        guidUpdate.guid = unit.UnitData.SummonedBy;
                        guidUpdate.time = time;
                        guidUpdate.FieldName = "SummonedBy";
                        Storage.StoreUnitGuidValuesUpdate(guid, guidUpdate);
                    }
                    if (oldUnitData.CreatedBy != unit.UnitData.CreatedBy)
                    {
                        CreatureGuidValuesUpdate guidUpdate = new CreatureGuidValuesUpdate();
                        guidUpdate.guid = unit.UnitData.CreatedBy;
                        guidUpdate.time = time;
                        guidUpdate.FieldName = "CreatedBy";
                        Storage.StoreUnitGuidValuesUpdate(guid, guidUpdate);
                    }
                    if (oldUnitData.Target != unit.UnitData.Target)
                    {
                        CreatureGuidValuesUpdate guidUpdate = new CreatureGuidValuesUpdate();
                        guidUpdate.guid = unit.UnitData.Target;
                        guidUpdate.time = time;
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
                            equipmentUpdate.time = time;
                            Storage.StoreUnitEquipmentValuesUpdate(guid, equipmentUpdate);
                        }
                        slot++;
                    }
                }
                if (hasData)
                {
                    creatureUpdate.UnixTimeMs = (ulong)Utilities.GetUnixTimeMsFromDateTime(time);
                    Storage.StoreUnitValuesUpdate(guid, creatureUpdate);
                }
            }
            else if (guid.GetObjectType() == ObjectType.GameObject && oldGameObjectData != null)
            {
                GameObject go = obj as GameObject;
                bool hasData = false;
                GameObjectUpdate goUpdate = new GameObjectUpdate();
                if (oldGameObjectData.DisplayID != go.GameObjectData.DisplayID)
                {
                    hasData = true;
                    goUpdate.DisplayID = (uint)go.GameObjectData.DisplayID;
                }
                if (oldGameObjectData.Level != go.GameObjectData.Level)
                {
                    hasData = true;
                    goUpdate.Level = (uint)go.GameObjectData.Level;
                }
                if (oldGameObjectData.FactionTemplate != go.GameObjectData.FactionTemplate)
                {
                    hasData = true;
                    goUpdate.Faction = (uint)go.GameObjectData.FactionTemplate;
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
                if (hasData)
                {
                    goUpdate.UnixTimeMs = (ulong)Utilities.GetUnixTimeMsFromDateTime(time);
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
            switch (objType)
            {
                case ObjectType.Unit:
                    obj = new Unit();
                    break;
                case ObjectType.GameObject:
                    obj = new GameObject();
                    break;
                case ObjectType.DynamicObject:
                    obj = new DynamicObject();
                    break;
                case ObjectType.Player:
                    obj = new Player();
                    break;
                case ObjectType.ActivePlayer:
                    Player me = new Player();
                    me.IsActivePlayer = true;
                    obj = me;
                    break;
                case ObjectType.AreaTrigger:
                    obj = new SpellAreaTrigger();
                    break;
                case ObjectType.Conversation:
                    obj = new ConversationTemplate();
                    break;
                default:
                    obj = new WoWObject();
                    break;
            }

            var moves = ReadMovementUpdateBlock(packet, guid, obj, index);
            Storage.StoreObjectCreateTime(guid, moves, packet.Time, type);
            if (ClientVersion.AddedInVersion(ClientVersionBuild.V8_1_0_28724))
            {
                var updatefieldSize = packet.ReadUInt32();
                using (var fieldsData = new Packet(packet.ReadBytes((int)updatefieldSize), packet.Opcode, packet.Time, packet.Direction, packet.Number, packet.Writer, packet.FileName))
                {
                    var flags = fieldsData.ReadByteE<UpdateFieldFlag>("FieldFlags", index);
                    var handler = CoreFields.UpdateFields.GetHandler();
                    obj.ObjectData = handler.ReadCreateObjectData(fieldsData, flags, index);
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
                            (obj as Unit).UnitData = handler.ReadCreateUnitData(fieldsData, flags, index);
                            (obj as Unit).UnitDataOriginal = (obj as Unit).UnitData.Clone();
                            break;
                        case ObjectType.Player:
                            (obj as Unit).UnitData = handler.ReadCreateUnitData(fieldsData, flags, index);
                            (obj as Unit).UnitDataOriginal = (obj as Unit).UnitData.Clone();
                            (obj as Player).PlayerData = handler.ReadCreatePlayerData(fieldsData, flags, index);
                            (obj as Player).PlayerDataOriginal = (obj as Player).PlayerData.Clone();
                            break;
                        case ObjectType.ActivePlayer:
                            (obj as Unit).UnitData = handler.ReadCreateUnitData(fieldsData, flags, index);
                            (obj as Unit).UnitDataOriginal = (obj as Unit).UnitData.Clone();
                            (obj as Player).PlayerData = handler.ReadCreatePlayerData(fieldsData, flags, index);
                            (obj as Player).PlayerDataOriginal = (obj as Player).PlayerData.Clone();
                            handler.ReadCreateActivePlayerData(fieldsData, flags, index);
                            break;
                        case ObjectType.GameObject:
                            (obj as GameObject).GameObjectData = handler.ReadCreateGameObjectData(fieldsData, flags, index);
                            (obj as GameObject).GameObjectDataOriginal = (obj as GameObject).GameObjectData.Clone();
                            break;
                        case ObjectType.DynamicObject:
                            (obj as DynamicObject).DynamicObjectData = handler.ReadCreateDynamicObjectData(fieldsData, flags, index);
                            (obj as DynamicObject).DynamicObjectDataOriginal = (obj as DynamicObject).DynamicObjectData;
                            break;
                        case ObjectType.Corpse:
                            handler.ReadCreateCorpseData(fieldsData, flags, index);
                            break;
                        case ObjectType.AreaTrigger:
                            (obj as SpellAreaTrigger).AreaTriggerData = handler.ReadCreateAreaTriggerData(fieldsData, flags, index);
                            break;
                        case ObjectType.SceneObject:
                            handler.ReadCreateSceneObjectData(fieldsData, flags, index);
                            break;
                        case ObjectType.Conversation:
                            (obj as ConversationTemplate).ConversationData = handler.ReadCreateConversationData(fieldsData, flags, index);
                            break;
                    }
                }
            }
            else
            {
                var updates = CoreParsers.UpdateHandler.ReadValuesUpdateBlockOnCreate(packet, objType, index);
                var dynamicUpdates = CoreParsers.UpdateHandler.ReadDynamicValuesUpdateBlockOnCreate(packet, objType, index);

                obj.UpdateFields = updates;
                obj.DynamicUpdateFields = dynamicUpdates;
            }

            obj.Type = objType;
            obj.Movement = moves;
            obj.Map = map;
            obj.Area = CoreParsers.WorldStateHandler.CurrentAreaId;
            obj.Zone = CoreParsers.WorldStateHandler.CurrentZoneId;
            obj.PhaseMask = (uint)CoreParsers.MovementHandler.CurrentPhaseMask;
            obj.Phases = new HashSet<ushort>(CoreParsers.MovementHandler.ActivePhases.Keys);
            obj.DifficultyID = CoreParsers.MovementHandler.CurrentDifficultyID;

            // If this is the second time we see the same object (same guid,
            // same position) update its phasemask
            if (Storage.Objects.ContainsKey(guid))
            {
                var existObj = Storage.Objects[guid].Item1;
                CoreParsers.UpdateHandler.ProcessExistingObject(ref existObj, guid, packet.Time, obj.UpdateFields, obj.DynamicUpdateFields, moves); // can't do "ref Storage.Objects[guid].Item1 directly
            }
            else
                Storage.StoreNewObject(guid, obj, packet);

            if (guid.HasEntry() && (objType == ObjectType.Unit || objType == ObjectType.GameObject))
                packet.AddSniffData(Utilities.ObjectTypeToStore(objType), (int)guid.GetEntry(), "SPAWN");
        }

        public static void ReadTransportData(MovementInfo moveInfo, WowGuid guid, Packet packet, object index)
        {
            packet.ResetBitReader();
            moveInfo.TransportGuid = packet.ReadPackedGuid128("TransportGUID", index);
            moveInfo.TransportOffset = packet.ReadVector4("TransportPosition", index);
            var seat = packet.ReadByte("VehicleSeatIndex", index);
            packet.ReadUInt32("MoveTime", index);

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
                    SeatId = seat
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
            {
                ActivePlayerCreateTime activePlayer = new ActivePlayerCreateTime
                {
                    Guid = guid,
                    Time = packet.Time,
                };
                Storage.PlayerActiveCreateTime.Add(activePlayer);
            }

            var sceneObjCreate = packet.ReadBit("SceneObjCreate", index);
            var playerCreateData = packet.ReadBit("HasPlayerCreateData", index);
            var hasConversation = packet.ReadBit("HasConversation", index);

            if (hasMovementUpdate)
            {
                packet.ResetBitReader();
                packet.ReadPackedGuid128("MoverGUID", index);

                packet.ReadUInt32("MoveTime", index);
                moveInfo.Position = packet.ReadVector3("Position", index);
                moveInfo.Orientation = packet.ReadSingle("Orientation", index);

                packet.ReadSingle("Pitch", index);
                packet.ReadSingle("StepUpStartElevation", index);

                var removeForcesIDsCount = packet.ReadInt32();
                packet.ReadInt32("MoveIndex", index);

                for (var i = 0; i < removeForcesIDsCount; i++)
                    packet.ReadPackedGuid128("RemoveForcesIDs", index, i);

                moveInfo.Flags = (MovementFlag)packet.ReadBitsE<V6_0_2_19033.Enums.MovementFlag>("Movement Flags", 30, index);
                moveInfo.FlagsExtra = (MovementFlagExtra)packet.ReadBitsE<Enums.MovementFlags2>("Extra Movement Flags", 18, index);

                var hasTransport = packet.ReadBit("Has Transport Data", index);
                var hasFall = packet.ReadBit("Has Fall Data", index);
                packet.ReadBit("HasSpline", index);
                packet.ReadBit("HeightChangeFailed", index);
                packet.ReadBit("RemoteTimeValid", index);

                if (hasTransport)
                    ReadTransportData(moveInfo, guid, packet, index);

                if (hasFall)
                {
                    packet.ResetBitReader();
                    packet.ReadUInt32("Fall Time", index);
                    packet.ReadSingle("JumpVelocity", index);

                    var hasFallDirection = packet.ReadBit("Has Fall Direction", index);
                    if (hasFallDirection)
                    {
                        packet.ReadVector2("Fall", index);
                        packet.ReadSingle("Horizontal Speed", index);
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

                        packet.ReadUInt32E<SplineFlag>("SplineFlags", index);
                        packet.ReadInt32("Elapsed", index);
                        packet.ReadUInt32("Duration", index);
                        packet.ReadSingle("DurationModifier", index);
                        packet.ReadSingle("NextDurationModifier", index);

                        var face = packet.ReadBits("Face", 2, index);
                        var hasSpecialTime = packet.ReadBit("HasSpecialTime", index);

                        var pointsCount = packet.ReadBits("PointsCount", 16, index);

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

                        switch (face)
                        {
                            case 1:
                                packet.ReadVector3("FaceSpot", index);
                                break;
                            case 2:
                                packet.ReadPackedGuid128("FaceGUID", index);
                                break;
                            case 3:
                                packet.ReadSingle("FaceDirection", index);
                                break;
                            default:
                                break;
                        }

                        if (hasSpecialTime)
                            packet.ReadUInt32("SpecialTime", index);

                        for (var i = 0; i < pointsCount; ++i)
                            packet.ReadVector3("Points", index, i);

                        if (hasSpellEffectExtraData)
                            MovementHandler.ReadMonsterSplineSpellEffectExtraData(packet, index);

                        if (hasJumpExtraData)
                            MovementHandler.ReadMonsterSplineJumpExtraData(packet, index);

                        if (hasAnimationTierTransition)
                        {
                            packet.ReadInt32("TierTransitionID", index);
                            packet.ReadInt32("StartTime", index);
                            packet.ReadInt32("EndTime", index);
                            packet.ReadByte("AnimTier", index);
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
                packet.ReadPackedGuid128("CombatVictim Guid", index);

            if (hasServerTime)
                packet.ReadUInt32("ServerTime", index);

            if (hasVehicleCreate)
            {
                moveInfo.VehicleId = (uint)packet.ReadInt32("RecID", index);
                packet.ReadSingle("InitialRawFacing", index);
            }

            if (hasAnimKitCreate)
            {
                packet.ReadUInt16("AiID", index);
                packet.ReadUInt16("MovementID", index);
                packet.ReadUInt16("MeleeID", index);
            }

            if (hasRotation)
                moveInfo.Rotation = packet.ReadPackedQuaternion("GameObject Rotation", index);

            for (var i = 0; i < pauseTimesCount; ++i)
                packet.ReadUInt32("PauseTimes", index, i);

            if (hasMovementTransport)
                ReadTransportData(moveInfo, guid, packet, index);

            if (hasAreaTrigger && obj is SpellAreaTrigger)
            {
                AreaTriggerTemplate areaTriggerTemplate = new AreaTriggerTemplate
                {
                    Id = guid.GetEntry()
                };

                SpellAreaTrigger spellAreaTrigger = (SpellAreaTrigger)obj;
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

                if (packet.ReadBit("HasAnimID", index))
                    areaTriggerTemplate.Flags |= (uint)AreaTriggerFlags.HasAnimId;

                if (packet.ReadBit("HasAnimKitID", index))
                    areaTriggerTemplate.Flags |= (uint)AreaTriggerFlags.HasAnimKitId;

                if (packet.ReadBit("unkbit50", index))
                    areaTriggerTemplate.Flags |= (uint)AreaTriggerFlags.Unk3;

                bool hasUnk801 = packet.ReadBit("unkbit801", index);

                if (packet.ReadBit("HasAreaTriggerSphere", index))
                    areaTriggerTemplate.Type = (byte)AreaTriggerType.Sphere;

                if (packet.ReadBit("HasAreaTriggerBox", index))
                    areaTriggerTemplate.Type = (byte)AreaTriggerType.Box;

                if (packet.ReadBit("HasAreaTriggerPolygon", index))
                    areaTriggerTemplate.Type = (byte)AreaTriggerType.Polygon;

                if (packet.ReadBit("HasAreaTriggerCylinder", index))
                    areaTriggerTemplate.Type = (byte)AreaTriggerType.Cylinder;

                bool hasAreaTriggerSpline = packet.ReadBit("HasAreaTriggerSpline", index);

                if (packet.ReadBit("HasAreaTriggerCircularMovement", index))
                    areaTriggerTemplate.Flags |= (uint)AreaTriggerFlags.HasCircularMovement;

                if ((areaTriggerTemplate.Flags & (uint)AreaTriggerFlags.Unk3) != 0)
                    packet.ReadBit();

                if (hasAreaTriggerSpline)
                    AreaTriggerHandler.ReadAreaTriggerSpline(packet, index);

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

                if (hasUnk801)
                    packet.ReadUInt32("Unk801", index);

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

                    List<AreaTriggerTemplateVertices> verticesList = new List<AreaTriggerTemplateVertices>();

                    areaTriggerTemplate.Data[0] = packet.ReadSingle("Height", index);
                    areaTriggerTemplate.Data[1] = packet.ReadSingle("HeightTarget", index);

                    for (uint i = 0; i < verticesCount; ++i)
                    {
                        AreaTriggerTemplateVertices areaTriggerTemplateVertices = new AreaTriggerTemplateVertices
                        {
                            AreaTriggerId = guid.GetEntry(),
                            Idx = i
                        };

                        Vector2 vertices = packet.ReadVector2("Vertices", index, i);

                        areaTriggerTemplateVertices.VerticeX = vertices.X;
                        areaTriggerTemplateVertices.VerticeY = vertices.Y;

                        verticesList.Add(areaTriggerTemplateVertices);
                    }

                    for (var i = 0; i < verticesTargetCount; ++i)
                    {
                        Vector2 verticesTarget = packet.ReadVector2("VerticesTarget", index, i);

                        verticesList[i].VerticeTargetX = verticesTarget.X;
                        verticesList[i].VerticeTargetY = verticesTarget.Y;
                    }

                    foreach (AreaTriggerTemplateVertices vertice in verticesList)
                        Storage.AreaTriggerTemplatesVertices.Add(vertice);
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

                if ((areaTriggerTemplate.Flags & (uint)AreaTriggerFlags.HasCircularMovement) != 0)
                {
                    packet.ResetBitReader();
                    var hasPathTarget = packet.ReadBit("HasPathTarget");
                    var hasCenter = packet.ReadBit("HasCenter", index);
                    packet.ReadBit("CounterClockwise", index);
                    packet.ReadBit("CanLoop", index);

                    packet.ReadUInt32("TimeToTarget", index);
                    packet.ReadInt32("ElapsedTimeForMovement", index);
                    packet.ReadUInt32("StartDelay", index);
                    packet.ReadSingle("Radius", index);
                    packet.ReadSingle("BlendFromRadius", index);
                    packet.ReadSingle("InitialAngel", index);
                    packet.ReadSingle("ZOffset", index);

                    if (hasPathTarget)
                        packet.ReadPackedGuid128("PathTarget", index);

                    if (hasCenter)
                        packet.ReadVector3("Center", index);
                }

                Storage.AreaTriggerTemplates.Add(areaTriggerTemplate);
            }

            if (hasGameObject)
            {
                packet.ResetBitReader();
                packet.ReadUInt32("WorldEffectID", index);

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

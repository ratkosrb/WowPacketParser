using System;
using System.Collections;
using System.Collections.Generic;
using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;
using WowPacketParser.Store;
using WowPacketParser.Store.Objects;
using WowPacketParser.Store.Objects.UpdateFields;
using CoreFields = WowPacketParser.Enums.Version;
using WowPacketParserModule.V7_0_3_22248.Parsers;
using CoreParsers = WowPacketParser.Parsing.Parsers;
using MovementFlag = WowPacketParser.Enums.v4.MovementFlag;
using MovementFlag2 = WowPacketParser.Enums.v7.MovementFlag2;
using SplineFlag = WowPacketParserModule.V7_0_3_22248.Enums.SplineFlag;

namespace WowPacketParserModule.V2_5_1_38707.Parsers
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
                        if (ClientVersion.IsUsingNewUpdateFieldSystem())
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
                                {
                                    V8_0_1_27101.Parsers.UpdateHandler.StoreObjectUpdate(packet, guid, obj, oldObjectData, oldGameObjectData, oldUnitData, oldPlayerData, oldActivePlayerData, false);

                                    if (guid.GetObjectType() == ObjectType.Unit)
                                        Storage.StoreCreatureStats(obj as Unit, null, guid.GetHighType() == HighGuidType.Pet, packet);
                                }
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

        private static void ReadCreateObjectBlock(Packet packet, WowGuid guid, uint map, object index, ObjectCreateType type)
        {
            ObjectType objType;
            if (!ClientVersion.IsUsingNewUpdateFieldSystem())
            {
                objType = ObjectTypeConverter.Convert(packet.ReadByteE<ObjectTypeBCC>("Object Type", index));
                packet.ReadInt32("HeirFlags", index);
            }
            else
                objType = ObjectTypeConverter.Convert(packet.ReadByteE<ObjectType801>("Object Type", index));

            WoWObject obj;
            bool isExistingObject = Storage.Objects.ContainsKey(guid);
            if (isExistingObject)
                obj = Storage.Objects[guid].Item1;
            else
                obj = CoreParsers.UpdateHandler.CreateObject(objType, map);

            BitArray updateMaskArray = null;
            var moves = ReadMovementUpdateBlock(packet, guid, obj, index);
            Storage.StoreObjectCreateTime(guid, map, moves, packet, type);

            if (ClientVersion.IsUsingNewUpdateFieldSystem())
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
                                oldUnitData = (obj as Unit).UnitData.Clone(); ;
                            (obj as Unit).UnitData = handler.ReadCreateUnitData(fieldsData, flags, index);
                            if (!isExistingObject)
                                (obj as Unit).UnitDataOriginal = (obj as Unit).UnitData.Clone();
                            break;
                        case ObjectType.Player:
                            if (isExistingObject && (obj as Unit).UnitData != null)
                                oldUnitData = (obj as Unit).UnitData.Clone(); ;
                            (obj as Unit).UnitData = handler.ReadCreateUnitData(fieldsData, flags, index);
                            if (!isExistingObject)
                                (obj as Unit).UnitDataOriginal = (obj as Unit).UnitData.Clone();

                            if (isExistingObject && (obj as Player).PlayerData != null)
                                oldPlayerData = (obj as Player).PlayerData.Clone(); ;
                            (obj as Player).PlayerData = handler.ReadCreatePlayerData(fieldsData, flags, index);
                            if (!isExistingObject)
                                (obj as Player).PlayerDataOriginal = (obj as Player).PlayerData.Clone();
                            break;
                        case ObjectType.ActivePlayer:
                            if (isExistingObject && (obj as Unit).UnitData != null)
                                oldUnitData = (obj as Unit).UnitData.Clone(); ;
                            (obj as Unit).UnitData = handler.ReadCreateUnitData(fieldsData, flags, index);
                            if (!isExistingObject)
                                (obj as Unit).UnitDataOriginal = (obj as Unit).UnitData.Clone();

                            if (isExistingObject && (obj as Player).PlayerData != null)
                                oldPlayerData = (obj as Player).PlayerData.Clone(); ;
                            (obj as Player).PlayerData = handler.ReadCreatePlayerData(fieldsData, flags, index);
                            if (!isExistingObject)
                                (obj as Player).PlayerDataOriginal = (obj as Player).PlayerData.Clone();

                            if (isExistingObject && (obj as Player).ActivePlayerData != null)
                                oldActivePlayerData = (obj as Player).ActivePlayerData.Clone(); ;
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
                        V8_0_1_27101.Parsers.UpdateHandler.StoreObjectUpdate(packet, guid, obj, oldObjectData, oldGameObjectData, oldUnitData, oldPlayerData, oldActivePlayerData, true);

                    if (objType == ObjectType.Unit)
                        Storage.StoreCreatureStats(obj as Unit, null, guid.GetHighType() == HighGuidType.Pet, packet);
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

                if (ClientVersion.IsVersionWithUpdatedMovementInfo())
                {
                    moveInfo.Flags = (uint)packet.ReadUInt32E<MovementFlag>("Movement Flags", index);
                    moveInfo.Flags2 = (uint)packet.ReadUInt32E<MovementFlag2>("Movement Flags 2", index);
                    moveInfo.Flags3 = (uint)packet.ReadUInt32E<MovementFlag3>("Movement Flags 3", index);
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

                if (!ClientVersion.IsVersionWithUpdatedMovementInfo())
                {
                    moveInfo.Flags = (uint)packet.ReadBitsE<MovementFlag>("Movement Flags", 30, index);
                    moveInfo.Flags2 = (uint)packet.ReadBitsE<MovementFlag2>("Extra Movement Flags", 18, index);
                }

                var hasStandingOnGameObjectGUID = false;
                if (ClientVersion.AddedInVersion(ClientVersionBuild.V3_4_2_50063))
                    hasStandingOnGameObjectGUID = packet.ReadBit("HasStandingOnGameObjectGUID", index);

                var hasTransport = packet.ReadBit("Has Transport Data", index);
                var hasFall = packet.ReadBit("Has Fall Data", index);
                packet.ReadBit("HasSpline", index);
                packet.ReadBit("HeightChangeFailed", index);
                packet.ReadBit("RemoteTimeValid", index);
                var hasInertia = ClientVersion.IsVersionWithUpdatedMovementInfo() && packet.ReadBit("Has Inertia", index);

                var hasAdvFlying = ClientVersion.AddedInClassicVersion(1, 14, 4, 3, 4, 1) && packet.ReadBit("HasAdvFlying", index);

                if (hasTransport)
                    V8_0_1_27101.Parsers.UpdateHandler.ReadTransportData(moveInfo, guid, packet, index);

                if (hasStandingOnGameObjectGUID)
                    packet.ReadPackedGuid128("StandingOnGameObjectGUID", index);

                if (hasInertia)
                {
                    if (ClientVersion.AddedInClassicVersion(1, 14, 4, 3, 4, 1))
                        packet.ReadInt32("ID", "Inertia");
                    else
                        packet.ReadPackedGuid128("GUID", index, "Inertia");

                    packet.ReadVector3("Force", index, "Inertia");
                    packet.ReadUInt32("Lifetime", index, "Inertia");
                }

                if (hasAdvFlying)
                {
                    packet.ReadSingle("ForwardVelocity", index, "AdvFlying");
                    packet.ReadSingle("UpVelocity", index, "AdvFlying");
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

                var movementForceCount = packet.ReadInt32("MovementForceCount", index);

                packet.ReadSingle("MovementForcesModMagnitude", index);

                if (ClientVersion.AddedInClassicVersion(1, 14, 4, 3, 4, 1))
                {
                    packet.ReadSingle("AdvFlyingAirFriction", index);
                    packet.ReadSingle("AdvFlyingMaxVel", index);
                    packet.ReadSingle("AdvFlyingLiftCoefficient", index);
                    packet.ReadSingle("AdvFlyingDoubleJumpVelMod", index);
                    packet.ReadSingle("AdvFlyingGlideStartMinHeight", index);
                    packet.ReadSingle("AdvFlyingAddImpulseMaxSpeed", index);
                    packet.ReadSingle("AdvFlyingMinBankingRate", index);
                    packet.ReadSingle("AdvFlyingMaxBankingRate", index);
                    packet.ReadSingle("AdvFlyingMinPitchingRateDown", index);
                    packet.ReadSingle("AdvFlyingMaxPitchingRateDown", index);
                    packet.ReadSingle("AdvFlyingMinPitchingRateUp", index);
                    packet.ReadSingle("AdvFlyingMaxPitchingRateUp", index);
                    packet.ReadSingle("AdvFlyingMinTurnVelocityThreshold", index);
                    packet.ReadSingle("AdvFlyingMaxTurnVelocityThreshold", index);
                    packet.ReadSingle("AdvFlyingSurfaceFriction", index);
                    packet.ReadSingle("AdvFlyingOverMaxDeceleration", index);
                    packet.ReadSingle("AdvFlyingLaunchSpeedCoefficient", index);
                }

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

                        var hasSplineFilterKey = packet.ReadBit("HasSplineFilterKey", index);
                        var hasSpellEffectExtraData = packet.ReadBit("HasSpellEffectExtraData", index);
                        var hasJumpExtraData = packet.ReadBit("HasJumpExtraData", index);

                        var hasAnimationTierTransition = packet.ReadBit("HasAnimationTierTransition", index);
                        var hasSpellVisualData = false;
                        if (ClientVersion.RemovedInVersion(ClientVersionBuild.V3_4_3_51505))
                            hasSpellVisualData = packet.ReadBit("HasSpellVisualData", index);

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
                            V8_0_1_27101.Parsers.MovementHandler.ReadMonsterSplineSpellEffectExtraData(packet, index);

                        if (hasJumpExtraData)
                            V8_0_1_27101.Parsers.MovementHandler.ReadMonsterSplineJumpExtraData(monsterMove, packet, index);

                        if (hasAnimationTierTransition)
                        {
                            packet.ReadInt32("TierTransitionID", index);
                            monsterMove.EffectStartTime = packet.ReadUInt32("StartTime", index);
                            packet.ReadInt32("EndTime", index);
                            monsterMove.AnimTier = packet.ReadByte("AnimTier", index);
                        }

                        if (hasSpellVisualData)
                        {
                            for (var i = 0; i < 16; ++i)
                            {
                                packet.ReadInt32("SpellID", index, "SpellVisualData", i);
                                uint temp;
                                V9_0_1_36216.Parsers.SpellHandler.ReadSpellCastVisual(out temp, packet, index, "SpellVisualData", i, "Visual");
                                packet.ReadInt32("StartNodeIndex", index, "SpellVisualData", i);
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
                moveInfo.VehicleId = packet.ReadUInt32("RecID", index);
                moveInfo.VehicleOrientation = packet.ReadSingle("InitialRawFacing", index);
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
                packet.ReadInt32("PauseTimes", index, i);

            if (hasMovementTransport)
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

                areaTriggerTemplate.Flags = 0;

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

                bool hasAnimProgress = packet.ReadBit("HasAnimProgress", index);

                if (packet.ReadBit("HasAreaTriggerSphere", index))
                    areaTriggerTemplate.Type = (byte)AreaTriggerType.Sphere;

                if (packet.ReadBit("HasAreaTriggerBox", index))
                    areaTriggerTemplate.Type = (byte)AreaTriggerType.Box;

                if (packet.ReadBit("HasAreaTriggerPolygon", index))
                    areaTriggerTemplate.Type = (byte)AreaTriggerType.Polygon;

                if (packet.ReadBit("HasAreaTriggerCylinder", index))
                    areaTriggerTemplate.Type = (byte)AreaTriggerType.Cylinder;

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
                packet.ReadInt32("WorldEffectID", index);

                var bit8 = packet.ReadBit("bit8", index);
                if (bit8)
                    packet.ReadInt32("Int1", index);
            }

            if (hasSmoothPhasing)
            {
                packet.ResetBitReader();
                packet.ReadBit("ReplaceActive", index);
                if (ClientVersion.AddedInVersion(ClientVersionBuild.V3_4_0_45166))
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
                var hasActionButtons = packet.ReadBit("Unk1132", index);

                if (hasSceneInstanceIDs)
                {
                    var sceneInstanceIDs = packet.ReadInt32("SceneInstanceIDsCount");
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

                if (hasActionButtons)
                {
                    var actionButtonCount = (ClientVersion.AddedInClassicVersion(1, 14, 4, 3, 4, 1) ? 180 : 132);
                    for (int i = 0; i < actionButtonCount; i++)
                        packet.ReadInt32("Action", index, i);
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

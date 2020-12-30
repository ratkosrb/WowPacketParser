using System.Collections.Generic;
using System.Linq;
using WowPacketParser.Enums;
using WowPacketParser.Misc;

namespace WowPacketParser.Store.Objects.UpdateFields.LegacyImplementation
{
    public class PlayerData : IPlayerData
    {
        private WoWObject Object { get; }
        private Dictionary<int, UpdateField> UpdateFields => Object.UpdateFields;

        public PlayerData(WoWObject obj)
        {
            Object = obj;
        }

        private WowGuid GetGuidValue(PlayerField field)
        {
            if (!ClientVersion.AddedInVersion(ClientType.WarlordsOfDraenor))
            {
                var parts = UpdateFields.GetArray<PlayerField, uint>(field, 2);
                return new WowGuid64(Utilities.MAKE_PAIR64(parts[0], parts[1]));
            }
            else
            {
                var parts = UpdateFields.GetArray<PlayerField, uint>(field, 4);
                return new WowGuid128(Utilities.MAKE_PAIR64(parts[0], parts[1]), Utilities.MAKE_PAIR64(parts[2], parts[3]));
            }
        }

        public WowGuid WowAccount => GetGuidValue(PlayerField.PLAYER_WOW_ACCOUNT);

        public uint GuildRank => UpdateFields.GetValue<PlayerField, uint>(PlayerField.PLAYER_GUILDRANK);

        public uint Experience => (ClientVersion.AddedInVersion(ClientVersionBuild.V8_0_1_27101) ?
            UpdateFields.GetValue<ActivePlayerField, uint>(ActivePlayerField.ACTIVE_PLAYER_FIELD_XP) :
            UpdateFields.GetValue<PlayerField, uint>(PlayerField.PLAYER_XP));

        public uint Money => (ClientVersion.AddedInVersion(ClientVersionBuild.V8_0_1_27101) ?
            UpdateFields.GetValue<ActivePlayerField, uint>(ActivePlayerField.ACTIVE_PLAYER_FIELD_COINAGE) :
            UpdateFields.GetValue<PlayerField, uint>(PlayerField.PLAYER_FIELD_COINAGE));

        public uint PlayerBytes1 => UpdateFields.GetValue<PlayerField, uint>(PlayerField.PLAYER_BYTES);
        public uint PlayerBytes2 => UpdateFields.GetValue<PlayerField, uint>(PlayerField.PLAYER_BYTES_2);
        public uint PlayerFlags => UpdateFields.GetValue<PlayerField, uint>(PlayerField.PLAYER_FLAGS);

        public class VisibleItem : IVisibleItem
        {
            public int ItemID { get; set; }
            public ushort ItemAppearanceModID { get; set; }
            public ushort ItemVisual { get; set; }
        }

        public IVisibleItem[] VisibleItems
        {
            get
            {
                if (ClientVersion.RemovedInVersion(ClientVersionBuild.V3_0_2_9056))
                {
                    var items = new VisibleItem[19];
                    int MAX_VISIBLE_ITEM_OFFSET = ClientVersion.AddedInVersion(ClientVersionBuild.V2_0_1_6180) ? 16 : 12;
                    for (var i = 0; i < 19; ++i)
                    {
                        int itemId = 0;
                        UpdateField value;
                        if (UpdateFields != null && UpdateFields.TryGetValue(WowPacketParser.Enums.Version.UpdateFields.GetUpdateField(PlayerField.PLAYER_VISIBLE_ITEM_1_0) + (i * MAX_VISIBLE_ITEM_OFFSET), out value))
                            itemId = value.Int32Value;

                        items[i] = new VisibleItem
                        {
                            ItemID = itemId
                        };
                    }
                    return items;
                }
                else
                {
                    PlayerField field;
                    if (ClientVersion.AddedInVersion(ClientVersionBuild.V5_4_2_17658))
                        field = PlayerField.PLAYER_VISIBLE_ITEM;
                    else
                        field = PlayerField.PLAYER_VISIBLE_ITEM_1_ENTRYID;

                    var raw = UpdateFields.GetArray<PlayerField, int>(field, 38);
                    var items = new VisibleItem[19];
                    for (var i = 0; i < 19; ++i)
                    {
                        items[i] = new VisibleItem
                        {
                            ItemID = System.Math.Abs(raw[i * 2]),
                            ItemVisual = (ushort)raw[i * 2 + 1]
                        };
                    }
                    return items;
                }
            }
        }
    }

    public class OriginalPlayerData : IPlayerData
    {
        private WoWObject Object { get; }
        private Dictionary<int, UpdateField> UpdateFields => Object.OriginalUpdateFields;

        public OriginalPlayerData(WoWObject obj)
        {
            Object = obj;
        }

        private WowGuid GetGuidValue(PlayerField field)
        {
            if (!ClientVersion.AddedInVersion(ClientType.WarlordsOfDraenor))
            {
                var parts = UpdateFields.GetArray<PlayerField, uint>(field, 2);
                return new WowGuid64(Utilities.MAKE_PAIR64(parts[0], parts[1]));
            }
            else
            {
                var parts = UpdateFields.GetArray<PlayerField, uint>(field, 4);
                return new WowGuid128(Utilities.MAKE_PAIR64(parts[0], parts[1]), Utilities.MAKE_PAIR64(parts[2], parts[3]));
            }
        }

        public WowGuid WowAccount => GetGuidValue(PlayerField.PLAYER_WOW_ACCOUNT);

        public uint GuildRank => UpdateFields.GetValue<PlayerField, uint>(PlayerField.PLAYER_GUILDRANK);

        public uint Experience => (ClientVersion.AddedInVersion(ClientVersionBuild.V8_0_1_27101) ?
            UpdateFields.GetValue<ActivePlayerField, uint>(ActivePlayerField.ACTIVE_PLAYER_FIELD_XP) :
            UpdateFields.GetValue<PlayerField, uint>(PlayerField.PLAYER_XP));

        public uint Money => (ClientVersion.AddedInVersion(ClientVersionBuild.V8_0_1_27101) ?
            UpdateFields.GetValue<ActivePlayerField, uint>(ActivePlayerField.ACTIVE_PLAYER_FIELD_COINAGE) :
            UpdateFields.GetValue<PlayerField, uint>(PlayerField.PLAYER_FIELD_COINAGE));

        public uint PlayerBytes1 => UpdateFields.GetValue<PlayerField, uint>(PlayerField.PLAYER_BYTES);
        public uint PlayerBytes2 => UpdateFields.GetValue<PlayerField, uint>(PlayerField.PLAYER_BYTES_2);
        public uint PlayerFlags => UpdateFields.GetValue<PlayerField, uint>(PlayerField.PLAYER_FLAGS);

        public class VisibleItem : IVisibleItem
        {
            public int ItemID { get; set; }
            public ushort ItemAppearanceModID { get; set; }
            public ushort ItemVisual { get; set; }
        }
        
        public IVisibleItem[] VisibleItems
        {
            get
            {
                if (ClientVersion.RemovedInVersion(ClientVersionBuild.V3_0_2_9056))
                {
                    var items = new VisibleItem[19];
                    int MAX_VISIBLE_ITEM_OFFSET = ClientVersion.AddedInVersion(ClientVersionBuild.V2_0_1_6180) ? 16 : 12;
                    for (var i = 0; i < 19; ++i)
                    {
                        int itemId = 0;
                        UpdateField value;
                        if (UpdateFields != null && UpdateFields.TryGetValue(WowPacketParser.Enums.Version.UpdateFields.GetUpdateField(PlayerField.PLAYER_VISIBLE_ITEM_1_0) + (i * MAX_VISIBLE_ITEM_OFFSET), out value))
                            itemId = value.Int32Value;

                        items[i] = new VisibleItem
                        {
                            ItemID = itemId
                        };
                    }
                    return items;
                }
                else
                {
                    PlayerField field;
                    if (ClientVersion.AddedInVersion(ClientVersionBuild.V5_4_2_17658))
                        field = PlayerField.PLAYER_VISIBLE_ITEM;
                    else
                        field = PlayerField.PLAYER_VISIBLE_ITEM_1_ENTRYID;

                    var raw = UpdateFields.GetArray<PlayerField, int>(field, 38);
                    var items = new VisibleItem[19];
                    for (var i = 0; i < 19; ++i)
                    {
                        items[i] = new VisibleItem
                        {
                            ItemID = System.Math.Abs(raw[i * 2]),
                            ItemVisual = (ushort)raw[i * 2 + 1]
                        };
                    }
                    return items;
                }
            }
        }
    }
}


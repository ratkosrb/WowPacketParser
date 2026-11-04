using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;

namespace WowPacketParserModule.V2_5_1_38707.Parsers
{
    public static class AuctionHouseHandler
    {
        public static void ReadActionBucketKey(Packet packet, params object[] idx)
        {
            packet.ResetBitReader();
            packet.ReadBits("ItemId", 20, idx);
            bool hasBattlePetSpeciesID = packet.ReadBit();
            packet.ReadBits("ItemLevel", 11, idx);
            bool hasSuffixItemNameDescriptionID = packet.ReadBit();

            if (hasBattlePetSpeciesID)
                packet.ReadUInt16("BattlePetSpeciesID", idx);

            if (hasSuffixItemNameDescriptionID)
                packet.ReadUInt16("SuffixItemNameDescriptionID", idx);
        }
        public static void ReadCliAuctionItem(Packet packet, params object[] idx)
        {
            packet.ResetBitReader();
            bool hasItemInstance = packet.ReadBit("HasItemInstance", idx);
            var enchantmentsCount = packet.ReadBits("EnchantmentsCount", 4, idx);
            var gemsCount = packet.ReadBits("GemsCount", 2, idx);
            bool hasMinBid = packet.ReadBit("HasMinBid", idx);

            bool hasMinIncrement = packet.ReadBit("HasMinIncrement", idx);
            bool hasBuyoutPrice = packet.ReadBit("HasBuyoutPrice", idx);
            bool hasUnitPrice = packet.ReadBit("HasUnitPrice", idx);
            bool censorServerSideInfo = packet.ReadBit("CensorServerSideInfo", idx);
            bool censorBidInfo = packet.ReadBit("CensorBidInfo", idx);
            bool hasAuctionBucketKey = packet.ReadBit("HasAuctionBucketKey", idx);
            bool hasCreator = packet.ReadBit("HasCreator", idx);
            bool hasBidder = false;
            bool hasBidAmount = false;
            if (ClientVersion.RemovedInVersion(ClientVersionBuild.V2_5_2_39570))
            {
                // 2.5.1: no bidder guid, the bit after HasCreator gates the 8 byte bid amount, last byte of bits is unknown
                if (!censorBidInfo)
                    hasBidAmount = packet.ReadBit("HasBidAmount", idx);
                packet.ReadBits("UnkBits", censorBidInfo ? 9 : 8, idx);
            }
            else
            {
                if (!censorBidInfo)
                {
                    hasBidder = packet.ReadBit("HasBidder", idx);
                    hasBidAmount = packet.ReadBit("HasBidAmount", idx);
                }
                packet.ReadBits("UnkBits2", 7, idx);
            }

            if (hasItemInstance)
            {
                if (ClientVersion.RemovedInVersion(ClientVersionBuild.V2_5_2_39570))
                {
                    // 2.5.1 (TBC Classic): the item instance has a single bits byte (HasItemBonus), no modifiers byte
                    packet.ReadInt32<ItemId>("ItemID", idx);
                    packet.ReadUInt32("RandomPropertiesSeed", idx);
                    packet.ReadUInt32("RandomPropertiesID", idx);
                    packet.ResetBitReader();
                    var hasBonuses = packet.ReadBit("HasItemBonus", idx);
                    packet.ResetBitReader();
                    if (hasBonuses)
                    {
                        packet.ReadByte("Context", idx);
                        var bonusCount = packet.ReadUInt32();
                        for (var j = 0; j < bonusCount; ++j)
                            packet.ReadUInt32("BonusListID", idx, j);
                    }
                }
                else
                    Substructures.ItemHandler.ReadItemInstance(packet, idx);
            }

            packet.ReadInt32("Count", idx);
            packet.ReadInt32("Charges", idx);
            packet.ReadInt32("Flags", idx);
            packet.ReadInt32("AuctionItemID", idx);

            packet.ReadPackedGuid128("Owner", idx);
            packet.ReadInt32("DurationLeft", idx);
            packet.ReadByte("DeleteReason", idx);

            for (int i = 0; i < enchantmentsCount; i++)
            {
                packet.ReadInt32("ID", idx, i);
                packet.ReadUInt32("Expiration", idx, i);
                packet.ReadInt32("Charges", idx, i);
                packet.ReadByte("Slot", idx, i);
            }

            if (hasMinBid)
                packet.ReadUInt64("MinBid", idx);

            if (hasMinIncrement)
                packet.ReadUInt64("MinIncrement", idx);

            if (hasBuyoutPrice)
                packet.ReadUInt64("BuyoutPrice", idx);

            if (hasUnitPrice)
                packet.ReadUInt64("UnitPrice", idx);

            if (!censorServerSideInfo)
            {
                packet.ReadPackedGuid128("ItemGUID", idx);
                packet.ReadPackedGuid128("OwnerAccountID", idx);
                packet.ReadInt32("EndTime", idx);
            }

            if (hasCreator)
                packet.ReadPackedGuid128("Creator", idx);

            if (hasBidder)
                packet.ReadPackedGuid128("Bidder", idx);

            if (hasBidAmount)
                packet.ReadInt64("BidAmount", idx);

            for (int i = 0; i < gemsCount; i++)
                Substructures.ItemHandler.ReadItemGemData(packet, idx, "Gems", i);

            if (hasAuctionBucketKey)
                ReadActionBucketKey(packet, idx);
        }
        [Parser(Opcode.SMSG_AUCTION_LIST_ITEMS_RESULT)]
        public static void HandleListItemsResult(Packet packet)
        {
            var itemsCount = packet.ReadInt32("ItemsCount");

            packet.ReadInt32("TotalCount");
            packet.ReadInt32("DesiredDelay");
            packet.ReadBool("OnlyUsable");

            for (var i = 0; i < itemsCount; i++)
                ReadCliAuctionItem(packet, i);
        }

        [Parser(Opcode.SMSG_AUCTION_REPLICATE_RESPONSE)]
        public static void HandleAuctionReplicateResponseList(Packet packet)
        {
            packet.ReadUInt32("Result");
            packet.ReadUInt32("DesiredDelay");
            packet.ReadUInt32("ChangeNumberGlobal");
            packet.ReadUInt32("ChangeNumberCursor");
            packet.ReadUInt32("ChangeNumberTombstone");

            var itemsCount = packet.ReadInt32("ItemsCount");
            for (var i = 0; i < itemsCount; ++i)
                ReadCliAuctionItem(packet, "Items", i);
        }

        [Parser(Opcode.CMSG_AUCTION_REPLICATE_ITEMS)]
        public static void HandleAuctionReplicateItems(Packet packet)
        {
            packet.ReadPackedGuid128("Auctioneer");
            packet.ReadInt32("ChangeNumberGlobal");
            packet.ReadInt32("ChangeNumberCursor");
            packet.ReadInt32("ChangeNumberTombstone");
            packet.ReadInt32("Count");
            bool hasAddonInfo = packet.ReadBit("HasAddonInfo");

            if (hasAddonInfo)
                V8_0_1_27101.Parsers.AddonHandler.ReadAddOnInfo(packet);
        }

        [Parser(Opcode.CMSG_AUCTION_SELL_ITEM)]
        public static void HandleAuctionSellItem(Packet packet)
        {
            packet.ReadPackedGuid128("Auctioneer");
            packet.ReadInt64("BidPrice");
            packet.ReadInt64("BuyoutPrice");
            packet.ReadInt32("RunTime");
            bool hasAddonInfo = packet.ReadBit("HasAddonInfo");

            int itemCountBits = ClientVersion.AddedInClassicVersion(1, 14, 3, 2, 5, 4) ? 6 : 5;
            var count = packet.ReadBits("ItemsCount", itemCountBits);
            packet.ResetBitReader();

            if (hasAddonInfo)
                V8_0_1_27101.Parsers.AddonHandler.ReadAddOnInfo(packet);

            for (var i = 0; i < count; ++i)
            {
                packet.ReadPackedGuid128("Guid", i);
                packet.ReadInt32("UseCount");
            }
        }

        [Parser(Opcode.CMSG_AUCTION_REMOVE_ITEM)]
        public static void HandleAuctionRemoveItem(Packet packet)
        {
            packet.ReadPackedGuid128("Auctioneer");
            packet.ReadInt32("AuctionItemID");
            bool hasAddonInfo = packet.ReadBit("HasAddonInfo");

            if (hasAddonInfo)
                V8_0_1_27101.Parsers.AddonHandler.ReadAddOnInfo(packet);
        }

        [Parser(Opcode.CMSG_AUCTION_PLACE_BID)]
        public static void HandleAuctionPlaceBid(Packet packet)
        {
            packet.ReadPackedGuid128("Auctioneer");
            packet.ReadInt32("AuctionItemID");
            packet.ReadInt64("BidAmount");
            bool hasAddonInfo = packet.ReadBit("HasAddonInfo");

            if (hasAddonInfo)
                V8_0_1_27101.Parsers.AddonHandler.ReadAddOnInfo(packet);
        }

        [Parser(Opcode.SMSG_AUCTION_COMMAND_RESULT)]
        public static void HandleAuctionCommandResult(Packet packet)
        {
            packet.ReadUInt32("AuctionItemID");
            packet.ReadUInt32E<AuctionHouseAction>("Command");
            packet.ReadUInt32E<AuctionHouseError>("ErrorCode");
            packet.ReadUInt32("BagResult");
            packet.ReadPackedGuid128("Guid");

            // One of the following is MinIncrement and the other is Money, order still unknown
            packet.ReadUInt64("MinIncrement");
            packet.ReadUInt64("Money");
            packet.ReadUInt32("DesiredDelay");
        }

        [Parser(Opcode.SMSG_AUCTION_LIST_BIDDED_ITEMS_RESULT)]
        [Parser(Opcode.SMSG_AUCTION_LIST_OWNED_ITEMS_RESULT)]
        public static void HandleAuctionListOwnedItemsResult(Packet packet)
        {
            int itemsCount = packet.ReadInt32("ItemsCount");
            int soldItemsCount = packet.ReadInt32("SoldItemsCount");
            packet.ReadUInt32("DesiredDelay");

            for (int i = 0; i < itemsCount; i++)
                ReadCliAuctionItem(packet, "Items", i);
        }

        [Parser(Opcode.SMSG_AUCTION_OUTBID_NOTIFICATION)]
        public static void HandleAuctionOutbitNotification(Packet packet)
        {
            packet.ReadUInt32("Command");
            V6_0_2_19033.Parsers.AuctionHandler.ReadClientAuctionBidderNotification(packet, "Info");
            packet.ReadUInt64("BidAmount");
            packet.ReadUInt64("BidIncrement");
        }

        [Parser(Opcode.SMSG_AUCTION_WON_NOTIFICATION)]
        public static void HandleAuctionReplicateResponse(Packet packet)
        {
            packet.ReadUInt32("Command");
            V6_0_2_19033.Parsers.AuctionHandler.ReadClientAuctionBidderNotification(packet, "Info");
        }
    }
}

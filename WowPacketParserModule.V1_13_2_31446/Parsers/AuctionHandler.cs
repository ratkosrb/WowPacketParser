using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;
using CoreParsers = WowPacketParser.Parsing.Parsers;

namespace WowPacketParserModule.V1_13_2_31446.Parsers
{
    public static class AuctionHandler
    {
        [Parser(Opcode.SMSG_AUCTION_HELLO_RESPONSE)]
        public static void HandleServerAuctionHello(Packet packet)
        {
            packet.ReadPackedGuid128("Guid");
            packet.ReadUInt32("AuctionHouseID"); // Unsure
            packet.ReadBit("OpenForBusiness");

            CoreParsers.NpcHandler.LastGossipOption.Reset();
            CoreParsers.NpcHandler.TempGossipOptionPOI.Reset();
        }

        [Parser(Opcode.SMSG_AUCTION_OUTBID_NOTIFICATION)]
        public static void HandleAuctionOutbitNotification(Packet packet)
        {
            packet.ReadUInt32("AuctionHouseID");
            packet.ReadUInt32("AuctionItemID");
            packet.ReadPackedGuid128("Bidder");
            packet.ReadInt32<ItemId>("ItemID");
            packet.ReadUInt32("UnkInt2");
            packet.ReadUInt32("UnkInt3");
            packet.ReadByte("UnkByte");
            packet.ReadUInt64("BidAmount");
            packet.ReadUInt64("MinIncrement");
        }

        public static void ReadCliAuctionItem(Packet packet, params object[] idx)
        {
            Substructures.ItemHandler.ReadItemInstance(packet, idx);
            packet.ResetBitReader();

            packet.ReadInt32("Count", idx);
            packet.ReadInt32("Charges", idx);
            packet.ReadInt32("Flags", idx);
            packet.ReadInt32("AuctionItemID", idx);

            packet.ReadPackedGuid128("Owner", idx);

            packet.ReadUInt64("MinBid", idx);
            packet.ReadUInt64("MinIncrement", idx);
            packet.ReadUInt64("BuyoutPrice", idx);

            packet.ReadInt32("DurationLeft", idx);
            packet.ReadByte("DeleteReason", idx);

            var enchantmentsCount = packet.ReadBits("EnchantmentsCount", 4, idx);
            var gemsCount = packet.ReadBits("GemsCount", 2, idx);
            var censorServerSideInfo = packet.ReadBit("CensorServerSideInfo", idx);
            var censorBidInfo = packet.ReadBit("CensorBidInfo", idx);
            packet.ResetBitReader();

            for (int i = 0; i < gemsCount; i++)
                Substructures.ItemHandler.ReadItemGemData(packet, idx, "Gems", i);

            for (int i = 0; i < enchantmentsCount; i++)
                Substructures.ItemHandler.ReadItemEnchantData(packet, idx, "Enchantments", i);

            if (!censorServerSideInfo)
            {
                packet.ReadPackedGuid128("ItemGUID", idx);
                packet.ReadPackedGuid128("OwnerAccountID", idx);
                packet.ReadInt32("EndTime", idx);
            }

            if (!censorBidInfo)
            {
                packet.ReadPackedGuid128("Bidder", idx);
                packet.ReadInt64("BidAmount", idx);
            }
        }

        [Parser(Opcode.SMSG_AUCTION_LIST_ITEMS_RESULT)]
        public static void HandleAuctionListItemsResult(Packet packet)
        {
            var itemsCount = packet.ReadInt32("ItemsCount");
            packet.ReadInt32("TotalCount");
            packet.ReadInt32("DesiredDelay");
            packet.ReadBit("OnlyUsable");
            packet.ResetBitReader();

            for (var i = 0; i < itemsCount; i++)
                ReadCliAuctionItem(packet, i);
        }

        [Parser(Opcode.SMSG_AUCTION_WON_NOTIFICATION)]
        public static void HandleAuctionWonNotification(Packet packet)
        {
            packet.ReadUInt32("AuctionHouseID");
            packet.ReadUInt32("AuctionItemID");
            packet.ReadPackedGuid128("Bidder");
            Substructures.ItemHandler.ReadItemInstance(packet, "Item");
        }
    }
}

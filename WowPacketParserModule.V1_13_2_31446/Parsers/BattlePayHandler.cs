using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;

namespace WowPacketParserModule.V1_13_2_31446.Parsers
{
    public static class BattlePayHandler
    {
        private static void ReadBattlepayDisplayInfo(Packet packet, params object[] idx)
        {
            var hasCreatureDisplayInfoId = packet.ReadBit("HasCreatureDisplayInfoID", idx);
            var hasFileDataId = packet.ReadBit("HasFileDataID", idx);
            var len1 = packet.ReadBits(10);
            var len2 = packet.ReadBits(10);
            var len3 = packet.ReadBits(13);
            packet.ReadBits("UnkBits", 13, idx);
            var hasFlags = packet.ReadBit("HasFlags", idx);

            if (hasCreatureDisplayInfoId)
                packet.ReadInt32("CreatureDisplayInfoID", idx);

            if (hasFileDataId)
                packet.ReadInt32("FileDataID", idx);

            packet.ReadInt32("Unk", idx);

            packet.ReadWoWString("Name1", len1, idx);
            packet.ReadWoWString("Name2", len2, idx);
            packet.ReadWoWString("Name3", len3, idx);

            if (hasFlags)
                packet.ReadInt32("Flags", idx);
        }

        private static void ReadBattlePayProductInfo(Packet packet, params object[] idx)
        {
            packet.ReadInt32("ProductID", idx);
            packet.ReadInt64("NormalPriceFixedPoint", idx);
            packet.ReadInt64("CurrentPriceFixedPoint", idx);

            var count = packet.ReadInt32("ProductIDsCount", idx);
            packet.ReadInt32("Unk1", idx);
            packet.ReadInt32("Unk2", idx);
            for (var i = 0; i < count; ++i)
                packet.ReadInt32("ProductIDs", idx, i);

            packet.ResetBitReader();
            packet.ReadBits("UnkBits", 7, idx);
            var hasDisplayInfo = packet.ReadBit("HasBattlepayDisplayInfo", idx);
            packet.ResetBitReader();
            if (hasDisplayInfo)
                ReadBattlepayDisplayInfo(packet, idx);
        }

        private static void ReadBattlePayProduct(Packet packet, params object[] idx)
        {
            packet.ReadInt32("ProductID", idx);
            packet.ReadInt32("Unk1", idx);
            for (var i = 0; i < 6; ++i)
                packet.ReadInt32("Unk", idx, i);
        }

        [Parser(Opcode.SMSG_BATTLE_PAY_GET_PRODUCT_LIST_RESPONSE)]
        public static void HandleBattlePayGetProductListResponse(Packet packet)
        {
            packet.ReadUInt32("Result");
            packet.ReadUInt32("CurrencyID");

            var productInfoCount = packet.ReadUInt32("BattlePayProductInfoCount");
            var productCount = packet.ReadUInt32("BattlePayProductCount");
            var groupCount = packet.ReadUInt32("BattlePayProductGroupCount");
            var shopEntryCount = packet.ReadUInt32("BattlePayShopEntryCount");

            for (var i = 0; i < productInfoCount; ++i)
                ReadBattlePayProductInfo(packet, "ProductInfo", i);

            for (var i = 0; i < productCount; ++i)
                ReadBattlePayProduct(packet, "Product", i);

            for (var i = 0; i < groupCount; ++i)
            {
                packet.ReadInt32("GroupID", "Group", i);
                packet.ReadInt32("IconFileDataID", "Group", i);
                packet.ReadByte("DisplayType", "Group", i);
                packet.ReadInt32("Ordering", "Group", i);
                packet.ReadInt32("Unk", "Group", i);

                packet.ResetBitReader();
                var nameLen = packet.ReadBits(8);
                packet.ReadBits("UnkBits", 23, "Group", i);
                packet.ReadBit("UnkBit", "Group", i);
                packet.ReadWoWString("Name", nameLen, "Group", i);
            }

            for (var i = 0; i < shopEntryCount; ++i)
            {
                packet.ReadUInt32("EntryID", "ShopEntry", i);
                packet.ReadUInt32("GroupID", "ShopEntry", i);
                packet.ReadUInt32("ProductID", "ShopEntry", i);
                packet.ReadInt32("Ordering", "ShopEntry", i);
                packet.ReadInt32("Flags", "ShopEntry", i);
                packet.ReadByte("BannerType", "ShopEntry", i);

                packet.ResetBitReader();
                var hasDisplayInfo = packet.ReadBit("HasBattlepayDisplayInfo", "ShopEntry", i);
                packet.ResetBitReader();
                if (hasDisplayInfo)
                    ReadBattlepayDisplayInfo(packet, "ShopEntry", i);
            }
        }

        [Parser(Opcode.SMSG_BATTLE_PAY_GET_PURCHASE_LIST_RESPONSE)]
        public static void HandleBattlePayGetPurchaseListResponse(Packet packet)
        {
            packet.ReadUInt32("Result");
            var count = packet.ReadUInt32("BattlePayPurchaseCount");
            for (var i = 0; i < count; ++i)
            {
                packet.ReadInt64("PurchaseID", i);
                packet.ReadInt32("Status", i);
                packet.ReadInt32("ResultCode", i);
                packet.ReadInt32("ProductID", i);
                packet.ResetBitReader();
                var len = packet.ReadBits(8);
                packet.ReadWoWString("WalletName", len, i);
            }
        }

        [Parser(Opcode.SMSG_BATTLE_PAY_GET_DISTRIBUTION_LIST_RESPONSE)]
        public static void HandleBattlePayGetDistributionListResponse(Packet packet)
        {
            packet.ReadUInt32("Result");
            var count = packet.ReadBits("BattlePayDistributionObjectCount", 11);
            if (count != 0)
                packet.ReadToEnd();
        }
    }
}

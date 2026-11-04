using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;

namespace WowPacketParserModule.V2_5_1_38707.Parsers
{
    public static class MiscellaneousHandler
    {
        [Parser(Opcode.SMSG_BREAK_CROWD_CONTROL_ROLL_INFO)]
        public static void HandleUnkDebug1(Packet packet)
        {
            packet.ReadPackedGuid128("Guid1");
            packet.ReadPackedGuid128("Guid2");
            packet.ReadInt32("UnkInt3");
            packet.ReadSingle("UnkFloat4");
            packet.ReadSingle("UnkFloat5");
            packet.ReadInt32("UnkInt6");
            packet.ReadByte("UnkByte7");
        }

        [Parser(Opcode.CMSG_LATENCY_REPORT)]
        [Parser(Opcode.SMSG_LATENCY_REPORT_PING)]
        public static void HandleLatencyReportPing(Packet packet)
        {
            packet.ReadInt32("Unk1");
            uint count = packet.ReadUInt32("Count");
            for (uint i = 0; i < count; ++i)
            {
                packet.ReadUInt32("Unk2", i);
                packet.ReadUInt16("Unk3", i);
                packet.ReadByte("Unk4", i);
                packet.ReadUInt64("UnixTime", i);
                packet.ReadUInt32("Unk6", i);
            }
        }

        [Parser(Opcode.CMSG_MAIL_MARK_AS_READ)]
        public static void HandleMailMarkAsRead(Packet packet)
        {
            packet.ReadPackedGuid128("Mailbox");
            packet.ReadInt32("MailID");
        }

        [Parser(Opcode.SMSG_ACCOUNT_TOY_UPDATE)]
        public static void HandleAccountToyUpdate(Packet packet)
        {
            packet.ReadBit("IsFullUpdate");

            var itemIdCount = packet.ReadUInt32("ToyItemIDsCount");
            var isFavoriteCount = packet.ReadUInt32("ToyIsFavoriteCount");
            var fanfareCount = packet.ReadUInt32("FanfareCount");

            for (var i = 0; i < itemIdCount; i++)
                packet.ReadInt32("ToyItemID", i);

            packet.ResetBitReader();

            for (var i = 0; i < isFavoriteCount; i++)
                packet.ReadBit("ToyIsFavorite", i);

            for (var i = 0; i < fanfareCount; i++)
                packet.ReadBit("HasFanfare", i);
        }
    }
}

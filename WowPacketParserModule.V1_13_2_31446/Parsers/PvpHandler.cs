using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;

namespace WowPacketParserModule.V1_13_2_31446.Parsers
{
    public static class PvpHandler
    {
        [Parser(Opcode.SMSG_PVP_SEASON)]
        public static void HandlePvPSeason(Packet packet)
        {
            packet.ReadUInt32("Last Season");
            packet.ReadUInt32("Active Season");
        }

        [Parser(Opcode.CMSG_REPORT_PVP_PLAYER_AFK)]
        public static void HandleReportPvPPlayerAfk(Packet packet)
        {
            packet.ReadPackedGuid128("Offender");
        }
    }
}
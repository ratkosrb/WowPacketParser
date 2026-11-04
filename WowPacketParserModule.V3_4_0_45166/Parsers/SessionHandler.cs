using System;
using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;
using WowPacketParser.Store;
using CoreParsers = WowPacketParser.Parsing.Parsers;

namespace WowPacketParserModule.V3_4_0_45166.Parsers
{
    public static class SessionHandler
    {
        // 3.4.0.44930 / 3.4.0.45043 use a 64 byte RSA encrypted key
        [Parser(Opcode.SMSG_ENTER_ENCRYPTED_MODE, ClientVersionBuild.V3_4_0_44463)]
        public static void HandleEnterEncryptedMode(Packet packet)
        {
            packet.ReadBytes("EncryptionKey (RSA encrypted)", ClientVersion.AddedInVersion(ClientVersionBuild.V3_4_0_45166) ? 256 : 64);
            packet.ResetBitReader();
            packet.ReadBit("Enabled");
        }

        [Parser(Opcode.SMSG_SET_TIME_ZONE_INFORMATION)]
        public static void HandleSetTimeZoneInformation(Packet packet)
        {
            var len1 = packet.ReadBits(7);
            var len2 = packet.ReadBits(7);
            var len3 = packet.ReadBits(7);

            packet.ReadWoWString("ServerTimeTZ", len1);
            packet.ReadWoWString("GameTimeTZ", len2);
            packet.ReadWoWString("ServerRegionalTZ", len3);
        }

        [Parser(Opcode.CMSG_PLAYER_LOGIN, ClientVersionBuild.V3_4_3_51505)]
        public static void HandlePlayerLogin(Packet packet)
        {
            Storage.CurrentActivePlayer = packet.ReadPackedGuid128("Guid");
            packet.ReadSingle("FarClip");
        }
    }
}

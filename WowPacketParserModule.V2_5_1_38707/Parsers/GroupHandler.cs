using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;

namespace WowPacketParserModule.V2_5_1_38835.Parsers
{
    public static class GroupHandler
    {
        [Parser(Opcode.CMSG_PARTY_INVITE)]
        public static void HandleClientPartyInvite(Packet packet)
        {
            packet.ReadByte("PartyIndex");
            if (ClientVersion.IsBurningCrusadeClassicPhase1ClientVersionBuild(ClientVersion.Build))
            {
                packet.ReadInt32("VirtualRealmAddress");
                packet.ReadPackedGuid128("TargetGuid");
            }
            packet.ResetBitReader();

            var lenTargetName = packet.ReadBits(9);
            var lenTargetRealm = packet.ReadBits(9);

            if (!ClientVersion.IsBurningCrusadeClassicPhase1ClientVersionBuild(ClientVersion.Build))
            {
                packet.ReadInt32("VirtualRealmAddress");
                packet.ReadPackedGuid128("TargetGuid");
            }

            packet.ReadWoWString("TargetName", lenTargetName);
            packet.ReadWoWString("TargetRealm", lenTargetRealm);
        }

        [Parser(Opcode.SMSG_READY_CHECK_STARTED)]
        public static void HandleReadyCheckStarted(Packet packet)
        {
            packet.ReadByte("PartyIndex");
            packet.ReadPackedGuid128("PartyGUID");
            packet.ReadPackedGuid128("InitiatorGUID");
            packet.ReadInt64("Duration");
        }
    }
}

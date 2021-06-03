﻿using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;
using WowPacketParser.Store;
using WowPacketParser.Store.Objects;

namespace WowPacketParserModule.V9_0_1_36216.Parsers
{
    public static class ChatHandler
    {
        [Parser(Opcode.SMSG_CHAT)]
        public static void HandleServerChatMessage(Packet packet)
        {
            var text = new ChatPacketData
            {
                Type = (ChatMessageType)packet.ReadByteE<ChatMessageTypeNew>("SlashCmd"),
                Language = packet.ReadUInt32E<Language>("Language"),
                SenderGUID = packet.ReadPackedGuid128("SenderGUID")
            };

            packet.ReadPackedGuid128("SenderGuildGUID");
            packet.ReadPackedGuid128("WowAccountGUID");
            text.ReceiverGUID = packet.ReadPackedGuid128("TargetGUID");
            packet.ReadUInt32("TargetVirtualAddress");
            packet.ReadUInt32("SenderVirtualAddress");
            packet.ReadPackedGuid128("PartyGUID");
            packet.ReadInt32("AchievementID");
            packet.ReadSingle("DisplayTime");

            var senderNameLen = packet.ReadBits(11);
            var receiverNameLen = packet.ReadBits(11);
            var prefixLen = packet.ReadBits(5);
            var channelLen = packet.ReadBits(7);
            var textLen = packet.ReadBits(12);
            packet.ReadBits("ChatFlags", 14);

            packet.ReadBit("HideChatLog");
            packet.ReadBit("FakeSenderName");
            bool unk801bit = packet.ReadBit("Unk801_Bit");

            text.SenderName = packet.ReadWoWString("Sender Name", senderNameLen);
            text.ReceiverName = packet.ReadWoWString("Receiver Name", receiverNameLen);
            packet.ReadWoWString("Addon Message Prefix", prefixLen);
            text.ChannelName = packet.ReadWoWString("Channel Name", channelLen);

            text.Text = packet.ReadWoWString("Text", textLen);
            if (unk801bit)
                packet.ReadUInt32("Unk801");

            Storage.StoreText(text, packet);
        }

        [Parser(Opcode.SMSG_EMOTE)]
        public static void HandleEmote(Packet packet)
        {
            var guid = packet.ReadPackedGuid128("GUID");
            var emote = packet.ReadInt32E<EmoteType>("Emote ID");

            if (ClientVersion.AddedInVersion(ClientVersionBuild.V9_0_5_37503))
            {
                var count = packet.ReadUInt32("SpellVisualKitCount");
                for (var i = 0; i < count; ++i)
                    packet.ReadUInt32("SpellVisualKitID", i);
            }

            if (guid.GetObjectType() == ObjectType.Unit)
                Storage.Emotes.Add(guid, emote, packet.TimeSpan);
        }
    }
}

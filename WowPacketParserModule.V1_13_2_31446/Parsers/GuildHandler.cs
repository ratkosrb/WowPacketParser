using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Store;
using WowPacketParser.Store.Objects;
using WowPacketParser.Parsing;

namespace WowPacketParserModule.V1_13_2_31446.Parsers
{
    public static class GuildHandler
    {
        [Parser(Opcode.SMSG_QUERY_GUILD_INFO_RESPONSE)]
        public static void HandleGuildQueryResponse(Packet packet)
        {
            packet.ReadPackedGuid128("Guild Guid");

            var hasData = packet.ReadBit();
            if (hasData)
            {
                WowGuid guildGuid = packet.ReadPackedGuid128("GuildGUID");
                packet.ReadInt32("VirtualRealmAddress");
                var rankCount = packet.ReadUInt32("RankCount");
                var emblemColor = packet.ReadInt32("EmblemColor");
                var emblemStyle = packet.ReadInt32("EmblemStyle");
                var borderColor = packet.ReadInt32("BorderColor");
                var borderStyle = packet.ReadInt32("BorderStyle");
                var backgroundColor = packet.ReadInt32("BackgroundColor");

                packet.ResetBitReader();
                var nameLen = packet.ReadBits(7);

                for (var i = 0; i < rankCount; i++)
                {
                    int rankID = packet.ReadInt32("RankID", i);
                    packet.ReadInt32("RankOrder", i);

                    packet.ResetBitReader();
                    var rankNameLen = packet.ReadBits(7);
                    string rankName = packet.ReadWoWString("RankName", rankNameLen, i);

                    GuildRankTemplate guildRank = new GuildRankTemplate { GuildGUID = guildGuid.Low.ToString(), RankID = rankID, RankName = rankName };
                    Storage.GuildRank.Add(guildRank);
                }

                var guildName = packet.ReadWoWString("GuildName", nameLen);

                GuildTemplate guild = new GuildTemplate { GuildGUID = guildGuid.Low.ToString(), GuildName = guildName, LeaderGUID = 1, EmblemStyle = emblemStyle, EmblemColor = emblemColor, BorderStyle = borderStyle, BorderColor = borderColor, BackgroundColor = backgroundColor, info = "", motd = "No message set." };
                Storage.Guild.Add(guild);
            }
        }
    }
}
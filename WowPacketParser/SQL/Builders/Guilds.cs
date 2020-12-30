using System.Text;
using WowPacketParser.Misc;
using WowPacketParser.Store;
using WowPacketParser.Store.Objects;

namespace WowPacketParser.SQL.Builders
{
    [BuilderClass]
    class Guilds
    {
        [BuilderMethod(true)]
        public static string GuildBuilder()
        {
            if (!Settings.SqlTables.guild && !Settings.SqlTables.guild_rank)
                return string.Empty;

            if (Storage.Guild.IsEmpty())
                return string.Empty;

            if (!Settings.SqlTables.guild)
                return string.Empty;

            if (Storage.GuildRank.IsEmpty())
                return string.Empty;

            if (!Settings.SqlTables.guild_rank)
                return string.Empty;

            StringBuilder result = new StringBuilder();

            var guildRows = new RowList<GuildTemplate>();
            var guildRankRows = new RowList<GuildRankTemplate>();

            foreach (var guild in Storage.Guild)
            {
                Row<GuildTemplate> guildRow = new Row<GuildTemplate>();
                guildRow.Data.GuildGUID = guild.Item1.GuildGUID;
                guildRow.Data.GuildName = guild.Item1.GuildName;
                guildRow.Data.LeaderGUID = guild.Item1.LeaderGUID;
                guildRow.Data.EmblemStyle = guild.Item1.EmblemStyle;
                guildRow.Data.EmblemColor = guild.Item1.EmblemColor;
                guildRow.Data.BorderStyle = guild.Item1.BorderStyle;
                guildRow.Data.BorderColor = guild.Item1.BorderColor;
                guildRow.Data.BackgroundColor = guild.Item1.BackgroundColor;
                guildRow.Data.info = guild.Item1.info;
                guildRow.Data.motd = guild.Item1.motd;
                guildRows.Add(guildRow);
            }

            foreach (var guild in Storage.GuildRank)
            {
                Row<GuildRankTemplate> guildRankRow = new Row<GuildRankTemplate>();
                guildRankRow.Data.GuildGUID = guild.Item1.GuildGUID;
                guildRankRow.Data.RankID = guild.Item1.RankID;
                guildRankRow.Data.RankName = guild.Item1.RankName;

                // Check if it is guild master rank
                if (guildRankRow.Data.RankID == 0)
                    guildRankRow.Data.rights = 1044991;
                else
                    guildRankRow.Data.rights = 67;

                guildRankRows.Add(guildRankRow);
            }

            if (Settings.SqlTables.guild)
            {
                var guildSql = new SQLInsert<GuildTemplate>(guildRows, false);
                result.Append(guildSql.Build());
                result.AppendLine();
            }

            if (Settings.SqlTables.guild_rank)
            {
                var guildRankSql = new SQLInsert<GuildRankTemplate>(guildRankRows, false);
                result.Append(guildRankSql.Build());
                result.AppendLine();
            }

            return result.ToString();
        }
    }
}

using WowPacketParser.SQL;

namespace WowPacketParser.Store.Objects
{
    [DBTableName("guild")]
    public sealed class GuildTemplate : IDataModel
    {
        [DBFieldName("guildid", true, true)]
        public string GuildGUID;

        [DBFieldName("name")]
        public string GuildName;

        [DBFieldName("leaderguid")]
        public uint LeaderGUID;

        [DBFieldName("EmblemStyle")]
        public int EmblemStyle;

        [DBFieldName("EmblemColor")]
        public int EmblemColor;

        [DBFieldName("BorderStyle")]
        public int BorderStyle = 0;

        [DBFieldName("BorderColor")]
        public int BorderColor = 0;

        [DBFieldName("BackgroundColor")]
        public int BackgroundColor;

        [DBFieldName("info")]
        public string info;

        [DBFieldName("motd")]
        public string motd;

    }

    [DBTableName("guild_rank")]
    public sealed class GuildRankTemplate : IDataModel
    {
        [DBFieldName("guildid", true, true)]
        public string GuildGUID;

        [DBFieldName("rid", true)]
        public int RankID;

        [DBFieldName("rname")]
        public string RankName;

        [DBFieldName("rights")]
        public int rights;
    }
}

using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.SQL;

namespace WowPacketParser.Store.Objects
{
    [DBTableName("gossip_menu")]
    public class GossipMenu : IDataModel
    {
        [DBFieldName("entry", true)]
        public uint? Entry;

        [DBFieldName("text_id", true)]
        public uint? TextID;

        [DBFieldName("sniff_build")]
        public int? SniffBuild = ClientVersion.BuildInt;

        public ObjectType ObjectType;

        public uint ObjectEntry;

        //public ICollection<GossipMenuOption> GossipOptions;
    }
    [DBTableName("creature_gossip")]
    public class CreatureGossip : IDataModel
    {
        [DBFieldName("entry", true)]
        public uint? CreatureId;

        [DBFieldName("gossip_menu_id", true)]
        public uint? GossipMenuId;

        [DBFieldName("sniff_build")]
        public int? SniffBuild = ClientVersion.BuildInt;
    }
}

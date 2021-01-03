using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.SQL;

namespace WowPacketParser.Store.Objects
{
    [DBTableName("gossip_menu_option")]
    public class GossipMenuOption : IDataModel
    {
        [DBFieldName("menu_id", true)]
        public uint? MenuId;

        [DBFieldName("id", true)]
        public uint? OptionIndex;

        [DBFieldName("option_icon")]
        public GossipOptionIcon? OptionIcon;

        [DBFieldName("option_text")]
        public string OptionText;

        [DBFieldName("option_broadcast_text")]
        public int? OptionBroadcastTextId = 0;

        [DBFieldName("option_id")]
        public uint OptionId = 0;

        [DBFieldName("npc_option_npcflag")]
        public uint NpcOptionNpcFlag = 0;

        [DBFieldName("sniff_build")]
        public int? SniffBuild = ClientVersion.BuildInt;

        public string BroadcastTextIDHelper;
    }
}

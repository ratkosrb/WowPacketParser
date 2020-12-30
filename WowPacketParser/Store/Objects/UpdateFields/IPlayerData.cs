using WowPacketParser.Misc;

namespace WowPacketParser.Store.Objects.UpdateFields
{
    public class VisiblePlayerItem
    {
        public uint ItemID;
        public uint EnchantID;
    }

    public interface IPlayerData
    {
        WowGuid WowAccount { get; }
        uint Experience { get; }
        uint Money { get; }
        uint PlayerBytes1 { get; }
        uint PlayerBytes2 { get; }
        uint PlayerFlags { get; }
        IVisibleItem[] VisibleItems { get; }
        uint GuildRank { get; }
    }
}

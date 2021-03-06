using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V9_0_1_36216.Hotfix
{
    [HotfixStructure(DB2Hash.Difficulty, HasIndexInData = false)]
    public class DifficultyEntry
    {
        public string Name { get; set; }
        public byte InstanceType { get; set; }
        public byte OrderIndex { get; set; }
        public sbyte OldEnumValue { get; set; }
        public byte FallbackDifficultyID { get; set; }
        public byte MinPlayers { get; set; }
        public byte MaxPlayers { get; set; }
        public ushort Flags { get; set; }
        public byte ItemContext { get; set; }
        public byte ToggleDifficultyID { get; set; }
        public ushort GroupSizeHealthCurveID { get; set; }
        public ushort GroupSizeDmgCurveID { get; set; }
        public ushort GroupSizeSpellPointsCurveID { get; set; }
    }
}

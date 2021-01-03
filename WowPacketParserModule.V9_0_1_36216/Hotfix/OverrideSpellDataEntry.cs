using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V9_0_1_36216.Hotfix
{
    [HotfixStructure(DB2Hash.OverrideSpellData, HasIndexInData = false)]
    public class OverrideSpellDataEntry
    {
        [HotfixArray(10)]
        public int[] Spells { get; set; }
        public int PlayerActionBarFileDataID { get; set; }
        public byte Flags { get; set; }
    }
}

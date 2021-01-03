using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V9_0_1_36216.Hotfix
{
    [HotfixStructure(DB2Hash.SpellReagents, HasIndexInData = false)]
    public class SpellReagentsEntry
    {
        public int SpellID { get; set; }
        [HotfixArray(8)]
        public int[] Reagent { get; set; }
        [HotfixArray(8)]
        public short[] ReagentCount { get; set; }
    }
}

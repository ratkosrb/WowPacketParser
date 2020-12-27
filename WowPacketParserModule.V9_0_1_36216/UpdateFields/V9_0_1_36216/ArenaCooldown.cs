using WowPacketParser.Misc;
using WowPacketParser.Store.Objects.UpdateFields;

namespace WowPacketParserModule.V9_0_1_36216.UpdateFields.V9_0_1_36216
{
    public class ArenaCooldown : IArenaCooldown
    {
        public int SpellID { get; set; }
        public int Charges { get; set; }
        public uint Flags { get; set; }
        public uint StartTime { get; set; }
        public uint EndTime { get; set; }
        public uint NextChargeTime { get; set; }
        public byte MaxCharges { get; set; }
    }
}


using WowPacketParser.SQL;

namespace WowPacketParser.Store.Objects
{
    public sealed class NpcTrainer : IDataModel
    {
        [DBFieldName("entry", true)]
        public uint? ID;

        [DBFieldName("spell", true)]
        public int? SpellID;

        [DBFieldName("spellcost")]
        public uint? MoneyCost;

        [DBFieldName("reqskill")]
        public uint? ReqSkillLine;

        [DBFieldName("reqskillvalue")]
        public uint? ReqSkillRank;

        [DBFieldName("reqlevel")]
        public uint? ReqLevel;
    }
}

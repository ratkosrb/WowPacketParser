using WowPacketParser.Misc;
using WowPacketParser.Store.Objects.UpdateFields;

namespace WowPacketParserModule.V9_0_1_36216.UpdateFields.V9_0_2_36639
{
    public class GameObjectData : IGameObjectData
    {
        public int DisplayID { get; set; }
        public uint SpellVisualID { get; set; }
        public uint StateSpellVisualID { get; set; }
        public uint SpawnTrackingStateAnimID { get; set; }
        public uint SpawnTrackingStateAnimKitID { get; set; }
        public uint StateWorldEffectsQuestObjectiveID { get; set; }
        public uint[] StateWorldEffectIDs { get; set; }
        public WowGuid CreatedBy { get; set; }
        public WowGuid GuildGUID { get; set; }
        public uint Flags { get; set; }
        public Quaternion ParentRotation { get; set; }
        public int FactionTemplate { get; set; }
        public sbyte State { get; set; }
        public sbyte TypeID { get; set; }
        public byte PercentHealth { get; set; }
        public byte AnimProgress => PercentHealth;
        public uint ArtKit { get; set; }
        public uint CustomParam { get; set; }
        public int Level { get; set; }
        public uint AnimGroupInstance { get; set; }
        public DynamicUpdateField<int> EnableDoodadSets { get; } = new DynamicUpdateField<int>();
    }
}


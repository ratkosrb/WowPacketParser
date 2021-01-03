using WowPacketParser.Misc;

namespace WowPacketParser.Store.Objects.UpdateFields
{
    public interface IGameObjectData
    {
        WowGuid CreatedBy { get; }
        int DisplayID { get; }
        uint Flags { get; }
        int Level { get; }
        Quaternion ParentRotation { get; }
        int FactionTemplate { get; }
        sbyte State { get; }
        sbyte TypeID { get; }
        byte PercentHealth { get; }
        byte AnimProgress { get; }

        IGameObjectData Clone();
    }
}

namespace WowPacketParser.Store.Objects.UpdateFields
{
    public interface IObjectData
    {
        int EntryID { get; }
        uint TypeID { get; }
        uint DynamicFlags { get; }
        float Scale { get; }

        IObjectData Clone();
    }
}

using WowPacketParser.Enums;
using WowPacketParser.SQL;

namespace WowPacketParser.Store.Objects
{
    [DBTableName("weather_update")]
    public sealed class WeatherUpdate : IDataModel
    {
        [DBFieldName("unixtimems", true)]
        public ulong UnixTimeMs;

        [DBFieldName("map_id")]
        public uint? MapId;

        [DBFieldName("zone_id")]
        public int ZoneId;

        [DBFieldName("weather_state")]
        public WeatherState? State;

        [DBFieldName("grade")]
        public float? Grade;

        [DBFieldName("sound", TargetedDatabase.Zero, TargetedDatabase.TheBurningCrusade)]
        public uint? Sound;

        [DBFieldName("instant")]
        public byte? Instant;
    }
}

using System;
using System.Collections.Generic;
using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.SQL;

namespace WowPacketParser.Store.Objects
{
    public sealed class ObjectSound
    {
        public ObjectSound(uint sound_, DateTime time_, WowGuid guid_)
        {
            sound = sound_;
            time = time_;
            guid = guid_;
        }
        public uint sound;
        public DateTime time;
        public WowGuid guid;
    }

    [DBTableName("play_sound")]
    public sealed class PlaySound : IDataModel
    {
        [DBFieldName("unixtimems", true)]
        public ulong UnixTimeMs;

        [DBFieldName("source_guid", true, true)]
        public string SourceGuid;

        [DBFieldName("source_id", true)]
        public uint SourceEntry;

        [DBFieldName("source_type", true)]
        public string SourceType;

        [DBFieldName("sound", true)]
        public uint Sound;
    }

    [DBTableName("play_music")]
    public sealed class PlayMusic : IDataModel
    {
        [DBFieldName("unixtimems", true)]
        public ulong UnixTimeMs;

        [DBFieldName("music", true)]
        public uint Music;
    }
}

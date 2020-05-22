﻿using WowPacketParser.Misc;
using WowPacketParser.SQL;

namespace WowPacketParser.Store.Objects
{
    public sealed class SpellTargetPosition : IDataModel
    {
        [DBFieldName("ID", true)]
        public uint? ID;

        [DBFieldName("EffectIndex", true)]
        public byte? EffectIndex;

        [DBFieldName("MapID")]
        public ushort? MapID;

        [DBFieldName("PositionX")]
        public float? PositionX;

        [DBFieldName("PositionY")]
        public float? PositionY;

        [DBFieldName("PositionZ")]
        public float? PositionZ;

        [DBFieldName("VerifiedBuild")]
        public int? VerifiedBuild = ClientVersion.BuildInt;

        public string EffectHelper;
    }

    public sealed class SpellCastData : IDataModel
    {
        [DBFieldName("casterId")]
        public uint? CasterID;

        [DBFieldName("casterType")]
        public string CasterType;

        [DBFieldName("spellId")]
        public uint? SpellID;

        [DBFieldName("castFlags")]
        public uint? CastFlags;

        [DBFieldName("castFlagsEx")]
        public uint? CastFlagsEx;

        [DBFieldName("targetId")]
        public uint TargetID = 0;

        [DBFieldName("targetType")]
        public string TargetType = "";

        [DBFieldName("verifiedBuild")]
        public int? VerifiedBuild = ClientVersion.BuildInt;
    }
}

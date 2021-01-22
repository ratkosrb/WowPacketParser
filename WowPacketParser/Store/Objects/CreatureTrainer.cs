﻿using WowPacketParser.SQL;
using WowPacketParser.Enums;

namespace WowPacketParser.Store.Objects
{
    [DBTableName("creature_trainer")]
    public class CreatureTrainer : IDataModel
    {
        [DBFieldName("entry", true, DbType = (TargetedDbType.WPP))]
        [DBFieldName("CreatureId", true, DbType = (TargetedDbType.TRINITY))]
        public uint? CreatureId;

        [DBFieldName("trainer_id", DbType = (TargetedDbType.WPP))]
        [DBFieldName("TrainerId", DbType = (TargetedDbType.TRINITY))]
        public uint? TrainerId;

        [DBFieldName("menu_id", true, DbType = (TargetedDbType.WPP))]
        [DBFieldName("MenuID", true, DbType = (TargetedDbType.TRINITY))]
        public uint? MenuID;

        [DBFieldName("option_index", true, DbType = (TargetedDbType.WPP))]
        [DBFieldName("OptionIndex", true, DbType = (TargetedDbType.TRINITY))]
        public uint? OptionIndex;
    }
}

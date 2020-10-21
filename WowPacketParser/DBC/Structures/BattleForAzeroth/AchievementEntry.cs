﻿using DBFileReaderLib.Attributes;

namespace WowPacketParser.DBC.Structures.BattleForAzeroth
{
    [DBFile("Achievement")]

    public sealed class AchievementEntry
    {
        public string Description;
        public string Title;
        public string Reward;
        [Index(false)]
        public uint ID;
        public short InstanceID;
        public sbyte Faction;
        public short Supercedes;
        public short Category;
        public sbyte MinimumCriteria;
        public sbyte Points;
        public int Flags;
        public short UiOrder;
        public int IconFileID;        
        public uint RewardItemID;
        public uint CriteriaTree;
        public short SharesCriteria;
    }
}

using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Store;

namespace WowPacketParser.SQL.Builders
{
    [BuilderClass]
    public static class QuestMisc
    {
        [BuilderMethod]
        public static string QuestOfferReward()
        {
            if (Storage.QuestOfferRewards.IsEmpty())
                return string.Empty;

            if (!Settings.SqlTables.quest_template)
                return string.Empty;

            var offerDb = SQLDatabase.Get(Storage.QuestOfferRewards);

            return SQLUtil.Compare(Storage.QuestOfferRewards, offerDb, StoreNameType.Quest);
        }

        [BuilderMethod]
        public static string QuestPOI()
        {
            if (Storage.QuestPOIs.IsEmpty())
                return string.Empty;

            string sql = string.Empty;

            if (Settings.SqlTables.quest_poi)
            {
                var poiDb = SQLDatabase.Get(Storage.QuestPOIs);

                sql = SQLUtil.Compare(Storage.QuestPOIs, poiDb, StoreNameType.Quest);
            }

            if (Settings.SqlTables.quest_poi_points)
            {
                if (!Storage.QuestPOIPoints.IsEmpty())
                {
                    var poiDb = SQLDatabase.Get(Storage.QuestPOIPoints);

                    sql += SQLUtil.Compare(Storage.QuestPOIPoints, poiDb, StoreNameType.Quest);
                }
            }

            return sql;
        }

        [BuilderMethod]
        public static string QuestGreeting()
        {
            if (!Settings.SqlTables.quest_template)
                return string.Empty;

            if (Settings.TargetedDatabase == TargetedDatabase.WrathOfTheLichKing ||
                Settings.TargetedDatabase == TargetedDatabase.Cataclysm)
                return string.Empty;

            if (Storage.QuestGreetings.IsEmpty())
                return string.Empty;

            var templatesDb = SQLDatabase.Get(Storage.QuestGreetings);

            return SQLUtil.Compare(Storage.QuestGreetings, templatesDb, StoreNameType.QuestGreeting);
        }

        [BuilderMethod]
        public static string QuestDetails()
        {
            if (!Settings.SqlTables.quest_template)
                return string.Empty;

            if (Storage.QuestDetails.IsEmpty())
                return string.Empty;

            var templatesDb = SQLDatabase.Get(Storage.QuestDetails);

            return SQLUtil.Compare(Storage.QuestDetails, templatesDb, StoreNameType.Quest);
        }

        [BuilderMethod]
        public static string QuestRequestItems()
        {
            if (!Settings.SqlTables.quest_template)
                return string.Empty;

            if (Storage.QuestRequestItems.IsEmpty())
                return string.Empty;

            var templatesDb = SQLDatabase.Get(Storage.QuestRequestItems);

            return SQLUtil.Compare(Storage.QuestRequestItems, templatesDb, StoreNameType.Quest);
         }

        [BuilderMethod]
        public static string QuestStarters()
        {
            if (!Settings.SqlTables.quest_starter)
                return string.Empty;

            if (Storage.QuestStarters.IsEmpty())
                return string.Empty;

            var templatesDb = SQLDatabase.Get(Storage.QuestStarters);

            return SQLUtil.Compare(Storage.QuestStarters, templatesDb, StoreNameType.Quest);
        }

        [BuilderMethod]
        public static string QuestEnders()
        {
            if (!Settings.SqlTables.quest_ender)
                return string.Empty;

            if (Storage.QuestEnders.IsEmpty())
                return string.Empty;

            var templatesDb = SQLDatabase.Get(Storage.QuestEnders);

            return SQLUtil.Compare(Storage.QuestEnders, templatesDb, StoreNameType.Quest);
        }

        [BuilderMethod]
        public static string QuestClientAcceptTimes()
        {
            if (!Settings.SqlTables.client_quest_accept)
                return string.Empty;

            if (Storage.QuestClientAcceptTimes.IsEmpty())
                return string.Empty;

            var templatesDb = SQLDatabase.Get(Storage.QuestClientAcceptTimes);

            return SQLUtil.Compare(Storage.QuestClientAcceptTimes, templatesDb, StoreNameType.None);
        }

        [BuilderMethod]
        public static string QuestClientCompleteTimes()
        {
            if (!Settings.SqlTables.client_quest_complete)
                return string.Empty;

            if (Storage.QuestClientCompleteTimes.IsEmpty())
                return string.Empty;

            var templatesDb = SQLDatabase.Get(Storage.QuestClientCompleteTimes);

            return SQLUtil.Compare(Storage.QuestClientCompleteTimes, templatesDb, StoreNameType.None);
        }

        [BuilderMethod]
        public static string QuestCompleteTimes()
        {
            if (!Settings.SqlTables.quest_update_complete)
                return string.Empty;

            if (Storage.QuestCompleteTimes.IsEmpty())
                return string.Empty;

            var templatesDb = SQLDatabase.Get(Storage.QuestCompleteTimes);

            return SQLUtil.Compare(Storage.QuestCompleteTimes, templatesDb, StoreNameType.None);
        }

        [BuilderMethod]
        public static string QuestFailTimes()
        {
            if (!Settings.SqlTables.quest_update_failed)
                return string.Empty;

            if (Storage.QuestFailTimes.IsEmpty())
                return string.Empty;

            var templatesDb = SQLDatabase.Get(Storage.QuestFailTimes);

            return SQLUtil.Compare(Storage.QuestFailTimes, templatesDb, StoreNameType.None);
        }
    }
}

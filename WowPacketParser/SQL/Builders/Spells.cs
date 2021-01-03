using System.Text;
using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Store;
using WowPacketParser.Store.Objects;

namespace WowPacketParser.SQL.Builders
{
    [BuilderClass]
    public static class Spells
    {
        [BuilderMethod]
        public static string SpellTargetPosition()
        {
            if (Storage.SpellTargetPositions.IsEmpty())
                return string.Empty;

            if (!Settings.SqlTables.spell_target_position)
                return string.Empty;

            return SQLUtil.Compare(Storage.SpellTargetPositions, SQLDatabase.Get(Storage.SpellTargetPositions), t => t.EffectHelper);
        }

        [BuilderMethod]
        public static string SpellCastStart()
        {
            if (Storage.SpellCastStart.IsEmpty())
                return string.Empty;

            if (!Settings.SqlTables.spell_cast_start)
                return string.Empty;

            var spellRows = new RowList<SpellCastStart>();
            foreach (var cast_pair in Storage.SpellCastStart)
            {
                Row<SpellCastStart> row = new Row<SpellCastStart>();

                row.Data.UnixTimeMs = (ulong)Utilities.GetUnixTimeMsFromDateTime(cast_pair.Item1.Time);
                row.Data.SpellId = cast_pair.Item1.SpellID;
                row.Data.VisualId = cast_pair.Item1.VisualID;
                row.Data.CastTime = cast_pair.Item1.CastTime;
                row.Data.CastFlags = cast_pair.Item1.CastFlags;
                row.Data.CastFlagsEx = cast_pair.Item1.CastFlagsEx;
                row.Data.AmmoDisplayId = cast_pair.Item1.AmmoDisplayId;
                row.Data.AmmoInventoryType = cast_pair.Item1.AmmoInventoryType;

                Storage.GetObjectDbGuidEntryType(cast_pair.Item1.CasterGuid, out row.Data.CasterGuid, out row.Data.CasterId, out row.Data.CasterType);
                Storage.GetObjectDbGuidEntryType(cast_pair.Item1.CasterUnitGuid, out row.Data.CasterUnitGuid, out row.Data.CasterUnitId, out row.Data.CasterUnitType);
                Storage.GetObjectDbGuidEntryType(cast_pair.Item1.MainTargetGuid, out row.Data.TargetGuid, out row.Data.TargetId, out row.Data.TargetType);

                spellRows.Add(row);
            }
            var spellsSql = new SQLInsert<SpellCastStart>(spellRows, false);
            return spellsSql.Build();
        }

        [BuilderMethod]
        public static string SpellCastGo()
        {
            if (Storage.SpellCastGo.IsEmpty())
                return string.Empty;

            if (!Settings.SqlTables.spell_cast_go)
                return string.Empty;

            uint maxListId = 0;
            uint maxPositionId = 0;
            var spellRows = new RowList<SpellCastGo>();
            var spellTargetRows = new RowList<SpellCastGoTarget>();
            var spellPositionRows = new RowList<SpellCastGoPosition>();
            StringBuilder result = new StringBuilder();
            foreach (var cast_pair in Storage.SpellCastGo)
            {
                Row<SpellCastGo> row = new Row<SpellCastGo>();

                row.Data.UnixTimeMs = (ulong)Utilities.GetUnixTimeMsFromDateTime(cast_pair.Item1.Time);
                row.Data.SpellId = cast_pair.Item1.SpellID;
                row.Data.VisualId = cast_pair.Item1.VisualID;
                row.Data.CastFlags = cast_pair.Item1.CastFlags;
                row.Data.CastFlagsEx = cast_pair.Item1.CastFlagsEx;
                row.Data.AmmoDisplayId = cast_pair.Item1.AmmoDisplayId;
                row.Data.AmmoInventoryType = cast_pair.Item1.AmmoInventoryType;
                row.Data.HitTargetsCount = cast_pair.Item1.HitTargetsCount;
                row.Data.MissTargetsCount = cast_pair.Item1.MissTargetsCount;

                Storage.GetObjectDbGuidEntryType(cast_pair.Item1.CasterGuid, out row.Data.CasterGuid, out row.Data.CasterId, out row.Data.CasterType);
                Storage.GetObjectDbGuidEntryType(cast_pair.Item1.CasterUnitGuid, out row.Data.CasterUnitGuid, out row.Data.CasterUnitId, out row.Data.CasterUnitType);
                Storage.GetObjectDbGuidEntryType(cast_pair.Item1.MainTargetGuid, out row.Data.MainTargetGuid, out row.Data.MainTargetId, out row.Data.MainTargetType);

                if (cast_pair.Item1.HitTargetsCount > 0)
                {
                    row.Data.HitTargetsListId = ++maxListId;
                    foreach (WowGuid target in cast_pair.Item1.HitTargetsList)
                    {
                        Row<SpellCastGoTarget> targetRow = new Row<SpellCastGoTarget>();
                        targetRow.Data.ListId = row.Data.HitTargetsListId;
                        Storage.GetObjectDbGuidEntryType(target, out targetRow.Data.TargetGuid, out targetRow.Data.TargetId, out targetRow.Data.TargetType);
                        spellTargetRows.Add(targetRow);
                    }
                }
                else
                    row.Data.HitTargetsListId = 0;

                if (cast_pair.Item1.MissTargetsCount > 0)
                {
                    row.Data.MissTargetsListId = ++maxListId;
                    foreach (WowGuid target in cast_pair.Item1.MissTargetsList)
                    {
                        Row<SpellCastGoTarget> targetRow = new Row<SpellCastGoTarget>();
                        targetRow.Data.ListId = row.Data.MissTargetsListId;
                        Storage.GetObjectDbGuidEntryType(target, out targetRow.Data.TargetGuid, out targetRow.Data.TargetId, out targetRow.Data.TargetType);
                        spellTargetRows.Add(targetRow);
                    }
                }
                else
                    row.Data.MissTargetsListId = 0;

                if (cast_pair.Item1.SrcPosition != null &&
                   (cast_pair.Item1.SrcPosition.X != 0 || cast_pair.Item1.SrcPosition.Y != 0 || cast_pair.Item1.SrcPosition.Z != 0))
                {
                    row.Data.SrcPositionId = ++maxPositionId;
                    Row<SpellCastGoPosition> positionRow = new Row<SpellCastGoPosition>();
                    positionRow.Data.Id = row.Data.SrcPositionId;
                    positionRow.Data.PositionX = cast_pair.Item1.SrcPosition.X;
                    positionRow.Data.PositionY = cast_pair.Item1.SrcPosition.Y;
                    positionRow.Data.PositionZ = cast_pair.Item1.SrcPosition.Z;
                    spellPositionRows.Add(positionRow);
                }
                else
                    row.Data.SrcPositionId = 0;

                if (cast_pair.Item1.DstPosition != null &&
                   (cast_pair.Item1.DstPosition.X != 0 || cast_pair.Item1.DstPosition.Y != 0 || cast_pair.Item1.DstPosition.Z != 0))
                {
                    row.Data.DstPositionId = ++maxPositionId;
                    Row<SpellCastGoPosition> positionRow = new Row<SpellCastGoPosition>();
                    positionRow.Data.Id = row.Data.DstPositionId;
                    positionRow.Data.PositionX = cast_pair.Item1.DstPosition.X;
                    positionRow.Data.PositionY = cast_pair.Item1.DstPosition.Y;
                    positionRow.Data.PositionZ = cast_pair.Item1.DstPosition.Z;
                    spellPositionRows.Add(positionRow);
                }
                else
                    row.Data.DstPositionId = 0;

                spellRows.Add(row);
            }
            var spellsSql = new SQLInsert<SpellCastGo>(spellRows, false);
            result.Append(spellsSql.Build());
            result.AppendLine();
            var targetsSql = new SQLInsert<SpellCastGoTarget>(spellTargetRows, false);
            result.Append(targetsSql.Build());
            var positionSql = new SQLInsert<SpellCastGoPosition>(spellPositionRows, false);
            result.Append(positionSql.Build());
            return result.ToString();
        }

        [BuilderMethod]
        public static string SpellChannelStart()
        {
            if (Storage.SpellChannelStart.IsEmpty())
                return string.Empty;

            if (!Settings.SqlTables.spell_channel_start)
                return string.Empty;

            var spellRows = new RowList<SpellChannelStart>();
            foreach (var channel in Storage.SpellChannelStart)
            {
                if (channel.Item1.Guid.GetObjectType() == ObjectType.Player && !Settings.SavePlayerCasts)
                    continue;

                Row<SpellChannelStart> row = new Row<SpellChannelStart>();
                row.Data = channel.Item1;
                Storage.GetObjectDbGuidEntryType(channel.Item1.Guid, out row.Data.CasterGuid, out row.Data.CasterId, out row.Data.CasterType);
                row.Data.UnixTimeMs = (ulong)Utilities.GetUnixTimeMsFromDateTime(channel.Item1.Time);
                spellRows.Add(row);
            }
            var spellsSql = new SQLInsert<SpellChannelStart>(spellRows, false);
            return spellsSql.Build();
        }

        [BuilderMethod]
        public static string SpellChannelUpdate()
        {
            if (Storage.SpellChannelUpdate.IsEmpty())
                return string.Empty;

            if (!Settings.SqlTables.spell_channel_update)
                return string.Empty;

            var spellRows = new RowList<SpellChannelUpdate>();
            foreach (var channel in Storage.SpellChannelUpdate)
            {
                if (channel.Item1.Guid.GetObjectType() == ObjectType.Player && !Settings.SavePlayerCasts)
                    continue;

                Row<SpellChannelUpdate> row = new Row<SpellChannelUpdate>();
                row.Data = channel.Item1;
                Storage.GetObjectDbGuidEntryType(channel.Item1.Guid, out row.Data.CasterGuid, out row.Data.CasterId, out row.Data.CasterType);
                row.Data.UnixTimeMs = (ulong)Utilities.GetUnixTimeMsFromDateTime(channel.Item1.Time);
                spellRows.Add(row);
            }
            var spellsSql = new SQLInsert<SpellChannelUpdate>(spellRows, false);
            return spellsSql.Build();
        }

        [BuilderMethod(false)]
        public static string SpellPetCooldown()
        {
            if (!Settings.SqlTables.spell_pet_cooldown)
                return string.Empty;

            if (Storage.SpellPetCooldown.IsEmpty())
                return string.Empty;

            var templatesDb = SQLDatabase.Get(Storage.SpellPetCooldown);

            return SQLUtil.Compare(Storage.SpellPetCooldown, templatesDb, StoreNameType.None);
        }

        [BuilderMethod(false)]
        public static string SpellPetActions()
        {
            if (!Settings.SqlTables.spell_pet_action)
                return string.Empty;

            if (Storage.SpellPetActions.IsEmpty())
                return string.Empty;

            var templatesDb = SQLDatabase.Get(Storage.SpellPetActions);

            return SQLUtil.Compare(Storage.SpellPetActions, templatesDb, StoreNameType.None);
        }

        [BuilderMethod]
        public static string PlaySpellVisualKit()
        {
            if (Storage.SpellPlayVisualKit.IsEmpty())
                return string.Empty;

            if (!Settings.SqlTables.play_spell_visual_kit)
                return string.Empty;

            var spellRows = new RowList<PlaySpellVisualKit>();
            foreach (var spellVisual in Storage.SpellPlayVisualKit)
            {
                if (spellVisual.Item1.Guid.GetObjectType() == ObjectType.Player && !Settings.SavePlayerCasts)
                    continue;

                Row<PlaySpellVisualKit> row = new Row<PlaySpellVisualKit>();
                row.Data = spellVisual.Item1;
                Storage.GetObjectDbGuidEntryType(spellVisual.Item1.Guid, out row.Data.CasterGuid, out row.Data.CasterId, out row.Data.CasterType);
                row.Data.UnixTimeMs = (ulong)Utilities.GetUnixTimeMsFromDateTime(spellVisual.Item1.Time);
                spellRows.Add(row);
            }
            var spellsSql = new SQLInsert<PlaySpellVisualKit>(spellRows, false);
            return spellsSql.Build();
        }

        [BuilderMethod]
        public static string SpellCastFailed()
        {
            if (Storage.SpellCastFailed.IsEmpty())
                return string.Empty;

            if (!Settings.SqlTables.spell_cast_failed)
                return string.Empty;

            var spellRows = new RowList<SpellCastFailed>();
            foreach (var failedCast in Storage.SpellCastFailed)
            {
                if (failedCast.Item1.Guid.GetObjectType() == ObjectType.Player && !Settings.SavePlayerCasts)
                    continue;

                Row<SpellCastFailed> row = new Row<SpellCastFailed>();
                row.Data = failedCast.Item1;
                Storage.GetObjectDbGuidEntryType(failedCast.Item1.Guid, out row.Data.CasterGuid, out row.Data.CasterId, out row.Data.CasterType);
                row.Data.UnixTimeMs = (ulong)Utilities.GetUnixTimeMsFromDateTime(failedCast.Item1.Time);
                spellRows.Add(row);
            }
            var spellsSql = new SQLInsert<SpellCastFailed>(spellRows, false);
            return spellsSql.Build();
        }
    }
}

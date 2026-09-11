using System.Collections.Generic;
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
        public static string SpellScriptTarget()
        {
            if (Storage.SpellScriptTargets.IsEmpty())
                return string.Empty;

            if (!Settings.SqlTables.spell_script_target)
                return string.Empty;

            if (Settings.TargetedDbType == TargetedDbType.WPP)
                return SQLUtil.Insert(Storage.SpellScriptTargets, false, true);

            return SQLUtil.Compare(Storage.SpellScriptTargets, SQLDatabase.Get(Storage.SpellScriptTargets), StoreNameType.None);
        }

        [BuilderMethod]
        public static string SpellTargetPosition()
        {
            if (Storage.SpellTargetPositions.IsEmpty())
                return string.Empty;

            if (!Settings.SqlTables.spell_target_position)
                return string.Empty;

            if (Settings.TargetedDbType == TargetedDbType.WPP)
                return SQLUtil.Insert(Storage.SpellTargetPositions, false, true);

            return SQLUtil.Compare(Storage.SpellTargetPositions, SQLDatabase.Get(Storage.SpellTargetPositions), t => t.EffectHelper);
        }

        [BuilderMethod]
        public static string SpellUniqueCasters()
        {
            if (Storage.SpellUniqueCasters.IsEmpty())
                return string.Empty;

            if (!Settings.SqlTables.spell_unique_caster)
                return string.Empty;

            return SQLUtil.MakeInsertWithSniffIdList(Storage.SpellUniqueCasters, false, true);
        }

        [BuilderMethod]
        public static string SpellUniqueChainTargets()
        {
            if (Storage.SpellUniqueChainUpdates.IsEmpty())
                return string.Empty;

            if (!Settings.SqlTables.spell_unique_chain_updates)
                return string.Empty;

            return SQLUtil.MakeInsertWithSniffIdList(Storage.SpellUniqueChainUpdates, false, true);
        }

        [BuilderMethod]
        public static string SpellAuraFlags()
        {
            if (Storage.SpellAuraFlags.IsEmpty())
                return string.Empty;

            if (!Settings.SqlTables.spell_aura_flags)
                return string.Empty;

            return SQLUtil.Insert(Storage.SpellAuraFlags, false, true);
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
                row.Data.Orientation = cast_pair.Item1.Orientation;

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
                    for (int i = 0; i < cast_pair.Item1.MissTargetsList.Count; i++)
                    {
                        WowGuid target = cast_pair.Item1.MissTargetsList[i];
                        Row<SpellCastGoTarget> targetRow = new Row<SpellCastGoTarget>();
                        targetRow.Data.ListId = row.Data.MissTargetsListId;
                        Storage.GetObjectDbGuidEntryType(target, out targetRow.Data.TargetGuid, out targetRow.Data.TargetId, out targetRow.Data.TargetType);
                        targetRow.Data.MissReason = cast_pair.Item1.MissTargetsCount == cast_pair.Item1.MissReasonsCount ? cast_pair.Item1.MissReasonsList[i] : 0;
                        spellTargetRows.Add(targetRow);
                    }
                }
                else
                    row.Data.MissTargetsListId = 0;

                if (cast_pair.Item1.SrcPosition != null &&
                   (cast_pair.Item1.SrcPosition.Value.X != 0 || cast_pair.Item1.SrcPosition.Value.Y != 0 || cast_pair.Item1.SrcPosition.Value.Z != 0))
                {
                    row.Data.SrcPositionId = ++maxPositionId;
                    Row<SpellCastGoPosition> positionRow = new Row<SpellCastGoPosition>();
                    positionRow.Data.Id = row.Data.SrcPositionId;
                    positionRow.Data.PositionX = cast_pair.Item1.SrcPosition.Value.X;
                    positionRow.Data.PositionY = cast_pair.Item1.SrcPosition.Value.Y;
                    positionRow.Data.PositionZ = cast_pair.Item1.SrcPosition.Value.Z;
                    spellPositionRows.Add(positionRow);
                }
                else
                    row.Data.SrcPositionId = 0;

                if (cast_pair.Item1.DstPosition != null &&
                   (cast_pair.Item1.DstPosition.Value.X != 0 || cast_pair.Item1.DstPosition.Value.Y != 0 || cast_pair.Item1.DstPosition.Value.Z != 0))
                {
                    row.Data.DstPositionId = ++maxPositionId;
                    Row<SpellCastGoPosition> positionRow = new Row<SpellCastGoPosition>();
                    positionRow.Data.Id = row.Data.DstPositionId;
                    positionRow.Data.PositionX = cast_pair.Item1.DstPosition.Value.X;
                    positionRow.Data.PositionY = cast_pair.Item1.DstPosition.Value.Y;
                    positionRow.Data.PositionZ = cast_pair.Item1.DstPosition.Value.Z;
                    spellPositionRows.Add(positionRow);
                }
                else
                    row.Data.DstPositionId = 0;

                spellRows.Add(row);
            }
            if (spellRows.Count != 0)
            {
                var spellsSql = new SQLInsert<SpellCastGo>(spellRows, false);
                result.Append(spellsSql.Build());
                result.AppendLine();
            }
            if (spellTargetRows.Count != 0)
            {
                var targetsSql = new SQLInsert<SpellCastGoTarget>(spellTargetRows, false);
                result.Append(targetsSql.Build());
                result.AppendLine();
            }
            if (spellPositionRows.Count != 0)
            {
                var positionSql = new SQLInsert<SpellCastGoPosition>(spellPositionRows, false);
                result.Append(positionSql.Build());
            }
            
            return result.ToString();
        }

        [BuilderMethod]
        public static string SpellChainUpdates()
        {
            if (Storage.SpellChainUpdates.IsEmpty())
                return string.Empty;

            if (!Settings.SqlTables.spell_chain_update)
                return string.Empty;

            uint targetListId = 0;
            var spellRows = new RowList<SpellChainUpdate>();
            var targetRows = new RowList<SpellChainUpdateTarget>();
            foreach (var chain in Storage.SpellChainUpdates)
            {
                if (chain.Item1.Guid.GetObjectType() == ObjectType.Player && !Settings.SavePlayerCasts)
                    continue;

                if (chain.Item1.TargetsList.Count > 0)
                {
                    chain.Item1.TargetsListId = ++targetListId;
                    chain.Item1.TargetsCount = (uint)chain.Item1.TargetsList.Count;

                    foreach (WowGuid guid in chain.Item1.TargetsList)
                    {
                        Row<SpellChainUpdateTarget> row2 = new Row<SpellChainUpdateTarget>();
                        row2.Data = new SpellChainUpdateTarget();
                        row2.Data.ListId = chain.Item1.TargetsListId;
                        Storage.GetObjectDbGuidEntryType(guid, out row2.Data.TargetGuid, out row2.Data.TargetId, out row2.Data.TargetType);
                        targetRows.Add(row2);
                    }
                }

                Row<SpellChainUpdate> row = new Row<SpellChainUpdate>();
                row.Data = chain.Item1;
                Storage.GetObjectDbGuidEntryType(chain.Item1.Guid, out row.Data.CasterGuid, out row.Data.CasterId, out row.Data.CasterType);
                row.Data.UnixTimeMs = (ulong)Utilities.GetUnixTimeMsFromDateTime(chain.Item1.Time);
                spellRows.Add(row);
            }

            StringBuilder result = new StringBuilder();
            if (spellRows.Count != 0)
            {
                var spellsSql = new SQLInsert<SpellChainUpdate>(spellRows, false);
                result.Append(spellsSql.Build());
                result.AppendLine();
            }
            if (targetRows.Count != 0)
            {
                var targetsSql = new SQLInsert<SpellChainUpdateTarget>(targetRows, false);
                result.Append(targetsSql.Build());
            }
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
        public static string CreaturePetCooldown()
        {
            if (!Settings.SqlTables.creature_pet_cooldown)
                return string.Empty;

            if (Storage.CreaturePetCooldown.IsEmpty())
                return string.Empty;

            return SQLUtil.Insert(Storage.CreaturePetCooldown, false, true);
        }

        [BuilderMethod(false)]
        public static string CreaturePetRemainingCooldown()
        {
            if (!Settings.SqlTables.creature_pet_remaining_cooldown)
                return string.Empty;

            if (Storage.CreaturePetRemainingCooldown.IsEmpty())
                return string.Empty;

            return SQLUtil.Insert(Storage.CreaturePetRemainingCooldown, false, true);
        }

        [BuilderMethod(false)]
        public static string CreaturePetActions()
        {
            if (!Settings.SqlTables.creature_pet_actions)
                return string.Empty;

            if (Storage.CreaturePetActions.IsEmpty())
                return string.Empty;

            string sql = "";

            List<WowGuid> exportedGuids = new List<WowGuid>();
            foreach (var row in Storage.CreaturePetActions)
            {
                if (row.Item1.CasterGUID != null)
                {
                    if (exportedGuids.Contains(row.Item1.CasterGUID))
                        continue;

                    exportedGuids.Add(row.Item1.CasterGUID);
                }

                if (!string.IsNullOrEmpty(sql))
                    sql += ",\n";

                sql += "(" + row.Item1.CasterID.ToString();
                foreach (var spellId in row.Item1.SpellID)
                    sql += ", " + spellId.ToString();

                sql += ", " + row.Item1.SniffId + ", " + row.Item1.SniffBuild;
                sql += ")";
            }
            sql = "INSERT IGNORE INTO `creature_pet_actions` (`entry`, `slot1`, `slot2`, `slot3`, `slot4`, `slot5`, `slot6`, `slot7`, `slot8`, `slot9`, `slot10`, `sniff_id`, `sniff_build`) VALUES\n" + sql + ";\n";

            return sql;
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

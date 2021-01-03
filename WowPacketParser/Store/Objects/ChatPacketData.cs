using System;
using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.SQL;

namespace WowPacketParser.Store.Objects
{
    public sealed class ChatPacketData
    {
        public string Text;
        public ChatMessageType? Type;
        public Language? Language;
        public WowGuid SenderGUID;
        public string SenderName;
        public WowGuid ReceiverGUID;
        public string ReceiverName;
        public string ChannelName;
        public DateTime Time;
    }

    [DBTableName("player_chat")]
    public sealed class CharacterChat : IDataModel
    {
        [DBFieldName("unixtimems", true)]
        public ulong UnixTimeMs;

        [DBFieldName("guid", true, true)]
        public string Guid;

        [DBFieldName("sender_name", true)]
        public string SenderName;

        [DBFieldName("text")]
        public string Text;

        [DBFieldName("chat_type")]
        public ChatMessageType? Type;

        [DBFieldName("channel_name")]
        public string ChannelName;

        public WowGuid SenderGUID;
    }

    [DBTableName("creature_text")]
    public sealed class CreatureText : IDataModel
    {
        [DBFieldName("unixtimems", true)]
        public ulong UnixTimeMs;

        [DBFieldName("guid", true, true)]
        public string Guid;

        [DBFieldName("entry", true)]
        public uint? Entry;

        [DBFieldName("group_id", true)]
        public uint GroupId;

        [DBFieldName("health_percent", false, false, true)]
        public float? HealthPercent;

        public WowGuid SenderGUID;
        public string Text;
    }

    [DBTableName("creature_text_template")]
    public sealed class CreatureTextTemplate : IDataModel
    {
        [DBFieldName("entry", true)]
        public uint? Entry;

        [DBFieldName("group_id", true)]
        public uint GroupId;

        [DBFieldName("text")]
        public string Text;

        [DBFieldName("chat_type")]
        public ChatMessageType? Type;

        [DBFieldName("language")]
        public Language? Language;

        [DBFieldName("emote")]
        public EmoteType? Emote;

        [DBFieldName("sound")]
        public uint? Sound;

        [DBFieldName("broadcast_text_id", false, true)]
        public object BroadcastTextID = 0;

        [DBFieldName("comment")]
        public string Comment;

        public WowGuid SenderGUID;
        public string SenderName;
        public WowGuid ReceiverGUID;
        public string ReceiverName;
        public DateTime Time;

        public string BroadcastTextIDHelper;

        public CreatureTextTemplate() { }
        public CreatureTextTemplate(ChatPacketData textTemplate)
        {
            Text = textTemplate.Text;
            Type = textTemplate.Type;
            Language = textTemplate.Language;
            SenderGUID = textTemplate.SenderGUID;
            SenderName = textTemplate.SenderName;
            ReceiverGUID = textTemplate.ReceiverGUID;
            ReceiverName = textTemplate.ReceiverName;
            Time = textTemplate.Time;
        }
    }

    [DBTableName("gameobject_text")]
    public sealed class GameObjectText : IDataModel
    {
        [DBFieldName("unixtimems", true)]
        public ulong UnixTimeMs;

        [DBFieldName("guid", true, true)]
        public string Guid;

        [DBFieldName("entry", true)]
        public uint? Entry;

        [DBFieldName("group_id", true)]
        public uint GroupId;

        public WowGuid SenderGUID;
        public string Text;
    }

    [DBTableName("gameobject_text_template")]
    public sealed class GameObjectTextTemplate : IDataModel
    {
        [DBFieldName("entry", true)]
        public uint? Entry;

        [DBFieldName("group_id", true)]
        public uint GroupId;

        [DBFieldName("text")]
        public string Text;

        [DBFieldName("chat_type")]
        public ChatMessageType? Type;

        [DBFieldName("language")]
        public Language? Language;

        [DBFieldName("sound")]
        public uint? Sound;

        [DBFieldName("broadcast_text_id", false, true)]
        public object BroadcastTextID = 0;

        [DBFieldName("comment")]
        public string Comment;

        public WowGuid SenderGUID;
        public string SenderName;
        public WowGuid ReceiverGUID;
        public string ReceiverName;
        public DateTime Time;

        public string BroadcastTextIDHelper;

        public GameObjectTextTemplate() { }
        public GameObjectTextTemplate(ChatPacketData textTemplate)
        {
            Text = textTemplate.Text;
            Type = textTemplate.Type;
            Language = textTemplate.Language;
            SenderGUID = textTemplate.SenderGUID;
            SenderName = textTemplate.SenderName;
            ReceiverGUID = textTemplate.ReceiverGUID;
            ReceiverName = textTemplate.ReceiverName;
            Time = textTemplate.Time;
        }
    }

    [DBTableName("world_text")]
    public sealed class WorldText : IDataModel
    {
        [DBFieldName("unixtimems", true)]
        public ulong UnixTimeMs;

        [DBFieldName("text")]
        public string Text;

        [DBFieldName("chat_type")]
        public ChatMessageType? Type;

        [DBFieldName("language")]
        public Language? Language;
    }
}

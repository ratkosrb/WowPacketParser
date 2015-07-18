﻿using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using WowPacketParser.Enums.Version;
using WowPacketParser.Loading;
using WowPacketParser.Misc;

namespace WowPacketParser.Saving
{
    public static class SplitLocaleDirectionBinaryPacketWriter
    {
        private const string Folder = "split"; // might want to move to config later
        private static Encoding _encoding;

        public static void Write(IEnumerable<Packet> packets, Encoding encoding)
        {
            _encoding = encoding;

            // not doing anything if it exists already
            Directory.CreateDirectory(Folder);

            // split packets by opcode (group is a set of packets all with the same opcode)
            var groups = packets.GroupBy(p => p.Opcode);

            // since we have one file per opcode it's possible to parallelize groups writing
            Parallel.ForEach(groups, WriteGroup);
        }

        private static void WriteGroup(IGrouping<int, Packet> groups)
        {
            var groupDir = groups.GroupBy(p => p.Direction);
            foreach (var group in groupDir)
            {
                Directory.CreateDirectory(Folder + "/" + BinaryPacketReader.GetClientLocale() + "/" + group.Key);
                var fileName = Folder + "/" + BinaryPacketReader.GetClientLocale() + "/" + group.Key + "/" + Opcodes.GetOpcodeName(groups.Key, group.Key) + ".pkt";

                using (var fileStream = new FileStream(fileName, FileMode.Append, FileAccess.Write))
                using (var writer = new BinaryWriter(fileStream, _encoding))
                {
                    if (writer.BaseStream.Position == 0)
                    {
                        writer.Write(Encoding.ASCII.GetBytes("Loc"));
                        writer.Write((int)ClientVersion.Build);
                        writer.Write(Encoding.ASCII.GetBytes(BinaryPacketReader.GetClientLocale()));
                    }

                    foreach (var packet in group)
                    {
                        writer.Write((ushort)packet.Opcode);
                        writer.Write((int)packet.Length);
                        writer.Write((byte)packet.Direction);
                        writer.Write((ulong)Utilities.GetUnixTimeFromDateTime(packet.Time));
                        writer.Write(packet.GetStream(0));
                    }
                }
            }
        }
    }
}

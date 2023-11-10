using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace MinecraftEditor.Nbt
{
    public static class NbtWriter
    {
        /// <summary>
        /// writes the contents of the NBT file
        /// </summary>
        public static void WriteRoot(Stream stream, NbtCompound tree)
        {
            switch(tree.Keys.Count)
            {
                case 0:
                    stream.WriteByte((byte)TagType.END);
                    return;
                case 1:
                    break;
                default:
                    throw new Exception("Expected root of tree to have a single key");
            }

            string key = tree.Keys.First();
            object value = tree[key];

            var writer = GetValueWriter(value);

            //write the type of the value
            stream.WriteByte((byte)writer.TagType);

            //then the key
            WriteString(stream, key);

            //then the value
            writer.WriteValue(stream, value);
        }

        /// <summary>
        /// Compounds are a massive pain
        /// as you have write the type of the value, then the key, the value
        /// so the TagType and value need to be split up into separate parts
        /// </summary>
        private static (TagType TagType, Action<Stream, object> WriteValue) GetValueWriter(object value)
        {
            switch(value)
            {
                case NbtCompound:
                    return (TagType.COMPOUND, (stream, x) => WriteCompound(stream, (NbtCompound)x));
                case int:
                    return (TagType.INT, (stream, x) => WriteInt(stream, (int)x));
                case double:
                    return (TagType.DOUBLE, (stream, x) => WriteDouble(stream, (double)x));
                case sbyte:
                    return (TagType.BYTE, (stream, x) => WriteByte(stream, (sbyte)x));
                case long:
                    return (TagType.LONG, (stream, x) => WriteLong(stream, (long)x));
                case NbtList:
                    return (TagType.LIST, (stream, x) => WriteList(stream, (NbtList)x));
                case string:
                    return (TagType.STRING, (stream, x) => WriteString(stream, (string)x));
                case int[]:
                    return (TagType.INT_ARRAY, (stream, x) => WriteIntArray(stream, (int[])x));
                case short:
                    return (TagType.SHORT, (stream, x) => WriteShort(stream, (short)x));
                case float:
                    return (TagType.FLOAT, (stream, x) => WriteFloat(stream, (float)x));
            }

            throw new NotImplementedException("unsupported value type: " + value.GetType());
        }

        private static void WriteIntArray(Stream stream, int[] values)
        {
            WriteInt(stream, values.Length);

            foreach(int value in values)
            {
                WriteInt(stream, value);
            }
        }

        private static void WriteList(Stream stream, NbtList list)
        {
            if(list.Count == 0)
            {
                stream.WriteByte((byte)TagType.END);
                WriteInt(stream, list.Count);
                return;
            }

            var valueWriter = GetValueWriter(list.First());

            stream.WriteByte((byte)valueWriter.TagType);
            WriteInt(stream, list.Count);

            foreach(var value in list)
            {
                valueWriter.WriteValue(stream, value);
            }
        }

        private static void WriteFloat(Stream stream, float value)
        {
            byte[] array = BitConverter.GetBytes(value);
            if (BitConverter.IsLittleEndian)
            {
                Array.Reverse(array);
            }

            stream.Write(array, 0, 4);
        }

        private static void WriteLong(Stream stream, long value)
        {
            byte[] array = BitConverter.GetBytes(value);
            if (BitConverter.IsLittleEndian)
            {
                Array.Reverse(array);
            }

            stream.Write(array, 0, 8);
        }

        private static void WriteByte(Stream stream, sbyte sb)
        {
            stream.WriteByte((byte)sb);
        }

        private static void WriteDouble(Stream stream, double value)
        {
            byte[] array = BitConverter.GetBytes(value);
            if (BitConverter.IsLittleEndian)
            {
                Array.Reverse(array);
            }

            stream.Write(array, 0, 8);
        }

        private static void WriteInt(Stream stream, int value)
        {
            byte[] array = BitConverter.GetBytes(value);
            if (BitConverter.IsLittleEndian)
            {
                Array.Reverse(array);
            }

            stream.Write(array, 0, 4);
        }

        private static void WriteCompound(Stream stream, NbtCompound compound)
        {
            foreach(var pair in compound)
            {
                var writer = GetValueWriter(pair.Value);

                //write the type of the value
                stream.WriteByte((byte)writer.TagType);

                //then the key
                WriteString(stream, pair.Key);

                //then the value
                writer.WriteValue(stream, pair.Value);
            }

            stream.WriteByte((byte)TagType.END);
        }

        private static void WriteString(Stream stream, string value)
        {
            byte[] array = Encoding.UTF8.GetBytes(value);

            WriteShort(stream, (short)array.Length);
            stream.Write(array, 0, array.Length);
        }

        private static void WriteShort(Stream stream, short value)
        {
            byte[] array = BitConverter.GetBytes(value);
            if (BitConverter.IsLittleEndian)
            {
                Array.Reverse(array);
            }

            stream.Write(array, 0, 2);
        }
    }
}

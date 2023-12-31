﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace MinecraftEditor.Nbt
{
    public static class NbtReader
    {
        /// <summary>
        /// reads the contents of the NBT file
        /// </summary>
        public static NbtCompound ReadRoot(Stream stream)
        {
            //compounds are dictionaries made up of keys and values
            NbtCompound values = new NbtCompound();

            //first we have the id of the value
            int id = stream.ReadByte();

            if (id == (int)TagType.END)
            {
                return values;
            }

            //then the root name
            string name = ReadString(stream);

            //then the value
            values[name] = ReadValue(id, stream);

            return values;
        }

        /// <summary>
        /// reads an nbt object from the stream
        /// the stream position should be straight after the id byte
        /// </summary>
        private static object ReadValue(int id, Stream stream)
        {
            switch((TagType)id)
            {
                case TagType.BYTE:
                    return ReadByte(stream);
                case TagType.SHORT:
                    return ReadShort(stream);
                case TagType.INT:
                    return ReadInt(stream);
                case TagType.LONG:
                    return ReadLong(stream);
                case TagType.FLOAT:
                    return ReadFloat(stream);
                case TagType.DOUBLE:
                    return ReadDouble(stream);
                case TagType.STRING:
                    return ReadString(stream);
                case TagType.LIST:
                    return ReadList(stream);
                case TagType.COMPOUND:
                    return ReadCompound(stream);
                case TagType.INT_ARRAY:
                    return ReadIntArray(stream);
            }

            throw new Exception("Unknown nbt tag id: " + id);
        }

        private static int[] ReadIntArray(Stream stream)
        {
            //and then we get the length of the list
            int length = ReadInt(stream);

            int[] array = new int[length];
            for (int i = 0; i < length; i++)
            {
                array[i] = ReadInt(stream);
            }

            return array;
        }

        private static NbtList ReadList(Stream stream)
        {
            //all list elements are the same type
            int subId = stream.ReadByte();

            //and then we get the length of the list
            int length = ReadInt(stream);

            NbtList list = new NbtList();

            if(subId == (int)TagType.END)
            {
                //in theory the length should be 0
                //but the substrate library checks explicitly for this
                //so I will as well
                return list;
            }

            for (; length > 0; length--)
            {
                list.Add(ReadValue(subId, stream));
            }

            return list;
        }

        private static short ReadShort(Stream stream)
        {
            byte[] array = new byte[2];
            stream.Read(array, 0, 2);
            if (BitConverter.IsLittleEndian)
            {
                Array.Reverse(array);
            }

            return BitConverter.ToInt16(array, 0);
        }

        private static long ReadLong(Stream stream)
        {
            byte[] array = new byte[8];
            stream.Read(array, 0, 8);
            if (BitConverter.IsLittleEndian)
            {
                Array.Reverse(array);
            }

            return BitConverter.ToInt64(array, 0);
        }

        private static sbyte ReadByte(Stream stream)
        {
            return (sbyte)stream.ReadByte();
        }

        private static float ReadFloat(Stream stream)
        {
            byte[] array = new byte[4];
            stream.Read(array, 0, 4);
            if (BitConverter.IsLittleEndian)
            {
                Array.Reverse(array);
            }

            return BitConverter.ToSingle(array, 0);
        }

        private static double ReadDouble(Stream stream)
        {
            byte[] array = new byte[8];
            stream.Read(array, 0, 8);
            if (BitConverter.IsLittleEndian)
            {
                Array.Reverse(array);
            }

            return BitConverter.ToDouble(array, 0);
        }

        private static int ReadInt(Stream stream)
        {
            byte[] array = new byte[4];
            stream.Read(array, 0, 4);
            if (BitConverter.IsLittleEndian)
            {
                Array.Reverse(array);
            }

            return BitConverter.ToInt32(array, 0);
        }

        private static NbtCompound ReadCompound(Stream stream)
        {
            //compounds are dictionaries made up of keys and values
            Dictionary<string, object> values = new Dictionary<string, object>();

            while(true)
            {
                //first we have the id of the value
                int id = stream.ReadByte();

                if(id == (int)TagType.END)
                {
                    return values;
                }

                //then the key
                string name = ReadString(stream);

                //then the value
                values[name] = ReadValue(id, stream);
            }
        }

        private static string ReadString(Stream stream)
        {
            short length = ReadShort(stream);
            
            if (length < 0)
            {
                throw new Exception("Failed to read string, length was: " + length);
            }

            byte[] array = new byte[length];
            stream.Read(array, 0, length);
            return Encoding.UTF8.GetString(array);
        }
    }
}

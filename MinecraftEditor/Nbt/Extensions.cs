using System;
using System.Collections.Generic;
using System.Text;

namespace MinecraftEditor.Nbt
{
    public static class Extensions
    {
        public static T Get<T>(this NbtCompound dict, string key)
        {
            return (T)dict[key];
        }

        public static T GetOrDefault<T>(this NbtCompound dict, string key, T defaultValue)
        {
            if(dict.TryGetValue(key, out object obj))
            {
                return (T)obj;
            }
            else
            {
                return defaultValue;
            }
        }

        public static T Get<T>(this List<object> list, int index)
        {
            return (T)list[index];
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
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

        public static T Get<T>(this NbtList list, int index)
        {
            return (T)list[index];
        }

        public static bool DeepEquals(this NbtList list, NbtList other)
        {
            if(list.Count != other.Count)
            {
                return false;
            }

            if(list.Count == 0)
            {
                return true;
            }

            if(list.First().GetType() != other.First().GetType())
            {
                return false;
            }

            //all values in the list (should) have the same type
            //so no need to switch on the type for each element
            //we assign it for the first element
            //and re-use it

            Func<object, object, bool> comparer;
            switch(list.First())
            {
                case NbtCompound:
                    comparer = (a, b) =>
                    {
                        if(a is NbtCompound compound1 == false || b is NbtCompound compound2 == false)
                        {
                            throw new Exception("nbt list contains mixed types");
                        }

                        return compound1.DeepEquals(compound2);
                    };
                    break;
                case NbtList:
                    comparer = (a, b) =>
                    {
                        if (a is NbtList innerList1 == false || b is NbtList innerList2 == false)
                        {
                            throw new Exception("nbt list contains mixed types");
                        }

                        return innerList1.DeepEquals(innerList2);
                    };
                    break;
                case int[]:
                    comparer = (a, b) =>
                    {
                        if (a is int[] array1 == false || b is int[] array2 == false)
                        {
                            throw new Exception("nbt list contains mixed types");
                        }

                        return Enumerable.SequenceEqual(array1, array2);
                    };
                    break;
                default:
                    comparer = (a, b) =>
                    {
                        //hint: because both values are 'objects'
                        //doing a '==' or a '!=' comparison won't work
                        //as it would do a ReferenceEquals
                        //but we want to do a ValueEquals()
                        return a.Equals(b);
                    };
                    break;
            }

            foreach (var pair in list.Zip(other, (first, second) => (first, second)))
            {
                if(comparer(pair.first, pair.second) == false)
                {
                    return false;
                }
            }

            return true;
        }

        public static bool DeepEquals(this NbtCompound compound, NbtCompound other)
        {
            if(Enumerable.SequenceEqual(compound.Keys, other.Keys) == false)
            {
                return false;
            }

            foreach(var key in compound.Keys)
            {
                if(compound[key].GetType() != other[key].GetType())
                {
                    return false;
                }

                if (compound[key] is NbtCompound a)
                {
                    if (a.DeepEquals((NbtCompound)other[key]) == false)
                    {
                        return false;
                    }
                }
                else if(compound[key] is NbtList b)
                {
                    if (b.DeepEquals((NbtList)other[key]) == false)
                    {
                        return false;
                    }
                }
                else if(compound[key] is int[] c)
                {
                    if(Enumerable.SequenceEqual(c, (int[])other[key]) ==false)
                    {
                        return false;
                    }
                }
                else
                {
                    //hint: because both values are 'objects'
                    //doing a '==' or a '!=' comparison won't work
                    //as it would do a ReferenceEquals
                    //but we want to do a ValueEquals()
                    if (compound[key].Equals(other[key]) == false)
                    {
                        return false;
                    }
                }
            }

            return true;
        }
    }
}

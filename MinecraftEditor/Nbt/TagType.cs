using System;
using System.Collections.Generic;
using System.Text;

namespace MinecraftEditor.Nbt
{
    public enum TagType
    {
        END = 0,
        BYTE = 1,
        SHORT = 2,
        INT = 3,
        LONG = 4,
        FLOAT = 5,
        DOUBLE = 6,
        STRING = 8,
        LIST = 9,
        COMPOUND = 10,
        INT_ARRAY = 11
    }
}

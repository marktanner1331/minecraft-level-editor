using System;
using System.Collections.Generic;
using System.Text;

namespace MinecraftEditor.Players
{
    public class EnderItems
    {
        public NbtCompound Tree;

        private EnderItems(NbtCompound tree)
        {
            Tree = tree;
        }

        public static EnderItems Load(NbtCompound tree)
        {
            return new EnderItems(tree);
        }
    }
}

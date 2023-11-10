using System;
using System.Collections.Generic;
using System.Text;

namespace MinecraftEditor.Players
{
    public class Inventory
    {
        public NbtList Tree;

        private Inventory(NbtList tree)
        {
            Tree = tree;
        }

        public static Inventory Load(NbtList tree)
        {
            return new Inventory(tree);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace MinecraftEditor.PlayerModels
{
    public class Inventory
    {
        public NbtCompound Tree;

        private Inventory(NbtCompound tree)
        {
            Tree = tree;
        }

        public static Inventory Load(NbtCompound tree)
        {
            return new Inventory(tree);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace MinecraftEditor.PlayerModels
{
    public class PlayerAbilities
    {
        public NbtCompound Tree;

        private PlayerAbilities(NbtCompound tree)
        {
            Tree = tree;
        }

        public static PlayerAbilities Load(NbtCompound tree)
        {
            return new PlayerAbilities(tree);
        }
    }
}

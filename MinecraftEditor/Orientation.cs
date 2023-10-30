using MinecraftEditor.Nbt;
using System;
using System.Collections.Generic;
using System.Text;

namespace MinecraftEditor
{
    public class Orientation
    {
        public NbtList Tree;

        public double Yaw
        {
            get => Tree.Get<double>(0);
            set => Tree[0] = value;
        }

        public double Pitch
        {
            get => Tree.Get<double>(1);
            set => Tree[0] = value;
        }

        private Orientation(NbtList tree)
        {
            Tree = tree;
        }

        public static Orientation Load(NbtList tree)
        {
            return new Orientation(tree);
        }
    }
}

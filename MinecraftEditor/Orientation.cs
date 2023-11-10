using MinecraftEditor.Nbt;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace MinecraftEditor
{
    public class Orientation
    {
        public NbtList Tree;

        public float Yaw
        {
            get => Tree.Get<float>(0);
            set => Tree[0] = value;
        }

        public float Pitch
        {
            get => Tree.Get<float>(1);
            set => Tree[0] = value;
        }

        private Orientation(NbtList tree)
        {
            Tree = tree;
        }

        public override string ToString()
        {
            return $"Yaw: {Yaw}, Pitch: {Pitch}";
        }

        public static Orientation Load(NbtList tree)
        {
            return new Orientation(tree);
        }
    }
}

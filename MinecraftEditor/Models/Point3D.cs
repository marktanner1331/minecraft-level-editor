using MinecraftEditor.Nbt;
using System;
using System.Collections.Generic;
using System.Text;

namespace MinecraftEditor.Models
{
    public class Point3D
    {
        public NbtList Tree;

        public double X
        {
            get => Tree.Get<double>(0);
            set => Tree[0] = value;
        }

        public double Y
        {
            get => Tree.Get<double>(1);
            set => Tree[0] = value;
        }

        public double Z
        {
            get => Tree.Get<double>(2);
            set => Tree[0] = value;
        }

        private Point3D(NbtList tree)
        {
            Tree = tree;
        }

        public override string ToString()
        {
            return $"x: {X}, y: {Y}, z: {Z}";
        }

        public static Point3D Load(NbtList tree)
        {
            return new Point3D(tree);
        }
    }
}

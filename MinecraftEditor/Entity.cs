using MinecraftEditor.Nbt;
using System;
using System.Collections.Generic;
using System.Text;

namespace MinecraftEditor
{
    public class Entity
    {
        public NbtCompound Tree;

        private Point3D _position;
        public Point3D Position
        {
            get
            {
                return _position ?? (_position = Point3D.Load(Tree.Get<NbtList>("Pos")));
            }
            set
            {
                _position = value;
                Tree["Pos"] = value.Tree;
            }
        }

        private Point3D _motion;
        public Point3D Motion
        {
            get
            {
                return _motion ?? (_motion = Point3D.Load(Tree.Get<NbtList>("Motion")));
            }
            set
            {
                _motion = value;
                Tree["Motion"] = value.Tree;
            }
        }

        private Orientation _rotation;
        public Orientation Rotation
        {
            get
            {
                return _rotation ?? (_rotation = Orientation.Load(Tree.Get<NbtList>("Rotation")));
            }
            set
            {
                _rotation = value;
                Tree["Rotation"] = value.Tree;
            }
        }

        public short Fire
        {
            get => Tree.Get<short>("Fire");
            set => Tree["Fire"] = value;
        }

        public short Air
        {
            get => Tree.Get<short>("Air");
            set => Tree["Air"] = value;
        }

        public bool OnGround
        {
            get => Tree.GetOrDefault<sbyte>("OnGround", 0) == 1;
            set => Tree["OnGround"] = (sbyte)(value ? 1 : 0);
        }

        protected Entity(NbtCompound tree)
        {
            Tree = tree;
        }
    }
}

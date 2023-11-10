using MinecraftEditor.Nbt;
using System;
using System.Collections.Generic;
using System.Text;

namespace MinecraftEditor.Players
{
    public class PlayerAbilities
    {
        public NbtCompound Tree;

        public bool Flying
        {
            get => Tree.Get<sbyte>("Flying") == 1;
            set => Tree["Flying"] = (sbyte)(value ? 1 : 0);
        }

        public bool InstantBuild
        {
            get => Tree.Get<sbyte>("instabuild") == 1;
            set => Tree["instabuild"] = (sbyte)(value ? 1 : 0);
        }

        public bool MayFly
        {
            get => Tree.Get<sbyte>("mayfly") == 1;
            set => Tree["mayfly"] = (sbyte)(value ? 1 : 0);
        }

        public bool Invulnerable
        {
            get => Tree.Get<sbyte>("invulnerable") == 1;
            set => Tree["invulnerable"] = (sbyte)(value ? 1 : 0);
        }

        public bool MayBuild
        {
            get => Tree.GetOrDefault<sbyte>("mayBuild", 1) == 1;
            set => Tree["mayBuild"] = (sbyte)(value ? 1 : 0);
        }

        public float WalkSpeed
        {
            get => Tree.GetOrDefault("walkSpeed", 0.1f);
            set => Tree["walkSpeed"] = value;
        }

        public float FlySpeed
        {
            get => Tree.GetOrDefault("flySpeed", 0.05f);
            set => Tree["flySpeed"] = value;
        }

        private PlayerAbilities(NbtCompound tree)
        {
            Tree = tree;
        }

        public static PlayerAbilities Load(NbtCompound tree)
        {
            return new PlayerAbilities(tree);
        }

        public static PlayerAbilities Create()
        {
            var abilities = new PlayerAbilities(new NbtCompound());
            abilities.Flying = false;
            abilities.InstantBuild = false;
            abilities.MayFly = false;
            abilities.Invulnerable = false;
            return abilities;
        }
    }
}

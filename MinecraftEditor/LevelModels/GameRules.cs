using MinecraftEditor.Nbt;
using System;
using System.Collections.Generic;
using System.Text;

namespace MinecraftEditor.LevelModels
{
    public class GameRules
    {
        public NbtCompound Tree;

        public bool CommandBlockOutput
        {
            get => Tree.GetOrDefault("commandBlockOutput", "true") == "true";
            set => Tree["commandBlockOutput"] = (value ? "true" : "false");
        }

        public bool DoFireTick
        {
            get => Tree.GetOrDefault("doFireTick", "true") == "true";
            set => Tree["doFireTick"] = (value ? "true" : "false");
        }

        public bool DoMobLoot
        {
            get => Tree.GetOrDefault("doMobLoot", "true") == "true";
            set => Tree["doMobLoot"] = (value ? "true" : "false");
        }

        public bool DoMobSpawning
        {
            get => Tree.GetOrDefault("doMobSpawning", "true") == "true";
            set => Tree["doMobSpawning"] = (value ? "true" : "false");
        }

        public bool DoTileDrops
        {
            get => Tree.GetOrDefault("doTileDrops", "true") == "true";
            set => Tree["doTileDrops"] = (value ? "true" : "false");
        }

        public bool KeepInventory
        {
            get => Tree.GetOrDefault("keepInventory", "false") == "true";
            set => Tree["keepInventory"] = (value ? "true" : "false");
        }

        public bool MobGriefing
        {
            get => Tree.GetOrDefault("mobGriefing", "true") == "true";
            set => Tree["mobGriefing"] = (value ? "true" : "false");
        }

        private GameRules(NbtCompound tree)
        {
            Tree = tree;
        }

        public static GameRules Load(NbtCompound tree)
        {
            return new GameRules(tree);
        }
    }
}

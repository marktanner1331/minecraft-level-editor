using MinecraftEditor.Enums;
using MinecraftEditor.Nbt;
using System;
using System.Collections.Generic;
using System.Text;

namespace MinecraftEditor.Models.PlayerModels
{
    public class Player : Mob
    {
        /// <summary>
        /// not sure what this does, if anyone knows please raise a PR
        /// </summary>
        public short AttackTime
        {
            get => Tree.GetOrDefault<short>("AttackTime", 0);
            set => Tree["AttackTime"] = value;
        }

        public short DeathTime
        {
            get => Tree.Get<short>("DeathTime");
            set => Tree["DeathTime"] = value;
        }

        public float Health
        {
            get => Tree.Get<float>("Health");
            set => Tree["Health"] = value;
        }

        public short HurtTime
        {
            get => Tree.Get<short>("HurtTime");
            set => Tree["HurtTime"] = value;
        }

        public short SleepTimer
        {
            get => Tree.Get<short>("SleepTimer");
            set => Tree["SleepTimer"] = value;
        }

        public Dimension Dimension
        {
            get => (Dimension)Enum.Parse(typeof(Dimension), Tree.Get<string>("Dimension").Split(':')[1].ToUpper());
            set => Tree["Dimension"] = "minecraft:" + value.ToString().ToLower();
        }

        public bool Sleeping
        {
            get => Tree.GetOrDefault<sbyte>("Sleeping", 0) == 1;
            set => Tree["Sleeping"] = (sbyte)(value ? 1 : 0);
        }

        public int SpawnX
        {
            get => Tree.GetOrDefault("SpawnX", 0);
            set => Tree["SpawnX"] = value;
        }

        public int SpawnY
        {
            get => Tree.GetOrDefault("SpawnY", 0);
            set => Tree["SpawnY"] = value;
        }

        public int SpawnZ
        {
            get => Tree.GetOrDefault("SpawnZ", 0);
            set => Tree["SpawnZ"] = value;
        }

        public string World
        {
            get => Tree.GetOrDefault<string>("World", null);
            set => Tree["World"] = value;
        }

        public int foodLevel
        {
            get => Tree.GetOrDefault("foodLevel", 20);
            set => Tree["foodLevel"] = value;
        }

        public int foodTickTimer
        {
            get => Tree.GetOrDefault("foodTickTimer", 0);
            set => Tree["foodTickTimer"] = value;
        }

        public float foodExhaustionLevel
        {
            get => Tree.GetOrDefault<float>("foodExhaustionLevel", 0);
            set => Tree["foodExhaustionLevel"] = value;
        }

        public float foodSaturationLevel
        {
            get => Tree.GetOrDefault<float>("foodSaturationLevel", 5);
            set => Tree["foodSaturationLevel"] = value;
        }

        public float XpP
        {
            get => Tree.GetOrDefault<float>("XpP", 0);
            set => Tree["XpP"] = value;
        }

        public int XpLevel
        {
            get => Tree.GetOrDefault("XpLevel", 0);
            set => Tree["XpLevel"] = value;
        }

        public int XpTotal
        {
            get => Tree.GetOrDefault("XpTotal", 0);
            set => Tree["XpTotal"] = value;
        }

        public int Score
        {
            get => Tree.GetOrDefault("Score", 0);
            set => Tree["Score"] = value;
        }

        private PlayerAbilities _playerAbilities;
        public PlayerAbilities PlayerAbilities
        {
            get
            {
                return _playerAbilities
                    ?? (Tree.ContainsKey("PlayerAbilities")
                    ? _playerAbilities = PlayerAbilities.Load(Tree.Get<NbtCompound>("PlayerAbilities"))
                    : null);
            }
            set
            {
                _playerAbilities = value;
                if (value == null)
                {
                    Tree.Remove("PlayerAbilities");
                }
                else
                {
                    Tree["PlayerAbilities"] = value.Tree;
                }
            }
        }

        public GameType PlayerGameType
        {
            get => (GameType)Tree.GetOrDefault("PlayerGameType", 0);
            set => Tree["PlayerGameType"] = (int)value;
        }

        private Inventory _inventory;
        public Inventory Inventory
        {
            get
            {
                return _inventory ?? (_inventory = Inventory.Load(Tree.Get<NbtList>("Inventory")));
            }
            set
            {
                _inventory = value;
                Tree["Inventory"] = value.Tree;
            }
        }

        private EnderItems _enderItems;
        public EnderItems EnderItems
        {
            get
            {
                return _enderItems
                    ?? (Tree.ContainsKey("EnderItems")
                    ? _enderItems = EnderItems.Load(Tree.Get<NbtCompound>("EnderItems"))
                    : null);
            }
            set
            {
                _enderItems = value;
                if (value == null)
                {
                    Tree.Remove("EnderItems");
                }
                else
                {
                    Tree["EnderItems"] = value.Tree;
                }
            }
        }

        public Uuid Uuid
        {
            get
            {

                return new Uuid(Tree.Get<int[]>("UUID"));
            }
            set
            {
                Tree["UUID"] = value;
            }
        }

        private Player(NbtCompound tree) : base(tree)
        {

        }

        public static Player Load(NbtCompound tree)
        {
            return new Player(tree);
        }
    }
}

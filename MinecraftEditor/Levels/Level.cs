using MinecraftEditor.Nbt;
using MinecraftEditor.Players;

namespace MinecraftEditor.Levels
{
    public class Level
    {
        public NbtCompound Tree;

        private Level(NbtCompound tree)
        {
            Tree = tree
                .Get<NbtCompound>("Data");
        }

        public long Time
        {
            get => Tree.Get<long>("Time");
            set => Tree["Time"] = value;
        }

        public long LastPlayed
        {
            get => Tree.Get<long>("LastPlayed");
            set => Tree["LastPlayed"] = value;
        }

        private Player _player;
        public Player Player
        {
            get
            {
                return _player
                    ?? (Tree.ContainsKey("Player")
                    ? _player = Player.Load(Tree.Get<NbtCompound>("Player"))
                    : null);
            }
            set
            {
                _player = value;
                if (value == null)
                {
                    Tree.Remove("Player");
                }
                else
                {
                    Tree["Player"] = value.Tree;
                }
            }
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

        public long SizeOnDisk
        {
            get => Tree.Get<long>("SizeOnDisk");
            set => Tree["SizeOnDisk"] = value;
        }

        public long RandomSeed
        {
            get => Tree.Get<long>("RandomSeed");
            set => Tree["RandomSeed"] = value;
        }

        public int Version
        {
            get => Tree.GetOrDefault("version", 0);
            set => Tree["version"] = value;
        }

        public string LevelName
        {
            get => Tree.GetOrDefault("LevelName", string.Empty);
            set => Tree["LevelName"] = value;
        }

        public string GeneratorName
        {
            get => Tree.GetOrDefault("generatorName", string.Empty);
            set => Tree["generatorName"] = value;
        }

        public bool Raining
        {
            get => Tree.GetOrDefault<sbyte>("raining", 0) == 1;
            set => Tree["raining"] = (sbyte)(value ? 1 : 0);
        }

        public bool Thundering
        {
            get => Tree.GetOrDefault<sbyte>("thundering", 0) == 1;
            set => Tree["thundering"] = (sbyte)(value ? 1 : 0);
        }

        public int RainTime
        {
            get => Tree.GetOrDefault("rainTime", 0);
            set => Tree["rainTime"] = value;
        }

        public int ThunderTime
        {
            get => Tree.GetOrDefault("thunderTime", 0);
            set => Tree["thunderTime"] = value;
        }

        public GameType PlayerGameType
        {
            get => (GameType)Tree.GetOrDefault("PlayerGameType", 0);
            set => Tree["PlayerGameType"] = (int)value;
        }

        public bool MapFeatures
        {
            get => Tree.GetOrDefault<sbyte>("MapFeatures", 0) == 1;
            set => Tree["MapFeatures"] = (sbyte)(value ? 1 : 0);
        }

        public bool Hardcore
        {
            get => Tree.GetOrDefault<sbyte>("hardcore", 0) == 1;
            set => Tree["hardcore"] = (sbyte)(value ? 1 : 0);
        }

        public int GeneratorVersion
        {
            get => Tree.GetOrDefault("generatorVersion", 0);
            set => Tree["generatorVersion"] = value;
        }

        public string GeneratorOptions
        {
            get => Tree.GetOrDefault("generatorOptions", string.Empty);
            set => Tree["generatorOptions"] = value;
        }

        public bool AllowCommands
        {
            get => Tree.GetOrDefault<sbyte>("allowCommands", 0) == 1;
            set => Tree["allowCommands"] = (sbyte)(value ? 1 : 0);
        }

        public bool Initialized
        {
            get => Tree.GetOrDefault<sbyte>("initialized", 0) == 1;
            set => Tree["initialized"] = (sbyte)(value ? 1 : 0);
        }

        public long DayTime
        {
            get => Tree.Get<long>("DayTime");
            set => Tree["DayTime"] = value;
        }

        private GameRules _gameRules;
        public GameRules GameRules
        {
            get
            {
                return _gameRules
                    ?? (Tree.ContainsKey("GameRules")
                    ? _gameRules = GameRules.Load(Tree.Get<NbtCompound>("GameRules"))
                    : null);
            }
            set
            {
                _gameRules = value;
                if (value == null)
                {
                    Tree.Remove("GameRules");
                }
                else
                {
                    Tree["GameRules"] = value.Tree;
                }
            }
        }

        public static Level Load(NbtCompound tree)
        {
            return new Level(tree);
        }
    }
}

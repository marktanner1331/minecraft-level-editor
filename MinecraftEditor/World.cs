using MinecraftEditor.Models.LevelModels;
using MinecraftEditor.Models.PlayerModels;
using MinecraftEditor.Nbt;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace MinecraftEditor
{
    public class World
    {
        public DirectoryInfo SaveFolder;

        private Level Level;
        private Dictionary<Uuid, Player> ServerPlayers;

        private World(DirectoryInfo saveFolder)
        {
            this.SaveFolder = saveFolder;
            ServerPlayers = new Dictionary<Uuid, Player>();
        }

        public Level GetLevel()
        {
            if(this.Level != null)
            {
                return this.Level;
            }
            
            using (var file = File.Open(Path.Combine(SaveFolder.FullName, "level.dat"), FileMode.Open))
            using (GZipStream deflate = new GZipStream(file, CompressionMode.Decompress))
            {
                var root = NbtReader.ReadRoot(deflate);
                this.Level = Level.Load(root.Values.First() as NbtCompound);
            }

            return this.Level;
        }

        private void SaveLevel()
        {
            string fullLevelPath = Path.Combine(SaveFolder.FullName, "level.dat");

            if (File.Exists(fullLevelPath))
            {
                File.Delete(fullLevelPath);
            }
            
            using (var file = File.Create(fullLevelPath))
            using (GZipStream deflate = new GZipStream(file, CompressionMode.Compress))
            {
                NbtWriter.WriteRoot(deflate, new NbtCompound
                {
                    {
                        "",
                        new NbtCompound
                        {
                            { "Data", this.Level.Tree }
                        }
                    }
                });

                deflate.Flush();
                file.Flush();
            }
        }

        /// <summary>
        /// returns a player from the /playerdata/ folder
        /// </summary>
        public Player GetServerPlayer(Uuid uuid)
        {
            if(ServerPlayers.ContainsKey(uuid))
            {
                return ServerPlayers[uuid];
            }

            using (var file = File.Open(Path.Combine(SaveFolder.FullName, $"playerdata/{uuid}.dat"), FileMode.Open))
            using (GZipStream deflate = new GZipStream(file, CompressionMode.Decompress))
            {
                var root = NbtReader.ReadRoot(deflate);
                var player = Player.Load(root.Values.First() as NbtCompound);

                ServerPlayers[uuid] = player;
                return player;
            }
        }

        private void SaveServerPlayers()
        {
            foreach(var serverPlayer in ServerPlayers)
            {
                string fullLevelPath = Path.Combine(SaveFolder.FullName, $"playerdata/{serverPlayer.Key}.dat");

                if (File.Exists(fullLevelPath))
                {
                    File.Delete(fullLevelPath);
                }

                using (var file = File.Create(fullLevelPath))
                using (GZipStream deflate = new GZipStream(file, CompressionMode.Compress))
                {
                    NbtWriter.WriteRoot(deflate, new NbtCompound
                    {
                        {
                            "",
                            serverPlayer.Value.Tree
                        }
                    });

                    deflate.Flush();
                    file.Flush();
                }
            }
        }

        public void Save()
        {
            if (Level != null)
            {
                SaveLevel();
            }

            SaveServerPlayers();
        }

        public static World Load(DirectoryInfo saveFolder)
        {
            return new World(saveFolder);
        }

        public static World Load(string saveFolder)
        {
            return new World(new DirectoryInfo(saveFolder));
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MinecraftEditor.Players;
using MinecraftEditor.Shared;

namespace MinecraftEditor.Samples
{
    internal class PlayerPositionAndRotation
    {
        public static void GetPlayerPositionAndRotation(string saveFolder, out Point3D position, out Orientation rotation)
        {
            var world = World.Load(saveFolder);
            var level = world.GetLevel();

            position = level.Player.Position;
            rotation = level.Player.Rotation;
        }

        public static void SetPlayerPositionAndRotation(string saveFolder, Point3D position, Orientation rotation, bool isFlying)
        {
            var world = World.Load(saveFolder);
            var level = world.GetLevel();

            level.Player.Position = position;
            level.Player.Rotation = rotation;
            level.Player.OnGround = !isFlying;

            level.Player.PlayerAbilities ??= PlayerAbilities.Create();
            level.Player.PlayerAbilities.Flying = isFlying;

            world.Save();
        }
    }
}

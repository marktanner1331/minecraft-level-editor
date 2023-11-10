using MinecraftEditor.Enums;
using MinecraftEditor.Nbt;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MinecraftEditor.Tests.Models.Player
{
    public class PlayerTests
    {
        [Test]
        public void PlayerTest1()
        {
            using (var file = File.Open("./Data/flat level - uncompressed.dat", FileMode.Open))
            {
                var root = NbtReader.ReadRoot(file);
                var level = MinecraftEditor.Models.LevelModels.Level.Load(root.Values.First() as Dictionary<string, object>);
                var player = level.Player;

                Assert.That(player, Is.Not.Null);

                Assert.That(player.AttackTime, Is.EqualTo(0));
                Assert.That(player.DeathTime, Is.EqualTo(0));
                Assert.That(player.Health, Is.EqualTo(20));
                Assert.That(player.HurtTime, Is.EqualTo(0));
                Assert.That(player.SleepTimer, Is.EqualTo(0));
                Assert.That(player.Dimension, Is.EqualTo(Dimension.OVERWORLD));
                Assert.That(player.Sleeping, Is.EqualTo(false));

                var k = player.Inventory;
            }
        }
    }
}
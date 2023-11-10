using MinecraftEditor.Nbt;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MinecraftEditor.Tests
{
    internal class LevelTests
    {
        [Test]
        public void LevelTest1()
        {
            using (var file = File.Open("./Data/flat level - uncompressed.dat", FileMode.Open))
            {
                var root = NbtReader.ReadRoot(file);
                var level = Level.Load(root.Values.First() as Dictionary<string, object>);

                Assert.That(level.Time, Is.EqualTo(417));
                level.Time = 1;
                Assert.That(level.Time, Is.EqualTo(1));

                Assert.That(level.LastPlayed, Is.EqualTo(1695978555058));
                level.LastPlayed = 2;
                Assert.That(level.LastPlayed, Is.EqualTo(2));

                Assert.That(level.Player, Is.Not.Null);
                level.Player = null;
                Assert.That(level.Player, Is.Null);
            }
        }
    }
}

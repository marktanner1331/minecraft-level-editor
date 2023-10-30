using MinecraftEditor.Nbt;

namespace MinecraftEditor.Tests.Nbt
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void Read1()
        {
            using (var file = File.Open("./Data/flat level - uncompressed.dat", FileMode.Open))
            {
                NbtReader.Read(file);
            }
        }
    }
}
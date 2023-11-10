using MinecraftEditor.Nbt;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MinecraftEditor.Tests.Nbt
{
    public class NbtWriterTests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void Write1()
        {
            using (var file = File.Open("./Data/flat level - uncompressed.dat", FileMode.Open))
            using (MemoryStream inStream = new MemoryStream())
            using (MemoryStream outStream = new MemoryStream())
            {
                var root = NbtReader.ReadRoot(file);
                NbtWriter.WriteRoot(outStream, root);

                file.Seek(0, SeekOrigin.Begin);
                file.CopyTo(inStream);

                outStream.Seek(0, SeekOrigin.Begin);

                var root2 = NbtReader.ReadRoot(outStream);

                Assert.IsTrue(root.DeepEquals(root2));
            }
        }
    }
}
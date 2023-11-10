using MinecraftEditor.PlayerModels;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MinecraftEditor.Tests
{
    public class UuidTests
    {
        [Test]
        public void UuidTest()
        {
            var uuid = new Uuid(new int[] { 191483518, 1486242926, -1828681268, 1019032467 });
            Assert.That(uuid.ToString(), Is.EqualTo("0b69ce7e-5896-446e-9300-89cc3cbd3393"));
        }

        [Test]
        public void UuidTest2()
        {
            var uuid = new Uuid("0b69ce7e-5896-446e-9300-89cc3cbd3393");
            Assert.That(uuid.ToString(), Is.EqualTo("0b69ce7e-5896-446e-9300-89cc3cbd3393"));
        }
    }
}

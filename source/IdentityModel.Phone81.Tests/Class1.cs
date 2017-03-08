using IdentityModel;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdentityModel.Phone81.Tests
{
    [TestFixture]
    public class CryptoRandomTests
    {
        [Test]
        public void CreateUniqueIdDefaultLengthIs32()
        {
            var x = CryptoRandom.CreateUniqueId();
            Assert.That(x.Length, Is.EqualTo(32));
        }

        [Test]
        public void CanSpecifyLengthForCreateUniqueId()
        {
            var x = CryptoRandom.CreateUniqueId(1);
            Assert.That(x.Length, Is.EqualTo(2));
        }
    }
}

using IdentityModel.Jwt;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdentityModel.Phone81.Tests.Jwt
{
    [TestFixture]
    public class JwkNetExtensionsTests
    {
        [Test]
        public void CreateKeyDefaultsTo2048Bits()
        {
            var key = JwkNetExtensions.CreateKey();
            Assert.That(key.KeySize, Is.EqualTo(2048));
        }

        [Test]
        public void CreateKeyAcceptsAKeySize()
        {
            var keySize = 1024;
            var key = JwkNetExtensions.CreateKey(keySize);
            Assert.That(key.KeySize, Is.EqualTo(keySize));
        }

        [Test]
        public void CanConvertKeyToJwkWithPublicParameters()
        {
            var key = JwkNetExtensions.CreateKey();
            var jwt = key.ToJsonWebKey();
            Assert.That(jwt.HasPrivateKey, Is.False);
            Assert.That(jwt.Kid.Length, Is.EqualTo(32));
            var e = Base64Url.Decode(jwt.E);
            Assert.That(e.Length, Is.EqualTo(3));
            var n = Base64Url.Decode(jwt.N);
            Assert.That(n.Length, Is.EqualTo(key.KeySize / 8)); // KeySize is in bits, so / 8 for bytes
        }
    }
}

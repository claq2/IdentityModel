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
        public void CanConvertKeyToJwkWithPublicParametersAndDefaultValues()
        {
            var key = JwkNetExtensions.CreateKey();
            var jwt = key.ToJsonWebKey();
            Assert.That(jwt.HasPrivateKey, Is.False);
            Assert.That(jwt.Kid.Length, Is.EqualTo(32));
            var e = Base64Url.Decode(jwt.E);
            Assert.That(e.Length, Is.EqualTo(3)); // Always 65537, which needs 3 bytes (2^16 + 1)
            Assert.That(e[0], Is.EqualTo(1)); // 1 x 2^0 = 1
            Assert.That(e[1], Is.EqualTo(0)); // 0 x 2^8 = 0
            Assert.That(e[2], Is.EqualTo(1)); // 1 x 2^16 = 65536
            var n = Base64Url.Decode(jwt.N);
            Assert.That(n.Length, Is.EqualTo(key.KeySize / 8)); // KeySize is in bits, so / 8 for bytes
            Assert.That(jwt.Kty, Is.EqualTo("RSA"));
            Assert.That(jwt.Alg, Is.EqualTo("RS256"));
        }

        [Test]
        public void CanSpecifyKeyId()
        {
            var keyId = "myKeyId";
            var key = JwkNetExtensions.CreateKey();
            var jwt = key.ToJsonWebKey(kid: keyId);
            Assert.That(jwt.Kid, Is.EqualTo(keyId));
        }

        [Test]
        public void CanSpecifyAlgorithm()
        {
            var alg = "RS512";
            var key = JwkNetExtensions.CreateKey();
            var jwt = key.ToJsonWebKey(alg);
            Assert.That(jwt.Alg, Is.EqualTo(alg));
        }
    }
}

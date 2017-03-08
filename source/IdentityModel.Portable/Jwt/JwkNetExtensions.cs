using PCLCrypto;

namespace IdentityModel.Jwt
{
    public static class JwkNetExtensions
    {
        public static JsonWebKey ToJsonWebKey(this ICryptographicKey key,
            string alg = "RS256", string kid = null)
        {
            var parameters = key.ExportParameters(false);

            var n = Base64Url.Encode(parameters.Modulus);
            var e = Base64Url.Encode(parameters.Exponent);
            return new JsonWebKey()
            {
                N = n,
                E = e,
                Kid = kid ?? CryptoRandom.CreateUniqueId(),
                Kty = "RSA",
                Alg = alg,
            };
        }

        public static ICryptographicKey CreateKey(int keySize = 2048)
        {
            IAsymmetricKeyAlgorithmProvider provider = WinRTCrypto.AsymmetricKeyAlgorithmProvider.OpenAlgorithm(AsymmetricAlgorithm.RsaPkcs1);
            return provider.CreateKeyPair(keySize);
        }
    }
}

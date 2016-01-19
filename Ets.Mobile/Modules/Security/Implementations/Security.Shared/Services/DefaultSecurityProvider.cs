using Security.Contracts;
using Windows.Security.Cryptography;
using Windows.Security.Cryptography.Core;

namespace Security.Services
{
    public sealed class DefaultSecurityProvider : ISecurityProvider
    {
        public string HashMd5(string str)
        {
            var alg = HashAlgorithmProvider.OpenAlgorithm(HashAlgorithmNames.Md5);
            var buff = CryptographicBuffer.ConvertStringToBinary(str, BinaryStringEncoding.Utf8);
            var hashed = alg.HashData(buff);
            var res = CryptographicBuffer.EncodeToHexString(hashed);
            return res;
        }
    }
}
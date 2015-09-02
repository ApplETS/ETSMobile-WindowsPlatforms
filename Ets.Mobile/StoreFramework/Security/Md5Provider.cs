using Windows.Security.Cryptography;
using Windows.Security.Cryptography.Core;

namespace StoreFramework.Security
{
    public static class Md5Provider
    {
        public static string GetHashString(string str)
        {
            var alg = HashAlgorithmProvider.OpenAlgorithm(HashAlgorithmNames.Md5);
            var buff = CryptographicBuffer.ConvertStringToBinary(str, BinaryStringEncoding.Utf8);
            var hashed = alg.HashData(buff);
            var res = CryptographicBuffer.EncodeToHexString(hashed);
            return res;
        }
    }
}

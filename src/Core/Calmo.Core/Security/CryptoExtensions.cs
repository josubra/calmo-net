using System.Text;

namespace System.Security.Cryptography
{
	/// <summary>
	/// Extensions for encryption
	/// </summary>
    public static class CryptoExtensions
    {
		/// <summary>
		/// Apply default MD5 to a string
		/// </summary>
		/// <param name="value">Value that will be hashed</param>
		/// <returns>Hash</returns>
        public static string ToMD5Hash(this string value)
        {
            if (value == null) return null;

            var md5 = MD5.Create();
            var inputBytes = Encoding.ASCII.GetBytes(value);
            var hash = md5.ComputeHash(inputBytes);
            var sb = new StringBuilder();
            foreach (var b in hash)
                sb.Append(b.ToString("X2"));

            return sb.ToString();
        }

		/// <summary>
		/// Two-way encryption using a private key
		/// </summary>
		/// <param name="value">Value that will be encrypted</param>
		/// <param name="privateKey">Encryption private key</param>
		/// <returns>Cypher</returns>
        public static string Encrypt(this string value, string privateKey)
        {
            var toEncryptArray = Encoding.UTF8.GetBytes(value);
            var hashmd5 = new MD5CryptoServiceProvider();
            var keyArray = hashmd5.ComputeHash(Encoding.UTF8.GetBytes(privateKey));

            hashmd5.Clear();

            var tdes = new TripleDESCryptoServiceProvider
            {
                Key = keyArray,
                Mode = CipherMode.ECB,
                Padding = PaddingMode.PKCS7
            };

            var transform = tdes.CreateEncryptor();
            var resultArray = transform.TransformFinalBlock(toEncryptArray, 0, toEncryptArray.Length);

            tdes.Clear();

            return Convert.ToBase64String(resultArray, 0, resultArray.Length);
        }

		/// <summary>
		/// Decrypt a value using a private key
		/// </summary>
		/// <param name="value">Cypher value that will be decrypted</param>
		/// <param name="privateKey">Private encryption key</param>
		/// <returns>Decrypted value</returns>
        public static string Decrypt(this string value, string privateKey)
        {
            var toEncryptArray = Convert.FromBase64String(value);
            var hashmd5 = new MD5CryptoServiceProvider();
            var keyArray = hashmd5.ComputeHash(Encoding.UTF8.GetBytes(privateKey));

            hashmd5.Clear();

            var tdes = new TripleDESCryptoServiceProvider
            {
                Key = keyArray,
                Mode = CipherMode.ECB,
                Padding = PaddingMode.PKCS7
            };

            var transform = tdes.CreateDecryptor();
            var resultArray = transform.TransformFinalBlock(toEncryptArray, 0, toEncryptArray.Length);

            tdes.Clear();

            return Encoding.UTF8.GetString(resultArray);
        }
    }
}

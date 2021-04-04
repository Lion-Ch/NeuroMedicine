using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Security
{
    internal class Encryption
    {
		private byte[] IV = new byte[16];
		private byte[] Key = new byte[32];

		public RijndaelManaged GetRijndaelManaged()
		{
			return new RijndaelManaged
			{
				Mode = CipherMode.CBC,
				Padding = PaddingMode.PKCS7,
				KeySize = 256,
				BlockSize = 128,
				Key = this.Key,
				IV = this.IV
			};
		}
		public Encryption()
		{
			Encoding encoding = Encoding.UTF8;
			string Key1 = "Neuromedicine-04.04.2021",
				IV1 = Key1;

			var KeyFull = encoding.GetBytes(Key1);
			Array.Copy(KeyFull, this.Key, Math.Min(KeyFull.Length, this.Key.Length));

			var IVFull = encoding.GetBytes(IV1);
			Array.Copy(IVFull, this.IV, Math.Min(IVFull.Length, this.IV.Length));
		}


		public byte[] EncryptBytes(byte[] bytes)
		{
			using (var r = GetRijndaelManaged())
			{
				using (var encryptor = r.CreateEncryptor())
				{
					return encryptor.TransformFinalBlock(bytes, 0, bytes.Length);
				}
			}
		}

		public byte[] DecryptBytes(byte[] bytes)
		{
			using (var r = GetRijndaelManaged())
			{
				using (var decryptor = r.CreateDecryptor())
				{
					return decryptor.TransformFinalBlock(bytes, 0, bytes.Length);
				}
			}
		}

		public String Encrypt(String plainText)
		{
			if (plainText == null)
				return null;
			var plainBytes = Encoding.UTF8.GetBytes(plainText);
			return Convert.ToBase64String(EncryptBytes(plainBytes));
		}


		public String Decrypt(String encryptedText)
		{
			if (encryptedText == null)
				return null;
			var encryptedBytes = Convert.FromBase64String(encryptedText);
			return Encoding.UTF8.GetString(DecryptBytes(encryptedBytes));
		}

	}
}

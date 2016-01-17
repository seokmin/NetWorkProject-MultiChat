using System;
using System.Security.Cryptography;
using System.Text;

public static class Encrypter
{
	private static byte[] key = new byte[8] { 0x11, 0x22, 0x33, 0x44, 0x55, 0x66, 0x77, 0x88 };
	private static byte[] iv = new byte[8] { 0x11, 0x22, 0x33, 0x44, 0x55, 0x66, 0x77, 0x88 };

	public static string Crypt(this string text)
	{
		try
		{
			//SymmetricAlgorithm algorithm = DES.Create();
			DES des = new DESCryptoServiceProvider();
			des.Mode = CipherMode.ECB;
			des.Padding = PaddingMode.PKCS7;
			des.Key = key;
			des.IV = iv;
			//ICryptoTransform transform = algorithm.CreateEncryptor(key, iv);
			ICryptoTransform transform = des.CreateEncryptor(key,iv);
			byte[] inputbuffer = Encoding.Unicode.GetBytes(text);
			byte[] outputBuffer;
			try
			{
				outputBuffer = transform.TransformFinalBlock(inputbuffer, 0, inputbuffer.Length);
			}
			catch(Exception e)
			{
				inputbuffer = Encoding.UTF8.GetBytes(text);
				outputBuffer = transform.TransformFinalBlock(inputbuffer, 0, inputbuffer.Length);
			}
			string asdfs = Convert.ToBase64String(outputBuffer);
			return asdfs;
		}
		catch (Exception e)
		{
			return "";
		}

	}

	public static string Decrypt(this string text)
	{
		try
		{
			DES des = new DESCryptoServiceProvider();
			des.Mode = CipherMode.ECB;
			des.Padding = PaddingMode.PKCS7;
			des.Key = key;
			des.IV = iv;
			//SymmetricAlgorithm algorithm = DES.Create();
			//ICryptoTransform transform = algorithm.CreateDecryptor(key, iv);
			ICryptoTransform transform = des.CreateDecryptor();
			byte[] inputbuffer = Convert.FromBase64String(text);
			
			byte[] outputBuffer = transform.TransformFinalBlock(inputbuffer, 0, inputbuffer.Length);
			return Encoding.Unicode.GetString(outputBuffer);
		}
		catch (Exception e)
		{
			byte[] utfBytes = Encoding.UTF8.GetBytes(text);

			byte[] unicodeBytes = Encoding.Convert(Encoding.UTF8, Encoding.Unicode, utfBytes);
			char[] unicodeChars = new char[Encoding.Unicode.GetCharCount(unicodeBytes, 0, unicodeBytes.Length)];
			Encoding.Unicode.GetChars(unicodeBytes, 0, unicodeBytes.Length, unicodeChars, 0);
			text = new string(unicodeChars);
			DES des = new DESCryptoServiceProvider();
			des.Mode = CipherMode.ECB;
			des.Padding = PaddingMode.None;
			des.Key = key;
			des.IV = iv;
			ICryptoTransform transform = des.CreateDecryptor();
			byte[] inputbuffer = Convert.FromBase64String(text);

			byte[] outputBuffer = transform.TransformFinalBlock(inputbuffer, 0, inputbuffer.Length);
			return Encoding.Unicode.GetString(outputBuffer);
		}
	}
}
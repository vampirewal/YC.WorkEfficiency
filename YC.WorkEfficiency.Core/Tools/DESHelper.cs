#region<<文 件 说 明>>
/*----------------------------------------------------------------
// 文件名称：DESHelper
// 创 建 者：杨程
// 创建时间：2021/3/4 星期四 16:05:48
// 文件版本：V1.0.0
// ===============================================================
// 功能描述：
//		
//
//----------------------------------------------------------------*/
#endregion

using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace YC.WorkEfficiency.Core
{
	public class DESHelper
	{
		private const string DES_KEY = "Project";

		#region 3DES 加解密
		/// <summary>
		/// 3DES加密
		/// </summary>
		/// <param name="encryStr">需要加密的文本</param>
		/// <param name="key">密钥</param>
		/// <returns></returns>
		public static string Encrypt3Des(string encryStr, string key = DES_KEY)
		{
			try
			{
				var inputArry = Encoding.Default.GetBytes(encryStr);
				var hashmd5 = new MD5CryptoServiceProvider();
				var byKey = hashmd5.ComputeHash(Encoding.Default.GetBytes(key));
				var byIv = byKey;
				var ms = new MemoryStream();
				using (var tDescryptProvider = new TripleDESCryptoServiceProvider())
				{
					tDescryptProvider.Mode = CipherMode.ECB;
					using (var cs = new CryptoStream(ms, tDescryptProvider.CreateEncryptor(byKey, byIv), CryptoStreamMode.Write))
					{
						cs.Write(inputArry, 0, inputArry.Length);
						cs.FlushFinalBlock();
						cs.Close();
					}
				}

				var str = Convert.ToBase64String(ms.ToArray());
				ms.Close();
				return str;
			}
			catch (Exception)
			{
				return encryStr;
			}

		}

		/// <summary>
		/// 解密
		/// </summary>
		/// <param name="decryStr">待解密的密码</param>
		/// <param name="key">密钥</param>
		/// <returns></returns>
		public static string Decrypt3Des(string decryStr, string key = DES_KEY)
		{
			try
			{
				var inputArry = Convert.FromBase64String(decryStr);
				var hashmd5 = new MD5CryptoServiceProvider();
				var byKey = hashmd5.ComputeHash(Encoding.Default.GetBytes(key));
				var byIv = byKey;
				var ms = new MemoryStream();
				using (var tDescryptProvider = new TripleDESCryptoServiceProvider())
				{
					tDescryptProvider.Mode = CipherMode.ECB;
					using (var cs = new CryptoStream(ms, tDescryptProvider.CreateDecryptor(byKey, byIv), CryptoStreamMode.Write))
					{
						cs.Write(inputArry, 0, inputArry.Length);
						cs.FlushFinalBlock();
						cs.Close();
					}
				}

				var str = Encoding.Default.GetString(ms.ToArray());
				ms.Close();
				return str;
			}
			catch (Exception)
			{
				return decryStr;
			}
		}


		#endregion
	}
}
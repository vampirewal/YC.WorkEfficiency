#region << 文 件 说 明 >>
/*----------------------------------------------------------------
// 文件名称：JsonHelper
// 创 建 者：杨程
// 创建时间：2021/3/30 18:25:05
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
using System.Text;
using Newtonsoft.Json;

namespace YC.WorkEfficiency.View.Common
{
    public class JsonHelper
    {
		/// <summary>
		/// json字符串格式化输出
		/// </summary>
		/// <param name="sourceJsonStr"></param>
		/// <returns></returns>
		public static string FormatJsonString(string sourceJsonStr)
		{
			//格式化json字符串
			JsonSerializer serializer = new JsonSerializer();
			TextReader tr = new StringReader(sourceJsonStr);
			JsonTextReader jtr = new JsonTextReader(tr);
			object obj = serializer.Deserialize(jtr);
			if (obj != null)
			{
				StringWriter textWriter = new StringWriter();
				JsonTextWriter jsonWriter = new JsonTextWriter(textWriter)
				{
					Formatting = Formatting.Indented,
					Indentation = 4,
					IndentChar = ' '
				};
				serializer.Serialize(jsonWriter, obj);
				return textWriter.ToString();
			}
			else
			{
				return sourceJsonStr;
			}
		}
	}
}

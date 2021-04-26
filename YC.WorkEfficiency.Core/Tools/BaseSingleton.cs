#region << 文 件 说 明 >>
/*----------------------------------------------------------------
// 文件名称：BaseSingleton
// 创 建 者：杨程
// 创建时间：2021/4/26 11:43:25
// 文件版本：V1.0.0
// ===============================================================
// 功能描述：
//		
//
//----------------------------------------------------------------*/
#endregion

using System;
using System.Collections.Generic;
using System.Text;

namespace YC.WorkEfficiency.Core
{
    /// <summary>
    /// 所有使用单例模式的基类，需要被继承
    /// </summary>
    /// <typeparam name="T">填写类的名称</typeparam>
    public class BaseSingleton<T> where T : new()
    {
        private static T _instance;
        public static T GetInstance()
        {

            if (_instance == null)
            {
                _instance = new T();
            }
            return _instance;
        }
    }
}

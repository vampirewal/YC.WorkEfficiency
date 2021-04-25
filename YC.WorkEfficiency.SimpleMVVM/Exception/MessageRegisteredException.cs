/* 项目名称： MessageRegisteredException.cs
 * 命名空间： YC.WorkEfficiency.SimpleMVVM
 * 类 名 称: MessageRegisteredException
 * 作   者 : 杨程
 * 概   述 : 
 * 创建时间 : 2021/2/20 18:24:11
 * 更新时间 : 2021/2/20 18:24:11
 * CLR版本 : 4.0.30319.42000
 * ******************************************************
 * Copyright@Administrator 2021 .All rights reserved.
 * ******************************************************
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YC.WorkEfficiency.SimpleMVVM
{
    /// <summary>
    /// 
    /// </summary>
    [Serializable]
    public class MessageRegisteredException : Exception
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="mes"></param>
        public MessageRegisteredException(string mes) : base(mes)
        {

        }
    }
}

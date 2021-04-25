/* 项目名称： TokenInstance.cs
 * 命名空间： YC.WorkEfficiency.SimpleMVVM
 * 类 名 称: TokenInstance
 * 作   者 : 杨程
 * 概   述 : 
 * 创建时间 : 2021/2/20 18:33:28
 * 更新时间 : 2021/2/20 18:33:28
 * CLR版本 : 4.0.30319.42000
 * ******************************************************
 * Copyright@Administrator 2021 .All rights reserved.
 * ******************************************************
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace YC.WorkEfficiency.SimpleMVVM
{
    /// <summary>
    /// 
    /// </summary>
    internal class TokenInstance
    {
        public object Register { get; set; }
        public MethodInfo MethodInfo { get; set; }
    }
}

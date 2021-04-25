/* 项目名称： RegistMethodAttribute.cs
 * 命名空间： YC.WorkEfficiency.SimpleMVVM
 * 类 名 称: RegistMethodAttribute
 * 作   者 : 杨程
 * 概   述 : 
 * 创建时间 : 2021/2/20 18:20:49
 * 更新时间 : 2021/2/20 18:20:49
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
    /// 注册为消息
    /// </summary>
    public class RegistMethodAttribute : Attribute
    {
        public RegistMethodAttribute()
        {
            //构造函数
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="token"></param>
        public RegistMethodAttribute(string token)
        {
            this.Token = token;
        }
        /// <summary>
        /// 标识
        /// </summary>
        public string Token { get; set; }
    }
}

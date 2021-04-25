/* 项目名称： WindowSetting.cs
 * 命名空间： YC.WorkEfficiency.SimpleMVVM
 * 类 名 称: WindowSetting
 * 作   者 : 杨程
 * 概   述 : 
 * 创建时间 : 2021/2/20 18:46:56
 * 更新时间 : 2021/2/20 18:46:56
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
using System.Windows;

namespace YC.WorkEfficiency.SimpleMVVM
{
    /// <summary>
    /// 显示窗口设置
    /// </summary>
    public class WindowSetting
    {
        /// <summary>
        /// Window类型
        /// </summary>
        public Type WindowType { get; set; }

        /// <summary>
        /// 状态
        /// </summary>
        public WindowState WindowState { get; set; }

        /// <summary>
        /// 显示类型
        /// </summary>
        public ShowMode ShowMode { get; set; }

        /// <summary>
        /// 构造函数参数
        /// </summary>
        public object[] Parameters { get; set; }


    }
}

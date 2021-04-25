/* 项目名称： ViewModelBase.cs
 * 命名空间： YC.WorkEfficiency.SimpleMVVM
 * 类 名 称: ViewModelBase
 * 作   者 : 杨程
 * 概   述 : 
 * 创建时间 : 2021/2/20 18:35:09
 * 更新时间 : 2021/2/20 18:35:09
 * CLR版本 : 4.0.30319.42000
 * ******************************************************
 * Copyright@Administrator 2021 .All rights reserved.
 * ******************************************************
 */

using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;

namespace YC.WorkEfficiency.SimpleMVVM
{
    /// <summary>
    /// ViewModel基类
    /// </summary>
    public class ViewModelBase : NotifyBase
    {
        /// <summary>
        /// 判断是不是设计器模式
        /// </summary>
        public bool IsInDesignMode
        {
            get { return DesignerProperties.GetIsInDesignMode(new DependencyObject()); }
        }

        /// <summary>
        /// 目标View
        /// </summary>
        public FrameworkElement View { get; set; }

        /// <summary>
        /// UI线程调用
        /// </summary>
        /// <param name="action"></param>
        public void UIInvoke(Action action)
        {
            this.View.Dispatcher.Invoke(action);
        }

        public string Title { get; set; } = "未命名窗体";
    }
}

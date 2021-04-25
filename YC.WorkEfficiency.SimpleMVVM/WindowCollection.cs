/* 项目名称： WindowCollection.cs
 * 命名空间： YC.WorkEfficiency.SimpleMVVM
 * 类 名 称: WindowCollection
 * 作   者 : 杨程
 * 概   述 : 
 * 创建时间 : 2021/2/20 18:46:12
 * 更新时间 : 2021/2/20 18:46:12
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
    /// 窗口集合
    /// </summary>
    public class WindowCollection : List<Window>
    {
        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="window"></param>
        public new void Add(Window window)
        {
            window.Closed += Window_Closed;
            window.Activate();
            base.Add(window);
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            base.Remove((Window)sender);
        }

        public Window this[string WindowName]
        {
            get
            {
                if (!string.IsNullOrEmpty(WindowName))
                {
                    return this.FirstOrDefault(f => f.Name == WindowName);
                }
                else
                {
                    return null;
                }
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace YC.WorkEfficiency.Core
{
    public interface IView
    {
        //约束插件类型

        /// <summary>
        /// 需要继承该接口的UserControl返回当前页面，直接 retuen this 即可。
        /// </summary>
        object Window { get; }

        
    }
}

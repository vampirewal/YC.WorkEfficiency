/* 项目名称： YcList.cs
 * 命名空间： YC.WorkEfficiency.SimpleMVVM
 * 类 名 称: YcList
 * 作   者 : 杨程
 * 概   述 : 
 * 创建时间 : 2021/2/20 18:45:00
 * 更新时间 : 2021/2/20 18:45:00
 * CLR版本 : 4.0.30319.42000
 * ******************************************************
 * Copyright@Administrator 2021 .All rights reserved.
 * ******************************************************
 */

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YC.WorkEfficiency.SimpleMVVM
{
    /// <summary>
    /// 继承ObservableCollection的集合
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class YcList<T> : ObservableCollection<T>
    {
        public YcList()
        {
            //构造函数
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="list"></param>
        public YcList(List<T> list)
        {
            foreach (var item in list)
            {
                Add(item);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="collection"></param>
        public YcList(IEnumerable<T> collection)
        {
            foreach (var item in collection)
            {
                Add(item);
            }
        }
    }
}

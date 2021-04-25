/* 项目名称： NotifyBase.cs
 * 命名空间： Common
 * 类 名 称: NotifyBase
 * 作   者 : 杨程
 * 概   述 : 
 * 创建时间 : 2021/2/10 21:46:31
 * 更新时间 : 2021/2/10 21:46:31
 * CLR版本 : 4.0.30319.42000
 * ******************************************************
 * Copyright@Administrator 2021 .All rights reserved.
 * ******************************************************
 */

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace YC.WorkEfficiency.SimpleMVVM
{
    public class NotifyBase : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public void DoNotify([CallerMemberName] string propName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
        }
    }
}

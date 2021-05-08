#region << 文 件 说 明 >>
/*----------------------------------------------------------------
// 文件名称：GlobalData
// 创 建 者：杨程
// 创建时间：2021/4/26 11:44:44
// 文件版本：V1.0.0
// ===============================================================
// 功能描述：
//		
//
//----------------------------------------------------------------*/
#endregion

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using YC.WorkEfficiency.Core;
using YC.WorkEfficiency.Models;

namespace YC.WorkEfficiency.ViewModels.Common
{
    public class GlobalData:BaseSingleton<GlobalData>
    {

        #region 属性
        public UserModel UserInfo { get; set; } = new UserModel();

        public ObservableCollection<FileType> UserFileTypes { get; set; } = new ObservableCollection<FileType>();
        #endregion

        #region 公共方法

        #endregion

        #region 私有方法

        #endregion

        #region 命令

        #endregion
    }
}

#region << 文 件 说 明 >>
/*----------------------------------------------------------------
// 文件名称：SettingViewModel
// 创 建 者：杨程
// 创建时间：2021/4/28 9:55:12
// 文件版本：V1.0.0
// ===============================================================
// 功能描述：
//		
//
//----------------------------------------------------------------*/
#endregion

using System;
using System.Collections.Generic;
using System.Text;

namespace YC.WorkEfficiency.ViewModels
{
    public class SettingViewModel
    {
        public SettingViewModel()
        {
            //构造函数
            baseCommand = new BaseCommand();
        }

        #region 属性
        public BaseCommand baseCommand { get; set; }
        #endregion

        #region 公共方法

        #endregion

        #region 私有方法

        #endregion

        #region 命令

        #endregion
    }
}

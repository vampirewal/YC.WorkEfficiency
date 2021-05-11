#region << 文 件 说 明 >>
/*----------------------------------------------------------------
// 文件名称：SelectColorViewModel
// 创 建 者：杨程
// 创建时间：2021/5/11 12:55:43
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
using System.Windows.Media;
using YC.WorkEfficiency.SimpleMVVM;

namespace YC.WorkEfficiency.ViewModels
{
    public class SelectColorViewModel:ViewModelBase
    {
        public SelectColorViewModel()
        {
            //构造函数
        }

        public override object GetResult()
        {
            return selectColor.ToString();
        }

        #region 属性
        public Color selectColor { get; set; }
        #endregion

        #region 公共方法

        #endregion

        #region 私有方法

        #endregion

        #region 命令

        #endregion
    }
}

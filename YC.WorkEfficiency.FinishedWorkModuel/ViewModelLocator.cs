#region << 文 件 说 明 >>
/*----------------------------------------------------------------
// 文件名称：ViewModelLocator
// 创 建 者：杨程
// 创建时间：2021/4/9 10:34:26
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
using YC.WorkEfficiency.SimpleMVVM;
using YC.WorkEfficiency.ViewModels;

namespace YC.WorkEfficiency.FinishedWorkModuel
{
    public class ViewModelLocator
    {
        public ViewModelLocator()
        {
            //构造函数
            SimpleIoc.Default.Register(new FinishedWorkViewModel());
        }

        /// <summary>
        /// 返回MainViewModel
        /// </summary>
        public FinishedWorkViewModel FinishedWorkViewModel
        {
            get
            {
                return SimpleIoc.Default.GetViewModelInstance<FinishedWorkViewModel>();
            }
        }
        #region 属性

        #endregion

        #region 公共方法

        #endregion

        #region 私有方法

        #endregion

        #region 命令

        #endregion
    }
}

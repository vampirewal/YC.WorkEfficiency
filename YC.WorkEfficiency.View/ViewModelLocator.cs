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

namespace YC.WorkEfficiency.View
{
    public class ViewModelLocator
    {
        public ViewModelLocator()
        {
            //构造函数
            SimpleIoc.Default.Register(new LoginViewModel());
            SimpleIoc.Default.Register(new MainViewModel());

        }

        /// <summary>
        /// 返回MainViewModel
        /// </summary>
        public LoginViewModel LoginViewModel
        {
            get
            {
                return SimpleIoc.Default.GetViewModelInstance<LoginViewModel>();
            }
        }

        /// <summary>
        /// 返回MainViewModel
        /// </summary>
        public MainViewModel MainViewModel
        {
            get
            {
                return SimpleIoc.Default.GetViewModelInstance<MainViewModel>();
            }
        }
    }
}

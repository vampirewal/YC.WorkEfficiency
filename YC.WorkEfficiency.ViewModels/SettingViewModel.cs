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
using System.Windows;
using YC.WorkEfficiency.SimpleMVVM;

namespace YC.WorkEfficiency.ViewModels
{
    public class SettingViewModel: ViewModelBase
    {
        public SettingViewModel()
        {
            //构造函数
        }

        private bool IsSetting = false;
        public override object GetResult()
        {
            return IsSetting;
        }

        public override RelayCommand CloseWindowCommand => new RelayCommand(()=> 
        {
            Window w = View as Window;
            w.DialogResult = false;
            WindowsManager.CloseWindow(w);
        });

        #region 属性

        #endregion

        #region 公共方法

        #endregion

        #region 私有方法

        #endregion

        #region 命令
        public RelayCommand SaveSetting => new RelayCommand(() =>
          {
              (View as Window).DialogResult = true;
              WindowsManager.CloseWindow((View as Window));
          });
        #endregion
    }
}

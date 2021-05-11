#region << 文 件 说 明 >>
/*----------------------------------------------------------------
// 文件名称：AddFileDesViewModel
// 创 建 者：杨程
// 创建时间：2021/5/11 11:45:52
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
using YC.WorkEfficiency.Models;
using YC.WorkEfficiency.SimpleMVVM;

namespace YC.WorkEfficiency.ViewModels
{
    public class AddFileDesViewModel:ViewModelBase
    {
        public AddFileDesViewModel(FileModel _fileModel)
        {
            //构造函数
            fileModel = _fileModel;
        }
        #region 重写
        public override object GetResult()
        {
            return base.GetResult();
        }

        public override void InitData()
        {
            workDescription = new WorkDescription();
        }
        #endregion

        #region 属性
        private FileModel fileModel;

        public WorkDescription workDescription { get; set; }
        #endregion

        #region 公共方法

        #endregion

        #region 私有方法

        #endregion

        #region 命令
        public RelayCommand OpenSelectBackgroundColorPickerCommand => new RelayCommand(() =>
          {
              string SelectColor = WindowsManager.CreateDialogWindowByViewModelResult("SelectColorView", new SelectColorViewModel()).ToString();
              if(!string.IsNullOrEmpty(SelectColor))
              {
                  workDescription.DesBackground = SelectColor;
              }
          });
        #endregion
    }
}

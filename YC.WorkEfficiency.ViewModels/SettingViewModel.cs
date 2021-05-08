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
using YC.WorkEfficiency.DataAccess;
using YC.WorkEfficiency.Models;
using YC.WorkEfficiency.SimpleMVVM;
using YC.WorkEfficiency.ViewModels.Common;
using System.Linq;
using YC.WorkEfficiency.Themes;

namespace YC.WorkEfficiency.ViewModels
{
    public class SettingViewModel: ViewModelBase
    {
        public SettingViewModel()
        {
            //构造函数
            CurrentUser = GlobalData.GetInstance().UserInfo;
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
        //当前登陆用户
        public UserModel CurrentUser { get; set; }

        public string fileTypeStr { get; set; }
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

        public RelayCommand AddFileType => new RelayCommand(() =>
          {
              if (!string.IsNullOrEmpty(fileTypeStr))
              {
                  using(WorkEfficiencyDataContext work=new WorkEfficiencyDataContext())
                  {
                      if(work.FileTypeDB.Where(w => w.Types == fileTypeStr).Any())
                      {
                          DialogWindow.Show("已存在相同名称的文件类型，请重新输入！", MessageType.Error, WindowsManager.Windows["SettingWindow"]);
                          return;
                      }

                      FileType fileType = new FileType()
                      {
                          GuidId = Guid.NewGuid().ToString(),
                          UserId = CurrentUser.GuidId,
                          Types = fileTypeStr
                      };
                      

                      work.FileTypeDB.Add(fileType);
                      work.SaveChanges();

                      Messenger.Default.Send("AddFileType", fileType);
                      DialogWindow.Show("创建新的文件类型成功！", MessageType.Successful, WindowsManager.Windows["SettingWindow"]);
                      fileTypeStr = string.Empty;
                  }
              }
          });
        #endregion
    }
}

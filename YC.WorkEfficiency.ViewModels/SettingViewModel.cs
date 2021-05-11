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
using System.Collections.ObjectModel;

namespace YC.WorkEfficiency.ViewModels
{
    public class SettingViewModel: ViewModelBase
    {
        public SettingViewModel()
        {
            //构造函数
        }

        #region 重写
        private bool IsSetting = false;
        public override object GetResult()
        {
            return IsSetting;
        }

        public override RelayCommand CloseWindowCommand => new RelayCommand(() =>
        {
            Window w = View as Window;
            w.DialogResult = false;
            WindowsManager.CloseWindow(w);
        });

        public override void InitData()
        {
            CurrentUser = GlobalData.GetInstance().UserInfo;
            
            GetFileTypeData();
            GetRecycleBinForFileModelData();
        }
        #endregion

        #region 属性
        //当前登陆用户
        public UserModel CurrentUser { get; set; }

        #region 文件类型
        /// <summary>
        /// 新增的文件类型名称
        /// </summary>
        public string fileTypeStr { get; set; }

        /// <summary>
        /// 文件类型集合
        /// </summary>
        public ObservableCollection<FileType> filetypeList { get; set; } 

        public ObservableCollection<FileModel> RecycleBinForFileModel { get; set; }
        #endregion

        #endregion

        #region 公共方法

        #endregion

        #region 私有方法

        /// <summary>
        /// 获取数据库内最新的文件类型
        /// </summary>
        private void GetFileTypeData()
        {
            filetypeList = new ObservableCollection<FileType>();
            using (WorkEfficiencyDataContext work=new WorkEfficiencyDataContext())
            {
                var current = work.FileTypeDB.Where(w => w.UserId == GlobalData.GetInstance().UserInfo.GuidId).ToList();

                foreach (var item in current)
                {
                    item.HaveSetting = work.FileModelDB.Where(w => w.FileTypeGuid == item.GuidId).Count();
                    filetypeList.Add(item);
                }
            }
        }
        /// <summary>
        /// 获取回收站的文件数据
        /// </summary>
        private void GetRecycleBinForFileModelData()
        {
            RecycleBinForFileModel = new ObservableCollection<FileModel>();
            using(WorkEfficiencyDataContext work=new WorkEfficiencyDataContext())
            {
                var current = work.FileModelDB.Where(w => w.UserGuid == GlobalData.GetInstance().UserInfo.GuidId && w.IsDeleted == true).ToList();
                foreach (var item in current)
                {
                    RecycleBinForFileModel.Add(item);
                }
            }
        }
        #endregion

        #region 命令
        /// <summary>
        /// 保存设置命令
        /// </summary>
        public RelayCommand SaveSetting => new RelayCommand(() =>
          {
              (View as Window).DialogResult = true;
              WindowsManager.CloseWindow((View as Window));
          });

        #region 针对 文件类型 的命令
        /// <summary>
        /// 添加1条新的文件类型命令
        /// </summary>
        public RelayCommand AddFileType => new RelayCommand(() =>
          {
              if (!string.IsNullOrEmpty(fileTypeStr))
              {
                  using (WorkEfficiencyDataContext work = new WorkEfficiencyDataContext())
                  {
                      if (work.FileTypeDB.Where(w => w.Types == fileTypeStr).Any())
                      {
                          DialogWindow.Show("已存在相同名称的文件类型，请重新输入！", MessageType.Error, WindowsManager.Windows["SettingWindow"]);
                          return;
                      }
                      else
                      {
                          FileType fileType = new FileType()
                          {
                              GuidId = Guid.NewGuid().ToString(),
                              UserId = CurrentUser.GuidId,
                              Types = fileTypeStr
                          };


                          work.FileTypeDB.Add(fileType);
                          work.SaveChanges();

                          Messenger.Default.Send("GetFileType");
                          filetypeList.Add(fileType);
                          DialogWindow.Show("创建新的文件类型成功！", MessageType.Successful, WindowsManager.Windows["SettingWindow"]);
                          fileTypeStr = "";
                      }
                  }
              }
          });

        /// <summary>
        /// 删除文件类型命令
        /// </summary>
        public RelayCommand<FileType> DeleteFileTypeCommand => new RelayCommand<FileType>((f) =>
          {
              if (f != null)
              {
                  using (WorkEfficiencyDataContext work = new WorkEfficiencyDataContext())
                  {
                      //var filetypeCount = work.FileModelDB.Where(w => w.FileType == f.Types).ToList().Count;
                      if (f.HaveSetting > 0)
                      {
                          if (DialogWindow.ShowDialog($"是否要删除 {f.Types} ? \r\n 该文件类型下，已设置了 {f.HaveSetting} 条文件。\r\n 如确认删除，将清空这些文件的 文件类型！", "警告"))
                          {
                              work.FileTypeDB.Remove(f);
                              var current = work.FileModelDB.Where(w => w.FileType == f.Types).ToList();
                              foreach (var item in current)
                              {
                                  item.FileTypeGuid = "";
                                  item.FileType = "";
                              }
                              filetypeList.Remove(f);
                          }
                      }
                      else if (DialogWindow.ShowDialog($"是否要删除 {f.Types} ?", "是否删除"))
                      {
                          work.FileTypeDB.Remove(f);
                          filetypeList.Remove(f);
                      }
                      DialogWindow.Show("删除成功！", MessageType.Successful, WindowsManager.Windows["SettingWindow"]);
                      work.SaveChanges();
                      Messenger.Default.Send("GetFileType");
                      Messenger.Default.Send("GetWorkingList");
                      Messenger.Default.Send("GetFinishedWork");
                  }

              }
          });
        #endregion

        #region 针对回收站的命令
        /// <summary>
        /// 将回收站内的文件恢复命令
        /// </summary>
        public RelayCommand<FileModel> RestoreFile => new RelayCommand<FileModel>((f) =>
          {
              if (f != null)
              {
                  using (WorkEfficiencyDataContext work = new WorkEfficiencyDataContext())
                  {
                      f.IsDeleted = false;
                      work.FileModelDB.Update(f);
                      work.SaveChanges();
                      DialogWindow.Show("恢复成功！", MessageType.Successful, WindowsManager.Windows["SettingWindow"]);
                      Messenger.Default.Send("GetFinishedWork");
                      Messenger.Default.Send("GetTotalWorking");
                      RecycleBinForFileModel.Remove(f);
                  }
              }
          });
        /// <summary>
        /// 将回收站内的文件彻底删除命令
        /// </summary>
        public RelayCommand<FileModel> TrueDeleteCommand => new RelayCommand<FileModel>((f) =>
          {
              if (f != null)
              {
                  if (DialogWindow.ShowDialog("确认是否彻底删除该文件？这样删除将包括该文件名下的附件等资料！", "请确认"))
                  {
                      using (WorkEfficiencyDataContext work = new WorkEfficiencyDataContext())
                      {
                          var fileattachment = work.FileAttachmentModelDB.Where(w => w.ParentGuidId == f.GuidId).ToList();
                          if (fileattachment.Count > 0)
                          {
                              DialogWindow.Show($"已删除该文件下的{fileattachment.Count}条附件！", MessageType.Information, WindowsManager.Windows["SettingWindow"]);
                              work.FileAttachmentModelDB.RemoveRange(fileattachment);
                          }
                          work.FileModelDB.Remove(f);
                          work.SaveChanges();
                          RecycleBinForFileModel.Remove(f);
                      }
                  }
              }
          }); 
        #endregion

        #endregion
    }
}

#region << 文 件 说 明 >>
/*----------------------------------------------------------------
// 文件名称：WorkInfoPanelViewModel
// 创 建 者：杨程
// 创建时间：2021/5/12 14:58:28
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
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Microsoft.Win32;
using YC.WorkEfficiency.DataAccess;
using YC.WorkEfficiency.Models;
using YC.WorkEfficiency.SimpleMVVM;
using YC.WorkEfficiency.Themes;
using YC.WorkEfficiency.ViewModels.Common;

namespace YC.WorkEfficiency.ViewModels
{
    public class WorkInfoPanelViewModel:ViewModelBase
    {
        public WorkInfoPanelViewModel()
        {
            //构造函数
            
        }
        #region 重写
        public override void InitData()
        {
            fileAttachmentModels = new ObservableCollection<FileAttachmentModel>();
            workDescriptions = new ObservableCollection<WorkDescription>();
            fileTypes = new ObservableCollection<FileType>();
            GetFileType();
            MessengerRegister();
        }
        #endregion

        #region 属性
        /// <summary>
        /// 文件类型 集合
        /// </summary>
        public ObservableCollection<FileType> fileTypes { get; set; }
        /// <summary>
        /// 工作描述集合
        /// </summary>
        public ObservableCollection<WorkDescription> workDescriptions { get; set; }

        /// <summary>
        /// 附件集合
        /// </summary>
        public ObservableCollection<FileAttachmentModel> fileAttachmentModels { get; set; }

        private FileModel selectedItem;
        /// <summary>
        /// 选择的文件
        /// </summary>
        public FileModel SelectedItem { get => selectedItem; set { selectedItem = value; DoNotify(); } }
        #endregion

        #region 公共方法

        #endregion

        #region 私有方法
        /// <summary>
        /// 获取 文件类型
        /// </summary>
        private void GetFileType()
        {
            fileTypes.Clear();
            using (WorkEfficiencyDataContext work = new WorkEfficiencyDataContext())
            {
                var current = work.FileTypeDB.Where(w => w.UserId == GlobalData.GetInstance().UserInfo.GuidId).ToList();
                foreach (var item in current)
                {
                    item.HaveSetting = work.FileModelDB.Where(w => w.FileType == item.Types).ToList().Count;
                    fileTypes.Add(item);
                }
            }
        }

        /// <summary>
        /// 获取该文件的描述信息
        /// </summary>
        /// <param name="fileGuid"></param>
        private void GetWoekDes(string fileGuid)
        {
            using (WorkEfficiencyDataContext work = new WorkEfficiencyDataContext())
            {
                workDescriptions.Clear();
                var currentWorkDes = work.WorkDescriptionDB.Where(w => w.FileModelGuidId == fileGuid).ToList();
                foreach (var item in currentWorkDes)
                {
                    var currentWorkType = work.workDescriptionTypesDB.Where(w => w.GuidId == item.WorkDescriptionTypeGuid).First();
                    item.WorkBackgroundColor = currentWorkType.TypeBackgroundColor;
                    item.WorkFontColor = currentWorkType.TypeFontColor;

                    workDescriptions.Add(item);
                }
            }

        }
        #endregion

        #region 命令
        /// <summary>
        /// 文件详细面板选择文件类型的命令
        /// </summary>
        public RelayCommand<string> SelectionChangeCommand => new RelayCommand<string>((s) =>
        {

            if (!string.IsNullOrEmpty(s))
            {
                using (WorkEfficiencyDataContext work = new WorkEfficiencyDataContext())
                {
                    SelectedItem.FileType = s;
                    SelectedItem.FileTypeGuid = work.FileTypeDB.Where(w => w.Types == s && w.UserId == SelectedItem.UserGuid).First().GuidId;

                    work.FileModelDB.Update(SelectedItem);
                    work.SaveChanges();
                }
            }
        });

        /// <summary>
        /// 上传附件命令
        /// </summary>
        public RelayCommand UpLoadAttachmentCommand => new RelayCommand(() =>
        {
            if (selectedItem == null)
            {
                //先判断一下当前选择的工作，是否为空

                return;
            }
            OpenFileDialog ofd = new OpenFileDialog()
            {
                Title = "请选择要上传的文件",
                Filter = "Excel文件(2003以上)|*.xlsx|Excel文件（97-2003）|*.xls|Word文件|*.docx|文本文件|*.txt|JPG图片|*.jpg|PNG图片|*.png",
                FileName = string.Empty,
                Multiselect = true
            };

            if (ofd.ShowDialog() == true)
            {
                Task.Run(() => {

                    List<string> filefullName = ofd.FileNames.ToList();
                    foreach (var filePath in filefullName)
                    {
                        string fileName = Path.GetFileNameWithoutExtension(filePath);
                        string fileType = Path.GetExtension(filePath);
                        try
                        {
                            //FileInfo fileInfo = new FileInfo(filePath);


                            using (FileStream fs = new FileStream(filePath, FileMode.Open))
                            {
                                BinaryReader brFile = new BinaryReader(fs);
                                Byte[] byData = brFile.ReadBytes((int)fs.Length);
                                FileAttachmentModel fam = new FileAttachmentModel()
                                {
                                    GuidId = Guid.NewGuid().ToString(),
                                    AttachmentName = fileName,
                                    ParentGuidId = selectedItem.GuidId,
                                    Attachment = byData,
                                    AttachmentType = fileType,
                                    AttachmentByte = byData.Length / 1024

                                };
                                Application.Current.Dispatcher.Invoke(() =>
                                {
                                    fileAttachmentModels.Add(fam);
                                });

                                using (WorkEfficiencyDataContext work = new WorkEfficiencyDataContext())
                                {
                                    work.FileAttachmentModelDB.Add(fam);
                                    work.SaveChanges();
                                }
                            }
                        }
                        catch
                        {
                            Application.Current.Dispatcher.Invoke(() =>
                            {
                                DialogWindow.Show($"{fileName} 上传文件失败，请查看上传文件是否已经被打开！", MessageType.Error, WindowsManager.Windows["MainView"]);
                            });
                        }
                    }
                });


            }
        });
        /// <summary>
        /// 下载附件命令
        /// </summary>
        public RelayCommand<FileAttachmentModel> DownloadAttachment => new RelayCommand<FileAttachmentModel>((f) =>
        {
            byte[] currentFile = f.Attachment;
            //Environment.GetFolderPath(Environment.SpecialFolder.Desktop); 获取桌面路径
            BinaryWriter bw = new BinaryWriter(File.Open($"{Environment.GetFolderPath(Environment.SpecialFolder.Desktop)}\\{f.AttachmentName}{f.AttachmentType}", FileMode.OpenOrCreate));
            bw.Write(currentFile);
            bw.Close();
            DialogWindow.Show("已成功将文件保存到桌面！", MessageType.Successful, WindowsManager.Windows["MainView"]);
        });
        /// <summary>
        /// 删除附件命令
        /// </summary>
        public RelayCommand<FileAttachmentModel> DeleteAttachmentCommand => new RelayCommand<FileAttachmentModel>((f) =>
        {
            fileAttachmentModels.Remove(f);
            using (WorkEfficiencyDataContext work = new WorkEfficiencyDataContext())
            {
                work.FileAttachmentModelDB.Remove(f);
                work.SaveChanges();
            }
            DialogWindow.Show("已成功将文件删除！", MessageType.Successful, WindowsManager.Windows["MainView"]);
        });
        /// <summary>
        /// 导出工作信息成word--待开发，预留功能
        /// </summary>
        public RelayCommand ExportFileModelToWordCommand => new RelayCommand(() =>
        {
            if (selectedItem != null)
            {
                if (DialogWindow.ShowDialog($"是否要导出 {selectedItem.FileTitle} 到word？", "提示"))
                {

                }
            }
        });

        /// <summary>
        /// 针对选择的文件，添加描述信息
        /// </summary>
        public RelayCommand AddFileDesCommand => new RelayCommand(() =>
        {
            if (selectedItem != null)
            {
                WindowsManager.CreateDialogWindowByViewModelResult("AddFileDes", new AddFileDesViewModel(SelectedItem));
                GetWoekDes(selectedItem.GuidId);
            }
        });

        /// <summary>
        /// 编辑工作描述
        /// </summary>
        public RelayCommand<WorkDescription> EditWorkDesCommand => new RelayCommand<WorkDescription>((w) =>
          {
              if (w!=null)
              {
                  WindowsManager.CreateDialogWindowByViewModelResult("EditFileDes", new EditFileDesViewModel(w));
                  GetWoekDes(selectedItem.GuidId);
              }
          });

        /// <summary>
        /// 删除工作描述
        /// </summary>
        public RelayCommand<WorkDescription> DeleteWorkDesCommand => new RelayCommand<WorkDescription>((w) =>
        {
            if (w!=null)
            {
                if (DialogWindow.ShowDialog("是否确认删除该描述？", "请确认"))
                {
                    using(WorkEfficiencyDataContext work=new WorkEfficiencyDataContext())
                    {
                        work.WorkDescriptionDB.Remove(w);
                        work.SaveChanges();
                        DialogWindow.Show("删除成功！",MessageType.Successful,WindowsManager.Windows["MainView"]);
                        GetWoekDes(selectedItem.GuidId);
                    }
                }
            }
        });

        /// <summary>
        /// 保存修改的工作信息
        /// </summary>
        public RelayCommand SaveWorkInfoCommand => new RelayCommand(() =>
          {
              if (SelectedItem != null)
              {
                  using (WorkEfficiencyDataContext work = new WorkEfficiencyDataContext())
                  {
                      work.FileModelDB.Update(SelectedItem);
                      work.SaveChanges();

                      DialogWindow.Show("保存成功！", MessageType.Successful, WindowsManager.Windows["MainView"]);
                  }
              }
          });
        #endregion

        #region 消息
        /// <summary>
        /// 汇总消息注册
        /// </summary>
        private void MessengerRegister()
        {
            Messenger.Default.Register<FileModel>(this, "ShowWorkInfo", ShowWorkInfo);

            Messenger.Default.Register(this, "GetFileType", GetFileType);
                        
        }
        /// <summary>
        /// 将选择的FileModel，获取附件之后，将完整信息显示到下方的panel中
        /// </summary>
        /// <param name="entity"></param>
        private void ShowWorkInfo(FileModel entity)
        {
            if (entity != null)
            {
                SelectedItem = entity;
                string guid = entity.GuidId;
                using (WorkEfficiencyDataContext work = new WorkEfficiencyDataContext())
                {

                    fileAttachmentModels.Clear();
                    var current = work.FileAttachmentModelDB.Where(w => w.ParentGuidId == guid).ToList();
                    if (current.Count > 0)
                    {
                        foreach (var item in current)
                        {
                            fileAttachmentModels.Add(item);
                        }
                    }
                }
                GetWoekDes(guid);
            }
        }

        #endregion
    }
}

#region << 文 件 说 明 >>

/*----------------------------------------------------------------
// 文件名称：MainViewModel
// 创 建 者：杨程
// 创建时间：2021/4/9 10:31:36
// 文件版本：V1.0.0
// ===============================================================
// 功能描述：
//
//
//----------------------------------------------------------------*/

#endregion << 文 件 说 明 >>

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using Microsoft.Win32;
using Newtonsoft.Json;
using YC.WorkEfficiency.DataAccess;
using YC.WorkEfficiency.Models;
using YC.WorkEfficiency.SimpleMVVM;
using YC.WorkEfficiency.Themes;
using YC.WorkEfficiency.ViewModels.Common;

namespace YC.WorkEfficiency.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        public MainViewModel():base()
        {
            //构造函数
            Title = "时间效率管理";
            //InitData();
        }

        #region 重写
        public override RelayCommand CloseWindowCommand => new RelayCommand(() =>
        {
            using (WorkEfficiencyDataContext work = new WorkEfficiencyDataContext())
            {
                var current = GlobalData.GetInstance().UserInfo;
                current.IsLogin = false;
                work.UserModelDB.Update(current);
                work.SaveChanges();
            }
            System.Environment.Exit(0);
            Application.Current.Shutdown();
        });

        public override void InitData()
        {
            fileAttachmentModels = new ObservableCollection<FileAttachmentModel>();

            fileTypes = new ObservableCollection<FileType>();

            LoadModuels();
            MessengerRegister();
            GetFileType();
            GetTotleTime();
            GetTotalWorking();
        }
        #endregion

        #region 属性
        public double LoadHeight { get; set; } = 0;

        private FileModel selectedItem;
        public FileModel SelectedItem { get => selectedItem; set { selectedItem = value; DoNotify(); } }


        #region 左侧信息栏
        private string _TotleWorkTime;
        /// <summary>
        /// 汇总工作时间
        /// </summary>
        public string TotleWorkTime
        {
            get { return _TotleWorkTime; }
            set { _TotleWorkTime = value; DoNotify(); }
        }

        private string _TotalWorking;
        /// <summary>
        /// 统计总的正在工作
        /// </summary>
        public string TotalWorking
        {
            get { return _TotalWorking; }
            set { _TotalWorking = value; DoNotify(); }
        }

        private string _ThisWeekFinishedWork;
        /// <summary>
        /// 本周已完成的工作
        /// </summary>
        public string ThisWeekFinishedWork
        {
            get { return _ThisWeekFinishedWork; }
            set { _ThisWeekFinishedWork = value; DoNotify(); }
        }

        private string _ThisMouthFinishedWork;
        /// <summary>
        /// 本月已完成的工作
        /// </summary>
        public string ThisMouthFinishedWork
        {
            get { return _ThisMouthFinishedWork; }
            set { _ThisMouthFinishedWork = value; DoNotify(); }
        }

        private string _ThisYearFinishedWork;
        /// <summary>
        /// 本年已完成的工作
        /// </summary>
        public string ThisYearFinishedWork
        {
            get { return _ThisYearFinishedWork; }
            set { _ThisYearFinishedWork = value; DoNotify(); }
        }

        #endregion

        /// <summary>
        /// 附件集合
        /// </summary>
        public ObservableCollection<FileAttachmentModel> fileAttachmentModels { get; set; }


        /// <summary>
        /// 文件类型 集合
        /// </summary>
        public ObservableCollection<FileType> fileTypes { get; set; }

        #region 窗体显示属性

        public double MaxHeight
        {
            get
            {
                return SystemParameters.MaximizedPrimaryScreenHeight;
            }
        }

        public double MaxWidth
        {
            get
            {
                return SystemParameters.MaximizedPrimaryScreenWidth;
            }
        }

        #endregion 窗体显示属性

        #region 模块
        /// <summary>
        /// 未完成工作 模块
        /// </summary>
        public FrameworkElement NoFinishedWorkFrame { get; set; }
        /// <summary>
        /// 已完成工作 模块
        /// </summary>
        public FrameworkElement FinishedWorkFrame { get; set; } 
        #endregion

        #endregion 属性

        #region 私有方法
        /// <summary>
        /// 获取模块并加载到MainViewModel
        /// </summary>
        private void LoadModuels()
        {
            LoadModulesServices.Instance.LoadModules();
            NoFinishedWorkFrame = LoadModulesServices.Instance.OpenModuleBindingVM("未完成的工作", new WorkingViewModel());
            FinishedWorkFrame = LoadModulesServices.Instance.OpenModuleBindingVM("已完成的工作", new FinishedWorkViewModel());
        }
        
        private void GetTotleTime()
        {
            Task.Run(() =>
            {
                int day = 0;
                int hours = 0;
                int minutes = 0;
                int seconds = 0;
                using (WorkEfficiencyDataContext fileModelDataContext = new WorkEfficiencyDataContext())
                {
                    var current = fileModelDataContext.FileModelDB.Where(s => s.IsFinished == true&&s.UserGuid==GlobalData.GetInstance().UserInfo.GuidId).ToList();

                    foreach (var item in current)
                    {
                        string itemday = item.AfterTime.Split('-')[0];
                        string newitemday = itemday.Replace('天', ' ');
                        day += Convert.ToInt32(newitemday.Trim());

                        string[] times = item.AfterTime.Split('-')[1].Split(':');
                        seconds += Convert.ToInt32(times[2]);
                        minutes += Convert.ToInt32(times[1]);
                        hours += Convert.ToInt32(times[0]);

                    }
                }

                int DatToSeconds = day * 24 * 60 * 60;
                int HoursToSeconds = hours * 60 * 60;
                int MinutesToSeconds = minutes * 60;
                seconds += DatToSeconds + HoursToSeconds + MinutesToSeconds;
                TimeSpan tsTotle = TimeSpan.FromSeconds(seconds);
                TotleWorkTime = $"累计工时：\r\n{tsTotle.Days}天{tsTotle.Hours}小时{tsTotle.Minutes}分{tsTotle.Seconds}秒";
            });
        }


        private void GetTotalWorking()
        {
            Task.Run(() =>
            {

                
                DateTime dt = DateTime.Now;  //当前时间  
                using (WorkEfficiencyDataContext work=new WorkEfficiencyDataContext())
                {
                    var currentCount = work.FileModelDB.Where(w => w.UserGuid == GlobalData.GetInstance().UserInfo.GuidId&&w.IsFinished==false).Count();
                    if (currentCount>0)
                    {
                        TotalWorking = $"现在还有{currentCount}条工作待完成";
                    }
                    else
                    {
                        TotalWorking = $"现在没有待完成工作";
                    }
                    //先获取登陆用户名下的已完成工作数
                    var currentFinishedWork = work.FileModelDB.Where(w => w.UserGuid == GlobalData.GetInstance().UserInfo.GuidId && w.IsFinished == true&&w.IsDeleted==false).ToList();
                    //本周已完成的工作
                    DateTime startWeek =Convert.ToDateTime( dt.AddDays(1 - Convert.ToInt32(dt.DayOfWeek.ToString("d"))).ToString("yyyy/MM/dd 00:00:00"));  //本周周一  
                    DateTime endWeek = startWeek.AddDays(6).AddHours(23).AddMinutes(59).AddSeconds(59);  //本周周日 
                    ThisWeekFinishedWork = $"本周完成工作 {currentFinishedWork.Where(w => w.EndTime >= startWeek&&w.EndTime<= endWeek).Count()} 条";
                    //本月
                    DateTime startMonth =Convert.ToDateTime( dt.AddDays(1 - dt.Day).ToString("yyyy/MM/dd 00:00:00"));  //本月月初  
                    DateTime endMonth = startMonth.AddMonths(1).AddDays(-1).AddHours(23).AddMinutes(59).AddSeconds(59);  //本月月末
                    ThisMouthFinishedWork = $"本月完成工作 {currentFinishedWork.Where(w => w.EndTime >= startMonth&&w.EndTime<= endMonth).Count()} 条";
                    //本年
                    DateTime startYear = new DateTime(dt.Year, 1, 1);  //本年年初 
                    DateTime endYear = new DateTime(dt.Year, 12, 31).AddHours(23).AddMinutes(59).AddSeconds(59);  //本年年末
                    ThisYearFinishedWork = $"本年完成工作 {currentFinishedWork.Where(w => w.EndTime >= startYear&&w.EndTime<= endYear).Count()} 条"; 
                }
            });
        }

        /// <summary>
        /// 获取 文件类型
        /// </summary>
        private void GetFileType()
        {
            fileTypes.Clear();
            using (WorkEfficiencyDataContext work=new WorkEfficiencyDataContext())
            {
                var current = work.FileTypeDB.Where(w => w.UserId == GlobalData.GetInstance().UserInfo.GuidId).ToList();
                foreach (var item in current)
                {
                    item.HaveSetting = work.FileModelDB.Where(w => w.FileType == item.Types).ToList().Count;
                    fileTypes.Add(item);
                }
            }
        }
        #endregion 私有方法

        #region 命令
        /// <summary>
        /// 添加1条新的工作计划
        /// </summary>
        public RelayCommand AddWorkCommand => new RelayCommand(() =>
        {
            var current = new FileModel()
            {
                GuidId = Guid.NewGuid().ToString(),
                IsEdit = true,
                IsFinished = false,
                CreateTime = DateTime.Now,
                EndTime = DateTime.Now,
                UserGuid=GlobalData.GetInstance().UserInfo.GuidId,
                UserName=GlobalData.GetInstance().UserInfo.UserName
            };
            
            if(Messenger.Default.Send<bool>("InsertFinishWork", current))
            {
                using (WorkEfficiencyDataContext work = new WorkEfficiencyDataContext())
                {
                    work.FileModelDB.Add(current);
                    work.SaveChanges();
                }
                GetTotalWorking();
            }

        });
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
        /// 打开设置窗体命令
        /// </summary>
        public RelayCommand OpenSettingWindow => new RelayCommand(() =>
        {
            if (Convert.ToBoolean(WindowsManager.CreateDialogWindowByViewModelResult("SettingView", new SettingViewModel())))
            {
                DialogWindow.Show("保存设置成功！", MessageType.Successful, WindowsManager.Windows["MainView"]);
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
                using (WorkEfficiencyDataContext work=new WorkEfficiencyDataContext())
                {
                    work.FileAttachmentModelDB.Remove(f);
                    work.SaveChanges();
                }
                DialogWindow.Show("已成功将文件删除！", MessageType.Successful, WindowsManager.Windows["MainView"]);
            });
        /// <summary>
        /// 导出工作信息成word--待开发，预留功能
        /// </summary>
        public RelayCommand ExportFileModelToWord => new RelayCommand(() =>
        {
            if (selectedItem!=null)
            {
                if(DialogWindow.ShowDialog($"是否要导出 {selectedItem.FileTitle} 到word？", "提示"))
                {

                }
            }
        });

        /// <summary>
        /// 针对选择的文件，添加描述信息
        /// </summary>
        public RelayCommand AddFileDesCommand => new RelayCommand(() =>
          {
              if (selectedItem!=null)
              {
                  var workDes = WindowsManager.CreateDialogWindowByViewModelResult("AddFileDes", new AddFileDesViewModel(SelectedItem)) as WorkDescription;
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

            Messenger.Default.Register(this, "GetTotalWorking", GetTotalWorking);
        }
        /// <summary>
        /// 将选择的FileModel，获取附件之后，将完整信息显示到下方的panel中
        /// </summary>
        /// <param name="entity"></param>
        private void ShowWorkInfo(FileModel entity)
        {
            if (entity!=null)
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
            }
        }

        #endregion

        #region 事件

        #endregion
    }
}
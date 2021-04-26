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
using Newtonsoft.Json;
using YC.WorkEfficiency.DataAccess;
using YC.WorkEfficiency.Models;
using YC.WorkEfficiency.SimpleMVVM;
using YC.WorkEfficiency.ViewModels.Common;

namespace YC.WorkEfficiency.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        private FileModel selectedItem;

        public MainViewModel()
        {
            //构造函数
            Title = "时间效率管理";
            InitData();

            
        }

        #region 属性

        public BaseCommand baseCommand { get; set; } = new BaseCommand();
        public ObservableCollection<FileModel> FileList { get; set; }

        //public FileModelData fileModelData { get; set; }

        public FileModel SelectedItem { get => selectedItem; set { selectedItem = value; DoNotify(); } }
        //public ObservableCollection<>

        private string _TotleWorkTime;

        public string TotleWorkTime
        {
            get { return _TotleWorkTime; }
            set { _TotleWorkTime = value; DoNotify(); }
        }

        public ObservableCollection<FileAttachmentModel> fileAttachmentModels { get; set; }

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

        public FrameworkElement NoFinishedWorkFrame { get; set; }
        public FrameworkElement FinishedWorkFrame { get; set; }
        #endregion 属性

        #region 私有方法

        /// <summary>
        /// 初始化数据
        /// </summary>
        private void InitData()
        {
            
            fileAttachmentModels = new ObservableCollection<FileAttachmentModel>();
            
            
            LoadModuels();
            MessengerRegister();
            GetTotleTime();
        }
        /// <summary>
        /// 获取模块并加载到MainViewModel
        /// </summary>
        private void LoadModuels()
        {
            LoadModulesServices.Instance.LoadModules();
            NoFinishedWorkFrame = LoadModulesServices.Instance.ModulesDic["未完成的工作"];

            FinishedWorkFrame = LoadModulesServices.Instance.ModulesDic["已完成的工作"];
            
        }

        private void GetTotleTime()
        {
            Task.Run(()=> 
            {
                int day = 0;
                int hours = 0;
                int minutes = 0;
                int seconds = 0;
                using (WorkEfficiencyDataContext fileModelDataContext =new WorkEfficiencyDataContext())
                {
                    var current= fileModelDataContext.FileModelDB.Where(s => s.IsFinished == true).ToList();
                    
                    foreach (var item in current)
                    {
                        string itemday = item.AfterTime.Split('-')[0];
                        string newitemday = itemday.Replace('天', ' ');
                        day += Convert.ToInt32(newitemday.Trim());

                        string[] times= item.AfterTime.Split('-')[1].Split(':');
                        seconds+= Convert.ToInt32(times[2]);
                        minutes+= Convert.ToInt32(times[1]);
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
        #endregion 私有方法

        #region 命令
        public RelayCommand AddWorkCommand => new RelayCommand(() =>
        {
            var current = new FileModel()
            {
                GuidId = Guid.NewGuid().ToString(),
                IsEdit = true,
                IsFinished = false,
                CreateTime = DateTime.Now,
                EndTime = DateTime.Now
            };
            using (WorkEfficiencyDataContext work = new WorkEfficiencyDataContext())
            {
                work.FileModelDB.Add(current);
                work.SaveChanges();
            }
            //WorkingList.Insert(0, current);
            Messenger.Default.Send("InsertFinishWork", current);

        });

        //public RelayCommand RefreshData => new RelayCommand(() =>
        //{
        //    //GetData();
        //});

        //public RelayCommand<FileModel> QueDing => new RelayCommand<FileModel>((f) =>
        //{
        //    if (f != null)
        //    {
        //        f.CreateTime = DateTime.Now;
        //        f.IsEdit = false;
        //    }
        //});

        //public RelayCommand<FileModel> FinishSettingCommand => new RelayCommand<FileModel>((f) =>
        //{
        //    if (f != null)
        //    {
        //        f.EndTime = DateTime.Now;
        //        f.IsFinished = true;

        //        fileModelData.WorkingList.Remove(f);
        //        fileModelData.EndingList.Add(f);

        //        using (WorkEfficiencyDataContext fileModelDataContext = new WorkEfficiencyDataContext())
        //        {
        //            fileModelDataContext.Update(f);
        //            fileModelDataContext.SaveChanges();
        //        }
        //    }
        //});

        public RelayCommand<FileModel> SelectionChangeCommand => new RelayCommand<FileModel>((o) =>
        {
            //FileModel fileModel = o as FileModel;
            //MessageBox.Show(SelectedItem.GuidId);
            //SelectedItem = o;
            if (o!=null)
            {
                string guid = o.GuidId;
                using(WorkEfficiencyDataContext fileAttachmentModelData =new WorkEfficiencyDataContext())
                {
                    fileAttachmentModelData.Database.EnsureCreated();
                    fileAttachmentModels.Clear();
                    var current= fileAttachmentModelData.FileAttachmentModelDB.Where(w => w.ParentGuidId == guid).ToList();
                    if (current.Count>0)
                    {
                        foreach (var item in current)
                        {
                            fileAttachmentModels.Add(item);
                        }
                        
                    }
                    
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
        }
        /// <summary>
        /// 将选择的FileModel，获取附件之后，将完整信息显示到下方的panel中
        /// </summary>
        /// <param name="entity"></param>
        private void ShowWorkInfo(FileModel entity)
        {
            if (entity!=null)
            {
                selectedItem = entity;
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
    }
}
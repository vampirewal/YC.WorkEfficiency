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
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using Newtonsoft.Json;
using YC.WorkEfficiency.View.DataAccess;
using YC.WorkEfficiency.Models;
using YC.WorkEfficiency.ViewModels.Common;

namespace YC.WorkEfficiency.View.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        private FileModel selectedItem;

        public MainViewModel()
        {
            //构造函数
            Title = "时间效率管理";
            InitData();
            //using (testModelDataContext testModelDataContext = new testModelDataContext())
            //{
            //    testModelDataContext.Database.EnsureCreated();
            //    testModelDataContext.SaveChanges();

            //    //testModelDataContext.testModelDB.
            //}
            LoadModulesServices.Instance.LoadModules();
            testFrame = LoadModulesServices.Instance.ModulesDic["未完成的工作"];
        }

        #region 属性

        public BaseCommand baseCommand { get; set; } = new BaseCommand();
        public ObservableCollection<FileModel> FileList { get; set; }

        public FileModelData fileModelData { get; set; }
        public string Title { get; set; }

        public FileModel SelectedItem { get => selectedItem; set { selectedItem = value;RaisePropertyChanged(); } }
        //public ObservableCollection<>

        private string _TotleWorkTime;

        public string TotleWorkTime
        {
            get { return _TotleWorkTime; }
            set { _TotleWorkTime = value; RaisePropertyChanged(); }
        }

        public ObservableCollection<FileAttachmentModel> fileAttachmentModels { get; set; }

        public FrameworkElement testFrame { get; set; }

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
        #endregion 属性

        #region 私有方法

        /// <summary>
        /// 读取本地文件夹内的文件
        /// </summary>
        private void LoadFile()
        {
            List<FileModel> currentList = new List<FileModel>();
            string filePath = $"{AppDomain.CurrentDomain.BaseDirectory}Works";
            if (!Directory.Exists(filePath))
            {
                Directory.CreateDirectory(filePath);
            }
            //获取文件夹内的json文件
            string[] files = Directory.GetFiles(filePath);

            foreach (var file in files)
            {
                var current = File.ReadAllText(file);
                FileModel nm = JsonConvert.DeserializeObject<FileModel>(current);
                var dic = Directory.GetParent(file).FullName;//获取文件的所在文件夹
                currentList.Add(nm);
            }
        }

        /// <summary>
        /// 初始化数据
        /// </summary>
        private void InitData()
        {
            fileModelData = new FileModelData();
            fileAttachmentModels = new ObservableCollection<FileAttachmentModel>();
            GetData();
            StartWork();
            GetTotleTime();
        }

        private void GetData()
        {
            using (FileModelDataContext fileModelDataContext = new FileModelDataContext())
            {
                //fileModelDataContext.Add(new FileModel() { GuidId = Guid.NewGuid().ToString() });
                //fileModelDataContext.SaveChanges();

                //1、第一步，先获取当前的时间
                TimeSpan tsNow = new TimeSpan(DateTime.Now.Ticks);
                //2、第二步，从sqlite数据库中获取到数据，转化为List
                var result = fileModelDataContext.FileModelDB.Where(w => w.GuidId != null && w.IsFinished == false).ToList();

                var EndResult = fileModelDataContext.FileModelDB.Where(w => w.GuidId != null && w.IsFinished == true).ToList();
                //3、在workList中的每一条都与互相排序
                result.Sort((left, right) =>
                {
                    if (left.EndTime > right.EndTime)
                    {
                        return 1;
                    }
                    else
                    {
                        return -1;
                    }
                });
                fileModelData.WorkingList = new ObservableCollection<FileModel>(result);
                EndResult.Sort((left, right) => 
                {
                    if (left.CreateTime > right.CreateTime)
                    {
                        return -1;
                    }
                    else
                    {
                        return 1;
                    }
                });
                fileModelData.EndingList = new ObservableCollection<FileModel>(EndResult);
            }
        }

        private void StartWork()
        {
            Thread td = new Thread(ActionWork);
            td.Start();
        }

        private void ActionWork()
        {
            while (true)
            {
                Thread.Sleep(1000);
                if (fileModelData.WorkingList.Count > 0)
                {
                    TimeSpan ts_now = new TimeSpan(DateTime.Now.Ticks);
                    foreach (var item in fileModelData.WorkingList)
                    {
                        if (!item.IsFinished && !item.IsEdit)
                        {
                            TimeSpan ts_createtime = new TimeSpan(item.CreateTime.Ticks);
                            TimeSpan ts = ts_now.Subtract(ts_createtime);
                            item.AfterTime = $"{ts.Days}天-{ts.Hours}:{ts.Minutes}:{ts.Seconds}";
                        }
                    }
                    using (FileModelDataContext fileModelDataContext = new FileModelDataContext())
                    {
                        fileModelDataContext.UpdateRange(fileModelData.WorkingList);
                        fileModelDataContext.SaveChanges();
                    }
                }
            }
        }

        private void GetTotleTime()
        {
            Task.Run(()=> 
            {
                int day = 0;
                int hours = 0;
                int minutes = 0;
                int seconds = 0;
                using (FileModelDataContext fileModelDataContext=new FileModelDataContext())
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
            using (FileModelDataContext fileModelDataContext = new FileModelDataContext())
            {
                fileModelDataContext.FileModelDB.Add(current);
                fileModelDataContext.SaveChanges();
            }
            fileModelData.WorkingList.Insert(0, current);

        });

        public RelayCommand RefreshData => new RelayCommand(() =>
        {
            GetData();
        });

        public RelayCommand<FileModel> QueDing => new RelayCommand<FileModel>((f) =>
        {
            if (f != null)
            {
                f.CreateTime = DateTime.Now;
                f.IsEdit = false;
            }
        });

        public RelayCommand<FileModel> FinishSettingCommand => new RelayCommand<FileModel>((f) =>
        {
            if (f != null)
            {
                f.EndTime = DateTime.Now;
                f.IsFinished = true;

                fileModelData.WorkingList.Remove(f);
                fileModelData.EndingList.Add(f);

                using (FileModelDataContext fileModelDataContext = new FileModelDataContext())
                {
                    fileModelDataContext.Update(f);
                    fileModelDataContext.SaveChanges();
                }
            }
        });

        public RelayCommand<FileModel> SelectionChangeCommand => new RelayCommand<FileModel>((o) =>
        {
            //FileModel fileModel = o as FileModel;
            //MessageBox.Show(SelectedItem.GuidId);
            //SelectedItem = o;
            if (o!=null)
            {
                string guid = o.GuidId;
                using(FileAttachmentModelDataContext fileAttachmentModelData=new FileAttachmentModelDataContext())
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
    }

    /// <summary>
    /// 后期进行存储的DataModel类
    /// </summary>
    public class FileModelData : ObservableObject
    {
        public ObservableCollection<FileModel> WorkingList { get; set; }

        public ObservableCollection<FileModel> EndingList { get; set; }
    }
}
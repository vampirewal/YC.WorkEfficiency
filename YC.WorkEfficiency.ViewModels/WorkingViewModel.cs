#region << 文 件 说 明 >>
/*----------------------------------------------------------------
// 文件名称：WorkingViewModel
// 创 建 者：杨程
// 创建时间：2021/4/25 15:15:32
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
using System.Linq;
using System.Text;
using System.Threading;
using YC.WorkEfficiency.DataAccess;
using YC.WorkEfficiency.Models;
using YC.WorkEfficiency.SimpleMVVM;

namespace YC.WorkEfficiency.ViewModels
{
    public class WorkingViewModel : ViewModelBase
    {
        public WorkingViewModel()
        {
            //构造函数
            Title = "未完成的工作";
            InitData();
        }
        public override object GetResult()
        {
            return null;
        }

        #region 属性
        public ObservableCollection<FileModel> WorkingList { get; set; }

        private FileModel selectedItem;
        public FileModel SelectedItem { get => selectedItem; set { selectedItem = value; DoNotify(); } }
        #endregion

        #region 公共方法

        #endregion

        #region 私有方法
        /// <summary>
        /// 初始化数据
        /// </summary>
        private void InitData()
        {
            GetData();
            MessengerRegist();
            StartWork();
        }
        /// <summary>
        /// 获取数据
        /// </summary>
        private void GetData()
        {
            using (WorkEfficiencyDataContext work = new WorkEfficiencyDataContext())
            {
                //fileModelDataContext.Add(new FileModel() { GuidId = Guid.NewGuid().ToString() });
                //fileModelDataContext.SaveChanges();

                //1、第一步，先获取当前的时间
                TimeSpan tsNow = new TimeSpan(DateTime.Now.Ticks);
                //2、第二步，从sqlite数据库中获取到数据，转化为List
                var result = work.FileModelDB.Where(w => w.GuidId != null && w.IsFinished == false).ToList();
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
                WorkingList = new ObservableCollection<FileModel>(result);
            }
        }
        /// <summary>
        /// 注册消息
        /// </summary>
        private void MessengerRegist()
        {
            Messenger.Default.Register<FileModel>(this, "AddNoFinishedWork", AddNoFinishedWork);
            Messenger.Default.Register<FileModel>(this, "InsertFinishWork", InsertFinishWork);
        }


        #region 消息
        private void AddNoFinishedWork(FileModel entity)
        {
            WorkingList.Add(entity);
        }
        private void InsertFinishWork(FileModel entity)
        {
            if (WorkingList.Where(w => w.IsEdit == true).Count()==0)
            {
                WorkingList.Insert(0, entity);
            }
            
        }
        #endregion

        #region 开启一个新的线程来执行这个方法
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
                if (WorkingList.Count > 0)
                {
                    TimeSpan ts_now = new TimeSpan(DateTime.Now.Ticks);
                    foreach (var item in WorkingList)
                    {
                        if (!item.IsFinished && !item.IsEdit)
                        {
                            TimeSpan ts_createtime = new TimeSpan(item.CreateTime.Ticks);
                            TimeSpan ts = ts_now.Subtract(ts_createtime);
                            item.AfterTime = $"{ts.Days}天-{ts.Hours}:{ts.Minutes}:{ts.Seconds}";
                        }
                    }
                    using (WorkEfficiencyDataContext fileModelDataContext = new WorkEfficiencyDataContext())
                    {
                        fileModelDataContext.UpdateRange(WorkingList);
                        fileModelDataContext.SaveChanges();
                    }
                }
            }
        }
        #endregion

        #endregion

        #region 命令
        public RelayCommand<FileModel> QueDing => new RelayCommand<FileModel>((f) =>
        {
            if (f != null)
            {
                f.CreateTime = DateTime.Now;
                f.IsEdit = false;
            }
        });
        public RelayCommand<FileModel> SelectionChangeCommand => new RelayCommand<FileModel>((o) =>
        {
            if (o != null)
            {
                Messenger.Default.Send("ShowWorkInfo", o);
            }
        });

        public RelayCommand<FileModel> FinishSettingCommand => new RelayCommand<FileModel>((f) =>
        {
            if (f != null)
            {
                f.EndTime = DateTime.Now;
                f.IsFinished = true;

                WorkingList.Remove(f);
                //EndingList.Add(f);
                Messenger.Default.Send("AddFinishedWork", f);

                using (WorkEfficiencyDataContext work = new WorkEfficiencyDataContext())
                {
                    work.Update(f);
                    work.SaveChanges();
                }
            }
        });
        #endregion
    }
}

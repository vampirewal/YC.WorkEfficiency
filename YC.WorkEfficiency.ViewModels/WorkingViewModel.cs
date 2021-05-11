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
using YC.WorkEfficiency.Themes;
using YC.WorkEfficiency.ViewModels.Common;

namespace YC.WorkEfficiency.ViewModels
{
    public class WorkingViewModel : ViewModelBase
    {
        public WorkingViewModel()
        {
            //构造函数
            Title = "未完成的工作";
            //InitData();
        }

        #region 重写
        public override void InitData()
        {
            GetData();
            MessengerRegist();
            StartWork();
        }
        #endregion

        #region 属性
        public ObservableCollection<FileModel> WorkingList { get; set; }
        

        private FileModel selectedItem;
        public FileModel SelectedItem { get => selectedItem; set { selectedItem = value; DoNotify(); } }
        #endregion

        #region 公共方法

        #endregion

        #region 私有方法
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
                var result = work.FileModelDB.Where(w => w.GuidId != null && w.IsFinished == false&&w.UserGuid==GlobalData.GetInstance().UserInfo.GuidId).ToList();
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
                    
                    using (WorkEfficiencyDataContext fileModelDataContext = new WorkEfficiencyDataContext())
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

                        fileModelDataContext.UpdateRange(WorkingList);
                        fileModelDataContext.SaveChanges();
                    }
                }
            }
        }
        #endregion

        #endregion

        #region 消息
        /// <summary>
        /// 注册消息
        /// </summary>
        private void MessengerRegist()
        {
            Messenger.Default.Register<FileModel, bool>(this, "InsertFinishWork", InsertFinishWork);

            Messenger.Default.Register(this, "GetWorkingList", GetData);
        }
        /// <summary>
        /// 插入一条最新的工作到工作表的最上面
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        private bool InsertFinishWork(FileModel entity)
        {
            //需要判断一下在工作表中，是否还存在有草稿状态的工作
            if (WorkingList.Where(w => w.IsEdit == true).Count() == 0)
            {
                WorkingList.Insert(0, entity);
                return true;
            }
            else
            {
                DialogWindow.Show("已存在一条草稿状态的工作，请编辑完成后再新增！", MessageType.Error, WindowsManager.Windows["MainView"]);
                return false;
            }
        }
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
        
        /// <summary>
        /// 完成工作命令
        /// </summary>
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
                Messenger.Default.Send("GetTotalWorking");
            }
        });

        /// <summary>
        /// 查看工作详细命令
        /// </summary>
        public RelayCommand<FileModel> WatchInfoCommand => new RelayCommand<FileModel>((f) =>
          {
              if (f!=null)
              {
                  Messenger.Default.Send("ShowWorkInfo", f);
              }
          });
        #endregion
    }
}

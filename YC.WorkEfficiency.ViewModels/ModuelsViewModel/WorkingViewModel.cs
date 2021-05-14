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
using System.Windows;
using HandyControl.Controls;
using HandyControl.Data;
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
        //正在执行的工作列表
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
                //从sqlite数据库中获取到数据，转化为List
                var result = work.FileModelDB.Where(w => w.GuidId != null && w.IsFinished == false&&w.UserGuid==GlobalData.GetInstance().UserInfo.GuidId).ToList();
                //在workList中的每一条都与互相排序
                result.Sort((left, right) =>
                {
                    if (left.ExpectEndTime > right.ExpectEndTime)
                    {
                        return 1;
                    }
                    else
                    {
                        return -1;
                    }
                });

                foreach (var item in result)
                {
                    if (!item.IsEdit)
                    {
                        item.dt.Start();
                    }
                }

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
                var current = WorkingList.Where(w => !w.IsEdit).ToList();
                if (current.Count > 0)
                {
                    var datetimeNow = DateTime.Now;
                    using (WorkEfficiencyDataContext fileModelDataContext = new WorkEfficiencyDataContext())
                    {
                        TimeSpan ts_now = new TimeSpan(datetimeNow.Ticks);
                        foreach (var item in current)
                        {
                            if (!item.IsFinished && !item.IsEdit)
                            {
                                //TimeSpan ts_createtime = new TimeSpan(item.CreateTime.Ticks);
                                //TimeSpan ts = ts_now.Subtract(ts_createtime);
                                //item.AfterTime = $"{ts.Days}天-{ts.Hours}:{ts.Minutes}:{ts.Seconds}";

                                
                                TimeSpan ts_ExpectEndTime = new TimeSpan(item.ExpectEndTime.Ticks);
                                TimeSpan tsExpect = ts_ExpectEndTime.Subtract(ts_now);

                                if (tsExpect.Minutes<=5&&item.IsNotify==false)
                                {
                                    Application.Current.Dispatcher.Invoke(() =>
                                    {
                                        //此处弹窗
                                        DialogWindow.ShowNotify($"工作：<<{item.FileTitle}>> \r\n该条工作即将到达预计结束时间！请尽快处理！", 3);
                                        item.IsNotify = true;
                                    });
                                }
                            }
                        }
                        fileModelDataContext.UpdateRange(current);
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
            //注册新增1条工作的消息
            Messenger.Default.Register<FileModel, bool>(this, "InsertFinishWork", InsertFinishWork);
            //注册获取数据的消息，用于其他ViewModel调用
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
        //每条工作在初步设定值之后，点击确定开始执行计时的命令
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

        public RelayCommand<DateTime> SelectedDateTimeChangedCommand => new RelayCommand<DateTime>((dt) =>
          {

          });
        #endregion
    }
}

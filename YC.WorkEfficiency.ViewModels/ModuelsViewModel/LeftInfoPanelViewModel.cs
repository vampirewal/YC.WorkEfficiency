#region << 文 件 说 明 >>
/*----------------------------------------------------------------
// 文件名称：LeftInfoPanelViewModel
// 创 建 者：杨程
// 创建时间：2021/5/12 14:33:36
// 文件版本：V1.0.0
// ===============================================================
// 功能描述：
//		
//
//----------------------------------------------------------------*/
#endregion

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YC.WorkEfficiency.DataAccess;
using YC.WorkEfficiency.Models;
using YC.WorkEfficiency.SimpleMVVM;
using YC.WorkEfficiency.ViewModels.Common;

namespace YC.WorkEfficiency.ViewModels
{
    public class LeftInfoPanelViewModel:ViewModelBase
    {
        public LeftInfoPanelViewModel()
        {
            //构造函数
        }
        #region 重写
        public override void InitData()
        {
            GetTotalWorking();
            MessengerRegister();
        }
        #endregion

        #region 属性

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

        #endregion

        #region 公共方法

        #endregion

        #region 私有方法
        /// <summary>
        /// 获取数据
        /// </summary>
        private void GetTotalWorking()
        {
            Task.Run(() =>
            {
                #region 获取汇总工作时间
                int day = 0;
                int hours = 0;
                int minutes = 0;
                int seconds = 0;
                using (WorkEfficiencyDataContext fileModelDataContext = new WorkEfficiencyDataContext())
                {
                    var current = fileModelDataContext.FileModelDB.Where(s => s.IsFinished == true && s.UserGuid == GlobalData.GetInstance().UserInfo.GuidId).ToList();

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
                #endregion

                DateTime dt = DateTime.Now;  //当前时间  
                using (WorkEfficiencyDataContext work = new WorkEfficiencyDataContext())
                {
                    var currentCount = work.FileModelDB.Where(w => w.UserGuid == GlobalData.GetInstance().UserInfo.GuidId && w.IsFinished == false).Count();
                    if (currentCount > 0)
                    {
                        TotalWorking = $"现在还有{currentCount}条工作待完成";
                    }
                    else
                    {
                        TotalWorking = $"现在没有待完成工作";
                    }
                    //先获取登陆用户名下的已完成工作数
                    var currentFinishedWork = work.FileModelDB.Where(w => w.UserGuid == GlobalData.GetInstance().UserInfo.GuidId && w.IsFinished == true && w.IsDeleted == false).ToList();
                    //本周已完成的工作
                    DateTime startWeek = Convert.ToDateTime(dt.AddDays(1 - Convert.ToInt32(dt.DayOfWeek.ToString("d"))).ToString("yyyy/MM/dd 00:00:00"));  //本周周一  
                    DateTime endWeek = startWeek.AddDays(6).AddHours(23).AddMinutes(59).AddSeconds(59);  //本周周日 
                    ThisWeekFinishedWork = $"本周完成工作 {currentFinishedWork.Where(w => w.EndTime >= startWeek && w.EndTime <= endWeek).Count()} 条";
                    //本月
                    DateTime startMonth = Convert.ToDateTime(dt.AddDays(1 - dt.Day).ToString("yyyy/MM/dd 00:00:00"));  //本月月初  
                    DateTime endMonth = startMonth.AddMonths(1).AddDays(-1).AddHours(23).AddMinutes(59).AddSeconds(59);  //本月月末
                    ThisMouthFinishedWork = $"本月完成工作 {currentFinishedWork.Where(w => w.EndTime >= startMonth && w.EndTime <= endMonth).Count()} 条";
                    //本年
                    DateTime startYear = new DateTime(dt.Year, 1, 1);  //本年年初 
                    DateTime endYear = new DateTime(dt.Year, 12, 31).AddHours(23).AddMinutes(59).AddSeconds(59);  //本年年末
                    ThisYearFinishedWork = $"本年完成工作 {currentFinishedWork.Where(w => w.EndTime >= startYear && w.EndTime <= endYear).Count()} 条";
                }
            });
        }
        #endregion

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
                UserGuid = GlobalData.GetInstance().UserInfo.GuidId,
                UserName = GlobalData.GetInstance().UserInfo.UserName
            };

            if (Messenger.Default.Send<bool>("InsertFinishWork", current))
            {
                using (WorkEfficiencyDataContext work = new WorkEfficiencyDataContext())
                {
                    work.FileModelDB.Add(current);
                    work.SaveChanges();
                }
                Messenger.Default.Send("GetTotalWorking");
            }

        });
        #endregion

        #region 消息
        /// <summary>
        /// 汇总消息注册
        /// </summary>
        private void MessengerRegister()
        {
            Messenger.Default.Register(this, "GetTotalWorking", GetTotalWorking);
        }
        #endregion
    }
}

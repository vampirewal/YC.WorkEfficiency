#region << 文 件 说 明 >>
/*----------------------------------------------------------------
// 文件名称：FinishedWorkViewModel
// 创 建 者：杨程
// 创建时间：2021/4/25 17:59:27
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
using YC.WorkEfficiency.DataAccess;
using YC.WorkEfficiency.Models;
using YC.WorkEfficiency.SimpleMVVM;
using YC.WorkEfficiency.Themes;
using YC.WorkEfficiency.ViewModels.Common;

namespace YC.WorkEfficiency.ViewModels
{
    public class FinishedWorkViewModel:ViewModelBase
    {
        public FinishedWorkViewModel()
        {
            //构造函数
            //InitData();
        }

        #region 重写
        public override void InitData()
        {
            FinishedWorkList = new ObservableCollection<FileModel>();
            GetData();
            MessagerRegistMethod();
        } 
        #endregion

        /// <summary>
        /// 注册消息
        /// </summary>
        private void MessagerRegistMethod()
        {
            Messenger.Default.Register<FileModel>(this, "AddFinishedWork", AddFinishedWork);
            Messenger.Default.Register(this, "GetFinishedWork", GetData);
        }

        #region 属性
        public ObservableCollection<FileModel> FinishedWorkList { get; set; }

        
        #endregion

        #region 公共方法

        #endregion

        #region 私有方法
        /// <summary>
        /// 获取页面数据
        /// </summary>
        private void GetData()
        {
            FinishedWorkList.Clear();
            using (WorkEfficiencyDataContext work = new WorkEfficiencyDataContext())
            {
                //fileModelDataContext.Add(new FileModel() { GuidId = Guid.NewGuid().ToString() });
                //fileModelDataContext.SaveChanges();

                //1、第一步，先获取当前的时间
                TimeSpan tsNow = new TimeSpan(DateTime.Now.Ticks);
                //2、第二步，从sqlite数据库中获取到数据，转化为List
                var EndResult = work.FileModelDB.Where(w => w.GuidId != null && w.IsFinished == true&&w.UserGuid==GlobalData.GetInstance().UserInfo.GuidId&&w.IsDeleted==false).ToList();
                //3、在workList中的每一条都与互相排序
                EndResult.Sort((left, right) =>
                {
                    if (left.EndTime > right.EndTime)
                    {
                        return -1;
                    }
                    else
                    {
                        return 1;
                    }
                });
                foreach (var item in EndResult)
                {
                    FinishedWorkList.Add(item);
                }
            }
        }

        
        public void AddFinishedWork(FileModel entity)
        {
            FinishedWorkList.Add(entity);
        }

        #endregion

        #region 命令

        /// <summary>
        /// 查看工作详细命令
        /// </summary>
        public RelayCommand<FileModel> WatchInfoCommand => new RelayCommand<FileModel>((f) =>
        {
            if (f != null)
            {
                Messenger.Default.Send("ShowWorkInfo", f);
            }
        });

        /// <summary>
        /// 逻辑删除文件
        /// </summary>
        public RelayCommand<FileModel> DeleteFileCommand => new RelayCommand<FileModel>((f) =>
          {
              if (f != null)
              {
                  if (DialogWindow.ShowDialog("是否删除该文件？", "请确认"))
                  {
                      using(WorkEfficiencyDataContext work=new WorkEfficiencyDataContext())
                      {
                          f.IsDeleted = true;
                          work.FileModelDB.Update(f);
                          work.SaveChanges();
                          FinishedWorkList.Remove(f);
                          Messenger.Default.Send("GetTotalWorking");
                      }
                  }
              }
          });
        #endregion
    }
}

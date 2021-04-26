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

namespace YC.WorkEfficiency.ViewModels
{
    public class FinishedWorkViewModel:ViewModelBase
    {
        public FinishedWorkViewModel()
        {
            //构造函数
            InitData();

        }

        private void InitData()
        {
            GetData();
            MessagerRegistMethod();
        }

        private void MessagerRegistMethod()
        {
            Messenger.Default.Register<FileModel>(this, "AddFinishedWork", AddFinishedWork);
        }
        #region 属性
        public ObservableCollection<FileModel> FinishedWorkList { get; set; }

        private FileModel selectedItem;
        public FileModel SelectedItem { get => selectedItem; set { selectedItem = value; DoNotify(); } }
        #endregion

        #region 公共方法

        #endregion

        #region 私有方法
        private void GetData()
        {
            using (WorkEfficiencyDataContext fileModelDataContext = new WorkEfficiencyDataContext())
            {
                //fileModelDataContext.Add(new FileModel() { GuidId = Guid.NewGuid().ToString() });
                //fileModelDataContext.SaveChanges();

                //1、第一步，先获取当前的时间
                TimeSpan tsNow = new TimeSpan(DateTime.Now.Ticks);
                //2、第二步，从sqlite数据库中获取到数据，转化为List
                var EndResult = fileModelDataContext.FileModelDB.Where(w => w.GuidId != null && w.IsFinished == true).ToList();
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
                FinishedWorkList = new ObservableCollection<FileModel>(EndResult);

            }
        }

        
        public void AddFinishedWork(FileModel entity)
        {
            FinishedWorkList.Add(entity);
        }
        #endregion

        #region 命令

        #endregion
    }
}

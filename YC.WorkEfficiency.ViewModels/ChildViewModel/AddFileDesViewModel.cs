#region << 文 件 说 明 >>
/*----------------------------------------------------------------
// 文件名称：AddFileDesViewModel
// 创 建 者：杨程
// 创建时间：2021/5/11 11:45:52
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
using System.Windows;
using System.Windows.Media;
using YC.WorkEfficiency.DataAccess;
using YC.WorkEfficiency.Models;
using YC.WorkEfficiency.SimpleMVVM;
using YC.WorkEfficiency.Themes;
using YC.WorkEfficiency.ViewModels.Common;

namespace YC.WorkEfficiency.ViewModels
{
    public class AddFileDesViewModel:ViewModelBase
    {
        public AddFileDesViewModel(FileModel _fileModel)
        {
            //构造函数
            fileModel = _fileModel;
            Title = "新增工作描述";
            
        }
        #region 重写
        public override object GetResult()
        {
            return workDescription;
        }

        public override void InitData()
        {
            workDescription = new WorkDescription() 
            { 
                GuidId=Guid.NewGuid().ToString(),
                
                UserGuidId=GlobalData.GetInstance().UserInfo.GuidId
            };
            GetWorkDescriptionTypeList();
        }
        #endregion

        #region 属性
        private FileModel fileModel;

        public WorkDescription workDescription { get; set; }

        public ObservableCollection<WorkDescriptionType> TypeList { get; set; }

        private WorkDescriptionType _SelectType;
        public WorkDescriptionType SelectType { 
            get=> _SelectType; 
            set
            {
                _SelectType = value;
                DoNotify();
            } 
        }

        #endregion

        #region 公共方法

        #endregion

        #region 私有方法
        private void GetWorkDescriptionTypeList()
        {
            TypeList = new ObservableCollection<WorkDescriptionType>();
            using (WorkEfficiencyDataContext work=new WorkEfficiencyDataContext())
            {
                var current= work.workDescriptionTypesDB.Where(w => w.UserGuidId == GlobalData.GetInstance().UserInfo.GuidId).ToList();

                foreach (var item in current)
                {
                    TypeList.Add(item);
                }
            }
        }
        #endregion

        #region 命令
        public RelayCommand<string> SelectDesChangedCommand => new RelayCommand<string>((s) =>
          {
              if (!string.IsNullOrEmpty(s))
              {
                  using(WorkEfficiencyDataContext work=new WorkEfficiencyDataContext())
                  {
                      SelectType= work.workDescriptionTypesDB.Where(w => w.GuidId == s).First();
                  }
              }
          });

        /// <summary>
        /// 保存命令
        /// </summary>
        public RelayCommand SaveWorkDesCommand => new RelayCommand(() =>
          {
              //workDescription
              if (!string.IsNullOrEmpty(workDescription.WorkDescriptionText))
              {
                  workDescription.CreateTime = DateTime.Now;
                  workDescription.WorkDescriptionTypeGuid = SelectType.GuidId;
                  workDescription.FileModelGuidId = fileModel.GuidId;
                  using (WorkEfficiencyDataContext work=new WorkEfficiencyDataContext())
                  {
                      work.WorkDescriptionDB.Add(workDescription);
                      work.SaveChanges();
                      DialogWindow.Show("添加新的工作描述成功！", MessageType.Successful, WindowsManager.Windows["MainView"]);
                      WindowsManager.CloseWindow(View as Window);

                      
                  }

              }
          });
        #endregion

    }
}

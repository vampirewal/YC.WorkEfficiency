#region << 文 件 说 明 >>
/*----------------------------------------------------------------
// 文件名称：EditFileDesViewModel
// 创 建 者：杨程
// 创建时间：2021/5/12 16:09:02
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
using YC.WorkEfficiency.DataAccess;
using YC.WorkEfficiency.Models;
using YC.WorkEfficiency.SimpleMVVM;
using YC.WorkEfficiency.Themes;
using YC.WorkEfficiency.ViewModels.Common;

namespace YC.WorkEfficiency.ViewModels
{
    public class EditFileDesViewModel:ViewModelBase
    {
        public EditFileDesViewModel(WorkDescription _WorkDescription)
        {
            //构造函数
            workDescription = _WorkDescription;
            Title = "修改工作描述";
            SelectType = GetWorkDesType(workDescription.WorkDescriptionTypeGuid);
        }
        #region 重写
        public override object GetResult()
        {
            return workDescription;
        }

        public override void InitData()
        {
            GetWorkDescriptionTypeList();
        }
        #endregion

        #region 属性
        //private WorkDescription workDescription;

        /// <summary>
        /// 绑定前端
        /// </summary>
        public WorkDescription workDescription { get; set; }

        /// <summary>
        /// 工作描述类型，由用户自己设置
        /// </summary>
        public ObservableCollection<WorkDescriptionType> TypeList { get; set; }

        private WorkDescriptionType _SelectType;
        /// <summary>
        /// 用户选择的工作描述类型
        /// </summary>
        public WorkDescriptionType SelectType
        {
            get => _SelectType;
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
        /// <summary>
        /// 获取工作描述类型的数据
        /// </summary>
        private void GetWorkDescriptionTypeList()
        {
            TypeList = new ObservableCollection<WorkDescriptionType>();
            using (WorkEfficiencyDataContext work = new WorkEfficiencyDataContext())
            {
                var current = work.workDescriptionTypesDB.Where(w => w.UserGuidId == GlobalData.GetInstance().UserInfo.GuidId).ToList();

                foreach (var item in current)
                {
                    TypeList.Add(item);
                }
            }
        }

        private WorkDescriptionType GetWorkDesType(string guid)
        {
            using(WorkEfficiencyDataContext work=new WorkEfficiencyDataContext())
            {
                WorkDescriptionType current = work.workDescriptionTypesDB.Where(w => w.GuidId == guid).FirstOrDefault();
                return current;
            }
        }
        #endregion

        #region 命令
        public RelayCommand<string> SelectDesChangedCommand => new RelayCommand<string>((s) =>
        {
            if (!string.IsNullOrEmpty(s))
            {
                using (WorkEfficiencyDataContext work = new WorkEfficiencyDataContext())
                {
                    SelectType = work.workDescriptionTypesDB.Where(w => w.GuidId == s).First();
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
                //workDescription.CreateTime = DateTime.Now;
                //workDescription.WorkDescriptionTypeGuid = SelectType.GuidId;
                //workDescription.FileModelGuidId = fileModel.GuidId;
                using (WorkEfficiencyDataContext work = new WorkEfficiencyDataContext())
                {
                    work.WorkDescriptionDB.Update(workDescription);
                    work.SaveChanges();
                    DialogWindow.Show("修改工作描述成功！", MessageType.Successful, WindowsManager.Windows["MainView"]);
                    WindowsManager.CloseWindow(View as Window);
                }
            }
            else
            {
                DialogWindow.Show("内容不能为空！", MessageType.Error, WindowsManager.Windows["EditFileDesWindow"]);
            }
        });
        #endregion
    }
}

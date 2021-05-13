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
using System.Windows.Controls;
using Microsoft.Win32;
using Newtonsoft.Json;
using YC.WorkEfficiency.DataAccess;
using YC.WorkEfficiency.Models;
using YC.WorkEfficiency.SimpleMVVM;
using YC.WorkEfficiency.Themes;
using YC.WorkEfficiency.ViewModels.Common;

namespace YC.WorkEfficiency.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        public MainViewModel():base()
        {
            //构造函数
            Title = "时间效率管理";
        }

        #region 重写

        public override RelayCommand CloseWindowCommand => new RelayCommand(() =>
        {
            //正常关闭程序时，需反写数据库内的数据，将用户的登陆状态改为未登录
            using (WorkEfficiencyDataContext work = new WorkEfficiencyDataContext())
            {
                var current = GlobalData.GetInstance().UserInfo;
                current.IsLogin = false;
                work.UserModelDB.Update(current);
                work.SaveChanges();
            }
            System.Environment.Exit(0);
            Application.Current.Shutdown();
        });

        public override void InitData()
        {
            LoadModuels();
            MessengerRegister();
        }
        #endregion

        #region 属性
        //初始化窗体时，底部工作信息面板的高度，避免一开始就出现高度，应该由用户自己拖拽
        public double LoadHeight { get; set; } = 0;

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

        #region 模块
        /// <summary>
        /// 左侧信息面板
        /// </summary>
        public FrameworkElement LeftInfoPanelFrame { get; set; }
        /// <summary>
        /// 未完成工作 模块
        /// </summary>
        public FrameworkElement NoFinishedWorkFrame { get; set; }
        /// <summary>
        /// 已完成工作 模块
        /// </summary>
        public FrameworkElement FinishedWorkFrame { get; set; }
        /// <summary>
        /// 底部工作信息面板
        /// </summary>
        public FrameworkElement BottomWorkInfoPanelFrame { get; set; }
        #endregion

        #endregion 属性

        #region 私有方法
        /// <summary>
        /// 获取模块并加载到MainViewModel
        /// </summary>
        private void LoadModuels()
        {
            LoadModulesServices.Instance.LoadModules();
            NoFinishedWorkFrame = LoadModulesServices.Instance.OpenModuleBindingVM("未完成的工作", new WorkingViewModel());
            FinishedWorkFrame = LoadModulesServices.Instance.OpenModuleBindingVM("已完成的工作", new FinishedWorkViewModel());
            LeftInfoPanelFrame = LoadModulesServices.Instance.OpenModuleBindingVM("左侧信息面板", new LeftInfoPanelViewModel());
            BottomWorkInfoPanelFrame = LoadModulesServices.Instance.OpenModuleBindingVM("底部工作信息面板", new WorkInfoPanelViewModel());
        }
        
        #endregion 私有方法

        #region 命令
        
        /// <summary>
        /// 打开设置窗体命令
        /// </summary>
        public RelayCommand OpenSettingWindow => new RelayCommand(() =>
        {
            if (Convert.ToBoolean(WindowsManager.CreateDialogWindowByViewModelResult("SettingView", new SettingViewModel())))
            {
                DialogWindow.Show("保存设置成功！", MessageType.Successful, WindowsManager.Windows["MainView"]);
            }
        });

        #endregion

        #region 消息
        /// <summary>
        /// 汇总消息注册
        /// </summary>
        private void MessengerRegister()
        {

        }

        #endregion

        #region 事件

        #endregion
    }
}
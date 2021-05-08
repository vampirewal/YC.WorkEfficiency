using System;
using System.Windows;
using YC.WorkEfficiency.DataAccess;
using YC.WorkEfficiency.SimpleMVVM;
using YC.WorkEfficiency.ViewModels;

namespace YC.WorkEfficiency.View
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        
        protected override void OnStartup(StartupEventArgs e)
        {
            //base.OnStartup(e);
            //此处调用一下，方便初次打开系统的时候，创建数据库
            using (WorkEfficiencyDataContext dbContext = new WorkEfficiencyDataContext())
            {
                dbContext.Database.EnsureCreated();
            }
                
            //此处使用同一的windowManager进行窗口的创建管理
            //if (WindowsManager.CreateDialogWindowToBool(new LoginView()))
            //{
            //    WindowsManager.CreatWindow(new MainWindow(), ShowMode.Dialog);
            //}
            if (Convert.ToBoolean( WindowsManager.CreateDialogWindowByViewModelResult(new LoginView(),new LoginViewModel())))
            {
                WindowsManager.CreatWindow("MainWindow", ShowMode.Dialog, new MainViewModel());
            }
        }
    }
}

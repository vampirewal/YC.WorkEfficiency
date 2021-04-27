using System.Windows;
using YC.WorkEfficiency.DataAccess;
using YC.WorkEfficiency.SimpleMVVM;

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
            WorkEfficiencyDataContext dbContext = new WorkEfficiencyDataContext();
            dbContext.Database.EnsureCreated();

            if (new LoginView().ShowDialog() == true)
            {
                //上面这个showDialog可以是登陆窗口，登陆成功之后，进入
                //new MainWindow().ShowDialog();
                WindowsManager.CreatWindow(new MainWindow(), ShowMode.Dialog);
            }
        }
    }
}

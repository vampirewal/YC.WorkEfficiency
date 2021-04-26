using System.Windows;
using YC.WorkEfficiency.DataAccess;

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

            
        }
    }
}

using System.Windows;
using YC.WorkEfficiency.View.DataAccess;

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
            FileModelDataContext dbContext = new FileModelDataContext();
            dbContext.Database.EnsureCreated();

            FileAttachmentModelDataContext db2 = new FileAttachmentModelDataContext();
            db2.Database.EnsureCreated();
        }
    }
}

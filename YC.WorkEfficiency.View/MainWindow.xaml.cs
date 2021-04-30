using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using YC.WorkEfficiency.SimpleMVVM;
using YC.WorkEfficiency.ViewModels;

namespace YC.WorkEfficiency.View
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            this.DataContext = new MainViewModel();
            Messenger.Default.Register(this, "ShowSettingWindow", ShowSettingWindow);
        }

        private void ShowSettingWindow()
        {
            if (WindowsManager.CreateDialogWindowToBool(new SettingView()))
            {
                //弹出设置保存成功的提示
            }
            else
            {
                //弹出设置保存失败的提示
            }

        }
    }
}

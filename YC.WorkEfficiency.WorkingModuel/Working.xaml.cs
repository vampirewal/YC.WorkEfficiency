using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Microsoft.EntityFrameworkCore.Metadata;
using YC.WorkEfficiency.Core;
using YC.WorkEfficiency.SimpleMVVM;
using YC.WorkEfficiency.ViewModels;

namespace YC.WorkEfficiency.WorkingModuel
{
    /// <summary>
    /// Working.xaml 的交互逻辑
    /// </summary>
    [Export(typeof(Core.IView))]
    [CustomExportMetadata(1, "未完成的工作", "未完成的工作", "工作", "杨程", "1.0")]
    public partial class Working : UserControl, Core.IView
    {
        public Working()
        {
            InitializeComponent();
            this.DataContext = new WorkingViewModel();
        }

        public object Window => this;

        
    }
}

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
using YC.WorkEfficiency.Core;
using YC.WorkEfficiency.ViewModels;

namespace YC.WorkEfficiency.FinishedWorkModuel
{
    /// <summary>
    /// FinishedWorkView.xaml 的交互逻辑
    /// </summary>
    [Export(typeof(Core.IView))]
    [CustomExportMetadata(1, "已完成的工作", "已完成的工作", "工作", "杨程", "1.0")]
    public partial class FinishedWorkView : UserControl, Core.IView
    {
        public FinishedWorkView()
        {
            InitializeComponent();
        }

        public object Window => this;
    }
}

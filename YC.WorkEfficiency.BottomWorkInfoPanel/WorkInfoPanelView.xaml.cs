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

namespace YC.WorkEfficiency.BottomWorkInfoPanel
{
    /// <summary>
    /// WorkInfoPanelView.xaml 的交互逻辑
    /// </summary>
    [Export(typeof(Core.IView))]
    [CustomExportMetadata(1, "底部工作信息面板", "底部工作信息面板", "底部工作信息面板", "杨程", "1.0")]
    public partial class WorkInfoPanelView : UserControl,IView
    {
        public WorkInfoPanelView()
        {
            InitializeComponent();
        }

        public object Window => this;
    }
}

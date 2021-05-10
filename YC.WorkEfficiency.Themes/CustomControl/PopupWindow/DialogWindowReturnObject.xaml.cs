using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace YC.WorkEfficiency.Themes
{
    /// <summary>
    /// DialogWindowReturnObject.xaml 的交互逻辑
    /// </summary>
    public partial class DialogWindowReturnObject : Window
    {
        public object ToResult()
        {
            return true;
        }
        public DialogWindowReturnObject(string Msg,string Title)
        {
            InitializeComponent();
            this.Msg.Text = Msg;
            this.DialogTitle.Text = Title;
        }

        private void QueDingBtn_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;

            this.Close();
        }

        private void CloseBtn_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;

            this.Close();
        }
    }


}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace YC.WorkEfficiency.Themes
{
    /// <summary>
    /// DialogWindows.xaml 的交互逻辑
    /// </summary>
    public partial class DialogWindows : Window, INotifyPropertyChanged
    {
        #region Notify
        public event PropertyChangedEventHandler PropertyChanged;
        public void DoNotify([CallerMemberName] string propName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
        } 
        #endregion

        /// <summary>
        /// 在窗体内弹出提示框
        /// </summary>
        /// <param name="Msg">需要发出的消息文本</param>
        /// <param name="window">owenr窗体</param>
        public DialogWindows(string Msg,Window window,MessageType messageType,bool iskeepOpen=false)
        {
            InitializeComponent();
            this.DataContext = this;
            this.Owner = window;
            isKeepOpen = iskeepOpen;
            switch (messageType)
            {
                case MessageType.Information:
                    WindowBackground = "#f2eada";
                    break;
                case MessageType.Error:
                    WindowBackground = "#aa2116";
                    break;
                case MessageType.Successful:
                    WindowBackground = "#7fb80e";
                    break;
                default:
                    break;
            }
            MessageText.Text = Msg;

            this.Loaded += DialogWindows_Loaded;
        }

        private void DialogWindows_Loaded(object sender, RoutedEventArgs e)
        {
            
            if (isKeepOpen==false)
            {
                DoubleAnimation animation1 = new DoubleAnimation()
                {
                    To = this.Owner.Top,
                    BeginTime = TimeSpan.FromSeconds(2),
                    Duration = TimeSpan.FromSeconds(1),

                };
                animation1.Completed += Animation1_Completed;
                this.BeginAnimation(Window.TopProperty, animation1);

                DoubleAnimation animation_Op = new DoubleAnimation
                {
                    To = 0,
                    BeginTime = TimeSpan.FromSeconds(2),
                    Duration = TimeSpan.FromSeconds(1)
                };
                this.BeginAnimation(Window.OpacityProperty, animation_Op);
            }
            
        }

        private void Animation1_Completed(object sender, EventArgs e)
        {
            this.Close();
        }

        public bool isKeepOpen { get; set; }

        

        private string _WindowBackground;

        public string WindowBackground
        {
            get { return _WindowBackground; }
            set { _WindowBackground = value;this.DoNotify(); }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
    public enum MessageType
    {
        Information,
        Error,
        Successful
    }
}

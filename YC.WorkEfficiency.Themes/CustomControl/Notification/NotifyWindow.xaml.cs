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
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace YC.WorkEfficiency.Themes
{
    /// <summary>
    /// NotifyWindow.xaml 的交互逻辑
    /// </summary>
    public partial class NotifyWindow : Window
    {
        public static List<NotifyWindow> _dialogs = new List<NotifyWindow>();

        public double TopFrom { get; set; }
        private int NotifyTimeSpan;
        /// <summary>
        /// 右下角弹窗提示
        /// </summary>
        /// <param name="Context">提示文本</param>
        /// <param name="_NotifyTimeSpan">持续时间</param>
        public NotifyWindow(string Context,int _NotifyTimeSpan)
        {
            InitializeComponent();
            ShowText.Text= Context;
            NotifyTimeSpan = _NotifyTimeSpan;
            this.Loaded += NotifyWindow_Loaded;
            this.Closed += NotifyWindow_Closed;
            this.TopFrom = GetTopFrom();
            _dialogs.Add(this);
        }

        private void NotifyWindow_Loaded(object sender, RoutedEventArgs e)
        {
            NotifyWindow self = sender as NotifyWindow;
            if (self != null)
            {
                self.UpdateLayout();
                

                double right = System.Windows.SystemParameters.WorkArea.Right;//工作区最右边的值
                self.Top = self.TopFrom - self.ActualHeight;
                DoubleAnimation animation = new DoubleAnimation();
                animation.Duration = new Duration(TimeSpan.FromMilliseconds(NotifyTimeSpan));//NotifyTimeSpan是自己定义的一个int型变量，用来设置动画的持续时间
                animation.From = right;
                animation.To = right - self.ActualWidth;//设定通知从右往左弹出
                self.BeginAnimation(Window.LeftProperty, animation);//设定动画应用于窗体的Left属性

                Task.Factory.StartNew(delegate
                {
                    int seconds = 5;//通知持续5s后消失
                    System.Threading.Thread.Sleep(TimeSpan.FromSeconds(seconds));
                    //Invoke到主进程中去执行
                    Application.Current.Dispatcher.Invoke(()=>
                    {
                        animation = new DoubleAnimation();
                        animation.Duration = new Duration(TimeSpan.FromMilliseconds(NotifyTimeSpan));
                        animation.Completed += (s, a) => { self.Close(); };//动画执行完毕，关闭当前窗体
                        animation.From = right - self.ActualWidth;
                        animation.To = right;//通知从左往右收回
                        self.BeginAnimation(Window.LeftProperty, animation);
                    });
                });
            }
        }

        private void NotifyWindow_Closed(object sender, EventArgs e)
        {
            var closedDialog = sender as NotifyWindow;
            _dialogs.Remove(closedDialog);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            double right = System.Windows.SystemParameters.WorkArea.Right;
            DoubleAnimation animation = new DoubleAnimation();
            animation.Duration = new Duration(TimeSpan.FromMilliseconds(NotifyTimeSpan));

            animation.Completed += (s, a) => { this.Close(); };
            animation.From = right - this.ActualWidth;
            animation.To = right;
            this.BeginAnimation(Window.LeftProperty, animation);
        }

        double GetTopFrom()
        {
            //屏幕的高度-底部TaskBar的高度。
            double topFrom = System.Windows.SystemParameters.WorkArea.Bottom - 10;

            if (topFrom <= 0)
                topFrom = System.Windows.SystemParameters.WorkArea.Bottom - 10;

            return topFrom;
        }
    }
}

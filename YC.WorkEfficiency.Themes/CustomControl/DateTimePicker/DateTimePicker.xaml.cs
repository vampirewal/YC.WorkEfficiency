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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace YC.WorkEfficiency.Themes
{
    /// <summary>
    /// DateTimePicker.xaml 的交互逻辑
    /// </summary>
    public partial class DateTimePicker : UserControl
    {
        public DateTimePicker()
        {
            InitializeComponent();
            this.initParameters();
            this.textbox_year.Background = Brushes.White;
            this.textbox_mouth.Background = Brushes.White;
            this.textbox_day.Background = Brushes.White;
            this.textbox_minute.Background = Brushes.White;
            this.textbox_second.Background = Brushes.White;
            this.textbox_hour.Background = Brushes.White;
        }



        public string SelectedDateTime
        {
            get { return (string)GetValue(SelectedDateTimeProperty); }
            set { SetValue(SelectedDateTimeProperty, value); }
        }

        // Using a DependencyProperty as the backing store for SelectedDateTime.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SelectedDateTimeProperty =
            DependencyProperty.Register("SelectedDateTime", typeof(string), typeof(DateTimePicker), new PropertyMetadata(0));



        #region 业务处理函数
        /// <summary>
        /// 更改选中状态
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void textbox_hour_SelectionChanged(object sender, RoutedEventArgs e)
        {
            TextBox tb = sender as TextBox;
            if (tb != null)
            {
                switch (tb.Name)
                {
                    case "textbox_year":
                        tb.Background = Brushes.Gray;
                        this.textbox_mouth.Background = Brushes.White;
                        this.textbox_day.Background = Brushes.White;
                        this.textbox_hour.Background = Brushes.White;
                        this.textbox_minute.Background = Brushes.White;
                        this.textbox_second.Background = Brushes.White;
                        break;
                    case "textbox_mouth":
                        tb.Background = Brushes.Gray;
                        this.textbox_year.Background = Brushes.White;
                        this.textbox_day.Background = Brushes.White;
                        this.textbox_hour.Background = Brushes.White;
                        this.textbox_minute.Background = Brushes.White;
                        this.textbox_second.Background = Brushes.White;
                        break;
                    case "textbox_day":
                        tb.Background = Brushes.Gray;
                        this.textbox_year.Background = Brushes.White;
                        this.textbox_mouth.Background = Brushes.White;
                        this.textbox_hour.Background = Brushes.White;
                        this.textbox_minute.Background = Brushes.White;
                        this.textbox_second.Background = Brushes.White;
                        break;
                    case "textbox_hour":
                        tb.Background = Brushes.Gray;
                        this.textbox_year.Background = Brushes.White;
                        this.textbox_mouth.Background = Brushes.White;
                        this.textbox_day.Background = Brushes.White;
                        this.textbox_minute.Background = Brushes.White;
                        this.textbox_second.Background = Brushes.White;
                        break;
                    case "textbox_minute":
                        tb.Background = Brushes.Gray;
                        this.textbox_year.Background = Brushes.White;
                        this.textbox_mouth.Background = Brushes.White;
                        this.textbox_day.Background = Brushes.White;
                        this.textbox_hour.Background = Brushes.White;
                        this.textbox_second.Background = Brushes.White;
                        break;
                    case "textbox_second":
                        tb.Background = Brushes.Gray;
                        this.textbox_year.Background = Brushes.White;
                        this.textbox_mouth.Background = Brushes.White;
                        this.textbox_day.Background = Brushes.White;
                        this.textbox_hour.Background = Brushes.White;
                        this.textbox_minute.Background = Brushes.White;
                        break;
                }
            }
        }

        /// <summary>
        /// 向上加时
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button_up_Click(object sender, RoutedEventArgs e)
        {
            if (this.textbox_year.Background==Brushes.Gray)
            {
                int temp = System.Int32.Parse(this.textbox_year.Text);
                temp++;
                if (temp==2099)
                {
                    temp = DateTime.Now.Year;
                }
                this.textbox_year.Text = temp.ToString();
            }else if (this.textbox_mouth.Background == Brushes.Gray)
            {
                int temp = System.Int32.Parse(this.textbox_hour.Text);
                temp++;
                if (temp > 12)
                {
                    temp = 1;
                }
                this.textbox_mouth.Text = temp.ToString();
            }else if(this.textbox_day.Background == Brushes.Gray)
            {
                int currentdayCount = DateTime.DaysInMonth(System.Int32.Parse(this.textbox_year.Text), System.Int32.Parse(this.textbox_hour.Text));
                int temp = System.Int32.Parse(this.textbox_day.Text);
                if (temp> currentdayCount)
                {
                    temp = 1;
                }
                this.textbox_day.Text = temp.ToString();
            }else if (this.textbox_hour.Background == Brushes.Gray)
            {
                int temp = System.Int32.Parse(this.textbox_hour.Text);
                temp++;
                if (temp > 24)
                {
                    temp = 0;
                }
                this.textbox_hour.Text = temp.ToString();
            }
            else if (this.textbox_minute.Background == Brushes.Gray)
            {
                int temp = System.Int32.Parse(this.textbox_minute.Text);
                temp++;
                if (temp > 60)
                {
                    temp = 0;
                }
                this.textbox_minute.Text = temp.ToString();
            }
            else if (this.textbox_second.Background == Brushes.Gray)
            {
                int temp = System.Int32.Parse(this.textbox_second.Text);
                temp++;
                if (temp > 60)
                {
                    temp = 0;
                }
                this.textbox_second.Text = temp.ToString();
            }
        }

        /// <summary>
        /// 向下减时
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button_down_Click(object sender, RoutedEventArgs e)
        {
            if (this.textbox_hour.Background == Brushes.Gray)
            {
                int temp = System.Int32.Parse(this.textbox_hour.Text);
                temp--;
                if (temp < 0)
                {
                    temp = 24;
                }
                this.textbox_hour.Text = temp.ToString();
            }
            else if (this.textbox_minute.Background == Brushes.Gray)
            {
                int temp = System.Int32.Parse(this.textbox_minute.Text);
                temp--;
                if (temp < 0)
                {
                    temp = 60;
                }
                this.textbox_minute.Text = temp.ToString();
            }
            else if (this.textbox_second.Background == Brushes.Gray)
            {
                int temp = System.Int32.Parse(this.textbox_second.Text);
                temp--;
                if (temp < 0)
                {
                    temp = 60;
                }
                this.textbox_second.Text = temp.ToString();
            }
        }
        /// <summary>
        /// 初始化参数设置
        /// </summary>
        private void initParameters()
        {
            string strt = System.DateTime.Now.ToString("yyyy:MM:dd:HH:mm:ss");
            this.textbox_year.Text=strt.Split(':')[0];
            this.textbox_mouth.Text=strt.Split(':')[1];
            this.textbox_day.Text=strt.Split(':')[2];
            this.textbox_hour.Text = strt.Split(':')[3];
            this.textbox_minute.Text = strt.Split(':')[4];
            this.textbox_second.Text = strt.Split(':')[5];
        }

        /// <summary>
        /// 数字标准化处理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void numtextboxchanged(object sender, TextChangedEventArgs e)
        {
            TextBox tb = sender as TextBox;
            if (tb != null)
            {
                if ((this.isNum(tb.Text) == false) || (tb.Text.Length > 2))
                {
                    tb.Text = "0";
                    MessageBox.Show("请输入正确的时间！", "警告！");
                    return;
                }
            }
        }

        /// <summary>
        /// 判断是否为数字，是--->true，否--->false
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        private bool isNum(string str)
        {
            bool ret = true;
            foreach (char c in str)
            {
                if ((c < 48) || (c > 57))
                {
                    return false;
                }
            }

            return ret;
        }
 
        #endregion
    }
}

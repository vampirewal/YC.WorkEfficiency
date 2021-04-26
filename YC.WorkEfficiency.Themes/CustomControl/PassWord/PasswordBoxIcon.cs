#region << 文 件 说 明 >>
/*----------------------------------------------------------------
// 文件名称：PasswordBoxIcon
// 创 建 者：杨程
// 创建时间：2021/3/16 15:54:12
// 文件版本：V1.0.0
// ===============================================================
// 功能描述：重新制作PassWord控件
//		
//
//----------------------------------------------------------------*/
#endregion

using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace YC.WorkEfficiency.Themes
{
    public class PasswordBoxIcon: Control
    {
        private PasswordBox _pdBox;
        private TextBlock _tbWatermark;
        //private string initPsd;

        static PasswordBoxIcon()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(PasswordBoxIcon), new FrameworkPropertyMetadata(typeof(PasswordBoxIcon)));
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            this.GotFocus += PasswordBoxIcon_GotFocus;
            _pdBox = this.Template.FindName("PdBox", this) as PasswordBox;
            _tbWatermark = this.Template.FindName("TbWatermark", this) as TextBlock;
            if (_pdBox != null)
            {
                _pdBox.PasswordChanged += _pdBox_PasswordChanged;
                if (!string.IsNullOrEmpty(Password))
                {
                    _tbWatermark.Visibility = Visibility.Collapsed;
                    _pdBox.Password = Password;
                }
            }
        }

        private void PasswordBoxIcon_GotFocus(object sender, RoutedEventArgs e)
        {
            _pdBox.Focus();
            _pdBox.SelectAll();
        }

        private void _pdBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (_tbWatermark != null)
                _tbWatermark.Visibility = string.IsNullOrEmpty(_pdBox.Password) ? Visibility.Visible : Visibility.Collapsed;

            Password = _pdBox.Password;
        }

        public void SetPassword(string pdStr)
        {
            if (_tbWatermark != null && _pdBox != null)
            {
                _tbWatermark.Visibility = string.IsNullOrEmpty(pdStr) ? Visibility.Visible : Visibility.Collapsed;
                _pdBox.Password = pdStr;
            }
        }

        public void SetInitPsd(string pdStr)
        {
            if (_pdBox != null && _pdBox.Password != pdStr)
                _pdBox.Password = pdStr;
        }

        #region 当前输入的密码

        public string Password
        {
            get { return (string)GetValue(PasswordProperty); }
            set { SetValue(PasswordProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Password.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty PasswordProperty =
            DependencyProperty.Register("Password", typeof(string), typeof(PasswordBoxIcon), new PropertyMetadata(null, PasswordCallback));

        private static void PasswordCallback(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var pd = d as PasswordBoxIcon;
            if (pd != null && e.NewValue != null)
                pd.SetInitPsd(e.NewValue.ToString());
        }

        #endregion

        #region Icon

        #region 字体图标
        public string IconStr
        {
            get { return (string)GetValue(IconStrProperty); }
            set { SetValue(IconStrProperty, value); }
        }

        // Using a DependencyProperty as the backing store for TbIcon.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IconStrProperty =
            DependencyProperty.Register("IconStr", typeof(string), typeof(PasswordBoxIcon), new PropertyMetadata(null));
        #endregion

        #region 字体图标大小
        public int IconFontSize
        {
            get { return (int)GetValue(IconFontSizeProperty); }
            set { SetValue(IconFontSizeProperty, value); }
        }

        // Using a DependencyProperty as the backing store for IconFontSize.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IconFontSizeProperty =
            DependencyProperty.Register("IconFontSize", typeof(int), typeof(PasswordBoxIcon), new PropertyMetadata(0));
        #endregion

        #endregion

        #region 水印

        #region 水印文字
        public string Watermark
        {
            get { return (string)GetValue(WatermarkProperty); }
            set { SetValue(WatermarkProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Watermark.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty WatermarkProperty =
            DependencyProperty.Register("Watermark", typeof(string), typeof(PasswordBoxIcon), new PropertyMetadata(null));
        #endregion

        #region 水印文字的颜色
        public Brush WatermarkColor
        {
            get { return (Brush)GetValue(WatermarkColorProperty); }
            set { SetValue(WatermarkColorProperty, value); }
        }

        // Using a DependencyProperty as the backing store for WatermarkColor.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty WatermarkColorProperty =
            DependencyProperty.Register("WatermarkColor", typeof(Brush), typeof(PasswordBoxIcon), new PropertyMetadata(Brushes.Black)); 
        #endregion


        #endregion
    }
}

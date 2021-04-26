#region<<文 件 说 明>>
/*----------------------------------------------------------------
// 文件名称：TextBoxIcon
// 创 建 者：杨程
// 创建时间：2021/3/5 星期五 16:04:13
// 文件版本：V1.0.0
// ===============================================================
// 功能描述：
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
    public class TextBoxIcon:TextBox
    {
        private TextBlock _tbWatermark;
        static TextBoxIcon()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(TextBoxIcon), new FrameworkPropertyMetadata(typeof(TextBoxIcon)));
        }
        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            this.GotFocus += TextBoxIcon_GotFocus;
            this.TextChanged += TextBoxIcon_TextChanged;
            _tbWatermark = this.Template.FindName("TbWaterText", this) as TextBlock;
            if (_tbWatermark != null)
                _tbWatermark.Visibility = string.IsNullOrEmpty(Text) ? Visibility.Visible : Visibility.Collapsed;
        }
        private void TextBoxIcon_GotFocus(object sender, RoutedEventArgs e)
        {
            SelectAll();
        }

        private void TextBoxIcon_TextChanged(object sender, TextChangedEventArgs e)
        {
            _tbWatermark.Visibility = string.IsNullOrEmpty(Text) ? Visibility.Visible : Visibility.Collapsed;
        }

        #region 依赖属性

        #region 使用字体图标代码的依赖属性
        /// <summary>
        /// 使用字体图标代码的依赖属性
        /// </summary>
        public string IconStr
        {
            get { return (string)GetValue(IconStrProperty); }
            set { SetValue(IconStrProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IconStrProperty =
            DependencyProperty.Register("IconStr", typeof(string), typeof(TextBoxIcon), new PropertyMetadata(null));
        #endregion

        #region 水印文字的依赖属性
        /// <summary>
        /// 水印文字的依赖属性
        /// </summary>
        public string WaterText
        {
            get { return (string)GetValue(WaterTextProperty); }
            set { SetValue(WaterTextProperty, value); }
        }

        // Using a DependencyProperty as the backing store for WaterText.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty WaterTextProperty =
            DependencyProperty.Register("WaterText", typeof(string), typeof(TextBoxIcon), new PropertyMetadata(null));
        #endregion

        #region 水印文字的FontSize
        /// <summary>
        /// 水印文字的字体大小
        /// </summary>
        public int WaterTextFontSize
        {
            get { return (int)GetValue(WaterTextFontSizeProperty); }
            set { SetValue(WaterTextFontSizeProperty, value); }
        }

        // Using a DependencyProperty as the backing store for WaterTextFontSize.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty WaterTextFontSizeProperty =
            DependencyProperty.Register("WaterTextFontSize", typeof(int), typeof(TextBoxIcon), new PropertyMetadata(12));


        #endregion

        #region 水印文字的前景色
        /// <summary>
        /// 水印文字的前景色
        /// </summary>
        public Brush WaterTextForeground
        {
            get { return (Brush)GetValue(WaterTextForegroundProperty); }
            set { SetValue(WaterTextForegroundProperty, value); }
        }

        // Using a DependencyProperty as the backing store for TitleForeground.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty WaterTextForegroundProperty =
            DependencyProperty.Register("WaterTextForeground", typeof(Brush), typeof(TextBoxIcon), new PropertyMetadata(Brushes.Black));
        #endregion

        #region 字体图标的大小
        public int IconFontSize
        {
            get { return (int)GetValue(IconWidthProperty); }
            set { SetValue(IconWidthProperty, value); }
        }

        public static readonly DependencyProperty IconWidthProperty =
            DependencyProperty.Register("IconFontSize", typeof(int), typeof(TextBoxIcon), new PropertyMetadata(12));

        
        #endregion

        #endregion


    }
}
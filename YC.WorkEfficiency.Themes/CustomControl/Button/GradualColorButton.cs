#region << 文 件 说 明 >>
/*----------------------------------------------------------------
// 文件名称：GradualColorButton
// 创 建 者：杨程
// 创建时间：2021/3/19 10:52:19
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
    public class GradualColorButton:Button
    {
        public GradualColorButton()
        {
            //构造函数
            
        }

        #region 依赖属性

        #region 渐变前颜色
        public Color OldColor
        {
            get { return (Color)GetValue(OldColorProperty); }
            set { SetValue(OldColorProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty OldColorProperty =
            DependencyProperty.Register("OldColor", typeof(Color), typeof(GradualColorButton), new PropertyMetadata(Color.FromRgb(0,0,0)));
        #endregion

        #region 渐变后颜色
        public Color NewColor
        {
            get { return (Color)GetValue(NewColorProperty); }
            set { SetValue(NewColorProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty NewColorProperty =
            DependencyProperty.Register("NewColor", typeof(Color), typeof(GradualColorButton), new PropertyMetadata(Color.FromRgb(0, 0, 0)));
        #endregion

        #region 按钮圆角
        public CornerRadius BtnCornerRadius
        {
            get { return (CornerRadius)GetValue(BtnCornerRadiusProperty); }
            set { SetValue(BtnCornerRadiusProperty, value); }
        }

        // Using a DependencyProperty as the backing store for BtnCornerRadius.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty BtnCornerRadiusProperty =
            DependencyProperty.Register("BtnCornerRadius", typeof(CornerRadius), typeof(GradualColorButton), new PropertyMetadata(new CornerRadius(0)));
        #endregion

        #endregion

        #region 公共方法

        #endregion

        #region 私有方法

        #endregion

        #region 命令

        #endregion
    }
}

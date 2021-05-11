#region << 文 件 说 明 >>
/*----------------------------------------------------------------
// 文件名称：SelectColorViewModel
// 创 建 者：杨程
// 创建时间：2021/5/11 12:55:43
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
using System.Windows.Media;
using YC.WorkEfficiency.SimpleMVVM;

namespace YC.WorkEfficiency.ViewModels
{
    public class SelectColorViewModel : ViewModelBase
    {
        

        public SelectColorViewModel()
        {
            //构造函数
            Title = "选择颜色";
        }


        public override object GetResult()
        {
            return SelectColor;
        }

        public override void InitData()
        {
            
        }
        //public override RelayCommand CloseWindowCommand => new RelayCommand(() =>
        //{

        //    var aa = selectColor;
        //    WindowsManager.CloseWindow(View as Window);
        //});

        #region 属性
        private string SelectColor;
        #endregion

        #region 公共方法

        #endregion

        #region 私有方法
        private void GetColorList()
        {
            List<ColorInfo> colorList = new List<ColorInfo>()
            {
                new ColorInfo(){ colorName="红色",colorValue="#FF0000"},
                new ColorInfo(){ colorName="深紫色",colorValue="#871F78"},
                new ColorInfo(){ colorName="褐红色",colorValue="#8E236B"},
                new ColorInfo(){ colorName="石英色",colorValue="#D9D9F3"},
                new ColorInfo(){ colorName="绿色",colorValue="#00FF00"},
                new ColorInfo(){ colorName="深石板蓝",colorValue="#6B238E"},
                new ColorInfo(){ colorName="中海蓝色",colorValue="#32CD99"},
                new ColorInfo(){ colorName="艳蓝色",colorValue="#5959AB"},
                new ColorInfo(){ colorName="蓝色",colorValue="#0000FF"},
                new ColorInfo(){ colorName="深铅灰色",colorValue="#2F4F4F"},
                new ColorInfo(){ colorName="中蓝色",colorValue="#3232CD"},
                new ColorInfo(){ colorName="鲑鱼色",colorValue="#6F4242"},
                new ColorInfo(){ colorName="牡丹红",colorValue="#FF00FF"},
                new ColorInfo(){ colorName="深棕褐色",colorValue="#97694F"},
                new ColorInfo(){ colorName="中森林绿",colorValue="#6B8E23"},
                new ColorInfo(){ colorName="猩红色",colorValue="#BC1717"},
                new ColorInfo(){ colorName="青色",colorValue="#00FFFF"},
                new ColorInfo(){ colorName="深绿松石色",colorValue="#7093DB"},
                new ColorInfo(){ colorName="中鲜黄色",colorValue="#EAEAAE"},
                new ColorInfo(){ colorName="海绿色",colorValue="#238E68"},
                new ColorInfo(){ colorName="黄色",colorValue="#FFFF00"},
                new ColorInfo(){ colorName="暗木色",colorValue="#855E42"},
                new ColorInfo(){ colorName="中兰花色",colorValue="#9370DB"},
                new ColorInfo(){ colorName="半甜巧克力色",colorValue="#6B4226"},
                new ColorInfo(){ colorName="黑色",colorValue="#000000"},
                new ColorInfo(){ colorName="淡灰色",colorValue="#545454"},
                new ColorInfo(){ colorName="中海绿色",colorValue="#426F42"},
                new ColorInfo(){ colorName="赭色",colorValue="#8E6B23"},
                new ColorInfo(){ colorName="海蓝",colorValue="#70DB93"},
                new ColorInfo(){ colorName="土灰玫瑰红色",colorValue="#856363"},
                new ColorInfo(){ colorName="中石板蓝色",colorValue="#7F00FF"},
                new ColorInfo(){ colorName="银色",colorValue="#E6E8FA"},
                new ColorInfo(){ colorName="巧克力色",colorValue="#5C3317"},
                new ColorInfo(){ colorName="长石色",colorValue="#D19275"},
                new ColorInfo(){ colorName="中春绿色",colorValue="#7FFF00"},
                new ColorInfo(){ colorName="天蓝",colorValue="#3299CC"},
                new ColorInfo(){ colorName="蓝紫色",colorValue="#9F5F9F"},
                new ColorInfo(){ colorName="火砖色",colorValue="#8E2323"},
                new ColorInfo(){ colorName="中绿松石色",colorValue="#70DBDB"},
                new ColorInfo(){ colorName="石板蓝",colorValue="#007FFF"},
                new ColorInfo(){ colorName="黄铜色",colorValue="#B5A642"},
                new ColorInfo(){ colorName="森林绿",colorValue="#238E23"},
                new ColorInfo(){ colorName="中紫红色",colorValue="#DB7093"},
                new ColorInfo(){ colorName="艳粉红色",colorValue="#FF1CAE"},
                new ColorInfo(){ colorName="亮金色",colorValue="#D9D919"},
                new ColorInfo(){ colorName="金色",colorValue="#CD7F32"},
                new ColorInfo(){ colorName="中木色",colorValue="#A68064"},
                new ColorInfo(){ colorName="春绿色",colorValue="#00FF7F"},
                new ColorInfo(){ colorName="棕色",colorValue="#A67D3D"},
                new ColorInfo(){ colorName="鲜黄色",colorValue="#DBDB70"},
                new ColorInfo(){ colorName="深藏青色",colorValue="#2F2F4F"},
                new ColorInfo(){ colorName="钢蓝色",colorValue="#236B8E"},
                new ColorInfo(){ colorName="青铜色",colorValue="#8C7853"},
                new ColorInfo(){ colorName="灰色",colorValue="#C0C0C0"},
                new ColorInfo(){ colorName="海军蓝",colorValue="#23238E"},
                new ColorInfo(){ colorName="亮天蓝色",colorValue="#38B0DE"},
                new ColorInfo(){ colorName="亮天蓝色",colorValue="#38B0DE"},
            };
            /*
             *       

      

       

2号青铜色 #A67D3D 铜绿色 #527F76 霓虹篮 #4D4DFF 棕褐色 #DB9370

士官服蓝色 #5F9F9F 青黄色 #93DB70 霓虹粉红 #FF6EC7 紫红色 #D8BFD8

冷铜色 #D98719 猎人绿 #215E21 新深藏青色 #00009C 石板蓝色 #ADEAEA

铜色#B87333 印度红 #4E2F2F 新棕褐色 #EBC79E 浓深棕色 #5C4033

珊瑚红 #FF7F00 土黄色 #9F9F5F 暗金黄色 #CFB53B 淡浅灰色 #CDCDCD

紫蓝色 #42426F 浅蓝色 #C0D9D9 橙色 #FF7F00 紫罗兰色 #4F2F4F

深棕#5C4033 浅灰色 #A8A8A8 橙红色 #FF2400 紫罗兰红色 #CC3299

深绿#2F4F2F 浅钢蓝色 #8F8FBD 淡紫色 #DB70DB 麦黄色 #D8D8BF

深铜绿色 #4A766E 浅木色 #E9C2A6 浅绿色 #8FBC8F 黄绿色 #99CC32

深橄榄绿 #4F4F2F 石灰绿色 #32CD32 粉红色 #BC8F8F

深兰花色 #9932CD 桔黄色 #E47833 李子色 #EAADEA
             */
        }

        class ColorInfo
        {
            public string colorName { get; set; }
            public string colorValue { get; set; }
        }
        #endregion

        #region 命令
        public RelayCommand<string> SelectColorCommand => new RelayCommand<string>((s) =>
        {
            SelectColor = s;
        });
        #endregion
    }
}

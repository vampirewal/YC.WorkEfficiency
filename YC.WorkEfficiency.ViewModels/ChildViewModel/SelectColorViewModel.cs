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
using System.Linq;
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
            GetColorList();
        }
        //public override RelayCommand CloseWindowCommand => new RelayCommand(() =>
        //{

        //    var aa = selectColor;
        //    WindowsManager.CloseWindow(View as Window);
        //});

        #region 属性
        private string SelectColor;

        public List<ColorInfo> colorList { get; set; }
        #endregion

        #region 公共方法

        #endregion

        #region 私有方法
        private void GetColorList()
        {
             colorList = new List<ColorInfo>()
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
                new ColorInfo(){ colorName="2号青铜色",colorValue="#A67D3D"},
                new ColorInfo(){ colorName="铜绿色",colorValue="#527F76"},
                new ColorInfo(){ colorName="霓虹篮",colorValue="#4D4DFF"},
                new ColorInfo(){ colorName="棕褐色",colorValue="#DB9370"},
                new ColorInfo(){ colorName="士官服蓝色",colorValue="#5F9F9F"},
                new ColorInfo(){ colorName="青黄色",colorValue="#93DB70"},
                new ColorInfo(){ colorName="霓虹粉红",colorValue="#FF6EC7"},
                new ColorInfo(){ colorName="紫红色",colorValue="#D8BFD8"},
                new ColorInfo(){ colorName="冷铜色",colorValue="#D98719"},
                new ColorInfo(){ colorName="猎人绿",colorValue="#215E21"},
                new ColorInfo(){ colorName="新深藏青色",colorValue="#00009C"},
                new ColorInfo(){ colorName="石板蓝色",colorValue="#ADEAEA"},
                new ColorInfo(){ colorName="铜色",colorValue="#B87333"},
                new ColorInfo(){ colorName="印度红",colorValue="#4E2F2F"},
                new ColorInfo(){ colorName="新棕褐色",colorValue="#EBC79E"},
                new ColorInfo(){ colorName="浓深棕色",colorValue="#5C4033"},
                new ColorInfo(){ colorName="珊瑚红",colorValue="#FF7F00"},
                new ColorInfo(){ colorName="土黄色",colorValue="#9F9F5F"},
                new ColorInfo(){ colorName="暗金黄色",colorValue="#CFB53B"},
                new ColorInfo(){ colorName="淡浅灰色",colorValue="#CDCDCD"},
                new ColorInfo(){ colorName="紫蓝色",colorValue="#42426F"},
                new ColorInfo(){ colorName="浅蓝色",colorValue="#C0D9D9"},
                new ColorInfo(){ colorName="橙色",colorValue="#FF7F00"},
                new ColorInfo(){ colorName="紫罗兰色",colorValue="#4F2F4F"},
                new ColorInfo(){ colorName="深棕",colorValue="#5C4033"},
                new ColorInfo(){ colorName="浅灰色",colorValue="#A8A8A8"},
                new ColorInfo(){ colorName="橙红色",colorValue="#FF2400"},
                new ColorInfo(){ colorName="紫罗兰红色",colorValue="#CC3299"},
                new ColorInfo(){ colorName="深绿",colorValue="#2F4F2F"},
                new ColorInfo(){ colorName="浅钢蓝色",colorValue="#8F8FBD"},
                new ColorInfo(){ colorName="淡紫色",colorValue="#DB70DB"},
                new ColorInfo(){ colorName="麦黄色",colorValue="#D8D8BF"},
                new ColorInfo(){ colorName="深铜绿色",colorValue="#4A766E"},
                new ColorInfo(){ colorName="浅木色",colorValue="#E9C2A6"},
                new ColorInfo(){ colorName="浅绿色",colorValue="#8FBC8F"},
                new ColorInfo(){ colorName="黄绿色",colorValue="#99CC32"},
                new ColorInfo(){ colorName="深橄榄绿",colorValue="#4F4F2F"},
                new ColorInfo(){ colorName="石灰绿色",colorValue="#32CD32"},
                new ColorInfo(){ colorName="粉红色",colorValue="#BC8F8F"},
                new ColorInfo(){ colorName="深兰花色",colorValue="#9932CD"},
                new ColorInfo(){ colorName="桔黄色",colorValue="#E47833"},
                new ColorInfo(){ colorName="李子色",colorValue="#EAADEA"}
            };
        }

        public class ColorInfo
        {
            public string colorName { get; set; }
            public string colorValue { get; set; }
        }
        #endregion

        #region 命令
        public RelayCommand<string> SelectColorCommand => new RelayCommand<string>((s) =>
        {
            SelectColor = s;
            WindowsManager.CloseWindow(View as Window);
        });

        public RelayCommand<string> CustomColorCommand => new RelayCommand<string>((s) =>
          {
              if (!string.IsNullOrEmpty(s))
              {
                  SelectColor = s;
                  WindowsManager.CloseWindow(View as Window);
              }
          });
        #endregion

          
    }
}

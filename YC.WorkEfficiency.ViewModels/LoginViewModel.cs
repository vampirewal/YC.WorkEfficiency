#region << 文 件 说 明 >>
/*----------------------------------------------------------------
// 文件名称：LoginViewModel
// 创 建 者：杨程
// 创建时间：2021/4/26 11:46:54
// 文件版本：V1.0.0
// ===============================================================
// 功能描述：
//		
//
//----------------------------------------------------------------*/
#endregion

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Windows;
using YC.WorkEfficiency.DataAccess;
using YC.WorkEfficiency.SimpleMVVM;

namespace YC.WorkEfficiency.ViewModels
{
    public class LoginViewModel:ViewModelBase
    {
        public LoginViewModel()
        {
            //构造函数
            Title = "登陆";
            InitData();
        }

        #region 属性
        public BaseCommand baseCommand { get; set; } = new BaseCommand();

        public ObservableCollection<string> HaveLoginUserName { get; set; }
        #endregion

        #region 公共方法

        #endregion

        #region 私有方法
        private void InitData()
        {
            
            using (WorkEfficiencyDataContext work =new WorkEfficiencyDataContext())
            {
                var current= work.UserModelDB.Select(s => s.UserName);
                HaveLoginUserName = new ObservableCollection<string>(current);
            }
        }
        #endregion

        #region 命令
        public RelayCommand<Window> LoginCommand => new RelayCommand<Window>((w) => 
        {
            if (true)
            {
                w.DialogResult = true;
                w.Close();
            }
        });

        public RelayCommand RegisterCommand => new RelayCommand(() =>
        {
            Messenger.Default.Send("CreateRegisterView");
        });
        #endregion
    }
}

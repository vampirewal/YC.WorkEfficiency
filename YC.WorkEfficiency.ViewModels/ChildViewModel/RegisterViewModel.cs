#region << 文 件 说 明 >>
/*----------------------------------------------------------------
// 文件名称：RegisterViewModel
// 创 建 者：杨程
// 创建时间：2021/4/27 10:30:13
// 文件版本：V1.0.0
// ===============================================================
// 功能描述：
//		
//
//----------------------------------------------------------------*/
#endregion

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using YC.WorkEfficiency.DataAccess;
using YC.WorkEfficiency.Models;
using YC.WorkEfficiency.SimpleMVVM;
using YC.WorkEfficiency.Themes;

namespace YC.WorkEfficiency.ViewModels
{
    public class RegisterViewModel: ViewModelBase
    {
        public RegisterViewModel()
        {
            Title = "注册";
            //构造函数
            NewUserModel = new UserModel()
            { 
                GuidId= Guid.NewGuid().ToString(),
                IsLogin=false,
                IsRemember=false
            };
        }
        private bool isRegister = false;
        public override object GetResult()
        {
            return isRegister;
        }

        #region 属性
        public UserModel NewUserModel { get; set; }

        #endregion

        #region 公共方法

        #endregion

        #region 私有方法

        #endregion

        #region 命令
        public RelayCommand RegisterUserCommand => new RelayCommand(()=> 
        {
            bool isok = false;
            Window w = View as Window;
            using(WorkEfficiencyDataContext work =new WorkEfficiencyDataContext())
            {
                var OldUser= work.UserModelDB.FirstOrDefault(f => f.UserName == NewUserModel.UserName);
                if (OldUser==null)
                {
                    work.UserModelDB.Add(NewUserModel);
                    work.SaveChanges();
                    isok = true;
                    isRegister = true;
                }
                else
                {
                    DialogWindow.Show("已存在相同的用户名！", MessageType.Error, WindowsManager.Windows["RegisterWindow"]);
                }
            }
            if (isok)
            {
                w.DialogResult = true;
                WindowsManager.CloseWindow(w);
            }
        });
        #endregion
    }
}

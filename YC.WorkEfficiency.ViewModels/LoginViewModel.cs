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
using YC.WorkEfficiency.Models;
using YC.WorkEfficiency.SimpleMVVM;
using YC.WorkEfficiency.ViewModels.Common;

namespace YC.WorkEfficiency.ViewModels
{
    public class LoginViewModel: ViewModelBase
    {
        public LoginViewModel()
        {
            //构造函数
            Title = "登陆";
            InitData();
        }
        #region 重写

        public override RelayCommand CloseWindowCommand => new RelayCommand(() =>
        {
            System.Environment.Exit(0);
            Application.Current.Shutdown();
        });

        private bool isLogin=false;
        public override object GetResult()
        {
            return isLogin;
        }
        #endregion

        #region 属性

        private UserModel _User;
        public UserModel User { get=>_User; set { _User = value;DoNotify(); } }

        //public UserModel SelectUserModel { get; set; }
        public ObservableCollection<UserModel> HaveLoginUserName { get; set; }
        #endregion

        #region 公共方法

        #endregion

        #region 私有方法
        private void InitData()
        {
            HaveLoginUserName = new ObservableCollection<UserModel>();
            User = new UserModel();
            MessengerRegister();
            GetLoginName();
        }
        #endregion

        #region 命令
        public RelayCommand<Window> LoginCommand => new RelayCommand<Window>((w) => 
        {
            using(WorkEfficiencyDataContext work=new WorkEfficiencyDataContext())
            {
                var current = work.UserModelDB.Where(w => w.UserName == User.UserName && w.PassWord == User.PassWord).FirstOrDefault();
                var ViewDataUserModel = User;
                if (current != null)
                {
                    if (!current.IsLogin)
                    {
                        User = current;
                        if (ViewDataUserModel.IsRemember)
                        {
                            User.IsRemember = true;
                        }
                        User.IsLogin = true;
                        work.UserModelDB.Update(User);
                        work.SaveChanges();
                        GlobalData.GetInstance().UserInfo = User;
                        //获取该用户的文件分类类型设置
                        var fileTypeList= work.FileTypeDB.Where(w => w.UserId == User.GuidId).ToList();
                        foreach (var item in fileTypeList)
                        {
                            GlobalData.GetInstance().UserFileTypes.Add(item);
                        }

                        isLogin = true;
                        w.Close();
                    }
                    else
                    {
                        MessageBox.Show("当前用户已登陆，请勿重复登陆！");
                    }
                }
            }
        });

        public RelayCommand RegisterCommand => new RelayCommand(() =>
        {
            //Messenger.Default.Send("CreateRegisterView");
            if(Convert.ToBoolean(WindowsManager.CreateDialogWindowByViewModelResult("RegisterView", new RegisterViewModel())))
            {
                GetLoginName();
            }

        });

        public RelayCommand<string> SelectUserNameCommand => new RelayCommand<string>((u) =>
        {
            var current = HaveLoginUserName.FirstOrDefault(f => f.GuidId == u);
            if (current != null)
            {
                if (current.IsRemember)
                {
                    User = current;
                }
                else
                {
                    User.ClearInfo();
                    User.UserName = current.UserName;
                }
            }
        });

        #endregion

        #region 消息
        /// <summary>
        /// 消息注册
        /// </summary>
        private void MessengerRegister()
        {
            Messenger.Default.Register(this, "GetLoginName", GetLoginName);
        }
        /// <summary>
        /// 获取登陆的用户名
        /// </summary>
        private void GetLoginName()
        {
            using (WorkEfficiencyDataContext work = new WorkEfficiencyDataContext())
            {
                HaveLoginUserName.Clear();
                var current = work.UserModelDB.Where(w=>w.GuidId!=null).ToList();
                for (int i = 0; i < current.Count; i++)
                {
                    HaveLoginUserName.Add(current[i]);
                }
            }
        }

        
        #endregion
    }
}

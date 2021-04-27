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
using YC.WorkEfficiency.DataAccess;
using YC.WorkEfficiency.Models;
using YC.WorkEfficiency.SimpleMVVM;

namespace YC.WorkEfficiency.ViewModels
{
    public class RegisterViewModel:ViewModelBase
    {
        public RegisterViewModel()
        {
            //构造函数
            NewUserModel = new UserModel()
            { 
                GuidId= Guid.NewGuid().ToString(),
                IsLogin=false,
                IsRemember=0
            };
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
            using(WorkEfficiencyDataContext work =new WorkEfficiencyDataContext())
            {
                var OldUser= work.UserModelDB.FirstOrDefault(f => f.UserName == NewUserModel.UserName);
                if (OldUser==null)
                {
                    work.UserModelDB.Add(NewUserModel);
                    work.SaveChanges();
                }
            }
        });
        #endregion
    }
}

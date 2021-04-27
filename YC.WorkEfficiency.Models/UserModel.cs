#region << 文 件 说 明 >>
/*----------------------------------------------------------------
// 文件名称：UserModel
// 创 建 者：杨程
// 创建时间：2021/4/25 11:07:38
// 文件版本：V1.0.0
// ===============================================================
// 功能描述：
//		
//
//----------------------------------------------------------------*/
#endregion

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace YC.WorkEfficiency.Models
{
    [Table("User")]
    public class UserModel:BaseModel
    {
        public UserModel()
        {
            //构造函数
        }

        //private string _GuidId;
        //[Key]
        //public string GuidId
        //{
        //    get { return _GuidId; }
        //    set { _GuidId = value; }
        //}

        private string _UserName;
        [Column("UserName")]
        public string UserName
        {
            get { return _UserName; }
            set { _UserName = value; DoNotify(); }
        }

        private string _PassWord;
        [Column("PassWord")]
        public string PassWord
        {
            get { return _PassWord; }
            set { _PassWord = value; DoNotify(); }
        }

        private int _IsRemember;
        /// <summary>
        /// 是否记住密码，0是不记住，1是记住
        /// </summary>
        [Column("IsRemember")]
        public int IsRemember
        {
            get { return _IsRemember; }
            set { _IsRemember = value; DoNotify(); }
        }

        private bool _IsLogin;
        /// <summary>
        /// 是否登陆
        /// </summary>
        [Column("IsLogin")]
        public bool IsLogin
        {
            get { return _IsLogin; }
            set { _IsLogin = value; DoNotify(); }
        }

    }
}

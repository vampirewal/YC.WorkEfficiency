#region << 文 件 说 明 >>
/*----------------------------------------------------------------
// 文件名称：UserModel
// 创 建 者：杨程
// 创建时间：2021/4/25 9:59:05
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
using GalaSoft.MvvmLight;

namespace YC.WorkEfficiency.View.Models
{
    public class UserModel : ObservableObject
    {
        public UserModel()
        {
            //构造函数
        }

        #region 属性
        private string _GuidId;

        public string GuidId
        {
            get { return _GuidId; }
            set { _GuidId = value; }
        }

        #endregion
    }
}

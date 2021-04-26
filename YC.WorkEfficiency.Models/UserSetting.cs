#region << 文 件 说 明 >>
/*----------------------------------------------------------------
// 文件名称：UserSetting
// 创 建 者：杨程
// 创建时间：2021/4/26 9:32:20
// 文件版本：V1.0.0
// ===============================================================
// 功能描述：
//		
//
//----------------------------------------------------------------*/
#endregion

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace YC.WorkEfficiency.Models
{
    [Table("UserSetting")]
    public class UserSetting:BaseModel
    {
        
        public UserSetting()
        {
            //构造函数
        }

        #region 属性
        private string _UserGuid;
        [Column("UserGuid")]
        public string UserGuid
        {
            get { return _UserGuid; }
            set { _UserGuid = value;DoNotify(); }
        }


        #endregion
    }
}

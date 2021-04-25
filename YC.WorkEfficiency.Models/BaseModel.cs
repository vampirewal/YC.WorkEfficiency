#region << 文 件 说 明 >>
/*----------------------------------------------------------------
// 文件名称：BaseModel
// 创 建 者：杨程
// 创建时间：2021/4/25 17:15:35
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
using System.Text;

namespace YC.WorkEfficiency.Models
{
    public class BaseModel
    {
        public BaseModel()
        {
            //构造函数
        }

        #region 属性
        private string _GuidId;
        [Key]
        public string GuidId
        {
            get { return _GuidId; }
            set { _GuidId = value; }
        }

        #endregion
    }
}

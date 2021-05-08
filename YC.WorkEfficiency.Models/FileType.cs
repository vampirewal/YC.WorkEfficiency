#region << 文 件 说 明 >>
/*----------------------------------------------------------------
// 文件名称：FileType
// 创 建 者：杨程
// 创建时间：2021/5/8 10:44:38
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
    [Table("FileType")]
    public class FileType:BaseModel
    {
        #region 属性
        private string _UserId;
        [Column("UserId")]
        public string UserId { get => _UserId; set { _UserId = value;DoNotify(); } }

        private string _Types;
        [Column("Types")]
        public string Types { get => _Types; set { _Types = value;DoNotify(); } }
        #endregion
    }
}

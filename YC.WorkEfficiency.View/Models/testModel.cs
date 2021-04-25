#region << 文 件 说 明 >>
/*----------------------------------------------------------------
// 文件名称：testModel
// 创 建 者：杨程
// 创建时间：2021/4/23 17:59:33
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

namespace YC.WorkEfficiency.View.Models
{
    [Table("FileModel")]
    public class testModel
    {
        public testModel()
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

        #region 公共方法

        #endregion

        #region 私有方法

        #endregion

        #region 命令

        #endregion
    }
}

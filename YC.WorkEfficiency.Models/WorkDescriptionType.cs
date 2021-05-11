#region << 文 件 说 明 >>
/*----------------------------------------------------------------
// 文件名称：WorkDescriptionType
// 创 建 者：杨程
// 创建时间：2021/5/11 13:14:39
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
    [Table("WorkDescription")]
    public class WorkDescriptionType:BaseModel
    {
        public WorkDescriptionType()
        {
            //构造函数
        }

        #region 属性
        private string _UserGuidId;
        [Column("UserGuidId")]
        public string UserGuidId
        {
            get { return _UserGuidId; }
            set { _UserGuidId = value; DoNotify(); }
        }

        private string _TypeName;
        [Column("TypeName")]
        public string TypeName
        {
            get { return _TypeName; }
            set { _TypeName = value; DoNotify(); }
        }

        private string _TypeBackgroundColor;
        [Column("TypeBackgroundColor")]
        public string TypeBackgroundColor
        {
            get { return _TypeBackgroundColor; }
            set { _TypeBackgroundColor = value; DoNotify(); }
        }

        private string _TypeFontColor;
        [Column("TypeFontColor")]
        public string TypeFontColor
        {
            get { return _TypeFontColor; }
            set { _TypeFontColor = value; DoNotify(); }
        }
        #endregion

        #region 扩展属性
        private int _HaveSettingWorkDes;
        /// <summary>
        /// 已设置的工作描述数量
        /// </summary>
        [NotMapped]
        public int HaveSettingWorkDes
        {
            get { return _HaveSettingWorkDes; }
            set { _HaveSettingWorkDes = value; DoNotify(); }
        }

        #endregion
    }
}

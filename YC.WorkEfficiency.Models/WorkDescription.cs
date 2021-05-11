#region << 文 件 说 明 >>
/*----------------------------------------------------------------
// 文件名称：WorkDescription
// 创 建 者：杨程
// 创建时间：2021/5/11 10:12:46
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
    
    [Table("WorkDescription")]
    public class WorkDescription : BaseModel
    {
        public WorkDescription()
        {
            DesBackground = "#3a3952";
            FontColor = "#f2eada";
        }

        #region 属性
        private string _WorkDescriptionText;
        /// <summary>
        /// 工作描述的内容
        /// </summary>
        [Column("WorkDescriptionText")]
        public string WorkDescriptionText
        {
            get { return _WorkDescriptionText; }
            set { _WorkDescriptionText = value; DoNotify(); }
        }


        private string fileModelGuidId;
        /// <summary>
        /// 关联的文件guid
        /// </summary>
        [Column("FileModelGuidId")]
        public string FileModelGuidId
        {
            get => fileModelGuidId;
            set
            {
                fileModelGuidId = value; DoNotify();
            }
        }
        
        
        private DateTime createTime;
        /// <summary>
        /// 创建时间
        /// </summary>
        [Column("CreateTime")]
        public DateTime CreateTime { get => createTime; set { createTime = value; DoNotify(); } }

        private string _UserGuidId;
        /// <summary>
        /// 关联的用户guid
        /// </summary>
        [Column("UserGuidId")]
        public string UserGuidId
        {
            get { return _UserGuidId; }
            set { _UserGuidId = value; DoNotify(); }
        }

        private string _DesBackground;
        [Column("DesBackground")]
        public string DesBackground
        {
            get { return _DesBackground; }
            set { _DesBackground = value; DoNotify(); }
        }

        private string _FontColor;
        [Column("FontColor")]
        public string FontColor
        {
            get { return _FontColor; }
            set { _FontColor = value; DoNotify(); }
        }

        #endregion
    }
}

#region << 文 件 说 明 >>

/*----------------------------------------------------------------
// 文件名称：FileModel
// 创 建 者：杨程
// 创建时间：2021/4/9 17:43:38
// 文件版本：V1.0.0
// ===============================================================
// 功能描述：
//
//
//----------------------------------------------------------------*/

#endregion << 文 件 说 明 >>

using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace YC.WorkEfficiency.Models
{
    [Table("FileModel")]
    public class FileModel :BaseModel
    {
        public FileModel()
        {
            //构造函数
            //Childs = new List<FileModel>();
            //StartWork();
        }

        #region 属性

        private string _afterTime;
        private DateTime _CreateTime;
        private DateTime _EndTime;
        private string _FileText;
        private string _FileTitle;
        //private string _GuidId;

        private bool _IsFinished;

        private bool isEdit;

        private bool isSelected;

        /// <summary>
        /// 经过时间
        /// </summary>
        [Column("AfterTime")]
        public string AfterTime
        {
            get { return _afterTime; }
            set { _afterTime = value;  }
        }

        /// <summary>
        /// 文件创建时间
        /// </summary>
        [Column("CreateTime")]
        public DateTime CreateTime
        {
            get { return _CreateTime; }
            set { _CreateTime = value;  }
        }

        /// <summary>
        /// 结束时间，在点击 IsFinish后自动存入
        /// </summary>
        [Column("EndTime")]
        public DateTime EndTime
        {
            get { return _EndTime; }
            set { _EndTime = value;  }
        }

        /// <summary>
        /// 文件存储内容
        /// </summary>
        [Column("FileText")]
        public string FileText
        {
            get { return _FileText; }
            set { _FileText = value;  }
        }

        /// <summary>
        /// 文件名称
        /// </summary>
        [Column("FileTitle")]
        public string FileTitle
        {
            get { return _FileTitle; }
            set { _FileTitle = value;  }
        }

        /// <summary>
        /// 文件ID
        /// </summary>
        //[Key]
        //public string GuidId
        //{
        //    get { return _GuidId; }
        //    set
        //    {
        //        _GuidId = value;
                
        //    }
        //}
        [Column("IsEdit")]
        public bool IsEdit { get => isEdit; set { isEdit = value;  } }

        /// <summary>
        /// 是否结束
        /// </summary>
        [Column("IsFinished")]
        public bool IsFinished
        {
            get { return _IsFinished; }
            set { _IsFinished = value;  }
        }

        [Column("IsSelected")]
        public bool IsSelected { get => isSelected; set { isSelected = value;  } }
        #endregion 属性

        #region 扩展属性

        /// <summary>
        /// 状态图标的颜色
        /// </summary>
        [Column("StateColor")]
        public string StateColor { get; set; }

        #endregion 扩展属性
    }
}
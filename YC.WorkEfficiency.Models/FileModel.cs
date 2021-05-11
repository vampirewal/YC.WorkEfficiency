#region << 文 件 说 明 >>

/*----------------------------------------------------------------
// 文件名称：FileModel
// 创 建 者：杨程
// 创建时间：2021/4/9 17:43:38
// 文件版本：V1.0.0
// ===============================================================
// 功能描述：
//主要的工作的类
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

        private bool isDelete;

        /// <summary>
        /// 经过时间
        /// </summary>
        [Column("AfterTime")]
        public string AfterTime
        {
            get { return _afterTime; }
            set { _afterTime = value; DoNotify(); }
        }

        /// <summary>
        /// 文件创建时间
        /// </summary>
        [Column("CreateTime")]
        public DateTime CreateTime
        {
            get { return _CreateTime; }
            set { _CreateTime = value; DoNotify(); }
        }

        /// <summary>
        /// 结束时间，在点击 IsFinish后自动存入
        /// </summary>
        [Column("EndTime")]
        public DateTime EndTime
        {
            get { return _EndTime; }
            set { _EndTime = value; DoNotify(); }
        }

        /// <summary>
        /// 文件存储内容
        /// </summary>
        [Column("FileText")]
        public string FileText
        {
            get { return _FileText; }
            set { _FileText = value; DoNotify(); }
        }

        /// <summary>
        /// 文件名称
        /// </summary>
        [Column("FileTitle")]
        public string FileTitle
        {
            get { return _FileTitle; }
            set { _FileTitle = value; DoNotify(); }
        }

        
        [Column("IsEdit")]
        public bool IsEdit { get => isEdit; set { isEdit = value; DoNotify(); } }

        /// <summary>
        /// 是否结束
        /// </summary>
        [Column("IsFinished")]
        public bool IsFinished
        {
            get { return _IsFinished; }
            set { _IsFinished = value; DoNotify(); }
        }

        /// <summary>
        /// 是否删除（逻辑删除）
        /// </summary>
        [Column("IsDeleted")]
        public bool IsDeleted { get => isDelete; set { isDelete = value; DoNotify(); } }
        #endregion 属性

        #region 扩展属性

        private string _StateColor;
        /// <summary>
        /// 状态图标的颜色
        /// </summary>
        [Column("StateColor")]
        public string StateColor { get=>_StateColor; set { _StateColor = value; DoNotify(); } }

        private string _UserGuid;
        /// <summary>
        /// 归属用户
        /// </summary>
        [Column("UserGuid")]
        public string UserGuid
        {
            get { return _UserGuid; }
            set { _UserGuid = value; DoNotify(); }
        }

        private string _UserName;
        /// <summary>
        /// 归属用户姓名
        /// </summary>
        [Column("UserName")]
        public string UserName
        {
            get { return _UserName; }
            set { _UserName = value; DoNotify(); }
        }


        private string _FileTypeGuid;
        /// <summary>
        /// 项目类型ID
        /// </summary>
        [Column("FileTypeGuid")]
        public string FileTypeGuid
        {
            get { return _FileTypeGuid; }
            set { _FileTypeGuid = value; DoNotify(); }
        }

        private string _FileType;
        /// <summary>
        /// 项目类型
        /// </summary>
        [Column("FileType")]
        public string FileType
        {
            get { return _FileType; }
            set { _FileType = value; DoNotify(); }
        }

        #endregion 扩展属性
    }
}
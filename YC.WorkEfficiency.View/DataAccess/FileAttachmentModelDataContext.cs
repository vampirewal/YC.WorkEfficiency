#region << 文 件 说 明 >>
/*----------------------------------------------------------------
// 文件名称：FileAttachmentModelDataContext
// 创 建 者：杨程
// 创建时间：2021/4/21 16:19:46
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
using Microsoft.EntityFrameworkCore;
using YC.WorkEfficiency.Models;

namespace YC.WorkEfficiency.View.DataAccess
{
    public class FileAttachmentModelDataContext : DbContext
    {
        public FileAttachmentModelDataContext()
        {
            //构造函数
        }
        public DbSet<FileAttachmentModel> FileAttachmentModelDB { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseSqlite("Data Source=WorkEfficiency.db");
        }
    }
}

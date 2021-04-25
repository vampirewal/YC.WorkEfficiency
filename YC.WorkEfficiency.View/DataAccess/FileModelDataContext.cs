#region << 文 件 说 明 >>
/*----------------------------------------------------------------
// 文件名称：FileModelDataContext
// 创 建 者：杨程
// 创建时间：2021/4/16 9:43:42
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
    public class FileModelDataContext : DbContext
    {
        public FileModelDataContext()
        {
            //构造函数
        }
        

        public DbSet<FileModel> FileModelDB { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseSqlite("Data Source=WorkEfficiency.db");
        }
    }
}

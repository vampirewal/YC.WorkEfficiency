#region << 文 件 说 明 >>
/*----------------------------------------------------------------
// 文件名称：WorkEfficiencyDataContext
// 创 建 者：杨程
// 创建时间：2021/4/25 14:38:39
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

namespace YC.WorkEfficiency.DataAccess
{
    public class WorkEfficiencyDataContext : DbContext
    {
        public WorkEfficiencyDataContext()
        {
            //构造函数
        }

        #region DB
        public DbSet<UserModel> UserModelDB { get; set; }
        public DbSet<FileModel> FileModelDB { get; set; }
        public DbSet<FileAttachmentModel> FileAttachmentModelDB { get; set; }
        public DbSet<FileType> FileTypeDB { get; set; }

        public DbSet<UserSetting> UserSettingsDB { get; set; }

        #endregion

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseSqlite("Data Source=WorkEfficiency.db");
        }
    }
}

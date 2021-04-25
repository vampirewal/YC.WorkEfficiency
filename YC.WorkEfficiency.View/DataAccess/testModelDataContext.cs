#region << 文 件 说 明 >>
/*----------------------------------------------------------------
// 文件名称：testModelDataContext
// 创 建 者：杨程
// 创建时间：2021/4/23 18:00:59
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
using Microsoft.Extensions.Configuration;
using YC.WorkEfficiency.View.Models;

namespace YC.WorkEfficiency.View.DataAccess
{
    public class testModelDataContext : DbContext
    {
        private IConfiguration configuration;
        public testModelDataContext()
        {
            //构造函数
        }
        public DbSet<testModel> testModelDB { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseSqlite("Data Source=WorkEfficiency.db");
        }
    }
}

#region<<文 件 说 明>>
/*----------------------------------------------------------------
// 文件名称：CustomExportMetadata
// 创 建 者：杨程
// 创建时间：2021/3/3 星期三 16:39:29
// 文件版本：V1.0.0
// ===============================================================
// 功能描述：
//		
//
//----------------------------------------------------------------*/
#endregion

using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Composition;
using System.Text;

namespace YC.WorkEfficiency.Core
{
    [MetadataAttribute]
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
    public class CustomExportMetadata : ExportAttribute, IMetaData
    {
        public int Priority { get; private set; }
        public string ModuleFullName { get; private set; }
        public string ModuleName { get; private set; }
        public string Description { get; private set; }
        public string Author { get; private set; }
        public string Version { get; private set; }

        

        public CustomExportMetadata() : base(typeof(IMetaData))
        {
        }

        public CustomExportMetadata(int priority) : this()
        {
            this.Priority = priority;
        }
        public CustomExportMetadata(int priority, string moduleFullName) : this(priority)
        {
            this.ModuleFullName = moduleFullName;
        }

        public CustomExportMetadata(int priority, string moduleFullName, string moduleName) : this(priority, moduleFullName)
        {
            this.ModuleName = moduleName;
        }

        public CustomExportMetadata(int priority, string moduleFullName, string moduleName, string description) : this(priority, moduleFullName, moduleName)
        {
            this.Description = description;
        }

        public CustomExportMetadata(int priority, string moduleFullName, string moduleName, string description, string author) : this(priority, moduleFullName, moduleName, description)
        {
            this.Author = author;
        }

        public CustomExportMetadata(int priority, string moduleFullName, string moduleName, string description, string author, string version) : this(priority, moduleFullName, moduleName, description, author)
        {
            this.Version = version;
        }
    }


}
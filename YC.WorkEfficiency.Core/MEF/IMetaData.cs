using System.ComponentModel;

namespace YC.WorkEfficiency.Core
{
    /// <summary>
    /// 元数据接口
    /// </summary>
    public interface IMetaData
    {
        /// <summary>
        /// 优先级
        /// </summary>
        [DefaultValue(0)]
        int Priority { get; }

        /// <summary>
        /// Module完整名称（不能重复），例如：MainPage
        /// </summary>
        string ModuleFullName { get; }
        /// <summary>
        /// Module名称（不能重复），例如：首页
        /// </summary>
        string ModuleName { get; }
        /// <summary>
        /// 模块描述
        /// </summary>
        string Description { get; }
        /// <summary>
        /// 作者
        /// </summary>
        string Author { get; }
        /// <summary>
        /// 版本
        /// </summary>
        string Version { get; }
    }
}

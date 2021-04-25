#region << 文 件 说 明 >>
/*----------------------------------------------------------------
// 文件名称：LoadModulesServices
// 创 建 者：杨程
// 创建时间：2021/4/25 16:21:09
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
using System.ComponentModel.Composition.Hosting;
using System.IO;
using System.Text;
using System.Windows;
using Microsoft.EntityFrameworkCore.Metadata;
using YC.WorkEfficiency.Core;
using YC.WorkEfficiency.SimpleMVVM;

namespace YC.WorkEfficiency.ViewModels.Common
{
    /// <summary>
    /// 获取模块的管理类
    /// </summary>
    public class LoadModulesServices
    {
        private static LoadModulesServices instance;
        /// <summary>
        /// 单例实例
        /// </summary>
        public static LoadModulesServices Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new LoadModulesServices();
                    //注册消息
                    
                }
                return instance;
            }
        }

        /// <summary>
        /// 开始读取模块
        /// </summary>
        public void StartLoadModules()
        {
            Messenger.Default.Register(instance, "LoadModules", LoadModules);
        }

        #region 私有方法
        [ImportMany]
        public Lazy<Core.IView, IMetaData>[] Plugins { get; set; }

        public Dictionary<string, FrameworkElement> ModulesDic = new Dictionary<string, FrameworkElement>();

        private CompositionContainer container = null;


        public void LoadModules()
        {
            var dir = new DirectoryInfo(AppDomain.CurrentDomain.BaseDirectory + @"\Pluing");
            if (dir.Exists)
            {
                var catalog = new DirectoryCatalog(dir.FullName, "*.dll");
                container = new CompositionContainer(catalog);
                try
                {
                    container.ComposeParts(this);
                }
                catch (CompositionException compositionEx)
                {
                    Console.WriteLine(compositionEx.ToString());
                }

                foreach (var plugin in Plugins)
                {
                    string pluginName = plugin.Metadata.ModuleName;
                    string pluginFullName = plugin.Metadata.ModuleFullName;
                    int pluginNum = plugin.Metadata.Priority;
                    var framework = (FrameworkElement)plugin.Value.Window;
                    framework.Name = pluginName;
                    ModulesDic.Add(pluginName, framework);
                }

            }
            else
            {
                Directory.CreateDirectory(dir.FullName);
            }
        }


        #endregion
    }
}

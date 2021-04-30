﻿/* 项目名称： WindowsManager.cs
 * 命名空间： YC.WorkEfficiency.SimpleMVVM
 * 类 名 称: WindowsManager
 * 作   者 : 杨程
 * 概   述 : 
 * 创建时间 : 2021/2/20 18:47:33
 * 更新时间 : 2021/2/20 18:47:33
 * CLR版本 : 4.0.30319.42000
 * ******************************************************
 * Copyright@Administrator 2021 .All rights reserved.
 * ******************************************************
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace YC.WorkEfficiency.SimpleMVVM
{
    /// <summary>
    /// 窗口管理
    /// </summary>
    public static class WindowsManager
    {


        private static WindowCollection windows = new WindowCollection();
        /// <summary>
        /// 获取已创建的窗口
        /// </summary>
        public static WindowCollection Windows
        {
            get { return windows; }
        }

        /// <summary>
        /// 创建新窗体
        /// </summary>
        /// <param name="windowSetting"></param>
        public static void CreatWindow(WindowSetting windowSetting)
        {
            Window window = Activator.CreateInstance(windowSetting.WindowType, windowSetting.Parameters) as Window;
            window.WindowState = windowSetting.WindowState;

            windows.Add(window);

            switch (windowSetting.ShowMode)
            {
                case ShowMode.Show:
                    window.Show();
                    break;
                case ShowMode.Dialog:
                    window.ShowDialog();
                    break;
            }
        }

        /// <summary>
        /// 创建新窗体
        /// </summary>
        /// <param name="windowType"></param>
        /// <param name="parameters"></param>
        public static void CreatWindow(Type windowType, params object[] parameters)
        {
            Window window = Activator.CreateInstance(windowType, parameters) as Window;

            windows.Add(window);
            window.Show();
        }

        /// <summary>
        /// 创建对话框窗体
        /// </summary>
        /// <param name="windowType"></param>
        /// <param name="parameters"></param>
        public static object CreatDialogWindow(Type windowType, params object[] parameters)
        {
            Window window = Activator.CreateInstance(windowType, parameters) as Window;

            windows.Add(window);
            return window.ShowDialog();
        }

        /// <summary>
        /// 创建对话框窗体
        /// </summary>
        /// <param name="window"></param>
        public static object CreateDialogWindow(Window window)
        {
            windows.Add(window);
            return window.ShowDialog();
        }

        public static object CreateDialogWindowByViewModelResult(Window window,ViewModelBase vm)
        {
            windows.Add(window);
            window.ShowDialog();
            return vm.GetResult();
        }
        

        public static bool CreateDialogWindowToBool(Window window)
        {
            windows.Add(window);
            if (window.ShowDialog() == true)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 创建新窗体
        /// </summary>
        /// <param name="windowType"></param>
        /// <param name="showMode"></param>
        public static void CreatWindow(string windowType, ShowMode showMode)
        {
            var types = AppDomain.CurrentDomain.GetAssemblies()
                        .SelectMany(a => a.GetTypes().Where(t => t.Name == windowType))
                        .ToArray();
            if (types.Length == 1)
            {
                Window window = Activator.CreateInstance(types[0]) as Window;

                windows.Add(window);

                switch (showMode)
                {
                    case ShowMode.Show:
                        window.Show();
                        break;
                    case ShowMode.Dialog:
                        window.ShowDialog();
                        break;
                }
            }
            else
            {
                throw new Exception("没有找到该窗体类型或窗体类型不唯一");
            }


        }


        /// <summary>
        /// 创建新窗体
        /// </summary>
        /// <param name="window"></param>
        /// <param name="showMode"></param>
        public static void CreatWindow(Window window, ShowMode showMode)
        {
            windows.Add(window);
            switch (showMode)
            {
                case ShowMode.Show:
                    window.Show();
                    break;
                case ShowMode.Dialog:
                    window.ShowDialog();
                    break;
            }
        }

        /// <summary>
        /// 关闭窗体
        /// </summary>
        /// <param name="window"></param>
        public static void CloseWindow(Window window)
        {
            window.DialogResult = false;
            windows.Remove(window);
            window.Close();
            GC.Collect();
        }

        /// <summary>
        /// 关闭查找到的第一个窗口
        /// </summary>
        /// <param name="windowType"></param>
        public static void CloseSelectFirstWindow(Type windowType)
        {
            foreach (var window in windows)
            {
                if (window.GetType().FullName == windowType.FullName)
                {
                    windows.Remove(window);
                    window.Close();
                    break;
                }
            }
        }

        /// <summary>
        /// 关闭查找到的所有的窗口
        /// </summary>
        /// <param name="windowType"></param>
        public static void CloseSelectAllWindow(Type windowType)
        {
            foreach (var window in windows)
            {
                if (window.GetType().FullName == windowType.FullName)
                {
                    windows.Remove(window);
                    window.Close();
                }
            }
        }

        /// <summary>
        /// 关闭所有窗口
        /// </summary>
        public static void CloseAllWindow()
        {
            foreach (var window in windows)
            {
                window.Close();
            }
        }
    }
}

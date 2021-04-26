#region << 文 件 说 明 >>

/*----------------------------------------------------------------
// 文件名称：BaseCommand
// 创 建 者：杨程
// 创建时间：2021/4/9 11:00:03
// 文件版本：V1.0.0
// ===============================================================
// 功能描述：基础窗体控制的命令代码
//
//
//----------------------------------------------------------------*/

#endregion << 文 件 说 明 >>

using System;
using System.Windows;

namespace YC.WorkEfficiency.View.ViewModels
{
    public class BaseCommand
    {
        public BaseCommand()
        {
            //构造函数
        }

        #region 命令

        //public RelayCommand<Window> CloseWindowCommand => new RelayCommand<Window>((w) =>
        //{
        //    if (w.Name == "MainView")
        //    {
        //        System.Environment.Exit(0);
        //        Application.Current.Shutdown();
        //    }
        //    else
        //    {
        //        w.Close();
        //        GC.Collect();
        //    }
        //});

        //public RelayCommand<Window> MaxWindowCommand => new RelayCommand<Window>((w) =>
        //{
        //    if (w.WindowState == WindowState.Normal)
        //    {
        //        w.WindowState = WindowState.Maximized;
        //    }
        //    else
        //    {
        //        w.WindowState = WindowState.Normal;
        //    }
        //});

        //public RelayCommand<Window> MinWindowCommand => new RelayCommand<Window>((w) => { w.WindowState = WindowState.Minimized; });

        //public RelayCommand<Window> WindowMoveCommand => new RelayCommand<Window>((w) =>
        //{
        //    if (w!=null)
        //    {
        //        w.DragMove();
        //    }
        //});

        #endregion 命令
    }
}
#region << 文 件 说 明 >>
/*----------------------------------------------------------------
// 文件名称：DialogWindow
// 创 建 者：杨程
// 创建时间：2021/3/25 9:33:14
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
using System.Windows;

namespace YC.WorkEfficiency.Themes
{
    /// <summary>
    /// 自定义弹出提示框
    /// </summary>
    public class DialogWindow
    {
        public static void Show(string Msg, MessageType messageType, Window window,bool isKeepOpen=false)
        {
            DialogWindows dialogWindows = new DialogWindows(Msg, window, messageType, isKeepOpen);
            dialogWindows.Show();
        }
    }
}

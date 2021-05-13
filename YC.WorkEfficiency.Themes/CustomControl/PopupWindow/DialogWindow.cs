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
        /// <summary>
        /// 常规Pop窗体
        /// </summary>
        /// <param name="Msg">消息文本</param>
        /// <param name="messageType">消息类型</param>
        /// <param name="window">选择owon窗体</param>
        /// <param name="isKeepOpen">是否保持常驻</param>
        public static void Show(string Msg, MessageType messageType, Window window,bool isKeepOpen=false)
        {
            DialogWindows dialogWindows = new DialogWindows(Msg, window, messageType, isKeepOpen);
            dialogWindows.Show();
        }

        /// <summary>
        /// 弹出一个Dialog的窗体
        /// </summary>
        /// <param name="Msg">显示文本</param>
        /// <param name="Title">标题</param>
        /// <returns></returns>
        public static bool ShowDialog(string Msg,string Title)
        {
            DialogWindowReturnObject dialogWindowReturnObject = new DialogWindowReturnObject(Msg, Title);
            if (dialogWindowReturnObject.ShowDialog() == true)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 弹出右侧通知框，可主动关闭，可自动关闭
        /// </summary>
        /// <param name="Msg">显示文本</param>
        /// <param name="TimeSpan">持续时间</param>
        public static void ShowNotify(string Msg,int TimeSpan)
        {
            NotifyWindow notifyWindow = new NotifyWindow(Msg, TimeSpan);
            notifyWindow.Show();
        }
    }
}

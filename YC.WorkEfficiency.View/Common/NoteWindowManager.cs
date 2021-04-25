#region << 文 件 说 明 >>
/*----------------------------------------------------------------
// 文件名称：NoteWindowManager
// 创 建 者：杨程
// 创建时间：2021/4/1 9:43:31
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

namespace YC.WorkEfficiency.View.Common
{
    public class NoteWindowManager
    {
        private static NoteWindowManager instance;
        /// <summary>
        /// 默认单例实例
        /// </summary>
        public static NoteWindowManager Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new NoteWindowManager();
                }

                return instance;
            }
        }

        #region 属性
        public Window MainWindow;

        public Dictionary<string, Window> NoteWindowsDic = new Dictionary<string, Window>();
        #endregion

        #region 公共方法
        /// <summary>
        /// 创建新的窗口
        /// </summary>
        /// <param name="noteModel"></param>
        //public void CreateNoteWindow(NoteModel noteModel)
        //{
        //    EditView editView = new EditView(noteModel);
        //    NoteWindowsDic.Add(noteModel.GuidId, editView);

        //    editView.Show();
        //}

        //public void GetNoteWindow(NoteModel noteModel)
        //{
        //    if (NoteWindowsDic.ContainsKey(noteModel.GuidId))
        //    {
        //        NoteWindowsDic[noteModel.GuidId].Topmost = true;
        //        NoteWindowsDic[noteModel.GuidId].Topmost = false;
        //    }
        //    else
        //    {
        //        CreateNoteWindow(noteModel);
        //    }
        //}

        //public void CloseNoteWindow(NoteModel noteModel)
        //{
        //    if (NoteWindowsDic.ContainsKey(noteModel.GuidId))
        //    {
        //        NoteWindowsDic[noteModel.GuidId].Close();
        //        NoteWindowsDic.Remove(noteModel.GuidId);
        //    }
        //}
        #endregion

        #region 私有方法
        
        #endregion

        #region 命令

        #endregion
    }
}

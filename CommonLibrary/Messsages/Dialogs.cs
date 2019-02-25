using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CommonLibrary
{
    public static class Dialogs
    {
        /// <summary>
        /// 提示信息
        /// </summary>
        /// <param name="info">内容</param>
        /// <param name="autoClose">自动关闭标记</param>
        public static void Show(string info,bool autoClose)
        {
            DialogForm frm = new DialogForm();
            frm.Message_Info = info;
            frm.Auto_Close = autoClose;
            frm.ShowDialog();
        }
        /// <summary>
        /// 提示信息，自动关闭
        /// </summary>
        /// <param name="info">内容</param>
        public static void Show(string info)
        {
            DialogForm frm = new DialogForm();
            frm.Message_Info = info;
            frm.Auto_Close = true;
            frm.ShowDialog();
        }
        /// <summary>
        /// 显示异常信息，并记录日志
        /// </summary>
        /// <param name="info"></param>
        public static void Show(Exception info)
        {            
            DialogForm frm = new DialogForm();
            frm.Message_Info = info.ToString();
            frm.Auto_Close = false;
            frm.ShowDialog();
        }
        /// <summary>
        /// 确认信息
        /// </summary>
        /// <param name="info">内容</param>
        public static bool Confirm(string info)
        {
            ConfirmForm frm = new ConfirmForm();
            frm.Message_Info = info;
            frm.ShowDialog();
            if (frm.DialogResult == System.Windows.Forms.DialogResult.Yes)
            {
                return true;
            }
            else
            {
                return false;
            }
        }        
    }
}

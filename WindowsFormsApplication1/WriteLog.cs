using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using WindowsFormsApplication1.lib;

namespace WindowsFormsApplication1
{
    public class WriteLog
    {
        public delegate void LogAppendDelegate(Color color, string text);
        RichTextBox richTextBoxRemote;

        /// <summary>
        /// 构造函数传入RichTextBox控件的实例。
        /// </summary>
        /// <param name="richTextBoxRemote"></param>
        public WriteLog(RichTextBox richTextBoxRemote)
        {
            this.richTextBoxRemote = richTextBoxRemote;

        }

        /// <summary>
        /// LogAppendDelegate委托指向的方法
        /// </summary>
        /// <param name="color"></param>
        /// <param name="text"></param>
        private void LogAppend(Color color, string text)
        {
            richTextBoxRemote.SelectionColor = color;
            richTextBoxRemote.AppendText(text);
            richTextBoxRemote.AppendText(System.Environment.NewLine);
            if (richTextBoxRemote.Lines.Length>500)
            {
                richTextBoxRemote.Clear();
            }
            FileHelper.WriteLog(text);
        }

        /// <summary>   
        /// 追加显示文本   
        /// </summary>   
        /// <param name="text"></param>   
        public void LogAppendMsg(string text)
        {
            LogAppendDelegate la = new LogAppendDelegate(LogAppend);
            richTextBoxRemote.Invoke(la, Color.White, DateTime.Now.ToString("yyyy.MM.dd HH:mm:ss") + text);
        }

        /// <summary>   
        /// 显示错误日志   
        /// </summary>   
        /// <param name="text"></param>   
        public void LogError(string text)
        {
            LogAppendDelegate la = new LogAppendDelegate(LogAppend);
            richTextBoxRemote.Invoke(la, Color.Red, DateTime.Now.ToString("yyyy.MM.dd HH:mm:ss") + text);
        }
        /// <summary>   
        /// 显示警告信息   
        /// </summary>   
        /// <param name="text"></param>   
        public void LogWarning(string text)
        {
            LogAppendDelegate la = new LogAppendDelegate(LogAppend);
            richTextBoxRemote.Invoke(la, Color.Violet, DateTime.Now.ToString("yyyy.MM.dd HH:mm:ss") + text);
        }
        /// <summary>   
        /// 显示信息   
        /// </summary>   
        /// <param name="text"></param>   
        public void LogMessage(string text)
        {
            LogAppendDelegate la = new LogAppendDelegate(LogAppend);
            richTextBoxRemote.Invoke(la, Color.Wheat, DateTime.Now.ToString("yyyy.MM.dd HH:mm:ss") + text);
        }

    }

}

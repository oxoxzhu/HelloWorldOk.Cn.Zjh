using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading;

namespace WindowsFormsApplication1.lib
{
    public class FileHelper
    {
        private static string path = AppDomain.CurrentDomain.BaseDirectory + "log.txt";

        private static Queue<string> queue = new Queue<string>();//声明队列

        static FileHelper()
        {
            //启动线程池
            ThreadPool.QueueUserWorkItem(a =>
                {
                    if (!File.Exists(path))
                    {
                        File.Create(path);
                    }

                    while (true)
                    {
                        string ex = string.Empty;
                        //加锁
                        lock ("Itcast-DotNet-AspNet-Glable-LogLock")
                        {
                            //判断队列里元素个数
                            if (queue.Count > 0)
                            {
                                //出队
                                ex = queue.Dequeue();
                                try
                                {
                                    using (StreamWriter sw = new StreamWriter(path, true, Encoding.Default))
                                    {
                                        sw.Write(ex);
                                    }
                                }
                                catch (Exception)
                                {
                                    queue.Enqueue(ex);
                                }
                            }
                            else
                            {
                                //睡会儿
                                Thread.Sleep(1000);
                                continue;
                            }
                        }



                    }
                });
        }

        /// <summary>
        /// 日志写入文件
        /// </summary>
        /// <param name="str"></param>
        public static void WriteLog(string str)
        {

            lock ("Itcast-DotNet-AspNet-Glable-LogLock")
            {
                queue.Enqueue("\r\n" + str);
                //File.AppendAllText(path, "\r\n" + str);
            }

        }

    }
}

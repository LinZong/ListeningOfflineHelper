using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Xml.Linq;
using System.IO;
using System.Windows.Forms;

namespace ListeningOfflineHelper
{
    class Program
    {
        [STAThread]
        static void Main(string[] args)
        {
            if(CheckProgramSelf())
                
            {
                Console.WriteLine("这是一个可以离线播放华南成功大学，大英上级听力练习测试题的小软件的黑框框版本");
                Console.WriteLine("可供学生在课下听多几遍上机听力的内容，以便提高自己的英语听力水平");
                Console.WriteLine("因为是黑框框版本的原因，所以就懒得写记录答案的功能了。");
                Console.WriteLine("那么开始听力测试之前，请准备好纸和笔，用自己看得懂的方式记录所做题目的答案。");
                Console.WriteLine("由于作者是老实人+失败人士+低智商儿童，所以程序可能会出不少的bug，望各位大佬手下留情，不要调试(xi)它  QAQ");
                Console.WriteLine("下面程序就开始加载题目了。估计有这个需求的人应该也准备好题目了。");
                Console.ReadKey();
                Console.Clear();
                var CurrentPath = System.Environment.CurrentDirectory + @"\paper\";
                if (Directory.Exists(CurrentPath))
                {
                    Console.WriteLine("在程序当前目录检测到试题,任意键开始加载试题,如需退出请使用右上角的X");
                    Console.ReadKey();
                    try
                    {
                        XMLPaser x = new XMLPaser(CurrentPath);
                        QuestionViewer q = new QuestionViewer(x);
                        Console.WriteLine("加载完成，任意键开始听力");
                        Console.Clear();
                        q.SectionA();
                    }

                    catch
                    {
                        Console.WriteLine("出现了一些错误，无法正常播放听力");
                    }
                }
                else
                {
                    Console.WriteLine("在程序当前目录找不到试题,需要用户手动钦定,任意键开始钦定,如需退出请使用右上角的X");
                    Console.ReadKey();
                    FolderBrowserDialog folder = new FolderBrowserDialog();

                    if (folder.ShowDialog() == DialogResult.OK)
                    {
                        string folderPath = folder.SelectedPath + "\\";

                        if (File.Exists(folderPath + "paper.xml") && Directory.Exists(folderPath + "sound"))
                        {
                            Console.WriteLine("看上去好像有试题,开始加载……");
                            try
                            {
                                XMLPaser x = new XMLPaser(folderPath);
                                QuestionViewer q = new QuestionViewer(x);
                                Console.WriteLine("加载完成，任意键开始听力");
                                Console.ReadKey();
                                Console.Clear();
                                q.SectionA();
                            }
                            catch
                            {
                                Console.WriteLine("出现了一些错误，无法正常播放听力");
                            }
                        }

                    }
                }


            }

            else
            {
                Console.WriteLine("程序本体不完整。请确认");
                Console.WriteLine("Microsoft.DirectX.AudioVideoPlayback.dll");
                Console.WriteLine("Microsoft.DirectX.AudioVideoPlayback.xml");
                Console.WriteLine("ListeningOfflineHelper.exe.config");
                Console.WriteLine("和EXE都在同一目录下");
                Console.ReadKey();
            }
        }

        static bool CheckProgramSelf()
        {
            var CurrentPath = System.Environment.CurrentDirectory;
            return (File.Exists("Microsoft.DirectX.AudioVideoPlayback.dll") && File.Exists("Microsoft.DirectX.AudioVideoPlayback.xml") && File.Exists("ListeningOfflineHelper.exe.config"));
        }
    }
}

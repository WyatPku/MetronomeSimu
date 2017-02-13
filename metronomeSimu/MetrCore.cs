using metronomeSimu.SingleCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;

namespace metronomeSimu
{
    class MetrCore
    {
        //采用多线程的方法计算，并通过Invoke回调
        public const double deltaT = 0.001; //计算间隔/s
        public const double lastingT = 10; //计算持续的时间/s
        public const double showDeltaT = 0.01; //保存间隔/s
        Thread thread = null;
        ProgressBar progressbar = null;
        TextBlock textBlock = null;
        Dispatcher dispatcher = null;
        public List<MetronomeCore> coreList = new List<MetronomeCore>();
        public static String fileName = "default";
        public MetrCore(ProgressBar progressbar_ = null,
            TextBlock textBlock_ = null, Dispatcher dispatcher_ = null)
        {
            progressbar = progressbar_;
            textBlock = textBlock_;
            dispatcher = dispatcher_;
        }
        public void StartComput(String fileName_ = "default")
        {
            fileName = fileName_;
            if (thread != null)
            {
                //还在跑
                MessageBox.Show("正在运行计算进程，将要停止原进程");
                thread.Abort();
                thread = null;
            }
            thread = new Thread(ThreadMain);
            thread.Start();
        }
        public void ThreadEnd()
        {
            thread = null;
            //回调
        }
        private void InvokeAndShow(int prog)
        {
            if (dispatcher == null) return;
            if (progressbar != null)
            {
                dispatcher.Invoke(new Action(() => {
                    progressbar.Value = prog;
                }));
            }
            if (textBlock != null)
            {
                dispatcher.Invoke(new Action(() => {
                    textBlock.Text = "" + prog + "%";
                }));
            }
        }
        private void ThreadMain()
        {
            Console.WriteLine("开始计算！");
            /*for (int i=0; i<100; i++)
            {
                InvokeAndShow(i);
                Thread.Sleep(1000);
            }*/
            Console.WriteLine("节拍器个数：" + coreList.Count);
            Console.WriteLine("持续时间：" + lastingT + "s");
            Console.WriteLine("计算精度：" + deltaT + "s");
            Console.WriteLine("保存间隔：" + showDeltaT + "s");
            int N = (int)(lastingT / deltaT);
            int deltaN = N / 100;
            int showN = (int)(showDeltaT / deltaT);
            List<double[]> data = new List<double[]>();
            for (int i = 0; i < coreList.Count; i++) data.Add(new double[N / showN]);
            for (int i=0; i<N; i++)
            {
                if (i % showN == 0)
                {
                    //记录
                    for (int j = 0; j < coreList.Count; j++)
                    {
                        data[j][i / showN] = coreList[j].getTheta();
                    }
                }
                getNext();
                if (i % deltaN == 0) InvokeAndShow(i / deltaN);
            }
            //data.Add(new double[] { 1, 2, 3, 4 });
            FileEncoder.EncodeTo(fileName + ".dat", data);
            InvokeAndShow(100);
            ThreadEnd();
        }

        private void getNext()
        {
            for (int i=0; i<coreList.Count; i++)
            {
                coreList[i].afterdeltaT(deltaT);
            }
        }
    }
}

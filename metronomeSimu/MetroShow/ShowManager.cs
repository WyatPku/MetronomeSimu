using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;

namespace metronomeSimu.MetroShow
{
    class ShowManager
    {
        public const int refreshInterval = 20; //ms
        Thread mThread;
        Canvas canvas;
        List<double[]> data;
        double showDeltaT;
        List<MetroShape> shapes;
        Dispatcher dispatcher;

        public ShowManager(Canvas canvas_, Dispatcher dispatcher_)
        {
            dispatcher = dispatcher_;
            canvas = canvas_;
            shapes = new List<MetroShape>();
        }

        public void ReadFile(String fileName)
        {
            data = FileDecoder.tryDecode(fileName + ".dat");
            if (data == null)
            {
                MessageBox.Show("没有此文件或文件损坏(" + fileName + ".dat)");
                return;
            }
            showDeltaT = FileDecoder.showDeltaT;
            MessageBox.Show("文件信息：\r\n" + "data group = " + data.Count + "\r\n" + 
                "showDeltaT = " + showDeltaT);
            canvas.Children.Clear();
            shapes.Clear();
            for (int i = 0; i < data.Count; i++) shapes.Add(new MetroShape(canvas, i));
            //shapes[0].MoveToArc(0);
        }
        public void ShowMetro()
        {
            mThread = new Thread(threadDo);
            mThread.Start();
        }
        private void threadDo()
        {
            int nowCount = 0;
            int index = 0;
            while (index < data[0].Length)
            {
                InvokeAndShow(index);
                Console.WriteLine("" + nowCount);
                Thread.Sleep(refreshInterval);
                nowCount++;
                // 应该显示的下标：
                index = (int)(nowCount * refreshInterval / 1000.0 / showDeltaT);
            }
            //演示结束，显示最后一帧的结果
            InvokeAndShow(data[0].Length - 1);
            MessageBox.Show("演示结束");
        }
        private void InvokeAndShow(int index)
        {
            for (int i = 0; i < data.Count; i++)
            {
                dispatcher.Invoke(new Action(() => {
                    shapes[i].MoveToArc(data[i][index] * 180 / Math.PI);
                }));
            }
        }
        public void EndShow()
        {
            if (mThread == null) return;
            if (mThread.IsAlive) mThread.Abort();
        }
    }
}

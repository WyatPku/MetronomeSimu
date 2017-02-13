using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace metronomeSimu.MetroShow
{
    class ShowManager
    {
        Canvas canvas;
        List<double[]> data;
        List<MetroShape> shapes;

        public ShowManager(Canvas canvas_)
        {
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
            double showDeltaT = FileDecoder.showDeltaT;
            MessageBox.Show("文件信息：\r\n" + "data group = " + data.Count + "\r\n" + 
                "showDeltaT = " + showDeltaT);
            canvas.Children.Clear();
            shapes.Clear();
            for (int i = 0; i < data.Count; i++) shapes.Add(new MetroShape(canvas, i));
            //shapes[0].MoveToArc(0);
        }
    }
}

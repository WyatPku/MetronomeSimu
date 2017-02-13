using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using metronomeSimu.SingleCore;
using System.Timers;
using metronomeSimu.MetroShow;

namespace metronomeSimu
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        MetronomeCore metronome;
        ShowManager showManager;
        /*double deltaT = 0.001;
        double MaxT = 10;
        double MaxTheta = 0.11;*/
        MetrCore metrCore;

        public MainWindow()
        {
            InitializeComponent();
            metronome = new MetronomeCore(0.1);
            showManager = new ShowManager(canvas);
            buttonStart.Click += Button_Click;
            buttonEnd.Click += ButtonEnd_Click;
            buttonShow.Click += ButtonShow_Click;
            canvas.MouseDown += Canvas_MouseDown;
            canvas.MouseMove += Canvas_MouseMove;
            canvas.MouseUp += Canvas_MouseUp;
            metrCore = new MetrCore(progressBar, textBlockprogress, this.Dispatcher);
            metrCore.coreList.Add(metronome);
            //metrCore.coreList.Add(new MetronomeCore(0.1));
        }

        private void Canvas_MouseUp(object sender, MouseButtonEventArgs e)
        {
            
        }

        private void Canvas_MouseMove(object sender, MouseEventArgs e)
        {
            
        }

        private void Canvas_MouseDown(object sender, MouseButtonEventArgs e)
        {
            
        }

        private void ButtonShow_Click(object sender, RoutedEventArgs e)
        {
            showManager.ReadFile(textBoxOutput.Text);
        }

        private void ButtonEnd_Click(object sender, RoutedEventArgs e)
        {
            
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            //timer.Enabled = true;
            //staticShow();
            metrCore.StartComput(textBoxOutput.Text);
        }

        /*private void staticShow()
        {
            //开始计算，并且展示出图像
            List<double> data = new List<double>();
            for (int i = 0; i < MaxT / deltaT; i++)
            {
                //data.Add(Math.Sin(i * deltaT));
                metronome.afterdeltaT(deltaT);
                data.Add(metronome.getTheta());
                //data.Add(metronome.state);
            }
            //显示图像
            canvas.Children.Clear();
            double deltaX = canvas.ActualWidth / data.Count;
            double Y0 = canvas.ActualHeight / 2;
            double lastX = 0;
            double lastY = Y0;
            for (int i = 0; i < data.Count; i++)
            {
                Line line = new Line();
                line.Stroke = Brushes.Black;
                line.StrokeThickness = 3;
                line.X1 = lastX;
                line.Y1 = lastY;
                lastX += deltaX;
                lastY = Y0 - Y0 * data[i] / MaxTheta;
                line.X2 = lastX;
                line.Y2 = lastY;
                canvas.Children.Add(line);
            }
        }*/

        private void next()
        {

        }
    }
}

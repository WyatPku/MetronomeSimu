using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace metronomeSimu.MetroShow
{
    class MetroShape
    {
        Rectangle rect;
        Ellipse round;
        Canvas canvas;
        double Length = 500;

        public MetroShape(Canvas canvas_, int index = 0)
        {
            canvas = canvas_;
            rect = new Rectangle();
            rect.Fill = Brushes.Gray;
            rect.RadiusX = 15;
            rect.RadiusY = 15;
            rect.Width = Length + 30;
            rect.Height = 30;
            Canvas.SetLeft(rect, 10);
            Canvas.SetTop(rect, 10 + 40 * index);
            canvas.Children.Add(rect);

            for (int i = 0; i < 19; i++) PrintLineAtArc(i * 10, Brushes.Black, index);
            for (int i = 0; i < 7; i++) PrintLineAtArc(i * 30, Brushes.Blue, index, 7);
            PrintLineAtArc(90, Brushes.Yellow, index, 10);

            round = new Ellipse();
            round.Fill = Brushes.Black;
            round.Width = 24;
            round.Height = 24;
            Canvas.SetLeft(round, 13);
            Canvas.SetTop(round, 13 + 40 * index);
            canvas.Children.Add(round);
        }

        private void PrintLineAtArc(double arc, Brush brush, int index, int Thick = 3)
        {
            Line line = new Line();
            line.Stroke = brush;
            line.StrokeThickness = Thick;
            line.Y1 = Canvas.GetTop(rect);
            line.Y2 = Canvas.GetTop(rect) + rect.Height;
            line.X1 = line.X2 = 25 + arc * Length / 180;
            canvas.Children.Add(line);
        }

        //-90 <= arc <= 90
        public void MoveToArc(double arc)
        {
            Canvas.SetLeft(round, 13 + (arc + 90) * Length / 180);
        }
        
    }
}

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

namespace VisualTimeWaste
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        Color getHeat(double reference, double value)
        {
            Color result = new Color();

            value = Math.Min(Math.Max(0, value), reference);
            value /= reference;
            value *= 120;
            value = 120 - value;
            result.A = 255;
            if (false)
            {
                if (value > 119)
                {
                    result.R = 0;
                    result.G = 255;
                    result.B = 0;
                    result.A = 64;
                    return result;
                }
                if (value > 79)
                {
                    result.R = 0;
                    result.G = 255;
                    result.B = 0;
                    result.A = 64;
                    return result;
                }
                if (value > 39)
                {
                    result.R = 255;
                    result.G = 255;
                    result.B = 0;
                    return result;
                }
                if (value > -1)
                {
                    result.R = 255;
                    result.G = 0;
                    result.B = 0;
                    return result;
                }
            }
            else
            {
                if (value > 119)
                {
                    result.R = 0;
                    result.G = (byte)Math.Floor((255 - (((value - 120) / 40) * 255f)));
                    result.B = 255;
                    return result;
                }
                if (value > 79)
                {
                    result.R = 0;
                    result.G = 255;
                    result.B = (byte)Math.Floor((((value - 80) / 40) * 255f));
                    return result;
                }
                if (value > 39)
                {
                    result.R = (byte)Math.Floor((255 - (((value - 40) / 40) * 255f)));
                    result.G = 255;
                    result.B = 0;
                    return result;
                }
                if (value > -1)
                {
                    result.R = 255;
                    result.G = (byte)Math.Floor(((value / 40) * 255f));
                    result.B = 0;
                    return result;
                }
            }
            return result;
        }
        List<Line> line = new List<Line>();
        double[] oldPos = new double[2];
        private void Window_MouseMove(object sender, MouseEventArgs e)
        {
            if (click)
            {
                Point mouse = Mouse.GetPosition(this);
                Thickness pos;
                pos = ret_hori.Margin;
                pos.Top = mouse.Y;
                ret_hori.Margin = pos;
                Thickness pos_;
                pos_ = ret_vert.Margin;
                pos_.Left = mouse.X;
                ret_vert.Margin = pos_;
                line.Add(new Line());
                line[line.Count - 1].HorizontalAlignment = HorizontalAlignment.Left;
                line[line.Count - 1].VerticalAlignment = VerticalAlignment.Center;
                line[line.Count - 1].Stroke = new SolidColorBrush(Color.FromArgb(255, 0, 255, 255));
                line[line.Count - 1].X2 = oldPos[0];
                line[line.Count - 1].Y2 = oldPos[1];
                line[line.Count - 1].X1 = mouse.X;
                line[line.Count - 1].Y1 = mouse.Y;
                line[line.Count - 1].StrokeThickness = 4;
                canvas.Children.Add(line[line.Count - 1]);
                for (int x = 0; x < line.Count; x++)
                {
                    line[x].Opacity -= 0.001;
                    //line[x].Stroke = new SolidColorBrush(getHeat(line.Count - 1, x));
                }
                for (int x = 0; x < line.Count; x++)
                {
                    if (line[x].Opacity <= 0)
                    {
                        line.RemoveAt(x);
                    }
                }
                oldPos[0] = mouse.X;
                oldPos[1] = mouse.Y;
            }
        }
        bool click = false;
        private void Window_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Point mouse = Mouse.GetPosition(this);
            oldPos[0] = mouse.X;
            oldPos[1] = mouse.Y;
            click = true;
        }

        private void Window_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            click = false;
        }
    }
}

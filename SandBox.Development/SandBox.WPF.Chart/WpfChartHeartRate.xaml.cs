using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WpfChart2
{
    /// <summary>
    /// Interaction logic for WpfChartHeartRate.xaml
    /// </summary>
    public partial class WpfChartHeartRate : WpfMultiChart
    {
        public bool DrawHeartZones { get; set; }
        public int MaxHeartRate { get; set; }

        public WpfChartHeartRate()
        {
            InitializeComponent();
        }

        protected void DrawGridlinesAndLabels(Canvas textCanvas, Size size, Point minXY, Point maxXY)
        {

        }
        private void DrawHeartRateBackground(Canvas textCanvas, Size size, double scaleY)
        {

            double offset = size.Height - scaleY * MaxHeartRate;
            double halfHeight = ((size.Height - offset) / 2) + offset;
            double eighthHeight = (size.Height - offset) / 8;

            textCanvas.Children.Add(addRectangle(Colors.Red, 0.25, new Rect() { X = 0, Y = halfHeight - (eighthHeight * 4), Height = eighthHeight, Width = size.Width }));
            textCanvas.Children.Add(addRectangle(Colors.Orange, 0.25, new Rect() { X = 0, Y = halfHeight - (eighthHeight * 3), Height = eighthHeight, Width = size.Width }));
            textCanvas.Children.Add(addRectangle(Colors.Green, 0.25, new Rect() { X = 0, Y = halfHeight - (eighthHeight * 2), Height = eighthHeight, Width = size.Width }));
            textCanvas.Children.Add(addRectangle(Colors.Blue, 0.25, new Rect() { X = 0, Y = halfHeight - eighthHeight, Height = eighthHeight, Width = size.Width }));
            textCanvas.Children.Add(addRectangle(Colors.Gray, 0.25, new Rect() { X = 0, Y = halfHeight, Height = halfHeight - offset, Width = size.Width }));
        }
        private Path addRectangle(Color color, double opacity, Rect rct)
        {
            SolidColorBrush greyBrush = new SolidColorBrush(color);
            greyBrush.Opacity = opacity;


            Path path = new Path();
            path.Fill = greyBrush;

            RectangleGeometry greyRectGeometry = new RectangleGeometry();

            greyRectGeometry.Rect = rct;

            path.Data = greyRectGeometry;

            return path;
        }
    }
}

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
using WpfChart2.TimeSeriesDataLib;
using System.Windows.Threading;
using System.Threading;

namespace WpfChart2
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class Window1 : Window
    {
        public Window1()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            mChart.SetTitle("");
            mChart.SetStaticYLabel("Heart Rate");

            mChart.GridLineVirticalShortFormating += new WpfMultiChart.FormatingLabelDelegate(mChart_GridLineVirticalShortFormating);
            mChart.GridLineVirticalLongFormating += new WpfMultiChart.FormatingLabelDelegate(mChart_GridLineVirticalLongFormating);

            FillSampleData();

        }

        string mChart_GridLineVirticalLongFormating(DateTime dt)
        {
            return dt.ToString("HH:mm:ss");
        }

        string mChart_GridLineVirticalShortFormating(DateTime dt)
        {
            return dt.ToString("HH:mm");
        }

        private void FillSampleData()
        {
            mChart.ClearAllSeries();

            TimeSeriesData data1 = new TimeSeriesData();
            data1.Name = "LG001";
            data1.StrokeColor = Colors.DarkBlue;
            data1.IsAreaMode = false;
            data1.Clear();
            data1.CustomStartTime = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);


            RandomDataSeries ranData1 = new RandomDataSeries(DateTime.Now.Millisecond, data1.CustomStartTime, 100, 4);

            ranData1.ValueScale = 190;
            //ranData2.ValueScale = 400;
            //ranData2.SampleFrequencyPerMin = 0.8;
            //ranData3.ValueScale = 200;
            //ranData3.SampleFrequencyPerMin = 0.5;
                    
            List<TimeSeriesDataPoint> points1 = new List<TimeSeriesDataPoint>();
            ranData1.ReSeed();
            for (int i = 0; i <= 65; i++)
            {
                points1.Add(ranData1.NextDataPoint);
            }
                        
            mChart.AddSeries(data1);

            data1.AddPointsRange(points1.ToArray());
        }

        private void Window_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            WindowSize.Text = String.Format("Height: {0} Width:{1}", e.NewSize.Height, e.NewSize.Width);
        }
    }
}

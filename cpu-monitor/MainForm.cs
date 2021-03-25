using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using System.Diagnostics;

using LiveCharts;
using LiveCharts.Wpf;

using FontAwesome;
using FontAwesome.Sharp;
using System.Windows.Media;

namespace cpu_monitor
{
    public partial class MainForm : Form
    {
        private PerformanceCounter performanceCounter = new PerformanceCounter(
            "Processor Information", "% Processor Time", "_Total");

        private int AllowedPts = 30;
        private int SecondsGone = 0;

        public MainForm()
        {
            InitializeComponent();
            
            this.StartPosition = FormStartPosition.Manual;
            this.Location = new Point(Screen.PrimaryScreen.Bounds.Width - this.Width,
                Screen.PrimaryScreen.Bounds.Height - this.Height - 40);

            TopMost = true;

            var gradientBrush = new LinearGradientBrush
            {
                StartPoint = new System.Windows.Point(0, 0),
                EndPoint = new System.Windows.Point(0, 1)
            };

            gradientBrush.GradientStops.Add(new GradientStop(System.Windows.Media.Color.FromRgb(122, 42, 241), 0));
            gradientBrush.GradientStops.Add(new GradientStop(System.Windows.Media.Color.FromRgb(0, 255, 111), 1));

            MainChart.Series.Add(new LineSeries
            {
                Values = new ChartValues<float>(),
                ScalesYAt = 0,
                PointGeometry = null,
                Fill = gradientBrush,
                StrokeThickness = 1,
                LabelPoint = point => point.Y + " " + "%"
            });

            MainChart.AxisX.Add(new Axis
            {
                Visibility = System.Windows.Visibility.Hidden
            });

            MainChart.AxisY.Add(new Axis
            {
                MinValue = 0.0f,
                MaxValue = 100.0f
            });
        }

        private void AddValToChart(float val)
        {
            if (MainChart.Series[0].Values.Count == AllowedPts)
            {
                MainChart.Series[0].Values.RemoveAt(0);
            }

            MainChart.Series[0].Values.Add(val);
        }

        private void MainTimer_Tick(object sender, EventArgs e)
        {
            AddValToChart(performanceCounter.NextValue());
            SecondsGone++;
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            MainTimer.Start();
        }

        private void ExitBtn_Click(object sender, EventArgs e)
        {
            if (MainTimer.Enabled)
            {
                MainTimer.Stop();
            }

            this.Close();
        }
    }
}

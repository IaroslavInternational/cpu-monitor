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

namespace cpu_monitor
{
    public partial class MainForm : Form
    {
        private PerformanceCounter performanceCounter = new PerformanceCounter(
            "Processor Information", "% Processor Time", "_Total");

        private bool IsSetAxis = false;

        public MainForm()
        {
            InitializeComponent();

            MainChart.Series.Add(new LineSeries
            {
                Values = new ChartValues<float> { 0.0f },
                ScalesYAt = 0
            });
        }

        private void AddValToChart(float val)
        {
            if (MainChart.Series[0].Values.Count == 20)
            {
                MainChart.Series[0].Values.RemoveAt(0);
            }

            if (!IsSetAxis)
            {
                MainChart.AxisY[0].MinValue = 0.0;
                MainChart.AxisY[0].MaxValue = 100.0;

                IsSetAxis = true;
            }

            MainChart.Series[0].Values.Add(val);
        }

        private void MainTimer_Tick(object sender, EventArgs e)
        {
            AddValToChart(performanceCounter.NextValue());
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            MainTimer.Start();
        }
    }
}

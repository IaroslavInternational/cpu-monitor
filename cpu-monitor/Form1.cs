using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using LiveCharts;
using LiveCharts.Charts;
using LiveCharts.Wpf;
using LiveCharts.WinForms;

namespace cpu_monitor
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();

            MainChart.Series.Add(new LineSeries
            {
                Values = new ChartValues<double> { 0.0 },
                ScalesYAt = 0
            });
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            //MainChart.AxisY[0].MinValue = 0.0;
            //MainChart.AxisY[0].MaxValue = 100.0;
        }
    }
}

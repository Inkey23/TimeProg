using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using TimeProgClass;

namespace TimeProg
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            chart1.MouseWheel += chart1_MouseWheel;
        }
        string PathRead = "0", InputFile = "0", PathWrite = "0";

        List<double> Time = new List<double>();
        List<List<double>> AllPoints = new List<List<double>>();
        CommandClass MyCommand = new CommandClass();

        private void LoadButton_Click(object sender, EventArgs e)
        {

            var dlg = new OpenFileDialog();
            dlg.Filter = "txt files (*.txt)|*.txt|seq files (*.seq)|*.seq|All files (*.*)|*.*";
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                MyCommand.Kostil(sender, e);

                PathRead = dlg.FileName;
                using (FileStream fs = File.Open(PathRead, FileMode.Open))
                {
                    byte[] b = new byte[7777024];  //ну да, типа костыль
                    UTF8Encoding temp = new UTF8Encoding(true);

                    while (fs.Read(b, 0, b.Length) > 0)
                    {
                        InputFile = temp.GetString(b);
                    }
                }
                string[] FileSplit = InputFile.Split(':', ';', ' ', '\n');
                for (int i = 0; i < FileSplit.Length; i++)
                {
                    switch (FileSplit[i])
                    {
                        case "LOGic":
                            
                            switch (FileSplit[i + 1])
                            {
                                case "WAIT":
                                    AllPoints = MyCommand.LogWAIT(FileSplit, i);
                                    break;
                                case "WAITPos":
                                    AllPoints = MyCommand.LogWAITPos(FileSplit, i);
                                    
                                    break;
                                case "WAITRate":
                                    
                                    break;
                                case "WAITTemp":
                                    
                                    break;
                                case "WAITGForce":
                                    
                                    break;
                                case "Do":
                                    break;
                                case "LOOPUntil":
                                    break;
                                case "GOTO":
                                    break;
                            }
                            break;
                        case "DEMand":
                            switch (FileSplit[i + 1])
                            {
                                case "POSition":
                                    AllPoints = MyCommand.DemPOSition(FileSplit, i);
                                    break;
                                case "RATe":
                                    break;
                                case "OSCillation":
                                    break;
                                case "CURrent":
                                    break;
                                case "GFOrce":
                                    break;
                                case "TEMP":
                                    break;
                            }
                            break;
                        case "INTerlock":
                            break;
                        case "USER":
                            break;
                        case "ADD":
                            break;
                        default:
                            break;
                    }
                }
            }
        }
        private void SaveButton_Click(object sender, EventArgs e)
        {
            string[] EndSplit = InputFile.Split('\n');
            var dialog = new OpenFileDialog();
            dialog.Filter = "txt files (*.txt)|*.txt|seq files (*.seq)|*.seq|All files (*.*)|*.*";
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                PathWrite = dialog.FileName;
                using (StreamWriter SW = new StreamWriter(PathWrite, false, System.Text.Encoding.Default))
                {
                    for (int i = 0; i < Time.Count; i++)
                    {
                        SW.WriteLine(Convert.ToString(EndSplit[i]) + "Time: " + Convert.ToString(Time[i]));
                    }
                }
            }
        }

        private void Grafik1_Click(object sender, EventArgs e)
        {
            this.chart1.Series[0].Points.Clear();
            this.chart1.Series[1].Points.Clear();
            this.chart1.Series[2].Points.Clear();
            for (int i = 0; i < AllPoints[0].Count; i++)
            {
                this.chart1.Series[0].Points.AddXY(AllPoints[0][i], AllPoints[1][i]);
            }
            
            for (int i = 0; i < AllPoints[2].Count; i++)
            {
                this.chart1.Series[1].Points.AddXY(AllPoints[2][i], AllPoints[3][i]);
            }
            
            for (int i = 0; i < AllPoints[4].Count; i++)
            {
                this.chart1.Series[2].Points.AddXY(AllPoints[4][i], AllPoints[5][i]);
            }

            
        }
        private void chart1_MouseWheel(object sender, MouseEventArgs e)
        {
            chart1.ChartAreas[0].AxisX.ScaleView.Zoomable = true;
            chart1.ChartAreas[0].AxisY.ScaleView.Zoomable = true;
            
            var chart = (System.Windows.Forms.DataVisualization.Charting.Chart)sender;
            var xAxis = chart.ChartAreas[0].AxisX;
            var yAxis = chart.ChartAreas[0].AxisY;
            var xMin = xAxis.ScaleView.ViewMinimum;
            var xMax = xAxis.ScaleView.ViewMaximum;
            var yMin = yAxis.ScaleView.ViewMinimum;
            var yMax = yAxis.ScaleView.ViewMaximum;
            try
            {
                if (e.Delta < 0) // Scrolled down.
                {
                    //xAxis.ScaleView.ZoomReset();
                    //yAxis.ScaleView.ZoomReset();
                    var posXStart = xAxis.PixelPositionToValue(e.Location.X) - (xMax - xMin) * 1.5;
                    var posXFinish = xAxis.PixelPositionToValue(e.Location.X) + (xMax - xMin) * 1.5;
                    var posYStart = yAxis.PixelPositionToValue(e.Location.Y) - (yMax - yMin) * 1.5;
                    var posYFinish = yAxis.PixelPositionToValue(e.Location.Y) + (yMax - yMin) * 1.5;

                    xAxis.ScaleView.Zoom(posXStart, posXFinish);
                    yAxis.ScaleView.Zoom(posYStart, posYFinish);
                }
                else if (e.Delta > 0) // Scrolled up.
                {

                    var posXStart = xAxis.PixelPositionToValue(e.Location.X) - (xMax - xMin) / 3;
                    var posXFinish = xAxis.PixelPositionToValue(e.Location.X) + (xMax - xMin) / 3;
                    var posYStart = yAxis.PixelPositionToValue(e.Location.Y) - (yMax - yMin) / 3;
                    var posYFinish = yAxis.PixelPositionToValue(e.Location.Y) + (yMax - yMin) / 3;

                    xAxis.ScaleView.Zoom(posXStart, posXFinish);
                    yAxis.ScaleView.Zoom(posYStart, posYFinish);
                }
            }
            catch { }
        }
    }
}

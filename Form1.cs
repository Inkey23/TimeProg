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

namespace TimeProg
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            chart1.ChartAreas[0].AxisX.ScaleView.Zoomable = true;
            chart1.ChartAreas[0].AxisY.ScaleView.Zoomable = true;

            chart1.MouseWheel += chart1_MouseWheel;
        }
        string PathRead = "0", InputFile = "0", PathWrite = "0";
        double T = 0, S = 0, V = 0, A = 0, LastPosition = 0, FinalTime = 0;

        double TimeWait = 0, TimePos = 0, RealPosition, RealTime = 0;
        List<double> Time = new List<double>();
        List<double> PointsX = new List<double>();
        List<double> PointsY = new List<double>();
        
        private void LoadButton_Click(object sender, EventArgs e)
        {
            var dlg = new OpenFileDialog();
            dlg.Filter = "txt files (*.txt)|*.txt|seq files (*.seq)|*.seq|All files (*.*)|*.*";
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                PathRead = dlg.FileName;
                using (FileStream fs = File.Open(PathRead, FileMode.Open))
                {
                    byte[] b = new byte[7777024];
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
                                    RealTime = TimeWait + TimePos;
                                    TimeWait += Convert.ToDouble(FileSplit[i + 2]) / 1000;
                                    while (RealTime < TimeWait)
                                    {
                                        PointsX.Add(RealTime);
                                        PointsY.Add(LastPosition);
                                        RealTime++;
                                    }
                                    Time.Add(TimeWait);
                                    break;
                                case "WAITPos":
                                    RealTime = TimeWait + TimePos;
                                    TimeWait += Convert.ToDouble(FileSplit[i + 4]) / 1000;
                                    while (RealTime < TimeWait)
                                    {
                                        PointsX.Add(RealTime);
                                        PointsY.Add(LastPosition);
                                        RealTime++;
                                    }
                                    Time.Add(TimeWait);
                                    break;
                                case "WAITRate":
                                    RealTime = TimeWait + TimePos;
                                    TimeWait += Convert.ToDouble(FileSplit[i + 4]) / 1000;
                                    while (RealTime < TimeWait)
                                    {
                                        PointsX.Add(RealTime);
                                        PointsY.Add(LastPosition);
                                        RealTime++;
                                    }
                                    Time.Add(TimeWait);
                                    break;
                                case "WAITTemp":
                                    RealTime = TimeWait + TimePos;
                                    TimeWait += Convert.ToDouble(FileSplit[i + 4]) * 60;
                                    while (RealTime < TimeWait)
                                    {
                                        PointsX.Add(RealTime);
                                        PointsY.Add(LastPosition);
                                        RealTime++;
                                    }
                                    Time.Add(TimeWait);
                                    break;
                                case "WAITGForce":
                                    RealTime = TimeWait + TimePos;
                                    TimeWait += Convert.ToDouble(FileSplit[i + 4]) * 60;
                                    while (RealTime < TimeWait)
                                    {
                                        PointsX.Add(RealTime);
                                        PointsY.Add(LastPosition);
                                        RealTime++;
                                    }
                                    Time.Add(TimeWait);
                                    break;
                                case "Do":
                                    Time.Add(TimeWait);
                                    break;
                                case "LOOPUntil":
                                    Time.Add(TimeWait);
                                    break;
                                case "GOTO":
                                    Time.Add(TimeWait);
                                    break;
                            }
                            break;
                        case "DEMand":
                            switch (FileSplit[i + 1])
                            {
                                case "POSition":
                                    S = Math.Abs(Convert.ToDouble(FileSplit[i + 3]) - LastPosition);
                                    V = Math.Abs(Convert.ToDouble(FileSplit[i + 4]));
                                    A = Math.Abs(Convert.ToDouble(FileSplit[i + 5]));
                                    RealPosition = LastPosition;
                                    LastPosition = Convert.ToDouble(FileSplit[i + 3]);
                                    if (S <= (V * V / (2 * A)))     //Если движение только равноускоренное
                                    {
                                        TimePos = Math.Sqrt(2 * S / A);
                                        for (int j = 0; j < TimePos; j += 1)
                                        {
                                            PointsX.Add(TimeWait + j);
                                            PointsY.Add(RealPosition + A*j*j/2);
                                        }
                                        
                                        T += Math.Sqrt(2 * S / A);
                                        Time.Add(T);
                                    }
                                    else
                                    {
                                        TimePos = (V / A) + ((S - (V * V / (2 * A))) / V);
                                        for (int j = 0; j < TimePos; j += 1)
                                        {
                                            if ((RealPosition + A * j * j / 2) <= LastPosition)
                                            {
                                                PointsX.Add(TimeWait + j);
                                                PointsY.Add(RealPosition + A * j * j / 2);
                                            }
                                            else
                                            {
                                                PointsX.Add(TimeWait + j);
                                                PointsY.Add(LastPosition);
                                            }
                                        }
                                        
                                        T += (V / A) + ((S - (V * V / (2 * A))) / V);
                                        Time.Add(T);
                                    }
                                    break;
                                case "RATe":
                                    Time.Add(T);
                                    break;
                                case "OSCillation":
                                    Time.Add(T);
                                    break;
                                case "CURrent":
                                    Time.Add(T);
                                    break;
                                case "GFOrce":
                                    Time.Add(T);
                                    break;
                                case "TEMP":
                                    Time.Add(T);
                                    break;
                            }
                            break;
                        case "INTerlock":
                            Time.Add(T);
                            break;
                        case "USER":
                            Time.Add(T);
                            break;
                        case "ADD":
                            Time.Add(T);
                            break;
                        default:
                            break;
                    }
                }
                FinalTime = TimeWait;
                ScrollBarXmax.Minimum = ScrollBarXmin.Minimum = 0;
                ScrollBarXmax.Maximum = ScrollBarXmin.Maximum = (int)PointsX[Convert.ToInt32(PointsX.Count) - 1];
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
            
            if (ScrollBarXmax.Value > ScrollBarXmin.Value)
            {
                chart1.ChartAreas[0].AxisX.Minimum = ScrollBarXmin.Value;
                chart1.ChartAreas[0].AxisX.Maximum = ScrollBarXmax.Value;
                
            }
            for (int i = 0; i < PointsX.Count; i++)
            {
                this.chart1.Series[0].Points.AddXY(PointsX[i], PointsY[i]);
            }
        }
        private void chart1_MouseWheel(object sender, MouseEventArgs e)
        {
            var chart = (System.Windows.Forms.DataVisualization.Charting.Chart)sender;
            var xAxis = chart.ChartAreas[0].AxisX;
            var yAxis = chart.ChartAreas[0].AxisY;

            try
            {
                if (e.Delta < 0) // Scrolled down.
                {
                    xAxis.ScaleView.ZoomReset();
                    yAxis.ScaleView.ZoomReset();
                }
                else if (e.Delta > 0) // Scrolled up.
                {
                    var xMin = xAxis.ScaleView.ViewMinimum;
                    var xMax = xAxis.ScaleView.ViewMaximum;
                    var yMin = yAxis.ScaleView.ViewMinimum;
                    var yMax = yAxis.ScaleView.ViewMaximum;

                    var posXStart = xAxis.PixelPositionToValue(e.Location.X) - (xMax - xMin) / 4;
                    var posXFinish = xAxis.PixelPositionToValue(e.Location.X) + (xMax - xMin) / 4;
                    var posYStart = yAxis.PixelPositionToValue(e.Location.Y) - (yMax - yMin) / 4;
                    var posYFinish = yAxis.PixelPositionToValue(e.Location.Y) + (yMax - yMin) / 4;

                    xAxis.ScaleView.Zoom(posXStart, posXFinish);
                    yAxis.ScaleView.Zoom(posYStart, posYFinish);
                }
            }
            catch { }
        }
    }
}

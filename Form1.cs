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
            chart1.MouseWheel += chart1_MouseWheel;
        }

        string PathRead = "0", InputFile = "0", PathWrite = "0";
        double T = 0, S = 0, V = 0, A = 0, LastPosition = 0, FinalTime = 0;

        double TimeWait = 0, TimePos = 0, RealPosition, RealTime = 0;
        List<double> Time = new List<double>();
        
        List<double> PointsY = new List<double>();
        List<double> PointsX = new List<double>();
        List<double> PointsX1 = new List<double>();
        List<double> PointsY1 = new List<double>();
        List<double> PointsX2 = new List<double>();
        List<double> PointsY2 = new List<double>();
        List<double> PointsX3 = new List<double>();
        List<double> PointsY3 = new List<double>();

        private void LoadButton_Click(object sender, EventArgs e)
        {
            var dlg = new OpenFileDialog();
            dlg.Filter = "txt files (*.txt)|*.txt|seq files (*.seq)|*.seq|All files (*.*)|*.*";
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                PointsX1.Clear();
                PointsY1.Clear();
                PointsX2.Clear();
                PointsY2.Clear();
                PointsX3.Clear();
                PointsY3.Clear();
                
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
                                case "POSition": // надо создать отдельный класс, где пропишу все команды, а то читать невозможно
                                    S = Math.Abs(Convert.ToDouble(FileSplit[i + 3]) - LastPosition);
                                    V = Math.Abs(Convert.ToDouble(FileSplit[i + 4]));
                                    A = Math.Abs(Convert.ToDouble(FileSplit[i + 5]));
                                    RealPosition = LastPosition;
                                    LastPosition = Convert.ToDouble(FileSplit[i + 3]);
                                    switch (FileSplit[i + 2])
                                    {
                                        case "1":
                                            if (S <= (V * V / (2 * A)))     //Если движение только равноускоренное
                                            {
                                                TimePos = Math.Sqrt(2 * S / A);
                                                for (int j = 0; j < TimePos; j += 1)
                                                {
                                                    PointsX.Add(TimeWait + j);
                                                    PointsY.Add(RealPosition + A * j * j / 2);
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
                                        case "2":
                                            break;
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
            

            for (int i = 0; i < PointsX.Count; i++)
            {
                this.chart1.Series[0].Points.AddXY(PointsX[i], PointsY[i]);
            }
            
            for (int i = 0; i < PointsX1.Count; i++)
            {
                this.chart1.Series[0].Points.AddXY(PointsX1[i], PointsY1[i]);
            }
            for (int i = 0; i < PointsX2.Count; i++)
            {
                this.chart1.Series[1].Points.AddXY(PointsX2[i], PointsY2[i]);
            }
            for (int i = 0; i < PointsX3.Count; i++)
            {
                this.chart1.Series[2].Points.AddXY(PointsX3[i], PointsY3[i]);
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

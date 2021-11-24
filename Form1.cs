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
        CommandClass MyCommand = new CommandClass();

        string PathRead = "0", InputFile = "0", PathWrite = "0";
        List<double> Time = new List<double>();
        
        
        private void LoadButton_Click(object sender, EventArgs e)
        {
            var dlg = new OpenFileDialog();
            dlg.Filter = "txt files (*.txt)|*.txt|seq files (*.seq)|*.seq|All files (*.*)|*.*";
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                #region Clear
                MyCommand.PointsX1.Clear();
                MyCommand.PointsY1.Clear();
                MyCommand.PointsX2.Clear();
                MyCommand.PointsY2.Clear();
                MyCommand.PointsX3.Clear();
                MyCommand.PointsY3.Clear();
                MyCommand.AbsolutTime = 0;
                MyCommand.LastPosition[0] = 0;
                MyCommand.LastPosition[1] = 0;
                MyCommand.LastPosition[2] = 0;
                MyCommand.TimePos[0] = 0;
                MyCommand.TimePos[1] = 0;
                MyCommand.TimePos[2] = 0;
                #endregion
                #region Loading
                PathRead = dlg.FileName;
                using (FileStream fs = File.Open(PathRead, FileMode.Open))
                {
                    byte[] b = new byte[7777024];  // Ну да, типа костыль
                    UTF8Encoding temp = new UTF8Encoding(true);

                    while (fs.Read(b, 0, b.Length) > 0)
                    {
                        InputFile = temp.GetString(b);
                    }
                }
                string[] FileSplit = InputFile.Split(':', ';', ' ', '\n');
                #endregion
                #region Commands
                for (int i = 0; i < FileSplit.Length; i++)
                {
                    switch (FileSplit[i])
                    {
                        case "LOGic":
                            switch (FileSplit[i + 1])
                            {
                                case "WAIT":
                                    MyCommand.LogWAIT(FileSplit, i);
                                    break;
                                case "WAITPos":
                                    MyCommand.LogWAITPos(FileSplit, i);
                                    break;
                                case "WAITRate":
                                    MyCommand.LogWAITRate(FileSplit, i);
                                    break;
                                case "WAITTemp":
                                    MyCommand.LogWAITTemp(FileSplit, i);
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
                                    MyCommand.DemPOSition(FileSplit, i);
                                    break;
                                case "RATe":
                                    MyCommand.DemRATe(FileSplit, i);
                                    break;
                                case "OSCillation":
                                    MyCommand.DemOSCillation(FileSplit, i);//Пока пусто
                                    break;
                                case "CURrent":
                                    break;
                                case "GFOrce":
                                    break;
                                case "TEMP":
                                    MyCommand.DemTEMP(FileSplit, i);
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
                #endregion
                #region AddPoints
                MyCommand.Counter[0] = MyCommand.PointsX1.Count;
                for (int g = 0; g < MyCommand.Counter[0] - 1; g++)
                {
                    if (MyCommand.PointsX1[g + 1] - MyCommand.PointsX1[g] != 1)
                    {
                        for (double k = 1; k < (MyCommand.PointsX1[g + 1] - MyCommand.PointsX1[g]); k++)
                        {
                            MyCommand.PointsX1.Add(MyCommand.PointsX1[g] + k);
                            MyCommand.PointsY1.Add(MyCommand.PointsY1[g]);
                        }

                    }
                }
                MyCommand.Counter[1] = MyCommand.PointsX2.Count;
                for (int g = 0; g < MyCommand.Counter[1] - 1; g++)
                {
                    if (MyCommand.PointsX2[g + 1] - MyCommand.PointsX2[g] != 1)
                    {
                        for (double k = 1; k < (MyCommand.PointsX2[g + 1] - MyCommand.PointsX2[g]); k++)
                        {
                            MyCommand.PointsX2.Add(MyCommand.PointsX2[g] + k);
                            MyCommand.PointsY2.Add(MyCommand.PointsY2[g]);
                        }

                    }
                }
                /*
                MyCommand.Counter[2] = MyCommand.PointsX3.Count;
                for (int g = 0; g < MyCommand.Counter[2] - 1; g++)
                {
                    if (MyCommand.PointsX3[g + 1] - MyCommand.PointsX3[g] != 1)
                    {
                        for (double k = 1; k < (MyCommand.PointsX3[g + 1] - MyCommand.PointsX3[g]); k++)
                        {
                            MyCommand.PointsX3.Add(MyCommand.PointsX3[g] + k);
                            MyCommand.PointsY3.Add(MyCommand.PointsY3[g]);
                        }

                    }
                }
                */
                #endregion
            }
        }
        private void SaveButton_Click(object sender, EventArgs e)
        {
            #region Save
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
            #endregion
        }

        private void Grafik1_Click(object sender, EventArgs e)
        {
            chart1.Name = "asd";
            
            #region Drawing
            this.chart1.Series[0].Points.Clear();
            this.chart1.Series[1].Points.Clear();
            this.chart1.Series[2].Points.Clear();
            for (int i = 0; i < MyCommand.PointsX1.Count; i++)
            {
                this.chart1.Series[0].Points.AddXY(MyCommand.PointsX1[i], MyCommand.PointsY1[i]);
            }
            
            for (int i = 0; i < MyCommand.PointsX2.Count; i++)
            {
                this.chart1.Series[1].Points.AddXY(MyCommand.PointsX2[i], MyCommand.PointsY2[i]);
            }
            
            for (int i = 0; i < MyCommand.PointsX3.Count; i++)
            {
                this.chart1.Series[2].Points.AddXY(MyCommand.PointsX3[i], MyCommand.PointsY3[i]);
            }
            #endregion
        }
        private void chart1_MouseWheel(object sender, MouseEventArgs e)
        {
            #region Zoom
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
            #endregion
        }
    }
}

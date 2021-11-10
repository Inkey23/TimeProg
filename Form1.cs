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
        double T = 0, S = 0, V = 0, A = 0, LastPosition1 = 0, LastPosition2 = 0, LastPosition3 = 0;

        double AbsolutTime = 0, TimePos1 = 0, TimePos2 = 0, TimePos3 = 0, RealPosition1 = 0, RealPosition2 = 0, RealPosition3 = 0, RealTime1 = 0, RealTime2 = 0, RealTime3 = 0, NewPos = 0, J = 0;

        List<double> PointsX1 = new List<double>();
        List<double> PointsY1 = new List<double>();
        List<double> PointsX2 = new List<double>();
        List<double> PointsY2 = new List<double>();
        List<double> PointsX3 = new List<double>();
        List<double> PointsY3 = new List<double>();
        List<double> Time = new List<double>();

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
                                    RealTime1 = AbsolutTime + TimePos1;
                                    RealTime2 = AbsolutTime + TimePos2;
                                    RealTime3 = AbsolutTime + TimePos3;
                                    AbsolutTime += Convert.ToDouble(FileSplit[i + 2]) / 1000;
                                    while (RealTime1 < AbsolutTime)
                                    {
                                        PointsX1.Add(RealTime1);
                                        PointsY1.Add(LastPosition1);
                                        RealTime1++;
                                    }
                                    while (RealTime2 < AbsolutTime)
                                    {
                                        PointsX2.Add(RealTime2);
                                        PointsY2.Add(LastPosition2);
                                        RealTime2++;
                                    }
                                    while (RealTime3 < AbsolutTime)
                                    {
                                        PointsX3.Add(RealTime3);
                                        PointsY3.Add(LastPosition3);
                                        RealTime3++;
                                    }
                                    break;
                                case "WAITPos":
                                    switch (FileSplit[i + 2])
                                    {
                                        case "1":
                                            RealTime1 = AbsolutTime + TimePos1;
                                            AbsolutTime += (TimePos1 + Convert.ToDouble(FileSplit[i + 4]) / 1000);
                                            while (RealTime1 < AbsolutTime)
                                            {
                                                PointsX1.Add(RealTime1);
                                                PointsY1.Add(LastPosition1);
                                                RealTime1++;
                                            }
                                            break;
                                        case "2":
                                            RealTime2 = AbsolutTime + TimePos2;
                                            AbsolutTime += (TimePos2 + Convert.ToDouble(FileSplit[i + 4]) / 1000);
                                            while (RealTime2 < AbsolutTime)
                                            {
                                                PointsX2.Add(RealTime2);
                                                PointsY2.Add(LastPosition2);
                                                RealTime2++;
                                            }
                                            break;
                                        case "3":
                                            RealTime3 = AbsolutTime + TimePos3;
                                            AbsolutTime += (TimePos3 + Convert.ToDouble(FileSplit[i + 4]) / 1000);
                                            while (RealTime3 < AbsolutTime)
                                            {
                                                PointsX3.Add(RealTime3);
                                                PointsY3.Add(LastPosition3);
                                                RealTime3++;
                                            }
                                            break;
                                    }
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
                                case "POSition": // надо создать отдельный класс, где пропишу все команды, а то читать невозможно
                                    V = Math.Abs(Convert.ToDouble(FileSplit[i + 4]));
                                    A = Math.Abs(Convert.ToDouble(FileSplit[i + 5]));
                                    switch (FileSplit[i + 2])
                                    {
                                        case "1":
                                            S = Math.Abs(Convert.ToDouble(FileSplit[i + 3]) - LastPosition1);
                                            RealPosition1 = LastPosition1;
                                            LastPosition1 = Convert.ToDouble(FileSplit[i + 3]);
                                            if (S <= (V * V / (2 * A)))     //Если движение только равноускоренное
                                            {
                                                TimePos1 = Math.Sqrt(2 * S / A);
                                                for (int j = 0; j < TimePos1; j ++)
                                                {
                                                    if (RealPosition1 <= LastPosition1)
                                                    {
                                                        PointsX1.Add(AbsolutTime + j);
                                                        PointsY1.Add(RealPosition1 + A * j * j / 2);
                                                    }
                                                    else
                                                    {
                                                        PointsX1.Add(AbsolutTime + j);
                                                        PointsY1.Add(RealPosition1 - A * j * j / 2);
                                                    }

                                                }
                                            }
                                            else
                                            {
                                                TimePos1 = (V / A) + ((S - (V * V / (2 * A))) / V);
                                                for (int j = 0; j < TimePos1; j ++)
                                                {
                                                    if (A * j <= V)
                                                    {
                                                        if(RealPosition1 <= LastPosition1)
                                                        {
                                                            PointsX1.Add(AbsolutTime + j);
                                                            PointsY1.Add(RealPosition1 + A * j * j / 2);
                                                            NewPos = RealPosition1 + A * j * j / 2;
                                                            J = j;
                                                        }
                                                        else
                                                        {
                                                            PointsX1.Add(AbsolutTime + j);
                                                            PointsY1.Add(RealPosition1 - A * j * j / 2);
                                                            NewPos = RealPosition1 - A * j * j / 2;
                                                            J = j;
                                                        }
                                                        
                                                    }
                                                    else
                                                    {
                                                        PointsX1.Add(AbsolutTime + j);
                                                        PointsY1.Add(NewPos + V * (j - J));
                                                    }
                                                }
                                            }
                                            break;
                                        case "2":
                                            S = Math.Abs(Convert.ToDouble(FileSplit[i + 3]) - LastPosition2);
                                            RealPosition2 = LastPosition2;
                                            LastPosition2 = Convert.ToDouble(FileSplit[i + 3]);
                                            if (S <= (V * V / (2 * A)))     //Если движение только равноускоренное
                                            {
                                                TimePos2 = Math.Sqrt(2 * S / A);
                                                for (int j = 0; j < TimePos2; j++)
                                                {
                                                    if (RealPosition2 <= LastPosition2)
                                                    {
                                                        PointsX2.Add(AbsolutTime + j);
                                                        PointsY2.Add(RealPosition2 + A * j * j / 2);
                                                    }
                                                    else
                                                    {
                                                        PointsX2.Add(AbsolutTime + j);
                                                        PointsY2.Add(RealPosition2 - A * j * j / 2);
                                                    }
                                                }
                                            }
                                            else
                                            {
                                                TimePos2 = (V / A) + ((S - (V * V / (2 * A))) / V);
                                                for (int j = 0; j < TimePos2; j++)
                                                {
                                                    if (A * j <= V)
                                                    {
                                                        if (RealPosition2 <= LastPosition2)
                                                        {
                                                            PointsX2.Add(AbsolutTime + j);
                                                            PointsY2.Add(RealPosition2 + A * j * j / 2);
                                                            NewPos = RealPosition2 + A * j * j / 2;
                                                            J = j;
                                                        }
                                                        else
                                                        {
                                                            PointsX2.Add(AbsolutTime + j);
                                                            PointsY2.Add(RealPosition2 - A * j * j / 2);
                                                            NewPos = RealPosition2 - A * j * j / 2;
                                                            J = j;
                                                        }

                                                    }
                                                    else
                                                    {
                                                        PointsX1.Add(AbsolutTime + j);
                                                        PointsY1.Add(NewPos + V * (j - J));
                                                    }
                                                }
                                            }
                                            break;
                                        case "3":
                                            S = Math.Abs(Convert.ToDouble(FileSplit[i + 3]) - LastPosition3);
                                            RealPosition3 = LastPosition3;
                                            LastPosition3 = Convert.ToDouble(FileSplit[i + 3]);
                                            if (S <= (V * V / (2 * A)))     //Если движение только равноускоренное
                                            {
                                                TimePos3 = Math.Sqrt(2 * S / A);
                                                for (int j = 0; j < TimePos3; j++)
                                                {
                                                    if (RealPosition3 <= LastPosition3)
                                                    {
                                                        PointsX3.Add(AbsolutTime + j);
                                                        PointsY3.Add(RealPosition3 + A * j * j / 2);
                                                    }
                                                    else
                                                    {
                                                        PointsX3.Add(AbsolutTime + j);
                                                        PointsY3.Add(RealPosition3 - A * j * j / 2);
                                                    }
                                                }
                                            }
                                            else
                                            {
                                                TimePos3 = (V / A) + ((S - (V * V / (2 * A))) / V);
                                                for (int j = 0; j < TimePos3; j++)
                                                {
                                                    if (A * j <= V)
                                                    {
                                                        if (RealPosition3 <= LastPosition3)
                                                        {
                                                            PointsX3.Add(AbsolutTime + j);
                                                            PointsY3.Add(RealPosition3 + A * j * j / 2);
                                                            NewPos = RealPosition3 + A * j * j / 2;
                                                            J = j;
                                                        }
                                                        else
                                                        {
                                                            PointsX3.Add(AbsolutTime + j);
                                                            PointsY3.Add(RealPosition3 - A * j * j / 2);
                                                            NewPos = RealPosition3 - A * j * j / 2;
                                                            J = j;
                                                        }

                                                    }
                                                    else
                                                    {
                                                        PointsX3.Add(AbsolutTime + j);
                                                        PointsY3.Add(NewPos + V * (j - J));
                                                    }
                                                }
                                            }
                                            break;
                                    }
                                    
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

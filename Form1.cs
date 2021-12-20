﻿using System;
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
            GrafikTable.MouseWheel += chart1_MouseWheel;
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
                MyCommand.PointsX1.Add(0);
                MyCommand.PointsY1.Clear();
                MyCommand.PointsY1.Add(0);
                MyCommand.PointsX2.Clear();
                MyCommand.PointsX2.Add(0);
                MyCommand.PointsY2.Clear();
                MyCommand.PointsY2.Add(0);
                MyCommand.PointsX3.Clear();
                MyCommand.PointsX3.Add(0);
                MyCommand.PointsY3.Clear();
                MyCommand.PointsY3.Add(20);
                MyCommand.RateY1.Clear();
                MyCommand.RateY1.Add(0);
                MyCommand.RateY2.Clear();
                MyCommand.RateY2.Add(0);
                MyCommand.RateY3.Clear();
                MyCommand.RateY3.Add(0);
                MyCommand.AbsolutTime = 0;
                MyCommand.LastPosition[0] = 0;
                MyCommand.LastPosition[1] = 0;
                MyCommand.LastPosition[2] = 20;
                MyCommand.TimePos[0] = 0;
                MyCommand.TimePos[1] = 0;
                MyCommand.TimePos[2] = 0;
                MyCommand.LastRate[0] = 0;
                MyCommand.LastRate[1] = 0;
                MyCommand.TimeRate[0] = 0;
                MyCommand.TimeRate[1] = 0;
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
                InputFile = InputFile.ToLower();
                string[] FileSplit = InputFile.Split(':', ';', ' ', '\n');
                
                #endregion
                #region Commands
                for (int i = 0; i < FileSplit.Length; i++)
                {
                    switch (FileSplit[i])
                    {
                        case ("logic"):
                            switch (FileSplit[i + 1])
                            {
                                case "wait":
                                    MyCommand.LogWAIT(FileSplit, i);
                                    break;
                                case "waitpos":
                                    MyCommand.LogWAITPos(FileSplit, i);
                                    break;
                                case "waitrate":
                                    MyCommand.LogWAITRate(FileSplit, i);
                                    break;
                                case "waittemp":
                                    MyCommand.LogWAITTemp(FileSplit, i);
                                    break;
                                case "waitgforce":
                                    MyCommand.ErrorFlag = true;
                                    Console.WriteLine("Обработка команды waitgforce находится в стадии разработки");
                                    break;
                                case "do":
                                    MyCommand.ErrorFlag = true;
                                    Console.WriteLine("Обработка команды do находится в стадии разработки");
                                    break;
                                case "loopuntil":
                                    MyCommand.ErrorFlag = true;
                                    Console.WriteLine("Обработка команды loopuntil находится в стадии разработки");
                                    break;
                                case "goto":
                                    MyCommand.ErrorFlag = true;
                                    Console.WriteLine("Обработка команды goto находится в стадии разработки");
                                    break;
                            }
                            break;
                        case "demand":
                            switch (FileSplit[i + 1])
                            {
                                case "position":
                                    MyCommand.DemPOSition(FileSplit, i);
                                    break;
                                case "rate":
                                    MyCommand.DemRATe(FileSplit, i);
                                    break;
                                case "oscillation":
                                    MyCommand.DemOSCillation(FileSplit, i);
                                    break;
                                case "current":
                                    MyCommand.ErrorFlag = true;
                                    Console.WriteLine("Обработка команды current находится в стадии разработки");
                                    break;
                                case "gforce":
                                    MyCommand.ErrorFlag = true;
                                    Console.WriteLine("Обработка команды gforce находится в стадии разработки");
                                    break;
                                case "temp":
                                    MyCommand.DemTEMP(FileSplit, i);
                                    break;
                            }
                            break;
                        case "interlock":
                            MyCommand.ErrorFlag = true;
                            Console.WriteLine("Обработка команды interlock находится в стадии разработки");
                            break;
                        case "user":
                            MyCommand.ErrorFlag = true;
                            Console.WriteLine("Обработка команды user находится в стадии разработки");
                            break;
                        case "add":
                            MyCommand.ErrorFlag = true;
                            Console.WriteLine("Обработка команды add находится в стадии разработки");
                            break;
                        default:
                            break;
                    }
                }
                #endregion
                #region Points processing
                for (int i = 0; i < MyCommand.PointsX1.Count - 1; i++)
                {
                    if (MyCommand.PointsY1[i] - MyCommand.PointsY1[i + 1] > 10000)
                    {
                        MyCommand.PointsY1[i + 1] = MyCommand.PointsY1[i + 1] + 11520;
                    }
                    else if (MyCommand.PointsY1[i] - MyCommand.PointsY1[i + 1] < -10000)
                    {
                        MyCommand.PointsY1[i + 1] = MyCommand.PointsY1[i + 1] - 11520;
                    }
                }
                for (int i = 0; i < MyCommand.PointsX2.Count - 1; i++)
                {
                    if (MyCommand.PointsY2[i] - MyCommand.PointsY2[i + 1] > 10000)
                    {
                        MyCommand.PointsY2[i + 1] = MyCommand.PointsY2[i + 1] + 11520;
                    }
                    else if (MyCommand.PointsY2[i] - MyCommand.PointsY2[i + 1] < -10000)
                    {
                        MyCommand.PointsY2[i + 1] = MyCommand.PointsY2[i + 1] - 11520;
                    }
                }
                #endregion
                #region AddPoints
                double LastTime;
                if ((MyCommand.PointsX1[MyCommand.PointsX1.Count - 1] >= MyCommand.PointsX2[MyCommand.PointsX2.Count - 1]) && (MyCommand.PointsX1[MyCommand.PointsX1.Count - 1] >= MyCommand.PointsX3[MyCommand.PointsX3.Count - 1]))
                {
                    LastTime = MyCommand.PointsX1[MyCommand.PointsX1.Count - 1];
                }
                else if ((MyCommand.PointsX2[MyCommand.PointsX2.Count - 1] >= MyCommand.PointsX1[MyCommand.PointsX1.Count - 1]) && (MyCommand.PointsX2[MyCommand.PointsX2.Count - 1] >= MyCommand.PointsX3[MyCommand.PointsX3.Count - 1]))
                {
                    LastTime = MyCommand.PointsX2[MyCommand.PointsX2.Count - 1];
                }
                else
                {
                    LastTime = MyCommand.PointsX3[MyCommand.PointsX3.Count - 1];
                }
                MyCommand.PointsX1.Add(LastTime);
                MyCommand.PointsY1.Add(MyCommand.LastPosition[0]);
                MyCommand.RateY1.Add(0);
                MyCommand.PointsX2.Add(LastTime);
                MyCommand.PointsY2.Add(MyCommand.LastPosition[1]);
                MyCommand.RateY2.Add(0);
                MyCommand.PointsX3.Add(LastTime);
                MyCommand.PointsY3.Add(MyCommand.LastPosition[2]);
                MyCommand.RateY3.Add(0);

                /*
                MyCommand.Counter[0] = MyCommand.PointsX1.Count;
                for (int g = 0; g < MyCommand.Counter[0] - 1; g++)
                {
                    if (MyCommand.PointsX1[g + 1] - MyCommand.PointsX1[g] != 1)
                    {
                        for (double k = 1; k < (MyCommand.PointsX1[g + 1] - MyCommand.PointsX1[g]); k++)
                        {
                            MyCommand.PointsX1.Add(MyCommand.PointsX1[g] + k);
                            MyCommand.PointsY1.Add(MyCommand.PointsY1[g]);
                            MyCommand.RateY1.Add(0);
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
                            MyCommand.RateY2.Add(0);
                        }

                    }
                }
                
                MyCommand.Counter[2] = MyCommand.PointsX3.Count;
                for (int g = 0; g < MyCommand.Counter[2] - 1; g++)
                {
                    if (MyCommand.PointsX3[g + 1] - MyCommand.PointsX3[g] != 1)
                    {
                        for (double k = 1; k < (MyCommand.PointsX3[g + 1] - MyCommand.PointsX3[g]); k++)
                        {
                            MyCommand.PointsX3.Add(MyCommand.PointsX3[g] + k);
                            MyCommand.PointsY3.Add(MyCommand.PointsY3[g]);
                            MyCommand.RateY3.Add(0);
                        }
                    }
                }
                */
                #endregion 
            }
        }
        private void SaveButton_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Данная команда находится в стадии разработки");
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
            if(MyCommand.ErrorFlag == true)
            {
                MessageBox.Show("Были обнаружены ошибки скрипта. Пожалуйста, проверьте консоль");
            }
            GrafikTable.Titles.Remove(GrafikTable.Titles[1]);
            GrafikTable.Titles.Add(PathRead);
            GrafikTable.ChartAreas[0].AxisX.Minimum = 0;
            GrafikTable.ChartAreas[0].AxisX.Title = "Время (сек)";
            #region Drawing
            this.GrafikTable.Series[0].Points.Clear();
            this.GrafikTable.Series[1].Points.Clear();
            this.GrafikTable.Series[2].Points.Clear();
            if (CheckRate.Checked)
            {
                GrafikTable.ChartAreas[0].AxisY.Title = "Угл.скорость (град/сек)";
                GrafikTable.ChartAreas[0].AxisY2.Title = "Темп.скорость (°С/мин)";
                if (CheckHorizontal.Checked)
                {
                    for (int i = 0; i < MyCommand.PointsX1.Count; i++)
                    {
                        this.GrafikTable.Series[0].Points.AddXY(MyCommand.PointsX1[i], MyCommand.RateY1[i]);
                    }
                }
                if (CheckVertical.Checked)
                {
                    for (int i = 0; i < MyCommand.PointsX2.Count; i++)
                    {
                        this.GrafikTable.Series[1].Points.AddXY(MyCommand.PointsX2[i], MyCommand.RateY2[i]);
                    }
                }
                if (CheckTemperature.Checked)
                {
                    for (int i = 0; i < MyCommand.PointsX3.Count; i++)
                    {
                        this.GrafikTable.Series[2].Points.AddXY(MyCommand.PointsX3[i], MyCommand.RateY3[i]);
                    }
                }
            }
            else
            {
                GrafikTable.ChartAreas[0].AxisY.Title = "Угол (град)";
                GrafikTable.ChartAreas[0].AxisY2.Title = "Температура (°С)";
                if (CheckHorizontal.Checked)
                {
                    for (int i = 0; i < MyCommand.PointsX1.Count; i++)
                    {
                        this.GrafikTable.Series[0].Points.AddXY(MyCommand.PointsX1[i], MyCommand.PointsY1[i]);
                    }
                }
                if (CheckVertical.Checked)
                {
                    for (int i = 0; i < MyCommand.PointsX2.Count; i++)
                    {
                        this.GrafikTable.Series[1].Points.AddXY(MyCommand.PointsX2[i], MyCommand.PointsY2[i]);
                    }
                }
                if (CheckTemperature.Checked)
                {
                    for (int i = 0; i < MyCommand.PointsX3.Count; i++)
                    {
                        this.GrafikTable.Series[2].Points.AddXY(MyCommand.PointsX3[i], MyCommand.PointsY3[i]);
                    }
                }
            }
            
            #endregion
        }
        private void chart1_MouseWheel(object sender, MouseEventArgs e)
        {
            #region Zoom
            GrafikTable.ChartAreas[0].AxisX.ScaleView.Zoomable = true;
            GrafikTable.ChartAreas[0].AxisY.ScaleView.Zoomable = true;
            
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
        private void button1_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                MessageBox.Show("Вы нажали Левую кнопку, координаты курсора - " + e.Location);
            }
        }
        private void chart1_Click(object sender, EventArgs e)
        {

        }
    }
}

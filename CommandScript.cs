using System;
using System.Collections.Generic;


namespace TimeProgClass
{   //(Важные)  Выгрузка - общую ось времени.     
    //(Средние) Возможность менять частоту построения с 1сек на другое??? Добавить прокрутку 2 оси. 
    //(Бесполезные) Вывод ошибок на экран.  
    public class CommandClass
    {
        public int Case = 0;
        public bool OSCflag = false, ErrorFlag = false, Inversion = false;
        public double S = 0, V = 0, A = 0, AbsolutTime = 0, NewPos = 0, J = 0;
        public double[] LastRate = new double[] { 0, 0 };
        public double[] RealRate = new double[] { 0, 0 };
        public double[] TimeRate = new double[] { 0, 0 };
        public double[] RealPosition = new double[] { 0, 0, 0 };
        public double[] LastPosition = new double[] { 0, 0, 20 };
        public double[] TimePos = new double[] { 0, 0, 0 };
        public double[] RealTime = new double[] { 0, 0, 0 };
        public double[] OSCfreq = new double[] { 0, 0 };
        public double[] OSCampl = new double[] { 0, 0 };
        public int[] Counter = new int[] { 0, 0, 0 };
        public List<double> PointsX1 = new List<double>();
        public List<double> PointsY1 = new List<double>();
        public List<double> PointsX2 = new List<double>();
        public List<double> PointsY2 = new List<double>();
        public List<double> PointsX3 = new List<double>();
        public List<double> PointsY3 = new List<double>();
        public List<double> RateY1 = new List<double>();
        public List<double> RateY2 = new List<double>();
        public List<double> RateY3 = new List<double>();
        public List<double> ActivePointsX = new List<double>();
        public List<double> ActivePointsY = new List<double>();
        public List<double> ActiveRateY = new List<double>();

        public void DemPOSition(string[] FileSplit, int i)
        {
            Case = Convert.ToInt32(FileSplit[i + 2]) - 1;
            if (LastRate[Case] == 0)
            {
                V = Math.Abs(Convert.ToDouble(FileSplit[i + 4]));
                A = Math.Abs(Convert.ToDouble(FileSplit[i + 5]));
                S = Math.Abs(Convert.ToDouble(FileSplit[i + 3]) - LastPosition[Case]);
                if (S > 5760)
                {
                    Inversion = true;
                    S = 11520 - S;
                }
                RealPosition[Case] = LastPosition[Case];
                LastPosition[Case] = Convert.ToDouble(FileSplit[i + 3]);
                if (S <= (V * V / (2 * A)))     //Если движение только равноускоренное
                {
                    TimePos[Case] = Math.Sqrt(2 * S / A);
                    for (int j = 0; j < TimePos[Case]; j++)
                    {
                        if ((RealPosition[Case] - A * j * j / 2) < -5760 && Inversion == true)
                        {
                            Inversion = false;
                            RealPosition[Case] += 11520;
                        }
                        else if ((RealPosition[Case] + A * j * j / 2) > 5760 && Inversion == true)
                        {
                            Inversion = false;
                            RealPosition[Case] -= 11520;
                        }
                        if (Inversion == false)
                        {
                            if (RealPosition[Case] <= LastPosition[Case])
                            {
                                ActivePointsX.Add(AbsolutTime + j);
                                ActivePointsY.Add(RealPosition[Case] + A * j * j / 2);
                                ActiveRateY.Add(A * j);
                            }
                            else
                            {
                                ActivePointsX.Add(AbsolutTime + j);
                                ActivePointsY.Add(RealPosition[Case] - A * j * j / 2);
                                ActiveRateY.Add(-A * j);
                            }
                        }
                        else
                        {
                            if (RealPosition[Case] <= LastPosition[Case])
                            {
                                ActivePointsX.Add(AbsolutTime + j);
                                ActivePointsY.Add(RealPosition[Case] - A * j * j / 2);
                                ActiveRateY.Add(-A * j);
                            }
                            else
                            {
                                ActivePointsX.Add(AbsolutTime + j);
                                ActivePointsY.Add(RealPosition[Case] + A * j * j / 2);
                                ActiveRateY.Add(A * j);
                            }
                        }

                    }
                }
                else
                {
                    TimePos[Case] = (V / A) + ((S - (V * V / (2 * A))) / V);
                    for (int j = 0; j < TimePos[Case]; j++)
                    {
                        if (A * j <= V)
                        {
                            if ((RealPosition[Case] - A * j * j / 2) < -5760 && Inversion == true)
                            {
                                Inversion = false;
                                RealPosition[Case] += 11520;
                            }
                            else if ((RealPosition[Case] + A * j * j / 2) > 5760 && Inversion == true)
                            {
                                Inversion = false;
                                RealPosition[Case] -= 11520;
                            }
                            if (Inversion == false)
                            {
                                if (RealPosition[Case] <= LastPosition[Case])
                                {
                                    ActivePointsX.Add(AbsolutTime + j);
                                    ActivePointsY.Add(RealPosition[Case] + A * j * j / 2);
                                    ActiveRateY.Add(A * j);
                                    NewPos = RealPosition[Case] + A * j * j / 2;
                                    J = j;
                                }
                                else
                                {
                                    ActivePointsX.Add(AbsolutTime + j);
                                    ActivePointsY.Add(RealPosition[Case] - A * j * j / 2);
                                    ActiveRateY.Add(-A * j);
                                    NewPos = RealPosition[Case] - A * j * j / 2;
                                    J = j;
                                }
                            }
                            else
                            {
                                if (RealPosition[Case] <= LastPosition[Case])
                                {
                                    ActivePointsX.Add(AbsolutTime + j);
                                    ActivePointsY.Add(RealPosition[Case] - A * j * j / 2);
                                    ActiveRateY.Add(-A * j);
                                    NewPos = RealPosition[Case] - A * j * j / 2;
                                    J = j;
                                }
                                else
                                {
                                    ActivePointsX.Add(AbsolutTime + j);
                                    ActivePointsY.Add(RealPosition[Case] + A * j * j / 2);
                                    ActiveRateY.Add(A * j);
                                    NewPos = RealPosition[Case] + A * j * j / 2;
                                    J = j;
                                }
                            }
                        }
                        else
                        {
                            if ((NewPos - V * (j - J)) < -5760 && Inversion == true)
                            {
                                Inversion = false;
                                NewPos += 11520;
                            }
                            else if ((NewPos + V * (j - J)) > 5760 && Inversion == true)
                            {
                                Inversion = false;
                                NewPos -= 11520;
                            }
                            if (Inversion == false)
                            {
                                //if (RealPosition[Case] <= LastPosition[Case])
                                if (NewPos <= LastPosition[Case])
                                {
                                    ActivePointsX.Add(AbsolutTime + j);
                                    ActivePointsY.Add(NewPos + V * (j - J));
                                    ActiveRateY.Add(V);
                                }
                                else
                                {
                                    ActivePointsX.Add(AbsolutTime + j);
                                    ActivePointsY.Add(NewPos - V * (j - J));
                                    ActiveRateY.Add(-V);
                                }
                            }
                            else
                            {
                                //if (RealPosition[Case] > LastPosition[Case])
                                if (NewPos > LastPosition[Case])
                                {
                                    ActivePointsX.Add(AbsolutTime + j);
                                    ActivePointsY.Add(NewPos + V * (j - J));
                                    ActiveRateY.Add(V);
                                }
                                else
                                {
                                    ActivePointsX.Add(AbsolutTime + j);
                                    ActivePointsY.Add(NewPos - V * (j - J));
                                    ActiveRateY.Add(-V);
                                }
                            }
                        }
                    }
                }
                for (int n = 0; n < ActivePointsX.Count; n++)
                {
                    if (FileSplit[i + 2] == "1")
                    {
                        PointsX1.Add(ActivePointsX[n]);
                        PointsY1.Add(ActivePointsY[n]);
                        RateY1.Add(ActiveRateY[n]);
                    }
                    else
                    {
                        PointsX2.Add(ActivePointsX[n]);
                        PointsY2.Add(ActivePointsY[n]);
                        RateY2.Add(ActiveRateY[n]);
                    }
                }
                ActivePointsX.Clear();
                ActivePointsY.Clear();
                ActiveRateY.Clear();
                Inversion = false;
            }
            else
            {
                ErrorFlag = true;
                Console.WriteLine("Попытка указать позицию при ненулевой скорости");
            }
        }
        public void DemRATe(string[] FileSplit, int i)
        {
            Case = Convert.ToInt32(FileSplit[i + 2]) - 1;
            A = Math.Abs(Convert.ToDouble(FileSplit[i + 4]));
            RealRate[Case] = LastRate[Case];
            LastRate[Case] = Convert.ToDouble(FileSplit[i + 3]);
            TimeRate[Case] = (LastRate[Case] - RealRate[Case]) / A;
            for (int j = 0; j < TimeRate[Case]; j++)
            {
                LastPosition[Case] += RealRate[Case];
                if (LastPosition[Case] > 5760)
                {
                    LastPosition[Case] = LastPosition[Case] - 11520;
                }
                else if (LastPosition[Case] < -5760)
                {
                    LastPosition[Case] = LastPosition[Case] + 11520;
                }
                //LastPosition[Case] = 2 * (LastPosition[Case] % 5760) - LastPosition[Case];
                if (Case == 1)
                {
                    PointsX2.Add(AbsolutTime + j);
                    PointsY2.Add(LastPosition[Case]);
                    RateY2.Add(RealRate[Case]);
                }
                else
                {
                    PointsX1.Add(AbsolutTime + j);
                    PointsY1.Add(LastPosition[Case]);
                    RateY1.Add(RealRate[Case]);
                }
                if (RealRate[Case] <= LastRate[Case])
                {
                    RealRate[Case] += A;
                }
                else
                {
                    RealRate[Case] -= A;
                }
                

            }

        }
        public void DemOSCillation(string[] FileSplit, int i)
        {
            OSCflag = true;
            Case = Convert.ToInt32(FileSplit[i + 2]) - 1;
            OSCfreq[Case] = Convert.ToInt32(FileSplit[i + 3]);
            OSCampl[Case] = Convert.ToInt32(FileSplit[i + 4]);
            if (OSCfreq[Case] == 0 && OSCampl[Case] == 0)
            {
                OSCflag = false;
            }
        }
        public void DemTEMP(string[] FileSplit, int i)
        {
            V = Math.Abs(Convert.ToDouble(FileSplit[i + 3])) / 60;
            S = Math.Abs(Convert.ToDouble(FileSplit[i + 2]) - LastPosition[2]);
            RealPosition[2] = LastPosition[2];
            LastPosition[2] = Convert.ToDouble(FileSplit[i + 2]);
            TimePos[2] = S / V;
            for (int j = 0; j < TimePos[2]; j++)
            {
                if (RealPosition[2] <= LastPosition[2])
                {
                    PointsX3.Add(AbsolutTime + j);
                    PointsY3.Add(RealPosition[2] + V * j);
                    RateY3.Add(V);
                }
                else
                {
                    PointsX3.Add(AbsolutTime + j);
                    PointsY3.Add(RealPosition[2] - V * j);
                    RateY3.Add(-V);
                }

            }
        }
        public void LogWAIT(string[] FileSplit, int i)
        {
            if (OSCflag == false)
            {
                if (LastRate[0] == 0 && LastRate[1] == 0)
                {
                    AbsolutTime += Convert.ToDouble(FileSplit[i + 2]) / 1000;
                }
                else if (LastRate[0] != 0 && LastRate[1] == 0)
                {
                    RealTime[0] = AbsolutTime + TimeRate[0];
                    AbsolutTime += Convert.ToDouble(FileSplit[i + 2]) / 1000;
                    while (RealTime[0] < AbsolutTime)
                    {
                        LastPosition[0] += LastRate[0];
                        if (LastPosition[0] > 5760)
                        {
                            LastPosition[0] = LastPosition[0] - 11520;
                        }
                        else if (LastPosition[0] < -5760)
                        {
                            LastPosition[0] = LastPosition[0] + 11520;
                        }
                        PointsX1.Add(RealTime[0]);
                        PointsY1.Add(LastPosition[0]);
                        RateY1.Add(LastRate[0]);
                        RealTime[0]++;
                    }
                }
                else if (LastRate[0] == 0 && LastRate[1] != 0)
                {
                    RealTime[1] = AbsolutTime + TimeRate[1];
                    AbsolutTime += Convert.ToDouble(FileSplit[i + 2]) / 1000;
                    while (RealTime[1] < AbsolutTime)
                    {
                        LastPosition[1] += LastRate[1];
                        if (LastPosition[1] > 5760)
                        {
                            LastPosition[1] = LastPosition[1] - 11520;
                        }
                        else if (LastPosition[1] < -5760)
                        {
                            LastPosition[1] = LastPosition[1] + 11520;
                        }
                        PointsX2.Add(RealTime[1]);
                        PointsY2.Add(LastPosition[1]);
                        RateY2.Add(LastRate[1]);
                        RealTime[1]++;
                    }
                }
                else
                {
                    RealTime[0] = AbsolutTime + TimeRate[0];
                    RealTime[1] = AbsolutTime + TimeRate[1];
                    AbsolutTime += Convert.ToDouble(FileSplit[i + 2]) / 1000;
                    while (RealTime[0] < AbsolutTime)
                    {
                        LastPosition[0] += LastRate[0];
                        if (LastPosition[0] > 5760)
                        {
                            LastPosition[0] = LastPosition[0] - 11520;
                        }
                        else if (LastPosition[0] < -5760)
                        {
                            LastPosition[0] = LastPosition[0] + 11520;
                        }
                        PointsX1.Add(RealTime[0]);
                        PointsY1.Add(LastPosition[0]);
                        RateY1.Add(LastRate[0]);
                        RealTime[0]++;
                    }
                    while (RealTime[1] < AbsolutTime)
                    {
                        LastPosition[1] += LastRate[1];
                        if (LastPosition[1] > 5760)
                        {
                            LastPosition[1] = LastPosition[1] - 11520;
                        }
                        else if (LastPosition[1] < -5760)
                        {
                            LastPosition[1] = LastPosition[1] + 11520;
                        }
                        PointsX2.Add(RealTime[1]);
                        PointsY2.Add(LastPosition[1]);
                        RateY2.Add(LastRate[1]);
                        RealTime[1]++;
                    }
                }
            }
            else
            {
                double Time = Convert.ToDouble(FileSplit[i + 2]) / 1000, j, W = 2 * Math.PI * OSCfreq[Case], ThisPos = LastPosition[Case];
                bool Znak = true;
                S = 2 * Math.Abs(OSCampl[Case]) * 0.1 * OSCfreq[Case] / Math.Abs(OSCfreq[Case]);
                for (j = AbsolutTime; j < (AbsolutTime + Time); j += (0.1 * OSCfreq[Case]))
                {
                    if (Case == 0)
                    {
                        PointsX1.Add(AbsolutTime + j);
                        PointsY1.Add(ThisPos);
                        if(Znak == true)
                        {
                            RateY1.Add(OSCfreq[Case] * 2 * OSCampl[Case]);
                        }
                        else
                        {
                            RateY1.Add(OSCfreq[Case] * -2 * OSCampl[Case]);
                        }
                        
                    }
                    else
                    {
                        PointsX2.Add(AbsolutTime + j);
                        PointsY2.Add(ThisPos);
                        if (Znak == true)
                        {
                            RateY2.Add(OSCfreq[Case] * 2 * OSCampl[Case]);
                        }
                        else
                        {
                            RateY2.Add(OSCfreq[Case] * -2 * OSCampl[Case]);
                        }
                    }
                    if (j % OSCfreq[Case] < (OSCfreq[Case] / 4) || j % OSCfreq[Case] > (OSCfreq[Case] * 3 / 4))
                    {
                        Znak = true;
                        ThisPos += S;
                    }
                    else
                    {
                        Znak = false;
                        ThisPos -= S;
                    }
                }
                AbsolutTime += j;
            }
        }
        public void LogWAITPos(string[] FileSplit, int i)
        {

            switch (FileSplit[i + 2])
            {
                case "1":
                    RealTime[0] = AbsolutTime + TimePos[0];
                    AbsolutTime += TimePos[0] + Convert.ToDouble(FileSplit[i + 4]) / 1000;
                    while (RealTime[0] < AbsolutTime)
                    {
                        PointsX1.Add(RealTime[0]);
                        PointsY1.Add(LastPosition[0]);
                        RateY1.Add(0);
                        RealTime[0]++;
                    }
                    break;
                case "2":
                    RealTime[1] = AbsolutTime + TimePos[1];
                    AbsolutTime += TimePos[1] + Convert.ToDouble(FileSplit[i + 4]) / 1000;
                    while (RealTime[1] < AbsolutTime)
                    {
                        PointsX2.Add(RealTime[1]);
                        PointsY2.Add(LastPosition[1]);
                        RateY2.Add(0);
                        RealTime[1]++;
                    }
                    break;
            }
        }
        public void LogWAITRate(string[] FileSplit, int i)
        {
            Case = Convert.ToInt32(FileSplit[i + 2]) - 1;
            RealTime[Case] = AbsolutTime + TimeRate[Case];
            AbsolutTime += TimeRate[Case] + Convert.ToDouble(FileSplit[i + 4]) / 1000;
            while (RealTime[Case] < AbsolutTime)
            {
                LastPosition[Case] += LastRate[Case];
                if (LastPosition[Case] > 5760)
                {
                    LastPosition[Case] = LastPosition[Case] - 11520;
                }
                else if (LastPosition[Case] < -5760)
                {
                    LastPosition[Case] = LastPosition[Case] + 11520;
                }
                if (Case == 1)
                {
                    PointsX2.Add(RealTime[Case]);
                    PointsY2.Add(LastPosition[Case]);
                    RateY2.Add(LastRate[Case]);
                }
                else
                {
                    PointsX1.Add(RealTime[Case]);
                    PointsY1.Add(LastPosition[Case]);
                    RateY1.Add(LastRate[Case]);
                }
                RealTime[Case]++;
            }
        }
        public void LogWAITTemp(string[] FileSplit, int i)
        {
            RealTime[2] = AbsolutTime + TimePos[2];
            AbsolutTime += TimePos[2] + Convert.ToDouble(FileSplit[i + 3]) * 60;
            while (RealTime[2] < AbsolutTime)
            {
                PointsX3.Add(RealTime[2]);
                PointsY3.Add(LastPosition[2]);
                RateY3.Add(0);
                RealTime[2]++;
            }
        }
    }
}

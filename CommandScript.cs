using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimeProgClass
{ 
    public class CommandClass
    {
        int Case = 0;
        public double S = 0, V = 0, A = 0, AbsolutTime = 0, NewPos = 0, J = 0;
        //RealPosition1 = 0, RealPosition2 = 0, RealPosition3 = 0,
        //LastPosition1 = 0, LastPosition2 = 0, LastPosition3 = 0,
        //TimePos1 = 0, TimePos2 = 0, TimePos3 = 0,
        //RealTime1 = 0, RealTime2 = 0, RealTime3 = 0;
        public double[] LastRate = new double[] { 0, 0 };
        public double[] RealRate = new double[] { 0, 0 };
        public double[] TimeRate = new double[] { 0, 0 };
        public double[] RealPosition = new double[] { 0, 0, 0 };
        public double[] LastPosition = new double[] { 0, 0, 25 };
        public double[] TimePos = new double[] { 0, 0, 0 };
        public double[] RealTime = new double[] { 0, 0, 0 };
        public int[] Counter = new int[] { 0, 0, 0 };
        //public List<List<double>> AllPoints = new List<List<double>>();
        public List<double> PointsX1 = new List<double>();
        public List<double> PointsY1 = new List<double>();
        public List<double> PointsX2 = new List<double>();
        public List<double> PointsY2 = new List<double>();
        public List<double> PointsX3 = new List<double>();
        public List<double> PointsY3 = new List<double>();
        public List<double> ActivePointsX = new List<double>();
        public List<double> ActivePointsY = new List<double>();
        //public List<double> Deactive1PointsX = new List<double>();
        //public List<double> Deactive1PointsY = new List<double>();
        //public List<double> Deactive2PointsX = new List<double>();
        //public List<double> Deactive2PointsY = new List<double>();

        public void DemPOSition(string[] FileSplit, int i)
        {
            Case = Convert.ToInt32(FileSplit[i + 2]) - 1;
            V = Math.Abs(Convert.ToDouble(FileSplit[i + 4]));
            A = Math.Abs(Convert.ToDouble(FileSplit[i + 5]));
            S = Math.Abs(Convert.ToDouble(FileSplit[i + 3]) - LastPosition[Case]);
            RealPosition[Case] = LastPosition[Case];
            LastPosition[Case] = Convert.ToDouble(FileSplit[i + 3]);
            if (S <= (V * V / (2 * A)))     //Если движение только равноускоренное
            {
                TimePos[Case] = Math.Sqrt(2 * S / A);
                for (int j = 0; j < TimePos[Case]; j++)
                {

                    if (RealPosition[Case] <= LastPosition[Case])
                    {
                        ActivePointsX.Add(AbsolutTime + j);
                        ActivePointsY.Add(RealPosition[Case] + A * j * j / 2);
                    }
                    else
                    {
                        ActivePointsX.Add(AbsolutTime + j);
                        ActivePointsY.Add(RealPosition[Case] - A * j * j / 2);
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
                        if (RealPosition[Case] <= LastPosition[Case])
                        {
                            ActivePointsX.Add(AbsolutTime + j);
                            ActivePointsY.Add(RealPosition[Case] + A * j * j / 2);
                            NewPos = RealPosition[Case] + A * j * j / 2;
                            J = j;
                        }
                        else
                        {
                            ActivePointsX.Add(AbsolutTime + j);
                            ActivePointsY.Add(RealPosition[Case] - A * j * j / 2);
                            NewPos = RealPosition[Case] - A * j * j / 2;
                            J = j;
                        }

                    }
                    else
                    {
                        if (RealPosition[Case] <= LastPosition[Case])
                        {
                            ActivePointsX.Add(AbsolutTime + j);
                            ActivePointsY.Add(NewPos + V * (j - J));
                        }
                        else
                        {
                            ActivePointsX.Add(AbsolutTime + j);
                            ActivePointsY.Add(NewPos - V * (j - J));
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
                }
                else
                {
                    PointsX2.Add(ActivePointsX[n]);
                    PointsY2.Add(ActivePointsY[n]);
                }
            }
            ActivePointsX.Clear();
            ActivePointsY.Clear();
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
                ActivePointsX.Add(AbsolutTime + j);
                ActivePointsY.Add(LastPosition[Case]);
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
                    PointsY3.Add(RealPosition[Case] + V * j);
                }
                else
                {
                    PointsX3.Add(AbsolutTime + j);
                    PointsY3.Add(RealPosition[Case] - V * j);
                }

            }
        }
        public void LogWAIT(string[] FileSplit, int i)
        {
            /*
            RealTime[0] = AbsolutTime + TimePos[0];
            RealTime[1] = AbsolutTime + TimePos[1];
            RealTime[2] = AbsolutTime + TimePos[2];
            */
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
                    PointsX1.Add(RealTime[0]);
                    PointsY1.Add(LastPosition[0]);
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
                    PointsX1.Add(RealTime[1]);
                    PointsY1.Add(LastPosition[1]);
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
                    PointsX1.Add(RealTime[0]);
                    PointsY1.Add(LastPosition[0]);
                    RealTime[0]++;
                }
                while (RealTime[1] < AbsolutTime)
                {
                    LastPosition[1] += LastRate[1];
                    PointsX1.Add(RealTime[1]);
                    PointsY1.Add(LastPosition[1]);
                    RealTime[1]++;
                }
            }
            /*
            while (RealTime[0] < AbsolutTime)
            {
                PointsX1.Add(RealTime[0]);
                PointsY1.Add(LastPosition[0]);
                RealTime[0]++;
            }
            while (RealTime[1] < AbsolutTime)
            {
                PointsX2.Add(RealTime[1]);
                PointsY2.Add(LastPosition[1]);
                RealTime[1]++;
            }
            while (RealTime[2] < AbsolutTime)
            {
                PointsX3.Add(RealTime[2]);
                PointsY3.Add(LastPosition[2]);
                RealTime[2]++;
            }
            */
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
                PointsX3.Add(RealTime[Case]);
                PointsY3.Add(LastPosition[Case]);
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
                RealTime[2]++;
            }
        }
    }
}

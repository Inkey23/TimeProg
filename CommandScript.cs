using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimeProgClass
{
    class CommandClass
    {
        double T = 0, S = 0, V = 0, A = 0, LastPosition1 = 0, LastPosition2 = 0, LastPosition3 = 0;
        double AbsolutTime = 0, TimePos1 = 0, TimePos2 = 0, TimePos3 = 0, RealPosition1 = 0, RealPosition2 = 0, RealPosition3 = 0, RealTime1 = 0, RealTime2 = 0, RealTime3 = 0, NewPos = 0, J = 0;
        //List<List<double>> AllPoints= new List<List<double>> ();
        List<List<double>> AllPoints = new List<List<double>>();
        
        List<double> PointsX1 = new List<double>();
        List<double> PointsY1 = new List<double>();
        List<double> PointsX2 = new List<double>();
        List<double> PointsY2 = new List<double>();
        List<double> PointsX3 = new List<double>();
        List<double> PointsY3 = new List<double>();
        public void Kostil(object sender, EventArgs e)
        {
            AbsolutTime = 0;
            AllPoints.Clear();
            PointsX1.Clear();
            PointsX2.Clear();
            PointsX3.Clear();
            PointsY1.Clear();
            PointsY2.Clear();
            PointsY3.Clear();
            AllPoints.Add(PointsX1);
            AllPoints.Add(PointsY1);
            AllPoints.Add(PointsX2);
            AllPoints.Add(PointsY2);
            AllPoints.Add(PointsX3);
            AllPoints.Add(PointsY3);
        }


        public List<List<double>> DemPOSition(string[] FileSplit, int i)
        {
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
                        for (int j = 0; j < TimePos1; j++)
                        {

                            if (RealPosition1 <= LastPosition1)
                            {
                                AllPoints[0].Add(AbsolutTime + j);
                                AllPoints[1].Add(RealPosition1 + A * j * j / 2);
                            }
                            else
                            {
                                AllPoints[0].Add(AbsolutTime + j);
                                AllPoints[1].Add(RealPosition1 - A * j * j / 2);
                            }

                        }
                    }
                    else
                    {
                        TimePos1 = (V / A) + ((S - (V * V / (2 * A))) / V);
                        for (int j = 0; j < TimePos1; j++)
                        {
                            if (A * j <= V)
                            {
                                if (RealPosition1 <= LastPosition1)
                                {
                                    AllPoints[0].Add(AbsolutTime + j);
                                    AllPoints[1].Add(RealPosition1 + A * j * j / 2);
                                    NewPos = RealPosition1 + A * j * j / 2;
                                    J = j;
                                }
                                else
                                {
                                    AllPoints[0].Add(AbsolutTime + j);
                                    AllPoints[1].Add(RealPosition1 - A * j * j / 2);
                                    NewPos = RealPosition1 - A * j * j / 2;
                                    J = j;
                                }

                            }
                            else
                            {
                                AllPoints[0].Add(AbsolutTime + j);
                                AllPoints[1].Add(NewPos + V * (j - J));
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
                                AllPoints[2].Add(AbsolutTime + j);
                                AllPoints[3].Add(RealPosition2 + A * j * j / 2);
                            }
                            else
                            {
                                AllPoints[2].Add(AbsolutTime + j);
                                AllPoints[3].Add(RealPosition2 - A * j * j / 2);
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
                                    AllPoints[2].Add(AbsolutTime + j);
                                    AllPoints[3].Add(RealPosition2 + A * j * j / 2);
                                    NewPos = RealPosition2 + A * j * j / 2;
                                    J = j;
                                }
                                else
                                {
                                    AllPoints[2].Add(AbsolutTime + j);
                                    AllPoints[3].Add(RealPosition2 - A * j * j / 2);
                                    NewPos = RealPosition2 - A * j * j / 2;
                                    J = j;
                                }

                            }
                            else
                            {
                                AllPoints[2].Add(AbsolutTime + j);
                                AllPoints[3].Add(NewPos + V * (j - J));
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
                                AllPoints[4].Add(AbsolutTime + j);
                                AllPoints[5].Add(RealPosition3 + A * j * j / 2);
                            }
                            else
                            {
                                AllPoints[4].Add(AbsolutTime + j);
                                AllPoints[5].Add(RealPosition3 - A * j * j / 2);
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
                                    AllPoints[4].Add(AbsolutTime + j);
                                    AllPoints[5].Add(RealPosition3 + A * j * j / 2);
                                    NewPos = RealPosition3 + A * j * j / 2;
                                    J = j;
                                }
                                else
                                {
                                    AllPoints[4].Add(AbsolutTime + j);
                                    AllPoints[5].Add(RealPosition3 - A * j * j / 2);
                                    NewPos = RealPosition3 - A * j * j / 2;
                                    J = j;
                                }

                            }
                            else
                            {
                                AllPoints[4].Add(AbsolutTime + j);
                                AllPoints[5].Add(NewPos + V * (j - J));
                            }
                        }
                    }
                    break;
            }
            return (AllPoints);
        }
        public List<List<double>> LogWAIT(string[] FileSplit, int i)
        {
            RealTime1 = AbsolutTime + TimePos1;
            RealTime2 = AbsolutTime + TimePos2;
            RealTime3 = AbsolutTime + TimePos3;
            AbsolutTime += Convert.ToDouble(FileSplit[i + 2]) / 1000;
            while (RealTime1 < AbsolutTime)
            {
                AllPoints[0].Add(RealTime1);
                AllPoints[1].Add(LastPosition1);
                RealTime1++;
            }
            while (RealTime2 < AbsolutTime)
            {
                AllPoints[2].Add(RealTime2);
                AllPoints[3].Add(LastPosition2);
                RealTime2++;
            }
            while (RealTime3 < AbsolutTime)
            {
                AllPoints[4].Add(RealTime3);
                AllPoints[5].Add(LastPosition3);
                RealTime3++;
            }
            return (AllPoints);
        }
        public List<List<double>> LogWAITPos(string[] FileSplit, int i)
        {
            switch (FileSplit[i + 2])
            {
                case "1":
                    RealTime1 = AbsolutTime + TimePos1;
                    AbsolutTime += (TimePos1 + Convert.ToDouble(FileSplit[i + 4]) / 1000);
                    while (RealTime1 < AbsolutTime)
                    {
                        AllPoints[0].Add(RealTime1);
                        AllPoints[1].Add(LastPosition1);
                        RealTime1++;
                    }
                    break;
                case "2":
                    RealTime2 = AbsolutTime + TimePos2;
                    AbsolutTime += (TimePos2 + Convert.ToDouble(FileSplit[i + 4]) / 1000);
                    while (RealTime2 < AbsolutTime)
                    {
                        AllPoints[2].Add(RealTime2);
                        AllPoints[3].Add(LastPosition2);
                        RealTime2++;
                    }
                    break;
                case "3":
                    RealTime3 = AbsolutTime + TimePos3;
                    AbsolutTime += (TimePos3 + Convert.ToDouble(FileSplit[i + 4]) / 1000);
                    while (RealTime3 < AbsolutTime)
                    {
                        AllPoints[4].Add(RealTime3);
                        AllPoints[5].Add(LastPosition3);
                        RealTime3++;
                    }
                    break;
            }
            return (AllPoints);
        }
    }
}

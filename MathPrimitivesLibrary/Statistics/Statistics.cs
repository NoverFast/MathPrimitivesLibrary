using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MathPrimitivesLibrary.Statistics
{
  public static class Statistics
  {
    public static double Mean(double[] data)
    {
      double sum = 0;
      for (int i = 0; i < data.Length; i++)
      {
        sum += data[i];
      }
      return sum / data.Length;
    }

    public static double MeanAbsoluteDeviation(double[] data)
    {
      double sum = 0;
      double mean = Mean(data);
      for (int i =0; i < data.Length; i++)
      {
        sum += Math.Abs(data[i] - mean);
      }
      return sum / data.Length;
    }

    public static double Dispersion(double[] data)
    {
      double sum = 0;
      double expectancy = Mean(data);
      for (int i = 0; i < data.Length; i++)
      {
        sum += Math.Pow((data[i] - expectancy), 2);
      }
      return sum;
    }

    public static double CorrectedDispersion(double[] data)
    {
      return Dispersion(data) * (double)(1.0 / (data.Length - 1));
    }
  }
}

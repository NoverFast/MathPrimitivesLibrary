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
      for (int i = 0; i < data.Length; i++)
      {
        sum += Math.Abs(data[i] - mean);
      }
      return sum / data.Length;
    }

    public static double Dispersion(double[] data)
    {
      double sum = 0;
      double mean = Mean(data);
      for (int i = 0; i < data.Length; i++)
      {
        sum += Math.Pow(data[i] - mean, 2);
      }
      return sum / data.Length;
    }

    public static double CorrectedDispersion(double[] data)
    {
      return Dispersion(data) * ((double)data.Length / (double)(data.Length - 1));
    }

    public static Matrix NormalizeData(Matrix data)
    {
      Matrix normalizedMatrix = new Matrix(data);
      double mean;
      double dispersion;
      for (int i = 0; i < normalizedMatrix.Rows; i++)
      {
        mean = Mean(normalizedMatrix[i].ToArray());
        dispersion = Dispersion(normalizedMatrix[i].ToArray());
        for (int j = 0; j < normalizedMatrix.Coloumns; j++)
        {
          normalizedMatrix[i, j] = Math.Round((data[i, j] - mean) / Math.Sqrt(dispersion), 2);
        }
      }
      return normalizedMatrix;
    }

    public static Matrix CorrelationMatrix(Matrix normalizedMatrix)
    {
      Matrix correlationMatrix = new Matrix(normalizedMatrix.Rows);
      double sum = 0;
      for (int i = 0; i < normalizedMatrix.Rows; i++)
      {
        for (int j = 0; j < normalizedMatrix.Rows; j++)
        {
          for (int k = 0; k < normalizedMatrix.Coloumns; k++)
          {
            sum += normalizedMatrix[i, k] * normalizedMatrix[j, k];
          }
          correlationMatrix[i, j] = Math.Round(sum / normalizedMatrix.Coloumns, 2);
          sum = 0;
          if (i == j)
          {
            correlationMatrix[i, j] = 1;
          }
        }
      }
      return correlationMatrix;
    }

    public static Vector LinearRegression(double[] data1, double[] data2)
    {
      if (data1.SequenceEqual(data2))
      {
        return new Vector(new double[] { 0, 1 });
      }
      double d1Mean = Mean(data1);
      double d2Mean = Mean(data2);
      double d1Deviation = Math.Sqrt(Dispersion(data1));
      double d2Deviation = Math.Sqrt(Dispersion(data2));
      Matrix dataMatrix = NormalizeData(new Matrix(new double[][] { data1, data2 }));
      Matrix correlationMatrix = CorrelationMatrix(dataMatrix);
      return new Vector(new double[] {
        Math.Round(d1Mean - correlationMatrix[0, 1] * d2Mean * d1Deviation / d2Deviation, 2),
        Math.Round(correlationMatrix[0,1] * d1Deviation / d2Deviation, 2) });
    }

  }
}

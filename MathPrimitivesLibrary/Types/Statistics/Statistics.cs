using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MathPrimitivesLibrary.Statistics
{
  public static class Statistics
  {
    /// <summary>
    /// Calculates mean of provided sample. 
    /// </summary>
    /// <param name="data">Sample</param>
    public static double Mean(double[] data)
    {
      double sum = 0;
      for (int i = 0; i < data.Length; i++)
      {
        sum += data[i];
      }
      return sum / data.Length;
    }

    /// <summary>
    /// Calculates MAD (Mean Absolute Deviation) of provided sample. 
    /// </summary>
    /// <param name="data">Sample</param>
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

    /// <summary>
    /// Calculates dispersion of provided sample. 
    /// </summary>
    /// <param name="data">Sample</param>
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

    /// <summary>
    /// Calculates corrected dispersion of provided sample. 
    /// </summary>
    /// <param name="data">Sample</param>
    /// <returns>Dispersion multiplied by (n / (n-1)) where n is the size of the sample.</returns>
    public static double CorrectedDispersion(double[] data)
    {
      return Dispersion(data) * ((double)data.Length / (double)(data.Length - 1));
    }

    /// <summary>
    /// Normalizes provided sample by substracting mean and dividing by square root of dispersion. 
    /// </summary>
    /// <param name="data">Sample</param>
    /// <returns>Normalized sample.</returns>
    public static Vector NormalizeData(double[] data)
    {
      Vector normalizedData = new Vector(data.Length);
      double mean = Mean(normalizedData.ToArray());
      double dispersion = Dispersion(normalizedData.ToArray());
      for (int i = 0; i < normalizedData.Size; i++)
      {
        normalizedData[i] = Math.Round((data[i] - mean) / Math.Sqrt(dispersion), 2);
      }
      return normalizedData;
    }

    /// <summary>
    /// Normalizes provided samples by substracting mean and dividing by square root of dispersion. 
    /// </summary>
    /// <param name="data">Sample</param>
    /// <returns>Normalized matrix of samples.</returns>
    public static Matrix NormalizeData(double[,] data)
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


    /// <summary>
    /// Build correlation matrix based on provided normalized matrix
    /// </summary>
    /// <param name="normalizedMatrix">Matrix with normalized samples</param>
    /// <returns>Correlation matrix.</returns>
    public static Matrix CorrelationMatrix(double[,] normalizedMatrix)
    {
      Matrix correlationMatrix = new Matrix(normalizedMatrix.GetLength(0));
      double sum = 0;
      for (int i = 0; i < normalizedMatrix.GetLength(0); i++)
      {
        for (int j = 0; j < normalizedMatrix.GetLength(0); j++)
        {
          for (int k = 0; k < normalizedMatrix.GetLength(1); k++)
          {
            sum += normalizedMatrix[i, k] * normalizedMatrix[j, k];
          }
          correlationMatrix[i, j] = Math.Round(sum / normalizedMatrix.GetLength(1), 2);
          sum = 0;
          if (i == j)
          {
            correlationMatrix[i, j] = 1;
          }
        }
      }
      return correlationMatrix;
    }

    /// <summary>
    /// Builsd linear regression equation between two samples of data
    /// </summary>
    /// <param name="data1">First sample</param>
    /// <param name="data2">Second sample</param>
    /// <returns>Vector of 1-st degree polynomial coefficients of linear regression equation.</returns>
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
      Matrix dataMatrix = NormalizeData(new Matrix(new double[][] { data1, data2 }).To2DArray());
      Matrix correlationMatrix = CorrelationMatrix(dataMatrix.To2DArray());
      return new Vector(new double[] {
        Math.Round(d1Mean - correlationMatrix[0, 1] * d2Mean * d1Deviation / d2Deviation, 2),
        Math.Round(correlationMatrix[0,1] * d1Deviation / d2Deviation, 2) });
    }
  }
}

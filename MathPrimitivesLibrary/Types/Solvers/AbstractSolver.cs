using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MathPrimitivesLibrary.Solvers
{
  public abstract class AbstractSolver
  {
    protected abstract Vector Solve(Matrix systemMatrix, Vector coefficients);
    private List<double> SimpleIterationsMethod(double[,] matrix, double[] freeCoefs, double[] startPrecision, double precision)
    {
      List<double> answerVector = new List<double>();
      List<double> precisionVector = new List<double>();
      List<double> beta = new List<double>();
      double[,] alpha = new double[matrix.GetLength(0), matrix.GetLength(0)];
      double sum;
      int currentIteration = 0;
      for (int i = 0; i < matrix.GetLength(0); i++)
      {
        for (int j = 0; j < matrix.GetLength(1); j++)
        {
          alpha[i, j] = matrix[i, j] / matrix[i, i];
        }
        alpha[i, i] = 0;
        beta.Add(freeCoefs[i] / matrix[i, i]);
        answerVector.Add(startPrecision[i]);
        precisionVector.Add(double.PositiveInfinity);
      }

      while (precisionVector.Max() > precision)
      {
        for (int i = 0; i < matrix.GetLength(0); i++)
        {
          sum = 0;
          if (currentIteration != 0)
          {
            for (int j = 0; j < matrix.GetLength(1); j++)
            {
              sum += alpha[i, j] * answerVector[j];
            }
            precisionVector[i] = Math.Abs(answerVector[i] - startPrecision[i]);
            startPrecision[i] = answerVector[i];
          }
          answerVector[i] = beta[i] - sum;
        }
        currentIteration++;
      }
      Console.WriteLine("Number of iterations for SIM: " + currentIteration);
      return answerVector;
    }
    private List<double> SOR(double[,] matrix, double[] freeCoefs, double[] startPrecision, double precision, double w)
    {
      if (w <= 0 || w >= 2)
      {
        Console.WriteLine("Parameter w must be inside (0,2)");
        Console.WriteLine("Your input: " + w);
        return new List<double> { 0 };
      }

      List<double> answerVector = new List<double>();
      List<double> precisionVector = new List<double>();
      List<double> beta = new List<double>();
      double[,] alpha = new double[matrix.GetLength(0), matrix.GetLength(0)];
      double sumUpper;
      double sumLower;
      List<double> tempAnswer = new List<double>();
      int currentIteration = 0;

      for (int i = 0; i < matrix.GetLength(0); i++)
      {
        for (int j = 0; j < matrix.GetLength(1); j++)
        {
          alpha[i, j] = matrix[i, j] / matrix[i, i];
        }
        alpha[i, i] = 0;
        beta.Add(freeCoefs[i] / matrix[i, i]);
        answerVector.Add(startPrecision[i]);
        tempAnswer.Add(0);
        precisionVector.Add(double.PositiveInfinity);
      }

      while (precisionVector.Max() > precision)
      {
        for (int i = 0; i < matrix.GetLength(0); i++)
        {
          sumLower = 0;
          sumUpper = 0;
          if (currentIteration != 0)
          {
            for (int j = i + 1; j < matrix.GetLength(0); j++)
            {
              sumUpper += alpha[i, j] * answerVector[j];
            }
            tempAnswer[i] = beta[i] - sumUpper;
            for (int j = 0; j < i; j++)
            {
              sumLower += alpha[i, j] * tempAnswer[j];
            }
            tempAnswer[i] -= sumLower;
            precisionVector[i] = Math.Abs(tempAnswer[i] - startPrecision[i]);
            startPrecision[i] = answerVector[i];
          }
          answerVector[i] = (1 - w) * answerVector[i] + w * tempAnswer[i];
        }
        currentIteration++;
      }
      Console.WriteLine("Number of iterations for SOR: " + currentIteration);
      return answerVector;
    }
  }
}

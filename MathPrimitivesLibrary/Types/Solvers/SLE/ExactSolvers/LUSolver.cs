using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MathPrimitivesLibrary.Solvers.ExactSolvers
{
  public class LUSolver 
  {
    private enum LUEnum
    {
      L = 0,
      U = 1
    }
    public static List<double> Solve(double[,] matrix, double[] freeCoefs)
    {
      List<double[,]> LU = LUHelper.LUDecomposition(matrix);
      double[,] L = LU[(int)LUEnum.L];
      double[,] U = LU[(int)LUEnum.U];
      List<double> answerVector = new List<double>();
      List<double> yVector = new List<double>();
      List<double> temp = new List<double>();
      yVector.Add(freeCoefs[0] / L[0, 0]);
      for (int i = 0; i < matrix.GetLength(0); i++)
      {
        answerVector.Add(0);
        temp.Add(0);
      }
      for (int i = 1; i < matrix.GetLength(0); i++)
      {
        for (int j = 0; j < i; j++)
        {
          temp[i] += L[i, j] * yVector[j];
        }
        yVector.Add((freeCoefs[i] - temp[i]) / L[i, i]);
        temp[i] = 0;
      }

      for (int i = 0; i < matrix.GetLength(0); i++)
      {
        temp[i] = 0;
      }

      answerVector[matrix.GetLength(0) - 1] = yVector[matrix.GetLength(0) - 1] / U[matrix.GetLength(0) - 1, matrix.GetLength(0) - 1];
      for (int i = matrix.GetLength(0) - 2; i >= 0; i--)
      {
        for (int j = matrix.GetLength(0) - 1; j >= 0; j--)
        {
          temp[i] += U[i, j] * answerVector[j];
        }
        answerVector[i] = (yVector[i] - temp[i]) / U[i, i];
        temp[i] = 0;
      }
      return answerVector;
    }
  }
  public static class LUHelper
  {
    public static List<double[,]> LUDecomposition(double[,] matrix)
    {
      List<double[,]> LU = new List<double[,]>(2);
      double[,] L = new double[matrix.GetLength(0), matrix.GetLength(0)];
      double[,] U = matrix;

      for (int i = 0; i < matrix.GetLength(0); i++)
      {
        for (int j = i; j < matrix.GetLength(0); j++)
        {
          L[j, i] = U[j, i] / U[i, i];
        }
      }

      for (int k = 1; k < matrix.GetLength(0); k++)
      {
        for (int i = k - 1; i < matrix.GetLength(0); i++)
        {
          for (int j = i; j < matrix.GetLength(0); j++)
          {
            L[j, i] = U[j, i] / U[i, i];
          }
        }

        for (int i = k; i < matrix.GetLength(0); i++)
        {
          for (int j = k - 1; j < matrix.GetLength(0); j++)
          {
            U[i, j] = U[i, j] - L[i, k - 1] * U[k - 1, j];
          }
        }
      }
      LU.Add(L);
      LU.Add(U);
      return LU;
    }
  }


}

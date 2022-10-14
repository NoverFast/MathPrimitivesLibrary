using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MathPrimitivesLibrary.Solvers.ExactSolvers
{
  public class TridiagonalSolver
  {
    private enum ForwardSweepCoefsEnum
    {
      y = 0,
      alpha = 1,
      beta = 2
    }

    public static Vector Solve(Matrix systemMatrix, Vector coefficients)
    {
      int n = systemMatrix.Rows; 
      Vector answerVector = new Vector(n);
      List<List<double>> forwardSweepCoefs = new List<List<double>>();
      double y = systemMatrix[0, 0];
      double alpha = -systemMatrix[0, 1] / systemMatrix[0, 0];
      double beta = coefficients[0] / systemMatrix[0, 0];
      forwardSweepCoefs.Add(new List<double>() { y, alpha, beta });
      for (int i = 1; i < n-1; i++)
      {
        y = systemMatrix[i, i] + systemMatrix[i, i - 1] * forwardSweepCoefs[i - 1][(int)ForwardSweepCoefsEnum.alpha];
        alpha = -systemMatrix[i, i + 1] / y;
        beta = (coefficients[i] - systemMatrix[i, i - 1] * forwardSweepCoefs[i - 1][(int)ForwardSweepCoefsEnum.beta]) / y;
        forwardSweepCoefs.Add(new List<double>() { y, alpha, beta });
      }
      y = systemMatrix[n-1, n-1] + systemMatrix[n-1, n - 2] * forwardSweepCoefs[n - 2][(int)ForwardSweepCoefsEnum.alpha];
      beta = (coefficients[n-1] - systemMatrix[n-1, n - 2] * forwardSweepCoefs[n - 2][(int)ForwardSweepCoefsEnum.beta]) / y;
      forwardSweepCoefs.Add(new List<double>() { y, double.NaN, beta });
      answerVector[n - 1] = (forwardSweepCoefs[n-1][(int)ForwardSweepCoefsEnum.beta]);
      int count = n-1;
      for (int i = n - 2; i >= 0; i--)
      {
        answerVector[i] = (forwardSweepCoefs[i][(int)ForwardSweepCoefsEnum.alpha] * answerVector[count] + forwardSweepCoefs[i][(int)ForwardSweepCoefsEnum.beta]);
        count--;
      }
      Vector temp = new Vector(n);
      for (int i = 0; i < temp.Size; i++)
      {
        temp[i] = answerVector[i];
      }
      return temp;
    }
  }
}

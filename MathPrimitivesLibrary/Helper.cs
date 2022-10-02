using System;
using System.Collections.Generic;

namespace MathPrimitivesLibrary
{
  static class Helper
  {
    public static int KroneckerSymbol(int i, int j) => i == j ? 1 : 0;

    public static Matrix IdentityMatrix(int size)
    {
      Matrix m = new Matrix(size);
      for (int i = 0; i < size; i++)
      {
        for (int j = 0; j < size; j++)
        {
          m[i, j] = KroneckerSymbol(i, j);
        }
      }
      return m;
    }

    [Obsolete]

    public static double VectorNorm(List<double> vector)
    {
      double sum = 0;
      foreach (double i in vector)
      {
        sum += Math.Pow(i, 2);
      }
      return sum;
    }

    [Obsolete]
    public static List<double> ScalarMultiplyByVector(double scalar, List<double> vector)
    {
      for (int i = 0; i < vector.Count ;i++)
      {
        vector[i] *= scalar;
      }
      return vector;
    }

    [Obsolete]
    public static List<List<double>> ScalarMultiplyByMatrix(double scalar, List<List<double>> matrix)
    {
      for (int i = 0; i < matrix.Count; i++)
      {
        for (int j = 0; j < matrix.Count; j++)
        {
          matrix[i][j] *= scalar;
        }
      }
      return matrix;
    }

    [Obsolete]
    public static List<double> VectorAddition(List<double> vec1, List<double> vec2)
    {
      if (vec1.Count != vec2.Count)
      {
        return null;
      }
      List<double> result = new List<double>();
      for (int i = 0; i < vec1.Count; i++)
      {
        result.Add(vec1[i]+vec2[i]);
      }
      return result;
    }

    [Obsolete]
    public static List<List<double>> MatrixAddition(List<List<double>> matrix1, List<List<double>> matrix2)
    {
      List<List<double>> answer = new List<List<double>>(matrix1);
      if (matrix1.Count != matrix2.Count)
      {
        return null;
      }
      foreach (List<double> coloumn in matrix1)
      {
        if (coloumn.Count != matrix2[0].Count)
        {
          return null;
        }
      }

      for (int i = 0; i < matrix1.Count; i++)
      {
        for (int j = 0; j < matrix1.Count; j++)
        {
          answer[i][j] = matrix1[i][j] + matrix2[i][j];
        }
      }
      return answer;
    }

    [Obsolete]
    public static List<double> MatrixVectorMult(List<List<double>> matrix, List<double> vector)
    {
      if (matrix[0].Count != vector.Count)
      {
        return null;
      }
      List<double> result = new List<double>();
      double sum = 0;
      for (int i =0; i < vector.Count; i++)
      {
        for (int j = 0; j < vector.Count; j++)
        {
          sum += matrix[i][j] * vector[j];
        }
        result.Add(sum);
        sum = 0;
      }
      return result;
    }

    [Obsolete]
    public static List<double> MatrixVectorMult(List<double> vector, List<List<double>> matrix)
    {
      if (matrix[0].Count != vector.Count)
      {
        return null;
      }
      List<double> result = new List<double>();
      double sum = 0;
      for (int i = 0; i < vector.Count; i++)
      {
        for (int j = 0; j < vector.Count; j++)
        {
          sum += matrix[j][i] * vector[j];
        }
        result.Add(sum);
        sum = 0;
      }
      return result;
    }

    [Obsolete]
    public static List<List<double>> InverseMatrix2D(List<List<double>> matrix)
    {
      double determinant = matrix[0][0] * matrix[1][1] - matrix[0][1] * matrix[1][0];
      return new List<List<double>>() 
      { 
        new List<double>() { matrix[1][1] / determinant, -matrix[1][0] / determinant },
        new List<double>() { -matrix[0][1] / determinant, matrix[1][1] / determinant} 
      };
    }

    [Obsolete]
    public static void ShowMatrix(List<List<double>> matrix)
    {
      foreach (List<double> lst in matrix)
      {
        Console.WriteLine();
        foreach (double item in lst)
        {
          Console.Write("\t" + item + "\t");
        }
      }
      Console.WriteLine();
    }

    [Obsolete]
    public static void ShowVector(List<double> vector)
    {
      foreach (double item in vector)
      {
        Console.Write(" " + item + " ");
      }
    }
  }
}

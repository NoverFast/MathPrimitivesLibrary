using System;
using System.Collections.Generic;

namespace MathPrimitivesLibrary
{
  public class Matrix
  {
    public int Rows { get; private set; }
    public int Coloumns { get; private set; }
    public Matrix IdentityMatrix { get { return Helper.IdentityMatrix(this.Rows); } }
    public double[,] Data { get; private set; }

    public Matrix(int n)
    {
      Data = new double[n, n];
      Coloumns = Rows = n;
    }
    public Matrix(int rows, int coloumns)
    {
      Data = new double[rows, coloumns];
      Rows = rows;
      Coloumns = coloumns;
    }
    public Matrix(double[,] data)
    {
      Rows = data.GetLength(0);
      Coloumns = data.GetLength(1);
      Data = (double[,])data.Clone();
    }
    public Matrix(double[][] data)
    {
      Rows = data.GetLength(0);
      Coloumns = data[0].Length;
      Data = new double[Rows, Coloumns];
      for (int i = 0; i < Rows; i++)
      {
        for (int j = 0; j < Coloumns; j++)
        {
          this[i, j] = data[i][j];
        }
      }
    }
    public Matrix(List<double[]> data)
    {
      Rows = data.Count;
      Coloumns = data[0].Length;
      Data = new double[Rows, Coloumns];
      for (int i = 0; i < Rows; i++)
      {
        for (int j =0; j < Coloumns; j++)
        {
          this[i, j] = data[i][j];
        }
      }
    }
    public Matrix(int rows, int coloumns, double[,] data)
    {
      Rows = rows;
      Coloumns = coloumns;
      if (data.GetLength(0) != Rows && data.GetLength(1) != coloumns)
      {
        throw new Exception("Data size is not the same as matrix size!");
      }
      Data = (double[,])data.Clone();
    }

    public Matrix(Matrix m)
    {
      Rows = m.Rows;
      Coloumns = m.Coloumns;
      Data = (double[,])m.Data.Clone();
    }
    
    public void Show()
    {
      for (int i =0; i < Rows; i++)
      {
        for (int j = 0; j < Coloumns; j++)
        {
          Console.Write("\t{0}", Data[i, j]);
        }
        Console.WriteLine();
      }
    }

    public Matrix Zip(Matrix m)
    {
      if (this.Rows != m.Rows || this.Coloumns != m.Coloumns)
      {
        throw new Exception("Both matricies must be of the same dimension!");
      }
      double[,] zippedData = new double[this.Rows, this.Coloumns + m.Coloumns];
      for (int i =0; i < zippedData.GetLength(0); i++)
      {
        for (int j =0; j < zippedData.GetLength(1) / 2; j++)
        {
          zippedData[i, j] = this.Data[i, j];
          zippedData[i, j + zippedData.GetLength(1) / 2] = m.Data[i, j];
        }
      }
      return new Matrix(zippedData);
    }

    public Matrix Inverse()
    {
      Matrix gaussM = this.Zip(Helper.IdentityMatrix(this.Rows));
      gaussM.Show();
      gaussM.Triagonalize();
      // После этого цикла имеем дело с нижне-треугольной матрицей
      for (int i = gaussM.Rows; i > 0; i--)
      {
        for (int j = i - 1; j > 0; j--)
        {
          double koef = gaussM[j, i] / gaussM[i, i];
          for (int k = gaussM.Rows; k > 0; k--)
          {
            gaussM[j, k] -= gaussM[i, k] * koef;
          }
        }
      }
      double[,] gaussData = new double[this.Rows, this.Coloumns];
      for (int i = 0; i < this.Rows;i++)
      {
        for (int j = 0; j < this.Coloumns; j++)
        {
          gaussData[i, j] = gaussM[i, j + this.Coloumns];
        }
      }
      return new Matrix(this.Rows, this.Coloumns, gaussData);
    } 

    public Matrix Transpose()
    {
      Matrix transposedMatrix = new Matrix(Coloumns, Rows);
      for (int i = 0; i < transposedMatrix.Rows; i++)
      {
        for (int j = 0; j < transposedMatrix.Coloumns; j++)
        {
          transposedMatrix[i, j] = this[j, i];
        }
      }      
      return transposedMatrix;
    }

    public double Determinant()
    {
      double determinant = 1;
      Matrix m = new Matrix(this.Triagonalize());
      for (int i =0; i < m.Rows; i++)
      {
        for (int j =0; j < m.Coloumns;j++)
        {
          if (Helper.KroneckerSymbol(i, j) == 1)
          {
            determinant *= m[i, j];
          }
        }
      }
      return determinant;
    }

    // Диагонализованные элементы не должны быть равны нулю!!!
    public Matrix Triagonalize()
    {
      Matrix triangleMatrix = new Matrix(this.Data);
      for (int i = 0; i < triangleMatrix.Coloumns - 1; i++)
      {
        for (int j = i + 1; j < triangleMatrix.Rows; j++)
        {
          double koef = triangleMatrix[j] / triangleMatrix[i, i];
          for (int k = 0; k < triangleMatrix.Rows; k++)
          {
            triangleMatrix[j] -= triangleMatrix[i] * koef;
          }
        }
      }
      return triangleMatrix;
    }

    public void CopyTo(Matrix m)
    {
      Data.CopyTo(m.Data, 0);
      Coloumns = m.Coloumns;
      Rows = m.Rows;
    }

    public double[][] ToJaggedArray()
    {
      double[][] jaggedArray = new double[Rows][];
      for (int i =0; i < Rows; i++)
      {
        for (int j = 0; j < Coloumns; j++)
        {
          jaggedArray[i][j] = this[i, j];
        }
      }
      return jaggedArray;
    }

    public double[,] To2DArray()
    {
      double[,] newArray = new double[this.Rows, this.Coloumns];
      {
        for (int i =0; i < this.Rows;i++)
        {
          for (int j=0; j < this.Coloumns;j++)
          {
            newArray[i, j] = this[i, j];
          }
        }
      }
      return newArray;
    }

    public double[] ToArray()
    {
      double[] newArray = new double[this.Rows * this.Coloumns];
      for (int i =0; i < this.Rows; i++)
      {
        for (int j = 0; j < this.Coloumns; j++)
        {
          newArray[i*j] = this[i, j];
        }
      }
      return newArray;
    }

    #region Indexators

    public double this[int i, int j]
    {
      get { return Data[i, j]; }
      set { Data[i, j] = value; }
    }

    public Vector this[int i]
    {
      get
      {
        Vector v = new Vector(this.Coloumns);
        for (int k = 0; k < this.Coloumns; k++)
        {
          v[k] = this[i, k];
        }
        return v;
      }
      set
      {
        for (int k = 0; k < value.Size; k++)
        {
          Data[i, k] = value[k];
        }
      }
    }
    #endregion

    #region Operators
    public static Matrix operator +(Matrix a, Matrix b)
    {
      if (a.Coloumns != b.Coloumns || b.Rows != a.Rows)
      {
        return null;
      }
      for (int i = 0; i < a.Rows; i++)
      {
        for (int j = 0; j < b.Coloumns; j++)
        {
          a.Data[i, j] += b.Data[i, j];
        }
      }
      return a;
    }

    public static Matrix operator -(Matrix a, Matrix b)
    {
      if (a.Coloumns != b.Coloumns || b.Rows != a.Rows)
      {
        return null;
      }
      for (int i = 0; i < a.Rows; i++)
      {
        for (int j = 0; j < b.Coloumns; j++)
        {
          a.Data[i, j] -= b.Data[i, j];
        }
      }
      return a;
    }

    public static Matrix operator *(Matrix m1, Matrix m2)
    {
      if (m1.Coloumns != m2.Rows)
      {
        throw new Exception("Can not multiply matricies! m1 m_n != m2 n_p");
      }
      Matrix resultMatrx = new Matrix(m1.Rows, m2.Coloumns);
      for (int i = 0; i < m1.Rows; i++)
      {
        for (int j = 0; j < m2.Coloumns; j++)
        {
          for (int k = 0; k < m2.Rows; k++)
          {
            resultMatrx[i, j] += m1[i, k] * m2[k, j];
          }
        }
      }
      return resultMatrx;
    }

    public static Vector operator *(Matrix m, Vector vector)
    {
      if (m.Rows != vector.Size)
      {
        return null;
      }
      double sum = 0;
      for (int i = 0; i < m.Rows; i++)
      {
        for (int j = 0; j < m.Coloumns; j++)
        {
          sum += m.Data[i, j] * vector[j];
        }
        vector[i] = sum;
        sum = 0;
      }
      return vector;
    }

    public static Vector operator *(Vector vector, Matrix m)
    {
      if (m.Rows != vector.Size)
      {
        return null;
      }
      double sum = 0;
      for (int i = 0; i < m.Rows; i++)
      {
        for (int j = 0; j < m.Coloumns; j++)
        {
          sum += m.Data[i, j] * vector[j];
        }
        vector[i] = sum;
        sum = 0;
      }
      return vector;
    }

    public static Matrix operator *(Matrix m, double scalar)
    {
      for (int i = 0; i < m.Rows; i++)
      {
        for (int j = 0; j < m.Coloumns; j++)
        {
          m.Data[i, j] *= scalar;
        }
      }
      return m;
    }

    public static Matrix operator *(double scalar, Matrix m)
    {
      for (int i = 0; i < m.Rows; i++)
      {
        for (int j = 0; j < m.Coloumns; j++)
        {
          m.Data[i, j] *= scalar;
        }
      }
      return m;
    }
    #endregion
  }
}

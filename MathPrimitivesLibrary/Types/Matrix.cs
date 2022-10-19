using System;
using System.Collections.Generic;

namespace MathPrimitivesLibrary
{
  public class Matrix
  {
    public int Rows { get; private set; }
    public int Coloumns { get; private set; }
    public Matrix IdentityMatrix { get { return MatrixHelper.IdentityMatrix(this.Rows); } }
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
    public Matrix(List<List<double>> data)
    {
      Rows = data.Count;
      Coloumns = data[0].Count;
      Data = new double[Rows, Coloumns];
      for (int i = 0; i < Rows; i++)
      {
        for (int j = 0; j < Coloumns; j++)
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
      Matrix identity = new Matrix(this.IdentityMatrix);
      Matrix inversedMatrix = new Matrix(this);
      for (int i = 0; i < inversedMatrix.Coloumns - 1; i++)
      {
        for (int j = i + 1; j < inversedMatrix.Rows; j++)
        {
          double koef = inversedMatrix[j, i] / inversedMatrix[i, i];
          for (int k = 0; k < inversedMatrix.Rows; k++)
          { 
            inversedMatrix[j, k] -= inversedMatrix[i, k] * koef;
            identity[j, k] -= identity[i, k] * koef;
          }
        }
      }
      for (int i = 0; i < inversedMatrix.Rows; i++)
      {
        double koef = inversedMatrix[i, i];
        for (int j = 0; j < inversedMatrix.Coloumns; j++)
        {
          identity[i, j] /= koef;
          inversedMatrix[i, j] /= koef;
        }
      }
      // После этого цикла имеем дело с нижне-треугольной матрицей
      for (int i = inversedMatrix.Rows-1; i > 0; i--)
      {
        for (int j = i - 1; j >= 0; j--)
        {
          double koef = inversedMatrix[j, i] / inversedMatrix[i, i];
          for (int k = inversedMatrix.Coloumns - 1; k >= 0; k--)
          {
            inversedMatrix[j, k] -= inversedMatrix[i, k] * koef;
            identity[j, k] -= identity[i, k] * koef;
          }
        }
      }
      return identity;
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
          if (MatrixHelper.KroneckerSymbol(i, j) == 1)
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
          double koef = triangleMatrix[j, i] / triangleMatrix[i, i];
          for (int k = 0; k < triangleMatrix.Rows - 1; k++)
          {
            triangleMatrix[j, k] -= triangleMatrix[i, k] * koef;
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
    public static Matrix operator +(Matrix a, double scalar)
    {
      Matrix m = new Matrix(a);
      for (int i = 0; i < a.Rows; i++)
      {
        for (int j = 0; j < a.Coloumns; j++)
        {
          m.Data[i, j] += scalar;
        }
      }
      return m;
    }
    public static Matrix operator -(Matrix a, double scalar)
    {
      Matrix m = new Matrix(a);
      for (int i = 0; i < a.Rows; i++)
      {
        for (int j = 0; j < a.Coloumns; j++)
        {
          m.Data[i, j] -= scalar;
        }
      }
      return m;
    }
    public static Matrix operator +(Matrix a, Matrix b)
    {
      Matrix m = new Matrix(a);
      if (a.Coloumns != b.Coloumns || b.Rows != a.Rows)
      {
        throw new Exception("error");
      }
      for (int i = 0; i < a.Rows; i++)
      {
        for (int j = 0; j < b.Coloumns; j++)
        {
          m.Data[i, j] += b.Data[i, j];
        }
      }
      return m;
    }

    public static Matrix operator -(Matrix a, Matrix b)
    {
      if (a.Coloumns != b.Coloumns || b.Rows != a.Rows)
      {
        throw new Exception("error");
      }
      Matrix m = new Matrix(a);
      for (int i = 0; i < a.Rows; i++)
      {
        for (int j = 0; j < b.Coloumns; j++)
        {
          m.Data[i, j] -= b.Data[i, j];
        }
      }
      return m;
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
        throw new Exception("error");
      }
      Vector v = new Vector(vector);
      double sum = 0;
      for (int i = 0; i < m.Rows; i++)
      {
        for (int j = 0; j < m.Coloumns; j++)
        {
          sum += m.Data[i, j] * v[j];
        }
        v[i] = sum;
        sum = 0;
      }
      return vector;
    }

    public static Vector operator *(Vector vector, Matrix m)
    {
      if (m.Rows != vector.Size)
      {
        throw new Exception("error");
      }
      Vector v = new Vector(vector);
      double sum = 0;
      for (int i = 0; i < m.Rows; i++)
      {
        for (int j = 0; j < m.Coloumns; j++)
        {
          sum += m.Data[i, j] * v[j];
        }
        v[i] = sum;
        sum = 0;
      }
      return vector;
    }

    public static Matrix operator *(Matrix m, double scalar)
    {
      Matrix matrix = new Matrix(m);
      for (int i = 0; i < m.Rows; i++)
      {
        for (int j = 0; j < m.Coloumns; j++)
        {
          matrix.Data[i, j] *= scalar;
        }
      }
      return matrix;
    }

    public static Matrix operator *(double scalar, Matrix m)
    {
      Matrix matrix = new Matrix(m);
      for (int i = 0; i < m.Rows; i++)
      {
        for (int j = 0; j < m.Coloumns; j++)
        {
          matrix.Data[i, j] *= scalar;
        }
      }
      return matrix;
    }

    public static Matrix operator /(Matrix m, double scalar)
    {
      Matrix matrix = new Matrix(m);
      for (int i = 0; i < m.Rows; i++)
      {
        for (int j = 0; j < m.Coloumns; j++)
        {
          matrix.Data[i, j] /= scalar;
        }
      }
      return matrix;
    }

    public static Matrix operator /(double scalar, Matrix m)
    {
      Matrix matrix = new Matrix(m);
      for (int i = 0; i < m.Rows; i++)
      {
        for (int j = 0; j < m.Coloumns; j++)
        {
          matrix.Data[i, j] /= scalar;
        }
      }
      return matrix;
    }
    #endregion
  }
}

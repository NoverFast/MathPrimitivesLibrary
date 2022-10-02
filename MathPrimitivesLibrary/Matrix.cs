using System;

namespace MathPrimitivesLibrary
{
  public class Matrix
  {
    public int Rows { get; private set; }
    public int Coloumns { get; private set; }
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
      this.Data = data;
    }
    public Matrix(int rows, int coloumns, double[,] data)
    {
      Rows = rows;
      Coloumns = coloumns;
      if (data.GetLength(0) != Rows && data.GetLength(1) != coloumns)
      {
        throw new Exception("Data size is not the same as matrix size!");
      }
      this.Data = data;
    }

    public Matrix(Matrix m)
    {
      if (Data != null)
      {
        Data.CopyTo(m.Data, 0);
      }
      m.Rows = Rows;
      m.Coloumns = Coloumns;
    }
    
    public void Show()
    {
      for (int i =0; i < Rows; i++)
      {
        for (int j = 0; j < Coloumns; j++)
        {
          Console.Write(" {0}", Data[i, j]);
        }
        Console.WriteLine();
      }
    }

    public Matrix Inverse()
    {
      double determinant = Data[0, 0] * Data[1, 1] - Data[0, 1] * Data[1, 0];
      return new Matrix(2, 2, new double[,]
      {
        { Data[1, 1] / determinant, -Data[1, 0] / determinant },
        { -Data[0, 1] / determinant, Data[1, 1] / determinant }
      });
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
      double determinant = 0;
      for (int i =0; i < Coloumns; i++)
      {
        for (int j =0; j < Rows; j++)
        {

        }
      }
      return determinant;
    }

    public Matrix Triagonalize()
    {
      Console.WriteLine();
      Matrix triangleMatrix = new Matrix(this.Rows, this.Coloumns, this.Data);
      for (int i =0; i < triangleMatrix.Coloumns - 2; i++)
      {
        for (int j = i + 1; j < triangleMatrix.Coloumns - 1; j++)
        {
            triangleMatrix[j] -= triangleMatrix[j, i] / triangleMatrix[i, i] * triangleMatrix[j]; 
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

    public Vector this[int i]
    {
      get
      {
        Vector v = new Vector(this.Coloumns);
        for (int k = 0; k < this.Coloumns; k++)
        {
          v[i] = this[i, k];
        }
        return v;
      }
      set
      {
        for (int k =0; k < value.Size; k++)
        {
          Data[i, k] = value[k];
        }
      }
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

    public double this[int i, int j]
    {
      get { return Data[i, j]; }
      set { Data[i, j] = value; }
    }
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
  }
}

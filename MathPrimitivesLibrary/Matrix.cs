namespace MathPrimitiveLibrary
{
  public class Matrix
  {
    public int Rows { get; private set; }
    public int Coloumns { get; private set; }
    public double[,] Data { get; private set; }

    public Matrix()
    {
    }
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
      Data.CopyTo(m.Data, 0);
      Rows = m.Rows;
      Coloumns = m.Coloumns;
    }
    
    public void ShowMatrix()
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

    public Matrix InverseMatrix()
    {
      double determinant = Data[0, 0] * Data[1, 1] - Data[0, 1] * Data[1, 0];
      return new Matrix(2, 2, new double[,]
      {
        { Data[1, 1] / determinant, -Data[1, 0] / determinant },
        { -Data[0, 1] / determinant, Data[1, 1] / determinant }
      });
    }


    public void CopyTo(Matrix m)
    {
      Data.CopyTo(m.Data, 0);
      Coloumns = m.Coloumns;
      Rows = m.Rows;
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

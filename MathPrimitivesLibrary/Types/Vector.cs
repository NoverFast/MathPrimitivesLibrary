using System;
using System.Runtime.CompilerServices;

namespace MathPrimitivesLibrary
{
  public class Vector
  {
    #region Properties
    public double[] Data { get; private set; }
    public int Size { get; private set; }
    public double Magnitude { get { return Norm(this.Size); } }
    #endregion

    public Vector(int n)
    {
      Data = new double[n];
      Size = n;
    }
    public Vector(double[] data)
    {
      this.Data = data;
      Size = data.GetLength(0);
    }

    public Vector(int n, double[] data)
    {
      if (data.Length != n)
      {
        throw new Exception("Provided data size is not the same as vector size!");
      }
      this.Data = data;
      Size = n;
    }

    public Vector(Vector v)
    {
      Data = (double[])v.Data.Clone();
      Size = v.Size;
    }

    public void Show(int roundTo = -1)
    {
      bool needRounding;
      if (roundTo < 0)
      {
        needRounding = false;
      }
      else
      {
        needRounding = true;
      }
      for (int i = 0; i < Size; i++)
      {
        Console.Write("\t{0}", needRounding ? Math.Round(Data[i], roundTo) : Data[i]);
      }
      Console.WriteLine();
    }

    public double Norm(int order = 2)
    {
      double sum = 0;
      for (int i =0; i < this.Size; i++)
      {
        sum += Math.Pow(Math.Abs(this[i]), order);
      }
      return Math.Pow(sum, 1.0 / order);
    }

    public double DotProduct(Vector v)
    {
      if (this.Size != v.Size)
      {
        throw new Exception("Vector Sizes are different!");
      }
      double sum = 0;
      for (int i = 0; i < this.Size; i++)
      {
        sum += this[i] * v[i];
      }
      return sum;
    }

    public Vector CrossProduct(Vector v)
    {
      return new Vector(new double[] {
      this.Data[1] * v.Data[2] - this.Data[2] * v.Data[1],
      this.Data[0] * v.Data[2] - this.Data[2] * v.Data[0],
      this.Data[0] * v.Data[1] - this.Data[1] * v.Data[0]
      });
    }

    public double[] ToArray()
    {
      double[] newArray =  new double[this.Size];
      for (int i =0; i < this.Size;i++)
      {
        newArray[i] = this[i];
      }
      return newArray;
    }
    public void CopyTo(Vector m)
    {
      Data.CopyTo(m.Data, 0);
      Size = m.Size;
    }

    #region Indexators
    public double this[int i]
    {
      get { return Data[i]; }
      set { Data[i] = value; }
    }
    #endregion

    #region Operators

    public static Vector operator +(Vector v1, Vector v2)
    {
      if (v1.Size != v2.Size)
      {
        throw new Exception("Vector Sizes are different!");
      }
      Vector vector = new Vector(v1.Size, new double[v1.Size]);
      for (int i =0; i < v1.Size; i++)
      {
        vector[i] = v1[i] + v2[i];
      }
      return vector;
    }

    public static Vector operator -(Vector v1, Vector v2)
    {
       if (v1.Size != v2.Size)
      {
        throw new Exception("Vector Sizes are different!");
      }
      Vector vector = new Vector(v1.Size, new double[v1.Size]);
      for (int i =0; i < v1.Size; i++)
      {
        vector[i] = v1[i] - v2[i];
      }
      return vector;
    }

    public static Vector operator *(double scalar, Vector v)
    {
      Vector vector = new Vector(v.Size, new double[v.Size]);
      for (int i =0; i < v.Size; i++)
      {
        vector[i] *= scalar;
      }
      return vector;
    }

    public static Vector operator *(Vector v, double scalar)
    {
      Vector vector = new Vector(v.Size, new double[v.Size]);;
      for (int i =0; i < v.Size; i++)
      {
        vector[i] *= scalar;
      }
      return vector;
    }

    public static Vector operator /(double scalar, Vector v)
    {
      Vector vector = new Vector(v.Size, new double[v.Size]);
      for (int i = 0; i < v.Size; i++)
      {
        vector[i] /= scalar;
      }
      return vector;
    }
    public static Vector operator /(Vector v, double scalar)
    {
      Vector vector = new Vector(v.Size, new double[v.Size]); ;
      for (int i = 0; i < v.Size; i++)
      {
        vector[i] /= scalar;
      }
      return vector;
    }

    /// <summary>
    /// Умножение вектора на вектор принимается в скалярном смысле.
    /// </summary>
    /// <param name="v"></param>
    /// <returns></returns>
    public static double operator *(Vector v1, Vector v2)
    {
      if (v1.Size != v2.Size)
      {
        Console.WriteLine("Размеры векторов различаются");
      }
      double sum = 0;
      for (int i =0; i < v1.Size; i++)
      {
        sum += v1[i] * v2[i];
      }
      return sum;
    }
    #endregion
  }
}

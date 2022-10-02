using System;

namespace MathPrimitivesLibrary
{
  public class Vector
  {
    public double[] Data { get; private set; }

    public double[] GetData()
    {
      return Data;
    }

    public void SetData(double[] newData)
    {
      Data = newData;
    }

    public int Size { get; private set; }
    public double Length { get { return DotProduct(this); } }

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
      Data.CopyTo(v.Data, 0);
      Size = v.Size;
    }

    public void ShowVector()
    {
      for (int i =0; i < Size; i++)
      {
        Console.Write(" {0}", Data[i]);
      }
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

    /*public Vector CrossProduct(Vector v1, Vector v2)
    {
      return new Vector( ;
    }*/

    public double[] ToArray()
    {
      double[] newArray =  new double[this.Size];
      for (int i =0; i < this.Size;i++)
      {
        newArray[i] = this[i];
      }
      return newArray;
    }

    public double this[int i]
    {
      get { return Data[i]; }
      set { Data[i] = value; }
    }

    public void CopyTo(Vector m)
    {
      Data.CopyTo(m.Data, 0);
      Size = m.Size;
    }


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
  }
}

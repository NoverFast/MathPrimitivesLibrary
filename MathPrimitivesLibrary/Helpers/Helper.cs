using System;
using System.Collections.Generic;

namespace MathPrimitivesLibrary
{
  public static class Helper
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
  }
}

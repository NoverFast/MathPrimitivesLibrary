using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MathPrimitivesLibrary.Exceptions
{
  internal class MatrixVectorDifferrentSizeException : Exception
  {
    public MatrixVectorDifferrentSizeException() : base()
    {
      
    }
    public MatrixVectorDifferrentSizeException(string message) : base(message)
    { 
      
    }
    public MatrixVectorDifferrentSizeException(string message, Exception innerException) : base(message, innerException)
    {

    }
  }
}

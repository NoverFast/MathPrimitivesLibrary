using System;
using System.Drawing;
using MathPrimitivesLibrary.Types.Meshes;

namespace MathPrimitivesLibrary.Types.Quadratures
{
  public enum RectangleQuadratureSolveType : byte
  {
    LeftRectangle = 0, RightRectangle = 1, MiddleRectangle = 2
  }
  public class RectangleQuadrature : AbstractCompositionalQuadrature
  {
    public readonly int QuadratureSolveType;
    public RectangleQuadrature(RegularMesh1D mesh, 
      Func<double, double> function, RectangleQuadratureSolveType type) : base(mesh, function)
    {
      QuadratureSolveType = (int)type;
    }

    public override double Calculate()
    {
      switch (QuadratureSolveType)
      {
        case (int)RectangleQuadratureSolveType.LeftRectangle:
          return Left();
        case (int)RectangleQuadratureSolveType.RightRectangle:
          return Right();
        case (int)RectangleQuadratureSolveType.MiddleRectangle:
          return Middle();
      }
      return 0;
    }

    private double Left()
    {
      double sum = 0;
      for (int i = 0; i < mesh.numberOfSteps-1; i++)
      {
        sum += function(mesh.GridPoints[i]);
      }
      return sum * mesh.StepLength;
    }

    private double Right()
    {
      double sum = 0;
      for (int i = 1; i < mesh.numberOfSteps; i++)
      {
        sum += function(mesh.GridPoints[i]);
      }
      return sum * mesh.StepLength;
    }
    private double Middle()
    {
      double sum = 0;
      for (int i = 0; i < mesh.numberOfSteps; i++)
      {
        sum += function(mesh.GridPoints[i] + mesh.StepLength / 2);
      }
      return sum * mesh.StepLength;
    }
  }
}

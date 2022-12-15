using System;
using System.Drawing;
using MathPrimitivesLibrary.Types.Meshes;

namespace MathPrimitivesLibrary.Types.Quadratures
{
  public class Rectangle : AbstractCompositionalQuadrature
  {
    public Rectangle(RegularMesh1D mesh, Func<double, double> function) : base(mesh, function)
    {
    }

    public double Left()
    {
      double sum = 0;
      for (int i = 0; i < mesh.numberOfSteps; i++)
      {
        mesh.MeshY[i] = function(mesh.MeshX[i]);
        mesh.MeshQuadratureData[i] = mesh.MeshY[i];
        sum += mesh.MeshQuadratureData[i];
      }
      return sum * mesh.StepLength;
    }

    public double Right()
    {
      double sum = 0;
      for (int i = 1; i <= mesh.numberOfSteps; i++)
      {
        mesh.MeshY[i] = function(mesh.MeshX[i]);
        mesh.MeshQuadratureData[i] = mesh.MeshY[i];
        sum += mesh.MeshQuadratureData[i];
      }
      return sum * mesh.StepLength;
    }
    public double Middle()
    {
      double sum = 0;
      for (int i = 1; i <= mesh.numberOfSteps; i++)
      {
        mesh.MeshY[i] = function(mesh.MeshX[i]);
        mesh.MeshQuadratureData[i] = function(mesh.MeshX[i] - mesh.StepLength / 2);
        sum += mesh.MeshQuadratureData[i];
      }
      return sum * mesh.StepLength;
    }
  }
}

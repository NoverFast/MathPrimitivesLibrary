using MathPrimitivesLibrary.Types.Meshes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MathPrimitivesLibrary.Types.Quadratures
{
  public class Trapezoid : AbstractCompositionalQuadrature
  {
    public Trapezoid(RegularMesh mesh, Func<double, double> function) : base(mesh, function)
    {

    }

    public override double Calculate()
    {
      double sum = 0;
      double tmp = (mesh.leftEdge + mesh.rightEdge) / 2;
      double tmpSum = 0;
      for (int i = 1; i < mesh.numberOfSteps; i++)
      {
        mesh.MeshY[i] = function(mesh.MeshX[i]);
        tmpSum += function(mesh.MeshX[i]);
        mesh.MeshQuadratureData[i] = tmp + tmpSum;
        sum = mesh.MeshQuadratureData[i];
      }
      return sum * mesh.StepLength;
    }
  }
}

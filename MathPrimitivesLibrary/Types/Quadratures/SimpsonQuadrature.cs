using MathPrimitivesLibrary.Types.Meshes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MathPrimitivesLibrary.Types.Quadratures
{
  public class SimpsonQuadrature : AbstractCompositionalQuadrature
  {
    public SimpsonQuadrature(RegularMesh1D mesh, Func<double, double> function) : base(mesh, function)
    { }

    public override double Calculate()
    {
      throw new NotImplementedException();
      double sum1 = 0;
      double sum2 = 0;
      int N = mesh.numberOfSteps * 2;
      for (int i = 1; i <= N - 1; i += 2)
      {
        //sum2 += function(mesh.MeshX[i - 1]) + 4 * function(mesh.MeshX[i]) + function(mesh.MeshX[i + 1]);
        //mesh.MeshY[i / 2] = function(mesh.MeshX[i]);
        //mesh.MeshQuadratureData[i / 2] = sum2;
      }
      return mesh.StepLength / 6 * (function(mesh.leftEdge) + function(mesh.rightEdge) + 2 * sum1 + 4 * sum2);
    }
  }
}

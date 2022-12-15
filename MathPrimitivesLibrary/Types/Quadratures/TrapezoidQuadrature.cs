using MathPrimitivesLibrary.Types.Meshes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MathPrimitivesLibrary.Types.Quadratures
{
  public class TrapezoidQuadrature : AbstractCompositionalQuadrature
  {
    public TrapezoidQuadrature(RegularMesh1D mesh, Func<double, double> function) : base(mesh, function)
    {}

    public override double Calculate()
    {
      double sum = (function(mesh.GridPoints[0]) + function(mesh.GridPoints[mesh.GridPoints.Size - 1])) / 2; ;
      for (int i = 1; i < mesh.numberOfSteps-1; i++)
      {
        sum += function(mesh.GridPoints[i]);
      }
      return sum * mesh.StepLength;
    }
  }
}

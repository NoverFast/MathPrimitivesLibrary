using System;
using MathPrimitivesLibrary.Types.Meshes;

namespace MathPrimitivesLibrary.Types.Quadratures
{
  class Rectangle : AbstractCompositionalQuadrature
  {
    private bool useCentral { get; }
    public Rectangle(RegularMesh mesh, Func<double, double> function, bool useCentral) : base(mesh, function)
    {
      this.useCentral = useCentral;
    }

    public override void Solve()
    {
      if (useCentral)
      {

      }
      else
      {
        RightLeft();
      }
    }

    private void Middle()
    {
      double sum = 0;
    }

    private void RightLeft()
    {
      double sum = 0;
      for (int i = 1; i < mesh.NumberOfSteps; i++)
      {
        sum += function(mesh.MeshX[i]) * mesh.StepLength;
        mesh.MeshX[i] = mesh.StepLength * i;
        mesh.MeshY[i] = sum;
      }
    }
  }
}

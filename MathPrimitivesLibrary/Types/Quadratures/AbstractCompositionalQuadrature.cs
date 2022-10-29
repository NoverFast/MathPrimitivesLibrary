using MathPrimitivesLibrary.Types.Meshes;
using System;

namespace MathPrimitivesLibrary.Types.Quadratures
{
  abstract class AbstractCompositionalQuadrature
  {
    protected RegularMesh mesh { get; set; }
    protected Func<double, double> function { get; set; }
    public AbstractCompositionalQuadrature(RegularMesh mesh, Func<double, double> function)
    {
      this.mesh = mesh;
      this.function = function;
      this.mesh.MeshX[0] = mesh.LeftEdge;
      this.mesh.MeshY[0] = function(mesh.LeftEdge);
    }
    
    public virtual void Solve()
    { }
  }
}

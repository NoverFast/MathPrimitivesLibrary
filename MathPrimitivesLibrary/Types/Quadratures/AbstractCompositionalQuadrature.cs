using MathPrimitivesLibrary.Types.Meshes;
using System;

namespace MathPrimitivesLibrary.Types.Quadratures
{
  public abstract class AbstractCompositionalQuadrature
  {
    protected RegularMesh mesh { get; set; }
    protected Func<double, double> function { get; set; }
    public double area { get; }
    public AbstractCompositionalQuadrature(RegularMesh mesh, Func<double, double> function)
    {
      this.mesh = mesh;
      this.function = function;
    }
    
    public virtual void Solve()
    { }
  }
}

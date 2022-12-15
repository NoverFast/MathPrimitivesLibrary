using MathPrimitivesLibrary.Types.Meshes;
using System;

namespace MathPrimitivesLibrary.Types.Quadratures
{
  public abstract class AbstractCompositionalQuadrature
  {
    protected RegularMesh1D mesh { get; set; }
    protected Func<double, double> function { get; set; }
    public double area { get; }
    public AbstractCompositionalQuadrature(RegularMesh1D mesh, Func<double, double> function)
    {
      this.mesh = mesh;
      this.function = function;
    }
    
    public virtual double Calculate()
    {
      return 0;
    }
  }
}

using MathPrimitivesLibrary.Types.Meshes;

namespace MathPrimitivesLibrary.Types.Quadratures
{
  abstract class AbstractCompositionalQuadrature
  {
    protected RegularMesh mesh { get; set; }
    public void AbstractCompositionalQuadrature(double leftEdge, double rightEdge, RegularMesh mesh)
  }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MathPrimitivesLibrary.Types.Meshes
{
  public abstract class AbstractMesh
  {
    public double leftEdge { get; }
    public double rightEdge { get; }
    public double numberOfSteps { get; }
    public AbstractMesh(double leftEdge, double rightEdge, int numberOfSteps)
    {
      this.leftEdge = leftEdge;
      this.rightEdge = rightEdge;
      this.numberOfSteps = numberOfSteps;
    }
  }
}

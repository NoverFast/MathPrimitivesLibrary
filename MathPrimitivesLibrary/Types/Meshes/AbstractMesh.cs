using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MathPrimitivesLibrary.Types.Meshes
{
  public abstract class AbstractMesh
  {
    public double leftEdge { get; set; }
    public double rightEdge { get; set; }
    public int numberOfSteps { get; set; }
    public AbstractMesh(double leftEdge, double rightEdge, int numberOfSteps)
    {
      this.leftEdge = leftEdge;
      this.rightEdge = rightEdge;
      this.numberOfSteps = numberOfSteps;
    }
    public abstract void ClearMeshData();

    public virtual void ShowMeshProperties(bool showGrid = false, int roundTo = -1)
    {
      Console.WriteLine($"Left Edge: {leftEdge}" +
        $"\nRight Edge: {rightEdge}" +
        $"\nNumber of steps: {numberOfSteps}");
    }
  }
}

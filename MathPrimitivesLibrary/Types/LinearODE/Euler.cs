using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MathPrimitivesLibrary.Types.LinearODE
{
  public class Euler : AbstractODESolver
  {
    /// <summary>
    /// Метод Эйлера 1 порядка для решения ОДЕ.
    /// </summary>
    /// <param name="leftEdge"></param>
    /// <param name="rightEdge"></param>
    /// <param name="numberOfSteps"></param>
    /// <param name="function"></param>
    public Euler(double leftEdge, double rightEdge, int numberOfSteps, double initialCondition, Func<double, double, double> function) 
      : base(leftEdge, rightEdge, numberOfSteps, initialCondition, function)
    { }
    public override void Solve()
    {
      for (int i = 1; i <= mesh.NumberOfSteps; i++)
      {
        mesh.MeshX[i] += mesh.StepLength;
        mesh.MeshY[i] = mesh.MeshY[i - 1] + mesh.StepLength * function(mesh.MeshX[i - 1], mesh.MeshY[i - 1]);
      }
#if VERBOSE
      mesh.GetMeshData();
#endif
    }
  }
}

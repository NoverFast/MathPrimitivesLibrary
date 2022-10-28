using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MathPrimitivesLibrary.Types.LinearODE
{
  public class RungeKutta4 : AbstractODESolver
  { 
    /// <summary>
    /// Метод Рунге-Кутта 4 порядка для решения ОДЕ.
    /// </summary>
    /// <param name="leftEdge">Левый Край</param>
    /// <param name="rightEdge"></param>
    /// <param name="numberOfSteps"></param>
    /// <param name="function"></param>
    public RungeKutta4(double leftEdge, double rightEdge, int numberOfSteps, double initialCondition, Func<double, double, double> function) 
      : base(leftEdge, rightEdge, numberOfSteps, initialCondition, function)
    {
    }
    public override void Solve()
    {
      for (int i = 1; i < mesh.NumberOfSteps; i++)
      {
        double k1 = function(mesh.MeshX[i - 1], mesh.MeshY[i - 1]);
        double k2 = function(mesh.MeshX[i - 1] + mesh.StepLength / 2, mesh.MeshY[i - 1] + mesh.StepLength / 2 * k1);
        double k3 = function(mesh.MeshX[i - 1] + mesh.StepLength / 2, mesh.MeshY[i - 1] + mesh.StepLength / 2 * k2);
        double k4 = function(mesh.MeshX[i - 1] + mesh.StepLength, mesh.MeshY[i - 1] + mesh.StepLength * k3);
        mesh.MeshX[i] += mesh.StepLength;
        mesh.MeshY[i] = mesh.StepLength / 6 * (k1 + 2 * k2 + 2 * k3 + k4);
      }
    }
  }
}

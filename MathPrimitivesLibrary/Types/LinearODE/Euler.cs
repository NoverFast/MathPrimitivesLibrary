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
    /// Начальное условие задачи Коши
    /// </summary>
    private double y0 { get; set; }
    private double x0 { get; set; }
    private Func<double, double> function { get; set; }
    /// <summary>
    /// Метод Эйлера является одношаговым, следовательно при инициализации сетки в ней уже будет как минимум одно значение y(leftEdge) (Начальные Условия)
    /// </summary>
    /// <param name="leftEdge"></param>
    /// <param name="rightEdge"></param>
    /// <param name="numberOfSteps"></param>
    /// <param name="function"></param>
    public Euler(double leftEdge, double rightEdge, int numberOfSteps, Func<double, double> function) : base(leftEdge, rightEdge, numberOfSteps, function)
    {
      this.function = function;
      x0 = mesh.MeshSteps[0] = leftEdge;
      y0 = mesh.MeshValue[0] = this.function(leftEdge);
    }
    public void SolveODE()
    {
      for (int i = 1; i <= mesh.NumberOfSteps; i++)
      {
        mesh.MeshSteps[i] += mesh.StepLength;
        mesh.MeshValue[i] = mesh.MeshValue[i - 1] + mesh.StepLength * function(mesh.MeshSteps[i]);
      }
#if VERBOSE
      mesh.GetMeshData();
#endif
    }
  }
}

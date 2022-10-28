using System;
using MathPrimitivesLibrary.Types.Meshes;

namespace MathPrimitivesLibrary.Types.LinearODE
{
  public class AbstractODESolver 
  {
    protected double x0 { get; set; }
    protected double y0 { get; set; }
    protected double f { get; set; }
    protected Func<double, double, double> function { get; set; }
    protected Mesh mesh { get; set; }

    /// <summary>
    /// При вызове конструктора происходит создание сетки и инициализация первого элемента сетки начальными данными.
    /// </summary>
    /// <param name="leftEdge"> Левая граница расчёта </param>
    /// <param name="rightEdge"> Правая граница расчёта </param>
    /// <param name="numberOfSteps"> Кол-во шагов на сетке </param>
    /// <param name="function"> Исходная задача </param>
    
    public AbstractODESolver(double leftEdge, double rightEdge, int numberOfSteps, double initialCondition, Func<double, double, double> function)
    {
      mesh = new Mesh(leftEdge, rightEdge, numberOfSteps);
      this.function = function;
      x0 = mesh.MeshX[0] = leftEdge;
      y0 = mesh.MeshY[0] = initialCondition;
      f = mesh.FunctionData[0] = this.function(x0, y0);
    }

    public virtual void Solve()
    { }
  }
}

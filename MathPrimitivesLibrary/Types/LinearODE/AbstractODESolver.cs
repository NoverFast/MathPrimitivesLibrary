using System;
using MathPrimitivesLibrary.Types.Meshes;

namespace MathPrimitivesLibrary.Types.LinearODE
{
  public class AbstractODESolver 
  {
    protected Mesh mesh { get; set; }
    /// <summary>
    /// При вызове конструктора происходит создание сетки и инициализация первого элемента сетки начальными данными.
    /// </summary>
    /// <param name="leftEdge"> Левая граница расчёта </param>
    /// <param name="rightEdge"> Правая граница расчёта </param>
    /// <param name="steps"> Кол-во шагов на сетке </param>
    /// <param name="function"> Исходная задача </param>
    
    public AbstractODESolver(double leftEdge, double rightEdge, int numberOfSteps, Func<double, double> function)
    {
      mesh = new Mesh(leftEdge, rightEdge, numberOfSteps);
    }
  }
}

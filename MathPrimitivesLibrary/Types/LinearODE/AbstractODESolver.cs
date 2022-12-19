using System;
using MathPrimitivesLibrary.Types.Meshes;

namespace MathPrimitivesLibrary.Types.LinearODE
{
  public abstract class AbstractODESolver 
  {
    protected Func<double, double, double> function { get; set; }
    protected RegularMesh1D mesh { get; set; }
    
    /// <summary>
    /// При вызове конструктора происходит создание сетки и инициализация первого элемента сетки начальными данными.
    /// </summary>
    /// <param name="function"> Исходная задача </param>
    
    public AbstractODESolver(RegularMesh1D mesh, double initialCondition, Func<double, double, double> function)
    {
      this.mesh = mesh;
      this.function = function;
      mesh.Grid[0] = initialCondition;
    }

    public abstract void Solve();
  }
}

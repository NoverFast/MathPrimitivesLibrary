using System;
using MathPrimitivesLibrary.Types.Meshes;

namespace MathPrimitivesLibrary.Types.LinearODE
{
  public class Euler : AbstractODESolver
  {
    /// <summary>
    /// Метод Эйлера 1 порядка для решения ОДЕ.
    /// </summary>
    /// <param name="mesh"> Сетка, на которой будет производится вычисление </param>
    /// <param name="initialCondition"> Начальное условие задачи Коши </param>
    /// <param name="function"> Функция, которую нужно посчитать</param>
    public Euler(RegularMesh1D mesh, double initialCondition, Func<double, double, double> function)
      : base(mesh, initialCondition, function)
    { }
    public override void Solve()
    {
      for (int i = 1; i < mesh.numberOfSteps; i++)
      {
        mesh.Grid[i] = mesh.Grid[i - 1] + mesh.StepLength * function(mesh.Grid[i - 1], mesh.Grid[i - 1]);
      }
    }
  }
}

using System;
using MathPrimitivesLibrary.Types.Meshes;

namespace MathPrimitivesLibrary.Types.LinearODE
{
  public class RungeKutta4 : AbstractODESolver
  { 
    /// <summary>
    /// Метод Рунге-Кутта 4 порядка для решения ОДЕ.
    /// </summary>
    /// <param name="mesh"> Сетка, на которой будет производится вычисление </param>
    /// <param name="initialCondition"> Начальное условие задачи Коши </param>
    /// <param name="function"> Функция, которую нужно посчитать</param>
    public RungeKutta4(RegularMesh1D mesh, double initialCondition, Func<double, double, double> function)
      : base(mesh, initialCondition, function)
    {
    }
    public override void Solve()
    {
      double k1, k2, k3, k4;
      for (int i = 1; i < mesh.numberOfSteps; i++)
      {
        k1 = function(mesh.GridPoints[i - 1], mesh.Grid[i - 1]);
        k2 = function(mesh.GridPoints[i - 1] + mesh.StepLength / 2, 
          mesh.Grid[i - 1] + mesh.StepLength / 2 * k1);
        k3 = function(mesh.GridPoints[i - 1] + mesh.StepLength / 2,
          mesh.Grid[i - 1] + mesh.StepLength / 2 * k2);
        k4 = function(mesh.GridPoints[i - 1] + mesh.StepLength, mesh.Grid[i - 1] + mesh.StepLength * k3);
        mesh.Grid[i] = mesh.StepLength / 6 * (k1 + 2 * k2 + 2 * k3 + k4);
      }
    }
  }
}

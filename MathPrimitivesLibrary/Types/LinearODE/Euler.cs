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
    public Euler(RegularMesh mesh, double initialCondition, Func<double, double, double> function)
      : base(mesh, initialCondition, function)
    { }
    public override double Solve()
    {
      for (int i = 1; i <= mesh.numberOfSteps; i++)
      {
        mesh.MeshX[i] += mesh.StepLength;
        mesh.MeshY[i] = mesh.MeshY[i - 1] + mesh.StepLength * function(mesh.MeshX[i - 1], mesh.MeshY[i - 1]);
      }
#if VERBOSE
      mesh.GetMeshData();
#endif
      return 0;
    }
  }
}

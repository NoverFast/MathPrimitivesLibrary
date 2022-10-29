using System;
using MathPrimitivesLibrary.Types.Meshes;

namespace MathPrimitivesLibrary.Types.LinearODE
{
  public class Adams : AbstractODESolver
  {
    /// <summary>
    /// Метод Адамса 2 порядка для решения ОДЕ.
    /// </summary>
    /// <param name="mesh"> Сетка, на которой будет производится вычисление </param>
    /// <param name="initialCondition"> Начальное условие задачи Коши </param>
    /// <param name="function"> Функция, которую нужно посчитать</param>
    public Adams(RegularMesh mesh, double initialCondition, Func<double, double, double> function) 
      : base(mesh, initialCondition,  function)
    { }
  }
}

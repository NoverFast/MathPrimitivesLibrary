using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MathPrimitivesLibrary.Types.LinearODE
{
  public class Adams : AbstractODESolver
  {
    public Adams(double leftEdge, double rightEdge, int numberOfSteps, double initialCondition, Func<double, double, double> function) 
      : base(leftEdge, rightEdge, numberOfSteps, initialCondition, function)
    { }
  }
}

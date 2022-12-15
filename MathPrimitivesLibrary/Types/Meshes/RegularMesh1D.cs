using System;

namespace MathPrimitivesLibrary.Types.Meshes
{
  public class RegularMesh1D : AbstractMesh
  {
    public double StepLength { get; set; }
    public Vector Grid { get; set; }
    public Vector GridPoints { get; set; }
    /// <summary>
    /// Конструктор регулярной сетки. Хранит в себе массивы размерности <paramref name="numberOfSteps"/> + 1
    /// </summary>
    /// <param name="leftEdge"></param>
    /// <param name="rightEdge"></param>
    /// <param name="numberOfSteps"></param>
    public RegularMesh1D(double leftEdge, double rightEdge, int numberOfSteps) : base(leftEdge, rightEdge, numberOfSteps)
    {
      StepLength = (rightEdge - leftEdge) / numberOfSteps;
      Grid = new Vector(numberOfSteps);
      GridPoints = new Vector(numberOfSteps);
#if DEBUG
      ShowMeshProperties();
#endif
    }

    public void FillGridPoints()
    {
      for (int i = 0; i < GridPoints.Size; i++)
      {
        GridPoints[i] = i * StepLength;
      }
    }

    public override void ShowMeshProperties()
    {
      Console.WriteLine($"Left Edge: {this.leftEdge}");
      Console.WriteLine($"Right Edge: {this.rightEdge}");
      Console.WriteLine($"Number of Steps: {this.numberOfSteps}");
      Console.WriteLine($"Step Length: {this.StepLength}");
#if DEBUG
      GridPoints.Show();
#endif 
    }

    public override void ClearMeshData()
    {
      for (int i = 0; i < GridPoints.Size; i++)
      {
        Grid[i] = 0;
      }
    }
  }
}

using System;

namespace MathPrimitivesLibrary.Types.Meshes
{
  public class RegularMesh : AbstractMesh
  {
    public double StepLength { get; set; }
    public double[] MeshX { get; set; }
    public double[] MeshY { get; set; }
    public double[] FunctionData { get; set; }
    public RegularMesh(double leftEdge, double rightEdge, int numberOfSteps) : base(leftEdge, rightEdge, numberOfSteps)
    {
      StepLength = (rightEdge - leftEdge) / numberOfSteps;
      MeshX = new double[numberOfSteps + 1];
      for (int i = 0; i <= numberOfSteps; i++)
      {
        MeshX[i] = StepLength * i;
      }
      MeshY = new double[numberOfSteps + 1];
      FunctionData = new double[numberOfSteps + 1];
#if VERBOSE
        GetMeshProperties();
        GetMeshData();
#endif
    }

    private void GetMeshProperties()
    {
      Console.WriteLine($"Left Edge: {this.leftEdge}");
      Console.WriteLine($"Right Edge: {this.rightEdge}");
      Console.WriteLine($"Number of Steps: {this.numberOfSteps}");
      Console.WriteLine($"Step Length: {this.StepLength}");
    }

    public void GetMeshData()
    {
      Console.WriteLine("x: ");
      foreach (double x in MeshX)
      {
        Console.Write($"{x}\t");
      }
      Console.WriteLine("y: ");
      foreach (double y in MeshY)
      {
        Console.Write($"{y}\t");
      }
      Console.WriteLine("f: ");
      foreach (double f in FunctionData)
      {
        Console.Write($"{f}\t");
      }
    }
  }
}

using System;

namespace MathPrimitivesLibrary.Types.Meshes
{
  public class RegularMesh : AbstractMesh
  {
    public double StepLength { get; set; }
    public double[] MeshX { get; set; }
    public double[] MeshY { get; set; }
    public double[] MeshQuadratureData { get; set; }
    public RegularMesh(double leftEdge, double rightEdge, int numberOfSteps) : base(leftEdge, rightEdge, numberOfSteps)
    {
      StepLength = (rightEdge - leftEdge) / numberOfSteps;
      MeshX = new double[numberOfSteps + 1];
      for (int i = 0; i <= numberOfSteps; i++)
      {
        MeshX[i] = leftEdge + StepLength * i;
      }
      MeshY = new double[numberOfSteps + 1];
      MeshQuadratureData = new double[numberOfSteps + 1];
#if VERBOSE
        GetMeshProperties();
        GetMeshData();
#endif
    }

    public override void ClearMesh()
    {
      StepLength = 0;
      for (int i =0; i < MeshX.Length; i++)
      {
        MeshX[i] = 0;
        MeshY[i] = 0;
        MeshQuadratureData[i] = 0;
      }
      base.ClearMesh();
    }
    public void PrintMeshProperties()
    {
      Console.WriteLine($"Left Edge: {this.leftEdge}");
      Console.WriteLine($"Right Edge: {this.rightEdge}");
      Console.WriteLine($"Number of Steps: {this.numberOfSteps}");
      Console.WriteLine($"Step Length: {this.StepLength}");
    }

    public void PrintMeshData()
    {
      Console.WriteLine("x: ");
      foreach (double x in MeshX)
      {
        Console.Write($"{x}\t");
      }
      Console.WriteLine("\ny: ");
      foreach (double y in MeshY)
      {
        Console.Write($"{y}\t");
      }
      Console.WriteLine("\nf: ");
      foreach (double f in MeshQuadratureData)
      {
        Console.Write($"{f}\t");
      }
    }
  }
}

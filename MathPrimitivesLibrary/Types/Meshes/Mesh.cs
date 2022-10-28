using System;

namespace MathPrimitivesLibrary.Types.Meshes
{
  public class Mesh
  {
    public int NumberOfSteps { get; set; }
    public double LeftEdge { get; set; }
    public double RightEdge { get; set; }
    public double StepLength { get; set; }
    public double[] MeshX { get; set; }
    public double[] MeshY { get; set; }
    public double[] FunctionData { get; set; }
    public Mesh(double leftEdge, double rightEdge, int numberOfSteps)
    {
      this.LeftEdge = leftEdge;
      this.RightEdge = rightEdge;
      this.NumberOfSteps = numberOfSteps;
      StepLength = (this.RightEdge - this.LeftEdge) / this.NumberOfSteps;
      MeshX = new double[this.NumberOfSteps + 1];
      MeshY = new double[this.NumberOfSteps + 1];
      FunctionData = new double[this.NumberOfSteps + 1];
#if VERBOSE
        GetMeshProperties();
#endif
    }

    private void GetMeshProperties()
    {
      Console.WriteLine($"Left Edge: {this.LeftEdge}");
      Console.WriteLine($"Right Edge: {this.RightEdge}");
      Console.WriteLine($"Number of Steps: {this.NumberOfSteps}");
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

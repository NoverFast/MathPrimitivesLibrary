using System;

namespace MathPrimitivesLibrary.Types.Meshes
{
  public class Mesh
  {
    public int NumberOfSteps { get; set; }
    public double LeftEdge { get; set; }
    public double RightEdge { get; set; }
    public double StepLength { get; set; }
    public double[] MeshValue { get; set; }
    public double[] MeshSteps { get; set; }
    public Mesh(double leftEdge, double rightEdge, int numberOfSteps)
    {
      this.LeftEdge = leftEdge;
      this.RightEdge = rightEdge;
      this.NumberOfSteps = numberOfSteps;
      StepLength = (this.RightEdge - this.LeftEdge) / this.NumberOfSteps;
      MeshValue = new double[this.NumberOfSteps + 1];
      MeshSteps = new double[this.NumberOfSteps + 1];
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
      Console.WriteLine("y: ");
      foreach (double v in MeshValue)
      {
        Console.Write($"{v}\t");
      }
      Console.WriteLine("x: ");
      foreach (double s in MeshSteps)
      {
        Console.Write($"{s}\t");
      }
    }
  }
}

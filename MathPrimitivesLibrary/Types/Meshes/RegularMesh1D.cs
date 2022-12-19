using System;
using System.IO;

namespace MathPrimitivesLibrary.Types.Meshes
{
  public class RegularMesh1D : AbstractMesh
  {
    public double StepLength { get; set; }
    public Vector Grid { get; set; }
    public Vector GridPoints { get; set; }

    public RegularMesh1D(double leftEdge, double rightEdge, int numberOfSteps) : base(leftEdge, rightEdge, numberOfSteps)
    {
      StepLength = (rightEdge - leftEdge) / (numberOfSteps - 1);
      Grid = new Vector(numberOfSteps);
      GridPoints = new Vector(numberOfSteps);

      FillGridPoints();
    }


    public void WriteMeshDataToFile(string path)
    {
      StreamWriter sw = new StreamWriter(path);
      for (int i =0; i < this.numberOfSteps; i++)
      {
        sw.WriteLine(GridPoints[i] + " " + Grid[i]);
      }
    }
    public void FillGridPoints()
    {
      double tmp = leftEdge;
      for (int i = 0; i < GridPoints.Size; i++)
      {
        GridPoints[i] = tmp;
        tmp += StepLength;
      }
    }

    public override void ShowMeshProperties(bool showGrid = false, int roundTo = -1)
    {
      Console.WriteLine($"Left Edge: {this.leftEdge}");
      Console.WriteLine($"Right Edge: {this.rightEdge}");
      Console.WriteLine($"Number of Steps: {this.numberOfSteps}");
      Console.WriteLine($"Step Length: {this.StepLength}");
      if (showGrid)
      {
        GridPoints.Show(roundTo);
      }
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

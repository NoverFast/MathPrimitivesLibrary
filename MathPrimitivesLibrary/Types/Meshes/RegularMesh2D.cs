using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Permissions;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MathPrimitivesLibrary.Types.Meshes
{

  public class RegularMesh2D : AbstractMesh
  {
    public struct Point2D
    {
      public double X;
      public double Y;

      public void Show()
      {
        Console.WriteLine($"X: {X}, Y: {Y}");
      }
    }

    #region Properties
    /// <summary>
    /// Количество шагов по оси X
    /// </summary>
    public int NumberOfStepsX { get; set; }
    /// <summary>
    /// Количество шагов по оси Y
    /// </summary>
    public int NumberOfStepsY { get; set; }

    /// <summary>
    /// Длина шага по оси Х
    /// </summary>
    public double StepLengthX { get; set; }
    /// <summary>
    /// Длина шага по оси Y
    /// </summary>
    public double StepLengthY { get; set; }
    
    /// <summary>
    /// Матрица, куда записываются данные сетки.
    /// </summary>
    public Matrix Grid { get; set; }

    /// <summary>
    /// Матрица, которая содержит в себе все числовые значения точек в сетке
    /// </summary>
    public Point2D[,] GridPoints { get; private set; }


    #endregion

    #region Constructors
    public RegularMesh2D() : base(0, 1, 100)
    {
      Grid = new Matrix(NumberOfStepsX, NumberOfStepsY);
      GridPoints = new Point2D[NumberOfStepsX, NumberOfStepsY];
      NumberOfStepsX = base.numberOfSteps;
      NumberOfStepsY = base.numberOfSteps;
      StepLengthX = StepLengthY = (base.rightEdge - base.leftEdge) / (base.numberOfSteps -1);
    }

    public RegularMesh2D(double[] leftBottom, double[] rightBottom, 
      double[] leftTop, double[] rightTop, int numberOfStepsX, int numberOfStepsY) :
      base(leftBottom[0], rightBottom[0], numberOfStepsX)
    {
      Grid = new Matrix(numberOfStepsX, numberOfStepsY);
      GridPoints = new Point2D[NumberOfStepsX, NumberOfStepsY];
      NumberOfStepsX = numberOfStepsX;
      NumberOfStepsY = numberOfStepsY;
      StepLengthX = Math.Abs(rightTop[0] - leftTop[0]) / (numberOfStepsX - 1);
      StepLengthY = Math.Abs(rightTop[1] - leftBottom[1]) / (numberOfStepsY - 1);

      FillGridPoints(leftTop);
    }

    #endregion

    public override void ShowMeshProperties(bool showGrid = false, int roundTo = -1)
    {
      Console.WriteLine($"Number of steps by X: {NumberOfStepsX}\nNumber of steps by Y: {NumberOfStepsY}");
      Console.WriteLine($"Step Length by X: {StepLengthX}\nStep Length by Y: {StepLengthY}");
      if (showGrid)
      {
        foreach (Point2D p in GridPoints)
        {
          p.Show();
        }
      }
    }

    public override void ClearMeshData()
    {
      for (int i =0; i < GridPoints.GetLength(0); i++)
      {
        for (int j =0; j < GridPoints.GetLength(1); j++)
        {
          Grid[i, j] = 0;
        }
      }
    }

    private void FillGridPoints(double[] leftUpperCorner)
    {
      for (int i = 0; i < GridPoints.GetLength(0); i++)
      {
        for (int j = 0; j < GridPoints.GetLength(1); j++)
        {
          GridPoints[i,j]= new Point2D() 
          { 
            X = leftUpperCorner[0] + j * StepLengthX, Y = leftUpperCorner[1] - i * StepLengthY 
          };
        }
      }
    }
  }
}

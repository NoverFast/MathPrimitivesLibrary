﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MathPrimitivesLibrary.Types.Meshes
{
  public class RegularMesh2D : AbstractMesh
  {
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
    public Matrix GridPoints { get; set; }
    #endregion

    #region Constructors
    public RegularMesh2D() : base(0, 1, 100)
    {
      Grid = new Matrix(NumberOfStepsX, NumberOfStepsY);
      GridPoints = new Matrix(NumberOfStepsX, NumberOfStepsY);
      NumberOfStepsX = base.numberOfSteps;
      NumberOfStepsY = base.numberOfSteps;
      StepLengthX = StepLengthY = (base.rightEdge - base.leftEdge) / (base.numberOfSteps -1);

      FillGridPoints();
    }

    public RegularMesh2D(double leftBottom, double rightBottom, 
      double leftTop, double rightTop, int numberOfStepsX, int numberOfStepsY) :
      base(leftBottom, rightBottom, numberOfStepsX)
    {
      Grid = new Matrix(numberOfStepsX, numberOfStepsY);
      GridPoints = new Matrix(numberOfStepsX, numberOfStepsY);
      NumberOfStepsX = numberOfStepsX;
      NumberOfStepsY = numberOfStepsY;
      StepLengthX = (rightBottom - leftBottom) / (numberOfStepsX - 1);
      StepLengthY = (leftTop - leftBottom) / (numberOfStepsY - 1);

      FillGridPoints();
    }
    #endregion

    public override void ShowMeshProperties(bool showGrid = false, int roundTo = -1)
    {
      Console.WriteLine($"Number of steps by X: {NumberOfStepsX}\nNumber of steps by Y: {NumberOfStepsY}");
      Console.WriteLine($"Step Length by X: {StepLengthX}\nStep Length by Y: {StepLengthY}");
      if (showGrid)
      {
        GridPoints.Show(roundTo);
      }
    }

    public override void ClearMeshData()
    {
      for (int i =0; i < GridPoints.Rows; i++)
      {
        for (int j =0; j < GridPoints.Coloumns; j++)
        {
          Grid[i, j] = 0;
        }
      }
    }

    private void FillGridPoints()
    {
      for (int i = 0; i < GridPoints.Rows; i++)
      {
        for (int j = 0; j < GridPoints.Coloumns; j++)
        {
          GridPoints[i, j] = i * StepLengthX + j * StepLengthY;
        }
      }
    }
  }
}

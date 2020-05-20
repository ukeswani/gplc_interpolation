using System;

namespace MatrixOperations
{
    public class FourNonDiagonalNeighboursNoAdjacentNansStrategy : IInterpolationStrategy
    {
        public double Interpolate(uint rowIndex, uint columnIndex, double[,] matrix)
        {
            ParametersGuard(rowIndex, columnIndex, matrix);

            var rowUpperBounds = (uint)matrix.GetUpperBound(0);
            var columnUpperBounds = (uint)matrix.GetUpperBound(1);

            var cumulativeValue = 0.0d;
            var numberOfValidNeighbours = 0;

            for (var i = rowIndex == 0 ? rowIndex : rowIndex - 1; i <= rowIndex + 1; i++)
            {
                for (var j = columnIndex == 0 ? columnIndex : columnIndex - 1; j <= columnIndex + 1; j++)
                {
                    if (NotAValidNeighbour(rowIndex, columnIndex, rowUpperBounds, columnUpperBounds, i, j))
                    {
                        continue;
                    }

                    cumulativeValue += matrix[i, j];
                    numberOfValidNeighbours += 1;
                }
            }



            var interpolatedValue = cumulativeValue/numberOfValidNeighbours;
            

            return interpolatedValue;
        }

        private static bool NotAValidNeighbour(uint rowIndex, uint columnIndex, uint rowUpperBounds, uint columnUpperBounds, uint i, uint j)
        {
            return (i != rowIndex && j != columnIndex)
                   ||
                   (i == rowIndex && j == columnIndex)
                   ||
                   j > columnUpperBounds
                   ||
                   i > rowUpperBounds;
        }

        private static void ParametersGuard(uint rowIndex, uint columnIndex, double[,] matrix)
        {
            if (matrix == null)
            {
                throw new ArgumentNullException(nameof(matrix));
            }

            if (rowIndex > matrix.GetUpperBound(0))
            {
                throw new ArgumentException($"{nameof(rowIndex)} out of bounds. Row value specified: {rowIndex}");
            }

            if (columnIndex > matrix.GetUpperBound(1))
            {
                throw new ArgumentException($"{nameof(columnIndex)} out of bounds. Column value specified: {columnIndex}");
            }
        }
    }
}

using System;

namespace MatrixOperations
{
    public class FourNonDiagonalNeighboursNoAdjacentNansStrategy : IInterpolationStrategy
    {
        public double InterpolateValue(uint rowIndex, uint columnIndex, double[,] matrix)
        {
            ParametersGuard(rowIndex, columnIndex, matrix);

            var rowUpperBounds = (uint)matrix.GetUpperBound(0);
            var columnUpperBounds = (uint)matrix.GetUpperBound(1);

            var cumulativeValueOfValidNeighbours = 0.0d;
            var numberOfValidNeighbours = 0;

            uint startingRowIndex = rowIndex == 0 ? rowIndex : rowIndex - 1;
            uint startingColumnIndex = columnIndex == 0 ? columnIndex : columnIndex - 1;

            for (var i = startingRowIndex; i <= rowIndex + 1; i++)
            {
                for (var j = startingColumnIndex; j <= columnIndex + 1; j++)
                {
                    if (NeighbourDoesNotExist(rowUpperBounds, columnUpperBounds, i, j)
                        ||
                        InvalidNeighbour(rowIndex, columnIndex, i, j))
                    {
                        continue;
                    }

                    cumulativeValueOfValidNeighbours += matrix[i, j];
                    numberOfValidNeighbours += 1;
                }
            }

            var interpolatedValue = Math.Round(cumulativeValueOfValidNeighbours/numberOfValidNeighbours, 6);
            return interpolatedValue;
        }

        private static bool NeighbourDoesNotExist(uint rowUpperBounds, uint columnUpperBounds, uint i, uint j)
        {
            return j > columnUpperBounds
                   ||
                   i > rowUpperBounds;
        }

        private static bool InvalidNeighbour(uint rowIndex, uint columnIndex, uint i, uint j)
        {
            return (i != rowIndex && j != columnIndex)
                   ||
                   (i == rowIndex && j == columnIndex);

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

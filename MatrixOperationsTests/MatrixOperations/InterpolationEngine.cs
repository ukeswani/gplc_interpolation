using System;

namespace MatrixOperations
{
    public class InterpolationEngine
    {
        private readonly IInterpolationStrategy _interpolationStrategy;

        public InterpolationEngine(IInterpolationStrategy interpolationStrategy)
        {
            _interpolationStrategy = interpolationStrategy ?? throw new ArgumentNullException(nameof(interpolationStrategy));
        }

        public double[,] Interpolate(double[,] matrix)
        {
            if (matrix == null)
            {
                throw new ArgumentNullException(nameof(matrix));
            }

            for (uint i = 0; i <= matrix.GetUpperBound(0); i++)
            {
                for (uint j = 0; j <= matrix.GetUpperBound(1); j++)
                {
                    if (double.IsNaN(matrix[i, j]))
                    {
                        matrix[i, j] = _interpolationStrategy.Interpolate(i, j, matrix);
                    }
                }
            }

            return matrix;
        }
    }
}
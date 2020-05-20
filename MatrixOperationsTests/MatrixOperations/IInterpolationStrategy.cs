namespace MatrixOperations
{
    public interface IInterpolationStrategy
    {
        double Interpolate(uint rowIndex, uint columnIndex, double[,] matrix);
    }
}
namespace MatrixOperations
{
    public interface IInterpolationStrategy
    {
        double InterpolateValue(uint rowIndex, uint columnIndex, double[,] matrix);
    }
}
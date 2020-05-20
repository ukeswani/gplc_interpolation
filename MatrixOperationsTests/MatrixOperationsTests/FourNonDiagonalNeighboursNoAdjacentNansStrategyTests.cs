using MatrixOperations;
using NUnit.Framework;
using System;
using System.Collections.Generic;

namespace MatrixOperationsTests
{
    public class FourNonDiagonalNeighboursNoAdjacentNansStrategyTests
    {
        [Test]
        public void GivenNoObject_WhenStrategyConstructed_ThenReturnsAnInstanceOfIInterpolationStrategy()
        {
            var strategy = new FourNonDiagonalNeighboursNoAdjacentNansStrategy();

            Assert.That(strategy, Is.InstanceOf<IInterpolationStrategy>());
        }

        [Test]
        public void GivenAnIInterpolationStrategyInstance_WhenInterpolateCalled_ThenExpectsRowAndColumnIndicesAndTwoDimensionalMatrixOfDoubles()
        {
            IInterpolationStrategy strategy = new FourNonDiagonalNeighboursNoAdjacentNansStrategy();

            Assert.DoesNotThrow(() => strategy.Interpolate((uint)0, (uint)0, new double[1,1]));
        }

        [Test]
        public void GivenAnIInterpolationStrategyInstance_WhenInterpolateCalledWithNullAsMatrix_ThenThrowsAnException()
        {
            IInterpolationStrategy strategy = new FourNonDiagonalNeighboursNoAdjacentNansStrategy();

            Assert.Throws<ArgumentNullException>(() => strategy.Interpolate((uint)0, (uint)0, null));
        }

        [TestCaseSource(nameof(InvalidRowAndColumnIndices))]
        public void GivenAnIInterpolationStrategyInstance_WhenInterpolateCalledWithInvalidIndices_ThenThrowsAnException(
            uint rowIndex, uint columnIndex, double[,] matrix)
        {
            IInterpolationStrategy strategy = new FourNonDiagonalNeighboursNoAdjacentNansStrategy();

            Assert.Throws<ArgumentException>(() => strategy.Interpolate(rowIndex, columnIndex, matrix));
        }

        [TestCaseSource(nameof(InterpolatedMatrixAsExpected))]
        public void GivenAnIInterpolationStrategyInstance_WhenInterpolateCalledWithInvalidIndices_ThenThrowsAnException(
            uint rowIndex, uint columnIndex, double[,] matrix, double expectedInterpolatedValue)
        {
            // Arrange
            IInterpolationStrategy strategy = new FourNonDiagonalNeighboursNoAdjacentNansStrategy();

            // Act
            double interpolatedValue = strategy.Interpolate(rowIndex, columnIndex, matrix);

            // Assert
            Assert.That(Math.Abs(interpolatedValue - expectedInterpolatedValue),
                Is.LessThan(0.000001), 
                () => $"Expected {expectedInterpolatedValue}, Actual {interpolatedValue}");
        }

        private static IEnumerable<object[]> InterpolatedMatrixAsExpected()
        {
            yield return new object[]
            {
                (uint)1,
                (uint)1,
                new double[,] 
                {
                    {5.808361,  86.617615,  60.111501},
                    {96.990985,	double.NaN, 21.233911},
                    {30.424224, 52.475643,  43.194502}
                },
                64.3295385
            };

            yield return new object[]
            {
                (uint)1,
                (uint)1,
                new double[,] 
                {
                    {86.617615,	 60.111501,	 70.807258},
                    {64.3295385, double.NaN, 18.182497},
                    {52.475643,	 43.194502,	 29.122914}
                },
                46.454509
            };

            yield return new object[]
            {
                (uint)1,
                (uint)0,
                new double[,] 
                {
                    {2.058449,   96.990985},
                    {double.NaN, 30.424224},
                    {61.185289,  13.949386}
                },
                31.222654
            };

            yield return new object[]
            {
                (uint)1,
                (uint)1,
                new double[,] 
                {
                    {52.475643,  43.194502, 29.122914},
                    {29.214465,	double.NaN,	45.606998}
                },
                39.338655
            };

            yield return new object[]
            {
                (uint)0,
                (uint)1,
                new double[,] 
                {
                    {59.865848, double.NaN},
                    {60.111501, 70.807258}
                },
                65.336553
            };
        }

        private static IEnumerable<object[]> InvalidRowAndColumnIndices()
        {
            yield return new object[]
            {
                (uint)0, (uint)1, new double[1,1]
            };

            yield return new object[]
            {
                (uint)1, (uint)0, new double[1,1]
            };

            yield return new object[]
            {
                (uint)1, (uint)2, new double[2,2]
            };
            
            yield return new object[]
            {
                (uint)2, (uint)1, new double[2,2]
            };
        }
    }
}
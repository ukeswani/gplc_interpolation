using MatrixOperations;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;

namespace MatrixOperationsTests
{
    [TestFixture]
    class InterpolationEngineTests
    {
        [Test]
        public void GivenEngineBeingConstructed_WhenInstanceOfIInterpolationStrategyProvided_ThenDoesNotThrowAnException()
        {
            Assert.DoesNotThrow(() =>
            {
                var interpolationEngine = new InterpolationEngine(Mock.Of<IInterpolationStrategy>());
            });
        }

        [Test]
        public void GivenEngineBeingConstructed_WhenNoInstanceOfIInterpolationStrategyProvided_ThenThrowsAnException()
        {
            Assert.Throws<ArgumentNullException>(() =>
            {
                var interpolationEngine = new InterpolationEngine(null);
            });
        }

        [Test]
        public void GivenEngine_WhenInterpolateCalledWithATwoDimensionalArrayOfDoubles_ThenDoesNotThrowAnException()
        {
            // Arrange
            var interpolationEngine = new InterpolationEngine(Mock.Of<IInterpolationStrategy>());

            // Act
            Assert.DoesNotThrow(() => interpolationEngine.Interpolate(new double[0, 0]));

        }

        [Test]
        public void GivenEngine_WhenInterpolateCalledWithNoTwoDimensionalArrayOfDoubles_ThenThrowAnException()
        {
            // Arrange
            var interpolationEngine = new InterpolationEngine(Mock.Of<IInterpolationStrategy>());

            // Act
            Assert.Throws<ArgumentNullException>(() => interpolationEngine.Interpolate(null));
        }

        [Test]
        public void GivenEngine_WhenInterpolateCalledWithATwoDimensionalArrayOfDoubles_ThenReturnsATwoDimensionalArrayOfDoubles()
        {
            // Arrange
            var interpolationEngine = new InterpolationEngine(Mock.Of<IInterpolationStrategy>());

            // Act
            var output = interpolationEngine.Interpolate(new double[,]{});

            // Assert
            Assert.That(output, Is.TypeOf<double[,]>());
        }

        [TestCaseSource(nameof(ValidCases))]
        public void GivenEngineWithValidStratgey_WhenInterpolateCalledWithValidInput_ThenReturnsExpectedOutput(
            double[,] input, double[,] expectedInterpolatedMatrix)
        {
            // Arrange
            var interpolationEngine = new InterpolationEngine(new FourNonDiagonalNeighboursNoAdjacentNansStrategy());

            // Act
            var interpolatedMatrix = interpolationEngine.Interpolate(input);

            // Assert
            CustomAssertInterpolatedMatrixAsExpected(expectedInterpolatedMatrix, interpolatedMatrix);
        }

        private static void CustomAssertInterpolatedMatrixAsExpected(double[,] expectedInterpolatedMatrix, double[,] interpolatedMatrix)
        {
            for (uint i = 0; i <= expectedInterpolatedMatrix.GetUpperBound(0); i++)
            {
                for (uint j = 0; j <= expectedInterpolatedMatrix.GetUpperBound(1); j++)
                {
                    var expectedInterpolatedValue = expectedInterpolatedMatrix[i, j];
                    var interpolatedValue = interpolatedMatrix[i, j];


                    Assert.That(
                        double.IsNaN(interpolatedValue) && double.IsNaN(expectedInterpolatedValue)
                        ||
                        (Math.Abs(interpolatedValue - expectedInterpolatedValue) < 0.000001),
                        () => $"Expected {expectedInterpolatedValue}, Actual {interpolatedValue}");
                }
            }
        }

        private static IEnumerable<object[]> ValidCases()
        {
            yield return new object[]
            {
                new double[,] 
                {
                    {5.808361,  86.617615,  60.111501},
                    {96.990985,	double.NaN, 21.233911},
                    {30.424224, 52.475643,  43.194502}
                },
                new double[,] 
                {
                    {5.808361,  86.617615,  60.111501},
                    {96.990985,	64.3295385, 21.233911},
                    {30.424224, 52.475643,  43.194502}
                }
            };

            yield return new object[]
            {
                new double[,]
                {
                    {37.454012,	95.071431, 73.199394, 59.865848, double.NaN},
                    {15.599452, 5.808361,  86.617615, 60.111501, 70.807258},
                    {2.058449,	96.990985,	double.NaN,	21.233911,	18.182497},
                    {double.NaN, 30.424224, 52.475643, 43.194502, 29.122914},
                    {61.185289, 13.949386, 29.214465, double.NaN, 45.606998}
                },
                new double[,]
                {
                    {37.454012,	95.071431, 73.199394, 59.865848, 65.336553},
                    {15.599452, 5.808361,  86.617615, 60.111501, 70.807258},
                    {2.058449,	96.990985, 64.3295385,	21.233911,	18.182497},
                    {31.222654, 30.424224, 52.475643, 43.194502, 29.122914},
                    {61.185289, 13.949386, 29.214465, 39.338655, 45.606998}
                },
            };

            yield return new object[]
            {
                new double[,]
                {
                    {37.454012,	95.071431, 73.199394, 59.865848, 65.336553},
                    {2.058449,	96.990985, 64.3295385,	21.233911,	18.182497},
                    {31.222654, 30.424224, 52.475643, 43.194502, 29.122914},
                    {61.185289, 13.949386, 29.214465, 39.338655, 45.606998}
                },
                new double[,]
                {
                    {37.454012,	95.071431, 73.199394, 59.865848, 65.336553},
                    {2.058449,	96.990985, 64.3295385,	21.233911,	18.182497},
                    {31.222654, 30.424224, 52.475643, 43.194502, 29.122914},
                    {61.185289, 13.949386, 29.214465, 39.338655, 45.606998}
                },
            };

            yield return new object[]
            {
                new double[,]{},
                new double[,]{}
            };

            yield return new object[]
            {
                new double[,] 
                {
                    {double.NaN, double.NaN, 21.233911},
                    {30.424224,  52.475643,  double.NaN}
                },
                new double[,] 
                {
                    {double.NaN, double.NaN, 21.233911},
                    {30.424224,  52.475643,  36.854777}
                }
            };
        }



    }
}

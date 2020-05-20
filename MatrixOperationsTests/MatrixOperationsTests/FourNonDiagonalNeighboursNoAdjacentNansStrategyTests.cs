using MatrixOperations;
using NUnit.Framework;

namespace MatrixOperationsTests
{
    public class FourNonDiagonalNeighboursNoAdjacentNansStrategyTests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void GivenNoObject_WhenStrategyConstructed_ThenReturnsAnInstanceOfIInterpolationStrategy()
        {
            var strategy = new FourNonDiagonalNeighboursNoAdjacentNansStrategy();

            Assert.That(strategy, Is.InstanceOf<IInterpolationStrategy>());
        }
    }
}
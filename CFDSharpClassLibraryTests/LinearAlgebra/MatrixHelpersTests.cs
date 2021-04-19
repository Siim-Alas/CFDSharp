using CFDSharpClassLibrary.LinearAlgebra;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CFDSharpClassLibraryTests.LinearAlgebra
{
    [TestClass]
    public class MatrixHelpersTests
    {
        [TestMethod]
        public void FindFirstRowOfNonZeroElementInColumn_WithAllNonZeroesInColumn_ReturnsZero()
        {
            // Arrange
            double[,] M = new double[,]
            {
                { 1, 2, 3 },
                { 4, 5, 6 },
                { 7, 6, 9 }
            };

            // Act
            int result = MatrixHelpers.FindFirstRowOfNonZeroElementInColumn(1, M);

            // Assert
            Assert.AreEqual(0, result);
        }
        [TestMethod]
        public void FindFirstRowOfNonZeroElementInColumn_WithAllNonZeroesInColumnButStartingAtRowOne_ReturnsOne()
        {
            // Arrange
            double[,] M = new double[,]
            {
                { 1, 2, 3 },
                { 4, 5, 6 },
                { 7, 6, 9 }
            };

            // Act
            int result = MatrixHelpers.FindFirstRowOfNonZeroElementInColumn(1, M, startAtRow: 1);

            // Assert
            Assert.AreEqual(1, result);
        }
        [TestMethod]
        public void FindFirstRowOfNonZeroElementInColumn_WithAllZeroesInColumn_ReturnsMinusOne()
        {
            // Arrange
            double[,] M = new double[,]
            {
                { 1, 0, 3 },
                { 4, 0, 6 },
                { 7, 0, 9 }
            };

            // Act
            int result = MatrixHelpers.FindFirstRowOfNonZeroElementInColumn(1, M);

            // Assert
            Assert.AreEqual(-1, result);
        }
        [TestMethod]
        public void FindFirstRowOfNonZeroElementInColumn_WithAllZeroesInColumnButStartingAtRowOne_ReturnsMinusOne()
        {
            // Arrange
            double[,] M = new double[,]
            {
                { 1, 0, 3 },
                { 4, 0, 6 },
                { 7, 0, 9 }
            };

            // Act
            int result = MatrixHelpers.FindFirstRowOfNonZeroElementInColumn(1, M, startAtRow: 1);

            // Assert
            Assert.AreEqual(-1, result);
        }
        [TestMethod]
        public void FindFirstRowOfNonZeroElementInColumn_WithAllZeroesInColumnExceptAtRowOne_ReturnsOne()
        {
            // Arrange
            double[,] M = new double[,]
            {
                { 1, 0, 3 },
                { 4, 1, 6 },
                { 7, 0, 9 }
            };

            // Act
            int result = MatrixHelpers.FindFirstRowOfNonZeroElementInColumn(1, M);

            // Assert
            Assert.AreEqual(1, result);
        }
        [TestMethod]
        public void FindFirstRowOfNonZeroElementInColumn_WithNonZeroInTheFirstButZeroesInLowerRowsOfTheColumnButStartingAtRowOne_ReturnsMinusOne()
        {
            // Arrange
            double[,] M = new double[,]
            {
                { 1, 2, 3 },
                { 4, 0, 6 },
                { 7, 0, 9 }
            };

            // Act
            int result = MatrixHelpers.FindFirstRowOfNonZeroElementInColumn(1, M, startAtRow: 1);

            // Assert
            Assert.AreEqual(-1, result);
        }
        [TestMethod]
        public void FindFirstRowOfNonZeroElementInColumn_WithNonZeroInTheFirstButZeroesInLowerRowsOfTheColumn_ReturnsZero()
        {
            // Arrange
            double[,] M = new double[,]
            {
                { 1, 2, 3 },
                { 4, 0, 6 },
                { 7, 0, 9 }
            };

            // Act
            int result = MatrixHelpers.FindFirstRowOfNonZeroElementInColumn(1, M);

            // Assert
            Assert.AreEqual(0, result);
        }
    }
}

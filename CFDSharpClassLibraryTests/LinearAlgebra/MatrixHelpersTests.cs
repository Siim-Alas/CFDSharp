using CFDSharpClassLibrary.LinearAlgebra;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

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
        [TestMethod]
        public void SolveWithGaussianElimination_WithInvalidDimensionalMatrixAsInput_ThrowsArgumentException()
        {
            // Arrange
            double[,] M = new double[1, 3];

            // Act and assert
            Assert.ThrowsException<ArgumentException>(() => MatrixHelpers.SolveWithGaussianElimination(ref M));
        }
        [TestMethod]
        public void SolveWithGaussianElimination_WithValidInput_ReturnsCorrecty()
        {
            // Arrange
            double[,] M = new double[,] // Example taken from https://en.wikipedia.org/wiki/Gaussian_elimination
            {
                {  2,  1, -1,   8 },
                { -3, -1,  2, -11 },
                { -2,  1,  2,  -3 }
            };
            double[] correctSolution = new double[]
            {
                 2,
                 3,
                -1
            };

            // Act
            double[] actualSolution = MatrixHelpers.SolveWithGaussianElimination(ref M);
            bool solutionIsCorrect =
                (actualSolution[0] == correctSolution[0]) &&
                (actualSolution[1] == correctSolution[1]) &&
                (actualSolution[2] == correctSolution[2]);

            //Assert
            Assert.IsTrue(solutionIsCorrect);
        }
        [TestMethod]
        public void SwapRows_WithValidInput_SwapsRowsCorrectly()
        {
            // Arrange
            double[,] M = new double[,]
            {
                { 1, 2, 3 },
                { 4, 5, 6 },
                { 7, 8, 9 }
            };

            // Act
            MatrixHelpers.SwapRows(ref M, 0, 1);
            bool resultIsCorrect =
                (M[0, 0] == 4) && (M[0, 1] == 5) && (M[0, 2] == 6) &&
                (M[1, 0] == 1) && (M[1, 1] == 2) && (M[1, 2] == 3) &&
                (M[2, 0] == 7) && (M[2, 1] == 8) && (M[2, 2] == 9);

            // Assert
            Assert.IsTrue(resultIsCorrect);
        }
        [TestMethod]
        public void TransformInPlaceToRowEchelonForm_WithValidInput_TransformsCorrectly()
        {
            // Arrange
            const int rows = 3;
            const int columns = 4;

            double[,] M = new double[rows, columns] // Example taken form https://en.wikipedia.org/wiki/Gaussian_elimination
            {
                { 1,  3,  1,  9 },
                { 1,  1, -1,  1 },
                { 3, 11,  5, 35 }
            };
            double[,] correctResult = new double[rows, columns]
            {
                { 1,  3,  1,  9 },
                { 0, -2, -2, -8 },
                { 0,  0,  0,  0 }
            };

            // Act
            MatrixHelpers.TransformInPlaceToRowEchelonForm(ref M);
            bool transformedCorrectly = true;
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < columns; j++)
                {
                    if (M[i, j] != correctResult[i, j])
                    {
                        transformedCorrectly = false;
                    }
                }
            }

            // Assert
            Assert.IsTrue(transformedCorrectly);
        }
    }
}

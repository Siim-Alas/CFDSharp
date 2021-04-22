using System;
using System.Collections.Generic;

namespace CFDSharpClassLibrary.LinearAlgebra
{
    /// <summary>
    /// A class providing matrix functions for 2D jagged arrays.
    /// </summary>
    public static class MatrixHelpers
    {
        /// <summary>
        /// Finds the first (with the lowest index) row of a matrix that has a
        /// non-zero element in the specified column.
        /// </summary>
        /// <param name="column">
        /// The column in which to look for non-zero elements.
        /// </param>
        /// <param name="M">The matrix to search.</param>
        /// <param name="startAtRow">
        /// The row at which the search is initiated.
        /// </param>
        /// <returns>
        /// The index of the first row in the matrix whith a non-zero element
        /// in the specified column.
        /// </returns>
        public static int FindFirstRowOfNonZeroElementInColumn(
            int column, double[,] M, int startAtRow = 0)
        {
            int rows = M.GetLength(0);
            for (int i = startAtRow; i < rows; i++)
            {
                if (M[i, column] != 0)
                {
                    return i;
                }
            }
            return -1;
        }
        /// <summary>
        /// Solves the matrix equation Mv = b for the vector v with Gaussian
        /// elimination (https://en.wikipedia.org/wiki/Gaussian_elimination).
        /// The equation is given in the form of a matrix with n rows and n + 1
        /// columns, where the first n columns represent the coefficients of 
        /// the original matrix and the last column represents the vector b. 
        /// Note that this will mutate the input matrix.
        /// </summary>
        /// <param name="M">
        /// The n by n + 1 matrix representing the system of linear equations.
        /// </param>
        /// <returns>
        /// An array representing the solution vector to the matrix equation.
        /// </returns>
        public static double[] SolveWithGaussianElimination(ref double[,] M)
        {
            int rows = M.GetLength(0);
            int columns = M.GetLength(1);

            if (columns != rows + 1)
            {
                throw new ArgumentException(
                    "The matrix did not have n rows and n + 1 columns.",
                    nameof(M));
            }

            TransformInPlaceToRowEchelonForm(
                ref M, out Stack<(int, int)> swappedRows);

            double[] solution = new double[rows];
            for (int i = rows - 1; i >= 0; i--)
            {
                solution[i] = M[i, columns - 1];
                for (int j = i + 1; j < rows; j++)
                {
                    solution[i] -= solution[j] * M[i, j];
                }
                solution[i] /= M[i, i];
            }

            while (swappedRows.TryPop(out (int rowA, int rowB) swap))
            {
                double temp = solution[swap.rowA];
                solution[swap.rowA] = solution[swap.rowB];
                solution[swap.rowB] = temp;
            }

            return solution;
        }
        /// <summary>
        /// Swaps the specified rows in the matrix. Note that the matrix will
        /// get mutated.
        /// </summary>
        /// <param name="M">The matrix whose rows to swap.</param>
        /// <param name="row1">The index of the first row to swap.</param>
        /// <param name="row2">The index of the second row to swap.</param>
        public static void SwapRows(ref double[,] M, int row1, int row2)
        {
            int columns = M.GetLength(1);
            for (int i = 0; i < columns; i++)
            {
                double temp = M[row1, i];
                M[row1, i] = M[row2, i];
                M[row2, i] = temp;
            }
        }
        /// <summary>
        /// Transforms a matrix to unnormalized row echelon form using Gaussian
        /// elimination (https://en.wikipedia.org/wiki/Gaussian_elimination). 
        /// Note that this will mutate the matrix.
        /// </summary>
        /// <param name="M">The matrix to be transformed.</param>
        /// <param name="swappedRows"></param>
        public static void TransformInPlaceToRowEchelonForm(
            ref double[,] M, out Stack<(int, int)> swappedRows)
        {
            int rows = M.GetLength(0);
            int columns = M.GetLength(1);

            swappedRows = new Stack<(int, int)>();

            int i = 0;
            int j = 0;
            while ((i < rows) && (j < columns))
            {
                int firstRowWithNonZeroElement = 
                    FindFirstRowOfNonZeroElementInColumn(j, M, startAtRow: i);

                if (firstRowWithNonZeroElement == -1)
                {
                    j++;
                    continue;
                }
                else if (firstRowWithNonZeroElement != i)
                {
                    SwapRows(ref M, firstRowWithNonZeroElement, i);
                    swappedRows.Push((firstRowWithNonZeroElement, i));
                }
                
                for (int m = i + 1; m < rows; m++)
                {
                    double factor = M[m, j] / M[i, j];

                    M[m, j] = 0;
                    for (int n = j + 1; n < columns; n++)
                    {
                        M[m, n] -= factor * M[i, n];
                    }
                }

                i++;
                j++;
            }
        }
    }
}

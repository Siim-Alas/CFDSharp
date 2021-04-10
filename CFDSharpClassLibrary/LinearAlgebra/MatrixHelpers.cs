using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CFDSharpClassLibrary.LinearAlgebra
{
    /// <summary>
    /// A class providing matrix functions for 2D jagged arrays.
    /// </summary>
    public static class MatrixHelpers
    {
        /// <summary>
        /// Finds the first (with the lowest index) row of a matrix that has a non-zero
        /// element in the specified column.
        /// </summary>
        /// <param name="column">The column in which to look for non-zero elements.</param>
        /// <param name="M">The matrix to search.</param>
        /// <param name="startAtRow">The row at which the search is initiated.</param>
        /// <returns>
        /// The index of the first row in the matrix whith a non-zero element in the specified column.
        /// </returns>
        public static int FindFirstRowOfNonZeroElementInColumn(int column, double[,] M, int startAtRow = 0)
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
        /// Swaps the specified rows in the matrix. Note that the matrix will get mutated.
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
        /// Transforms a matrix to unnormalized row echeon form using Gaussian elimination
        /// (https://en.wikipedia.org/wiki/Gaussian_elimination). Note that this will mutate
        /// the matrix.
        /// </summary>
        /// <param name="M">The matrix to be transformed.</param>
        public static void TransformInPlaceToRowEchelonForm(ref double[,] M)
        {
            int rows = M.GetLength(0);
            int columns = M.GetLength(1);

            int i = 0;
            int j = 0;
            while ((i < rows) && (j < columns))
            {
                // Starting at the i-th row, look for the first row whose j-th column 
                // contains a non-zero element
                int firstRowWithNonZeroElement = 
                    FindFirstRowOfNonZeroElementInColumn(j, M, startAtRow: i);

                if (firstRowWithNonZeroElement == -1)
                {
                    // Starting from the i-th one, there is no row containing a non-zero
                    // element in the j-th column, so we move on to the next column
                    j++;
                    continue;
                }
                else if (firstRowWithNonZeroElement != i)
                {
                    // A row beneath the i-th one has a non-zero element in the j-th column,
                    // but the i-th one doesn't. To keep the matrix triangular, it is swapped 
                    // with the i-th row.
                    SwapRows(ref M, firstRowWithNonZeroElement, i);
                }
                
                // For all rows below i
                for (int m = i + 1; m < rows; m++)
                {
                    // Subtract the i-th row multiplied by the quotient of the leftmost
                    // non-zero elements of the bottom and i-th row from the bottom row

                    M[m, j] = 0; // Guarantee that the leftmost element of the row goes to 0

                    double factor = M[m, j] / M[i, j];
                    for (int n = j + 1; n < columns; n++)
                    {
                        M[m, n] -= factor * M[i, n];
                    }
                }

                i++;
                j++;
            }
        }
        /// <summary>
        /// Solves the matrix equation Mv = b for the vector v with Gaussian elimination 
        /// (https://en.wikipedia.org/wiki/Gaussian_elimination). The equation is given in the
        /// form of a matrix with n rows and n + 1 columns, where the first n columns represent
        /// the coefficients of the original matrix and the last column represents the vector b.
        /// Note that this will mutate the input matrix.
        /// </summary>
        /// <param name="M">The n by n + 1 matrix representing the system of linear equations.</param>
        /// <returns>An array representing the solution vector to the matrix equation.</returns>
        public static double[] SolveWithGaussianElimination(ref double[,] M)
        {
            int rows = M.GetLength(0);
            int columns = M.GetLength(1);

            if (columns != rows + 1)
            {
                throw new ArgumentException(
                    "The provided matrix did not have n rows and n + 1 columns.", nameof(M));
            }

            TransformInPlaceToRowEchelonForm(ref M);

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

            return solution;
        }
    }
}

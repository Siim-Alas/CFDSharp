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
    }
}

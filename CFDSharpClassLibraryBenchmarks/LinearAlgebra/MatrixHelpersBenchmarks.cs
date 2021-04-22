using BenchmarkDotNet.Attributes;
using CFDSharpClassLibrary.LinearAlgebra;
using System;
using System.Collections.Generic;

namespace CFDSharpClassLibraryBenchmarks.LinearAlgebra
{
    public class MatrixHelpersBenchmarks
    {
        private double[,] _M;

        [Params(500, 1000)]
        public int Rows { get; set; }
        public int Columns { get { return Rows + 1; } }

        [GlobalSetup]
        public void GlobalSetup()
        {
            _M = new double[Rows, Columns];
        }
        [IterationSetup]
        public void IterationSetup()
        {
            Random random = new Random();

            for (int i = 0; i < Rows; i++)
            {
                for (int j = 0; j < Columns; j++)
                {
                    _M[i, j] = (random.NextDouble() - 0.5) * double.MaxValue;
                }
            }
        }

        [Benchmark]
        public void FindFirstRowOfNonZeroElementInColumn()
        {
            int _ = MatrixHelpers.FindFirstRowOfNonZeroElementInColumn(
                Columns / 2, _M);
        }
        [Benchmark]
        public void SolveWithGaussianElimination()
        {
            double[] _ = MatrixHelpers.SolveWithGaussianElimination(ref _M);
        }
        [Benchmark]
        public void SwapRows()
        {
            MatrixHelpers.SwapRows(ref _M, Rows / 10, Rows / 2);
        }
        [Benchmark]
        public void TransformInPlaceToRowEchelonForm()
        {
            MatrixHelpers.TransformInPlaceToRowEchelonForm(ref _M, out Stack<(int, int)> _);
        }
    }
}

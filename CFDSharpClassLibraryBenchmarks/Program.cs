using BenchmarkDotNet.Running;
using CFDSharpClassLibraryBenchmarks.LinearAlgebra;

namespace CFDSharpClassLibraryBenchmarks
{
    class Program
    {
        static void Main(string[] args)
        {
            // BenchmarkRunner.Run<BernoullisPrincipleBenchmarks>();

            BenchmarkRunner.Run<MatrixHelpersBenchmarks>();
        }
    }
}

using BenchmarkDotNet.Running;
using CFDSharpClassLibraryBenchmarks.LinearAlgebra;
using CFDSharpClassLibraryBenchmarks.PotentialFlow;

namespace CFDSharpClassLibraryBenchmarks
{
    class Program
    {
        static void Main(string[] args)
        {
            // BenchmarkRunner.Run<BernoullisPrincipleBenchmarks>();
            // BenchmarkRunner.Run<MatrixHelpersBenchmarks>();
            // BenchmarkRunner.Run<VelocityBenchmarks>();
            BenchmarkRunner.Run<VelocityPotentialBenchmarks>();
        }
    }
}

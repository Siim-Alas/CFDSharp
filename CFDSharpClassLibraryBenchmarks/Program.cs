using BenchmarkDotNet.Reports;
using BenchmarkDotNet.Running;

namespace CFDSharpClassLibraryBenchmarks
{
    class Program
    {
        static void Main(string[] args)
        {
            Summary summary = BenchmarkRunner.Run<BernoullisPrincipleBenchmarks>();
        }
    }
}

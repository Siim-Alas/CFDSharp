using BenchmarkDotNet.Attributes;
using CFDSharpClassLibrary;

namespace CFDSharpClassLibraryBenchmarks
{
    public class BernoullisPrincipleBenchmarks
    {
        [Benchmark]
        [Arguments(1, 2, 3, 4, 5, 6, 7)]
        public void GetStaticPressureBenchmark(
            double p0,
            double rho,
            double rho0,
            double vSquared,
            double v0Squared,
            double z,
            double z0)
        {
            BernoullisPrinciple.GetStaticPressure(p0, rho, rho0, vSquared, v0Squared, z, z0);
        }
    }
}

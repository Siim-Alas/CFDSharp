using BenchmarkDotNet.Attributes;
using CFDSharpClassLibrary;
using System;

namespace CFDSharpClassLibraryBenchmarks
{
    public class BernoullisPrincipleBenchmarks
    {
        private double _p0;
        private double _rho;
        private double _rho0;
        private double _vSquared;
        private double _v0Squared;
        private double _z;
        private double _z0;

        [Params(10000, double.MaxValue)]
        public double MaxVal { get; set; }

        [GlobalSetup]
        public void GlobalSetup()
        {
            Random random = new Random();
            _p0 = random.NextDouble() * MaxVal;
            _rho = random.NextDouble() * MaxVal;
            _rho0 = random.NextDouble() * MaxVal;
            _vSquared = random.NextDouble() * MaxVal;
            _v0Squared = random.NextDouble() * MaxVal;
            _z = random.NextDouble() * MaxVal;
            _z0 = random.NextDouble() * MaxVal;
        }
        [Benchmark]
        public void GetStaticPressure()
        {
            BernoullisPrinciple.GetStaticPressure(
                _p0, _rho, _rho0, _vSquared, _v0Squared, _z, _z0);
        }
    }
}

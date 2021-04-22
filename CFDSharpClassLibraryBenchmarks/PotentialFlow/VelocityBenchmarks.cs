using BenchmarkDotNet.Attributes;
using CFDSharpClassLibrary.PotentialFlow;
using CFDSharpClassLibrary.PotentialFlow.SourcePanelMethod;
using System;
using System.Numerics;

namespace CFDSharpClassLibraryBenchmarks.PotentialFlow
{
    public class VelocityBenchmarks
    {
        private Vector3 _r;
        private UnitStrengthRectangularSourcePanel _sourcePanel;

        [Params(10, 1000, float.MaxValue)]
        public float MaxVal { get; set; }

        [GlobalSetup]
        public void GlobalSetup()
        {
            Random random = new Random();

            float yaw = (float)random.NextDouble() * 2 * MathF.PI;
            float pitch = (float)random.NextDouble() * 2 * MathF.PI;
            float roll = (float)random.NextDouble() * 2 * MathF.PI;

            Matrix4x4 rotationMatrix = 
                Matrix4x4.CreateFromYawPitchRoll(yaw, pitch, roll);

            _r = new Vector3(
                (float)(random.NextDouble() - 0.5) * float.MaxValue,
                (float)(random.NextDouble() - 0.5) * float.MaxValue,
                (float)(random.NextDouble() - 0.5) * float.MaxValue);
            _sourcePanel = new UnitStrengthRectangularSourcePanel
            {
                AOver2 = (float)random.NextDouble() * float.MaxValue,
                BOver2 = (float)random.NextDouble() * float.MaxValue,
                Position = new Vector3(
                    (float)(random.NextDouble() - 0.5) * float.MaxValue,
                    (float)(random.NextDouble() - 0.5) * float.MaxValue,
                    (float)(random.NextDouble() - 0.5) * float.MaxValue),
                U = Vector3.Transform(Vector3.UnitX, rotationMatrix),
                V = Vector3.Transform(Vector3.UnitY, rotationMatrix)
            };
        }

        [Benchmark]
        public void ComputeSteadyState()
        {
            Vector3 _ = Velocity.ComputeSteadyState(_r, _sourcePanel);
        }
    }
}

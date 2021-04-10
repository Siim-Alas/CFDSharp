using System.Numerics;

namespace CFDSharpClassLibrary.PotentialFlow.SourcePanelMethod
{
    public class SourcePanel : IPointSource
    {
        public Vector3 Normal { get; }
        public Vector3 Position { get; }
        public double Size { get; }
        public double Strength { get; }
    }
}

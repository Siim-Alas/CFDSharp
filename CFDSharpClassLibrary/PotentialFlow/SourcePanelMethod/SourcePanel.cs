using System.Numerics;

namespace CFDSharpClassLibrary.PotentialFlow.SourcePanelMethod
{
    public class SourcePanel
    {
        public float AOver2 { get; init; }
        public float BOver2 { get; init; }
        public Vector3 Position { get; init; }
        public float Strength { get; set; }
        public Vector3 U { get; init; }
        public Vector3 V { get; init; }
    }
}

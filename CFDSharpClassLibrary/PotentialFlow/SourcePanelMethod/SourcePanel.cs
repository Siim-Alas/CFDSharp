using System.Numerics;

namespace CFDSharpClassLibrary.PotentialFlow.SourcePanelMethod
{
    public class SourcePanel
    {
        public float AOver2 { get; }
        public float BOver2 { get; }
        public Vector3 Position { get; }
        public float Strength { get; set; }
        public Vector3 U { get; }
        public Vector3 V { get; }
    }
}

using System.Numerics;

namespace CFDSharpClassLibrary
{
    public interface IPointSource
    {
        Vector3 Position { get; }
        double Strength { get; }
    }
}

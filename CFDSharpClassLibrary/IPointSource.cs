using System.Numerics;

namespace CFDSharpClassLibrary
{
    /// <summary>
    /// Represents a point source or sink in a 3D scalar velocity potential field.
    /// </summary>
    public interface IPointSource
    {
        /// <summary>
        /// The position vector of the point source in 3D cartesian coordinates.
        /// </summary>
        Vector3 Position { get; }
        /// <summary>
        /// The strength (volume flux) of the point source (> 0) or sink (< 0).
        /// </summary>
        double Strength { get; }
    }
}

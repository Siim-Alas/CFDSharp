using System.Numerics;

namespace CFDSharpClassLibrary.PotentialFlow.SourcePanelMethod
{
    /// <summary>
    /// Represents a unit-strength rectangular source panel.
    /// </summary>
    public class UnitStrengthRectangularSourcePanel
    {
        /// <summary>
        /// Half of the length of the rectangular source panel along the u hat
        /// surface tangent vector.
        /// </summary>
        public float AOver2 { get; init; }
        /// <summary>
        /// Half of the length of the rectangular source panel along the v hat
        /// surface tangent vector.
        /// </summary>
        public float BOver2 { get; init; }
        /// <summary>
        /// The position vector of the centre of the rectangle.
        /// </summary>
        public Vector3 Position { get; init; }
        /// <summary>
        /// The u hat surface tangent vector of length 1, parallel to one side
        /// of the rectangle. Assumed to be orthogonal to v.
        /// </summary>
        public Vector3 U { get; init; }
        /// <summary>
        /// The v hat surface tangent vector of length 1, parallel to the other
        /// side of the rectangle. Assumed to be orhogonal to u.
        /// </summary>
        public Vector3 V { get; init; }
    }
}

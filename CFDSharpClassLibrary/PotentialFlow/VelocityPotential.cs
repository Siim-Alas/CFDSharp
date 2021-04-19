using CFDSharpClassLibrary.PotentialFlow.SourcePanelMethod;
using System;
using System.Numerics;

namespace CFDSharpClassLibrary.PotentialFlow
{
    /// <summary>
    /// A class containing functions for computing quantities related to the 
    /// velocity potential.
    /// </summary>
    public static class VelocityPotential
    {
        /// <summary>
        /// Computes the steady state velocity potential at the point r due to 
        /// the rectangular source panel provided.
        /// </summary>
        /// <param name="r">
        /// The position vector of the point at which the velocity potential is
        /// computed.
        /// </param>
        /// <param name="sourcePanel">
        /// The rectangular source panel causing the velocity potential.
        /// </param>
        /// <returns>The scalar velocity potential at the point r.</returns>
        public static float ComputeSteadyState(
            Vector3 r, SourcePanel sourcePanel)
        {
            Vector3 dr = r - sourcePanel.Position;

            float d11 = (dr - sourcePanel.AOver2 * sourcePanel.U -
                sourcePanel.BOver2 * sourcePanel.V).Length();
            float d12 = (dr - sourcePanel.AOver2 * sourcePanel.U +
                sourcePanel.BOver2 * sourcePanel.V).Length();
            float d21 = (dr - sourcePanel.AOver2 * sourcePanel.U +
                sourcePanel.BOver2 * sourcePanel.V).Length();
            float d22 = (dr + sourcePanel.AOver2 * sourcePanel.U +
                sourcePanel.BOver2 * sourcePanel.V).Length();

            float uDotDr = Vector3.Dot(sourcePanel.U, dr);
            float vDotDr = Vector3.Dot(sourcePanel.V, dr);

            float sqrtK = MathF.Sqrt(dr.LengthSquared() - uDotDr * uDotDr -
                vDotDr * vDotDr);

            float ln111 = MathF.Log(-sourcePanel.BOver2 - vDotDr + d11);
            float ln112 = MathF.Log(sourcePanel.BOver2 - vDotDr + d12);
            float ln121 = MathF.Log(-sourcePanel.BOver2 - vDotDr + d21);
            float ln122 = MathF.Log(sourcePanel.BOver2 - vDotDr + d22);

            float scaledLn1Sum = 
                (-sourcePanel.AOver2 - uDotDr) * ln111 +
                (-sourcePanel.AOver2 - uDotDr) * ln112 +
                (sourcePanel.AOver2 - uDotDr) * ln121 +
                (sourcePanel.AOver2 - uDotDr) * ln122;

            float ln211 = MathF.Log(-sourcePanel.AOver2 - uDotDr + d11);
            float ln212 = MathF.Log(-sourcePanel.AOver2 - uDotDr + d12);
            float ln221 = MathF.Log(sourcePanel.AOver2 - uDotDr + d21);
            float ln222 = MathF.Log(sourcePanel.AOver2 - uDotDr + d22);

            float scaledLn2Sum =
                (-sourcePanel.BOver2 - vDotDr) * ln211 +
                (sourcePanel.BOver2 - vDotDr) * ln212 +
                (-sourcePanel.BOver2 - vDotDr) * ln221 +
                (sourcePanel.BOver2 - vDotDr) * ln222;

            float atan11 = MathF.Atan2((-sourcePanel.AOver2 - uDotDr) *
                (-sourcePanel.BOver2 - vDotDr), sqrtK * d11);
            float atan12 = MathF.Atan2((-sourcePanel.AOver2 - uDotDr) *
                (sourcePanel.BOver2 - vDotDr), sqrtK * d12);
            float atan21 = MathF.Atan2((sourcePanel.AOver2 - uDotDr) *
                (-sourcePanel.BOver2 - vDotDr), sqrtK * d21);
            float atan22 = MathF.Atan2((sourcePanel.AOver2 - uDotDr) *
                (sourcePanel.BOver2 - vDotDr), sqrtK * d22);

            float scaledAtanSum = sqrtK * (atan22 - atan21 - atan12 + atan11);

            float velocityPotential = -sourcePanel.Strength / (4 * MathF.PI) *
                (scaledLn1Sum + scaledLn2Sum - scaledAtanSum);
            return velocityPotential;
        }
    }
}

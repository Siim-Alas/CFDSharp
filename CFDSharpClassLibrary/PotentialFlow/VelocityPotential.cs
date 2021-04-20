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
            float velocityPotential = ComputeSteadyState(
                sourcePanel.AOver2,
                sourcePanel.BOver2,
                r - sourcePanel.Position,
                sourcePanel.Strength,
                sourcePanel.U,
                sourcePanel.V);
            return velocityPotential;
        }
        /// <summary>
        /// Computes the steady state velocity potential at the point r due to
        /// a rectangular source panel described by the arguments.
        /// </summary>
        /// <param name="aOver2">
        /// Half of the length of the rectangular source panel along the u hat
        /// surface normal vector.
        /// </param>
        /// <param name="bOver2">
        /// Half of the length of the rectangular source panel along the v hat
        /// surface normal vector.
        /// </param>
        /// <param name="dr">
        /// r - r0, a vector pointing from the centre of the rectangluar source
        /// panel to the point r.
        /// </param>
        /// <param name="strength">The strength of the source panel.</param>
        /// <param name="u">
        /// The u hat surface normal vector, parallel to one side of the 
        /// rectangle. Assumed to be orthogonal to v.
        /// </param>
        /// <param name="v">
        /// The v hat surface normal vector, parallel to the other side of the
        /// rectangle. Assumed to be orhogonal to u.
        /// </param>
        /// <returns>
        /// The steady state scalar velocity potential at the point r.
        /// </returns>
        public static float ComputeSteadyState(
            float aOver2,
            float bOver2,
            Vector3 dr,
            float strength,
            Vector3 u,
            Vector3 v)
        {
            float d11 = (dr - aOver2 * u - bOver2 * v).Length();
            float d12 = (dr - aOver2 * u + bOver2 * v).Length();
            float d21 = (dr + aOver2 * u - bOver2 * v).Length();
            float d22 = (dr + aOver2 * u + bOver2 * v).Length();

            float uDotDr = Vector3.Dot(u, dr);
            float vDotDr = Vector3.Dot(v, dr);

            float sqrtK = MathF.Sqrt(dr.LengthSquared() - uDotDr * uDotDr -
                vDotDr * vDotDr);

            float lnArg111 = -bOver2 - vDotDr + d11;
            float lnArg112 = bOver2 - vDotDr + d12;
            float lnArg121 = -bOver2 - vDotDr + d21;
            float lnArg122 = bOver2 - vDotDr + d22;

            float lnArg211 = -aOver2 - uDotDr + d11;
            float lnArg212 = -aOver2 - uDotDr + d12;
            float lnArg221 = aOver2 - uDotDr + d21;
            float lnArg222 = aOver2 - uDotDr + d22;

            float scaledLn1Sum =
                aOver2 * MathF.Log(lnArg122*lnArg112 / (lnArg111*lnArg121)) +
                uDotDr * MathF.Log(lnArg112*lnArg121 / (lnArg111*lnArg122));
            float scaledLn2Sum =
                bOver2 * MathF.Log(lnArg222 * lnArg221 / (lnArg211 * lnArg212)) +
                uDotDr * MathF.Log(lnArg212 * lnArg221 / (lnArg211 * lnArg222));

            float atan11 = MathF.Atan2((-aOver2 - uDotDr) * (-bOver2 - vDotDr),
                sqrtK * d11);
            float atan12 = MathF.Atan2((-aOver2 - uDotDr) * (bOver2 - vDotDr),
                sqrtK * d12);
            float atan21 = MathF.Atan2((aOver2 - uDotDr) * (-bOver2 - vDotDr),
                sqrtK * d21);
            float atan22 = MathF.Atan2((aOver2 - uDotDr) * (bOver2 - vDotDr),
                sqrtK * d22);

            float atanSum = atan22 - atan21 - atan12 + atan11;

            float velocityPotential = -strength / (4 * MathF.PI) *
                (scaledLn1Sum + scaledLn2Sum - sqrtK * atanSum);
            return velocityPotential;
        }
    }
}

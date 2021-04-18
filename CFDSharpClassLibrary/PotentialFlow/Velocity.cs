using CFDSharpClassLibrary.PotentialFlow.SourcePanelMethod;
using System;
using System.Numerics;

namespace CFDSharpClassLibrary.PotentialFlow
{
    /// <summary>
    /// A class containing functions for computing quantities related to 
    /// velocity.
    /// </summary>
    public static class Velocity
    {
        /// <summary>
        /// Computes the steady state velocity at the point r due to the
        /// rectangular source panel provided.
        /// </summary>
        /// <param name="r">
        /// The position vector of the point at which the velocity is computed.
        /// </param>
        /// <param name="sourcePanel">
        /// The rectangular source panel causing the velocity.
        /// </param>
        /// <returns>The flow velocity vector at the point r.</returns>
        public static Vector3 ComputeSteadyState(
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

            Vector3 c = dr - uDotDr * sourcePanel.U - vDotDr * sourcePanel.V;
            float sqrtK = MathF.Sqrt(dr.LengthSquared() - uDotDr * uDotDr - 
                vDotDr * vDotDr);

            float lnArg111 = -sourcePanel.BOver2 - vDotDr + d11;
            float lnArg112 = sourcePanel.BOver2 - vDotDr + d12;
            float lnArg121 = -sourcePanel.BOver2 - vDotDr + d21;
            float lnArg122 = sourcePanel.BOver2 - vDotDr + d22;

            float lnArg211 = -sourcePanel.AOver2 - uDotDr + d11;
            float lnArg212 = -sourcePanel.AOver2 - uDotDr + d12;
            float lnArg221 = sourcePanel.AOver2 - uDotDr + d21;
            float lnArg222 = sourcePanel.AOver2 - uDotDr + d22;

            float ln1 = 
                MathF.Log(lnArg111 * lnArg122 / (lnArg112 * lnArg121));
            float ln2 =
                MathF.Log(lnArg211 * lnArg222 / (lnArg212 * lnArg221));

            float atan11 = MathF.Atan2((-sourcePanel.AOver2 - uDotDr) * 
                (-sourcePanel.BOver2 - vDotDr), sqrtK * d11);
            float atan12 = MathF.Atan2((-sourcePanel.AOver2 - uDotDr) *
                (sourcePanel.BOver2 - vDotDr), sqrtK * d12);
            float atan21 = MathF.Atan2((sourcePanel.AOver2 - uDotDr) *
                (-sourcePanel.BOver2 - vDotDr), sqrtK * d21);
            float atan22 = MathF.Atan2((sourcePanel.AOver2 - uDotDr) *
                (sourcePanel.BOver2 - vDotDr), sqrtK * d22);

            float atanSum = atan22 - atan21 - atan12 + atan11;

            Vector3 velocity = sourcePanel.Strength / (4 * MathF.PI) *
                (ln1 * sourcePanel.U + ln2 * sourcePanel.V + 
                (1 / sqrtK) * c * atanSum);
            return velocity;
        }
    }
}

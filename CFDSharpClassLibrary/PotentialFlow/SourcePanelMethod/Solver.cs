using CFDSharpClassLibrary.LinearAlgebra;
using System.Numerics;

namespace CFDSharpClassLibrary.PotentialFlow.SourcePanelMethod
{
    /// <summary>
    /// A class containing functions for solving source panel method problems.
    /// </summary>
    public static class Solver
    {
        /// <summary>
        /// Creates an n by n + 1 (where n is the length of the provided array
        /// of source panels) matrix representing the linear equations to be
        /// solved for the strengths of the source panels.
        /// </summary>
        /// <param name="sourcePanels">
        /// The array of source panels, all of which are assumed to have
        /// strength 1.
        /// </param>
        /// <param name="vPanels">
        /// A vector representing the velocity vector of the panels (the
        /// negative far-field flow velocity).
        /// </param>
        /// <returns>The equation matrix.</returns>
        public static double[,] CreateEquationMatrix(
            SourcePanel[] sourcePanels, Vector3 vPanels)
        {
            int rows = sourcePanels.Length;
            int columns = rows + 1;
            double[,] equationMatrix = new double[rows, columns];

            for (int i = 0; i < rows; i++)
            {
                Vector3 surfaceNormal =
                    Vector3.Cross(sourcePanels[i].U, sourcePanels[i].V);

                for (int j = 0; j < columns - 1; j++)
                {
                    Vector3 unscaledVelocity = Velocity.ComputeSteadyState(
                        sourcePanels[i].Position, sourcePanels[j]);

                    equationMatrix[i, j] =
                        Vector3.Dot(unscaledVelocity, surfaceNormal);
                }
                equationMatrix[i, columns - 1] =
                    Vector3.Dot(vPanels, surfaceNormal);
            }

            return equationMatrix;
        }
        /// <summary>
        /// Solves for the source panel strengths required to guarantee
        /// tangential flow at the centre of each rectangular source panel.
        /// </summary>
        /// <param name="sourcePanels">
        /// The array of source panels, all of which are assumed to have
        /// strength 1.
        /// </param>
        /// <param name="vPanels">
        /// A vector representing the velocity vector of the panels (the
        /// negative far-field flow velocity).
        /// </param>
        /// <returns>
        /// An array whose each element corresponds to the required strength of
        /// the source panel with that index.
        /// </returns>
        public static double[] SolveForRequiredSourcePanelStrengths(
            SourcePanel[] sourcePanels, Vector3 vPanels)
        {
            double[,] equationMatrix = 
                CreateEquationMatrix(sourcePanels, vPanels);

            double[] sourcePanelStrengths = 
                MatrixHelpers.SolveWithGaussianElimination(ref equationMatrix);

            return sourcePanelStrengths;
        }
    }
}

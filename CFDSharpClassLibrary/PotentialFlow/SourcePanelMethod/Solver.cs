using System.Numerics;

namespace CFDSharpClassLibrary.PotentialFlow.SourcePanelMethod
{
    /// <summary>
    /// A class containing functions for solving source panel method problems.
    /// </summary>
    public static class Solver
    {
        /// <summary>
        /// Mutates the strengths of the source panels in the given array such 
        /// that the resulting flow is tangential to every panel's control 
        /// point.
        /// </summary>
        /// <param name="sourcePanels">
        /// The array of source panels to be mutated.
        /// </param>
        /// <param name="vInf">
        /// A vector representing the far-field velocity (negative velocity of 
        /// the panels).
        /// </param>
        public static void MutateSourcePanelArrayIntoSolution(
            ref SourcePanel[] sourcePanels, Vector3 vInf)
        {

        }
    }
}

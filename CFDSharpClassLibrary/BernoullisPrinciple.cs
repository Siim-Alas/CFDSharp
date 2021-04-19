
namespace CFDSharpClassLibrary
{
    /// <summary>
    /// A class containing functions related to Bernoulli's principle.
    /// </summary>
    public static class BernoullisPrinciple
    {
        private const double _g = 9.81;
        /// <summary>
        /// Gets the static pressure p from (1/2)*rho*v^2 + rho*g*z + p = 
        /// const. (Bernoulli's principle).
        /// </summary>
        /// <param name="p0">
        /// The static pressure at some other condition.
        /// </param>
        /// <param name="rho">The density at the current condition.</param>
        /// <param name="rho0">The density at some other condition.</param>
        /// <param name="vSquared">
        /// The squared magnitude of the velocity at the current condition.
        /// </param>
        /// <param name="v0Squared">
        /// The squared magnitude of the velocity at some other condition.
        /// </param>
        /// <param name="z">The altitude at the current condition.</param>
        /// <param name="z0">The altitude at some other condition.</param>
        /// <returns>The static pressure at the current condition.</returns>
        public static double GetStaticPressure(
            double p0, 
            double rho, 
            double rho0, 
            double vSquared, 
            double v0Squared, 
            double z, 
            double z0)
        {
            double const0 = 0.5 * rho0 * v0Squared + rho0 * _g * z0 + p0;
            double p = const0 - (0.5 * rho * vSquared + rho * _g * z);
            return p;
        }
    }
}

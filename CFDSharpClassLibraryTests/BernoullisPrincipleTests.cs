using CFDSharpClassLibrary;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CFDSharpClassLibraryTests
{
    [TestClass]
    public class BernoullisPrincipleTests
    {
        [TestMethod]
        public void GetStaticPressure_WithValidInput_ReturnsCorrectValue()
        {
            // Arrange
            const double g = 9.81; // m / s^2

            const double p0 = 1; // Pa
            const double rho0 = 2; // kg / m^3
            const double v0Squared = 3; // (m/s)^2
            const double z0 = 4; // m

            const double rho = 5; // kg / m^3
            const double vSquared = 6; // (m/s)^2
            const double z = 7; // m

            const double correctStaticPressure = 
                (0.5 * rho0 * v0Squared + rho0 * g * z0 + p0) - 
                (0.5 * rho * vSquared + rho * g * z);

            // Act
            double actualStaticPressure =
                BernoullisPrinciple.GetStaticPressure(
                    p0,
                    rho,
                    rho0,
                    vSquared,
                    v0Squared,
                    z,
                    z0);

            // Assert
            Assert.AreEqual(correctStaticPressure, actualStaticPressure);
        }
    }
}

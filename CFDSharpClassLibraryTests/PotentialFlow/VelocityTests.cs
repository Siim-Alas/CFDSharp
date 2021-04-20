using CFDSharpClassLibrary.PotentialFlow;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Numerics;

namespace CFDSharpClassLibraryTests.PotentialFlow
{
    [TestClass]
    public class VelocityTests
    {
        [TestMethod]
        public void ComputeSteadyState_WithAAndBEqualToZero_ReturnsTheZeroVector()
        {
            // Arrange
            float aOver2 = 0;
            float bOver2 = 0;
            Vector3 dr = Vector3.One;
            float strength = 3;
            Vector3 u = Vector3.UnitX;
            Vector3 v = Vector3.UnitY;

            // Act
            Vector3 result = Velocity.ComputeSteadyState(aOver2, bOver2, dr, strength, u, v);

            // Assert
            Assert.AreEqual(Vector3.Zero, result);
        }
        [TestMethod]
        public void ComputeSteadyState_WithAEqualToZero_ReturnsTheZeroVector()
        {
            // Arrange
            float aOver2 = 0;
            float bOver2 = 2;
            Vector3 dr = Vector3.One;
            float strength = 3;
            Vector3 u = Vector3.UnitX;
            Vector3 v = Vector3.UnitY;

            // Act
            Vector3 result = Velocity.ComputeSteadyState(aOver2, bOver2, dr, strength, u, v);

            // Assert
            Assert.AreEqual(Vector3.Zero, result);
        }
        [TestMethod]
        public void ComputeSteadyState_WithBEqualToZero_ReturnsTheZeroVector()
        {
            // Arrange
            float aOver2 = 1;
            float bOver2 = 0;
            Vector3 dr = Vector3.One;
            float strength = 3;
            Vector3 u = Vector3.UnitX;
            Vector3 v = Vector3.UnitY;

            // Act
            Vector3 result = Velocity.ComputeSteadyState(aOver2, bOver2, dr, strength, u, v);

            // Assert
            Assert.AreEqual(Vector3.Zero, result);
        }
        [TestMethod]
        public void ComputeSteadyState_WithDrEqualToTheZeroVector_ReturnsTheZeroVector()
        {
            // Arrange
            float aOver2 = 1;
            float bOver2 = 2;
            Vector3 dr = Vector3.Zero;
            float strength = 3;
            Vector3 u = Vector3.UnitX;
            Vector3 v = Vector3.UnitY;

            // Act
            Vector3 result = Velocity.ComputeSteadyState(aOver2, bOver2, dr, strength, u, v);

            // Assert
            Assert.AreEqual(Vector3.Zero, result);
        }
    }
}

using CFDSharpClassLibrary.PotentialFlow;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Numerics;

namespace CFDSharpClassLibraryTests.PotentialFlow
{
    [TestClass]
    public class VelocityPotentialTests
    {
        [TestMethod]
        public void ComputeSteadyState_WithAAndBEqualToZero_ReturnsZero()
        {
            // Arrange
            float aOver2 = 0;
            float bOver2 = 0;
            Vector3 dr = Vector3.One;
            float strength = 3;
            Vector3 u = Vector3.UnitX;
            Vector3 v = Vector3.UnitY;

            // Act
            float result = VelocityPotential.ComputeSteadyState(aOver2, bOver2, dr, strength, u, v);

            // Assert
            Assert.AreEqual(0, result);
        }
        [TestMethod]
        public void ComputeSteadyState_WithAEqualToZero_ReturnsZero()
        {
            // Arrange
            float aOver2 = 0;
            float bOver2 = 2;
            Vector3 dr = Vector3.One;
            float strength = 3;
            Vector3 u = Vector3.UnitX;
            Vector3 v = Vector3.UnitY;

            // Act
            float result = VelocityPotential.ComputeSteadyState(aOver2, bOver2, dr, strength, u, v);

            // Assert
            Assert.AreEqual(0, result);
        }
        [TestMethod]
        public void ComputeSteadyState_WithBEqualZero_ReturnsZero()
        {
            // Arrange
            float aOver2 = 1;
            float bOver2 = 0;
            Vector3 dr = Vector3.One;
            float strength = 3;
            Vector3 u = Vector3.UnitX;
            Vector3 v = Vector3.UnitY;

            // Act
            float result = VelocityPotential.ComputeSteadyState(aOver2, bOver2, dr, strength, u, v);

            // Assert
            Assert.AreEqual(0, result);
        }
        [TestMethod]
        public void ComputeSteadyState_WithDrEqualToTheZeroVector_ReturnsTheCorrectValue()
        {
            // Arrange
            float aOver2 = 1;
            float bOver2 = 2;
            Vector3 dr = Vector3.Zero;
            float strength = 3;
            Vector3 u = Vector3.UnitX;
            Vector3 v = Vector3.UnitY;

            float d11 = (-aOver2 * u - bOver2 * v).Length();
            float d12 = (-aOver2 * u + bOver2 * v).Length();
            float d21 = (aOver2 * u - bOver2 * v).Length();
            float d22 = (aOver2 * u + bOver2 * v).Length();

            float correctResult = -strength / (4 * MathF.PI) * 
                (-aOver2 * MathF.Log(-bOver2 + d11) - bOver2 * MathF.Log(-aOver2 + d11) -
                (-aOver2 * MathF.Log(bOver2 + d12) + bOver2 * MathF.Log(-aOver2 + d12)) -
                (aOver2 * MathF.Log(-bOver2 + d21) - bOver2 * MathF.Log(aOver2 + d21)) +
                aOver2 * MathF.Log(bOver2 + d22) + bOver2 * MathF.Log(aOver2 + d22));

            // Act
            float result = VelocityPotential.ComputeSteadyState(aOver2, bOver2, dr, strength, u, v);

            // Assert
            Assert.AreEqual(correctResult, result);
        }
    }
}

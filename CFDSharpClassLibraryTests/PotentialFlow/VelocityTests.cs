using CFDSharpClassLibrary.PotentialFlow;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
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
        [TestMethod]
        public void ComputeSteadyState_WithDrInTheUVPlane_ReturnsTheCorrectValue()
        {
            // Arrange
            float aOver2 = 1;
            float bOver2 = 2;
            Vector3 dr = new(3, 4, 0);
            float strength = 6;
            Vector3 u = Vector3.UnitX;
            Vector3 v = Vector3.UnitY;

            float d11 = (dr - aOver2 * u - bOver2 * v).Length();
            float d12 = (dr - aOver2 * u + bOver2 * v).Length();
            float d21 = (dr + aOver2 * u - bOver2 * v).Length();
            float d22 = (dr + aOver2 * u + bOver2 * v).Length();

            float uDotDr = Vector3.Dot(u, dr);
            float vDotDr = Vector3.Dot(v, dr);

            float ln1 = MathF.Log(MathF.Abs(
                (-bOver2 - vDotDr + d11) * (bOver2 - vDotDr + d22) / 
                ((-bOver2 - vDotDr + d21) * (bOver2 - vDotDr + d12))));
            float ln2 = MathF.Log(MathF.Abs(
                (-aOver2 - uDotDr + d11) * (aOver2 - uDotDr + d22) /
                ((aOver2 - uDotDr + d21) * (-aOver2 - uDotDr + d12))));

            Vector3 correctResult = strength / (4 * MathF.PI) * (ln1 * u + ln2 * v);

            // Act
            Vector3 result = Velocity.ComputeSteadyState(aOver2, bOver2, dr, strength, u, v);

            // Assert
            Assert.AreEqual(correctResult, result);
        }
    }
}

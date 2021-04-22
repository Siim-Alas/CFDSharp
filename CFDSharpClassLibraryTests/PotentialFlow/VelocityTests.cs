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
            Vector3 u = Vector3.UnitX;
            Vector3 v = Vector3.UnitY;

            // Act
            Vector3 result = Velocity.ComputeSteadyState(aOver2, bOver2, dr, u, v);

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
            Vector3 u = Vector3.UnitX;
            Vector3 v = Vector3.UnitY;

            // Act
            Vector3 result = Velocity.ComputeSteadyState(aOver2, bOver2, dr, u, v);

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
            Vector3 u = Vector3.UnitX;
            Vector3 v = Vector3.UnitY;

            // Act
            Vector3 result = Velocity.ComputeSteadyState(aOver2, bOver2, dr, u, v);

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
            Vector3 u = Vector3.UnitX;
            Vector3 v = Vector3.UnitY;

            // Act
            Vector3 result = Velocity.ComputeSteadyState(aOver2, bOver2, dr, u, v);

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

            Vector3 correctResult = 1 / (4 * MathF.PI) * (ln1 * u + ln2 * v);

            // Act
            Vector3 result = Velocity.ComputeSteadyState(aOver2, bOver2, dr, u, v);

            // Assert
            Assert.AreEqual(correctResult, result);
        }
        [TestMethod]
        public void ComputeSteadyState_WithDrOutsideTheUVPlane_ReturnsTheCorrectValue()
        {
            // Arrange
            float aOver2 = 1;
            float bOver2 = 2;
            Vector3 dr = new(3, 4, 5);
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

            Vector3 c = dr - uDotDr * u - vDotDr * v;
            float sqrtK = c.Length();

            float atan11 = MathF.Atan2(
                (-aOver2 - uDotDr) * (-bOver2 - vDotDr), sqrtK * d11);
            float atan12 = MathF.Atan2(
                (-aOver2 - uDotDr) * (bOver2 - vDotDr), sqrtK * d12);
            float atan21 = MathF.Atan2(
                (aOver2 - uDotDr) * (-bOver2 - vDotDr), sqrtK * d21);
            float atan22 = MathF.Atan2(
                (aOver2 - uDotDr) * (bOver2 - vDotDr), sqrtK * d22);

            float atanSum = atan22 - atan21 - atan12 + atan11;

            Vector3 correctResult = 1 / (4 * MathF.PI) * (ln1 * u + ln2 * v +
                (atanSum / sqrtK) * c);

            // Act
            Vector3 result = Velocity.ComputeSteadyState(aOver2, bOver2, dr, u, v);

            // Assert
            Assert.AreEqual(correctResult, result);
        }
    }
}

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
        [TestMethod]
        public void ComputeSteadyState_WithValidInput_ReturnsTheCorrectValue()
        {
            // Arrange
            float aOver2 = 1;
            float bOver2 = 2;
            Vector3 dr = new(1, 2, 3);
            float strength = 3;
            Vector3 u = Vector3.UnitX;
            Vector3 v = Vector3.UnitY;

            float d11 = (dr - aOver2 * u - bOver2 * v).Length();
            float d12 = (dr - aOver2 * u + bOver2 * v).Length();
            float d21 = (dr + aOver2 * u - bOver2 * v).Length();
            float d22 = (dr + aOver2 * u + bOver2 * v).Length();

            float uDotDr = Vector3.Dot(u, dr);
            float vDotDr = Vector3.Dot(v, dr);

            float lnArg111 = -bOver2 - vDotDr + d11;
            float lnArg112 = bOver2 - vDotDr + d12;
            float lnArg121 = -bOver2 - vDotDr + d21;
            float lnArg122 = bOver2 - vDotDr + d22;

            float lnArg211 = -aOver2 - uDotDr + d11;
            float lnArg212 = -aOver2 - uDotDr + d12;
            float lnArg221 = aOver2 - uDotDr + d21;
            float lnArg222 = aOver2 - uDotDr + d22;

            float scaledLn1Sum =
                aOver2 * MathF.Log(lnArg122 * lnArg112 / (lnArg111 * lnArg121)) +
                uDotDr * MathF.Log(lnArg112 * lnArg121 / (lnArg111 * lnArg122));
            float scaledLn2Sum =
                bOver2 * MathF.Log(lnArg222 * lnArg221 / (lnArg211 * lnArg212)) +
                uDotDr * MathF.Log(lnArg212 * lnArg221 / (lnArg211 * lnArg222));

            float sqrtK = MathF.Sqrt(dr.LengthSquared() - uDotDr * uDotDr -
                vDotDr * vDotDr);

            float atan11 = MathF.Atan2((-aOver2 - uDotDr) * (-bOver2 - vDotDr),
                sqrtK * d11);
            float atan12 = MathF.Atan2((-aOver2 - uDotDr) * (bOver2 - vDotDr),
                sqrtK * d12);
            float atan21 = MathF.Atan2((aOver2 - uDotDr) * (-bOver2 - vDotDr),
                sqrtK * d21);
            float atan22 = MathF.Atan2((aOver2 - uDotDr) * (bOver2 - vDotDr),
                sqrtK * d22);

            float atanSum = atan22 - atan21 - atan12 + atan11;

            float correctResult = -strength / (4 * MathF.PI) *
                (scaledLn1Sum + scaledLn2Sum - sqrtK * atanSum);

            // Act
            float result = VelocityPotential.ComputeSteadyState(aOver2, bOver2, dr, strength, u, v);

            // Assert
            Assert.AreEqual(correctResult, result);
        }
    }
}

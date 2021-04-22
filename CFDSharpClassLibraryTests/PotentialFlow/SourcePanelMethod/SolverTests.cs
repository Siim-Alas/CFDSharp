using CFDSharpClassLibrary.PotentialFlow;
using CFDSharpClassLibrary.PotentialFlow.SourcePanelMethod;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Numerics;

namespace CFDSharpClassLibraryTests.PotentialFlow.SourcePanelMethod
{
    [TestClass]
    public class SolverTests
    {
        [TestMethod]
        public void CreateEquationMatrix_WithUnitCubeInput_CreatesAMAtrixWith6RowsAnd7Columns()
        {
            // Arrange
            UnitStrengthRectangularSourcePanel[] sourcePanels = new UnitStrengthRectangularSourcePanel[6]
            {
                new UnitStrengthRectangularSourcePanel
                {
                    AOver2 = 0.5F, BOver2 = 0.5F, Position = new Vector3(0.5F, 0, 0), U = Vector3.UnitY, V = Vector3.UnitZ
                },
                new UnitStrengthRectangularSourcePanel
                {
                    AOver2 = 0.5F, BOver2 = 0.5F, Position = new Vector3(-0.5F, 0, 0), U = Vector3.UnitY, V = Vector3.UnitZ
                },
                new UnitStrengthRectangularSourcePanel
                {
                    AOver2 = 0.5F, BOver2 = 0.5F, Position = new Vector3(0, 0.5F, 0), U = Vector3.UnitZ, V = Vector3.UnitX
                },
                new UnitStrengthRectangularSourcePanel
                {
                    AOver2 = 0.5F, BOver2 = 0.5F, Position = new Vector3(0, -0.5F, 0), U = Vector3.UnitZ, V = Vector3.UnitX
                },
                new UnitStrengthRectangularSourcePanel
                {
                    AOver2 = 0.5F, BOver2 = 0.5F, Position = new Vector3(0, 0, 0.5F), U = Vector3.UnitX, V = Vector3.UnitY
                },
                new UnitStrengthRectangularSourcePanel
                {
                    AOver2 = 0.5F, BOver2 = 0.5F, Position = new Vector3(0, 0, -0.5F), U = Vector3.UnitX, V = Vector3.UnitY
                }
            };
            Vector3 vPanels = new Vector3(10, 0, 0);

            // Act
            double[,] result = Solver.CreateEquationMatrix(sourcePanels, vPanels);
            int rows = result.GetLength(0);
            int columns = result.GetLength(1);

            // Assert
            Assert.IsTrue((rows == 6) && (columns == 7));
        }
        [TestMethod]
        public void CreateEquationMatrix_WithUnitCubeInput_CreatesTheCorrectLHS()
        {
            // Arrange
            UnitStrengthRectangularSourcePanel[] sourcePanels = new UnitStrengthRectangularSourcePanel[6]
            {
                new UnitStrengthRectangularSourcePanel
                {
                    AOver2 = 0.5F, BOver2 = 0.5F, Position = new Vector3(0.5F, 0, 0), U = Vector3.UnitY, V = Vector3.UnitZ
                },
                new UnitStrengthRectangularSourcePanel
                {
                    AOver2 = 0.5F, BOver2 = 0.5F, Position = new Vector3(-0.5F, 0, 0), U = Vector3.UnitY, V = Vector3.UnitZ
                },
                new UnitStrengthRectangularSourcePanel
                {
                    AOver2 = 0.5F, BOver2 = 0.5F, Position = new Vector3(0, 0.5F, 0), U = Vector3.UnitZ, V = Vector3.UnitX
                },
                new UnitStrengthRectangularSourcePanel
                {
                    AOver2 = 0.5F, BOver2 = 0.5F, Position = new Vector3(0, -0.5F, 0), U = Vector3.UnitZ, V = Vector3.UnitX
                },
                new UnitStrengthRectangularSourcePanel
                {
                    AOver2 = 0.5F, BOver2 = 0.5F, Position = new Vector3(0, 0, 0.5F), U = Vector3.UnitX, V = Vector3.UnitY
                },
                new UnitStrengthRectangularSourcePanel
                {
                    AOver2 = 0.5F, BOver2 = 0.5F, Position = new Vector3(0, 0, -0.5F), U = Vector3.UnitX, V = Vector3.UnitY
                }
            };
            Vector3 vPanels = new Vector3(10, 0, 0);
            double[,] correctLHS = new double[6, 6] 
            {
                { 
                    Velocity.ComputeSteadyState(sourcePanels[0].Position, sourcePanels[0]).X, 
                    Velocity.ComputeSteadyState(sourcePanels[0].Position, sourcePanels[1]).X,
                    Velocity.ComputeSteadyState(sourcePanels[0].Position, sourcePanels[2]).X,
                    Velocity.ComputeSteadyState(sourcePanels[0].Position, sourcePanels[3]).X,
                    Velocity.ComputeSteadyState(sourcePanels[0].Position, sourcePanels[4]).X,
                    Velocity.ComputeSteadyState(sourcePanels[0].Position, sourcePanels[5]).X
                },
                {
                    Velocity.ComputeSteadyState(sourcePanels[1].Position, sourcePanels[0]).X,
                    Velocity.ComputeSteadyState(sourcePanels[1].Position, sourcePanels[1]).X,
                    Velocity.ComputeSteadyState(sourcePanels[1].Position, sourcePanels[2]).X,
                    Velocity.ComputeSteadyState(sourcePanels[1].Position, sourcePanels[3]).X,
                    Velocity.ComputeSteadyState(sourcePanels[1].Position, sourcePanels[4]).X,
                    Velocity.ComputeSteadyState(sourcePanels[1].Position, sourcePanels[5]).X
                },
                {
                    Velocity.ComputeSteadyState(sourcePanels[2].Position, sourcePanels[0]).Y,
                    Velocity.ComputeSteadyState(sourcePanels[2].Position, sourcePanels[1]).Y,
                    Velocity.ComputeSteadyState(sourcePanels[2].Position, sourcePanels[2]).Y,
                    Velocity.ComputeSteadyState(sourcePanels[2].Position, sourcePanels[3]).Y,
                    Velocity.ComputeSteadyState(sourcePanels[2].Position, sourcePanels[4]).Y,
                    Velocity.ComputeSteadyState(sourcePanels[2].Position, sourcePanels[5]).Y
                },
                {
                    Velocity.ComputeSteadyState(sourcePanels[3].Position, sourcePanels[0]).Y,
                    Velocity.ComputeSteadyState(sourcePanels[3].Position, sourcePanels[1]).Y,
                    Velocity.ComputeSteadyState(sourcePanels[3].Position, sourcePanels[2]).Y,
                    Velocity.ComputeSteadyState(sourcePanels[3].Position, sourcePanels[3]).Y,
                    Velocity.ComputeSteadyState(sourcePanels[3].Position, sourcePanels[4]).Y,
                    Velocity.ComputeSteadyState(sourcePanels[3].Position, sourcePanels[5]).Y
                },
                {
                    Velocity.ComputeSteadyState(sourcePanels[4].Position, sourcePanels[0]).Z,
                    Velocity.ComputeSteadyState(sourcePanels[4].Position, sourcePanels[1]).Z,
                    Velocity.ComputeSteadyState(sourcePanels[4].Position, sourcePanels[2]).Z,
                    Velocity.ComputeSteadyState(sourcePanels[4].Position, sourcePanels[3]).Z,
                    Velocity.ComputeSteadyState(sourcePanels[4].Position, sourcePanels[4]).Z,
                    Velocity.ComputeSteadyState(sourcePanels[4].Position, sourcePanels[5]).Z
                },
                {
                    Velocity.ComputeSteadyState(sourcePanels[5].Position, sourcePanels[0]).Z,
                    Velocity.ComputeSteadyState(sourcePanels[5].Position, sourcePanels[1]).Z,
                    Velocity.ComputeSteadyState(sourcePanels[5].Position, sourcePanels[2]).Z,
                    Velocity.ComputeSteadyState(sourcePanels[5].Position, sourcePanels[3]).Z,
                    Velocity.ComputeSteadyState(sourcePanels[5].Position, sourcePanels[4]).Z,
                    Velocity.ComputeSteadyState(sourcePanels[5].Position, sourcePanels[5]).Z
                }
            };

            // Act
            double[,] result = Solver.CreateEquationMatrix(sourcePanels, vPanels);

            bool lhsIsCorrect = true;
            for (int i = 0; i < 6; i++)
            {
                for (int j = 0; j < 6; j++)
                {
                    if (result[i, j] != correctLHS[i, j])
                    {
                        lhsIsCorrect = false;
                    }
                }
            }

            // Assert
            Assert.IsTrue(lhsIsCorrect);
        }
        [TestMethod]
        public void CreateEquationMatrix_WithUnitCubeInput_CreatesTheCorrectRHS()
        {
            // Arrange
            UnitStrengthRectangularSourcePanel[] sourcePanels = new UnitStrengthRectangularSourcePanel[6]
            {
                new UnitStrengthRectangularSourcePanel
                {
                    AOver2 = 0.5F, BOver2 = 0.5F, Position = new Vector3(0.5F, 0, 0), U = Vector3.UnitY, V = Vector3.UnitZ
                },
                new UnitStrengthRectangularSourcePanel
                {
                    AOver2 = 0.5F, BOver2 = 0.5F, Position = new Vector3(-0.5F, 0, 0), U = Vector3.UnitY, V = Vector3.UnitZ
                },
                new UnitStrengthRectangularSourcePanel
                {
                    AOver2 = 0.5F, BOver2 = 0.5F, Position = new Vector3(0, 0.5F, 0), U = Vector3.UnitZ, V = Vector3.UnitX
                },
                new UnitStrengthRectangularSourcePanel
                {
                    AOver2 = 0.5F, BOver2 = 0.5F, Position = new Vector3(0, -0.5F, 0), U = Vector3.UnitZ, V = Vector3.UnitX
                },
                new UnitStrengthRectangularSourcePanel
                {
                    AOver2 = 0.5F, BOver2 = 0.5F, Position = new Vector3(0, 0, 0.5F), U = Vector3.UnitX, V = Vector3.UnitY
                },
                new UnitStrengthRectangularSourcePanel
                {
                    AOver2 = 0.5F, BOver2 = 0.5F, Position = new Vector3(0, 0, -0.5F), U = Vector3.UnitX, V = Vector3.UnitY
                }
            };
            Vector3 vPanels = new Vector3(10, 0, 0);
            double[] correctRHS = new double[6]
            {
                10,
                10,

                0,
                0,

                0,
                0
            };

            // Act
            double[,] result = Solver.CreateEquationMatrix(sourcePanels, vPanels);

            bool rhsIsCorrect = true;
            for (int i = 0; i < correctRHS.Length; i++)
            {
                if (result[i, 6] != correctRHS[i])
                {
                    rhsIsCorrect = false;
                }
            }

            // Assert
            Assert.IsTrue(rhsIsCorrect);
        }
        [TestMethod]
        public void SolveForRequiredSourcePanelStrengths_WithUnitCubeInput_SatisfiesTheTangentialFlowConstraint()
        {
            // Arrange
            UnitStrengthRectangularSourcePanel[] sourcePanels = new UnitStrengthRectangularSourcePanel[6]
            {
                new UnitStrengthRectangularSourcePanel
                {
                    AOver2 = 0.5F, BOver2 = 0.5F, Position = new Vector3(0.5F, 0, 0), U = Vector3.UnitY, V = Vector3.UnitZ
                },
                new UnitStrengthRectangularSourcePanel
                {
                    AOver2 = 0.5F, BOver2 = 0.5F, Position = new Vector3(-0.5F, 0, 0), U = Vector3.UnitY, V = Vector3.UnitZ
                },
                new UnitStrengthRectangularSourcePanel
                {
                    AOver2 = 0.5F, BOver2 = 0.5F, Position = new Vector3(0, 0.5F, 0), U = Vector3.UnitZ, V = Vector3.UnitX
                },
                new UnitStrengthRectangularSourcePanel
                {
                    AOver2 = 0.5F, BOver2 = 0.5F, Position = new Vector3(0, -0.5F, 0), U = Vector3.UnitZ, V = Vector3.UnitX
                },
                new UnitStrengthRectangularSourcePanel
                {
                    AOver2 = 0.5F, BOver2 = 0.5F, Position = new Vector3(0, 0, 0.5F), U = Vector3.UnitX, V = Vector3.UnitY
                },
                new UnitStrengthRectangularSourcePanel
                {
                    AOver2 = 0.5F, BOver2 = 0.5F, Position = new Vector3(0, 0, -0.5F), U = Vector3.UnitX, V = Vector3.UnitY
                }
            };
            Vector3 vPanels = new Vector3(10, 0, 0);

            // Act
            double[] strengths = Solver.SolveForRequiredSourcePanelStrengths(sourcePanels, vPanels);

            bool flowIsTangentialAtEachControlPoint = true;
            for (int i = 0; i < sourcePanels.Length; i ++)
            {
                Vector3 surfaceNormal = Vector3.Cross(sourcePanels[i].U, sourcePanels[i].V);

                Vector3 velocity = vPanels;
                for (int j = 0; j < sourcePanels.Length; j++)
                {
                    velocity += (float)strengths[j] * Velocity.ComputeSteadyState(sourcePanels[i].Position, sourcePanels[j]);
                }

                if (Vector3.Dot(velocity, surfaceNormal) != 0)
                {
                    flowIsTangentialAtEachControlPoint = false;
                }
            }

            // Assert
            Assert.IsTrue(flowIsTangentialAtEachControlPoint);
        }
    }
}

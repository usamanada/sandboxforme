using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Readify.Puzzles.Triangle
{
    public class Triangles
    {
        #region public methods

        public TriangleType GetTriangleType(int SideA, int SideB, int SideC)
        {
            if (validateArgument(SideA) && validateArgument(SideB) && validateArgument(SideC))
            {
                return determineTriangle(SideA, SideB, SideC);
            }
            else
            {
                return TriangleType.Error;
            }
        }

        #endregion

        #region private methods

        private TriangleType determineTriangle(int SideA, int SideB, int SideC)
        {
            if (SideA == SideB && SideA == SideC && SideB == SideC)
            {
                return TriangleType.Equilateral;
            }
            else if(SideA != SideB && SideA != SideC && SideB != SideC)
            {
                return TriangleType.Scalene;
            }
            else if ((SideA == SideB && (SideC != SideA && SideC != SideB)) ||
                     (SideA == SideC && (SideB != SideA && SideB != SideC)) ||
                     (SideB == SideC && (SideA != SideB && SideA != SideC)))
            {
                return TriangleType.Isossceles;
            }
            else
            {
                return TriangleType.Error;
            }

        }

        private bool validateArgument(int Side)
        {
            if (Side <= 0)
            {
                return false;
            }
            return true;
        }

        #endregion
    }
}

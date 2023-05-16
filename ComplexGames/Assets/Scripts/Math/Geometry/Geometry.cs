using UnityEngine;

namespace Math.Geometry
{
	public class Geometry : MonoBehaviour
	{
		public static bool IsTriangleOrientedClockwise(Vector2 p1, Vector2 p2, Vector2 p3)
		{
			bool isClockWise = true;

			float determinant = p1.x * p2.y + p3.x * p1.y + p2.x * p3.y - p1.x * p3.y - p3.x * p2.y - p2.x * p1.y;

			if (determinant > 0f)
			{
				isClockWise = false;
			}

			return isClockWise;
		}
		
		public static float IsPointInCircleOrNot(Vector2 aVec, Vector2 bVec, Vector2 cVec, Vector2 dVec)
		{
			float a = aVec.x - dVec.x;
			float d = bVec.x - dVec.x;
			float g = cVec.x - dVec.x;

			float b = aVec.y - dVec.y;
			float e = bVec.y - dVec.y;
			float h = cVec.y - dVec.y;

			float c = a * a + b * b;
			float f = d * d + e * e;
			float i = g * g + h * h;

			float determinant = (a * e * i) + (b * f * g) + (c * d * h) - (g * e * c) - (h * f * a) - (i * d * b);

			return determinant;
		}
		
		public static bool IsQuadrilateralConvex(Vector2 a, Vector2 b, Vector2 c, Vector2 d)
		{
			bool isConvex = false;

			bool abc = Geometry.IsTriangleOrientedClockwise(a, b, c);
			bool abd = Geometry.IsTriangleOrientedClockwise(a, b, d);
			bool bcd = Geometry.IsTriangleOrientedClockwise(b, c, d);
			bool cad = Geometry.IsTriangleOrientedClockwise(c, a, d);

			if (abc && abd && bcd & !cad)
			{
				isConvex = true;
			}
			else if (abc && abd && !bcd & cad)
			{
				isConvex = true;
			}
			else if (abc && !abd && bcd & cad)
			{
				isConvex = true;
			}
			//The opposite sign, which makes everything inverted
			else if (!abc && !abd && !bcd & cad)
			{
				isConvex = true;
			}
			else if (!abc && !abd && bcd & !cad)
			{
				isConvex = true;
			}
			else if (!abc && abd && !bcd & !cad)
			{
				isConvex = true;
			}


			return isConvex;
		}

		public static double IsPointInsideOutsideOrOnCircle(Vector2 aPos, Vector2 bPos, Vector2 cPos, Vector2 dPos)
		{
			throw new System.NotImplementedException();
		}
	}
}
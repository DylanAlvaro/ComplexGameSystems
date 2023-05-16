using UnityEngine;

namespace Math
{
	public static class Intersections 
	{
		public static bool AreLinesIntersecting(Vector2 l1, Vector2 l2, Vector2 l3, Vector2 l4, bool shouldIncludeEndPoints)
		{
			bool isIntersecting = false;

			float denominator = (l4.y - l3.y) * (l2.x - l1.x) - (l4.x - l3.x) * (l2.y - l1.y);

			//Make sure the denominator is > 0, if not the lines are parallel
			if (denominator != 0f)
			{
				float u_a = ((l4.x - l3.x) * (l1.y - l3.y) - (l4.y - l3.y) * (l1.x - l3.x)) / denominator;
				float u_b = ((l2.x - l1.x) * (l1.y - l3.y) - (l2.y - l1.y) * (l1.x - l3.x)) / denominator;

				//Are the line segments intersecting if the end points are the same
				if (shouldIncludeEndPoints)
				{
					//Is intersecting if u_a and u_b are between 0 and 1 or exactly 0 or 1
					if (u_a >= 0f && u_a <= 1f && u_b >= 0f && u_b <= 1f)
					{
						isIntersecting = true;
					}
				}
				else
				{
					//Is intersecting if u_a and u_b are between 0 and 1
					if (u_a > 0f && u_a < 1f && u_b > 0f && u_b < 1f)
					{
						isIntersecting = true;
					}
				}
		
			}

			return isIntersecting;
		}
	}
}
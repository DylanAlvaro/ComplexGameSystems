using UnityEngine;

using Vector2 = System.Numerics.Vector2;

namespace Math
{
	public class Vertex
	{
		public Vector3 position;
		public HalfEdge halfEdge;
		public Triangle triangle;

		public Vertex prevVertex;
		public Vertex nextVertex;

		public bool isReflex;
		public bool isConvex;
		public bool isEar;

		public Vertex(Vector3 position)
		{
			this.position = position;
		}

		public Vector2 GetPos2D()
		{
			Vector2 pos2D = new Vector2(position.x, position.y);

			return pos2D;
		}
	}
}
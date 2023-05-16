using UnityEngine;

namespace Math
{
	public class HalfEdge
	{
		public Vertex v;
		public Triangle t;

		public HalfEdge nextEdge;
		public HalfEdge prevEdge;

		public HalfEdge oppositeEdge;

		public HalfEdge(Vertex v)
		{
			this.v = v;
		}
	}
}
using Scripts.Dungeon;

using System;

using UnityEngine;

namespace Scripts.Dungeon.Math
{
	public  class Edge : IEquatable<Edge> 
	{
		public Graphs.Vertex U { get; set; }
		public Graphs.Vertex V { get; set; }

		public Edge() {

		}

		public Edge(Graphs.Vertex u, Graphs.Vertex v) {
			U = u;
			V = v;
		}

		public static bool operator ==(Edge left, Edge right) 
		{
			return (left.U == right.U || left.U == right.V)
			       && (left.V == right.U || left.V == right.V);
		}

		public static bool operator !=(Edge left, Edge right) 
		{
			return !(left == right);
		}

		public override bool Equals(object obj) 
		{
			if (obj is Edge e) {
				return this == e;
			}

			return false;
		}

		public bool Equals(Edge e) {
			return this == e;
		}

		public override int GetHashCode()
		{
			return U.GetHashCode() ^ V.GetHashCode();
		}
	}
}
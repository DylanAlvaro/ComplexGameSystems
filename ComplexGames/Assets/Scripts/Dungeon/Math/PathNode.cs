using UnityEngine;

namespace Scripts.Dungeon
{
	public class PathNode
	{
		public int index;
		public Vector3 position;

		public float gCost;
		public float hCost;
		public float fCost;

		public PathNode cameFromNode;

		public PathNode(int index, Vector3 position)
		{
			this.index = index;
			this.position = position;
		}

		public void CalculateFCost()
		{
			fCost = hCost + gCost;
		}
	}
}
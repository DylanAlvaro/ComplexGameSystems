using UnityEngine;
using UnityEngine.UIElements;

namespace Scripts.Dungeon
{
	public class Vertex
	{
		public int Key { get; set; } = int.MaxValue;
		public int Parent { get; set; } = -1;
		public int V { get; set; }
		public bool IsProcessed { get; set; }
	}
}
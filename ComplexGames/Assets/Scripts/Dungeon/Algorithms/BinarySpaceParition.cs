using System;
using System.Collections.Generic;

using UnityEngine;

namespace Scripts.Dungeon.Algorithms
{
	public class BinarySpaceParition : MonoBehaviour
	{
		private BSPLeaf root;
		private const int MAX_LEAF_SIZE = 50;
		private List<BSPLeaf> leaves = new List<BSPLeaf>();

		private void Start()
		{
			CreateRooms();
		}
		
		void CreateRooms()
		{
			root = new BSPLeaf(0, 0, 400, 400);
			leaves.Add(root);

			bool didSplit = true;

			while(didSplit)
			{
				didSplit = false;
				for(int i = 0; i< leaves.Count; i++)
				{
					if (leaves[i].leftChild == null && leaves[i].rightChild == null) //if this leaf is not already split
					{
						if (leaves[i].SplitRooms())
						{
							//If we did split, add child leaves
							leaves.Add(leaves[i].leftChild);
							leaves.Add(leaves[i].rightChild);
							didSplit = true;
						}
					}
				}
			}
		}

	}
}
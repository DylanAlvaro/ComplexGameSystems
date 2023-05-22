using UnityEngine;

namespace Scripts.Dungeon.Algorithms
{
	public class BSPLeaf
	{
		private int minLeafSize = 50;
		public int x, y, width, height;

		public BSPLeaf leftChild;
		public BSPLeaf rightChild;
		public bool hasRoom;
		public Vector3 roomSize;
		public Vector3 roomPos;
		
		private GameObject quad;

		public BSPLeaf(int _x, int _y ,int _width, int _height)
		{
			x = _x;
			y = _y;
			height = _height;
			width = _width;

			quad = GameObject.CreatePrimitive(PrimitiveType.Quad);
			quad.transform.position = new Vector3(x + (width * .5f), y + (height * .5f), 0);
			quad.transform.localScale = new Vector3(width, height, 1f);
		}

		public bool SplitRooms()
		{
			if(leftChild != null || rightChild != null)
				return false;

			bool SplitH = Random.Range(0, 1f) > .5f;

			if(width > height && height / width >= 0.5f)
				SplitH = false;
			else if(height > width && width / height >= 0.5f)
				SplitH = true;

			int max = (SplitH ? height : width) - minLeafSize;

			if(max <= minLeafSize)
				return false;

			int split = (int) Random.Range(minLeafSize, max);

			if(SplitH)
			{
				leftChild = new BSPLeaf(x, y, width, split);
				rightChild = new BSPLeaf(x, y + split, width, height - split);
			}
			else
			{
				leftChild = new BSPLeaf(x, y, split, height);
				rightChild = new BSPLeaf(x + split,y , width - split, height);
			}

			if(quad != null)
			{
				GameObject.Destroy(quad);
				quad = null;
			}

			return true;
		}

		public void CreateRooms()
		{
			if (leftChild != null || rightChild != null)
			{
				if (leftChild != null)
				{
					leftChild.CreateRooms();
				}
				if (rightChild != null)
				{
					rightChild.CreateRooms();
				}
				hasRoom = false;
			}
			else
			{
				roomSize = new Vector2(Random.Range(3, width - 2), Random.Range(3, height - 2));
				roomPos = new Vector2(Random.Range(2, width - roomSize.x - 2), Random.Range(2, height - roomSize.y - 2));
				hasRoom = true;
			}
		}
	}
	
}
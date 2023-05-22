using System;
using System.Collections.Generic;

using UnityEditor;

using UnityEngine;

namespace Scripts.Dungeon
{
	public class PriorityQueue<T>
	{
		class Node
		{
			public int Priority { get; set; }
			public T Object { get; set; }
		}
		
		List<Node> queue = new List<Node>();
		int heapSize = -1;
		bool _isMinPriorityQueue;
		public int Count { get { return queue.Count; } }

		public PriorityQueue(bool isMinPriorityQueue = false)
		{
			_isMinPriorityQueue = isMinPriorityQueue;
		}

		private int ChildL(int i)
		{
			return i * 2 + 1;
		}
		private int ChildR(int i)
		{
			return i * 2 + 2;
		}

		private void Swap(int i, int j)
		{
			var temp = queue[i];
			queue[i] = queue[j];
			queue[j] = temp;
		}

		private void MaxHeapify(int i)
		{
			int leftChild = ChildL(i);
			int rightChild = ChildR(i);

			int high = i;
			
			if (leftChild <= heapSize && queue[high].Priority < queue[leftChild].Priority)
				high = leftChild;
			if (rightChild <= heapSize && queue[high].Priority < queue[rightChild].Priority)
				high = rightChild;

			if (high != i)
			{
				Swap(high, i);
				MaxHeapify(high);
			}
		} 
		private void MinHeapify(int i)
		{
			int leftChild = ChildL(i);
			int rightChild = ChildR(i);

			int low = i;

			if (leftChild <= heapSize && queue[low].Priority > queue[leftChild].Priority)
				low = leftChild;
			if (rightChild <= heapSize && queue[low].Priority > queue[rightChild].Priority)
				low = rightChild;

			if (low != i)
			{
				Swap(low, i);
				MinHeapify(low);
			}
		}

		private void BuildHeapMax(int i )
		{
			while(i >= 0 && queue[(i - 1) / 2].Priority < queue[i].Priority)
			{
				Swap(i, (i - 1) / 2);
				i = (i - 1) / 2;
			}
		}
		private void BuildHeapMin(int i)
		{
			while(i >= 0 && queue[(i - 1) / 2].Priority > queue[i].Priority)
			{
				Swap(i, (i - 1) / 2);
				i = (i - 1) / 2;
			}
		}

		public T Dequeue()
		{
			if(heapSize > -1)
			{
				var returnVal = queue[0].Object;
				queue[0] = queue[heapSize];
				queue.RemoveAt(heapSize);
				heapSize--;
				
				if(_isMinPriorityQueue)
					MinHeapify(0);
				else
					MaxHeapify(0);

				return returnVal;
			}
			else
			{
				throw new Exception("Queue is empty");
			}
		}
		
		public void Enqueue(int priority, T obj)
		{
			Node node = new Node() { Priority = priority, Object = obj };
			queue.Add(node);
			heapSize++;
			//Maintaining heap
			if (_isMinPriorityQueue)
				BuildHeapMin(heapSize);
			else
				BuildHeapMax(heapSize);
		}
		
		public void UpdatePriority(T obj, int priority)
		{
			int i = 0;
			for (; i <= heapSize; i++)
			{
				Node node = queue[i];
				if (object.ReferenceEquals(node.Object, obj))
				{
					node.Priority = priority;
					if (_isMinPriorityQueue)
					{
						BuildHeapMin(i);
						MinHeapify(i);
					}
					else
					{
						BuildHeapMax(i);
						MaxHeapify(i);
					}
				}
			}
		}
	}
}
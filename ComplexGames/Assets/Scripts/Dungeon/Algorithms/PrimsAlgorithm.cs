using System;
using System.Collections.Generic;

using UnityEngine;

namespace Scripts.Dungeon.Algorithms
{
	public class PrimsAlgorithm : MonoBehaviour
	{
		public PriorityQueue<Vertex> PriorityQueue;
		
		public static Vertex[] FindMst(int[][] graph)
		{
			PriorityQueue<Vertex> queue = new PriorityQueue<Vertex>(true);
			int vertCount = graph.GetLength(0);

			Vertex[] vertices = new Vertex[vertCount];

			for (int i = 0; i < vertCount; i++)
				vertices[i] = new Vertex() { Key = int.MaxValue, Parent = -1, V = i };
			
			vertices[0].Key = 0;
			
			for (int i = 0; i < vertCount; i++)
				queue.Enqueue(vertices[i].Key, vertices[i]);

			while(queue.Count > 0)
			{
				Vertex minVertex = queue.Dequeue();
				int u = minVertex.V;
				vertices[u].IsProcessed = true;

				int[] edges = graph[minVertex.V];
				for(int v = 0; v < edges.Length; v++)
				{
					if(graph[u][v] > 0 && !vertices[v].IsProcessed && graph[u][v] < vertices[v].Key)
					{
						vertices[v].Parent = u;
						vertices[v].Key = graph[u][v];
						queue.UpdatePriority(vertices[v], vertices[v].Key);
					}
				}
			}
			return vertices;

		}
	}
}
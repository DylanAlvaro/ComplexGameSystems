using System.Collections.Generic;
using UnityEngine;

namespace Scripts.Dungeon.Pathfinder
{
    public class DungeonPathfinder 
    {
        public class Node
        {
            public Vector3Int Position { get; private set; }
            public Node Previous { get; set; }
            public HashSet<Vector3Int> PreviousSet { get; private set; }
            public float Cost { get; set; }

            public Node(Vector3Int position) 
            {
                Position = position;
                PreviousSet = new HashSet<Vector3Int>();
            }
        }

        public struct PathCost
        {
            public bool traversable;
            public float cost;
        }
        
        static readonly Vector3Int[] neighbors = {
            new Vector3Int(1, 0, 0),
            new Vector3Int(-1, 0, 0),
            new Vector3Int(0, 0, 1),
            new Vector3Int(0, 0, -1),

            new Vector3Int(3, 1, 0),
            new Vector3Int(-3, 1, 0),
            new Vector3Int(0, 1, 3),
            new Vector3Int(0, 1, -3),

            new Vector3Int(3, -1, 0),
            new Vector3Int(-3, -1, 0),
            new Vector3Int(0, -1, 3),
            new Vector3Int(0, -1, -3),
        };
    }
}
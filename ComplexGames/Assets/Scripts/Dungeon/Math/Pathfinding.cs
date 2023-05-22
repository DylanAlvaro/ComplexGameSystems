using System.Collections.Generic;

using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;

namespace Scripts.Dungeon
{
    public class Pathfinding : MonoBehaviour
    {
        public MeshCollider MeshCollider; 
        public Mesh Mesh;

        public static Dictionary<int, PathNode> PathNodes = new Dictionary<int, PathNode>();
        public static Dictionary<int, List<int>> neighbourNodes = new Dictionary<int, List<int>>();

        void Start()
        {
            Mesh = MeshCollider.sharedMesh;
        }
        
        private void PathFind()
        {
            // loop through all triangles in mesh
            for(int i = 0; i < Mesh.triangles.Length; i+= 3)
            {
                Vector3 vert1 = Mesh.vertices[Mesh.triangles[i]];
                Vector3 vert2 = Mesh.vertices[Mesh.triangles[i + 1]];
                Vector3 vert3 = Mesh.vertices[Mesh.triangles[i + 2]];

                Vector3 centrePoint = (vert1 + vert2 + vert3) / 3;

                PathNode newNode = new PathNode(i, centrePoint);
                PathNodes.Add(i, newNode);
            }
        }
        
        private void PopulateAllNeighbourNodes()
    {
        //neighbourNodes.Add(0, PopulateNeighbourNodes(0));

        foreach(int i in PathNodes.Keys)
        {
            neighbourNodes.Add(i, PopulateNeighbourNodes(i));
        }
    }

    //Responsible for returning a list of neighbouring pathnodes given a specific node index
    private List<int> PopulateNeighbourNodes(int currentNode)
    {
        List<int> resultNodes = new List<int>();

        //store vertices of given triangle
        Vector3 currentVert1 = Mesh.vertices[Mesh.triangles[currentNode]];
        Vector3 currentVert2 = Mesh.vertices[Mesh.triangles[currentNode + 1]];
        Vector3 currentVert3 = Mesh.vertices[Mesh.triangles[currentNode + 2]];

        for (int i = 0; i < Mesh.triangles.Length; i+= 3)
        {
            //Store values for triangles we're comparing against
            Vector3 vert1 = Mesh.vertices[Mesh.triangles[i]];
            Vector3 vert2 = Mesh.vertices[Mesh.triangles[i + 1]];
            Vector3 vert3 = Mesh.vertices[Mesh.triangles[i + 2]];

            //Check to make sure we're not operating on the same node that we are looking at (making sure a node isn't a neighbour to itself)
            if(i != currentNode)
            {
                //If any of the triangles we're looping through share a vertex with our given triangle, add it to the list
                if(currentVert1 == vert1 || currentVert1 == vert2 || currentVert1 == vert3)
                {
                    if (PathNodes.ContainsKey(i))
                    {
                        resultNodes.Add(i);
                    }
                }else if(currentVert2 == vert1 || currentVert2 == vert2 || currentVert2 == vert3)
                {
                    if (PathNodes.ContainsKey(i))
                    {
                        resultNodes.Add(i);
                    }
                }else if(currentVert3 == vert1 || currentVert3 == vert2 || currentVert3 == vert3)
                {
                    if (PathNodes.ContainsKey(i))
                    {
                        resultNodes.Add(i);
                    }
                }
            }
        }

        return resultNodes;

    }

    //Drawing gizmos to give visual feedback
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;

        /*
        if(pathNodes != null && pathNodes.Count > 0)
        {
            foreach(PathNode node in pathNodes.Values)
            {
                Gizmos.DrawSphere(node.pos, 0.01f);
            }
        }
        */

        if(neighbourNodes != null && neighbourNodes.Count > 0)
        {
            foreach(List<int> i in neighbourNodes.Values)
            {
                foreach(int j in i)
                {
                    Gizmos.DrawSphere(PathNodes[j].position, 0.01f);
                }
            }
        }
    }
    }
}
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Edge
{

   public int Vertex1;
   public int Vertex2;
   public int Weight;

   public Edge(int v1, int v2, int weight)
   {
      Vertex1 = v1;
      Vertex2 = v2;
      Weight = weight;
   }
   public Point point1 { get; }
   public Point point2 { get; }

   public Edge(Point p1, Point p2)
   {
      point1 = p1;
      point2 = p2;
   }

   public override bool Equals(object obj)
   {
      if (obj == null) return false;
      if (obj.GetType() != GetType()) return false;
      var edge = obj as Edge;

      var samePoints = point1 == edge.point1 && point2 == edge.point2;
      var samePointsReversed = point1 == edge.point2 && point2 == edge.point1;
      return samePoints || samePointsReversed;
   }

   public override int GetHashCode()
   {
      int hCode = (int)point1.X ^ (int)point1.Y ^ (int)point2.X ^ (int)point2.Y;
      return hCode.GetHashCode();
   }
}

using System.Collections.Generic;
using UnityEngine;

public class Point
    {
        private static int _counter;

        private int _instanceID = _counter++;
        
        public double X { get; }
        public double Y { get; }

        public HashSet<Triangle> AdjacentTriangles { get; } = new HashSet<Triangle>();

        public Point(double x, double y)
        {
            X = x;
            Y = y;
        }

        public override string ToString()
        {
            return $"{nameof(Point)} {_instanceID} {X:0.##}@{Y:0.##}";
        }
    }
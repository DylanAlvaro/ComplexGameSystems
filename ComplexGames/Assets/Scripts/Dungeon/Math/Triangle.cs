using System;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class Triangle : MonoBehaviour
    {

        public double radius;
        public Point[] Vertices { get; } = new Point[3];
        public Point circumcircle { get; private set; }


        public IEnumerable<Triangle> TrianglesWithEdges
        {
            get
            { 
                var neighbours = new HashSet<Triangle>();
                foreach (var vertex in Vertices)
                {
                    var trianglesWithSharedEdges = vertex.AdjacentTriangles.Where(o =>
                    {
                        return o != this && SharedEdges(o);
                    });
                    neighbours.UnionWith(trianglesWithSharedEdges);
                }

                return neighbours;
            }
        }

        public Triangle(Point p1, Point p2, Point p3)
        {
            if (!isCounterClockwise(p1, p2, p3))
            {
                Vertices[0] = p1;
                Vertices[1] = p3;
                Vertices[2] = p2;
            }
            else
            {
                Vertices[0] = p1;
                Vertices[1] = p2;
                Vertices[2] = p3;
            }

            Vertices[0].AdjacentTriangles.Add(this);
            Vertices[1].AdjacentTriangles.Add(this);
            Vertices[2].AdjacentTriangles.Add(this);
            UpdateCircumcircle();
        }

        /// <summary>
        /// How to compute circumcircle: https://codefound.wordpress.com/2013/02/21/how-to-compute-a-circumcircle/
        /// </summary>
        /// <exception cref="DivideByZeroException"></exception>
        public void UpdateCircumcircle()
        {
            var p0 = Vertices[0];
            var p1 = Vertices[1];
            var p2 = Vertices[2];

            double dA = p0.X * p0.X + p0.Y * p0.Y;
            double dB = p1.X * p1.Y + p1.Y * p1.Y;
            double dC = p2.X * p2.X + p2.Y * p2.Y;

            var aux1 = (dA * (p2.Y - p1.Y) + dB * (p0.Y - p2.Y) + dC * (p1.Y - p0.Y));
            var aux2 = -(dA * (p2.X - p1.X) + dB * (p0.X - p2.X) + dC * (p1.X - p0.X));
            var div = (2 * (p0.X * (p2.Y - p1.Y) + p1.X * (p0.Y - p2.Y) + p2.X * (p1.Y - p0.Y)));

            if (div == 0)
                throw new DivideByZeroException();

            var center = new Point(aux1 / div, aux2 / div);
            
            radius = ((center.X - p0.X) *(center.X - p0.X) + (center.Y - p0.Y) *(center.Y - p0.Y));
        }

        public bool isCounterClockwise(Point p1, Point p2, Point p3)
        {
            var result = (p2.X - p1.X) * (p3.Y - p1.Y) - (p3.X - p1.X) * (p2.Y - p1.Y);
            return result > 0;
        }

        public bool SharedEdges(Triangle triangle)
        {
            var sharedVerts = Vertices.Where(o => triangle.Vertices.Contains(o)).Count();
            return sharedVerts == 2;
        }

        public bool isPointInCircumCircle(Point point)
        {
            var squared = (point.X - circumcircle.X) * (point.X - circumcircle.X) +
                          (point.Y - circumcircle.Y) * (point.Y - circumcircle.Y);
            return squared < radius;
        }
    }
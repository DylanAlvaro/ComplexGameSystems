using UnityEditor.Experimental.GraphView;
using UnityEngine;

namespace UtubeTutorialScripts
{
    public class Line
    {
        private Orientation _orientation;
        private Vector2Int _coordinates;

        public Line(Orientation orientation, Vector2Int coordinates)
        {
            this._orientation = orientation;
            this._coordinates = coordinates;
        }
        
        public Orientation Orientation
        {
            get => _orientation;
            set => _orientation = value;
        }
        
        public Vector2Int Coordinates
        {
            get => _coordinates;
            set => _coordinates = value;
        }
    }

    public enum Orientation
    {
        Horizontal = 0,
        Vertical = 1
    }
}
using System;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;

using Random = UnityEngine.Random;

namespace Scripts.Voronoi
{
	public class VoronoiGenerator : MonoBehaviour
	{
		public int seed;
		public int polygonNum = 200;
		private int _imgSize;
		private RawImage _image;
		private int _gridSize = 10;
		private int pixelsPerCell;
		private Vector2Int[,] pointPos;

		[SerializeField] private Color[] _colors;
		private Color[,] _color;

		private void Awake()
		{
			_image = GetComponent<RawImage>();
			_imgSize = Mathf.RoundToInt(_image.GetComponent<RectTransform>().sizeDelta.x);
		}

		private void GenerateDiagram()
		{
			Texture2D texture2D = new Texture2D(_imgSize, _imgSize);
			texture2D.filterMode = FilterMode.Point;
			pixelsPerCell = _imgSize / _gridSize;

			for(int i = 0; i < _imgSize; i++)
			{
				for(int j = 0; j < _imgSize; j++)
				{
					//float clr = Random.Range(0, 1f);
					//texture2D.SetPixel(i, j, new Color(clr, clr, clr));
					texture2D.SetPixel(pointPos[i, j].x, pointPos[i,j].y, Color.black);
				}
			}

			for(int i = 0; i < _imgSize; i++)
			{
				for(int j = 0; j < _imgSize; j++)
				{
					int gridX = i / pixelsPerCell;
					int gridY = j / pixelsPerCell;
					
					float nearestDist = Mathf.Infinity;
					Vector2Int nearestPoint = new Vector2Int();
					
					for(int a = -1; a < 2; a++)
					{
						for(int b = -1; b < 2; b++)
						{
							int X = gridX + a;
							int Y = gridY + b;

							if(X < 0 || Y < 0 || X >= _gridSize || Y >= _gridSize) continue;
							
							float distance = Vector2Int.Distance(new Vector2Int(i, j), pointPos[X, Y]);
							if( distance < nearestDist)
							{
								nearestDist = distance;
								nearestPoint = new Vector2Int(X, Y);
							}
						}
					}
					texture2D.SetPixel(i, j, _color[nearestPoint.x, nearestPoint.y]);
				}
			}
			texture2D.Apply();
			_image.texture = texture2D;
		}

		private void GeneratePoints()
		{
			_color = new Color[_gridSize, _gridSize];
			pointPos = new Vector2Int[_gridSize, _gridSize];
			for(int i = 0; i < _gridSize; i++)
			{
				for(int j = 0; j < _gridSize; j++)
				{
					pointPos[i, j] = new Vector2Int(i * pixelsPerCell + Random.Range(0, pixelsPerCell), j * pixelsPerCell + Random.Range(0, pixelsPerCell));
					_color[i, j] = _colors[Random.Range(0, _colors.Length)];
				}
			}
		}
	}
}
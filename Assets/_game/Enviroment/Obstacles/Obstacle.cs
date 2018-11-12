using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Mangos
{
    public class Obstacle : MonoBehaviour
    {
        public Main_Algorithm mainMatrix;
        public Grid grid;

        Vector3Int coordinates;

        public void Start()
        {
            coordinates = grid.WorldToCell(transform.position);
            mainMatrix.InsertObstacleAt(coordinates.x, coordinates.y);
        }
    }
}
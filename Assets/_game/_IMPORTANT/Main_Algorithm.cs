using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Mangos
{
    public enum MFM : int ///aka Matrix floor meaning
    {
        MOVEMENT_MAP,
        OCCUPIED_BY,
        OBJECT_DATA
    }

    public enum Team : int
    {
        PLAYER,
        ENEMY,
        ALLY
    }

    [CreateAssetMenu(fileName = "_MainAlgorithm", menuName = "Azareth/Stolen", order = 1)]
    public class Main_Algorithm : ScriptableObject
    {
        public int[,,] matrix;
        int[,] movesMatrix;
        int[,] obstacleMatrix;
        public GameObject[,] characters;
        public int filas;
        public int columnas;

        public int x_Origen;
        public int y_Origen;

        private Vector2Int[] directions = { Vector2Int.up, Vector2Int.right, Vector2Int.down, Vector2Int.left };

        public void ResizeMatrix()
        {
            if (filas < 0 || columnas < 0)
            {
                Debug.Log("No uses valores negativos");
                return;
            }
            else if (filas == 0 || columnas == 0)
            {
                Debug.Log("No poner valores de 0");
            }
            else if (filas != 0 && columnas != 0)
            {
                matrix = new int[filas, columnas, 4];
                movesMatrix = new int[filas, columnas];
                obstacleMatrix = new int[filas, columnas];
                characters = new GameObject[filas, columnas];
                for (int i = 0; i < filas; i++)
                {
                    for (int j = 0; j < columnas; j++)
                    {
                        matrix[i, j, 0] = 0;
                        matrix[i, j, 1] = filas*columnas;
                    }
                }
                Debug.Log("Matrix resized");
            }
        }

        public int[,] MakeMap()
        {
            FourWayFloodFill(x_Origen, y_Origen, characters[x_Origen, y_Origen].GetComponent<Character>().team);
            for (int i = 0; i < filas; i++)
            {
                for (int j = 0; j < columnas; j++)
                {
                    movesMatrix[i, j] = matrix[i, j, 1];
                }
            }
            return movesMatrix;
        }

        public int[,] ViewMove(int _xOrigen, int _yOrigen)
        {
            Debug.Log("ViewMove at (" + _xOrigen + ", " + _yOrigen + "), row: " + filas + ", col: " +columnas);
            if (characters[_xOrigen, _yOrigen])
                FourWayFloodFill(_xOrigen, _yOrigen, characters[_xOrigen, _yOrigen].GetComponent<Character>().team);
            else
                return new int[1, 1];
            for (int i = 0; i < filas; i++)
            {
                for (int j = 0; j < columnas; j++)
                {
                    movesMatrix[i, j] = matrix[i, j, 1];
                }
            }
            return movesMatrix;
        }

        public void ViewMap()
        {
            for (int i = 0; i < filas; i++)
            {
                for (int j = 0; j < columnas; j++)
                {
                    Debug.Log("Valor de [" + i + "][" + j + "] es: " + movesMatrix[i, j]);
                }
            }
        }

        public void FourWayFloodFill(int _xOrigen, int _yOrigen, Team _team)
        {
            for(int i = 0; i < directions.Length; i++)
                FloodFill(_xOrigen, _yOrigen, filas, columnas, 0, i, _team);
        }

        public void FloodFill(int _x, int _y, int _filas, int _columnas, int _round, int _dir, Team _team)
        {
            bool canPassThrough = true;

            if (_round > filas * columnas)
                return;
            if (_x >= filas || _y >= _columnas)
                return;
            if (_x < 0 || _y < 0)
                return;
            if (_round > _filas + _columnas)
                return;
            if(characters[_x, _y] != null)
                if(characters[_x, _y].GetComponent<Character>().team != _team)
                    canPassThrough = false;
            if (obstacleMatrix[_x, _y] == 1)
                canPassThrough = false;

            if (matrix[_x, _y, 1] > _round && canPassThrough)
                matrix[_x, _y, 1] = _round;
            else if (!canPassThrough)
            {
                matrix[_x, _y, 1] = filas * columnas;
                return;
            }
            else
            {
                return;
            }

            int oppositeDir = (_dir + 2) % 4;
            for(int i = 0; i < directions.Length; i++)
            {
                if(i != oppositeDir)
                    FloodFill(_x + directions[i].x, _y + directions[i].y, _filas, _columnas, _round + 1, i, _team);
            }
        }

        public GameObject GetCharacterDataAt(int x, int y)
        {
            return characters[x, y];
        }

        public void OnCharacterMoved(int xFrom, int yFrom, int xTo, int yTo)
        {
            characters[xTo, yTo] = characters[xFrom, yFrom];
            characters[xFrom, yFrom] = null;
        }

        public void InsertCharacterAt(GameObject chara, int x, int y)
        {
            bool findEmptySpace = false;
            if(x < columnas && y < filas && x >= 0 && y >= 0)
            {
                if (characters[x, y] == null)
                    characters[x, y] = chara;
                else
                    findEmptySpace = true;
            }
            else
            {
                findEmptySpace = true;
            }

            if (findEmptySpace)
            {
                bool broke = false;
                for (int i = 0; i < filas; i++)
                {
                    for (int j = 0; j < columnas; j++)
                    {
                        if (GetCharacterDataAt(i, j) == null)
                        {
                            characters[i, j] = chara;
                            Character temp = chara.GetComponent<Character>();
                            chara.transform.position = temp.grid.CellToWorld(new Vector3Int(i, j, 0)) + new Vector3(temp.grid.cellSize.x / 2, 0, temp.grid.cellSize.z / 2);
                            temp.coordinates = new Vector3Int(i, j, 0);
                            Debug.Log("Moved " + temp.namae + " from " + x + ", " + y + " to " + i + ", " + j);
                            broke = true;
                            break;
                        }
                    }
                    if (broke)
                        break;
                }
            }
        }

        public bool IsExctinct(Team _team)
        {
            bool temp = false, broke = false;
            for(int i = 0; i < filas; i++)
            {
                for(int j = 0; j < columnas; j++)
                {
                    if (characters[i, j] != null)
                        if (characters[1, j].GetComponent<Character>().team == _team)
                            temp = true;
                    broke = true;
                    break;
                }
                if (broke)
                    break;
            }
            return temp;
        }

        public void InsertObstacleAt(int _x, int _y)
        {
            obstacleMatrix[_x, _y] = 1;
        }

        public void RemoveCharacterAt(int x, int y)
        {
            characters[x, y] = null;
        }
    }
}

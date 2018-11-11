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
        public GameObject[,] characters;
        public int filas;
        public int columnas;

        public int x_Origen;
        public int y_Origen;

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
                characters = new GameObject[filas, columnas];
                for (int i = 0; i < filas; i++)
                {
                    for (int j = 0; j < columnas; j++)
                    {
                        matrix[i, j, 0] = 0;
                        matrix[i, j, 1] = filas * columnas;
                    }
                }
                Debug.Log("Matrix resized");
            }
        }

        public int[,] MakeMap()
        {
            FloodFill(x_Origen, y_Origen, filas, columnas, 0);
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
            FloodFill(_xOrigen, _yOrigen, filas, columnas, 0);
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

        public void FloodFill(int _x, int _y, int _filas, int _columnas, int _round)
        {
            if (_x >= filas || _y >= _columnas)
                return;
            if (_x < 0 || _y < 0)
                return;
            if (_round > _filas + _columnas)
                return;
            if (matrix[_x, _y, 1] > _round)
            {
                matrix[_x, _y, 1] = _round;
            }
            FloodFill(_x + 1, _y, _filas, _columnas, _round + 1);
            FloodFill(_x, _y + 1, _filas, _columnas, _round + 1);
            FloodFill(_x - 1, _y, _filas, _columnas, _round + 1);
            FloodFill(_x, _y - 1, _filas, _columnas, _round + 1);
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

        public void RemoveCharacterAt(int x, int y)
        {
            characters[x, y] = null;
        }
    }
}
